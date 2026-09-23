using DAL;
using Model;
using System;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// 工位业务。向界面提供可绑定的药位列表。
    /// </summary>
    public class StationBLL
    {
        private readonly StationDAL dal = new StationDAL();
        private readonly AppLogBLL logBll = new AppLogBLL();

        /// <summary>
        /// 取得左侧药位和右侧药位。
        /// </summary>
        public List<Station> GetBindableStations()
        {
            return dal.GetBindableStations();
        }

        /// <summary>
        /// 取得全部工位，包含正前分拣槽。
        /// </summary>
        public List<Station> GetAll()
        {
            return dal.GetAll();
        }

        /// <summary>
        /// 校验并保存全部工位的宏指令和延时。任一工位不合法时，一条都不写入。
        /// </summary>
        public void SaveAll(IList<Station> stations)
        {
            if (stations == null || stations.Count == 0)
            {
                throw new ArgumentException("没有可保存的工位");
            }

            foreach (Station station in stations)
            {
                Normalize(station);
            }

            foreach (Station station in stations)
            {
                dal.UpdateAction(station.StationId.Value, station.GrabCommand, station.DropCommand, station.EstTimeMs);
            }

            logBll.Add(null, AppLogType.StationConfig, BuildContent(stations));
        }

        /// <summary>
        /// 把本次保存的工位指令整理成一条日志，方便事后核对改过什么。
        /// </summary>
        private static string BuildContent(IList<Station> stations)
        {
            System.Text.StringBuilder content = new System.Text.StringBuilder("保存工位指令：");
            for (int i = 0; i < stations.Count; i++)
            {
                Station station = stations[i];
                if (i > 0)
                {
                    content.Append("；");
                }

                content.Append(station.StationName);
                content.Append(" 抓取=");
                content.Append(string.IsNullOrEmpty(station.GrabCommand) ? "空" : station.GrabCommand);
                content.Append(" 放置=");
                content.Append(string.IsNullOrEmpty(station.DropCommand) ? "空" : station.DropCommand);
                content.Append(" 延时=");
                content.Append(station.EstTimeMs);
                content.Append("ms");
            }
            return content.ToString();
        }

        /// <summary>
        /// 整理一条工位配置：去掉首尾空格，并检查指令长度、延时和必填项。
        /// </summary>
        private static void Normalize(Station station)
        {
            if (station == null || !station.StationId.HasValue)
            {
                throw new ArgumentException("工位数据无效");
            }

            station.GrabCommand = NormalizeCommand(station.GrabCommand, station.StationName + "的抓取指令");
            station.DropCommand = NormalizeCommand(station.DropCommand, station.StationName + "的放置指令");

            if (station.EstTimeMs < 500 || station.EstTimeMs > 60000)
            {
                throw new ArgumentException(station.StationName + "的延时必须在 500 到 60000 毫秒之间");
            }

            // 左右药位靠抓取指令取药，正前分拣槽靠放置指令投递。
            if ((station.StationId == 2 || station.StationId == 3) && station.GrabCommand == null)
            {
                throw new ArgumentException(station.StationName + "必须填写抓取触发字符串");
            }

            if (station.StationId == 1 && station.DropCommand == null)
            {
                throw new ArgumentException("正前分拣槽必须填写放置触发字符串");
            }
        }

        /// <summary>
        /// 空指令保存为 null。有内容时不能超过数据库字段长度。
        /// </summary>
        private static string NormalizeCommand(string command, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                return null;
            }

            command = command.Trim();
            if (command.Length > 100)
            {
                throw new ArgumentException(fieldName + "不能超过 100 个字符");
            }
            return command;
        }
    }
}