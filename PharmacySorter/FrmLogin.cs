using BLL;
using Model;
using System;
using System.Windows.Forms;

namespace PharmacySorter
{
    /// <summary>
    /// 登录窗口。只有校验通过才允许进入主界面。
    /// </summary>
    public partial class FrmLogin : Form
    {
        private readonly UserBLL userBll = new UserBLL();

        public AppUser CurrentUser { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();
            AcceptButton = btnLogin;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentUser = userBll.Login(txtUserName.Text, txtPassword.Text);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.SelectAll();
                txtPassword.Focus();
            }
        }
    }
}
