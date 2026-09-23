using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PharmacySorter
{
    /// <summary>
    /// 配药监控看板。展示当前处方、明细进度和机械臂作业方向。
    /// 启动前只做工位和药品绑定检查，真正的抓取循环由核对流程继续。
    /// </summary>
    public partial class UcDashboard : UserControl
    {
        /// <summary>
        /// 看板业务。界面不直接查询数据库。
        /// </summary>
        private readonly DispenseBLL dispenseBll = new DispenseBLL();

        /// <summary>
        /// 当前展示的处方。没有待处理处方时为空。
        /// </summary>
        private Prescription currentPrescription;

        /// <summary>
        /// 当前处方的明细，表格直接绑定这份列表。
        /// </summary>
        private List<PrescriptionItem> currentItems = new List<PrescriptionItem>();

        /// <summary>
        /// 工位指示的常态颜色。动作中再改成高亮色。
        /// </summary>
        private readonly Color stationIdleColor = Color.WhiteSmoke;

        public UcDashboard()
        {
            InitializeComponent();
            dgvItems.CellFormatting += dgvItems_CellFormatting;
            Load += UcDashboard_Load;
        }

        /// <summary>
        /// 每次进入看板都重新读取队列，避免处方页提交后这里还是旧数据。
        /// </summary>
        private void UcDashboard_Load(object sender, EventArgs e)
        {
            LoadCurrent();
        }

        /// <summary>
        /// 检查当前处方能否启动。条件不满足时说明原因，不改变处方状态。
        /// </summary>
        private void btnStart_Click(object sender, EventArgs e)
        {
            string reason = dispenseBll.GetStartBlockReason(currentPrescription, currentItems);
            if (!string.IsNullOrEmpty(reason))
            {
                AppendLog(reason);
                MessageBox.Show(reason, "不能启动", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            AppendLog("处方 " + currentPrescription.PrescriptionId + " 检查通过，可以开始配药");
            MessageBox.Show("工位和药品绑定检查通过。抓取循环将在数量核对流程中执行。", "可以启动", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 读取当前处方和明细，并刷新摘要、进度和工位指示。
        /// </summary>
        private void LoadCurrent()
        {
            try
            {
                currentPrescription = dispenseBll.GetCurrentPrescription();
                currentItems = currentPrescription == null
                    ? new List<PrescriptionItem>()
                    : dispenseBll.GetCurrentItems(currentPrescription.PrescriptionId);
            }
            catch (Exception ex)
            {
                currentPrescription = null;
                currentItems = new List<PrescriptionItem>();
                AppendLog("读取看板数据失败：" + ex.Message);
            }

            BindSummary();
            BindItems();
            ShowActiveStation(null);
        }

        /// <summary>
        /// 顶部显示处方号、患者编号和整体状态。没有处方时显示空闲。
        /// </summary>
        private void BindSummary()
        {
            if (currentPrescription == null)
            {
                lblSummary.Text = "当前没有待配处方";
                lblStatus.Text = "空闲";
                lblStatus.ForeColor = Color.Gray;
                btnStart.Enabled = false;
                return;
            }

            lblSummary.Text = "处方号 " + currentPrescription.PrescriptionId + "    患者编号 " + currentPrescription.PatientNo;
            lblStatus.Text = currentPrescription.Status;
            lblStatus.ForeColor = GetStatusColor(currentPrescription.Status);
            btnStart.Enabled = currentPrescription.Status == "待配药";
        }

        /// <summary>
        /// 绑定明细表，并在标题上显示已核对条数和总条数。
        /// </summary>
        private void BindItems()
        {
            int passed = dispenseBll.CountPassed(currentItems);
            lblItems.Text = "处方明细 " + passed + "/" + currentItems.Count;
            dgvItems.DataSource = null;
            dgvItems.DataSource = currentItems;
        }

        /// <summary>
        /// 正在处理的行用黄色，核对通过的行用绿色，异常行用红色。
        /// </summary>
        private void dgvItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvItems.Rows.Count)
            {
                return;
            }

            PrescriptionItem item = dgvItems.Rows[e.RowIndex].DataBoundItem as PrescriptionItem;
            if (item == null)
            {
                return;
            }

            DataGridViewCellStyle style = dgvItems.Rows[e.RowIndex].DefaultCellStyle;
            if (item.Status == "取药中")
            {
                style.BackColor = Color.LightYellow;
                style.ForeColor = Color.Black;
            }
            else if (item.Status == "核对通过")
            {
                style.BackColor = Color.Honeydew;
                style.ForeColor = Color.DarkGreen;
            }
            else if (item.Status == "异常")
            {
                style.BackColor = Color.MistyRose;
                style.ForeColor = Color.DarkRed;
            }
            else
            {
                style.BackColor = dgvItems.DefaultCellStyle.BackColor;
                style.ForeColor = dgvItems.DefaultCellStyle.ForeColor;
            }
        }

        /// <summary>
        /// 点亮正在作业的工位。传入空值时三个工位都恢复常态。
        /// </summary>
        /// <param name="stationId">1 正前分拣槽，2 左侧药位，3 右侧药位。</param>
        private void ShowActiveStation(int? stationId)
        {
            pnlLeftStation.BackColor = stationId == 2 ? Color.Gold : stationIdleColor;
            pnlSlot.BackColor = stationId == 1 ? Color.DeepSkyBlue : stationIdleColor;
            pnlRightStation.BackColor = stationId == 3 ? Color.Gold : stationIdleColor;

            if (stationId == 2)
            {
                lblArmHint.Text = "机械臂正在左侧药位作业";
            }
            else if (stationId == 3)
            {
                lblArmHint.Text = "机械臂正在右侧药位作业";
            }
            else if (stationId == 1)
            {
                lblArmHint.Text = "机械臂正在正前分拣槽投递";
            }
            else
            {
                lblArmHint.Text = "机械臂待命";
            }
        }

        /// <summary>
        /// 在底部日志中追加一行，并滚到最新内容。
        /// </summary>
        private void AppendLog(string message)
        {
            txtLog.AppendText(DateTime.Now.ToString("HH:mm:ss") + " - " + message + Environment.NewLine);
        }

        /// <summary>
        /// 处方状态对应的标签颜色。未知状态用灰色。
        /// </summary>
        private static Color GetStatusColor(string status)
        {
            if (status == "配药中")
            {
                return Color.DodgerBlue;
            }
            if (status == "已完成")
            {
                return Color.SeaGreen;
            }
            if (status == "部分异常")
            {
                return Color.Firebrick;
            }
            if (status == "待配药")
            {
                return Color.DarkOrange;
            }
            return Color.Gray;
        }
    }
}
