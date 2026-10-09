using System;
using System.Configuration;

namespace Common
{
    /// <summary>
    /// 按 App.config 创建落药检测。模拟关闭时使用独立传感器串口。
    /// </summary>
    public static class DropDetectorFactory
    {
        public static IDropDetector Create()
        {
            if (ReadBool("DropSimulate", false))
            {
                return new SimulatedDropDetector(ReadPercent("DropSuccessPercent", 100));
            }

            return new SerialDropDetector(
                ConfigurationManager.AppSettings["SensorPortName"],
                ReadInt("SensorBaudRate", 9600),
                ReadInt("SensorDataBits", 8),
                ReadParity(),
                ReadStopBits(),
                ConfigurationManager.AppSettings["SensorDropToken"]);
        }

        private static bool ReadBool(string key, bool defaultValue)
        {
            bool value;
            if (bool.TryParse(ConfigurationManager.AppSettings[key], out value))
            {
                return value;
            }

            return defaultValue;
        }

        private static int ReadInt(string key, int defaultValue)
        {
            int value;
            if (int.TryParse(ConfigurationManager.AppSettings[key], out value) && value > 0)
            {
                return value;
            }

            return defaultValue;
        }

        private static int ReadPercent(string key, int defaultValue)
        {
            int value;
            if (int.TryParse(ConfigurationManager.AppSettings[key], out value))
            {
                if (value < 0)
                {
                    return 0;
                }

                return value > 100 ? 100 : value;
            }

            return defaultValue;
        }

        private static System.IO.Ports.Parity ReadParity()
        {
            System.IO.Ports.Parity parity;
            if (System.Enum.TryParse(ConfigurationManager.AppSettings["SensorParity"], true, out parity))
            {
                return parity;
            }

            return System.IO.Ports.Parity.None;
        }

        private static System.IO.Ports.StopBits ReadStopBits()
        {
            System.IO.Ports.StopBits stopBits;
            if (System.Enum.TryParse(ConfigurationManager.AppSettings["SensorStopBits"], true, out stopBits))
            {
                return stopBits;
            }

            return System.IO.Ports.StopBits.One;
        }
    }
}
