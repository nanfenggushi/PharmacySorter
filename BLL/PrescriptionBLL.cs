using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 固定处方维护。处方名称必须唯一，药品必须已启用并绑定左右药位。
    /// </summary>
    public partial class PrescriptionBLL
    {
        private readonly PrescriptionDAL dal = new PrescriptionDAL();
        private readonly DrugDAL drugDal = new DrugDAL();
        private readonly AppLogBLL logBll = new AppLogBLL();

        /// <summary>
        /// 处方维护下拉框使用的药品。停用或未绑定工位的药品不能加入处方。
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

        public List<Prescription> GetAll()
        {
            return dal.GetAll();
        }

        public Prescription GetById(int prescriptionId)
        {
            return dal.GetById(prescriptionId);
        }

        public List<PrescriptionItem> GetItems(int prescriptionId)
        {
            return dal.GetItems(prescriptionId);
        }

        public int Save(int prescriptionId, string prescriptionName, IList<PrescriptionItem> items)
        {
            prescriptionName = NormalizeName(prescriptionName);
            items = NormalizeItems(items);
            EnsureUniqueName(prescriptionId, prescriptionName);

            if (prescriptionId <= 0)
            {
                int newId = dal.Add(prescriptionName, items);
                logBll.Add(null, AppLogType.Prescription, BuildContent("新增处方", newId, prescriptionName, items));
                return newId;
            }

            if (dal.GetById(prescriptionId) == null)
            {
                throw new ArgumentException("处方不存在");
            }

            dal.Update(prescriptionId, prescriptionName, items);
            logBll.Add(null, AppLogType.Prescription, BuildContent("修改处方", prescriptionId, prescriptionName, items));
            return prescriptionId;
        }

        public void Delete(int prescriptionId)
        {
            Prescription prescription = dal.GetById(prescriptionId);
            if (prescription == null)
            {
                throw new ArgumentException("处方不存在");
            }
            if (dal.HasOrders(prescriptionId))
            {
                throw new ArgumentException("该处方已有配药记录，不能删除");
            }
            if (dal.Delete(prescriptionId) == 0)
            {
                throw new ArgumentException("处方不存在");
            }
            logBll.Add(null, AppLogType.Prescription, "删除处方 " + prescription.PrescriptionName);
        }

        private void EnsureUniqueName(int prescriptionId, string prescriptionName)
        {
            foreach (Prescription exists in dal.GetAll())
            {
                if (exists.PrescriptionId != prescriptionId
                    && string.Equals(exists.PrescriptionName, prescriptionName, StringComparison.OrdinalIgnoreCase))
                {
                    throw new ArgumentException("处方名称已存在");
                }
            }
        }

        private static string NormalizeName(string prescriptionName)
        {
            if (string.IsNullOrWhiteSpace(prescriptionName))
            {
                throw new ArgumentException("请输入处方名称");
            }

            prescriptionName = prescriptionName.Trim();
            if (prescriptionName.Length > 50)
            {
                throw new ArgumentException("处方名称不能超过 50 个字符");
            }
            return prescriptionName;
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
                    RequiredQty = item.RequiredQty
                });
            }
            return result;
        }

        private static string BuildContent(string action, int prescriptionId, string prescriptionName, IList<PrescriptionItem> items)
        {
            StringBuilder content = new StringBuilder();
            content.Append(action);
            content.Append(" ");
            content.Append(prescriptionId);
            content.Append("【");
            content.Append(prescriptionName);
            content.Append("】：");
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
