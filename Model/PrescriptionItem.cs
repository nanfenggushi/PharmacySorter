namespace Model
{
    /// <summary>
    /// 固定处方中的一种药品和数量。不记录抓取进度。
    /// </summary>
    public class PrescriptionItem
    {
        public int ItemId { get; set; }

        public int PrescriptionId { get; set; }

        public int DrugId { get; set; }

        public string DrugName { get; set; }

        public string Spec { get; set; }

        /// <summary>
        /// 处方要求抓取的数量。
        /// </summary>
        public int RequiredQty { get; set; }
    }
}