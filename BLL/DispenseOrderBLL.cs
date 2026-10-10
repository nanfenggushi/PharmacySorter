using DAL;
using Model;
using System;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// 待配任务。加入队列时复制固定处方的药品和数量，之后改处方不影响已加入的任务。
    /// </summary>
    public partial class DispenseOrderBLL
    {
        private readonly DispenseOrderDAL dal = new DispenseOrderDAL();
        private readonly PrescriptionDAL prescriptionDal = new PrescriptionDAL();
        private readonly DrugDAL drugDal = new DrugDAL();
        private readonly AppLogBLL logBll = new AppLogBLL();

        public List<DispenseOrder> GetWaitingQueue()
        {
            return dal.GetWaitingQueue();
        }

        /// <summary>
        /// 导航栏“今日统计”。待配任务数是实时值，其余按当天零点起算。
        /// </summary>
        public TodayStats GetTodayStats()
        {
            return dal.GetTodayStats(DateTime.Today);
        }

        public List<DispenseOrder> SearchHistory(string orderIdText, string prescriptionName, DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue && startDate.Value.Date > endDate.Value.Date)
            {
                throw new ArgumentException("开始日期不能晚于结束日期");
            }

            DateTime? startTime = startDate.HasValue ? startDate.Value.Date : (DateTime?)null;
            DateTime? endTime = endDate.HasValue ? endDate.Value.Date.AddDays(1) : (DateTime?)null;
            return dal.SearchHistory(ParseOrderId(orderIdText), prescriptionName, startTime, endTime);
        }

        public int AddToQueue(string patientNo, int prescriptionId)
        {
            patientNo = NormalizePatientNo(patientNo);
            Prescription prescription = prescriptionDal.GetById(prescriptionId);
            if (prescription == null)
            {
                throw new ArgumentException("请选择固定处方");
            }
            List<PrescriptionItem> items = prescriptionDal.GetItems(prescriptionId);
            if (items.Count == 0)
            {
                throw new ArgumentException("该处方没有药品明细");
            }
            EnsureItemsDispensable(items);

            int orderId = dal.AddFromPrescription(prescriptionId, patientNo, dal.GetNextSortNo());
            logBll.Add(null, AppLogType.Prescription,
                "加入待配队列 " + orderId + "，患者 " + patientNo + "，处方 " + prescription.PrescriptionName);
            return orderId;
        }

        /// <summary>
        /// 处方保存时校验过药品可用，但之后药品可能被停用或改绑。
        /// 入队前再校验一次，不通过就拒绝加入，避免生成一条永远配不了的任务。
        /// </summary>
        private void EnsureItemsDispensable(IList<PrescriptionItem> items)
        {
            foreach (PrescriptionItem item in items)
            {
                Drug drug = drugDal.GetById(item.DrugId);
                if (drug == null || !drug.IsActive)
                {
                    throw new ArgumentException("【" + item.DrugName + "】已停用或不存在，请先在药品字典中处理后再加入队列");
                }
                if (drug.StationId != 2 && drug.StationId != 3)
                {
                    throw new ArgumentException("【" + item.DrugName + "】没有绑定左侧或右侧药位，请先在药品字典中绑定后再加入队列");
                }
            }
        }

        public void MoveToTop(int orderId)
        {
            DispenseOrder order = EnsureWaiting(orderId);
            int sortNo = dal.GetMinWaitingSortNo() - 1;
            if (dal.UpdateSortNo(orderId, sortNo) == 0)
            {
                throw new ArgumentException("任务状态已变化，不能置顶");
            }
            logBll.Add(null, AppLogType.Prescription, "待配任务 " + order.OrderId + " 已置顶");
        }

        public void Cancel(int orderId)
        {
            DispenseOrder order = EnsureWaiting(orderId);
            if (dal.Cancel(orderId) == 0)
            {
                throw new ArgumentException("任务状态已变化，不能撤销");
            }
            logBll.Add(null, AppLogType.Prescription,
                "撤销待配任务 " + order.OrderId + "，患者 " + order.PatientNo + "，处方 " + order.PrescriptionName);
        }

        public DispenseOrder GetById(int orderId)
        {
            return dal.GetById(orderId);
        }

        public List<DispenseOrderItem> GetItems(int orderId)
        {
            return dal.GetItems(orderId);
        }

        /// <summary>
        /// 作废当前任务。配药中或部分异常的任务配不下去时用它让出队首，
        /// 剩余明细保留原进度，历史查询仍能看到这条记录。
        /// </summary>
        public void Abandon(int orderId)
        {
            DispenseOrder order = dal.GetById(orderId);
            if (order == null)
            {
                throw new ArgumentException("待配任务不存在");
            }
            if (order.Status != "待配药" && order.Status != "配药中" && order.Status != "部分异常")
            {
                throw new ArgumentException("该任务当前状态不能作废");
            }
            if (dal.Abandon(orderId) == 0)
            {
                throw new ArgumentException("任务状态已变化，不能作废");
            }
            logBll.Add(null, AppLogType.Prescription,
                "作废任务 " + order.OrderId + "，患者 " + order.PatientNo + "，处方 " + order.PrescriptionName);
        }

        private DispenseOrder EnsureWaiting(int orderId)
        {
            DispenseOrder order = dal.GetById(orderId);
            if (order == null)
            {
                throw new ArgumentException("待配任务不存在");
            }
            if (order.Status != "待配药")
            {
                throw new ArgumentException("只有待配药任务可以调整");
            }
            return order;
        }

        private static int? ParseOrderId(string orderIdText)
        {
            if (string.IsNullOrWhiteSpace(orderIdText))
            {
                return null;
            }

            int orderId;
            if (!int.TryParse(orderIdText.Trim(), out orderId) || orderId <= 0)
            {
                throw new ArgumentException("任务编号必须是正整数");
            }
            return orderId;
        }

        private static string NormalizePatientNo(string patientNo)
        {
            if (string.IsNullOrWhiteSpace(patientNo))
            {
                throw new ArgumentException("请输入患者编号");
            }

            patientNo = patientNo.Trim();
            if (patientNo.Length > 50)
            {
                throw new ArgumentException("患者编号不能超过 50 个字符");
            }
            return patientNo;
        }
    }
}
