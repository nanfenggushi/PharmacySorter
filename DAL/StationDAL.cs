using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// 工位数据访问。药品只能绑定左侧药位和右侧药位，不能绑定正前分拣槽。
    /// </summary>
    public class StationDAL
    {
        /// <summary>
        /// 读取可供药品绑定的工位：2 左侧药位、3 右侧药位。
        /// </summary>
        public List<Station> GetBindableStations()
        {
            string sql = SelectSql + @"
WHERE StationId IN (2, 3)
ORDER BY StationId";
            return MapList(DbHelper.Find(sql));
        }

        /// <summary>
        /// 读取全部固定工位及其宏指令，供工位配置表显示。
        /// </summary>
        public List<Station> GetAll()
        {
            return MapList(DbHelper.Find(SelectSql + " ORDER BY StationId"));
        }

        /// <summary>
        /// 保存一个工位的抓取指令、放置指令和延时。工位名称是固定的，不在这里修改。
        /// </summary>
        public int UpdateAction(int stationId, string grabCommand, string dropCommand, int estTimeMs)
        {
            string sql = @"UPDATE StationAction
SET GrabCommand = @GrabCommand,
    DropCommand = @DropCommand,
    EstTimeMs = @EstTimeMs
WHERE StationId = @StationId";
            return DbHelper.Update(sql,
                new SqlParameter("@GrabCommand", DbText(grabCommand)),
                new SqlParameter("@DropCommand", DbText(dropCommand)),
                new SqlParameter("@EstTimeMs", estTimeMs),
                new SqlParameter("@StationId", stationId));
        }

        /// <summary>
        /// 判断工位是否允许绑定药品。未绑定（null）由调用方单独处理。
        /// </summary>
        public bool IsBindableStation(int stationId)
        {
            string sql = "SELECT COUNT(1) FROM StationAction WHERE StationId = @StationId AND StationId IN (2, 3)";
            object result = DbHelper.Scalar(sql, new SqlParameter("@StationId", stationId));
            return Convert.ToInt32(result) > 0;
        }

        private const string SelectSql = @"SELECT StationId, StationName, GrabCommand, DropCommand, EstTimeMs
FROM StationAction";

        private static List<Station> MapList(DataSet dataSet)
        {
            List<Station> list = new List<Station>();
            if (dataSet.Tables.Count == 0)
            {
                return list;
            }

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                list.Add(new Station
                {
                    StationId = Convert.ToInt32(row["StationId"]),
                    StationName = row["StationName"].ToString(),
                    GrabCommand = row["GrabCommand"] == DBNull.Value ? string.Empty : row["GrabCommand"].ToString(),
                    DropCommand = row["DropCommand"] == DBNull.Value ? string.Empty : row["DropCommand"].ToString(),
                    EstTimeMs = Convert.ToInt32(row["EstTimeMs"])
                });
            }
            return list;
        }

        private static object DbText(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : value.Trim();
        }
    }
}