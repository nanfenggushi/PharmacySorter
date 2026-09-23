using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        /// 机械臂指令发送。没有串口时只按延时演示动作。
        /// </summary>
        private readonly ArmCommandService arm = new ArmCommandService();

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
        /// 检查通过后启动当前处方。抓取过程中按钮不可再次点击。
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

            try
            {
                btnStart.Enabled = false;
                stopRequested = false;
                dispenseBll.Start(currentPrescription);
                AppendLog("下发复位指令 " + ArmCommandService.ResetCommand);
                arm.Send(ArmCommandService.ResetCommand, 1000);
                BindSummary();
                RunPrescription();
            }
            catch (Exception ex)
            {
                AppendLog("配药中断：" + ex.Message);
                MessageBox.Show(ex.Message, "配药失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                ShowActiveStation(null);
                BindSummary();
                BindItems();
            }
        }

        /// <summary>
        /// 急停。当前循环会在下一次动作前停止，并下发复位指令。
        /// </summary>
        public void EmergencyStop()
        {
            stopRequested = true;
            try
            {
                arm.Send(ArmCommandService.ResetCommand, 0);
                AppendLog("急停，已下发复位指令 " + ArmCommandService.ResetCommand);
            }
            catch (Exception ex)
            {
                AppendLog("急停复位失败：" + ex.Message);
            }

            ShowActiveStation(null);
        }

        /// <summary>
        /// 逐条处理未完成的明细。每条抓满后必须人工核对。
        /// </summary>
        private void RunPrescription()
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
                    GrabTimes(item, count);
                }

                if (stopRequested)
                {
                    return;
                }

                dispenseBll.MarkWaitingCheck(item);
                BindItems();
                if (!VerifyItem(item))
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 对一条明细连续抓取指定次数。每次都是先抓药位，再投到分拣槽。
        /// </summary>
        private void GrabTimes(PrescriptionItem item, int count)
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
                AppendLog("下发抓取指令 " + grabStation.GrabCommand);
                arm.Send(grabStation.GrabCommand, grabStation.EstTimeMs);
                Application.DoEvents();

                ShowActiveStation(dropStation.StationId);
                AppendLog("下发投递指令 " + dropStation.DropCommand);
                arm.Send(dropStation.DropCommand, dropStation.EstTimeMs);
                dispenseBll.RecordGrab(item, grabStation.GrabCommand, dropStation.DropCommand);
                BindItems();
                Application.DoEvents();
            }

            ShowActiveStation(null);
        }

        /// <summary>
        /// 弹出数量核对。补抓和作废都会回到抓取流程，通过后才处理下一条。
        /// </summary>
        private bool VerifyItem(PrescriptionItem item)
        {
            using (FrmQuantityVerify dialog = new FrmQuantityVerify(item))
            {
                if (dialog.ShowDialog(FindForm()) != DialogResult.OK)
                {
                    AppendLog("数量核对已取消");
                    return false;
                }

                int nextCount = dispenseBll.ApplyDecision(item, dialog.ActualQty, dialog.Decision);
                BindItems();
                if (nextCount <= 0)
                {
                    return true;
                }

                if (dialog.Decision == VerifyDecision.Reset)
                {
                    AppendLog("【" + item.DrugName + "】已作废，重新抓取");
                }
                else
                {
                    AppendLog("【" + item.DrugName + "】补抓 " + nextCount + " 盒");
                }

                GrabTimes(item, nextCount);
                if (stopRequested)
                {
                    return false;
                }

                return VerifyItem(item);
            }
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
