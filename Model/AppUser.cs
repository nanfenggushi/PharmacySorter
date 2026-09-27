namespace Model
{
    /// <summary>
    /// 登录账号。密码哈希只在校验时使用，不提供给界面显示。
    /// </summary>
    public class AppUser
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        /// <summary>
        /// 管理员、药师、操作员。
        /// </summary>
        public string RoleName { get; set; }

        public bool IsActive { get; set; }

        public string PasswordHash { get; set; }

        public string StatusText
        {
            get { return IsActive ? "启用" : "停用"; }
        }
    }

    /// <summary>
    /// 系统角色。界面和权限判断都使用这些固定名称。
    /// </summary>
    public static class UserRole
    {
        public const string Admin = "管理员";
        public const string Pharmacist = "药师";
        public const string Operator = "操作员";

        public static readonly string[] All = new string[]
        {
            Admin,
            Pharmacist,
            Operator
        };

        public static bool IsValid(string roleName)
        {
            foreach (string role in All)
            {
                if (role == roleName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
