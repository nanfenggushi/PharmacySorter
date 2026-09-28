using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// 操作日志数据访问。日志只新增和查询，不允许在界面上修改或删除。
    /// </summary>
    public class AppLogDAL
    {
        /// <summary>
        /// 写入一条日志。itemId 为空表示这条日志不属于某条处方明细。
        /// </summary>
        public int Add(int? itemId, string logType, string content)
        {
            string sql = @"INSERT INTO AppLog (ItemId, LogType, Content)
VALUES (@ItemId, @LogType, @Content)";
            return DbHelper.Update(sql,
                new SqlParameter("@ItemId", (object)itemId ?? DBNull.Value),
                new SqlParameter("@LogType", logType),
                new SqlParameter("@Content", content));
        }

        /// <summary>
        /// 按时间范围、处方号和日志类型查询。空条件不参与过滤，结果按时间倒序。
        /// endTime 由调用方处理成不包含的结束时刻，这样结束日期当天的记录也能查到。
        /// </summary>
        public List<AppLog> Search(DateTime? startTime, DateTime? endTime, int? prescriptionId, string logType)
        {
            string sql = @"
SELECT l.LogId, l.ItemId, i.OrderId AS PrescriptionId, l.LogType, l.Content, l.LogTime
FROM AppLog l
LEFT JOIN DispenseOrderItem i ON l.ItemId = i.ItemId
WHERE (@StartTime IS NULL OR l.LogTime >= @StartTime)
  AND (@EndTime IS NULL OR l.LogTime < @EndTime)
  AND (@PrescriptionId IS NULL OR i.OrderId = @PrescriptionId)
  AND (@LogType IS NULL OR l.LogType = @LogType)
ORDER BY l.LogTime DESC, l.LogId DESC";

            DataSet dataSet = DbHelper.Find(sql,
                new SqlParameter("@StartTime", (object)startTime ?? DBNull.Value),
                new SqlParameter("@EndTime", (object)endTime ?? DBNull.Value),
                new SqlParameter("@PrescriptionId", (object)prescriptionId ?? DBNull.Value),
                new SqlParameter("@LogType", string.IsNullOrWhiteSpace(logType) ? (object)DBNull.Value : logType));

            List<AppLog> list = new List<AppLog>();
            if (dataSet.Tables.Count == 0)
            {
                return list;
            }

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                list.Add(new AppLog
                {
                    LogId = Convert.ToInt32(row["LogId"]),
                    ItemId = row["ItemId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["ItemId"]),
                    PrescriptionId = row["PrescriptionId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["PrescriptionId"]),
                    LogType = row["LogType"].ToString(),
                    Content = row["Content"].ToString(),
                    LogTime = Convert.ToDateTime(row["LogTime"])
                });
            }
            return list;
        }
    }
}