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
            this.lblItems = new System.Windows.Forms.Label();
            this.pnlArm = new System.Windows.Forms.Panel();
            this.lblArmHint = new System.Windows.Forms.Label();
            this.lblRightStation = new System.Windows.Forms.Label();
            this.lblSlot = new System.Windows.Forms.Label();
            this.lblLeftStation = new System.Windows.Forms.Label();
            this.pnlRightStation = new System.Windows.Forms.Panel();
            this.pnlSlot = new System.Windows.Forms.Panel();
            this.pnlLeftStation = new System.Windows.Forms.Panel();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSummary.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.tlpBody.SuspendLayout();
            this.pnlItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.pnlArm.SuspendLayout();
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
            this.lblSummary.Text = "当前没有待配处方";
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
            this.btnStart.Text = "启动连续配药";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
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
            this.dgvItems.AutoGenerateColumns = false;
            this.dgvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dgvItems.Location = new System.Drawing.Point(12, 48);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.RowHeadersWidth = 51;
            this.dgvItems.RowTemplate.Height = 27;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvItems.Size = new System.Drawing.Size(813, 405);
            this.dgvItems.TabIndex = 1;
            // 
            // lblItems
            // 
            this.lblItems.AutoSize = true;
            this.lblItems.Location = new System.Drawing.Point(17, 28);
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(99, 15);
            this.lblItems.TabIndex = 0;
            this.lblItems.Text = "处方明细 0/0";
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
            this.lblArmHint.Location = new System.Drawing.Point(24, 270);
            this.lblArmHint.Name = "lblArmHint";
            this.lblArmHint.Size = new System.Drawing.Size(370, 40);
            this.lblArmHint.TabIndex = 6;
            this.lblArmHint.Text = "机械臂待命";
            // 
            // lblRightStation
            // 
            this.lblRightStation.Location = new System.Drawing.Point(302, 118);
            this.lblRightStation.Name = "lblRightStation";
            this.lblRightStation.Size = new System.Drawing.Size(74, 15);
            this.lblRightStation.TabIndex = 5;
            this.lblRightStation.Text = "右药位";
            // 
            // lblSlot
            // 
            this.lblSlot.Location = new System.Drawing.Point(164, 118);
            this.lblSlot.Name = "lblSlot";
            this.lblSlot.Size = new System.Drawing.Size(90, 15);
            this.lblSlot.TabIndex = 4;
            this.lblSlot.Text = "正前分拣槽";
            // 
            // lblLeftStation
            // 
            this.lblLeftStation.Location = new System.Drawing.Point(42, 118);
            this.lblLeftStation.Name = "lblLeftStation";
            this.lblLeftStation.Size = new System.Drawing.Size(74, 15);
            this.lblLeftStation.TabIndex = 3;
            this.lblLeftStation.Text = "左药位";
            // 
            // pnlRightStation
            // 
            this.pnlRightStation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlRightStation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRightStation.Location = new System.Drawing.Point(284, 150);
            this.pnlRightStation.Name = "pnlRightStation";
            this.pnlRightStation.Size = new System.Drawing.Size(110, 90);
            this.pnlRightStation.TabIndex = 2;
            // 
            // pnlSlot
            // 
            this.pnlSlot.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlSlot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSlot.Location = new System.Drawing.Point(154, 150);
            this.pnlSlot.Name = "pnlSlot";
            this.pnlSlot.Size = new System.Drawing.Size(110, 90);
            this.pnlSlot.TabIndex = 1;
            // 
            // pnlLeftStation
            // 
            this.pnlLeftStation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlLeftStation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLeftStation.Location = new System.Drawing.Point(24, 150);
            this.pnlLeftStation.Name = "pnlLeftStation";
            this.pnlLeftStation.Size = new System.Drawing.Size(110, 90);
            this.pnlLeftStation.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "DrugName";
            this.Column1.HeaderText = "药品名称";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "colDashDrugName";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "StationText";
            this.Column2.HeaderText = "目标工位";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "colDashStation";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "RequiredQty";
            this.Column3.HeaderText = "应取数量";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "colDashRequiredQty";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "GrabCount";
            this.Column4.HeaderText = "已执行抓取次数";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "colDashGrabCount";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Status";
            this.Column5.HeaderText = "当前状态";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "colDashStatus";
            this.Column5.ReadOnly = true;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Label lblLeftStation;
        private System.Windows.Forms.Panel pnlRightStation;
        private System.Windows.Forms.Panel pnlSlot;
        private System.Windows.Forms.Panel pnlLeftStation;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblArmHint;
        private System.Windows.Forms.Label lblRightStation;
        private System.Windows.Forms.Label lblSlot;
    }
}
