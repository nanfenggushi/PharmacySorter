using BLL;
using Model;
using System;
using System.Windows.Forms;

namespace PharmacySorter
{
    /// <summary>
    /// 系统运维模块。单人使用的上位机不再维护账号列表，
    /// 页面保留系统日志查询和当前账号的密码修改。
    /// </summary>
    public partial class UcSystemOps : UserControl
    {
        /// <summary>
        /// 账号业务。修改当前登录账号的密码。
        /// </summary>
        private readonly UserBLL userBll = new UserBLL();

        /// <summary>
        /// 当前登录账号。主窗体每次切到本页时传入。
        /// </summary>
        private AppUser currentUser;

        public UcSystemOps()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 绑定当前登录账号，修改密码时使用。
        /// </summary>
        public void BindUser(AppUser user)
        {
            currentUser = user;
        }

        /// <summary>
        /// 修改当前账号的登录密码。验证原密码通过后新密码立即生效，下次登录使用新密码。
        /// </summary>
        private void BtnChangePassword_Click(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("当前没有登录账号，不能修改密码", "修改密码", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("两次输入的新密码不一致", "修改密码", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConfirmPassword.SelectAll();
                txtConfirmPassword.Focus();
                return;
            }

            try
            {
                userBll.ChangePassword(currentUser.UserId, txtOldPassword.Text, txtNewPassword.Text);
                txtOldPassword.Text = string.Empty;
                txtNewPassword.Text = string.Empty;
                txtConfirmPassword.Text = string.Empty;
                MessageBox.Show("密码已修改，下次登录请使用新密码", "修改密码", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "修改失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
