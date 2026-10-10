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
            this.btnResetArm = new System.Windows.Forms.Button();
            this.lblSensorStatus = new System.Windows.Forms.Label();
            this.btnEmergencyStop = new System.Windows.Forms.Button();
            this.lblConnection = new System.Windows.Forms.Label();
            this.btnReconnect = new System.Windows.Forms.Button();
            this.pnlNav = new System.Windows.Forms.Panel();
            this.grpStats = new System.Windows.Forms.GroupBox();
            this.tlpStats = new System.Windows.Forms.TableLayoutPanel();
            this.lblStatWaiting = new System.Windows.Forms.Label();
            this.lblStatWaitingValue = new System.Windows.Forms.Label();
            this.lblStatCompleted = new System.Windows.Forms.Label();
            this.lblStatCompletedValue = new System.Windows.Forms.Label();
            this.lblStatDropped = new System.Windows.Forms.Label();
            this.lblStatDroppedValue = new System.Windows.Forms.Label();
            this.lblStatAbnormal = new System.Windows.Forms.Label();
            this.lblStatAbnormalValue = new System.Windows.Forms.Label();
            this.btnSystemOps = new System.Windows.Forms.Button();
            this.btnPrescriptionCatalog = new System.Windows.Forms.Button();
            this.btnStationMapping = new System.Windows.Forms.Button();
            this.btnDrugDictionary = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.pnlPageContainer = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            this.pnlNav.SuspendLayout();
            this.grpStats.SuspendLayout();
            this.tlpStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.btnResetArm);
            this.pnlHeader.Controls.Add(this.lblSensorStatus);
            this.pnlHeader.Controls.Add(this.btnEmergencyStop);
            this.pnlHeader.Controls.Add(this.lblConnection);
            this.pnlHeader.Controls.Add(this.btnReconnect);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1511, 57);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnResetArm
            // 
            this.btnResetArm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResetArm.Location = new System.Drawing.Point(210, 12);
            this.btnResetArm.Name = "btnResetArm";
            this.btnResetArm.Size = new System.Drawing.Size(94, 28);
            this.btnResetArm.TabIndex = 6;
            this.btnResetArm.Text = "复位";
            this.btnResetArm.UseVisualStyleBackColor = true;
            this.btnResetArm.Click += new System.EventHandler(this.BtnResetArm_Click);
            // 
            // lblSensorStatus
            // 
            this.lblSensorStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSensorStatus.AutoSize = true;
            this.lblSensorStatus.Location = new System.Drawing.Point(907, 19);
            this.lblSensorStatus.Name = "lblSensorStatus";
            this.lblSensorStatus.Size = new System.Drawing.Size(142, 15);
            this.lblSensorStatus.TabIndex = 4;
            this.lblSensorStatus.Text = "传感器状态：检测中";
            // 
            // btnEmergencyStop
            // 
            this.btnEmergencyStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEmergencyStop.Location = new System.Drawing.Point(44, 12);
            this.btnEmergencyStop.Name = "btnEmergencyStop";
            this.btnEmergencyStop.Size = new System.Drawing.Size(94, 28);
            this.btnEmergencyStop.TabIndex = 5;
            this.btnEmergencyStop.Text = "急停";
            this.btnEmergencyStop.UseVisualStyleBackColor = true;
            this.btnEmergencyStop.Click += new System.EventHandler(this.BtnEmergencyStop_Click);
            // 
            // lblConnection
            // 
            this.lblConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConnection.AutoSize = true;
            this.lblConnection.Location = new System.Drawing.Point(1321, 19);
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(142, 15);
            this.lblConnection.TabIndex = 3;
            this.lblConnection.Text = "通信状态：正在连接";
            // 
            // btnReconnect
            // 
            this.btnReconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReconnect.Location = new System.Drawing.Point(396, 12);
            this.btnReconnect.Name = "btnReconnect";
            this.btnReconnect.Size = new System.Drawing.Size(90, 28);
            this.btnReconnect.TabIndex = 5;
            this.btnReconnect.Text = "重新连接";
            this.btnReconnect.UseVisualStyleBackColor = true;
            this.btnReconnect.Click += new System.EventHandler(this.BtnReconnect_Click);
            // 
            // pnlNav
            // 
            this.pnlNav.Controls.Add(this.grpStats);
            this.pnlNav.Controls.Add(this.btnSystemOps);
            this.pnlNav.Controls.Add(this.btnPrescriptionCatalog);
            this.pnlNav.Controls.Add(this.btnStationMapping);
            this.pnlNav.Controls.Add(this.btnDrugDictionary);
            this.pnlNav.Controls.Add(this.btnDashboard);
            this.pnlNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNav.Location = new System.Drawing.Point(0, 57);
            this.pnlNav.Name = "pnlNav";
            this.pnlNav.Size = new System.Drawing.Size(182, 721);
            this.pnlNav.TabIndex = 1;
            // 
            // grpStats
            // 
            this.grpStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpStats.Controls.Add(this.tlpStats);
            this.grpStats.Location = new System.Drawing.Point(20, 319);
            this.grpStats.Name = "grpStats";
            this.grpStats.Size = new System.Drawing.Size(142, 284);
            this.grpStats.TabIndex = 8;
            this.grpStats.TabStop = false;
            this.grpStats.Text = "今日统计";
            // 
            // tlpStats
            // 
            this.tlpStats.ColumnCount = 2;
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.tlpStats.Controls.Add(this.lblStatWaiting, 0, 0);
            this.tlpStats.Controls.Add(this.lblStatWaitingValue, 1, 0);
            this.tlpStats.Controls.Add(this.lblStatCompleted, 0, 1);
            this.tlpStats.Controls.Add(this.lblStatCompletedValue, 1, 1);
            this.tlpStats.Controls.Add(this.lblStatDropped, 0, 2);
            this.tlpStats.Controls.Add(this.lblStatDroppedValue, 1, 2);
            this.tlpStats.Controls.Add(this.lblStatAbnormal, 0, 3);
            this.tlpStats.Controls.Add(this.lblStatAbnormalValue, 1, 3);
            this.tlpStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStats.Location = new System.Drawing.Point(3, 21);
            this.tlpStats.Name = "tlpStats";
            this.tlpStats.RowCount = 4;
            this.tlpStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStats.Size = new System.Drawing.Size(136, 260);
            this.tlpStats.TabIndex = 0;
            // 
            // lblStatWaiting
            // 
            this.lblStatWaiting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatWaiting.Location = new System.Drawing.Point(3, 0);
            this.lblStatWaiting.Name = "lblStatWaiting";
            this.lblStatWaiting.Size = new System.Drawing.Size(72, 65);
            this.lblStatWaiting.TabIndex = 0;
            this.lblStatWaiting.Text = "待配任务";
            this.lblStatWaiting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatWaitingValue
            // 
            this.lblStatWaitingValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatWaitingValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStatWaitingValue.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblStatWaitingValue.Location = new System.Drawing.Point(81, 0);
            this.lblStatWaitingValue.Name = "lblStatWaitingValue";
            this.lblStatWaitingValue.Size = new System.Drawing.Size(52, 65);
            this.lblStatWaitingValue.TabIndex = 1;
            this.lblStatWaitingValue.Text = "0 单";
            this.lblStatWaitingValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatCompleted
            // 
            this.lblStatCompleted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatCompleted.Location = new System.Drawing.Point(3, 65);
            this.lblStatCompleted.Name = "lblStatCompleted";
            this.lblStatCompleted.Size = new System.Drawing.Size(72, 65);
            this.lblStatCompleted.TabIndex = 2;
            this.lblStatCompleted.Text = "今日完成";
            this.lblStatCompleted.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatCompletedValue
            // 
            this.lblStatCompletedValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatCompletedValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStatCompletedValue.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblStatCompletedValue.Location = new System.Drawing.Point(81, 65);
            this.lblStatCompletedValue.Name = "lblStatCompletedValue";
            this.lblStatCompletedValue.Size = new System.Drawing.Size(52, 65);
            this.lblStatCompletedValue.TabIndex = 3;
            this.lblStatCompletedValue.Text = "0 单";
            this.lblStatCompletedValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatDropped
            // 
            this.lblStatDropped.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatDropped.Location = new System.Drawing.Point(3, 130);
            this.lblStatDropped.Name = "lblStatDropped";
            this.lblStatDropped.Size = new System.Drawing.Size(72, 65);
            this.lblStatDropped.TabIndex = 4;
            this.lblStatDropped.Text = "今日落药";
            this.lblStatDropped.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatDroppedValue
            // 
            this.lblStatDroppedValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatDroppedValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStatDroppedValue.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblStatDroppedValue.Location = new System.Drawing.Point(81, 130);
            this.lblStatDroppedValue.Name = "lblStatDroppedValue";
            this.lblStatDroppedValue.Size = new System.Drawing.Size(52, 65);
            this.lblStatDroppedValue.TabIndex = 5;
            this.lblStatDroppedValue.Text = "0 盒";
            this.lblStatDroppedValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatAbnormal
            // 
            this.lblStatAbnormal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatAbnormal.Location = new System.Drawing.Point(3, 195);
            this.lblStatAbnormal.Name = "lblStatAbnormal";
            this.lblStatAbnormal.Size = new System.Drawing.Size(72, 65);
            this.lblStatAbnormal.TabIndex = 6;
            this.lblStatAbnormal.Text = "今日异常";
            this.lblStatAbnormal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatAbnormalValue
            // 
            this.lblStatAbnormalValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatAbnormalValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStatAbnormalValue.ForeColor = System.Drawing.Color.Firebrick;
            this.lblStatAbnormalValue.Location = new System.Drawing.Point(81, 195);
            this.lblStatAbnormalValue.Name = "lblStatAbnormalValue";
            this.lblStatAbnormalValue.Size = new System.Drawing.Size(52, 65);
            this.lblStatAbnormalValue.TabIndex = 7;
            this.lblStatAbnormalValue.Text = "0 单";
            this.lblStatAbnormalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSystemOps
            // 
            this.btnSystemOps.Location = new System.Drawing.Point(30, 252);
            this.btnSystemOps.Name = "btnSystemOps";
            this.btnSystemOps.Size = new System.Drawing.Size(124, 32);
            this.btnSystemOps.TabIndex = 4;
            this.btnSystemOps.Text = "系统运维";
            this.btnSystemOps.UseVisualStyleBackColor = true;
            this.btnSystemOps.Click += new System.EventHandler(this.BtnSystemOps_Click);
            // 
            // btnPrescriptionCatalog
            // 
            this.btnPrescriptionCatalog.Location = new System.Drawing.Point(30, 87);
            this.btnPrescriptionCatalog.Name = "btnPrescriptionCatalog";
            this.btnPrescriptionCatalog.Size = new System.Drawing.Size(124, 32);
            this.btnPrescriptionCatalog.TabIndex = 7;
            this.btnPrescriptionCatalog.Text = "处方模板";
            this.btnPrescriptionCatalog.UseVisualStyleBackColor = true;
            this.btnPrescriptionCatalog.Click += new System.EventHandler(this.BtnPrescriptionCatalog_Click);
            // 
            // btnStationMapping
            // 
            this.btnStationMapping.Location = new System.Drawing.Point(30, 197);
            this.btnStationMapping.Name = "btnStationMapping";
            this.btnStationMapping.Size = new System.Drawing.Size(124, 31);
            this.btnStationMapping.TabIndex = 3;
            this.btnStationMapping.Text = "工位指令";
            this.btnStationMapping.UseVisualStyleBackColor = true;
            this.btnStationMapping.Click += new System.EventHandler(this.BtnStationMapping_Click);
            // 
            // btnDrugDictionary
            // 
            this.btnDrugDictionary.Location = new System.Drawing.Point(30, 142);
            this.btnDrugDictionary.Name = "btnDrugDictionary";
            this.btnDrugDictionary.Size = new System.Drawing.Size(124, 31);
            this.btnDrugDictionary.TabIndex = 2;
            this.btnDrugDictionary.Text = "药品字典";
            this.btnDrugDictionary.UseVisualStyleBackColor = true;
            this.btnDrugDictionary.Click += new System.EventHandler(this.BtnDrugDictionary_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.Location = new System.Drawing.Point(30, 33);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(124, 32);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "配药工作台";
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.BtnDashboard_Click);
            // 
            // pnlPageContainer
            // 
            this.pnlPageContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPageContainer.Location = new System.Drawing.Point(182, 57);
            this.pnlPageContainer.Name = "pnlPageContainer";
            this.pnlPageContainer.Size = new System.Drawing.Size(1329, 721);
            this.pnlPageContainer.TabIndex = 2;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1511, 778);
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
            this.grpStats.ResumeLayout(false);
            this.tlpStats.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblConnection;
        private System.Windows.Forms.Button btnReconnect;
        private System.Windows.Forms.Panel pnlNav;
        private System.Windows.Forms.Panel pnlPageContainer;
        private System.Windows.Forms.Button btnEmergencyStop;
        private System.Windows.Forms.Button btnSystemOps;
        private System.Windows.Forms.Button btnPrescriptionCatalog;
        private System.Windows.Forms.Button btnStationMapping;
        private System.Windows.Forms.Button btnDrugDictionary;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Label lblSensorStatus;
        private System.Windows.Forms.GroupBox grpStats;
        private System.Windows.Forms.TableLayoutPanel tlpStats;
        private System.Windows.Forms.Label lblStatWaiting;
        private System.Windows.Forms.Label lblStatWaitingValue;
        private System.Windows.Forms.Label lblStatCompleted;
        private System.Windows.Forms.Label lblStatCompletedValue;
        private System.Windows.Forms.Label lblStatDropped;
        private System.Windows.Forms.Label lblStatDroppedValue;
        private System.Windows.Forms.Label lblStatAbnormal;
        private System.Windows.Forms.Label lblStatAbnormalValue;
        private System.Windows.Forms.Button btnResetArm;
    }
}

