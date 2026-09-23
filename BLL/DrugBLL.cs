using DAL;
using Model;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class DrugBLL
    {
        private readonly DrugDAL dal = new DrugDAL();
        private readonly StationDAL stationDal = new StationDAL();
        private readonly AppLogBLL logBll = new AppLogBLL();

        public List<Drug> Search(string drugIdText, string drugName, string stationName)
        {
            int? drugId = ParseDrugId(drugIdText);
            return dal.Search(drugId, drugName, stationName);
        }

        /// <summary>
        /// 按一个关键字查询药品。关键字为空时返回全部。
        /// 纯数字同时按编号精确匹配，名称和规格始终按包含关系匹配。
        /// </summary>
        /// <param name="keyword">药品编号、名称或规格。</param>
        public List<Drug> SearchByKeyword(string keyword)
        {
            return dal.SearchByKeyword(keyword);
        }

        /// <summary>
        /// 处方录入、工位绑定只允许选择已启用药品。
        /// </summary>
        public List<Drug> GetEnabled()
        {
            return dal.GetEnabled();
        }

        /// <summary>
        /// 新增药品。stationId 为空表示未绑定工位。
        /// </summary>
        public int Add(string drugName, string spec, int? stationId)
        {
            drugName = NormalizeName(drugName);
            spec = NormalizeSpec(spec);
            EnsureStation(stationId);
            if (dal.ExistsName(drugName, null))
            {
                throw new ArgumentException("已存在同名药品，请更换名称");
            }

            int drugId = dal.Add(drugName, spec, stationId);
            logBll.Add(null, AppLogType.DrugMaintain,
                "新增药品【" + drugName + "】，规格：" + DisplaySpec(spec) + "，工位：" + StationText(stationId));
            return drugId;
        }

        /// <summary>
        /// 按药品编号修改名称、规格和工位。编号本身不允许修改。
        /// </summary>
        public void Update(int drugId, string drugName, string spec, int? stationId)
        {
            EnsureExists(drugId);
            drugName = NormalizeName(drugName);
            spec = NormalizeSpec(spec);
            EnsureStation(stationId);
            if (dal.ExistsName(drugName, drugId))
            {
                throw new ArgumentException("已存在同名药品，请更换名称");
            }

            dal.Update(drugId, drugName, spec, stationId);
            logBll.Add(null, AppLogType.DrugMaintain,
                "修改药品 " + drugId + "【" + drugName + "】，规格：" + DisplaySpec(spec) + "，工位：" + StationText(stationId));
        }

        public void SetActive(int drugId, bool isActive)
        {
            Drug drug = dal.GetById(drugId);
            if (drug == null)
            {
                throw new ArgumentException("药品不存在");
            }

            dal.SetActive(drugId, isActive);
            logBll.Add(null, AppLogType.DrugMaintain,
                (isActive ? "启用" : "停用") + "药品 " + drugId + "【" + drug.DrugName + "】");
        }

        /// <summary>
        /// 把已启用药品绑定到左侧或右侧药位。stationId 为空时解除绑定。
        /// </summary>
        public void BindStation(int drugId, int? stationId)
        {
            Drug drug = dal.GetById(drugId);
            if (drug == null)
            {
                throw new ArgumentException("药品不存在");
            }

            if (!drug.IsActive)
            {
                throw new ArgumentException("停用药品不能绑定工位");
            }

            EnsureStation(stationId);
            dal.SetStation(drugId, stationId);
            logBll.Add(null, AppLogType.StationConfig,
                "药品 " + drug.DrugId + "【" + drug.DrugName + "】绑定工位：" + StationText(stationId));
        }

        private void EnsureExists(int drugId)
        {
            if (dal.GetById(drugId) == null)
            {
                throw new ArgumentException("药品不存在");
            }
        }

        /// <summary>
        /// 工位只能是左侧药位或右侧药位，空值表示不绑定。
        /// </summary>
        private void EnsureStation(int? stationId)
        {
            if (!stationId.HasValue)
            {
                return;
            }

            if (!stationDal.IsBindableStation(stationId.Value))
            {
                throw new ArgumentException("只能绑定左侧药位或右侧药位");
            }
        }

        private static int? ParseDrugId(string drugIdText)
        {
            if (string.IsNullOrWhiteSpace(drugIdText))
            {
                return null;
            }

            int drugId;
            if (!int.TryParse(drugIdText.Trim(), out drugId) || drugId <= 0)
            {
                throw new ArgumentException("药品编号必须是正整数");
            }
            return drugId;
        }

        private static string NormalizeName(string drugName)
        {
            if (string.IsNullOrWhiteSpace(drugName))
            {
                throw new ArgumentException("请输入药品名称");
            }

            drugName = drugName.Trim();
            if (drugName.Length > 100)
            {
                throw new ArgumentException("药品名称不能超过 100 个字符");
            }
            return drugName;
        }

        private static string NormalizeSpec(string spec)
        {
            if (string.IsNullOrWhiteSpace(spec))
            {
                return null;
            }

            spec = spec.Trim();
            if (spec.Length > 100)
            {
                throw new ArgumentException("规格不能超过 100 个字符");
            }
            return spec;
        }

        private static string DisplaySpec(string spec)
        {
            return string.IsNullOrWhiteSpace(spec) ? "未填写" : spec;
        }

        private string StationText(int? stationId)
        {
            if (!stationId.HasValue)
            {
                return "未绑定";
            }

            foreach (Station station in stationDal.GetBindableStations())
            {
                if (station.StationId == stationId)
                {
                    return station.StationName;
                }
            }
            return stationId.Value.ToString();
        }
    }
}