using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Common
{
    public class DbHelper
    {
        // 判断是否有连接字符串（??是空合并运算符，意思是前面的值不为空就用前面的值，为空就用后面的值）
        private static readonly string connStr = ConfigurationManager.ConnectionStrings["connStr"]?.ConnectionString
                                ?? throw new InvalidOperationException("未在App.config中配置名为'connStr'的连接字符串");

        /// <summary>
        /// 供需要自行开启事务的数据访问使用，避免连接字符串散落在多个类里。
        /// </summary>
        public static string ConnectionString
        {
            get { return connStr; }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public static DataSet Find(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            }
        }

        /// <summary>
        /// 增删改方法
        /// </summary>
        /// <param name="sql">增删改语句</param>
        /// <param name="parameters">增删改参数</param>
        /// <returns></returns>
        public static int Update(string sql, params SqlParameter[] parameters)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            return count;
        }

        /// <summary>
        /// 执行返回单个值的语句（如 COUNT、SCOPE_IDENTITY）
        /// </summary>
        public static object Scalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }
    }
}
