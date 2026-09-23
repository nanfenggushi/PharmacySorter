using DAL;
using Model;
using System;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// 配药看板业务。只负责读取当前处方和判断能否启动，不直接操作界面。
    /// </summary>
    public class DispenseBLL
    {
        private readonly PrescriptionDAL prescriptionDal = new PrescriptionDAL();
        private readonly StationDAL stationDal = new StationDAL();

        /// <summary>
        /// 当前应展示的处方。没有待处理处方时返回 null。
        /// </summary>
        public Prescription GetCurrentPrescription()
        {
            return prescriptionDal.GetCurrent();
        }

        /// <summary>
        /// 当前处方的明细。处方不存在时返回空列表。
        /// </summary>
        public List<PrescriptionItem> GetCurrentItems(int prescriptionId)
        {
            return prescriptionDal.GetItems(prescriptionId);
        }

        /// <summary>
        /// 启动前检查工位指令和药品绑定。返回空字符串表示可以启动。
        /// </summary>
        public string GetStartBlockReason(Prescription prescription, IList<PrescriptionItem> items)
        {
            if (prescription == null)
            {
                return "当前没有待配处方";
            }

            if (prescription.Status != "待配药")
            {
                return "只有待配药的处方可以启动";
            }

            if (items == null || items.Count == 0)
            {
                return "该处方没有药品明细";
            }

            Station slot = FindStation(1);
            if (slot == null || string.IsNullOrWhiteSpace(slot.DropCommand))
            {
                return "正前分拣槽还没有配置放置指令";
            }

            foreach (PrescriptionItem item in items)
            {
                if (item.StationId != 2 && item.StationId != 3)
                {
                    return "【" + item.DrugName + "】没有绑定左侧或右侧药位";
                }

                Station station = FindStation(item.StationId.Value);
                if (station == null || string.IsNullOrWhiteSpace(station.GrabCommand))
                {
                    return "【" + item.DrugName + "】所在工位还没有配置抓取指令";
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 已核对通过的明细条数，用于顶部进度。
        /// </summary>
        public int CountPassed(IList<PrescriptionItem> items)
        {
            int count = 0;
            if (items == null)
            {
                return count;
            }

            foreach (PrescriptionItem item in items)
            {
                if (item.Status == "核对通过")
                {
                    count++;
                }
            }
            return count;
        }

        private Station FindStation(int stationId)
        {
            foreach (Station station in stationDal.GetAll())
            {
                if (station.StationId == stationId)
                {
                    return station;
                }
            }
            return null;
        }
    }
}
