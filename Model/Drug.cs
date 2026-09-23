namespace Model
{
    /// <summary>
    /// 药品字典。StationName 仅用于界面展示，不对应 Drug 表字段。
    /// </summary>
    public class Drug
    {
        public int DrugId { get; set; }

        public string DrugName { get; set; }

        public string Spec { get; set; }

        public int? StationId { get; set; }

        public string StationName { get; set; }

        /// <summary>
        /// true 启用，false 停用（软删除）。
        /// </summary>
        public bool IsActive { get; set; }

        public string StationText
        {
            get { return string.IsNullOrEmpty(StationName) ? "未绑定" : StationName; }
        }

        public string StatusText
        {
            get { return IsActive ? "启用" : "停用"; }
        }
    }
}