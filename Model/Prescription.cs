using System;

namespace Model
{
    /// <summary>
    /// 固定处方。和药品一样可以反复选用，不绑定患者，也不记录某一次配药进度。
    /// </summary>
    public class Prescription
    {
        public int PrescriptionId { get; set; }

        /// <summary>
        /// 处方名称。全库唯一，待配时按这个名称选择。
        /// </summary>
        public string PrescriptionName { get; set; }

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 处方包含的药品条数，只用于列表展示。
        /// </summary>
        public int ItemCount { get; set; }

        public string CreateTimeText
        {
            get { return CreateTime.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
    }
}
