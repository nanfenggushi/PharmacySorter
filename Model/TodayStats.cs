namespace Model
{
    /// <summary>
    /// 主界面导航栏“今日统计”的一组数字。待配任务数是实时值，其余按当天零点起算。
    /// </summary>
    public class TodayStats
    {
        /// <summary>
        /// 当前处于待配药状态的任务数。
        /// </summary>
        public int WaitingCount { get; set; }

        /// <summary>
        /// 今天完成的任务单数。
        /// </summary>
        public int CompletedToday { get; set; }

        /// <summary>
        /// 今天创建的任务累计确认落药的盒数。
        /// </summary>
        public int DroppedToday { get; set; }

        /// <summary>
        /// 今天创建的任务中处于部分异常状态的单数。
        /// </summary>
        public int AbnormalToday { get; set; }
    }
}
