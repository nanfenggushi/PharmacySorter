using System;
using System.Configuration;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace Common
{
    /// <summary>
    /// 机械臂串口服务。负责打开串口、下发宏指令，并按动作耗时阻塞等待。
    /// 未连接串口时不允许下发动作，避免机械臂没有执行却把配药流程走完。
    /// </summary>
    public class ArmCommandService
    {
        /// <summary>
        /// 开机复位和急停使用的动作组编号，对应控制器 INI 中的 G0002。
        /// G0002 不是 $DGT 动作组，必须把该帧的舵机脉宽直接拼成串口指令。
        /// </summary>
        public const string StandbyActionName = "G0002";

        /// <summary>
        /// G0002 的完整待命指令。
        /// </summary>
        public const string StandbyCommand =
            "#000P1500T1000!" +
            "#001P1500T1000!" +
            "#002P2300T1000!" +
            "#003P1000T1000!" +
            "#004P1500T1000!" +
            "#005P1500T1000!";

        /// <summary>
        /// 待命动作里最长的单舵机时间是 1000 毫秒。急停和开机复位按这个时间等待。
        /// </summary>
        public const int StandbyWaitMilliseconds = 1000;

        /// <summary>
        /// 等待过程中检查急停标记的间隔。间隔太长时，急停按钮会显得没有立刻生效。
        /// </summary>
        private const int StopCheckIntervalMilliseconds = 50;

        private readonly object gate = new object();
        private SerialPort port;

        /// <summary>
        /// 为 true 时不阻塞等待。没有接机械臂时用它把配药流程快速走完。
        /// </summary>
        public bool SkipWait { get; set; }

        /// <summary>
        /// 急停标记。为 true 时，正在进行的动作等待会提前结束，后续发送也会立即返回。
        /// </summary>
        private volatile bool stopRequested;


        /// <summary>
        /// 当前是否已经打开串口。
        /// </summary>
        public bool IsConnected
        {
            get { return port != null && port.IsOpen; }
        }

        /// <summary>
        /// 当前串口名。未连接时返回空字符串。
        /// </summary>
        public string PortName
        {
            get { return IsConnected ? port.PortName : string.Empty; }
        }

        /// <summary>
        /// 按 App.config 中的端口、波特率、校验位、数据位和停止位打开串口。
        /// 已经连接同一端口时不重复打开。
        /// </summary>
        public void Connect()
        {
            Connect(ConfigurationManager.AppSettings["ArmPortName"]);
        }

        /// <summary>
        /// 打开串口。已经连接同一端口时不重复打开。
        /// </summary>
        public void Connect(string portName)
        {
            if (string.IsNullOrWhiteSpace(portName))
            {
                throw new ArgumentException("请配置串口号");
            }

            int baudRate = ReadIntSetting("ArmBaudRate", 115200);
            int dataBits = ReadIntSetting("ArmDataBits", 8);
            Parity parity = ReadParitySetting();
            StopBits stopBits = ReadStopBitsSetting();

            lock (gate)
            {
                if (IsConnected && string.Equals(port.PortName, portName, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                ClosePort();
                SerialPort serialPort = new SerialPort(portName.Trim(), baudRate, parity, dataBits, stopBits);
                serialPort.Encoding = Encoding.ASCII;
                serialPort.NewLine = "\r\n";
                serialPort.Open();
                port = serialPort;
            }
        }

        /// <summary>
        /// 读取整数配置。配置缺失或不是整数时使用默认值。
        /// </summary>
        private static int ReadIntSetting(string key, int defaultValue)
        {
            int value;
            if (int.TryParse(ConfigurationManager.AppSettings[key], out value) && value > 0)
            {
                return value;
            }

            return defaultValue;
        }

        /// <summary>
        /// 读取校验位。可选 None、Odd、Even、Mark、Space。
        /// </summary>
        private static Parity ReadParitySetting()
        {
            Parity parity;
            if (Enum.TryParse(ConfigurationManager.AppSettings["ArmParity"], true, out parity))
            {
                return parity;
            }

            return Parity.None;
        }

        /// <summary>
        /// 读取停止位。可选 None、One、Two、OnePointFive。
        /// </summary>
        private static StopBits ReadStopBitsSetting()
        {
            StopBits stopBits;
            if (Enum.TryParse(ConfigurationManager.AppSettings["ArmStopBits"], true, out stopBits))
            {
                return stopBits;
            }

            return StopBits.One;
        }

        /// <summary>
        /// 关闭串口。重复调用没有影响。
        /// </summary>
        public void Disconnect()
        {
            lock (gate)
            {
                ClosePort();
            }
        }

        /// <summary>
        /// 请求停止。正在 Sleep 的抓取或投递会在下一个检查点结束，不再把整段延时睡完。
        /// </summary>
        public void RequestStop()
        {
            lock (gate)
            {
                stopRequested = true;
            }
        }

        /// <summary>
        /// 清除急停标记。新一轮配药开始前必须调用，否则后续动作会被直接跳过。
        /// </summary>
        public void ClearStop()
        {
            lock (gate)
            {
                stopRequested = false;
            }
        }

        /// <summary>
        /// 下发 G0002 待命指令，并等待动作完成。急停期间不再重复下发。
        /// </summary>
        public void SendStandby()
        {
            Send(StandbyCommand, StandbyWaitMilliseconds);
        }

        /// <summary>
        /// 下发一条指令，并按给定毫秒数等待机械臂做完。
        /// </summary>
        /// <param name="command">$DGT 动作组，或 G0002 拼出的舵机指令。</param>
        /// <param name="waitMilliseconds">动作预估耗时。小于 0 时按 0 处理。</param>
        public void Send(string command, int waitMilliseconds)
        {
            if (IsStopRequested())
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(command))
            {
                throw new ArgumentException("宏指令不能为空");
            }

            string trimmed = command.Trim();
            if (trimmed.StartsWith("$DGT:", StringComparison.OrdinalIgnoreCase))
            {
                SendActionGroup(trimmed, waitMilliseconds);
                return;
            }

            SendFrame(trimmed, waitMilliseconds);
        }

        /// <summary>
        /// 把 $DGT:起始-结束,次数! 展开成逐帧舵机指令。
        /// </summary>
        private void SendActionGroup(string command, int waitMilliseconds)
        {
            int colon = command.IndexOf(':');
            int comma = command.IndexOf(',');
            int dash = command.IndexOf('-');
            int endMark = command.IndexOf('!');
            int start;
            int end;
            if (colon < 0 || dash < 0 || comma < 0 || endMark < 0
                || !int.TryParse(command.Substring(colon + 1, dash - colon - 1), out start)
                || !int.TryParse(command.Substring(dash + 1, comma - dash - 1), out end)
                || start < 0 || end < start)
            {
                throw new ArgumentException("动作组格式不正确：" + command);
            }

            int frameCount = end - start + 1;
            int eachWait = frameCount == 0 ? 0 : Math.Max(waitMilliseconds, 0) / frameCount;
            for (int index = start; index <= end; index++)
            {
                if (IsStopRequested())
                {
                    return;
                }

                string frame = GetServoFrame(index);
                SendFrame(frame, eachWait);
            }
        }

        /// <summary>
        /// 下发一帧舵机指令，并等待这一帧做完。
        /// </summary>
        private void SendFrame(string command, int waitMilliseconds)
        {
            int wait = SkipWait || waitMilliseconds < 0 ? 0 : waitMilliseconds;
            lock (gate)
            {
                if (!IsConnected)
                {
                    throw new InvalidOperationException("机械臂串口未连接，不能下发动作");
                }

                port.Write(command.Trim());
            }

            Wait(wait);
        }

        /// <summary>
        /// 取控制器动作组里的一帧。编号对应 INI 的 G0003 到 G0027。
        /// 只保留 0 到 5 号舵机，未安装的舵机不发给控制板。
        /// </summary>
        private static string GetServoFrame(int index)
        {
            switch (index)
            {
                case 3: return "#000P1500T1000!#001P1500T1000!#002P1980T1000!#003P0850T1000!#004P1500T1000!#005P1500T1000!";
                case 4: return "#000P1500T1000!#001P1250T1000!#002P2000T1000!#003P1000T1000!#004P1500T1000!#005P1200T1000!";
                case 5: return "#000P1500T1000!#001P1250T1000!#002P2000T1000!#003P1000T1000!#004P1500T1000!#005P1800T1000!";
                case 6: return "#000P1500T1000!#001P1300T1000!#002P1900T1000!#003P1000T1000!#004P1500T1000!#005P1800T1000!";
                case 7: return "#000P1500T1000!#001P2100T2000!#002P2100T1000!#003P0850T2000!#004P1500T1000!#005P1800T1000!";
                case 8: return "#000P1500T1000!#001P2100T1000!#002P2100T1000!#003P0850T1000!#004P1500T1000!#005P1800T1000!";
                case 9: return "#000P1500T1000!#001P1000T2000!#002P1600T2000!#003P1000T2000!#004P1500T1000!#005P1800T1000!";
                case 10: return "#000P1500T1000!#001P1000T1000!#002P1600T1000!#003P1000T1000!#004P1500T1000!#005P1200T1000!";
                case 11: return "#000P1500T1000!#001P2100T2000!#002P2100T2000!#003P0850T2000!#004P1500T1000!#005P1200T1000!";
                case 12: return "#000P1500T1000!#001P1500T1000!#002P1980T1000!#003P0850T1000!#004P1500T1000!#005P1500T1000!";
                case 13: return "#000P2000T1500!#001P1500T1500!#002P1980T1500!#003P0850T1500!#004P1500T1500!#005P1200T1500!";
                case 14: return "#000P2000T1000!#001P1250T1500!#002P2000T1000!#003P1000T1000!#004P1500T1000!#005P1200T1000!";
                case 15: return "#000P2000T1000!#001P1250T1000!#002P2000T1000!#003P1000T1000!#004P1500T1000!#005P1800T1000!";
                case 16: return "#000P2000T1500!#001P2100T1500!#002P2100T1500!#003P0850T1500!#004P1500T1500!#005P1800T1500!";
                case 17: return "#000P1500T1000!#001P2100T1000!#002P2100T1000!#003P0850T1000!#004P1500T1000!#005P1800T1000!";
                case 23: return "#000P1000T1000!#001P1500T1000!#002P1980T1000!#003P0850T1000!#004P1500T1000!#005P1200T1000!";
                case 24: return "#000P1000T1000!#001P1250T1000!#002P2000T1000!#003P1000T1000!#004P1500T1000!#005P1200T1000!";
                case 25: return "#000P1000T1000!#001P1250T1000!#002P2000T1000!#003P1000T1000!#004P1500T1000!#005P1800T1000!";
                case 26: return "#000P1000T1500!#001P2100T1500!#002P2100T1500!#003P0850T1500!#004P1500T1500!#005P1800T1500!";
                case 27: return "#000P1500T1000!#001P2100T1000!#002P2100T1000!#003P0850T1000!#004P1500T1000!#005P1800T1000!";
                default: throw new ArgumentException("没有动作组 " + index.ToString("000") + " 的舵机指令");
            }
        }

        /// <summary>
        /// 分段等待，方便急停在动作还没做完时打断。
        /// </summary>
        private void Wait(int waitMilliseconds)
        {
            int remaining = waitMilliseconds;
            while (remaining > 0 && !IsStopRequested())
            {
                int slice = remaining < StopCheckIntervalMilliseconds ? remaining : StopCheckIntervalMilliseconds;
                Thread.Sleep(slice);
                remaining -= slice;
            }
        }

        /// <summary>
        /// 是否请求停止
        /// </summary>
        /// <returns></returns>
        private bool IsStopRequested()
        {
            lock (gate)
            {
                return stopRequested;
            }
        }

        /// <summary>
        /// 断开串口连接
        /// </summary>
        private void ClosePort()
        {
            if (port == null)
            {
                return;
            }

            if (port.IsOpen)
            {
                port.Close();
            }

            port.Dispose();
            port = null;
        }
    }
}
