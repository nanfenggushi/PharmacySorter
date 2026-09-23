using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace Common
{
    /// <summary>
    /// 机械臂宏指令发送。没有串口时仍按工位延时等待，方便在没有硬件时演示流程。
    /// </summary>
    public class ArmCommandService
    {
        /// <summary>
        /// 开机复位使用的动作帧。控制器收到后抬臂并张开夹爪。
        /// </summary>
        public const string ResetCommand = "#000P1500T1000!";

        private readonly object gate = new object();
        private SerialPort port;

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
        /// 下发一条宏指令，并按给定毫秒数等待机械臂做完。
        /// 串口未连接时不抛错，只等待，避免演示流程被硬件挡住。
        /// </summary>
        public void Send(string command, int waitMilliseconds)
        {
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

            if (wait > 0)
            {
                Thread.Sleep(wait);
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
