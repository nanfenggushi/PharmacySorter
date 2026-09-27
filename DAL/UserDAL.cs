using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// 登录账号数据访问。只保存密码哈希，不保存明文密码。
    /// </summary>
    public class UserDAL
    {
        public void EnsureReady()
        {
            DbHelper.Update(@"
IF OBJECT_ID('AppUser', 'U') IS NULL
BEGIN
    CREATE TABLE AppUser (
        UserId INT IDENTITY(1,1) PRIMARY KEY,
        UserName NVARCHAR(50) NOT NULL,
        DisplayName NVARCHAR(50) NOT NULL,
        PasswordHash NVARCHAR(200) NOT NULL,
        RoleName NVARCHAR(20) NOT NULL,
        IsActive BIT NOT NULL CONSTRAINT DF_AppUser_IsActive DEFAULT 1,
        CONSTRAINT UQ_AppUser_UserName UNIQUE (UserName)
    );
END");
            const string hash = "PBKDF2$100000$dGVzdC1zYWx0LTAwMDAwMQ==$MN9jQ/waBESrVTLeHCG0b4oSf+VO8pwnB5LvbCsQumw=";
            DbHelper.Update(@"
IF NOT EXISTS (SELECT 1 FROM AppUser WHERE UserName = N'admin')
INSERT INTO AppUser (UserName, DisplayName, PasswordHash, RoleName, IsActive)
VALUES (N'admin', N'系统管理员', @PasswordHash, N'管理员', 1)",
                new SqlParameter("@PasswordHash", hash));
        }

        public AppUser GetByUserName(string userName)
        {
            const string sql = @"
SELECT UserId, UserName, DisplayName, RoleName, IsActive, PasswordHash
FROM AppUser
WHERE UserName = @UserName";
            List<AppUser> list = Map(DbHelper.Find(sql, new SqlParameter("@UserName", userName)));
            return list.Count == 0 ? null : list[0];
        }

        public List<AppUser> GetAll()
        {
            const string sql = @"
SELECT UserId, UserName, DisplayName, RoleName, IsActive, PasswordHash
FROM AppUser
ORDER BY UserId";
            return Map(DbHelper.Find(sql));
        }

        public int CountActiveAdmins(int exceptUserId)
        {
            object result = DbHelper.Scalar(@"
SELECT COUNT(1)
FROM AppUser
WHERE RoleName = N'管理员' AND IsActive = 1 AND UserId <> @UserId",
                new SqlParameter("@UserId", exceptUserId));
            return Convert.ToInt32(result);
        }

        public int Add(AppUser user)
        {
            const string sql = @"
INSERT INTO AppUser (UserName, DisplayName, PasswordHash, RoleName, IsActive)
VALUES (@UserName, @DisplayName, @PasswordHash, @RoleName, @IsActive);
SELECT CAST(SCOPE_IDENTITY() AS INT);";
            object result = DbHelper.Scalar(sql,
                new SqlParameter("@UserName", user.UserName),
                new SqlParameter("@DisplayName", user.DisplayName),
                new SqlParameter("@PasswordHash", user.PasswordHash),
                new SqlParameter("@RoleName", user.RoleName),
                new SqlParameter("@IsActive", user.IsActive));
            return Convert.ToInt32(result);
        }

        public int Update(AppUser user, bool changePassword)
        {
            string sql = changePassword
                ? @"UPDATE AppUser
SET DisplayName = @DisplayName, RoleName = @RoleName, IsActive = @IsActive, PasswordHash = @PasswordHash
WHERE UserId = @UserId"
                : @"UPDATE AppUser
SET DisplayName = @DisplayName, RoleName = @RoleName, IsActive = @IsActive
WHERE UserId = @UserId";
            return DbHelper.Update(sql,
                new SqlParameter("@DisplayName", user.DisplayName),
                new SqlParameter("@RoleName", user.RoleName),
                new SqlParameter("@IsActive", user.IsActive),
                new SqlParameter("@PasswordHash", (object)user.PasswordHash ?? DBNull.Value),
                new SqlParameter("@UserId", user.UserId));
        }

        private static List<AppUser> Map(DataSet dataSet)
        {
            List<AppUser> list = new List<AppUser>();
            if (dataSet.Tables.Count == 0)
            {
                return list;
            }

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                list.Add(new AppUser
                {
                    UserId = Convert.ToInt32(row["UserId"]),
                    UserName = row["UserName"].ToString(),
                    DisplayName = row["DisplayName"].ToString(),
                    RoleName = row["RoleName"].ToString(),
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    PasswordHash = row["PasswordHash"].ToString()
                });
            }
            return list;
        }
    }
}
