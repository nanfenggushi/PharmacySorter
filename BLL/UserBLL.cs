using Common;
using DAL;
using Model;
using System;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// 登录与账号权限。密码只以哈希形式交给数据层。
    /// </summary>
    public class UserBLL
    {
        private readonly UserDAL dal = new UserDAL();
        private readonly AppLogBLL logBll = new AppLogBLL();

        public AppUser Login(string userName, string password)
        {
            dal.EnsureReady();
            userName = NormalizeUserName(userName);
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("请输入密码");
            }

            AppUser user = dal.GetByUserName(userName);
            if (user == null || !user.IsActive || !PasswordHasher.Verify(password, user.PasswordHash))
            {
                throw new ArgumentException("账号或密码错误，或账号已停用");
            }

            user.PasswordHash = null;
            logBll.Add(null, AppLogType.User, "账号 " + user.UserName + " 登录系统");
            return user;
        }

        public List<AppUser> GetAll()
        {
            List<AppUser> users = dal.GetAll();
            foreach (AppUser user in users)
            {
                user.PasswordHash = null;
            }
            return users;
        }

        public int Add(string userName, string displayName, string password, string roleName)
        {
            AppUser user = new AppUser
            {
                UserName = NormalizeUserName(userName),
                DisplayName = NormalizeDisplayName(displayName),
                PasswordHash = PasswordHasher.Hash(NormalizePassword(password)),
                RoleName = NormalizeRole(roleName),
                IsActive = true
            };
            if (dal.GetByUserName(user.UserName) != null)
            {
                throw new ArgumentException("登录账号已存在");
            }

            int userId = dal.Add(user);
            logBll.Add(null, AppLogType.User, "新增账号 " + user.UserName + "，角色 " + user.RoleName);
            return userId;
        }

        public void Update(int userId, string displayName, string roleName, bool isActive, string newPassword)
        {
            AppUser current = FindById(userId);
            bool changePassword = !string.IsNullOrWhiteSpace(newPassword);
            AppUser user = new AppUser
            {
                UserId = userId,
                DisplayName = NormalizeDisplayName(displayName),
                RoleName = NormalizeRole(roleName),
                IsActive = isActive,
                PasswordHash = changePassword ? PasswordHasher.Hash(NormalizePassword(newPassword)) : null
            };
            EnsureAdminRemains(current, user);
            if (dal.Update(user, changePassword) == 0)
            {
                throw new ArgumentException("账号不存在");
            }

            logBll.Add(null, AppLogType.User, "修改账号 " + current.UserName + "，角色 " + user.RoleName + "，状态 " + (user.IsActive ? "启用" : "停用"));
        }

        public void ChangePassword(int userId, string oldPassword, string newPassword)
        {
            AppUser user = FindById(userId);
            if (!PasswordHasher.Verify(oldPassword ?? string.Empty, user.PasswordHash))
            {
                throw new ArgumentException("原密码不正确");
            }

            user.PasswordHash = PasswordHasher.Hash(NormalizePassword(newPassword));
            dal.Update(user, true);
            logBll.Add(null, AppLogType.User, "账号 " + user.UserName + " 修改了自己的密码");
        }

        private AppUser FindById(int userId)
        {
            foreach (AppUser user in dal.GetAll())
            {
                if (user.UserId == userId)
                {
                    return user;
                }
            }
            throw new ArgumentException("账号不存在");
        }

        private void EnsureAdminRemains(AppUser current, AppUser updated)
        {
            bool removesAdmin = current.RoleName == UserRole.Admin && current.IsActive
                && (updated.RoleName != UserRole.Admin || !updated.IsActive);
            if (removesAdmin && dal.CountActiveAdmins(current.UserId) == 0)
            {
                throw new ArgumentException("至少要保留一个启用的管理员");
            }
        }

        private static string NormalizeUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("请输入登录账号");
            }

            userName = userName.Trim();
            if (userName.Length < 3 || userName.Length > 50)
            {
                throw new ArgumentException("登录账号长度必须在 3 到 50 个字符之间");
            }
            return userName;
        }

        private static string NormalizeDisplayName(string displayName)
        {
            if (string.IsNullOrWhiteSpace(displayName))
            {
                throw new ArgumentException("请输入姓名");
            }

            displayName = displayName.Trim();
            if (displayName.Length > 50)
            {
                throw new ArgumentException("姓名不能超过 50 个字符");
            }
            return displayName;
        }

        private static string NormalizePassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8 || password.Length > 50)
            {
                throw new ArgumentException("密码长度必须在 8 到 50 个字符之间");
            }
            return password;
        }

        private static string NormalizeRole(string roleName)
        {
            if (!UserRole.IsValid(roleName))
            {
                throw new ArgumentException("请选择有效角色");
            }
            return roleName;
        }
    }
}
