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
        private readonly AppLogBLL logBll = new AppLogBLL();

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

        /// <summary>
        /// 开始一张处方。状态改为配药中，并记下复位动作。
        /// </summary>
        public void Start(Prescription prescription)
        {
            if (prescription == null)
            {
                throw new ArgumentException("当前没有待配处方");
            }

            if (prescriptionDal.MarkDispensing(prescription.PrescriptionId) == 0)
            {
                throw new ArgumentException("处方状态已变化，不能启动");
            }

            prescription.Status = "配药中";
            logBll.Add(null, AppLogType.Command,
                "处方 " + prescription.PrescriptionId + " 开始配药，下发" + Common.ArmCommandService.StandbyActionName + "待命指令");
        }

        /// <summary>
        /// 找到下一条还没核对通过的明细。全部完成时返回 null。
        /// </summary>
        public PrescriptionItem FindNextItem(IList<PrescriptionItem> items)
        {
            if (items == null)
            {
                return null;
            }

            foreach (PrescriptionItem item in items)
            {
                if (item.Status != "核对通过")
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 记录一次抓取和一次投递，并把抓取次数加一。
        /// </summary>
        public void RecordGrab(PrescriptionItem item, string grabCommand, string dropCommand)
        {
            EnsureItem(item);
            item.GrabCount++;
            item.Status = "取药中";
            if (prescriptionDal.UpdateItemProgress(item.ItemId, item.GrabCount, item.Status) == 0)
            {
                throw new ArgumentException("更新抓取进度失败");
            }

            logBll.Add(item.ItemId, AppLogType.Command,
                "【" + item.DrugName + "】第 " + item.GrabCount + " 次抓取 " + grabCommand + "，投递 " + dropCommand);
        }

        /// <summary>
        /// 抓取次数达到应发数量后，明细进入待核对。
        /// </summary>
        public void MarkWaitingCheck(PrescriptionItem item)
        {
            EnsureItem(item);
            item.Status = "待核对";
            prescriptionDal.UpdateItemProgress(item.ItemId, item.GrabCount, item.Status);
        }

        /// <summary>
        /// 按核对弹窗的决定更新明细。返回还需要补抓的次数，不需要补抓时为 0。
        /// </summary>
        public int ApplyDecision(PrescriptionItem item, int actualQty, VerifyDecision decision)
        {
            EnsureItem(item);
            if (actualQty < 0)
            {
                throw new ArgumentException("实收数量不能小于 0");
            }

            if (decision == VerifyDecision.Reset)
            {
                item.GrabCount = 0;
                item.ActualQty = 0;
                item.Status = "待取药";
                prescriptionDal.ResetItem(item.ItemId);
                logBll.Add(item.ItemId, AppLogType.ExceptionFix, "【" + item.DrugName + "】作废重置，抓取次数已清零");
                return item.RequiredQty;
            }

            if (decision == VerifyDecision.Refill)
            {
                int shortage = item.RequiredQty - actualQty;
                if (shortage <= 0)
                {
                    throw new ArgumentException("实收数量没有短少，不能补抓");
                }

                item.ActualQty = actualQty;
                item.Status = "取药中";
                prescriptionDal.UpdateItemProgress(item.ItemId, item.GrabCount, item.Status);
                logBll.Add(item.ItemId, AppLogType.QuantityCheck,
                    "【" + item.DrugName + "】实收 " + actualQty + "，应发 " + item.RequiredQty + "，补抓 " + shortage);
                return shortage;
            }

            if (decision == VerifyDecision.ReleaseExcess)
            {
                int extra = actualQty - item.RequiredQty;
                if (extra <= 0)
                {
                    throw new ArgumentException("实收数量没有超出，不能按超量放行");
                }

                Pass(item);
                logBll.Add(item.ItemId, AppLogType.ExceptionFix,
                    "【" + item.DrugName + "】实收 " + actualQty + "，应发 " + item.RequiredQty + "，已确认剔除多余 " + extra + " 盒");
                return 0;
            }

            if (actualQty != item.RequiredQty)
            {
                throw new ArgumentException("数量不一致，不能确认通过");
            }

            Pass(item);
            logBll.Add(item.ItemId, AppLogType.QuantityCheck,
                "【" + item.DrugName + "】实收 " + actualQty + "，与应发数量一致");
            return 0;
        }

        /// <summary>
        /// 如果所有明细都已通过，把处方改为已完成。
        /// </summary>
        public bool TryComplete(Prescription prescription)
        {
            if (prescription == null)
            {
                return false;
            }

            if (prescriptionDal.CompleteIfAllPassed(prescription.PrescriptionId) == 0)
            {
                return false;
            }

            prescription.Status = "已完成";
            logBll.Add(null, AppLogType.Prescription, "处方 " + prescription.PrescriptionId + " 配药完成");
            return true;
        }

        /// <summary>
        /// 取得药品所在药位。只允许左药位和右药位。
        /// </summary>
        public Station GetGrabStation(PrescriptionItem item)
        {
            EnsureItem(item); // 判断处方明细是否有效
            if (item.StationId != 2 && item.StationId != 3)
            {
                throw new ArgumentException("【" + item.DrugName + "】没有绑定左侧或右侧药位");
            }

            Station station = FindStation(item.StationId.Value);
            if (station == null || string.IsNullOrWhiteSpace(station.GrabCommand))
            {
                throw new ArgumentException("【" + item.DrugName + "】所在工位还没有配置抓取指令");
            }
            return station;
        }

        /// <summary>
        /// 取得正前分拣槽。投递必须使用它的放置指令。
        /// </summary>
        public Station GetDropStation()
        {
            Station station = FindStation(1);
            if (station == null || string.IsNullOrWhiteSpace(station.DropCommand))
            {
                throw new ArgumentException("正前分拣槽还没有配置放置指令");
            }
            return station;
        }

        private void Pass(PrescriptionItem item)
        {
            item.ActualQty = item.RequiredQty;
            item.Status = "核对通过";
            prescriptionDal.PassItem(item.ItemId, item.RequiredQty);
        }

        /// <summary>
        /// 判断处方明细是否有效
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="ArgumentException"></exception>
        private static void EnsureItem(PrescriptionItem item)
        {
            if (item == null || item.ItemId <= 0)
            {
                throw new ArgumentException("处方明细无效");
            }
        }

        /// <summary>
        /// 根据工位编号查询工位
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
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
