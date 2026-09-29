namespace PharmacySorter
{
    partial class FrmPrescriptionHistory
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblPrescriptionId = new System.Windows.Forms.Label();
            this.txtPrescriptionId = new System.Windows.Forms.TextBox();
            this.lblStart = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRequeue = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.colHistoryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistoryPatientNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistoryItemCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistoryCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistoryStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPrescriptionId
            // 
            this.lblPrescriptionId.AutoSize = true;
            this.lblPrescriptionId.Location = new System.Drawing.Point(18, 22);
            this.lblPrescriptionId.Name = "lblPrescriptionId";
            this.lblPrescriptionId.Size = new System.Drawing.Size(67, 15);
            this.lblPrescriptionId.TabIndex = 8;
            this.lblPrescriptionId.Text = "任务编号";
            // 
            // txtPrescriptionId
            // 
            this.txtPrescriptionId.Location = new System.Drawing.Point(92, 16);
            this.txtPrescriptionId.Name = "txtPrescriptionId";
            this.txtPrescriptionId.Size = new System.Drawing.Size(120, 25);
            this.txtPrescriptionId.TabIndex = 7;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(230, 22);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(67, 15);
            this.lblStart.TabIndex = 6;
            this.lblStart.Text = "开始日期";
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(304, 16);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(130, 25);
            this.dtpStart.TabIndex = 5;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(452, 22);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(67, 15);
            this.lblEnd.TabIndex = 4;
            this.lblEnd.Text = "结束日期";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(526, 16);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(130, 25);
            this.dtpEnd.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(680, 14);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // btnRequeue
            // 
            this.btnRequeue.Location = new System.Drawing.Point(788, 14);
            this.btnRequeue.Name = "btnRequeue";
            this.btnRequeue.Size = new System.Drawing.Size(90, 30);
            this.btnRequeue.TabIndex = 9;
            this.btnRequeue.Text = "再次配药";
            this.btnRequeue.UseVisualStyleBackColor = true;
            this.btnRequeue.Click += new System.EventHandler(this.BtnRequeue_Click);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(18, 450);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(61, 15);
            this.lblCount.TabIndex = 1;
            this.lblCount.Text = "共 0 条";
            // 
            // dgvHistory
            // 
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHistoryId,
            this.colHistoryPatientNo,
            this.colHistoryItemCount,
            this.colHistoryCreateTime,
            this.colHistoryStatus});
            this.dgvHistory.Location = new System.Drawing.Point(18, 62);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.RowHeadersWidth = 51;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.Size = new System.Drawing.Size(860, 360);
            this.dgvHistory.TabIndex = 0;
            // 
            // colHistoryId
            // 
            this.colHistoryId.DataPropertyName = "OrderId";
            this.colHistoryId.HeaderText = "任务号";
            this.colHistoryId.MinimumWidth = 6;
            this.colHistoryId.Name = "colHistoryId";
            this.colHistoryId.ReadOnly = true;
            // 
            // colHistoryPatientNo
            // 
            this.colHistoryPatientNo.DataPropertyName = "PatientNo";
            this.colHistoryPatientNo.HeaderText = "患者编号";
            this.colHistoryPatientNo.MinimumWidth = 6;
            this.colHistoryPatientNo.Name = "colHistoryPatientNo";
            this.colHistoryPatientNo.ReadOnly = true;
            // 
            // colHistoryItemCount
            // 
            this.colHistoryItemCount.DataPropertyName = "PrescriptionName";
            this.colHistoryItemCount.HeaderText = "处方名称";
            this.colHistoryItemCount.MinimumWidth = 6;
            this.colHistoryItemCount.Name = "colHistoryItemCount";
            this.colHistoryItemCount.ReadOnly = true;
            // 
            // colHistoryCreateTime
            // 
            this.colHistoryCreateTime.DataPropertyName = "CreateTimeText";
            this.colHistoryCreateTime.HeaderText = "接收时间";
            this.colHistoryCreateTime.MinimumWidth = 6;
            this.colHistoryCreateTime.Name = "colHistoryCreateTime";
            this.colHistoryCreateTime.ReadOnly = true;
            // 
            // colHistoryStatus
            // 
            this.colHistoryStatus.DataPropertyName = "Status";
            this.colHistoryStatus.HeaderText = "状态";
            this.colHistoryStatus.MinimumWidth = 6;
            this.colHistoryStatus.Name = "colHistoryStatus";
            this.colHistoryStatus.ReadOnly = true;
            // 
            // FrmPrescriptionHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 480);
            this.Controls.Add(this.dgvHistory);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.btnRequeue);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.txtPrescriptionId);
            this.Controls.Add(this.lblPrescriptionId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPrescriptionHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "历史任务查询";
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblPrescriptionId;
        private System.Windows.Forms.TextBox txtPrescriptionId;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRequeue;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistoryId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistoryPatientNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistoryItemCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistoryCreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistoryStatus;
    }
}
