namespace PharmacySorter
{
    partial class FrmMain
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblConnection = new System.Windows.Forms.Label();
            this.lblClock = new System.Windows.Forms.Label();
            this.lblOperator = new System.Windows.Forms.Label();
            this.pnlNav = new System.Windows.Forms.Panel();
            this.btnEmergencyStop = new System.Windows.Forms.Button();
            this.btnUserAdmin = new System.Windows.Forms.Button();
            this.btnAuditLog = new System.Windows.Forms.Button();
            this.btnPrescription = new System.Windows.Forms.Button();
            this.btnPrescriptionCatalog = new System.Windows.Forms.Button();
            this.btnDrugDictionary = new System.Windows.Forms.Button();
            this.btnStationMapping = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.pnlPageContainer = new System.Windows.Forms.Panel();
            this.lblSensorStatus = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlNav.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblSensorStatus);
            this.pnlHeader.Controls.Add(this.lblConnection);
            this.pnlHeader.Controls.Add(this.lblClock);
            this.pnlHeader.Controls.Add(this.lblOperator);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1439, 57);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblConnection
            // 
            this.lblConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConnection.AutoSize = true;
            this.lblConnection.Location = new System.Drawing.Point(1249, 19);
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(142, 15);
            this.lblConnection.TabIndex = 3;
            this.lblConnection.Text = "通信状态：正在连接";
            // 
            // lblClock
            // 
            this.lblClock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClock.AutoSize = true;
            this.lblClock.Location = new System.Drawing.Point(803, 19);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(67, 15);
            this.lblClock.TabIndex = 2;
            this.lblClock.Text = "系统时间";
            // 
            // lblOperator
            // 
            this.lblOperator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOperator.AutoSize = true;
            this.lblOperator.Location = new System.Drawing.Point(24, 19);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(52, 15);
            this.lblOperator.TabIndex = 1;
            this.lblOperator.Text = "操作员";
            // 
            // pnlNav
            // 
            this.pnlNav.Controls.Add(this.btnEmergencyStop);
            this.pnlNav.Controls.Add(this.btnUserAdmin);
            this.pnlNav.Controls.Add(this.btnAuditLog);
            this.pnlNav.Controls.Add(this.btnPrescription);
            this.pnlNav.Controls.Add(this.btnPrescriptionCatalog);
            this.pnlNav.Controls.Add(this.btnDrugDictionary);
            this.pnlNav.Controls.Add(this.btnStationMapping);
            this.pnlNav.Controls.Add(this.btnDashboard);
            this.pnlNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNav.Location = new System.Drawing.Point(0, 57);
            this.pnlNav.Name = "pnlNav";
            this.pnlNav.Size = new System.Drawing.Size(166, 721);
            this.pnlNav.TabIndex = 1;
            // 
            // btnEmergencyStop
            // 
            this.btnEmergencyStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEmergencyStop.Location = new System.Drawing.Point(20, 646);
            this.btnEmergencyStop.Name = "btnEmergencyStop";
            this.btnEmergencyStop.Size = new System.Drawing.Size(124, 50);
            this.btnEmergencyStop.TabIndex = 5;
            this.btnEmergencyStop.Text = "急停复位";
            this.btnEmergencyStop.UseVisualStyleBackColor = true;
            this.btnEmergencyStop.Click += new System.EventHandler(this.BtnEmergencyStop_Click);
            // 
            // btnUserAdmin
            // 
            this.btnUserAdmin.Location = new System.Drawing.Point(20, 356);
            this.btnUserAdmin.Name = "btnUserAdmin";
            this.btnUserAdmin.Size = new System.Drawing.Size(124, 32);
            this.btnUserAdmin.TabIndex = 6;
            this.btnUserAdmin.Text = "账号管理";
            this.btnUserAdmin.UseVisualStyleBackColor = true;
            this.btnUserAdmin.Click += new System.EventHandler(this.BtnUserAdmin_Click);
            // 
            // btnAuditLog
            // 
            this.btnAuditLog.Location = new System.Drawing.Point(20, 304);
            this.btnAuditLog.Name = "btnAuditLog";
            this.btnAuditLog.Size = new System.Drawing.Size(124, 32);
            this.btnAuditLog.TabIndex = 4;
            this.btnAuditLog.Text = "操作日志";
            this.btnAuditLog.UseVisualStyleBackColor = true;
            this.btnAuditLog.Click += new System.EventHandler(this.BtnAuditLog_Click);
            // 
            // btnPrescription
            // 
            this.btnPrescription.Location = new System.Drawing.Point(20, 87);
            this.btnPrescription.Name = "btnPrescription";
            this.btnPrescription.Size = new System.Drawing.Size(124, 31);
            this.btnPrescription.TabIndex = 3;
            this.btnPrescription.Text = "待配任务";
            this.btnPrescription.UseVisualStyleBackColor = true;
            this.btnPrescription.Click += new System.EventHandler(this.BtnPrescription_Click);
            // 
            // btnPrescriptionCatalog
            // 
            this.btnPrescriptionCatalog.Location = new System.Drawing.Point(20, 142);
            this.btnPrescriptionCatalog.Name = "btnPrescriptionCatalog";
            this.btnPrescriptionCatalog.Size = new System.Drawing.Size(124, 32);
            this.btnPrescriptionCatalog.TabIndex = 7;
            this.btnPrescriptionCatalog.Text = "处方模板";
            this.btnPrescriptionCatalog.UseVisualStyleBackColor = true;
            this.btnPrescriptionCatalog.Click += new System.EventHandler(this.BtnPrescriptionCatalog_Click);
            // 
            // btnDrugDictionary
            // 
            this.btnDrugDictionary.Location = new System.Drawing.Point(20, 197);
            this.btnDrugDictionary.Name = "btnDrugDictionary";
            this.btnDrugDictionary.Size = new System.Drawing.Size(124, 31);
            this.btnDrugDictionary.TabIndex = 2;
            this.btnDrugDictionary.Text = "药品字典";
            this.btnDrugDictionary.UseVisualStyleBackColor = true;
            this.btnDrugDictionary.Click += new System.EventHandler(this.BtnDrugDictionary_Click);
            // 
            // btnStationMapping
            // 
            this.btnStationMapping.Location = new System.Drawing.Point(20, 249);
            this.btnStationMapping.Name = "btnStationMapping";
            this.btnStationMapping.Size = new System.Drawing.Size(124, 31);
            this.btnStationMapping.TabIndex = 1;
            this.btnStationMapping.Text = "工位指令";
            this.btnStationMapping.UseVisualStyleBackColor = true;
            this.btnStationMapping.Click += new System.EventHandler(this.BtnStationMapping_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.Location = new System.Drawing.Point(20, 33);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(124, 32);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "配药看板";
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.BtnDashboard_Click);
            // 
            // pnlPageContainer
            // 
            this.pnlPageContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPageContainer.Location = new System.Drawing.Point(166, 57);
            this.pnlPageContainer.Name = "pnlPageContainer";
            this.pnlPageContainer.Size = new System.Drawing.Size(1273, 721);
            this.pnlPageContainer.TabIndex = 2;
            // 
            // lblSensorStatus
            // 
            this.lblSensorStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSensorStatus.AutoSize = true;
            this.lblSensorStatus.Location = new System.Drawing.Point(1028, 19);
            this.lblSensorStatus.Name = "lblSensorStatus";
            this.lblSensorStatus.Size = new System.Drawing.Size(126, 15);
            this.lblSensorStatus.TabIndex = 4;
            this.lblSensorStatus.Text = "传感器状态：检测中";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1439, 778);
            this.Controls.Add(this.pnlPageContainer);
            this.Controls.Add(this.pnlNav);
            this.Controls.Add(this.pnlHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "医院药品分拣系统";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlNav.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblConnection;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Label lblOperator;
        private System.Windows.Forms.Panel pnlNav;
        private System.Windows.Forms.Panel pnlPageContainer;
        private System.Windows.Forms.Button btnEmergencyStop;
        private System.Windows.Forms.Button btnAuditLog;
        private System.Windows.Forms.Button btnUserAdmin;
        private System.Windows.Forms.Button btnPrescription;
        private System.Windows.Forms.Button btnPrescriptionCatalog;
        private System.Windows.Forms.Button btnDrugDictionary;
        private System.Windows.Forms.Button btnStationMapping;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Label lblSensorStatus;
    }
}

