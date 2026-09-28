namespace PharmacySorter
{
    partial class UcSystemAuditLog
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
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.cmbLogType = new System.Windows.Forms.ComboBox();
            this.lblLogType = new System.Windows.Forms.Label();
            this.txtPrescriptionId = new System.Windows.Forms.TextBox();
            this.lblPrescriptionId = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.btnSearch);
            this.pnlFilter.Controls.Add(this.btnExport);
            this.pnlFilter.Controls.Add(this.cmbLogType);
            this.pnlFilter.Controls.Add(this.lblLogType);
            this.pnlFilter.Controls.Add(this.txtPrescriptionId);
            this.pnlFilter.Controls.Add(this.lblPrescriptionId);
            this.pnlFilter.Controls.Add(this.dtpEnd);
            this.pnlFilter.Controls.Add(this.lblEnd);
            this.pnlFilter.Controls.Add(this.lblStart);
            this.pnlFilter.Controls.Add(this.dtpStart);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1192, 95);
            this.pnlFilter.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(640, 33);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 39);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(1018, 33);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(121, 39);
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "导出为 Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // cmbLogType
            // 
            this.cmbLogType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogType.FormattingEnabled = true;
            this.cmbLogType.Location = new System.Drawing.Point(390, 59);
            this.cmbLogType.Name = "cmbLogType";
            this.cmbLogType.Size = new System.Drawing.Size(200, 23);
            this.cmbLogType.TabIndex = 7;
            // 
            // lblLogType
            // 
            this.lblLogType.AutoSize = true;
            this.lblLogType.Location = new System.Drawing.Point(317, 69);
            this.lblLogType.Name = "lblLogType";
            this.lblLogType.Size = new System.Drawing.Size(67, 15);
            this.lblLogType.TabIndex = 6;
            this.lblLogType.Text = "日志类型";
            // 
            // txtPrescriptionId
            // 
            this.txtPrescriptionId.Location = new System.Drawing.Point(97, 59);
            this.txtPrescriptionId.Name = "txtPrescriptionId";
            this.txtPrescriptionId.Size = new System.Drawing.Size(200, 25);
            this.txtPrescriptionId.TabIndex = 5;
            // 
            // lblPrescriptionId
            // 
            this.lblPrescriptionId.AutoSize = true;
            this.lblPrescriptionId.Location = new System.Drawing.Point(24, 69);
            this.lblPrescriptionId.Name = "lblPrescriptionId";
            this.lblPrescriptionId.Size = new System.Drawing.Size(67, 15);
            this.lblPrescriptionId.TabIndex = 4;
            this.lblPrescriptionId.Text = "任务编号";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(390, 15);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.ShowCheckBox = true;
            this.dtpEnd.Size = new System.Drawing.Size(200, 25);
            this.dtpEnd.TabIndex = 3;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(317, 21);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(67, 15);
            this.lblEnd.TabIndex = 2;
            this.lblEnd.Text = "结束时间";
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(24, 21);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(67, 15);
            this.lblStart.TabIndex = 1;
            this.lblStart.Text = "开始时间";
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(97, 15);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.ShowCheckBox = true;
            this.dtpStart.Size = new System.Drawing.Size(200, 25);
            this.dtpStart.TabIndex = 0;
            // 
            // dgvLog
            // 
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.AllowUserToDeleteRows = false;
            this.dgvLog.AutoGenerateColumns = false;
            this.dgvLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgvLog.Location = new System.Drawing.Point(27, 101);
            this.dgvLog.MultiSelect = false;
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.ReadOnly = true;
            this.dgvLog.RowHeadersVisible = false;
            this.dgvLog.RowHeadersWidth = 51;
            this.dgvLog.RowTemplate.Height = 27;
            this.dgvLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLog.Size = new System.Drawing.Size(1140, 574);
            this.dgvLog.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "LogTimeText";
            this.Column1.HeaderText = "时间";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "colLogTime";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "PrescriptionText";
            this.Column2.HeaderText = "处方号";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "colPrescriptionId";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "LogType";
            this.Column3.HeaderText = "事件类型";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "colLogType";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Content";
            this.Column4.HeaderText = "详细内容";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "colContent";
            this.Column4.ReadOnly = true;
            // 
            // UcSystemAuditLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvLog);
            this.Controls.Add(this.pnlFilter);
            this.Name = "UcSystemAuditLog";
            this.Size = new System.Drawing.Size(1192, 742);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.DataGridView dgvLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Label lblLogType;
        private System.Windows.Forms.TextBox txtPrescriptionId;
        private System.Windows.Forms.Label lblPrescriptionId;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmbLogType;
    }
}
