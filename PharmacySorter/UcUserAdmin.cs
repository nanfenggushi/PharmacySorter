using BLL;
using Model;
using System;
using System.Windows.Forms;

namespace PharmacySorter
{
    /// <summary>
    /// 账号与权限维护。只有管理员可以进入。
    /// </summary>
    public partial class UcUserAdmin : UserControl
    {
        private readonly UserBLL userBll = new UserBLL();
        private int? editingUserId;

        /// <summary>
        /// 刷新表格时忽略选中变化，避免把第一行自动填进输入区。
        /// </summary>
        private bool loadingUsers;

        /// <summary>
        /// 为 true 时正在切换到新增状态，表格取消选中不能把旧账号填回输入框。
        /// </summary>
        private bool clearingSelection;

        public UcUserAdmin()
        {
            InitializeComponent();
            cmbRole.Items.AddRange(UserRole.All);
            dgvUsers.SelectionChanged += dgvUsers_SelectionChanged;
            Load += UcUserAdmin_Load;
        }

        private void UcUserAdmin_Load(object sender, EventArgs e)
        {
            LoadUsers();
            BeginAdd();
        }

        /// <summary>
        /// 按上方输入创建账号。表格当前选中哪一行都不影响。
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string displayName = txtDisplayName.Text;
            string password = txtPassword.Text;
            string roleName = cmbRole.Text;

            if (!string.Equals(password, txtConfirmPassword.Text, StringComparison.Ordinal))
            {
                MessageBox.Show("两次输入的密码不一致", "新增失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConfirmPassword.Focus();
                return;
            }

            try
            {
                userBll.Add(userName, displayName, password, roleName);
                clearingSelection = true;
                LoadUsers();
                BeginAdd();
                MessageBox.Show("账号已新增", "账号管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "新增失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 只修改表格中当前选中的账号，不负责新增。
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!editingUserId.HasValue)
            {
                MessageBox.Show("当前没有选中账号。创建新账号请点击“新增”。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            AppUser selected = SelectedUser();
            if (selected != null && !string.Equals(txtUserName.Text.Trim(), selected.UserName, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("保存不能修改登录账号。请改回原账号后保存，或点击“新增”创建新账号。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserName.Focus();
                return;
            }

            if (!ConfirmPassword())
            {
                return;
            }

            try
            {
                userBll.Update(editingUserId.Value, txtDisplayName.Text, cmbRole.Text, chkActive.Checked, txtPassword.Text);
                int savedUserId = editingUserId.Value;
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                LoadUsers();
                SelectUser(savedUserId);
                MessageBox.Show("账号已保存", "账号管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 新增必须填写并确认密码。修改时两个密码框都留空表示不改密码。
        /// </summary>
        private bool ConfirmPassword()
        {
            if (string.Equals(txtPassword.Text, txtConfirmPassword.Text, StringComparison.Ordinal))
            {
                return true;
            }

            MessageBox.Show("两次输入的密码不一致", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtConfirmPassword.Focus();
            return false;
        }

        /// <summary>
        /// 当前表格选中的账号。没有选中时返回 null。
        /// </summary>
        private AppUser SelectedUser()
        {
            return dgvUsers.CurrentRow == null ? null : dgvUsers.CurrentRow.DataBoundItem as AppUser;
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (loadingUsers || clearingSelection)
            {
                return;
            }

            AppUser user = SelectedUser();
            if (user == null)
            {
                return;
            }

            editingUserId = user.UserId;
            txtUserName.Text = user.UserName;
            txtUserName.ReadOnly = false;
            txtDisplayName.Text = user.DisplayName;
            cmbRole.SelectedItem = user.RoleName;
            chkActive.Checked = user.IsActive;
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            lblPassword.Text = "新密码（留空不修改）";
        }

        private void LoadUsers()
        {
            try
            {
                loadingUsers = true;
                dgvUsers.DataSource = null;
                dgvUsers.DataSource = userBll.GetAll();
                dgvUsers.ClearSelection();
                dgvUsers.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "加载账号失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadingUsers = false;
            }
        }

        /// <summary>
        /// 保存修改后重新选中原来的账号，避免刷新列表后编辑区被清空。
        /// </summary>
        private void SelectUser(int userId)
        {
            foreach (DataGridViewRow row in dgvUsers.Rows)
            {
                AppUser user = row.DataBoundItem as AppUser;
                if (user == null || user.UserId != userId)
                {
                    continue;
                }

                row.Selected = true;
                dgvUsers.CurrentCell = row.Cells[0];
                return;
            }
        }

        /// <summary>
        /// 退出表格选中，进入空白的新增状态。登录账号恢复可编辑。
        /// </summary>
        private void BeginAdd()
        {
            clearingSelection = true;
            try
            {
                editingUserId = null;
                txtUserName.Clear();
                txtUserName.ReadOnly = false;
                txtDisplayName.Clear();
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                cmbRole.SelectedIndex = -1;
                chkActive.Checked = true;
                lblPassword.Text = "初始密码";
                dgvUsers.ClearSelection();
                dgvUsers.CurrentCell = null;
            }
            finally
            {
                clearingSelection = false;
            }
        }
    }
}
