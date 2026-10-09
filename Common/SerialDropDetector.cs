using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace Common
{
    /// <summary>
    /// 独立串口上的落药传感器。投放前清空缓冲，投放后只在时间窗内认一次信号。
    /// 传感器发送包含约定标记的一行文本，例如 DROP。
    /// </summary>
    public class SerialDropDetector : IDropDetector
    {
        private readonly object gate = new object();
        private readonly string portName;
        private readonly int baudRate;
        private readonly int dataBits;
        private readonly Parity parity;
        private readonly StopBits stopBits;
        private readonly string dropToken;
        private SerialPort port;
        private readonly StringBuilder received = new StringBuilder();
        private bool dropDetected;

        public SerialDropDetector(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits, string dropToken)
        {
            if (string.IsNullOrWhiteSpace(portName))
            {
                throw new ArgumentException("请配置落药传感器串口号");
            }

            this.portName = portName.Trim();
            this.baudRate = baudRate;
            this.dataBits = dataBits;
            this.parity = parity;
            this.stopBits = stopBits;
            this.dropToken = string.IsNullOrWhiteSpace(dropToken) ? "DROP" : dropToken.Trim();
        }

        public string SourceName
        {
            get { return "落药传感器 " + portName; }
        }

        public void Open()
        {
            lock (gate)
            {
                if (port != null && port.IsOpen)
                {
                    return;
                }

                SerialPort serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
                serialPort.Encoding = Encoding.ASCII;
                serialPort.NewLine = "\n";
                serialPort.DataReceived += Port_DataReceived;
                serialPort.Open();
                port = serialPort;
                received.Clear();
                dropDetected = false;
            }
        }

        public void Close()
        {
            lock (gate)
            {
                if (port == null)
                {
                    return;
                }

                port.DataReceived -= Port_DataReceived;
                if (port.IsOpen)
                {
                    port.Close();
                }

                port.Dispose();
                port = null;
            }
        }

        public void Arm()
        {
            lock (gate)
            {
                if (port == null || !port.IsOpen)
                {
                    throw new InvalidOperationException("落药传感器未连接");
                }

                port.DiscardInBuffer();
                received.Clear();
                dropDetected = false;
            }
        }

        public bool WaitForDrop(int timeoutMilliseconds)
        {
            int remaining = Math.Max(timeoutMilliseconds, 0);
            while (remaining > 0)
            {
                lock (gate)
                {
                    if (dropDetected)
                    {
                        dropDetected = false;
                        return true;
                    }
                }

                int slice = remaining < 20 ? remaining : 20;
                Thread.Sleep(slice);
                remaining -= slice;
            }

            return false;
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            lock (gate)
            {
                if (port == null || !port.IsOpen)
                {
                    return;
                }

                received.Append(port.ReadExisting());
                if (received.ToString().IndexOf(dropToken, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    dropDetected = true;
                    received.Clear();
                }
                else if (received.Length > 256)
                {
                    received.Remove(0, received.Length - 64);
                }
            }
        }
    }
}
