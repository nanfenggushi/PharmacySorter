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
        // 固定处方维护
        private UserControl ucPrescriptionCatalog = null;
        // 待配队列
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
        /// 当前选中的导航按钮。
        /// </summary>
        private Button selectedNavButton;

        /// <summary>
        /// 导航按钮未选中时的背景色。
        /// </summary>
        private readonly Color navNormalColor = SystemColors.Control;

        /// <summary>
        /// 导航按钮未选中时的文字颜色。
        /// </summary>
        private readonly Color navNormalTextColor = SystemColors.ControlText;

        /// <summary>
        /// 当前页面导航按钮的背景色。
        /// </summary>
        private readonly Color navSelectedColor = Color.FromArgb(0, 120, 215);

        /// <summary>
        /// 当前页面导航按钮的文字颜色。
        /// </summary>
        private readonly Color navSelectedTextColor = Color.White;

        /// <summary>
        /// 全局机械臂连接。急停和看板抓取共用这一条串口。
        /// </summary>
        private readonly ArmCommandService arm = new ArmCommandService();

        /// <summary>
        /// 全局落药检测。主窗体先打开通道并显示状态，看板配药复用同一通道。
        /// </summary>
        private readonly IDropDetector dropDetector = DropDetectorFactory.Create();

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
            clockTimer.Tick += ClockTimer_Tick;
            Load += FrmMain_Load;
            FormClosed += FrmMain_FormClosed;
        }

        /// <summary>
        /// 打开默认看板，尝试连接串口和落药传感器，并下发一次开机复位。
        /// </summary>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            clockTimer.Start(); // 启动定时器
            ApplyPermissions(); // 按角色权限显示不同菜单
            ClockTimer_Tick(this, EventArgs.Empty);
            ShowConnection(false, ConfigurationManager.AppSettings["ArmPortName"]);
            ShowSensorStatus(false, ConfigurationManager.AppSettings["SensorPortName"]);
            BtnDashboard_Click(this, EventArgs.Empty); // 切换到看板界面
            BeginInvoke(new Action(ConnectArm));
            BeginInvoke(new Action(ConnectSensor));
        }

        /// <summary>
        /// 关闭窗口时释放串口、落药检测通道和时钟。
        /// </summary>
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            clockTimer.Stop();
            clockTimer.Dispose();
            arm.Disconnect();
            dropDetector.Close();
        }

        /// <summary>
        /// 刷新右上角系统时间。
        /// </summary>
        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 按配置打开串口。打不开时界面显示未连接，不能启动配药。
        /// </summary>
        private void ConnectArm()
        {
            arm.SkipWait = string.Equals(ConfigurationManager.AppSettings["SkipArmWait"], "true", StringComparison.OrdinalIgnoreCase);
            string portName = ConfigurationManager.AppSettings["ArmPortName"];
            try
            {
                arm.Connect(portName);
                // 开机先回到 G0002 待命姿态。串口未打开时 SendStandby 会失败，启动配药会被拦住。
                arm.SendStandby();
                ShowConnection(true, arm.PortName);
                UcDashboard dashboard = ucDashboard as UcDashboard;
                if (dashboard != null)
                {
                    dashboard.StartWhenConnected();
                }
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
        /// 按配置打开落药检测通道。打不开时界面显示未连接，配药环节检测到未连接会停止。
        /// </summary>
        private void ConnectSensor()
        {
            string portName = ConfigurationManager.AppSettings["SensorPortName"];
            try
            {
                dropDetector.Open();
                ShowSensorStatus(true, portName);
            }
            catch (Exception)
            {
                ShowSensorStatus(false, portName);
            }
        }

        /// <summary>
        /// 更新传感器状态标签。模拟模式显示检测器自带的说明（含成功率），
        /// 真实传感器模式已连接为绿色，未连接为红色。
        /// </summary>
        private void ShowSensorStatus(bool connected, string portName)
        {
            string name = string.IsNullOrWhiteSpace(portName) ? "未配置" : portName.Trim();

            if (connected && dropDetector is SimulatedDropDetector)
            {
                lblSensorStatus.Text = "传感器状态：" + dropDetector.SourceName;
                lblSensorStatus.ForeColor = Color.SeaGreen;
                return;
            }

            if (connected)
            {
                lblSensorStatus.Text = "传感器状态：已连接 " + name;
                lblSensorStatus.ForeColor = Color.SeaGreen;
                return;
            }

            lblSensorStatus.Text = "传感器状态：未连接 " + name;
            lblSensorStatus.ForeColor = Color.Firebrick;
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
            btnPrescriptionCatalog.Visible = isAdmin || isPharmacist;
            btnAuditLog.Visible = isAdmin;
            btnUserAdmin.Visible = isAdmin;
            btnDashboard.Visible = true;
            btnEmergencyStop.Visible = true;
            PrepareNavButtons();
        }

        /// <summary>
        /// 关闭导航按钮的系统绘制，否则自定义背景色不会显示。
        /// </summary>
        private void PrepareNavButtons()
        {
            Button[] buttons = new Button[]
            {
                btnDashboard,
                btnStationMapping,
                btnDrugDictionary,
                btnPrescriptionCatalog,
                btnPrescription,
                btnAuditLog,
                btnUserAdmin
            };

            foreach (Button button in buttons)
            {
                button.UseVisualStyleBackColor = false;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.BackColor = navNormalColor;
                button.ForeColor = navNormalTextColor;
            }
        }

        /// <summary>
        /// 高亮当前页面按钮，并把上一个选中按钮恢复成普通颜色。
        /// </summary>
        private void SelectNav(Button button)
        {
            if (selectedNavButton != null && selectedNavButton != button)
            {
                selectedNavButton.BackColor = navNormalColor;
                selectedNavButton.ForeColor = navNormalTextColor;
            }

            selectedNavButton = button;
            button.BackColor = navSelectedColor;
            button.ForeColor = navSelectedTextColor;
        }

        // 跳转到账号与权限管理
        private void BtnUserAdmin_Click(object sender, EventArgs e)
        {
            if (currentUser.RoleName != UserRole.Admin)
            {
                return;
            }

            PageHelper.SwitchPage<UcUserAdmin>(pnlPageContainer, ref ucUserAdmin);
            SelectNav(btnUserAdmin);
        }

        // 跳转到配药监控看板
        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            PageHelper.SwitchPage<UcDashboard>(pnlPageContainer, ref ucDashboard);
            SelectNav(btnDashboard);
            UcDashboard dashboard = ucDashboard as UcDashboard;
            if (dashboard != null)
            {
                dashboard.BindArm(arm);
                dashboard.BindDropDetector(dropDetector);
            }
        }

        // 跳转到工位与药品配置
        private void BtnStationMapping_Click(object sender, EventArgs e)
        {
            PageHelper.SwitchPage<UcDrugStationMapping>(pnlPageContainer, ref ucDrugStationMapping);
            SelectNav(btnStationMapping);
        }

        // 跳转到药品字典管理
        private void BtnDrugDictionary_Click(object sender, EventArgs e)
        {
            PageHelper.SwitchPage<UcDrugDictionary>(pnlPageContainer, ref ucDrugDictionary);
            SelectNav(btnDrugDictionary);
        }

        // 跳转到待配队列
        private void BtnPrescription_Click(object sender, EventArgs e)
        {
            PageHelper.SwitchPage<UcPrescriptionQueue>(pnlPageContainer, ref ucPrescriptionQueue);
            SelectNav(btnPrescription);
        }

        // 跳转到固定处方维护
        private void BtnPrescriptionCatalog_Click(object sender, EventArgs e)
        {
            PageHelper.SwitchPage<UcPrescriptionCatalog>(pnlPageContainer, ref ucPrescriptionCatalog);
            SelectNav(btnPrescriptionCatalog);
        }

        // 跳转到系统操作日志审计
        private void BtnAuditLog_Click(object sender, EventArgs e)
        {
            PageHelper.SwitchPage<UcSystemAuditLog>(pnlPageContainer, ref ucSystemAuditLog);
            SelectNav(btnAuditLog);
        }

        /// <summary>
        /// 急停。先打断正在等待的动作，再下发 G0002，让机械臂抬起并张开夹爪。
        /// </summary>
        private void BtnEmergencyStop_Click(object sender, EventArgs e)
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
