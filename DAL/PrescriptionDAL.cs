using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// 固定处方的数据访问。处方只保存名称和药品清单，不保存患者和配药进度。
    /// </summary>
    public partial class PrescriptionDAL
    {
        public List<Prescription> GetAll()
        {
            const string sql = @"
                        SELECT p.PrescriptionId, p.PrescriptionName, p.CreateTime,
                               (SELECT COUNT(1) FROM PrescriptionItem i WHERE i.PrescriptionId = p.PrescriptionId) AS ItemCount
                        FROM Prescription p
                        ORDER BY p.PrescriptionName, p.PrescriptionId";
            return MapPrescriptions(DbHelper.Find(sql));
        }

        public Prescription GetById(int prescriptionId)
        {
            const string sql = @"
                        SELECT p.PrescriptionId, p.PrescriptionName, p.CreateTime,
                               (SELECT COUNT(1) FROM PrescriptionItem i WHERE i.PrescriptionId = p.PrescriptionId) AS ItemCount
                        FROM Prescription p
                        WHERE p.PrescriptionId = @PrescriptionId";
            List<Prescription> list = MapPrescriptions(DbHelper.Find(sql, new SqlParameter("@PrescriptionId", prescriptionId)));
            return list.Count == 0 ? null : list[0];
        }

        public int Add(string prescriptionName, IList<PrescriptionItem> items)
        {
            using (SqlConnection connection = OpenConnection())
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    int prescriptionId = InsertPrescription(connection, transaction, prescriptionName);
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

        public void Update(int prescriptionId, string prescriptionName, IList<PrescriptionItem> items)
        {
            using (SqlConnection connection = OpenConnection())
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    const string updateSql = "UPDATE Prescription SET PrescriptionName = @PrescriptionName WHERE PrescriptionId = @PrescriptionId";
                    using (SqlCommand update = new SqlCommand(updateSql, connection, transaction))
                    {
                        update.Parameters.Add(new SqlParameter("@PrescriptionName", prescriptionName));
                        update.Parameters.Add(new SqlParameter("@PrescriptionId", prescriptionId));
                        if (update.ExecuteNonQuery() == 0)
                        {
                            throw new ArgumentException("处方不存在");
                        }
                    }

                    using (SqlCommand delete = new SqlCommand("DELETE FROM PrescriptionItem WHERE PrescriptionId = @PrescriptionId", connection, transaction))
                    {
                        delete.Parameters.Add(new SqlParameter("@PrescriptionId", prescriptionId));
                        delete.ExecuteNonQuery();
                    }

                    foreach (PrescriptionItem item in items)
                    {
                        InsertItem(connection, transaction, prescriptionId, item);
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public int Delete(int prescriptionId)
        {
            using (SqlConnection connection = OpenConnection())
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    using (SqlCommand deleteItems = new SqlCommand("DELETE FROM PrescriptionItem WHERE PrescriptionId = @PrescriptionId", connection, transaction))
                    {
                        deleteItems.Parameters.Add(new SqlParameter("@PrescriptionId", prescriptionId));
                        deleteItems.ExecuteNonQuery();
                    }

                    int count;
                    using (SqlCommand delete = new SqlCommand("DELETE FROM Prescription WHERE PrescriptionId = @PrescriptionId", connection, transaction))
                    {
                        delete.Parameters.Add(new SqlParameter("@PrescriptionId", prescriptionId));
                        count = delete.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    return count;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool HasOrders(int prescriptionId)
        {
            object result = DbHelper.Scalar(
                "SELECT COUNT(1) FROM DispenseOrder WHERE PrescriptionId = @PrescriptionId",
                new SqlParameter("@PrescriptionId", prescriptionId));
            return Convert.ToInt32(result) > 0;
        }

        public List<PrescriptionItem> GetItems(int prescriptionId)
        {
            const string sql = @"
                        SELECT i.ItemId, i.PrescriptionId, i.DrugId, d.DrugName, d.Spec, i.RequiredQty
                        FROM PrescriptionItem i
                        INNER JOIN Drug d ON i.DrugId = d.DrugId
                        WHERE i.PrescriptionId = @PrescriptionId
                        ORDER BY i.ItemId";
            return MapItems(DbHelper.Find(sql, new SqlParameter("@PrescriptionId", prescriptionId)));
        }

        private static int InsertPrescription(SqlConnection connection, SqlTransaction transaction, string prescriptionName)
        {
            const string sql = @"
                            INSERT INTO Prescription (PrescriptionName)
                            VALUES (@PrescriptionName);
                            SELECT CAST(SCOPE_IDENTITY() AS INT);";
            using (SqlCommand command = new SqlCommand(sql, connection, transaction))
            {
                command.Parameters.Add(new SqlParameter("@PrescriptionName", prescriptionName));
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private static void InsertItem(SqlConnection connection, SqlTransaction transaction, int prescriptionId, PrescriptionItem item)
        {
            const string sql = @"
                            INSERT INTO PrescriptionItem (PrescriptionId, DrugId, RequiredQty)
                            VALUES (@PrescriptionId, @DrugId, @RequiredQty)";
            using (SqlCommand command = new SqlCommand(sql, connection, transaction))
            {
                command.Parameters.Add(new SqlParameter("@PrescriptionId", prescriptionId));
                command.Parameters.Add(new SqlParameter("@DrugId", item.DrugId));
                command.Parameters.Add(new SqlParameter("@RequiredQty", item.RequiredQty));
                command.ExecuteNonQuery();
            }
        }

        private static SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(DbHelper.ConnectionString);
            connection.Open();
            return connection;
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
                    PrescriptionName = row["PrescriptionName"].ToString(),
                    CreateTime = Convert.ToDateTime(row["CreateTime"]),
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
                    RequiredQty = Convert.ToInt32(row["RequiredQty"])
                });
            }
            return list;
        }
    }
}
