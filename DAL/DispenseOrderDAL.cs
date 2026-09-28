using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// 待配任务的数据访问。一次任务引用一张固定处方，并单独记录患者和抓取进度。
    /// </summary>
    public class DispenseOrderDAL
    {
        public List<DispenseOrder> GetWaitingQueue()
        {
            const string sql = @"
                        SELECT o.OrderId, o.PrescriptionId, p.PrescriptionName, o.PatientNo, o.Status,
                               o.CreateTime, o.CompleteTime, o.SortNo,
                               (SELECT COUNT(1) FROM DispenseOrderItem i WHERE i.OrderId = o.OrderId) AS ItemCount
                        FROM DispenseOrder o
                        INNER JOIN Prescription p ON o.PrescriptionId = p.PrescriptionId
                        WHERE o.Status = N'待配药'
                        ORDER BY o.SortNo, o.CreateTime, o.OrderId";
            return MapOrders(DbHelper.Find(sql));
        }

        public int GetNextSortNo()
        {
            object result = DbHelper.Scalar("SELECT ISNULL(MAX(SortNo), 0) + 1 FROM DispenseOrder");
            return Convert.ToInt32(result);
        }

        public int GetMinWaitingSortNo()
        {
            object result = DbHelper.Scalar("SELECT ISNULL(MIN(SortNo), 1) FROM DispenseOrder WHERE Status = N'待配药'");
            return Convert.ToInt32(result);
        }

        public int AddFromPrescription(int prescriptionId, string patientNo, int sortNo)
        {
            using (SqlConnection connection = OpenConnection())
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    const string orderSql = @"
                            INSERT INTO DispenseOrder (PrescriptionId, PatientNo, Status, SortNo, CreateTime)
                            VALUES (@PrescriptionId, @PatientNo, N'待配药', @SortNo, GETDATE());
                            SELECT CAST(SCOPE_IDENTITY() AS INT);";;
                    int orderId;
                    using (SqlCommand command = new SqlCommand(orderSql, connection, transaction))
                    {
                        command.Parameters.Add(new SqlParameter("@PrescriptionId", prescriptionId));
                        command.Parameters.Add(new SqlParameter("@PatientNo", patientNo));
                        command.Parameters.Add(new SqlParameter("@SortNo", sortNo));
                        orderId = Convert.ToInt32(command.ExecuteScalar());
                    }

                    const string itemSql = @"
                            INSERT INTO DispenseOrderItem (OrderId, DrugId, RequiredQty, ActualQty, GrabCount, Status)
                            SELECT @OrderId, DrugId, RequiredQty, 0, 0, N'待取药'
                            FROM PrescriptionItem
                            WHERE PrescriptionId = @PrescriptionId";
                    using (SqlCommand command = new SqlCommand(itemSql, connection, transaction))
                    {
                        command.Parameters.Add(new SqlParameter("@OrderId", orderId));
                        command.Parameters.Add(new SqlParameter("@PrescriptionId", prescriptionId));
                        if (command.ExecuteNonQuery() == 0)
                        {
                            throw new ArgumentException("该处方没有药品明细");
                        }
                    }

                    transaction.Commit();
                    return orderId;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public int UpdateSortNo(int orderId, int sortNo)
        {
            const string sql = "UPDATE DispenseOrder SET SortNo = @SortNo WHERE OrderId = @OrderId AND Status = N'待配药'";
            return DbHelper.Update(sql,
                new SqlParameter("@SortNo", sortNo),
                new SqlParameter("@OrderId", orderId));
        }

        public int Cancel(int orderId)
        {
            const string sql = "UPDATE DispenseOrder SET Status = N'已撤销' WHERE OrderId = @OrderId AND Status = N'待配药'";
            return DbHelper.Update(sql, new SqlParameter("@OrderId", orderId));
        }

        public DispenseOrder GetById(int orderId)
        {
            const string sql = @"
                        SELECT o.OrderId, o.PrescriptionId, p.PrescriptionName, o.PatientNo, o.Status,
                               o.CreateTime, o.CompleteTime, o.SortNo,
                               (SELECT COUNT(1) FROM DispenseOrderItem i WHERE i.OrderId = o.OrderId) AS ItemCount
                        FROM DispenseOrder o
                        INNER JOIN Prescription p ON o.PrescriptionId = p.PrescriptionId
                        WHERE o.OrderId = @OrderId";
            List<DispenseOrder> list = MapOrders(DbHelper.Find(sql, new SqlParameter("@OrderId", orderId)));
            return list.Count == 0 ? null : list[0];
        }

        public List<DispenseOrder> SearchHistory(int? orderId, string prescriptionName, DateTime? startTime, DateTime? endTime)
        {
            const string sql = @"
                        SELECT o.OrderId, o.PrescriptionId, p.PrescriptionName, o.PatientNo, o.Status,
                               o.CreateTime, o.CompleteTime, o.SortNo,
                               (SELECT COUNT(1) FROM DispenseOrderItem i WHERE i.OrderId = o.OrderId) AS ItemCount
                        FROM DispenseOrder o
                        INNER JOIN Prescription p ON o.PrescriptionId = p.PrescriptionId
                        WHERE (@OrderId IS NULL OR o.OrderId = @OrderId)
                          AND (@PrescriptionName IS NULL OR p.PrescriptionName LIKE @PrescriptionName)
                          AND (@StartTime IS NULL OR o.CreateTime >= @StartTime)
                          AND (@EndTime IS NULL OR o.CreateTime < @EndTime)
                        ORDER BY o.CreateTime DESC, o.OrderId DESC";
            return MapOrders(DbHelper.Find(sql,
                new SqlParameter("@OrderId", (object)orderId ?? DBNull.Value),
                new SqlParameter("@PrescriptionName", string.IsNullOrWhiteSpace(prescriptionName) ? (object)DBNull.Value : "%" + prescriptionName.Trim() + "%"),
                new SqlParameter("@StartTime", (object)startTime ?? DBNull.Value),
                new SqlParameter("@EndTime", (object)endTime ?? DBNull.Value)));
        }

        public DispenseOrder GetCurrent(int skipOrderId)
        {
            const string sql = @"
                        SELECT TOP 1 o.OrderId, o.PrescriptionId, p.PrescriptionName, o.PatientNo, o.Status,
                               o.CreateTime, o.CompleteTime, o.SortNo,
                               (SELECT COUNT(1) FROM DispenseOrderItem i WHERE i.OrderId = o.OrderId) AS ItemCount
                        FROM DispenseOrder o
                        INNER JOIN Prescription p ON o.PrescriptionId = p.PrescriptionId
                        WHERE o.Status IN (N'配药中', N'部分异常', N'待配药')
                          AND o.OrderId <> @SkipOrderId
                        ORDER BY CASE o.Status WHEN N'配药中' THEN 0 WHEN N'部分异常' THEN 1 ELSE 2 END,
                                 o.SortNo, o.CreateTime, o.OrderId";
            List<DispenseOrder> list = MapOrders(DbHelper.Find(sql, new SqlParameter("@SkipOrderId", skipOrderId)));
            return list.Count == 0 ? null : list[0];
        }

        public List<DispenseOrderItem> GetItems(int orderId)
        {
            const string sql = @"
                        SELECT i.ItemId, i.OrderId, i.DrugId, d.DrugName, d.Spec,
                               i.RequiredQty, i.ActualQty, i.GrabCount, i.Status,
                               d.StationId, s.StationName
                        FROM DispenseOrderItem i
                        INNER JOIN Drug d ON i.DrugId = d.DrugId
                        LEFT JOIN StationAction s ON d.StationId = s.StationId
                        WHERE i.OrderId = @OrderId
                        ORDER BY i.ItemId";
            return MapItems(DbHelper.Find(sql, new SqlParameter("@OrderId", orderId)));
        }

        public int MarkDispensing(int orderId)
        {
            const string sql = @"UPDATE DispenseOrder
                        SET Status = N'配药中', CompleteTime = NULL
                        WHERE OrderId = @OrderId AND Status IN (N'待配药', N'配药中', N'部分异常')";
            return DbHelper.Update(sql, new SqlParameter("@OrderId", orderId));
        }

        public int UpdateItemProgress(int itemId, int grabCount, string status)
        {
            const string sql = @"UPDATE DispenseOrderItem
                        SET GrabCount = @GrabCount, Status = @Status
                        WHERE ItemId = @ItemId";
            return DbHelper.Update(sql,
                new SqlParameter("@GrabCount", grabCount),
                new SqlParameter("@Status", status),
                new SqlParameter("@ItemId", itemId));
        }

        public int PassItem(int itemId, int actualQty)
        {
            const string sql = @"UPDATE DispenseOrderItem
                        SET ActualQty = @ActualQty, Status = N'核对通过'
                        WHERE ItemId = @ItemId";
            return DbHelper.Update(sql,
                new SqlParameter("@ActualQty", actualQty),
                new SqlParameter("@ItemId", itemId));
        }

        public int ResetItem(int itemId)
        {
            const string sql = @"UPDATE DispenseOrderItem
                        SET GrabCount = 0, ActualQty = 0, Status = N'待取药'
                        WHERE ItemId = @ItemId";
            return DbHelper.Update(sql, new SqlParameter("@ItemId", itemId));
        }

        public int CompleteIfAllPassed(int orderId)
        {
            const string sql = @"UPDATE DispenseOrder
                        SET Status = N'已完成', CompleteTime = GETDATE()
                        WHERE OrderId = @OrderId
                          AND NOT EXISTS (
                              SELECT 1 FROM DispenseOrderItem
                              WHERE OrderId = @OrderId AND Status <> N'核对通过')";
            return DbHelper.Update(sql, new SqlParameter("@OrderId", orderId));
        }

        private static SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(DbHelper.ConnectionString);
            connection.Open();
            return connection;
        }

        private static List<DispenseOrder> MapOrders(DataSet dataSet)
        {
            List<DispenseOrder> list = new List<DispenseOrder>();
            if (dataSet.Tables.Count == 0)
            {
                return list;
            }

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                list.Add(new DispenseOrder
                {
                    OrderId = Convert.ToInt32(row["OrderId"]),
                    PrescriptionId = Convert.ToInt32(row["PrescriptionId"]),
                    PrescriptionName = row["PrescriptionName"].ToString(),
                    PatientNo = row["PatientNo"].ToString(),
                    Status = row["Status"].ToString(),
                    CreateTime = Convert.ToDateTime(row["CreateTime"]),
                    CompleteTime = row["CompleteTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["CompleteTime"]),
                    SortNo = Convert.ToInt32(row["SortNo"]),
                    ItemCount = Convert.ToInt32(row["ItemCount"])
                });
            }
            return list;
        }

        private static List<DispenseOrderItem> MapItems(DataSet dataSet)
        {
            List<DispenseOrderItem> list = new List<DispenseOrderItem>();
            if (dataSet.Tables.Count == 0)
            {
                return list;
            }

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                list.Add(new DispenseOrderItem
                {
                    ItemId = Convert.ToInt32(row["ItemId"]),
                    OrderId = Convert.ToInt32(row["OrderId"]),
                    DrugId = Convert.ToInt32(row["DrugId"]),
                    DrugName = row["DrugName"].ToString(),
                    Spec = row["Spec"] == DBNull.Value ? string.Empty : row["Spec"].ToString(),
                    RequiredQty = Convert.ToInt32(row["RequiredQty"]),
                    ActualQty = Convert.ToInt32(row["ActualQty"]),
                    GrabCount = Convert.ToInt32(row["GrabCount"]),
                    Status = row["Status"].ToString(),
                    StationId = row["StationId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["StationId"]),
                    StationName = row["StationName"] == DBNull.Value ? string.Empty : row["StationName"].ToString()
                });
            }
            return list;
        }
    }
}
