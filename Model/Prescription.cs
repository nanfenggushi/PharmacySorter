using System;

namespace Model
{
    /// <summary>
    /// 处方。SortNo 越小越优先，待配队列按它和接收时间排序。
    /// </summary>
    public class Prescription
    {
        public int PrescriptionId { get; set; }

        public string PatientNo { get; set; }

        /// <summary>
        /// 待配药、配药中、部分异常、已完成、已撤销。
        /// </summary>
        public string Status { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? CompleteTime { get; set; }

        /// <summary>
        /// 队列顺序。数值越小越靠前，置顶时取当前最小序号再减一。
        /// </summary>
        public int SortNo { get; set; }

        /// <summary>
        /// 该处方包含的药品条数，仅用于队列展示。
        /// </summary>
        public int ItemCount { get; set; }

        public string CreateTimeText
        {
            get { return CreateTime.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
    }
}