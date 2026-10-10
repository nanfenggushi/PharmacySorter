using Common;
using DAL;
using Model;
using System;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// 配药工作台业务。只负责读取当前待配任务和判断能否启动，不直接操作界面。
    /// </summary>
    public class DispenseBLL
    {
        private readonly DispenseOrderDAL orderDal = new DispenseOrderDAL();
        private readonly StationDAL stationDal = new StationDAL();
        private readonly AppLogBLL logBll = new AppLogBLL();

        /// <summary>
        /// 当前应展示的待配任务。没有待处理任务时返回 null。
        /// </summary>
        public DispenseOrder GetCurrentOrder()
        {
            return GetCurrentOrder(0);
        }

        /// <summary>
        /// 当前应展示的待配任务。skipOrderId 用于跳过指令不完整、暂时不能配的任务。
        /// </summary>
        public DispenseOrder GetCurrentOrder(int skipOrderId)
        {
            return orderDal.GetCurrent(skipOrderId);
        }

        /// <summary>
        /// 当前任务的明细。任务不存在时返回空列表。
        /// </summary>
        public List<DispenseOrderItem> GetCurrentItems(int orderId)
        {
            return orderDal.GetItems(orderId);
        }

        /// <summary>
        /// 启动前检查工位指令和药品绑定。返回空字符串表示可以启动。
        /// </summary>
        public string GetStartBlockReason(DispenseOrder order, IList<DispenseOrderItem> items)
        {
            if (order == null)
            {
                return "当前没有待配任务";
            }

            if (order.Status != "待配药" && order.Status != "配药中" && order.Status != "部分异常")
            {
                return "当前任务不能继续配药";
            }

            if (items == null || items.Count == 0)
            {
                return "该任务没有药品明细";
            }

            Station slot = FindStation(1);
            if (slot == null || string.IsNullOrWhiteSpace(slot.DropCommand))
            {
                return "正前分拣槽还没有配置放置指令";
            }

            foreach (DispenseOrderItem item in items)
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
        public int CountPassed(IList<DispenseOrderItem> items)
        {
            int count = 0;
            if (items == null)
            {
                return count;
            }

            foreach (DispenseOrderItem item in items)
            {
                if (item.Status == "核对通过")
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// 开始一个待配任务。状态改为配药中，并记下复位动作。
        /// </summary>
        public void Start(DispenseOrder order)
        {
            if (order == null)
            {
                throw new ArgumentException("当前没有待配任务");
            }

            bool resume = order.Status == "配药中" || order.Status == "部分异常";
            // 修改任务状态
            if (orderDal.MarkDispensing(order.OrderId) == 0)
            {
                throw new ArgumentException("任务状态已变化，不能启动");
            }

            order.Status = "配药中";
            logBll.Add(null, AppLogType.Command, resume
                ? "待配任务 " + order.OrderId + " 从中断处继续配药"
                : "待配任务 " + order.OrderId + " 开始配药，下发" + ArmCommandService.StandbyActionName + "待命指令");
        }

        /// <summary>
        /// 找到下一条还没核对通过的明细。全部完成时返回 null。
        /// </summary>
        public DispenseOrderItem FindNextItem(IList<DispenseOrderItem> items)
        {
            if (items == null)
            {
                return null;
            }

            foreach (DispenseOrderItem item in items)
            {
                if (item.Status != "核对通过")
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 记录一次抓取和一次投递。只有传感器确认落药时，实收数量才加一。
        /// </summary>
        public void RecordGrab(DispenseOrderItem item, string grabCommand, string dropCommand, bool dropped)
        {
            EnsureItem(item);
            item.GrabCount++;
            if (dropped)
            {
                item.ActualQty++;
            }

            item.Status = item.ActualQty >= item.RequiredQty ? "核对通过" : "取药中";
            if (orderDal.UpdateItemProgress(item.ItemId, item.GrabCount, item.ActualQty, item.Status) == 0)
            {
                throw new ArgumentException("更新抓取进度失败");
            }

            logBll.Add(item.ItemId, AppLogType.Command,
                "【" + item.DrugName + "】第 " + item.GrabCount + " 次抓取 " + grabCommand);
            logBll.Add(item.ItemId, AppLogType.Command,
                "【" + item.DrugName + "】第 " + item.GrabCount + " 次投递 " + dropCommand);
            logBll.Add(item.ItemId, dropped ? AppLogType.QuantityCheck : AppLogType.ExceptionFix,
                dropped
                    ? "【" + item.DrugName + "】第 " + item.GrabCount + " 次落药确认，实收 " + item.ActualQty
                    : "【" + item.DrugName + "】第 " + item.GrabCount + " 次未检测到落药，准备补抓");
        }

        /// <summary>
        /// 连续没有检测到落药时，停止这条明细并保留已完成的数量。
        /// </summary>
        public void MarkSensorFault(DispenseOrder order, DispenseOrderItem item, int consecutiveMisses)
        {
            EnsureItem(item);
            item.Status = "异常";
            orderDal.UpdateItemProgress(item.ItemId, item.GrabCount, item.ActualQty, item.Status);
            if (orderDal.MarkPartialException(item.OrderId) > 0 && order != null)
            {
                order.Status = "部分异常";
            }
            logBll.Add(item.ItemId, AppLogType.ExceptionFix,
                "【" + item.DrugName + "】连续 " + consecutiveMisses + " 次未检测到落药，配药已停止");
        }

        /// <summary>
        /// 如果所有明细都已通过，把任务改为已完成。
        /// </summary>
        public bool TryComplete(DispenseOrder order)
        {
            if (order == null)
            {
                return false;
            }

            if (orderDal.CompleteIfAllPassed(order.OrderId) == 0)
            {
                return false;
            }

            order.Status = "已完成";
            logBll.Add(null, AppLogType.Prescription, "待配任务 " + order.OrderId + " 配药完成");
            return true;
        }

        /// <summary>
        /// 取得药品所在药位。只允许左药位和右药位。
        /// </summary>
        public Station GetGrabStation(DispenseOrderItem item)
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

        /// <summary>
        /// 判断任务明细是否有效
        /// </summary>
        private static void EnsureItem(DispenseOrderItem item)
        {
            if (item == null || item.ItemId <= 0)
            {
                throw new ArgumentException("任务明细无效");
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
