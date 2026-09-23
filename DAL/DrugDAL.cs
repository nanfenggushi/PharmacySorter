using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DrugDAL
    {
        private const string SelectColumns = @"
SELECT d.DrugId, d.DrugName, d.Spec, d.StationId, d.IsActive, s.StationName
FROM Drug d
LEFT JOIN StationAction s ON d.StationId = s.StationId";

        public List<Drug> Search(int? drugId, string drugName, string stationName)
        {
            string sql = SelectColumns + @"
WHERE (@DrugId IS NULL OR d.DrugId = @DrugId)
  AND (@DrugName IS NULL OR d.DrugName LIKE @DrugName)
  AND (@StationName IS NULL OR s.StationName LIKE @StationName)
ORDER BY d.DrugId";

            DataSet dataSet = DbHelper.Find(sql,
                new SqlParameter("@DrugId", (object)drugId ?? DBNull.Value),
                new SqlParameter("@DrugName", LikeOrNull(drugName)),
                new SqlParameter("@StationName", LikeOrNull(stationName)));

            return MapList(dataSet);
        }

        /// <summary>
        /// 关键字同时匹配药品编号、药品名称和规格。空关键字返回全部药品。
        /// </summary>
        /// <param name="keyword">查询关键字。</param>
        public List<Drug> SearchByKeyword(string keyword)
        {
            string sql = SelectColumns + @"
WHERE (@Keyword IS NULL
    OR CAST(d.DrugId AS VARCHAR(20)) = @Exact
    OR d.DrugName LIKE @Keyword
    OR d.Spec LIKE @Keyword)
ORDER BY d.DrugId";

            string trimmed = string.IsNullOrWhiteSpace(keyword) ? null : keyword.Trim();
            DataSet dataSet = DbHelper.Find(sql,
                new SqlParameter("@Keyword", LikeOrNull(trimmed)),
                new SqlParameter("@Exact", (object)trimmed ?? DBNull.Value));
            return MapList(dataSet);
        }

        /// <summary>
        /// 仅返回已启用药品，供处方录入和工位绑定使用。
        /// </summary>
        public List<Drug> GetEnabled()
        {
            string sql = SelectColumns + @"
WHERE d.IsActive = 1
ORDER BY d.DrugId";
            return MapList(DbHelper.Find(sql));
        }

        public Drug GetById(int drugId)
        {
            string sql = SelectColumns + " WHERE d.DrugId = @DrugId";
            DataSet dataSet = DbHelper.Find(sql, new SqlParameter("@DrugId", drugId));
            List<Drug> list = MapList(dataSet);
            return list.Count == 0 ? null : list[0];
        }

        public bool ExistsName(string drugName, int? exceptDrugId)
        {
            string sql = @"SELECT COUNT(1) FROM Drug
WHERE DrugName = @DrugName
  AND (@DrugId IS NULL OR DrugId <> @DrugId)";
            object result = DbHelper.Scalar(sql,
                new SqlParameter("@DrugName", drugName),
                new SqlParameter("@DrugId", (object)exceptDrugId ?? DBNull.Value));
            return Convert.ToInt32(result) > 0;
        }

        /// <summary>
        /// 新增药品。stationId 为空表示暂不绑定工位。
        /// </summary>
        public int Add(string drugName, string spec, int? stationId)
        {
            string sql = @"
SET NOCOUNT ON;
INSERT INTO Drug (DrugName, Spec, StationId, IsActive)
VALUES (@DrugName, @Spec, @StationId, 1);
SELECT CAST(SCOPE_IDENTITY() AS INT);";
            object result = DbHelper.Scalar(sql,
                new SqlParameter("@DrugName", drugName),
                new SqlParameter("@Spec", DbText(spec)),
                new SqlParameter("@StationId", (object)stationId ?? DBNull.Value));
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// 修改药品名称、规格和绑定工位。药品编号不变。
        /// </summary>
        public int Update(int drugId, string drugName, string spec, int? stationId)
        {
            string sql = @"UPDATE Drug
SET DrugName = @DrugName, Spec = @Spec, StationId = @StationId
WHERE DrugId = @DrugId";
            return DbHelper.Update(sql,
                new SqlParameter("@DrugName", drugName),
                new SqlParameter("@Spec", DbText(spec)),
                new SqlParameter("@StationId", (object)stationId ?? DBNull.Value),
                new SqlParameter("@DrugId", drugId));
        }

        public int SetActive(int drugId, bool isActive)
        {
            string sql = "UPDATE Drug SET IsActive = @IsActive WHERE DrugId = @DrugId";
            return DbHelper.Update(sql,
                new SqlParameter("@IsActive", isActive),
                new SqlParameter("@DrugId", drugId));
        }

        /// <summary>
        /// 只修改药品绑定的工位。stationId 为空表示解除绑定。
        /// </summary>
        public int SetStation(int drugId, int? stationId)
        {
            string sql = "UPDATE Drug SET StationId = @StationId WHERE DrugId = @DrugId";
            return DbHelper.Update(sql,
                new SqlParameter("@StationId", (object)stationId ?? DBNull.Value),
                new SqlParameter("@DrugId", drugId));
        }

        private static List<Drug> MapList(DataSet dataSet)
        {
            List<Drug> list = new List<Drug>();
            if (dataSet.Tables.Count == 0)
            {
                return list;
            }

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                list.Add(new Drug
                {
                    DrugId = Convert.ToInt32(row["DrugId"]),
                    DrugName = row["DrugName"].ToString(),
                    Spec = row["Spec"] == DBNull.Value ? string.Empty : row["Spec"].ToString(),
                    StationId = row["StationId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["StationId"]),
                    StationName = row["StationName"] == DBNull.Value ? string.Empty : row["StationName"].ToString(),
                    IsActive = Convert.ToBoolean(row["IsActive"])
                });
            }
            return list;
        }

        private static object DbText(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : value.Trim();
        }

        private static object LikeOrNull(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return DBNull.Value;
            }

            string escaped = value.Trim()
                .Replace("[", "[[]")
                .Replace("%", "[%]")
                .Replace("_", "[_]");
            return "%" + escaped + "%";
        }
    }
}