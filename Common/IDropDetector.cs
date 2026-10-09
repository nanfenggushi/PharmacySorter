namespace Common
{
    /// <summary>
    /// 落药检测。投放结束后只回答这一盒有没有掉进分拣槽。
    /// 真实传感器和配置模拟都实现这个接口，配药流程不用分叉。
    /// </summary>
    public interface IDropDetector
    {
        /// <summary>
        /// 当前检测来源，只用于日志。
        /// </summary>
        string SourceName { get; }

        /// <summary>
        /// 打开检测通道。模拟模式不连接硬件。
        /// </summary>
        void Open();

        /// <summary>
        /// 关闭检测通道。重复调用没有影响。
        /// </summary>
        void Close();

        /// <summary>
        /// 清空旧信号。必须在投放动作开始前调用，避免上一盒药被算进这一次。
        /// </summary>
        void Arm();

        /// <summary>
        /// 在时间窗内等待一次落药。急停或超时返回 false。
        /// </summary>
        bool WaitForDrop(int timeoutMilliseconds);
    }
}
