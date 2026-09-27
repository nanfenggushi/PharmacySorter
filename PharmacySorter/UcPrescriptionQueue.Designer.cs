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
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.dgvDraft = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblDraft = new System.Windows.Forms.Label();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.cmbDrug = new System.Windows.Forms.ComboBox();
            this.lblDrugName = new System.Windows.Forms.Label();
            this.lblAddDrug = new System.Windows.Forms.Label();
            this.txtPatientNo = new System.Windows.Forms.TextBox();
            this.lblPatientNo = new System.Windows.Forms.Label();
            this.txtPrescriptionId = new System.Windows.Forms.TextBox();
            this.lblPrescriptionId = new System.Windows.Forms.Label();
            this.pnlQueue = new System.Windows.Forms.Panel();
            this.grpQueue = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMoveTop = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            this.dgvQueue = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlpMain.SuspendLayout();
            this.pnlEntry.SuspendLayout();
            this.grpEntry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDraft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.pnlQueue.SuspendLayout();
            this.grpQueue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).BeginInit();
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
            this.grpEntry.Controls.Add(this.btnRemoveItem);
            this.grpEntry.Controls.Add(this.btnAddItem);
            this.grpEntry.Controls.Add(this.dgvDraft);
            this.grpEntry.Controls.Add(this.lblDraft);
            this.grpEntry.Controls.Add(this.nudQuantity);
            this.grpEntry.Controls.Add(this.lblQuantity);
            this.grpEntry.Controls.Add(this.cmbDrug);
            this.grpEntry.Controls.Add(this.lblDrugName);
            this.grpEntry.Controls.Add(this.lblAddDrug);
            this.grpEntry.Controls.Add(this.txtPatientNo);
            this.grpEntry.Controls.Add(this.lblPatientNo);
            this.grpEntry.Controls.Add(this.txtPrescriptionId);
            this.grpEntry.Controls.Add(this.lblPrescriptionId);
            this.grpEntry.Location = new System.Drawing.Point(17, 15);
            this.grpEntry.Name = "grpEntry";
            this.grpEntry.Size = new System.Drawing.Size(562, 678);
            this.grpEntry.TabIndex = 0;
            this.grpEntry.TabStop = false;
            this.grpEntry.Text = "处方录入表单";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(204, 611);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(140, 35);
            this.btnSubmit.TabIndex = 13;
            this.btnSubmit.Text = "正式提交处方";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
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
            // dgvDraft
            // 
            this.dgvDraft.AllowUserToAddRows = false;
            this.dgvDraft.AllowUserToDeleteRows = false;
            this.dgvDraft.AutoGenerateColumns = false;
            this.dgvDraft.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDraft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDraft.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
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
            // Column1
            // 
            this.Column1.DataPropertyName = "DrugName";
            this.Column1.HeaderText = "药品名称";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "colDraftDrugName";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Spec";
            this.Column2.HeaderText = "规格";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "colDraftSpec";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "RequiredQty";
            this.Column3.HeaderText = "数量";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "colDraftQuantity";
            this.Column3.ReadOnly = true;
            // 
            // lblDraft
            // 
            this.lblDraft.AutoSize = true;
            this.lblDraft.Location = new System.Drawing.Point(28, 275);
            this.lblDraft.Name = "lblDraft";
            this.lblDraft.Size = new System.Drawing.Size(82, 15);
            this.lblDraft.TabIndex = 10;
            this.lblDraft.Text = "待提交清单";
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
            // cmbDrug
            // 
            this.cmbDrug.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDrug.FormattingEnabled = true;
            this.cmbDrug.Location = new System.Drawing.Point(217, 162);
            this.cmbDrug.Name = "cmbDrug";
            this.cmbDrug.Size = new System.Drawing.Size(168, 23);
            this.cmbDrug.TabIndex = 7;
            // 
            // lblDrugName
            // 
            this.lblDrugName.AutoSize = true;
            this.lblDrugName.Location = new System.Drawing.Point(128, 166);
            this.lblDrugName.Name = "lblDrugName";
            this.lblDrugName.Size = new System.Drawing.Size(82, 15);
            this.lblDrugName.TabIndex = 6;
            this.lblDrugName.Text = "药品名称：";
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
            // txtPrescriptionId
            // 
            this.txtPrescriptionId.Location = new System.Drawing.Point(128, 42);
            this.txtPrescriptionId.Name = "txtPrescriptionId";
            this.txtPrescriptionId.ReadOnly = true;
            this.txtPrescriptionId.Size = new System.Drawing.Size(140, 25);
            this.txtPrescriptionId.TabIndex = 1;
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
            this.grpQueue.Text = "待配处方队列";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(215, 607);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 35);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "撤销处方";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnMoveTop
            // 
            this.btnMoveTop.Location = new System.Drawing.Point(21, 607);
            this.btnMoveTop.Name = "btnMoveTop";
            this.btnMoveTop.Size = new System.Drawing.Size(140, 35);
            this.btnMoveTop.TabIndex = 1;
            this.btnMoveTop.Text = "优先处理(置顶)";
            this.btnMoveTop.UseVisualStyleBackColor = true;
            this.btnMoveTop.Click += new System.EventHandler(this.BtnMoveTop_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(400, 607);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(140, 35);
            this.btnHistory.TabIndex = 3;
            this.btnHistory.Text = "历史处方";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.BtnHistory_Click);
            // 
            // dgvQueue
            // 
            this.dgvQueue.AllowUserToAddRows = false;
            this.dgvQueue.AllowUserToDeleteRows = false;
            this.dgvQueue.AutoGenerateColumns = false;
            this.dgvQueue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQueue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8});
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
            // Column4
            // 
            this.Column4.DataPropertyName = "PrescriptionId";
            this.Column4.HeaderText = "处方号";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "colQueueId";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "PatientNo";
            this.Column5.HeaderText = "患者编号";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "colQueuePatientNo";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "ItemCount";
            this.Column6.HeaderText = "药品种类数";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "colQueueItemCount";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "CreateTimeText";
            this.Column7.HeaderText = "接收时间";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "colQueueCreateTime";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "Status";
            this.Column8.HeaderText = "状态";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "colQueueStatus";
            this.Column8.ReadOnly = true;
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
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.pnlQueue.ResumeLayout(false);
            this.grpQueue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlEntry;
        private System.Windows.Forms.GroupBox grpEntry;
        private System.Windows.Forms.Panel pnlQueue;
        private System.Windows.Forms.GroupBox grpQueue;
        private System.Windows.Forms.Label lblPrescriptionId;
        private System.Windows.Forms.TextBox txtPrescriptionId;
        private System.Windows.Forms.TextBox txtPatientNo;
        private System.Windows.Forms.Label lblPatientNo;
        private System.Windows.Forms.Label lblAddDrug;
        private System.Windows.Forms.Label lblDrugName;
        private System.Windows.Forms.ComboBox cmbDrug;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Label lblDraft;
        private System.Windows.Forms.DataGridView dgvDraft;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.DataGridView dgvQueue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.Button btnMoveTop;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnHistory;
    }
}
