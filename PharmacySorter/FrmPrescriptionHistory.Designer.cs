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
            this.lblCount = new System.Windows.Forms.Label();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.colHistoryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistoryPatientNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistoryItemCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistoryCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistoryStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();
            this.lblPrescriptionId.AutoSize = true;
            this.lblPrescriptionId.Location = new System.Drawing.Point(18, 22);
            this.lblPrescriptionId.Name = "lblPrescriptionId";
            this.lblPrescriptionId.Text = "处方编号";
            this.txtPrescriptionId.Location = new System.Drawing.Point(92, 16);
            this.txtPrescriptionId.Name = "txtPrescriptionId";
            this.txtPrescriptionId.Size = new System.Drawing.Size(120, 25);
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(230, 22);
            this.lblStart.Name = "lblStart";
            this.lblStart.Text = "开始日期";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(304, 16);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(130, 25);
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(452, 22);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Text = "结束日期";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(526, 16);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(130, 25);
            this.btnSearch.Location = new System.Drawing.Point(680, 14);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 30);
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(790, 22);
            this.lblCount.Name = "lblCount";
            this.lblCount.Text = "共 0 张";
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.AutoGenerateColumns = false;
            this.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistory.Location = new System.Drawing.Point(18, 62);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.Size = new System.Drawing.Size(860, 400);
            this.colHistoryId.DataPropertyName = "PrescriptionId";
            this.colHistoryId.HeaderText = "处方号";
            this.colHistoryId.Name = "colHistoryId";
            this.colHistoryPatientNo.DataPropertyName = "PatientNo";
            this.colHistoryPatientNo.HeaderText = "患者编号";
            this.colHistoryPatientNo.Name = "colHistoryPatientNo";
            this.colHistoryItemCount.DataPropertyName = "ItemCount";
            this.colHistoryItemCount.HeaderText = "药品数";
            this.colHistoryItemCount.Name = "colHistoryItemCount";
            this.colHistoryCreateTime.DataPropertyName = "CreateTimeText";
            this.colHistoryCreateTime.HeaderText = "接收时间";
            this.colHistoryCreateTime.Name = "colHistoryCreateTime";
            this.colHistoryStatus.DataPropertyName = "Status";
            this.colHistoryStatus.HeaderText = "状态";
            this.colHistoryStatus.Name = "colHistoryStatus";
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 480);
            this.Controls.Add(this.dgvHistory);
            this.Controls.Add(this.lblCount);
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
            this.Text = "历史处方查询";
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.dgvHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHistoryId,
            this.colHistoryPatientNo,
            this.colHistoryItemCount,
            this.colHistoryCreateTime,
            this.colHistoryStatus});
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
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistoryId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistoryPatientNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistoryItemCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistoryCreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistoryStatus;
    }
}
