namespace PharmacySorter
{
    partial class UcDashboard
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblSummary = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.tlpBody = new System.Windows.Forms.TableLayoutPanel();
            this.pnlArm = new System.Windows.Forms.Panel();
            this.grpEntry = new System.Windows.Forms.GroupBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.dgvDraft = new System.Windows.Forms.DataGridView();
            this.colDraftDrugName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDraftSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDraftQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblDraft = new System.Windows.Forms.Label();
            this.cmbDrug = new System.Windows.Forms.ComboBox();
            this.lblDrugName = new System.Windows.Forms.Label();
            this.txtPatientNo = new System.Windows.Forms.TextBox();
            this.lblPatientNo = new System.Windows.Forms.Label();
            this.pnlItems = new System.Windows.Forms.Panel();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMoveTop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvQueue = new System.Windows.Forms.DataGridView();
            this.colQueueId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQueuePatientNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQueueItemCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQueueCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQueueStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.colDashDrugName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDashStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDashRequiredQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDashGrabCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDashStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblItems = new System.Windows.Forms.Label();
            this.prgItems = new System.Windows.Forms.ProgressBar();
            this.pnlSummary.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.tlpBody.SuspendLayout();
            this.pnlArm.SuspendLayout();
            this.grpEntry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDraft)).BeginInit();
            this.pnlItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSummary
            // 
            this.pnlSummary.Controls.Add(this.lblStatus);
            this.pnlSummary.Controls.Add(this.lblSummary);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSummary.Location = new System.Drawing.Point(0, 0);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(1285, 63);
            this.pnlSummary.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(980, 16);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(270, 32);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "空闲";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSummary
            // 
            this.lblSummary.Font = new System.Drawing.Font("Microsoft YaHei UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblSummary.Location = new System.Drawing.Point(13, 16);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(760, 36);
            this.lblSummary.TabIndex = 0;
            this.lblSummary.Text = "当前没有待配任务";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.txtLog);
            this.pnlBottom.Controls.Add(this.btnStart);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 644);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1285, 94);
            this.pnlBottom.TabIndex = 1;
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtLog.ForeColor = System.Drawing.Color.Lime;
            this.txtLog.Location = new System.Drawing.Point(180, 12);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(1070, 70);
            this.txtLog.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(16, 26);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(150, 42);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始配药";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // tlpBody
            // 
            this.tlpBody.ColumnCount = 2;
            this.tlpBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.50195F));
            this.tlpBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.49805F));
            this.tlpBody.Controls.Add(this.pnlArm, 1, 0);
            this.tlpBody.Controls.Add(this.pnlItems, 0, 0);
            this.tlpBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBody.Location = new System.Drawing.Point(0, 63);
            this.tlpBody.Name = "tlpBody";
            this.tlpBody.RowCount = 1;
            this.tlpBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBody.Size = new System.Drawing.Size(1285, 581);
            this.tlpBody.TabIndex = 2;
            // 
            // pnlArm
            // 
            this.pnlArm.Controls.Add(this.grpEntry);
            this.pnlArm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlArm.Location = new System.Drawing.Point(819, 3);
            this.pnlArm.Name = "pnlArm";
            this.pnlArm.Size = new System.Drawing.Size(463, 575);
            this.pnlArm.TabIndex = 1;
            // 
            // grpEntry
            // 
            this.grpEntry.Controls.Add(this.btnSubmit);
            this.grpEntry.Controls.Add(this.dgvDraft);
            this.grpEntry.Controls.Add(this.lblDraft);
            this.grpEntry.Controls.Add(this.cmbDrug);
            this.grpEntry.Controls.Add(this.lblDrugName);
            this.grpEntry.Controls.Add(this.txtPatientNo);
            this.grpEntry.Controls.Add(this.lblPatientNo);
            this.grpEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEntry.Location = new System.Drawing.Point(0, 0);
            this.grpEntry.Name = "grpEntry";
            this.grpEntry.Size = new System.Drawing.Size(463, 575);
            this.grpEntry.TabIndex = 1;
            this.grpEntry.TabStop = false;
            this.grpEntry.Text = "添加任务";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSubmit.Location = new System.Drawing.Point(168, 515);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(140, 35);
            this.btnSubmit.TabIndex = 13;
            this.btnSubmit.Text = "加入队列";
            this.btnSubmit.UseVisualStyleBackColor = true;
            // 
            // dgvDraft
            // 
            this.dgvDraft.AllowUserToAddRows = false;
            this.dgvDraft.AllowUserToDeleteRows = false;
            this.dgvDraft.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDraft.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDraft.ColumnHeadersHeight = 32;
            this.dgvDraft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDraft.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDraftDrugName,
            this.colDraftSpec,
            this.colDraftQuantity});
            this.dgvDraft.Location = new System.Drawing.Point(22, 233);
            this.dgvDraft.MultiSelect = false;
            this.dgvDraft.Name = "dgvDraft";
            this.dgvDraft.ReadOnly = true;
            this.dgvDraft.RowHeadersVisible = false;
            this.dgvDraft.RowHeadersWidth = 51;
            this.dgvDraft.RowTemplate.Height = 27;
            this.dgvDraft.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDraft.Size = new System.Drawing.Size(424, 248);
            this.dgvDraft.TabIndex = 11;
            // 
            // colDraftDrugName
            // 
            this.colDraftDrugName.DataPropertyName = "DrugName";
            this.colDraftDrugName.FillWeight = 180F;
            this.colDraftDrugName.HeaderText = "药品名称";
            this.colDraftDrugName.MinimumWidth = 80;
            this.colDraftDrugName.Name = "colDraftDrugName";
            this.colDraftDrugName.ReadOnly = true;
            // 
            // colDraftSpec
            // 
            this.colDraftSpec.DataPropertyName = "Spec";
            this.colDraftSpec.FillWeight = 140F;
            this.colDraftSpec.HeaderText = "规格";
            this.colDraftSpec.MinimumWidth = 60;
            this.colDraftSpec.Name = "colDraftSpec";
            this.colDraftSpec.ReadOnly = true;
            // 
            // colDraftQuantity
            // 
            this.colDraftQuantity.DataPropertyName = "RequiredQty";
            this.colDraftQuantity.FillWeight = 60F;
            this.colDraftQuantity.HeaderText = "数量";
            this.colDraftQuantity.MinimumWidth = 48;
            this.colDraftQuantity.Name = "colDraftQuantity";
            this.colDraftQuantity.ReadOnly = true;
            // 
            // lblDraft
            // 
            this.lblDraft.AutoSize = true;
            this.lblDraft.Location = new System.Drawing.Point(19, 215);
            this.lblDraft.Name = "lblDraft";
            this.lblDraft.Size = new System.Drawing.Size(97, 15);
            this.lblDraft.TabIndex = 10;
            this.lblDraft.Text = "处方药品明细";
            // 
            // cmbDrug
            // 
            this.cmbDrug.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDrug.FormattingEnabled = true;
            this.cmbDrug.Location = new System.Drawing.Point(128, 44);
            this.cmbDrug.Name = "cmbDrug";
            this.cmbDrug.Size = new System.Drawing.Size(257, 23);
            this.cmbDrug.TabIndex = 1;
            // 
            // lblDrugName
            // 
            this.lblDrugName.AutoSize = true;
            this.lblDrugName.Location = new System.Drawing.Point(28, 52);
            this.lblDrugName.Name = "lblDrugName";
            this.lblDrugName.Size = new System.Drawing.Size(67, 15);
            this.lblDrugName.TabIndex = 0;
            this.lblDrugName.Text = "处方模板";
            // 
            // txtPatientNo
            // 
            this.txtPatientNo.Location = new System.Drawing.Point(128, 102);
            this.txtPatientNo.MaxLength = 50;
            this.txtPatientNo.Name = "txtPatientNo";
            this.txtPatientNo.Size = new System.Drawing.Size(257, 25);
            this.txtPatientNo.TabIndex = 4;
            // 
            // lblPatientNo
            // 
            this.lblPatientNo.AutoSize = true;
            this.lblPatientNo.Location = new System.Drawing.Point(28, 112);
            this.lblPatientNo.Name = "lblPatientNo";
            this.lblPatientNo.Size = new System.Drawing.Size(67, 15);
            this.lblPatientNo.TabIndex = 3;
            this.lblPatientNo.Text = "患者编号";
            // 
            // pnlItems
            // 
            this.pnlItems.Controls.Add(this.prgItems);
            this.pnlItems.Controls.Add(this.btnHistory);
            this.pnlItems.Controls.Add(this.btnCancel);
            this.pnlItems.Controls.Add(this.btnMoveTop);
            this.pnlItems.Controls.Add(this.label1);
            this.pnlItems.Controls.Add(this.dgvQueue);
            this.pnlItems.Controls.Add(this.dgvItems);
            this.pnlItems.Controls.Add(this.lblItems);
            this.pnlItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlItems.Location = new System.Drawing.Point(3, 3);
            this.pnlItems.Name = "pnlItems";
            this.pnlItems.Size = new System.Drawing.Size(810, 575);
            this.pnlItems.TabIndex = 0;
            // 
            // btnHistory
            // 
            this.btnHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHistory.Location = new System.Drawing.Point(644, 515);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(140, 35);
            this.btnHistory.TabIndex = 6;
            this.btnHistory.Text = "历史任务";
            this.btnHistory.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(337, 515);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 35);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "撤销任务";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnMoveTop
            // 
            this.btnMoveTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMoveTop.Location = new System.Drawing.Point(43, 515);
            this.btnMoveTop.Name = "btnMoveTop";
            this.btnMoveTop.Size = new System.Drawing.Size(140, 35);
            this.btnMoveTop.TabIndex = 4;
            this.btnMoveTop.Text = "置顶";
            this.btnMoveTop.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "待配任务队列";
            // 
            // dgvQueue
            // 
            this.dgvQueue.AllowUserToAddRows = false;
            this.dgvQueue.AllowUserToDeleteRows = false;
            this.dgvQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvQueue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQueue.ColumnHeadersHeight = 32;
            this.dgvQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvQueue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colQueueId,
            this.colQueuePatientNo,
            this.colQueueItemCount,
            this.colQueueCreateTime,
            this.colQueueStatus});
            this.dgvQueue.Location = new System.Drawing.Point(16, 233);
            this.dgvQueue.MultiSelect = false;
            this.dgvQueue.Name = "dgvQueue";
            this.dgvQueue.ReadOnly = true;
            this.dgvQueue.RowHeadersVisible = false;
            this.dgvQueue.RowHeadersWidth = 51;
            this.dgvQueue.RowTemplate.Height = 27;
            this.dgvQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQueue.Size = new System.Drawing.Size(777, 248);
            this.dgvQueue.TabIndex = 2;
            // 
            // colQueueId
            // 
            this.colQueueId.DataPropertyName = "OrderId";
            this.colQueueId.FillWeight = 70F;
            this.colQueueId.HeaderText = "任务号";
            this.colQueueId.MinimumWidth = 56;
            this.colQueueId.Name = "colQueueId";
            this.colQueueId.ReadOnly = true;
            // 
            // colQueuePatientNo
            // 
            this.colQueuePatientNo.DataPropertyName = "PatientNo";
            this.colQueuePatientNo.FillWeight = 90F;
            this.colQueuePatientNo.HeaderText = "患者编号";
            this.colQueuePatientNo.MinimumWidth = 72;
            this.colQueuePatientNo.Name = "colQueuePatientNo";
            this.colQueuePatientNo.ReadOnly = true;
            // 
            // colQueueItemCount
            // 
            this.colQueueItemCount.DataPropertyName = "PrescriptionName";
            this.colQueueItemCount.FillWeight = 120F;
            this.colQueueItemCount.HeaderText = "处方名称";
            this.colQueueItemCount.MinimumWidth = 72;
            this.colQueueItemCount.Name = "colQueueItemCount";
            this.colQueueItemCount.ReadOnly = true;
            // 
            // colQueueCreateTime
            // 
            this.colQueueCreateTime.DataPropertyName = "ItemCount";
            this.colQueueCreateTime.HeaderText = "药品种类数";
            this.colQueueCreateTime.MinimumWidth = 80;
            this.colQueueCreateTime.Name = "colQueueCreateTime";
            this.colQueueCreateTime.ReadOnly = true;
            // 
            // colQueueStatus
            // 
            this.colQueueStatus.DataPropertyName = "CreateTimeText";
            this.colQueueStatus.FillWeight = 150F;
            this.colQueueStatus.HeaderText = "接收时间";
            this.colQueueStatus.MinimumWidth = 120;
            this.colQueueStatus.Name = "colQueueStatus";
            this.colQueueStatus.ReadOnly = true;
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.ColumnHeadersHeight = 32;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDashDrugName,
            this.colDashStation,
            this.colDashRequiredQty,
            this.colDashGrabCount,
            this.colDashStatus});
            this.dgvItems.Location = new System.Drawing.Point(16, 18);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.RowHeadersWidth = 51;
            this.dgvItems.RowTemplate.Height = 27;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvItems.Size = new System.Drawing.Size(777, 180);
            this.dgvItems.TabIndex = 1;
            // 
            // colDashDrugName
            // 
            this.colDashDrugName.DataPropertyName = "DrugName";
            this.colDashDrugName.HeaderText = "药品名称";
            this.colDashDrugName.MinimumWidth = 6;
            this.colDashDrugName.Name = "colDashDrugName";
            this.colDashDrugName.ReadOnly = true;
            // 
            // colDashStation
            // 
            this.colDashStation.DataPropertyName = "StationText";
            this.colDashStation.HeaderText = "取药工位";
            this.colDashStation.MinimumWidth = 6;
            this.colDashStation.Name = "colDashStation";
            this.colDashStation.ReadOnly = true;
            // 
            // colDashRequiredQty
            // 
            this.colDashRequiredQty.DataPropertyName = "RequiredQty";
            this.colDashRequiredQty.HeaderText = "应取数量";
            this.colDashRequiredQty.MinimumWidth = 6;
            this.colDashRequiredQty.Name = "colDashRequiredQty";
            this.colDashRequiredQty.ReadOnly = true;
            // 
            // colDashGrabCount
            // 
            this.colDashGrabCount.DataPropertyName = "GrabCount";
            this.colDashGrabCount.HeaderText = "已抓取次数";
            this.colDashGrabCount.MinimumWidth = 6;
            this.colDashGrabCount.Name = "colDashGrabCount";
            this.colDashGrabCount.ReadOnly = true;
            // 
            // colDashStatus
            // 
            this.colDashStatus.DataPropertyName = "Status";
            this.colDashStatus.HeaderText = "当前状态";
            this.colDashStatus.MinimumWidth = 6;
            this.colDashStatus.Name = "colDashStatus";
            this.colDashStatus.ReadOnly = true;
            // 
            // lblItems
            // 
            this.lblItems.AutoSize = true;
            this.lblItems.Location = new System.Drawing.Point(13, 0);
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(129, 15);
            this.lblItems.TabIndex = 0;
            this.lblItems.Text = "当前任务明细 0/0";
            // 
            // prgItems
            //
            this.prgItems.Location = new System.Drawing.Point(164, 0);
            this.prgItems.Name = "prgItems";
            this.prgItems.Size = new System.Drawing.Size(300, 15);
            this.prgItems.TabIndex = 7;
            // 
            // UcDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpBody);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.pnlBottom);
            this.Name = "UcDashboard";
            this.Size = new System.Drawing.Size(1285, 738);
            this.pnlSummary.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.tlpBody.ResumeLayout(false);
            this.pnlArm.ResumeLayout(false);
            this.grpEntry.ResumeLayout(false);
            this.grpEntry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDraft)).EndInit();
            this.pnlItems.ResumeLayout(false);
            this.pnlItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.TableLayoutPanel tlpBody;
        private System.Windows.Forms.Panel pnlItems;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.Panel pnlArm;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDashDrugName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDashStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDashRequiredQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDashGrabCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDashStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvQueue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQueueId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQueuePatientNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQueueItemCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQueueCreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQueueStatus;
        private System.Windows.Forms.Button btnMoveTop;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.GroupBox grpEntry;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.DataGridView dgvDraft;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDraftDrugName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDraftSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDraftQuantity;
        private System.Windows.Forms.Label lblDraft;
        private System.Windows.Forms.ComboBox cmbDrug;
        private System.Windows.Forms.Label lblDrugName;
        private System.Windows.Forms.TextBox txtPatientNo;
        private System.Windows.Forms.Label lblPatientNo;
        private System.Windows.Forms.ProgressBar prgItems;
    }
}
