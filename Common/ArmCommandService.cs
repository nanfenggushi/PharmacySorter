using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace Common
{
    /// <summary>
    /// 机械臂串口服务。负责打开串口、下发宏指令，并按动作耗时阻塞等待。
    /// 没有连接硬件时只等待、不抛错，方便在电脑上把配药流程走完。
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
        /// 打开串口。已经连接同一端口时不重复打开。
        /// </summary>
        public void Connect(string portName)
        {
            if (string.IsNullOrWhiteSpace(portName))
            {
                throw new ArgumentException("请配置串口号");
            }

            lock (gate)
            {
                if (IsConnected && string.Equals(port.PortName, portName, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                ClosePort();
                SerialPort serialPort = new SerialPort(portName.Trim(), 115200, Parity.None, 8, StopBits.One);
                serialPort.Encoding = Encoding.ASCII;
                serialPort.NewLine = "\r\n";
                serialPort.Open();
                port = serialPort;
            }
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
            stopRequested = true;
        }

        /// <summary>
        /// 清除急停标记。新一轮配药开始前必须调用，否则后续动作会被直接跳过。
        /// </summary>
        public void ClearStop()
        {
            stopRequested = false;
        }

        /// <summary>
        /// 下发 G0002 待命指令，并等待动作完成。急停期间不再重复下发。
        /// </summary>
        public void SendStandby()
        {
            Send(StandbyCommand, StandbyWaitMilliseconds);
        }

        /// <summary>
        /// 下发一条宏指令，并按给定毫秒数等待机械臂做完。
        /// 串口未连接时不抛错，只等待，避免演示流程被硬件挡住。
        /// 等待途中收到急停时提前返回，调用方要自己停止后续循环。
        /// </summary>
        /// <param name="command">$DGT 动作组，或 G0002 拼出的舵机指令。</param>
        /// <param name="waitMilliseconds">动作预估耗时。小于 0 时按 0 处理。</param>
        public void Send(string command, int waitMilliseconds)
        {
            if (stopRequested)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(command))
            {
                throw new ArgumentException("宏指令不能为空");
            }

            int wait = waitMilliseconds < 0 ? 0 : waitMilliseconds;
            lock (gate)
            {
                if (IsConnected)
                {
                    port.WriteLine(command.Trim());
                }
            }

            Wait(wait);
        }

        /// <summary>
        /// 分段等待，方便急停在动作还没做完时打断。
        /// </summary>
        private void Wait(int waitMilliseconds)
        {
            int remaining = waitMilliseconds;
            while (remaining > 0 && !stopRequested)
            {
                int slice = remaining < StopCheckIntervalMilliseconds ? remaining : StopCheckIntervalMilliseconds;
                Thread.Sleep(slice);
                remaining -= slice;
            }
        }


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
