using BLL;
using Common;
using Model;
using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace PharmacySorter
{
    public partial class FrmMain : Form
    {
        // 配药监控看板
        private UserControl ucDashboard = null;
        // 工位与药品配置
        private UserControl ucDrugStationMapping = null;
        // 药品字典管理
        private UserControl ucDrugDictionary = null;
        // 处方录入与队列管理
        private UserControl ucPrescriptionQueue = null;
        // 系统操作日志审计
        private UserControl ucSystemAuditLog = null;

        // 账号与权限管理
        private UserControl ucUserAdmin = null;

        /// <summary>
        /// 当前登录账号。菜单和操作权限都根据它的角色判断。
        /// </summary>
        private readonly AppUser currentUser;
        private readonly Timer clockTimer = new Timer();

        /// <summary>
        /// 全局机械臂连接。急停和看板抓取共用这一条串口。
        /// </summary>
        private readonly ArmCommandService arm = new ArmCommandService();

        public FrmMain(AppUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            currentUser = user;
            InitializeComponent();
            lblOperator.Text = user.DisplayName + "（" + user.RoleName + "）";
            btnEmergencyStop.BackColor = Color.Firebrick;
            btnEmergencyStop.ForeColor = Color.White;
            clockTimer.Interval = 1000;
            clockTimer.Tick += clockTimer_Tick;
            Load += FrmMain_Load;
            FormClosed += FrmMain_FormClosed;
        }

        /// <summary>
        /// 打开默认看板，尝试连接串口，并下发一次开机复位。
        /// </summary>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            clockTimer.Start();
            ApplyPermissions();
            clockTimer_Tick(this, EventArgs.Empty);
            ShowConnection(false, ConfigurationManager.AppSettings["ArmPortName"]);
            btnDashboard_Click(this, EventArgs.Empty);
            BeginInvoke(new Action(ConnectArm));
        }

        /// <summary>
        /// 关闭窗口时释放串口和时钟。
        /// </summary>
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            clockTimer.Stop();
            clockTimer.Dispose();
            arm.Disconnect();
        }

        /// <summary>
        /// 刷新右上角系统时间。
        /// </summary>
        private void clockTimer_Tick(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 按配置打开串口。打不开时界面显示未连接，配药流程仍可按延时演示。
        /// </summary>
        private void ConnectArm()
        {
            arm.SkipWait = string.Equals(ConfigurationManager.AppSettings["SkipArmWait"], "true", StringComparison.OrdinalIgnoreCase);
            string portName = ConfigurationManager.AppSettings["ArmPortName"];
            try
            {
                arm.Connect(portName);
                // 开机先回到 G0002 待命姿态。未连接时 SendStandby 只等待，不中断启动。
                arm.SendStandby();
                ShowConnection(true, arm.PortName);
            }
            catch (Exception)
            {
                ShowConnection(false, portName);
            }
        }

        /// <summary>
        /// 更新通信状态标签。已连接为绿色，未连接为红色。
        /// </summary>
        private void ShowConnection(bool connected, string portName)
        {
            if (connected)
            {
                lblConnection.Text = "通信状态：已连接 " + portName;
                lblConnection.ForeColor = Color.SeaGreen;
                return;
            }

            string name = string.IsNullOrWhiteSpace(portName) ? "未配置" : portName.Trim();
            lblConnection.Text = "通信状态：未连接 " + name;
            lblConnection.ForeColor = Color.Firebrick;
        }

        /// <summary>
        /// 按角色显示菜单。管理员拥有全部权限，药师维护药品和处方，操作员负责配药。
        /// </summary>
        private void ApplyPermissions()
        {
            bool isAdmin = currentUser.RoleName == UserRole.Admin;
            bool isPharmacist = currentUser.RoleName == UserRole.Pharmacist;
            btnStationMapping.Visible = isAdmin;
            btnDrugDictionary.Visible = isAdmin || isPharmacist;
            btnPrescription.Visible = isAdmin || isPharmacist;
            btnAuditLog.Visible = isAdmin;
            btnUserAdmin.Visible = isAdmin;
            btnDashboard.Visible = true;
            btnEmergencyStop.Visible = true;
        }

        private void btnUserAdmin_Click(object sender, EventArgs e)
        {
            if (currentUser.RoleName != UserRole.Admin)
            {
                return;
            }

            PageHelper.SwitchPage<UcUserAdmin>(pnlPageContainer, ref ucUserAdmin);
        }

        // 跳转到配药监控看板
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            PageHelper.SwitchPage<UcDashboard>(pnlPageContainer, ref ucDashboard);
            UcDashboard dashboard = ucDashboard as UcDashboard;
            if (dashboard != null)
            {
                dashboard.BindArm(arm);
            }
        }

        // 跳转到工位与药品配置
        private void btnStationMapping_Click(object sender, EventArgs e)
        {
            PageHelper.SwitchPage<UcDrugStationMapping>(pnlPageContainer, ref ucDrugStationMapping);
        }

        // 跳转到药品字典管理
        private void btnDrugDictionary_Click(object sender, EventArgs e)
        {
            PageHelper.SwitchPage<UcDrugDictionary>(pnlPageContainer, ref ucDrugDictionary);
        }

        // 跳转到处方录入与队列管理
        private void btnPrescription_Click(object sender, EventArgs e)
        {
            PageHelper.SwitchPage<UcPrescriptionQueue>(pnlPageContainer, ref ucPrescriptionQueue);
        }

        // 跳转到系统操作日志审计
        private void btnAuditLog_Click(object sender, EventArgs e)
        {
            PageHelper.SwitchPage<UcSystemAuditLog>(pnlPageContainer, ref ucSystemAuditLog);
        }

        /// <summary>
        /// 急停。先打断正在等待的动作，再下发 G0002，让机械臂抬起并张开夹爪。
        /// </summary>
        private void btnEmergencyStop_Click(object sender, EventArgs e)
        {
            arm.RequestStop();
            UcDashboard dashboard = ucDashboard as UcDashboard;
            if (dashboard != null)
            {
                dashboard.RequestStop();
            }

            try
            {
                // RequestStop 会让普通发送直接返回，复位前要先清掉标记。
                arm.ClearStop();
                arm.SendStandby();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "急停复位失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
