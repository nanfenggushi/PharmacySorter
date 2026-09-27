using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacySorter
{
    /// <summary>
    /// 配药监控看板。展示当前处方、明细进度和机械臂作业方向。
    /// 启动后按明细循环抓取，次数达到应发数量时弹出数量核对。
    /// </summary>
    public partial class UcDashboard : UserControl
    {
        /// <summary>
        /// 看板业务。界面不直接查询数据库。
        /// </summary>
        private readonly DispenseBLL dispenseBll = new DispenseBLL();

        /// <summary>
        /// 机械臂指令发送。由主窗体传入，急停和抓取使用同一条串口。
        /// </summary>
        private ArmCommandService arm = new ArmCommandService();

        /// <summary>
        /// 急停后置为 true，当前抓取循环会在下一次动作前停下来。
        /// </summary>
        private bool stopRequested;

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

        /// <summary>
        /// 正在自动配药。切回看板时不能把进行中的处方刷新成空闲。
        /// </summary>
        private bool dispensing;

        public UcDashboard()
        {
            InitializeComponent();
            dgvItems.CellFormatting += DgvItems_CellFormatting;
            dgvItems.SelectionChanged += DgvItems_SelectionChanged;
            VisibleChanged += UcDashboard_VisibleChanged;
        }

        /// <summary>
        /// 绑定主窗体已经打开的串口。未传入时保留本地对象，仍可按延时演示。
        /// </summary>
        public void BindArm(ArmCommandService commandService)
        {
            if (commandService != null)
            {
                arm = commandService;
            }
        }

        /// <summary>
        /// 请求停止当前抓取循环。复位指令由主窗体统一下发。
        /// </summary>
        public void RequestStop()
        {
            stopRequested = true;
            AppendLog("急停，当前抓取循环已中断");
            ShowActiveStation(null);
        }

        /// <summary>
        /// 每次重新显示看板都读取最新队列。配药过程中不打断当前处方。
        /// </summary>
        private void UcDashboard_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible && !dispensing)
            {
                LoadCurrent();
            }
        }

        /// <summary>
        /// 启动后持续处理队列。当前处方完成后自动读取下一张，没有处方时等待新处方。
        /// </summary>
        private async void BtnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            stopRequested = false;
            dispensing = true;
            arm.ClearStop();
            try
            {
                AppendLog("下发待命指令 " + ArmCommandService.StandbyCommand);
                await Task.Run(new Action(arm.SendStandby));
                while (!stopRequested)
                {
                    if (!BeginCurrentPrescription())
                    {
                        lblStatus.Text = "等待新处方";
                        lblStatus.ForeColor = Color.Gray;
                        await Task.Delay(1000);
                        if (!stopRequested)
                        {
                            LoadCurrent();
                        }
                        continue;
                    }

                    await RunPrescriptionAsync();
                    if (stopRequested)
                    {
                        break;
                    }

                    LoadCurrent();
                }
            }
            catch (Exception ex)
            {
                AppendLog("配药中断：" + ex.Message);
                MessageBox.Show(ex.Message, "配药失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                dispensing = false;
                ShowActiveStation(null);
                LoadCurrent();
            }
        }

        /// <summary>
        /// 开始或继续当前处方。不能配药时跳过并读取下一张，队列空了返回 false。
        /// </summary>
        private bool BeginCurrentPrescription()
        {
            while (!stopRequested)
            {
                if (currentPrescription == null)
                {
                    return false;
                }

                bool resume = currentPrescription.Status == "配药中" || currentPrescription.Status == "部分异常";
                if (!resume && currentPrescription.Status != "待配药")
                {
                    return false;
                }

                string reason = dispenseBll.GetStartBlockReason(currentPrescription, currentItems);
                if (!string.IsNullOrEmpty(reason))
                {
                    AppendLog("跳过处方 " + currentPrescription.PrescriptionId + "：" + reason);
                    LoadCurrent(currentPrescription.PrescriptionId);
                    continue;
                }

                dispenseBll.Start(currentPrescription);
                BindSummary();
                AppendLog((resume ? "继续处方 " : "开始处方 ") + currentPrescription.PrescriptionId);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 逐条处理未完成的明细。每条抓满后必须人工核对。
        /// </summary>
        private async Task RunPrescriptionAsync()
        {
            while (!stopRequested)
            {
                PrescriptionItem item = dispenseBll.FindNextItem(currentItems);
                if (item == null)
                {
                    if (dispenseBll.TryComplete(currentPrescription))
                    {
                        AppendLog("处方 " + currentPrescription.PrescriptionId + " 已完成");
                    }
                    return;
                }

                int count = item.Status == "待核对" ? 0 : Math.Max(item.RequiredQty - item.GrabCount, 0);
                if (count > 0)
                {
                    await GrabTimesAsync(item, count);
                }

                if (stopRequested)
                {
                    return;
                }

                dispenseBll.MarkWaitingCheck(item);
                BindItems();
                int nextCount = VerifyItem(item);
                while (nextCount > 0 && !stopRequested)
                {
                    await GrabTimesAsync(item, nextCount);
                    if (stopRequested)
                    {
                        return;
                    }

                    nextCount = VerifyItem(item);
                }
            }
        }

        /// <summary>
        /// 对一条明细连续抓取指定次数。每次都是先抓药位，再投到分拣槽。
        /// </summary>
        private async Task GrabTimesAsync(PrescriptionItem item, int count)
        {
            Station grabStation = dispenseBll.GetGrabStation(item);
            Station dropStation = dispenseBll.GetDropStation();
            for (int i = 0; i < count; i++)
            {
                if (stopRequested)
                {
                    AppendLog("已急停，剩余抓取不再执行");
                    return;
                }

                ShowActiveStation(grabStation.StationId);
                int grabNo = item.GrabCount + 1;
                AppendLog("【" + item.DrugName + "】第 " + grabNo + " 次抓取 " + grabStation.GrabCommand);
                await SendArmAsync(grabStation.GrabCommand, grabStation.EstTimeMs);

                if (stopRequested)
                {
                    return;
                }

                ShowActiveStation(dropStation.StationId);
                AppendLog("【" + item.DrugName + "】第 " + grabNo + " 次投递 " + dropStation.DropCommand);
                await SendArmAsync(dropStation.DropCommand, dropStation.EstTimeMs);
                dispenseBll.RecordGrab(item, grabStation.GrabCommand, dropStation.DropCommand);
                BindItems();
            }

            ShowActiveStation(null);
        }

        /// <summary>
        /// 在后台发送并等待机械臂。界面线程只负责刷新日志和工位颜色。
        /// </summary>
        private Task SendArmAsync(string command, int waitMilliseconds)
        {
            string currentCommand = command;
            int currentWait = waitMilliseconds;
            return Task.Run(delegate
            {
                arm.Send(currentCommand, currentWait);
            });
        }

        /// <summary>
        /// 弹出数量核对。返回还要补抓的次数，取消、急停或已经通过时返回 0。
        /// </summary>
        private int VerifyItem(PrescriptionItem item)
        {
            using (FrmQuantityVerify dialog = new FrmQuantityVerify(item))
            {
                if (dialog.ShowDialog(FindForm()) != DialogResult.OK)
                {
                    AppendLog("数量核对已取消");
                    return 0;
                }

                // 核对弹窗是模态的，急停当时清不掉这里的循环标记，关闭后再补一次。
                if (stopRequested)
                {
                    AppendLog("急停后不再继续当前药品");
                    return 0;
                }

                int nextCount = dispenseBll.ApplyDecision(item, dialog.ActualQty, dialog.Decision);
                BindItems();
                if (nextCount <= 0)
                {
                    return 0;
                }

                if (dialog.Decision == VerifyDecision.Reset)
                {
                    AppendLog("【" + item.DrugName + "】已作废，重新抓取");
                }
                else
                {
                    AppendLog("【" + item.DrugName + "】补抓 " + nextCount + " 盒");
                }

                return nextCount;
            }
        }

        /// <summary>
        /// 读取当前处方和明细，并刷新摘要、进度和工位指示。
        /// </summary>
        /// <param name="skipPrescriptionId">跳过不能配药的处方，避免反复选中同一张。</param>
        private void LoadCurrent(int skipPrescriptionId)
        {
            try
            {
                currentPrescription = dispenseBll.GetCurrentPrescription(skipPrescriptionId);
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
        /// 读取当前处方和明细，并刷新摘要、进度和工位指示。
        /// </summary>
        private void LoadCurrent()
        {
            LoadCurrent(0);
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
                btnStart.Enabled = !dispensing;
                return;
            }

            lblSummary.Text = "处方号 " + currentPrescription.PrescriptionId + "    患者编号 " + currentPrescription.PatientNo;
            lblStatus.Text = currentPrescription.Status;
            lblStatus.ForeColor = GetStatusColor(currentPrescription.Status);
            btnStart.Enabled = !dispensing;
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
        private void DgvItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
        /// 明细表只展示进度，不允许选中，避免选中色盖住行颜色。
        /// </summary>
        private void DgvItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItems.CurrentCell != null || dgvItems.SelectedCells.Count > 0)
            {
                dgvItems.ClearSelection();
                dgvItems.CurrentCell = null;
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
