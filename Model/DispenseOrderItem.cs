namespace Model
{
    /// <summary>
    /// 一次待配任务的明细。抓取次数和核对结果都记在这里。
    /// </summary>
    public class DispenseOrderItem
    {
        public int ItemId { get; set; }

        public int OrderId { get; set; }

        public int DrugId { get; set; }

        public string DrugName { get; set; }

        public string Spec { get; set; }

        /// <summary>
        /// 处方要求抓取的数量。加入队列时从固定处方复制。
        /// </summary>
        public int RequiredQty { get; set; }

        /// <summary>
        /// 传感器确认落进分拣槽的数量。新建明细默认为 0。
        /// </summary>
        public int ActualQty { get; set; }

        /// <summary>
        /// 已经下发过的抓取次数。看板“已执行抓取次数”绑定这个字段。
        /// </summary>
        public int GrabCount { get; set; }

        /// <summary>
        /// 药品当前绑定的工位编号。2 为左侧药位，3 为右侧药位。
        /// </summary>
        public int? StationId { get; set; }

        /// <summary>
        /// 目标工位名称，只用于看板展示。
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// 未绑定工位时，表格明确显示原因，避免空白。
        /// </summary>
        public string StationText
        {
            get { return string.IsNullOrEmpty(StationName) ? "未绑定" : StationName; }
        }

        /// <summary>
        /// 待取药、取药中、核对通过、异常。
        /// </summary>
        public string Status { get; set; }
    }
}
