namespace Model
{
    /// <summary>
    /// 数量核对弹窗返回给看板的决定。
    /// </summary>
    public enum VerifyDecision
    {
        /// <summary>
        /// 实收与应发一致，当前明细可以结束。
        /// </summary>
        Confirm = 0,

        /// <summary>
        /// 实收少于应发，需要按差额继续抓取。
        /// </summary>
        Refill = 1,

        /// <summary>
        /// 实收多于应发，操作员已确认拿走多余药品。
        /// </summary>
        ReleaseExcess = 2,

        /// <summary>
        /// 当前明细作废，抓取次数清零后重新开始。
        /// </summary>
        Reset = 3
    }
}
