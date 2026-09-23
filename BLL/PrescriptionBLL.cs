using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 处方录入与待配队列。只允许选择已启用且已绑定左右药位的药品。
    /// </summary>
    public class PrescriptionBLL
    {
        private readonly PrescriptionDAL dal = new PrescriptionDAL();
        private readonly DrugDAL drugDal = new DrugDAL();
        private readonly AppLogBLL logBll = new AppLogBLL();

        /// <summary>
        /// 处方录入下拉框使用的药品。停用或未绑定工位的药品不能加入处方。
        /// </summary>
        public List<Drug> GetSelectableDrugs()
        {
            List<Drug> result = new List<Drug>();
            foreach (Drug drug in drugDal.GetEnabled())
            {
                if (drug.StationId == 2 || drug.StationId == 3)
                {
                    result.Add(drug);
                }
            }
            return result;
        }

        /// <summary>
        /// 取得待配药队列，已经按优先级排好。
        /// </summary>
        public List<Prescription> GetWaitingQueue()
        {
            return dal.GetWaitingQueue();
        }

        /// <summary>
        /// 预览下一个自动生成的处方号。真正编号仍在提交时由数据库生成。
        /// </summary>
        public int PreviewNextId()
        {
            return dal.PreviewNextId();
        }

        /// <summary>
        /// 校验并提交一张处方。返回数据库生成的处方号。
        /// </summary>
        public int Submit(string patientNo, IList<PrescriptionItem> items)
        {
            patientNo = NormalizePatientNo(patientNo);
            items = NormalizeItems(items);

            Prescription prescription = new Prescription
            {
                PatientNo = patientNo,
                SortNo = dal.GetNextSortNo()
            };
            int prescriptionId = dal.Add(prescription, items);
            logBll.Add(null, AppLogType.Prescription, BuildSubmitContent(prescriptionId, patientNo, items));
            return prescriptionId;
        }

        /// <summary>
        /// 将待配处方移到队列最前面。
        /// </summary>
        public void MoveToTop(int prescriptionId)
        {
            Prescription prescription = EnsureWaiting(prescriptionId);
            int sortNo = dal.GetMinWaitingSortNo() - 1;
            if (dal.UpdateSortNo(prescriptionId, sortNo) == 0)
            {
                throw new ArgumentException("处方状态已变化，不能置顶");
            }
            logBll.Add(null, AppLogType.Prescription, "处方 " + prescription.PrescriptionId + " 已置顶");
        }

        /// <summary>
        /// 撤销尚未开始配药的处方。明细保留，方便事后核对。
        /// </summary>
        public void Cancel(int prescriptionId)
        {
            Prescription prescription = EnsureWaiting(prescriptionId);
            if (dal.Cancel(prescriptionId) == 0)
            {
                throw new ArgumentException("处方状态已变化，不能撤销");
            }
            logBll.Add(null, AppLogType.Prescription, "撤销处方 " + prescription.PrescriptionId + "，患者 " + prescription.PatientNo);
        }

        private Prescription EnsureWaiting(int prescriptionId)
        {
            Prescription prescription = dal.GetById(prescriptionId);
            if (prescription == null)
            {
                throw new ArgumentException("处方不存在");
            }
            if (prescription.Status != "待配药")
            {
                throw new ArgumentException("只有待配药的处方可以操作");
            }
            return prescription;
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

        private List<PrescriptionItem> NormalizeItems(IList<PrescriptionItem> items)
        {
            if (items == null || items.Count == 0)
            {
                throw new ArgumentException("请至少加入一种药品");
            }

            List<PrescriptionItem> result = new List<PrescriptionItem>();
            foreach (PrescriptionItem item in items)
            {
                if (item.RequiredQty <= 0 || item.RequiredQty > 99)
                {
                    throw new ArgumentException("药品数量必须在 1 到 99 之间");
                }

                Drug drug = drugDal.GetById(item.DrugId);
                if (drug == null || !drug.IsActive)
                {
                    throw new ArgumentException("药品不存在或已停用");
                }
                if (drug.StationId != 2 && drug.StationId != 3)
                {
                    throw new ArgumentException("【" + drug.DrugName + "】还没有绑定左侧或右侧药位");
                }

                foreach (PrescriptionItem exists in result)
                {
                    if (exists.DrugId == drug.DrugId)
                    {
                        throw new ArgumentException("【" + drug.DrugName + "】已在清单中，请直接修改数量");
                    }
                }

                result.Add(new PrescriptionItem
                {
                    DrugId = drug.DrugId,
                    DrugName = drug.DrugName,
                    Spec = drug.Spec,
                    RequiredQty = item.RequiredQty,
                    Status = "待取药"
                });
            }
            return result;
        }

        private static string BuildSubmitContent(int prescriptionId, string patientNo, IList<PrescriptionItem> items)
        {
            StringBuilder content = new StringBuilder();
            content.Append("提交处方 ");
            content.Append(prescriptionId);
            content.Append("，患者 ");
            content.Append(patientNo);
            content.Append("：");
            for (int i = 0; i < items.Count; i++)
            {
                if (i > 0)
                {
                    content.Append("，");
                }
                content.Append(items[i].DrugName);
                content.Append(" x");
                content.Append(items[i].RequiredQty);
            }
            return content.ToString();
        }
    }
}