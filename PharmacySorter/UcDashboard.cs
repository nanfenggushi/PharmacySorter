using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacySorter
{
    /// <summary>
    /// 配药工作台。展示当前处方、明细进度和待配任务队列，
    /// 并直接在录入区把固定处方加入队列、置顶、撤销和查询历史。
    /// 启动后按明细循环抓取，每次投放由落药检测自动确认。
    /// </summary>
    public partial class UcDashboard : UserControl
    {
        /// <summary>
        /// 工作台业务。界面不直接查询数据库。
        /// </summary>
        private readonly DispenseBLL dispenseBll = new DispenseBLL();

        /// <summary>
        /// 待配任务队列。加入队列、置顶、撤销和历史查询都在工作台操作。
        /// </summary>
        private readonly DispenseOrderBLL orderBll = new DispenseOrderBLL();

        /// <summary>
        /// 固定处方业务。录入区的处方下拉和明细预览使用。
        /// </summary>
        private readonly PrescriptionBLL prescriptionBll = new PrescriptionBLL();

        /// <summary>
        /// 机械臂指令发送。由主窗体传入，急停和抓取使用同一条串口。
        /// </summary>
        private ArmCommandService arm = new ArmCommandService();

        /// <summary>
        /// 落药检测。主窗体先打开通道并传入，状态显示和配药使用同一通道；未传入时按配置自建。
        /// </summary>
        private IDropDetector dropDetector = DropDetectorFactory.Create();

        /// <summary>
        /// 投放结束后等待落药信号的时间。
        /// </summary>
        private readonly int dropTimeoutMilliseconds = ReadPositiveSetting("DropTimeoutMs", 1500);

        /// <summary>
        /// 同一种药连续漏抓的上限。达到后停止，避免传感器故障时机械臂一直抓。
        /// </summary>
        private readonly int maxConsecutiveMisses = ReadPositiveSetting("DropMaxConsecutiveMisses", 3);

        /// <summary>
        /// 急停后置为 true，当前抓取循环会在下一次动作前停下来。
        /// </summary>
        private bool stopRequested;

        /// <summary>
        /// 急停、取消核对或暂停取药后，状态标签不能再显示“配药中”。
        /// </summary>
        private bool paused;

        /// <summary>
        /// 当前展示的待配任务。没有待处理任务时为空。
        /// </summary>
        private DispenseOrder currentOrder;

        /// <summary>
        /// 当前任务的明细，表格直接绑定这份列表。
        /// </summary>
        private List<DispenseOrderItem> currentItems = new List<DispenseOrderItem>();

        /// <summary>
        /// 正在自动配药。切回工作台时不能把进行中的处方刷新成空闲。
        /// </summary>
        private bool dispensing;

        /// <summary>
        /// 当前是否正在连续配药。主窗体用它判断复位按钮是否可用。
        /// </summary>
        public bool IsDispensing
        {
            get { return dispensing; }
        }

        /// <summary>
        /// 是否已经尝试过自动启动。只在程序启动并连上串口后启动一次。
        /// </summary>
        private bool autoStartAttempted;

        /// <summary>
        /// 本轮连续配药跳过的任务及原因。与上一轮相同的不重复写日志，避免同一行每秒刷屏。
        /// </summary>
        private readonly List<string> skipMessages = new List<string>();

        /// <summary>
        /// 上一轮跳过的任务及原因，用于比较这一轮跳过的内容是否发生了变化。
        /// </summary>
        private List<string> lastSkipMessages = new List<string>();

        public UcDashboard()
        {
            InitializeComponent();
            dgvItems.AutoGenerateColumns = false;
            dgvQueue.AutoGenerateColumns = false;
            dgvDraft.AutoGenerateColumns = false;
            dgvItems.CellFormatting += DgvItems_CellFormatting;
            dgvItems.SelectionChanged += DgvItems_SelectionChanged;
            VisibleChanged += UcDashboard_VisibleChanged;
            Load += UcDashboard_Load;
            cmbDrug.SelectedIndexChanged += CmbDrug_SelectedIndexChanged;
            btnSubmit.Click += BtnSubmit_Click;
            btnMoveTop.Click += BtnMoveTop_Click;
            btnCancel.Click += BtnCancel_Click;
            btnHistory.Click += BtnHistory_Click;
            btnAbandon.Click += BtnAbandon_Click;
        }

        /// <summary>
        /// 串口连接完成后由主窗体调用。只自动启动一次连续配药。
        /// </summary>
        public void StartWhenConnected()
        {
            if (autoStartAttempted || dispensing || !arm.IsConnected)
            {
                return;
            }

            autoStartAttempted = true;
            BtnStart_Click(this, EventArgs.Empty);
        }

        /// <summary>
        /// 绑定主窗体已经打开的串口。急停和抓取必须使用这一条连接。
        /// </summary>
        public void BindArm(ArmCommandService commandService)
        {
            if (commandService != null)
            {
                arm = commandService;
            }
        }

        /// <summary>
        /// 绑定主窗体已经打开的落药检测通道。状态显示和配药检测必须使用同一通道。
        /// </summary>
        public void BindDropDetector(IDropDetector detector)
        {
            if (detector != null)
            {
                dropDetector = detector;
            }
        }

        /// <summary>
        /// 请求停止当前抓取循环。复位指令由主窗体统一下发。
        /// </summary>
        public void RequestStop()
        {
            stopRequested = true;
            paused = true;
            AppendLog("急停，当前抓取循环已中断");
            ShowPausedStatus();
        }

        /// <summary>
        /// 每次重新显示工作台都读取最新队列。配药过程中不打断当前处方。
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
            paused = false;
            dispensing = true;
            arm.ClearStop();
            lastSkipMessages.Clear();
            try
            {
                if (!arm.IsConnected)
                {
                    paused = true;
                    throw new InvalidOperationException("机械臂串口未连接，不能启动配药");
                }

                AppendLog("下发待命指令 " + ArmCommandService.StandbyCommand);
                await Task.Run(new Action(arm.SendStandby));
                while (!stopRequested)
                {
                    // 判断当前是否有任务可以进行
                    if (!BeginCurrentOrder())
                    {
                        // 队列空了，或剩下的任务全都配不了。先把队首任务和原因显示出来，再放慢轮询。
                        LogSkipsOnce();
                        LoadCurrent();
                        bool blocked = skipMessages.Count > 0;
                        lblStatus.Text = blocked ? "任务被阻塞" : "等待新任务";
                        lblStatus.ForeColor = blocked ? Color.Firebrick : Color.Gray;
                        // 有配不了的任务时每 5 秒重试一次，避免每秒重查数据库并刷日志。
                        // 按 1 秒切片等待，急停后不用等满 5 秒就能退出循环。
                        int waitSeconds = blocked ? 5 : 1;
                        for (int i = 0; i < waitSeconds && !stopRequested; i++)
                        {
                            await Task.Delay(1000);
                        }
                        continue;
                    }
                    await RunOrderAsync();
                    if (stopRequested)
                    {
                        break;
                    }

                    LoadCurrent();
                }
            }
            catch (Exception ex)
            {
                paused = true;
                AppendLog("配药中断：" + ex.Message);
                MessageBox.Show(ex.Message, "配药失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                dispensing = false;
                LoadCurrent();
            }
        }

        /// <summary>
        /// 开始或继续当前任务。不能配药时跳过并读取下一条，队列空了返回 false。
        /// </summary>
        private bool BeginCurrentOrder()
        {
            skipMessages.Clear();
            while (!stopRequested)
            {
                if (currentOrder == null)
                {
                    return false;
                }

                // 当前任务状态是这两种的话说明需要恢复
                bool resume = currentOrder.Status == "配药中" || currentOrder.Status == "部分异常";
                // 当前任务状态"配药中"、"部分异常"、"待配药"这三种都不是说明不能继续任务
                if (!resume && currentOrder.Status != "待配药")
                {
                    return false;
                }

                // 检查任务是否可以进行
                string reason = dispenseBll.GetStartBlockReason(currentOrder, currentItems);
                if (!string.IsNullOrEmpty(reason))
                {
                    RememberSkip("跳过任务 " + currentOrder.OrderId + "：" + reason);
                    LoadCurrent(currentOrder.OrderId);
                    continue;
                }

                // 将当前任务设置为"配药中"
                dispenseBll.Start(currentOrder);
                BindSummary();
                AppendLog((resume ? "继续任务 " : "开始任务 ") + currentOrder.OrderId);
                // 找到能配的任务前跳过的任务也要让操作员知道，并且之后同样的阻塞要重新提示。
                lastSkipMessages.Clear();
                LogSkipsOnce();
                return true;
            }

            return false;
        }

        /// <summary>
        /// 记录一条跳过信息。同一轮里重复出现只记一次，统一在轮末输出。
        /// </summary>
        private void RememberSkip(string message)
        {
            if (!skipMessages.Contains(message))
            {
                skipMessages.Add(message);
            }
        }

        /// <summary>
        /// 输出本轮跳过的任务。与上一轮完全相同时不重复写，避免日志被同一行刷屏。
        /// </summary>
        private void LogSkipsOnce()
        {
            if (skipMessages.Count == 0 || SameMessages(skipMessages, lastSkipMessages))
            {
                return;
            }

            foreach (string message in skipMessages)
            {
                AppendLog(message);
            }
            lastSkipMessages = new List<string>(skipMessages);
        }

        /// <summary>
        /// 比较两份跳过信息是否完全相同。
        /// </summary>
        private static bool SameMessages(IList<string> left, IList<string> right)
        {
            if (left.Count != right.Count)
            {
                return false;
            }

            for (int i = 0; i < left.Count; i++)
            {
                if (!string.Equals(left[i], right[i], StringComparison.Ordinal))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 作废当前任务。配不下去的任务（缺药、传感器故障）作废后让出队首，不再阻塞队列。
        /// 剩余明细保留当前进度，历史查询仍能看到这条记录。
        /// </summary>
        private void BtnAbandon_Click(object sender, EventArgs e)
        {
            if (currentOrder == null)
            {
                MessageBox.Show("当前没有任务可作废", "作废任务", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "确定作废任务 " + currentOrder.OrderId + "（患者 " + currentOrder.PatientNo + "，处方 " + currentOrder.PrescriptionName + "）吗？\r\n作废后不再配药，剩余明细保留当前进度。",
                "作废确认",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                orderBll.Abandon(currentOrder.OrderId);
                AppendLog("已作废任务 " + currentOrder.OrderId);
                LoadCurrent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "作废失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 逐条处理未完成的明细。每次投放后由落药检测确认，不再人工核对。
        /// </summary>
        private async Task RunOrderAsync()
        {
            while (!stopRequested)
            {
                // 获取下一条还没有核对通过的明细
                DispenseOrderItem item = dispenseBll.FindNextItem(currentItems);
                if (item == null)
                {
                    // 所有明细都通过，将当前任务状态改成已完成
                    if (dispenseBll.TryComplete(currentOrder))
                    {
                        AppendLog("任务 " + currentOrder.OrderId + " 已完成");
                        // 将药品取走后机械臂才可以继续
                        if (!ConfirmSlotCleared(currentOrder))
                        {
                            stopRequested = true;
                            paused = true;
                        }
                    }
                    return;
                }

                // 获取当前明细需要抓取的次数
                int count = Math.Max(item.RequiredQty - item.ActualQty, 0);
                if (count > 0)
                {
                    await GrabTimesAsync(item, count);
                }

                if (stopRequested || item.Status == "异常")
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 一条任务核对通过后，分拣槽里的药品必须先取走，机械臂才能继续下一条。
        /// </summary>
        private bool ConfirmSlotCleared(DispenseOrder order)
        {
            if (order == null)
            {
                return false;
            }

            DialogResult result = MessageBox.Show(
                "任务 " + order.OrderId + "（患者 " + order.PatientNo + "，处方 " + order.PrescriptionName + "）已核对通过。\r\n请先取走分拣槽内的全部药品，取走后再继续配药。",
                "请取走药品",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information);
            if (result != DialogResult.OK)
            {
                AppendLog("药品尚未取走，连续配药已暂停");
                return false;
            }

            AppendLog("分拣槽药品已取走，可以继续下一条任务");
            return true;
        }

        /// <summary>
        /// 对一条明细连续抓取，直到实收数量达到应发数量。每次投放后立刻检测落药。
        /// </summary>
        private async Task GrabTimesAsync(DispenseOrderItem item, int count)
        {
            Station grabStation = dispenseBll.GetGrabStation(item);
            Station dropStation = dispenseBll.GetDropStation();
            int consecutiveMisses = 0;
            int remaining = count;
            while (remaining > 0)
            {
                if (stopRequested)
                {
                    AppendLog("已急停，剩余抓取不再执行");
                    return;
                }

                int grabNo = item.GrabCount + 1;
                AppendLog("【" + item.DrugName + "】第 " + grabNo + " 次抓取（" + grabStation.StationName + "）" + grabStation.GrabCommand);
                await SendArmAsync(grabStation.GrabCommand, grabStation.EstTimeMs);

                if (stopRequested)
                {
                    return;
                }

                AppendLog("【" + item.DrugName + "】第 " + grabNo + " 次投递（" + dropStation.StationName + "）" + dropStation.DropCommand);
                await SendDropAndDetectAsync(dropStation.DropCommand, dropStation.EstTimeMs);
                if (stopRequested)
                {
                    return;
                }

                bool dropped = await WaitDropAsync();
                dispenseBll.RecordGrab(item, grabStation.GrabCommand, dropStation.DropCommand, dropped);
                BindItems();
                if (dropped)
                {
                    consecutiveMisses = 0;
                    remaining--;
                    AppendLog("【" + item.DrugName + "】" + dropDetector.SourceName + "确认落药，实收 " + item.ActualQty);
                }
                else
                {
                    consecutiveMisses++;
                    AppendLog("【" + item.DrugName + "】未检测到落药，连续漏抓 " + consecutiveMisses + " 次");
                    if (consecutiveMisses >= maxConsecutiveMisses)
                    {
                        dispenseBll.MarkSensorFault(currentOrder, item, consecutiveMisses);
                        BindItems();
                        throw new InvalidOperationException("【" + item.DrugName + "】连续 " + consecutiveMisses + " 次未检测到落药，已停止配药");
                    }
                }
            }
        }

        /// <summary>
        /// 投放前清空旧信号，投放动作结束后再开始计时。
        /// </summary>
        private Task SendDropAndDetectAsync(string command, int waitMilliseconds)
        {
            string currentCommand = command;
            int currentWait = waitMilliseconds;
            IDropDetector detector = dropDetector;
            return Task.Run(delegate
            {
                detector.Open();
                detector.Arm();
                arm.Send(currentCommand, currentWait);
            });
        }

        /// <summary>
        /// 在后台等待落药信号，避免检测时间窗卡住界面。
        /// </summary>
        private Task<bool> WaitDropAsync()
        {
            IDropDetector detector = dropDetector;
            int timeout = dropTimeoutMilliseconds;
            return Task.Run(delegate
            {
                return detector.WaitForDrop(timeout);
            });
        }

        /// <summary>
        /// 读取正整数配置。配置缺失或不是正整数时使用默认值。
        /// </summary>
        private static int ReadPositiveSetting(string key, int defaultValue)
        {
            int value;
            if (int.TryParse(ConfigurationManager.AppSettings[key], out value) && value > 0)
            {
                return value;
            }

            return defaultValue;
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
        /// 读取当前任务和明细，并刷新摘要、进度和工位指示。
        /// </summary>
        /// <param name="skipOrderId">跳过不能配药的任务，传入0表示正常的加载队首任务，传入正常任务号表示跳过这个任务</param>
        private void LoadCurrent(int skipOrderId)
        {
            try
            {
                currentOrder = dispenseBll.GetCurrentOrder(skipOrderId);
                currentItems = currentOrder == null
                    ? new List<DispenseOrderItem>()
                    : dispenseBll.GetCurrentItems(currentOrder.OrderId);
            }
            catch (Exception ex)
            {
                currentOrder = null;
                currentItems = new List<DispenseOrderItem>();
                AppendLog("读取工作台数据失败：" + ex.Message);
            }

            BindSummary();
            BindItems();
            BindQueue();
        }

        /// <summary>
        /// 读取当前处方和明细，并刷新摘要、进度和工位指示。
        /// </summary>
        private void LoadCurrent()
        {
            LoadCurrent(0);
        }

        /// <summary>
        /// 顶部显示任务号、患者编号、处方名称和整体状态。没有任务时显示空闲。
        /// </summary>
        private void BindSummary()
        {
            if (currentOrder == null)
            {
                lblSummary.Text = "当前没有待配任务";
                lblStatus.Text = "空闲";
                lblStatus.ForeColor = Color.Gray;
                lblBlockReason.Text = string.Empty;
                btnStart.Enabled = !dispensing;
                btnAbandon.Enabled = false;
                return;
            }

            lblSummary.Text = "任务号 " + currentOrder.OrderId
                + "    患者编号 " + currentOrder.PatientNo
                + "    处方 " + currentOrder.PrescriptionName;
            if (paused && !dispensing)
            {
                ShowPausedStatus();
            }
            else if (!dispensing && (currentOrder.Status == "配药中" || currentOrder.Status == "部分异常"))
            {
                // 程序退出后数据库仍是配药中，但机械臂已经停止。重新打开时显示为已暂停。
                ShowPausedStatus();
            }
            else
            {
                lblStatus.Text = currentOrder.Status;
                lblStatus.ForeColor = GetStatusColor(currentOrder.Status);
            }
            btnStart.Enabled = !dispensing;
            btnAbandon.Enabled = !dispensing;
            UpdateBlockReason();
        }

        /// <summary>
        /// 不在配药时，把当前任务不能开始配药的原因直接显示在摘要下方，操作员不用去翻日志。
        /// </summary>
        private void UpdateBlockReason()
        {
            if (dispensing || currentOrder == null)
            {
                lblBlockReason.Text = string.Empty;
                return;
            }

            try
            {
                lblBlockReason.Text = dispenseBll.GetStartBlockReason(currentOrder, currentItems);
            }
            catch (Exception)
            {
                lblBlockReason.Text = string.Empty;
            }
        }

        /// <summary>
        /// 中断后的界面状态。数据库仍是“配药中”，但机械臂已经停止。
        /// </summary>
        private void ShowPausedStatus()
        {
            lblStatus.Text = "已暂停";
            lblStatus.ForeColor = Color.Firebrick;
        }

        /// <summary>
        /// 绑定明细表，并在标题上显示已核对条数和总条数，进度条同步该比例。
        /// </summary>
        private void BindItems()
        {
            int passed = dispenseBll.CountPassed(currentItems);
            lblItems.Text = "当前任务明细 " + passed + "/" + currentItems.Count;
            prgItems.Maximum = Math.Max(currentItems.Count, 1);
            prgItems.Value = Math.Min(passed, prgItems.Maximum);
            dgvItems.DataSource = null;
            dgvItems.DataSource = currentItems;
        }

        /// <summary>
        /// 刷新下方待配任务队列。正在配药的任务已经离开待配状态，所以不会出现在这里。
        /// </summary>
        private void BindQueue()
        {
            try
            {
                dgvQueue.DataSource = null;
                dgvQueue.DataSource = orderBll.GetWaitingQueue();
            }
            catch (Exception ex)
            {
                AppendLog("读取待配任务队列失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 首次加载时绑定固定处方下拉，并预览第一张处方的明细。
        /// </summary>
        private void UcDashboard_Load(object sender, EventArgs e)
        {
            BindPrescriptions();
        }

        /// <summary>
        /// 录入区的处方下拉只列已保存的固定处方。
        /// </summary>
        private void BindPrescriptions()
        {
            try
            {
                cmbDrug.DisplayMember = "PrescriptionName";
                cmbDrug.ValueMember = "PrescriptionId";
                cmbDrug.DataSource = prescriptionBll.GetAll();
                ShowPrescriptionItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "读取处方失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 切换处方模板时刷新下方的明细预览。
        /// </summary>
        private void CmbDrug_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPrescriptionItems();
        }

        /// <summary>
        /// 预览选中固定处方的药品明细，加入队列时按这份明细复制。
        /// </summary>
        private void ShowPrescriptionItems()
        {
            Prescription prescription = cmbDrug.SelectedItem as Prescription;
            dgvDraft.DataSource = null;
            if (prescription == null)
            {
                return;
            }

            try
            {
                dgvDraft.DataSource = prescriptionBll.GetItems(prescription.PrescriptionId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "读取处方明细失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 按选中的固定处方生成一条待配任务。配药进行中只刷新队列，不打断当前任务。
        /// </summary>
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            Prescription prescription = cmbDrug.SelectedItem as Prescription;
            if (prescription == null)
            {
                MessageBox.Show("请选择固定处方", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int orderId = orderBll.AddToQueue(txtPatientNo.Text, prescription.PrescriptionId);
                txtPatientNo.Text = string.Empty;
                if (dispensing)
                {
                    BindQueue();
                }
                else
                {
                    LoadCurrent();
                }
                SelectOrder(orderId);
                AppendLog("已加入待配队列，任务号：" + orderId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "加入失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 把选中的待配任务移到队首。
        /// </summary>
        private void BtnMoveTop_Click(object sender, EventArgs e)
        {
            DispenseOrder order = SelectedQueueOrder();
            if (order == null)
            {
                MessageBox.Show("请先选择一条待配任务", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                orderBll.MoveToTop(order.OrderId);
                BindQueue();
                SelectOrder(order.OrderId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "置顶失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 撤销选中的待配任务。只有待配药状态的任务可以撤销。
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DispenseOrder order = SelectedQueueOrder();
            if (order == null)
            {
                MessageBox.Show("请先选择一条待配任务", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "确定撤销任务 " + order.OrderId + " 吗？撤销后不再配药。",
                "撤销确认",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                orderBll.Cancel(order.OrderId);
                if (dispensing)
                {
                    BindQueue();
                }
                else
                {
                    LoadCurrent();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "撤销失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 打开历史任务查询窗口。历史任务再次配药后刷新队列。
        /// </summary>
        private void BtnHistory_Click(object sender, EventArgs e)
        {
            using (FrmPrescriptionHistory dialog = new FrmPrescriptionHistory())
            {
                if (dialog.ShowDialog(FindForm()) == DialogResult.OK)
                {
                    if (dispensing)
                    {
                        BindQueue();
                    }
                    else
                    {
                        LoadCurrent();
                    }
                }
            }
        }

        /// <summary>
        /// 待配队列表格当前选中的任务。
        /// </summary>
        private DispenseOrder SelectedQueueOrder()
        {
            if (dgvQueue.CurrentRow == null)
            {
                return null;
            }
            return dgvQueue.CurrentRow.DataBoundItem as DispenseOrder;
        }

        /// <summary>
        /// 加入队列或置顶后，让该任务在队列表格中保持选中。
        /// </summary>
        private void SelectOrder(int orderId)
        {
            foreach (DataGridViewRow row in dgvQueue.Rows)
            {
                DispenseOrder order = row.DataBoundItem as DispenseOrder;
                if (order != null && order.OrderId == orderId)
                {
                    row.Selected = true;
                    dgvQueue.CurrentCell = row.Cells[0];
                    return;
                }
            }
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

            DispenseOrderItem item = dgvItems.Rows[e.RowIndex].DataBoundItem as DispenseOrderItem;
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
