using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// 处方及明细的数据访问。提交处方和明细放在同一个事务里。
    /// </summary>
    public class PrescriptionDAL
    {
        /// <summary>
        /// 读取待配药队列。SortNo 越小越靠前，相同序号再按接收时间。
        /// </summary>
        public List<Prescription> GetWaitingQueue()
        {
            string sql = @"
SELECT p.PrescriptionId, p.PatientNo, p.Status, p.CreateTime, p.CompleteTime, p.SortNo,
       (SELECT COUNT(1) FROM PrescriptionItem i WHERE i.PrescriptionId = p.PrescriptionId) AS ItemCount
FROM Prescription p
WHERE p.Status = N'待配药'
ORDER BY p.SortNo, p.CreateTime, p.PrescriptionId";
            return MapPrescriptions(DbHelper.Find(sql));
        }

        /// <summary>
        /// 下一个队列序号。新处方追加到队尾。
        /// </summary>
        public int GetNextSortNo()
        {
            object result = DbHelper.Scalar("SELECT ISNULL(MAX(SortNo), 0) + 1 FROM Prescription");
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// 预览下一次提交会生成的处方号。正式编号仍以提交时数据库生成的值为准。
        /// </summary>
        public int PreviewNextId()
        {
            object result = DbHelper.Scalar(@"
SELECT CASE
    WHEN MAX(PrescriptionId) IS NULL THEN CAST(IDENT_SEED('Prescription') AS INT)
    ELSE MAX(PrescriptionId) + CAST(IDENT_INCR('Prescription') AS INT)
END
FROM Prescription");
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// 当前待配队列里最小的序号，置顶时再减一。
        /// </summary>
        public int GetMinWaitingSortNo()
        {
            object result = DbHelper.Scalar("SELECT ISNULL(MIN(SortNo), 1) FROM Prescription WHERE Status = N'待配药'");
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// 保存处方头和全部明细。任一条插入失败都会回滚，不会留下半张处方。
        /// </summary>
        public int Add(Prescription prescription, IList<PrescriptionItem> items)
        {
            using (SqlConnection connection = OpenConnection())
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    int prescriptionId = InsertPrescription(connection, transaction, prescription);
                    foreach (PrescriptionItem item in items)
                    {
                        InsertItem(connection, transaction, prescriptionId, item);
                    }
                    transaction.Commit();
                    return prescriptionId;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 把指定待配处方的顺序改为给定值。
        /// </summary>
        public int UpdateSortNo(int prescriptionId, int sortNo)
        {
            string sql = "UPDATE Prescription SET SortNo = @SortNo WHERE PrescriptionId = @PrescriptionId AND Status = N'待配药'";
            return DbHelper.Update(sql,
                new SqlParameter("@SortNo", sortNo),
                new SqlParameter("@PrescriptionId", prescriptionId));
        }

        /// <summary>
        /// 撤销仍处于待配药的处方。已经开始配药的处方不能撤销。
        /// </summary>
        public int Cancel(int prescriptionId)
        {
            string sql = "UPDATE Prescription SET Status = N'已撤销' WHERE PrescriptionId = @PrescriptionId AND Status = N'待配药'";
            return DbHelper.Update(sql, new SqlParameter("@PrescriptionId", prescriptionId));
        }

        public Prescription GetById(int prescriptionId)
        {
            string sql = @"
SELECT p.PrescriptionId, p.PatientNo, p.Status, p.CreateTime, p.CompleteTime, p.SortNo,
       (SELECT COUNT(1) FROM PrescriptionItem i WHERE i.PrescriptionId = p.PrescriptionId) AS ItemCount
FROM Prescription p
WHERE p.PrescriptionId = @PrescriptionId";
            List<Prescription> list = MapPrescriptions(DbHelper.Find(sql, new SqlParameter("@PrescriptionId", prescriptionId)));
            return list.Count == 0 ? null : list[0];
        }

        /// <summary>
        /// 看板当前处方：正在配药的优先，否则取待配队列第一张。
        /// </summary>
        public Prescription GetCurrent()
        {
            string sql = @"
SELECT TOP 1 p.PrescriptionId, p.PatientNo, p.Status, p.CreateTime, p.CompleteTime, p.SortNo,
       (SELECT COUNT(1) FROM PrescriptionItem i WHERE i.PrescriptionId = p.PrescriptionId) AS ItemCount
FROM Prescription p
WHERE p.Status IN (N'配药中', N'部分异常', N'待配药')
ORDER BY CASE p.Status WHEN N'配药中' THEN 0 WHEN N'部分异常' THEN 1 ELSE 2 END,
         p.SortNo, p.CreateTime, p.PrescriptionId";
            List<Prescription> list = MapPrescriptions(DbHelper.Find(sql));
            return list.Count == 0 ? null : list[0];
        }

        /// <summary>
        /// 读取一张处方的全部明细，并带上药品当前绑定的工位。
        /// </summary>
        public List<PrescriptionItem> GetItems(int prescriptionId)
        {
            string sql = @"
SELECT i.ItemId, i.PrescriptionId, i.DrugId, d.DrugName, d.Spec,
       i.RequiredQty, i.ActualQty, i.GrabCount, i.Status,
       d.StationId, s.StationName
FROM PrescriptionItem i
INNER JOIN Drug d ON i.DrugId = d.DrugId
LEFT JOIN StationAction s ON d.StationId = s.StationId
WHERE i.PrescriptionId = @PrescriptionId
ORDER BY i.ItemId";
            return MapItems(DbHelper.Find(sql, new SqlParameter("@PrescriptionId", prescriptionId)));
        }

        private static SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(DbHelper.ConnectionString);
            connection.Open();
            return connection;
        }

        private static int InsertPrescription(SqlConnection connection, SqlTransaction transaction, Prescription prescription)
        {
            const string sql = @"
INSERT INTO Prescription (PatientNo, Status, SortNo)
VALUES (@PatientNo, N'待配药', @SortNo);
SELECT CAST(SCOPE_IDENTITY() AS INT);";
            using (SqlCommand command = new SqlCommand(sql, connection, transaction))
            {
                command.Parameters.Add(new SqlParameter("@PatientNo", prescription.PatientNo));
                command.Parameters.Add(new SqlParameter("@SortNo", prescription.SortNo));
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private static void InsertItem(SqlConnection connection, SqlTransaction transaction, int prescriptionId, PrescriptionItem item)
        {
            const string sql = @"
INSERT INTO PrescriptionItem (PrescriptionId, DrugId, RequiredQty, ActualQty, Status)
VALUES (@PrescriptionId, @DrugId, @RequiredQty, 0, N'待取药')";
            using (SqlCommand command = new SqlCommand(sql, connection, transaction))
            {
                command.Parameters.Add(new SqlParameter("@PrescriptionId", prescriptionId));
                command.Parameters.Add(new SqlParameter("@DrugId", item.DrugId));
                command.Parameters.Add(new SqlParameter("@RequiredQty", item.RequiredQty));
                command.ExecuteNonQuery();
            }
        }

        private static List<Prescription> MapPrescriptions(DataSet dataSet)
        {
            List<Prescription> list = new List<Prescription>();
            if (dataSet.Tables.Count == 0)
            {
                return list;
            }

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                list.Add(new Prescription
                {
                    PrescriptionId = Convert.ToInt32(row["PrescriptionId"]),
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

        private static List<PrescriptionItem> MapItems(DataSet dataSet)
        {
            List<PrescriptionItem> list = new List<PrescriptionItem>();
            if (dataSet.Tables.Count == 0)
            {
                return list;
            }

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                list.Add(new PrescriptionItem
                {
                    ItemId = Convert.ToInt32(row["ItemId"]),
                    PrescriptionId = Convert.ToInt32(row["PrescriptionId"]),
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