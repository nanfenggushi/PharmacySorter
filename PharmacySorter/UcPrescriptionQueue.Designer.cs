namespace PharmacySorter
{
    partial class UcPrescriptionQueue
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlEntry = new System.Windows.Forms.Panel();
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
            this.lblPrescriptionId = new System.Windows.Forms.Label();
            this.pnlQueue = new System.Windows.Forms.Panel();
            this.grpQueue = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMoveTop = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            this.dgvQueue = new System.Windows.Forms.DataGridView();
            this.colQueueId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQueuePatientNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQueueItemCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQueueCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQueueStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.lblAddDrug = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.pnlEntry.SuspendLayout();
            this.grpEntry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDraft)).BeginInit();
            this.pnlQueue.SuspendLayout();
            this.grpQueue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.pnlEntry, 0, 0);
            this.tlpMain.Controls.Add(this.pnlQueue, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1208, 722);
            this.tlpMain.TabIndex = 0;
            // 
            // pnlEntry
            // 
            this.pnlEntry.Controls.Add(this.grpEntry);
            this.pnlEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEntry.Location = new System.Drawing.Point(3, 3);
            this.pnlEntry.Name = "pnlEntry";
            this.pnlEntry.Size = new System.Drawing.Size(598, 716);
            this.pnlEntry.TabIndex = 0;
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
            this.grpEntry.Controls.Add(this.lblPrescriptionId);
            this.grpEntry.Location = new System.Drawing.Point(17, 15);
            this.grpEntry.Name = "grpEntry";
            this.grpEntry.Size = new System.Drawing.Size(562, 678);
            this.grpEntry.TabIndex = 0;
            this.grpEntry.TabStop = false;
            this.grpEntry.Text = "加入待配队列";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(204, 611);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(140, 35);
            this.btnSubmit.TabIndex = 13;
            this.btnSubmit.Text = "加入队列";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // dgvDraft
            // 
            this.dgvDraft.AllowUserToAddRows = false;
            this.dgvDraft.AllowUserToDeleteRows = false;
            this.dgvDraft.AutoGenerateColumns = false;
            this.dgvDraft.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDraft.ColumnHeadersHeight = 32;
            this.dgvDraft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDraft.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDraftDrugName,
            this.colDraftSpec,
            this.colDraftQuantity});
            this.dgvDraft.Location = new System.Drawing.Point(31, 305);
            this.dgvDraft.MultiSelect = false;
            this.dgvDraft.Name = "dgvDraft";
            this.dgvDraft.ReadOnly = true;
            this.dgvDraft.RowHeadersVisible = false;
            this.dgvDraft.RowHeadersWidth = 51;
            this.dgvDraft.RowTemplate.Height = 27;
            this.dgvDraft.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDraft.Size = new System.Drawing.Size(496, 250);
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
            this.lblDraft.Location = new System.Drawing.Point(28, 275);
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
            this.cmbDrug.SelectedIndexChanged += new System.EventHandler(this.CmbPrescription_SelectedIndexChanged);
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
            // lblPrescriptionId
            // 
            this.lblPrescriptionId.AutoSize = true;
            this.lblPrescriptionId.Location = new System.Drawing.Point(28, 52);
            this.lblPrescriptionId.Name = "lblPrescriptionId";
            this.lblPrescriptionId.Size = new System.Drawing.Size(67, 15);
            this.lblPrescriptionId.TabIndex = 0;
            this.lblPrescriptionId.Text = "处方编号";
            // 
            // pnlQueue
            // 
            this.pnlQueue.Controls.Add(this.grpQueue);
            this.pnlQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlQueue.Location = new System.Drawing.Point(607, 3);
            this.pnlQueue.Name = "pnlQueue";
            this.pnlQueue.Size = new System.Drawing.Size(598, 716);
            this.pnlQueue.TabIndex = 1;
            // 
            // grpQueue
            // 
            this.grpQueue.Controls.Add(this.btnCancel);
            this.grpQueue.Controls.Add(this.btnMoveTop);
            this.grpQueue.Controls.Add(this.btnHistory);
            this.grpQueue.Controls.Add(this.dgvQueue);
            this.grpQueue.Location = new System.Drawing.Point(18, 19);
            this.grpQueue.Name = "grpQueue";
            this.grpQueue.Size = new System.Drawing.Size(562, 678);
            this.grpQueue.TabIndex = 1;
            this.grpQueue.TabStop = false;
            this.grpQueue.Text = "待配任务队列";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(215, 607);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 35);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "撤销任务";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnMoveTop
            // 
            this.btnMoveTop.Location = new System.Drawing.Point(21, 607);
            this.btnMoveTop.Name = "btnMoveTop";
            this.btnMoveTop.Size = new System.Drawing.Size(140, 35);
            this.btnMoveTop.TabIndex = 1;
            this.btnMoveTop.Text = "置顶";
            this.btnMoveTop.UseVisualStyleBackColor = true;
            this.btnMoveTop.Click += new System.EventHandler(this.BtnMoveTop_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(400, 607);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(140, 35);
            this.btnHistory.TabIndex = 3;
            this.btnHistory.Text = "历史任务";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.BtnHistory_Click);
            // 
            // dgvQueue
            // 
            this.dgvQueue.AllowUserToAddRows = false;
            this.dgvQueue.AllowUserToDeleteRows = false;
            this.dgvQueue.AutoGenerateColumns = false;
            this.dgvQueue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQueue.ColumnHeadersHeight = 32;
            this.dgvQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvQueue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colQueueId,
            this.colQueuePatientNo,
            this.colQueueItemCount,
            this.colQueueCreateTime,
            this.colQueueStatus});
            this.dgvQueue.Location = new System.Drawing.Point(21, 38);
            this.dgvQueue.MultiSelect = false;
            this.dgvQueue.Name = "dgvQueue";
            this.dgvQueue.ReadOnly = true;
            this.dgvQueue.RowHeadersVisible = false;
            this.dgvQueue.RowHeadersWidth = 51;
            this.dgvQueue.RowTemplate.Height = 27;
            this.dgvQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQueue.Size = new System.Drawing.Size(519, 540);
            this.dgvQueue.TabIndex = 0;
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
            // btnRemoveItem
            // 
            this.btnRemoveItem.Location = new System.Drawing.Point(31, 575);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(110, 35);
            this.btnRemoveItem.TabIndex = 14;
            this.btnRemoveItem.Text = "移除选中";
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            this.btnRemoveItem.Click += new System.EventHandler(this.BtnRemoveItem_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(404, 190);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(110, 35);
            this.btnAddItem.TabIndex = 12;
            this.btnAddItem.Text = "加入清单";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.BtnAddItem_Click);
            // 
            // nudQuantity
            // 
            this.nudQuantity.Location = new System.Drawing.Point(217, 201);
            this.nudQuantity.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nudQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(168, 25);
            this.nudQuantity.TabIndex = 9;
            this.nudQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(128, 206);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(82, 15);
            this.lblQuantity.TabIndex = 8;
            this.lblQuantity.Text = "药品数量：";
            // 
            // lblAddDrug
            // 
            this.lblAddDrug.AutoSize = true;
            this.lblAddDrug.Location = new System.Drawing.Point(28, 166);
            this.lblAddDrug.Name = "lblAddDrug";
            this.lblAddDrug.Size = new System.Drawing.Size(82, 15);
            this.lblAddDrug.TabIndex = 5;
            this.lblAddDrug.Text = "药品添加：";
            // 
            // UcPrescriptionQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "UcPrescriptionQueue";
            this.Size = new System.Drawing.Size(1208, 722);
            this.tlpMain.ResumeLayout(false);
            this.pnlEntry.ResumeLayout(false);
            this.grpEntry.ResumeLayout(false);
            this.grpEntry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDraft)).EndInit();
            this.pnlQueue.ResumeLayout(false);
            this.grpQueue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlEntry;
        private System.Windows.Forms.GroupBox grpEntry;
        private System.Windows.Forms.Panel pnlQueue;
        private System.Windows.Forms.GroupBox grpQueue;
        private System.Windows.Forms.Label lblPrescriptionId;
        private System.Windows.Forms.TextBox txtPatientNo;
        private System.Windows.Forms.Label lblPatientNo;
        private System.Windows.Forms.Label lblAddDrug;
        private System.Windows.Forms.Label lblDrugName;
        private System.Windows.Forms.ComboBox cmbDrug;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Label lblDraft;
        private System.Windows.Forms.DataGridView dgvDraft;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.DataGridView dgvQueue;
        private System.Windows.Forms.Button btnMoveTop;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDraftDrugName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDraftSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDraftQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQueueId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQueuePatientNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQueueItemCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQueueCreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQueueStatus;
    }
}
