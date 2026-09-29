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
            this.colLogTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrescriptionId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLogType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlPage = new System.Windows.Forms.Panel();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.lblPage = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.pnlFilter.SuspendLayout();
            this.pnlPage.SuspendLayout();
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
            this.dgvLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLogTime,
            this.colPrescriptionId,
            this.colLogType,
            this.colContent});
            this.dgvLog.Location = new System.Drawing.Point(27, 101);
            this.dgvLog.MultiSelect = false;
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.ReadOnly = true;
            this.dgvLog.RowHeadersVisible = false;
            this.dgvLog.RowHeadersWidth = 51;
            this.dgvLog.RowTemplate.Height = 27;
            this.dgvLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLog.Size = new System.Drawing.Size(1140, 517);
            this.dgvLog.TabIndex = 1;
            // 
            // pnlPage
            // 
            this.pnlPage.Controls.Add(this.btnFirst);
            this.pnlPage.Controls.Add(this.btnPrevious);
            this.pnlPage.Controls.Add(this.lblPage);
            this.pnlPage.Controls.Add(this.btnNext);
            this.pnlPage.Controls.Add(this.btnLast);
            this.pnlPage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPage.Location = new System.Drawing.Point(0, 692);
            this.pnlPage.Name = "pnlPage";
            this.pnlPage.Size = new System.Drawing.Size(1192, 50);
            this.pnlPage.TabIndex = 2;
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(330, 8);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(75, 32);
            this.btnFirst.TabIndex = 0;
            this.btnFirst.Text = "首页";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.BtnFirst_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(420, 8);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 32);
            this.btnPrevious.TabIndex = 1;
            this.btnPrevious.Text = "上一页";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.BtnPrevious_Click);
            // 
            // lblPage
            // 
            this.lblPage.Location = new System.Drawing.Point(510, 14);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(170, 23);
            this.lblPage.TabIndex = 2;
            this.lblPage.Text = "第 1/1 页，共 0 条";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(695, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 32);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "下一页";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(785, 8);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(75, 32);
            this.btnLast.TabIndex = 4;
            this.btnLast.Text = "末页";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.BtnLast_Click);
            // 
            // colLogTime
            // 
            this.colLogTime.DataPropertyName = "LogTimeText";
            this.colLogTime.HeaderText = "时间";
            this.colLogTime.MinimumWidth = 6;
            this.colLogTime.Name = "colLogTime";
            this.colLogTime.ReadOnly = true;
            // 
            // colPrescriptionId
            // 
            this.colPrescriptionId.DataPropertyName = "PrescriptionText";
            this.colPrescriptionId.HeaderText = "任务编号";
            this.colPrescriptionId.MinimumWidth = 6;
            this.colPrescriptionId.Name = "colPrescriptionId";
            this.colPrescriptionId.ReadOnly = true;
            // 
            // colLogType
            // 
            this.colLogType.DataPropertyName = "LogType";
            this.colLogType.HeaderText = "日志类型";
            this.colLogType.MinimumWidth = 6;
            this.colLogType.Name = "colLogType";
            this.colLogType.ReadOnly = true;
            // 
            // colContent
            // 
            this.colContent.DataPropertyName = "Content";
            this.colContent.HeaderText = "详细内容";
            this.colContent.MinimumWidth = 6;
            this.colContent.Name = "colContent";
            this.colContent.ReadOnly = true;
            // 
            // UcSystemAuditLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvLog);
            this.Controls.Add(this.pnlPage);
            this.Controls.Add(this.pnlFilter);
            this.Name = "UcSystemAuditLog";
            this.Size = new System.Drawing.Size(1192, 742);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlPage.ResumeLayout(false);
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
        private System.Windows.Forms.Panel pnlPage;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLogTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrescriptionId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLogType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContent;
    }
}
