namespace Model
{
    /// <summary>
    /// 物理工位。药品字典只使用左侧药位和右侧药位。
    /// </summary>
    public class Station
    {
        /// <summary>
        /// 工位编号。null 表示未绑定。
        /// </summary>
        public int? StationId { get; set; }

        /// <summary>
        /// 工位名称，用于下拉框显示。
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// 该工位的抓取宏指令，例如 $DGT:13-17,1!。空字符串表示未配置。
        /// </summary>
        public string GrabCommand { get; set; }

        /// <summary>
        /// 该工位的放置宏指令。药位通常为空，正前分拣槽用于投递。
        /// </summary>
        public string DropCommand { get; set; }

        /// <summary>
        /// 动作预估耗时，单位毫秒。配药时线程按这个时间等待机械臂做完。
        /// </summary>
        public int EstTimeMs { get; set; }

        /// <summary>
        /// 下拉框未设置 DisplayMember 时，用名称作为显示文本。
        /// </summary>
        public override string ToString()
        {
            return StationName ?? string.Empty;
        }
    }
}