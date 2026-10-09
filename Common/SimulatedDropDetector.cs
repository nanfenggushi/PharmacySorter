using System;

namespace Common
{
    /// <summary>
    /// 按配置成功率模拟落药。每次投放独立判断一次。
    /// </summary>
    public class SimulatedDropDetector : IDropDetector
    {
        private readonly int successPercent;
        private readonly Random random = new Random();

        public SimulatedDropDetector(int successPercent)
        {
            if (successPercent < 0)
            {
                successPercent = 0;
            }
            else if (successPercent > 100)
            {
                successPercent = 100;
            }

            this.successPercent = successPercent;
        }

        public string SourceName
        {
            get { return "模拟落药（成功率 " + successPercent + "%）"; }
        }

        public void Open()
        {
        }

        public void Close()
        {
        }

        public void Arm()
        {
        }

        public bool WaitForDrop(int timeoutMilliseconds)
        {
            lock (random)
            {
                return random.Next(100) < successPercent;
            }
        }
    }
}
