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
            this.pnlItems = new System.Windows.Forms.Panel();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.colDashDrugName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDashStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDashRequiredQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDashGrabCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDashStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblItems = new System.Windows.Forms.Label();
            this.pnlArm = new System.Windows.Forms.Panel();
            this.lblArmHint = new System.Windows.Forms.Label();
            this.lblRightStation = new System.Windows.Forms.Label();
            this.lblSlot = new System.Windows.Forms.Label();
            this.lblLeftStation = new System.Windows.Forms.Label();
            this.pnlRightStation = new System.Windows.Forms.Panel();
            this.pnlSlot = new System.Windows.Forms.Panel();
            this.pnlLeftStation = new System.Windows.Forms.Panel();
            this.dgvQueue = new System.Windows.Forms.DataGridView();
            this.colQueueId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQueuePatientNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQueueItemCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQueueCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQueueStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlSummary.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.tlpBody.SuspendLayout();
            this.pnlItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.pnlArm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).BeginInit();
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
            this.lblSummary.Location = new System.Drawing.Point(24, 14);
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
            this.tlpBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.38132F));
            this.tlpBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.61868F));
            this.tlpBody.Controls.Add(this.pnlItems, 0, 0);
            this.tlpBody.Controls.Add(this.pnlArm, 1, 0);
            this.tlpBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBody.Location = new System.Drawing.Point(0, 63);
            this.tlpBody.Name = "tlpBody";
            this.tlpBody.RowCount = 1;
            this.tlpBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBody.Size = new System.Drawing.Size(1285, 581);
            this.tlpBody.TabIndex = 2;
            // 
            // pnlItems
            // 
            this.pnlItems.Controls.Add(this.label1);
            this.pnlItems.Controls.Add(this.dgvQueue);
            this.pnlItems.Controls.Add(this.dgvItems);
            this.pnlItems.Controls.Add(this.lblItems);
            this.pnlItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlItems.Location = new System.Drawing.Point(3, 3);
            this.pnlItems.Name = "pnlItems";
            this.pnlItems.Size = new System.Drawing.Size(846, 575);
            this.pnlItems.TabIndex = 0;
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
            this.dgvItems.Location = new System.Drawing.Point(12, 48);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.RowHeadersWidth = 51;
            this.dgvItems.RowTemplate.Height = 27;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvItems.Size = new System.Drawing.Size(813, 192);
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
            this.lblItems.Location = new System.Drawing.Point(17, 28);
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(129, 15);
            this.lblItems.TabIndex = 0;
            this.lblItems.Text = "当前任务明细 0/0";
            // 
            // pnlArm
            // 
            this.pnlArm.Controls.Add(this.lblArmHint);
            this.pnlArm.Controls.Add(this.lblRightStation);
            this.pnlArm.Controls.Add(this.lblSlot);
            this.pnlArm.Controls.Add(this.lblLeftStation);
            this.pnlArm.Controls.Add(this.pnlRightStation);
            this.pnlArm.Controls.Add(this.pnlSlot);
            this.pnlArm.Controls.Add(this.pnlLeftStation);
            this.pnlArm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlArm.Location = new System.Drawing.Point(855, 3);
            this.pnlArm.Name = "pnlArm";
            this.pnlArm.Size = new System.Drawing.Size(427, 575);
            this.pnlArm.TabIndex = 1;
            // 
            // lblArmHint
            // 
            this.lblArmHint.Location = new System.Drawing.Point(24, 335);
            this.lblArmHint.Name = "lblArmHint";
            this.lblArmHint.Size = new System.Drawing.Size(370, 40);
            this.lblArmHint.TabIndex = 6;
            this.lblArmHint.Text = "机械臂待命";
            // 
            // lblRightStation
            // 
            this.lblRightStation.Location = new System.Drawing.Point(302, 183);
            this.lblRightStation.Name = "lblRightStation";
            this.lblRightStation.Size = new System.Drawing.Size(74, 15);
            this.lblRightStation.TabIndex = 5;
            this.lblRightStation.Text = "右药位";
            // 
            // lblSlot
            // 
            this.lblSlot.Location = new System.Drawing.Point(164, 183);
            this.lblSlot.Name = "lblSlot";
            this.lblSlot.Size = new System.Drawing.Size(90, 15);
            this.lblSlot.TabIndex = 4;
            this.lblSlot.Text = "正前分拣槽";
            // 
            // lblLeftStation
            // 
            this.lblLeftStation.Location = new System.Drawing.Point(42, 183);
            this.lblLeftStation.Name = "lblLeftStation";
            this.lblLeftStation.Size = new System.Drawing.Size(74, 15);
            this.lblLeftStation.TabIndex = 3;
            this.lblLeftStation.Text = "左药位";
            // 
            // pnlRightStation
            // 
            this.pnlRightStation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlRightStation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRightStation.Location = new System.Drawing.Point(284, 215);
            this.pnlRightStation.Name = "pnlRightStation";
            this.pnlRightStation.Size = new System.Drawing.Size(110, 90);
            this.pnlRightStation.TabIndex = 2;
            // 
            // pnlSlot
            // 
            this.pnlSlot.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlSlot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSlot.Location = new System.Drawing.Point(154, 215);
            this.pnlSlot.Name = "pnlSlot";
            this.pnlSlot.Size = new System.Drawing.Size(110, 90);
            this.pnlSlot.TabIndex = 1;
            // 
            // pnlLeftStation
            // 
            this.pnlLeftStation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlLeftStation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLeftStation.Location = new System.Drawing.Point(24, 215);
            this.pnlLeftStation.Name = "pnlLeftStation";
            this.pnlLeftStation.Size = new System.Drawing.Size(110, 90);
            this.pnlLeftStation.TabIndex = 0;
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
            this.dgvQueue.Location = new System.Drawing.Point(12, 293);
            this.dgvQueue.MultiSelect = false;
            this.dgvQueue.Name = "dgvQueue";
            this.dgvQueue.ReadOnly = true;
            this.dgvQueue.RowHeadersVisible = false;
            this.dgvQueue.RowHeadersWidth = 51;
            this.dgvQueue.RowTemplate.Height = 27;
            this.dgvQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQueue.Size = new System.Drawing.Size(813, 248);
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
            this.colQueueCreateTime.FillWeight = 100F;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 275);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "待配任务队列";
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
            this.pnlItems.ResumeLayout(false);
            this.pnlItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.pnlArm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).EndInit();
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
        private System.Windows.Forms.Label lblLeftStation;
        private System.Windows.Forms.Panel pnlRightStation;
        private System.Windows.Forms.Panel pnlSlot;
        private System.Windows.Forms.Panel pnlLeftStation;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblArmHint;
        private System.Windows.Forms.Label lblRightStation;
        private System.Windows.Forms.Label lblSlot;
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
    }
}
