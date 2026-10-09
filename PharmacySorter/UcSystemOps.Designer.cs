namespace PharmacySorter
{
    partial class UcSystemOps
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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabUserAdmin = new System.Windows.Forms.TabPage();
            this.ucUserAdmin = new PharmacySorter.UcUserAdmin();
            this.tabAuditLog = new System.Windows.Forms.TabPage();
            this.ucSystemAuditLog = new PharmacySorter.UcSystemAuditLog();
            this.tabMain.SuspendLayout();
            this.tabUserAdmin.SuspendLayout();
            this.tabAuditLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabUserAdmin);
            this.tabMain.Controls.Add(this.tabAuditLog);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1273, 721);
            this.tabMain.TabIndex = 0;
            // 
            // tabUserAdmin
            // 
            this.tabUserAdmin.Controls.Add(this.ucUserAdmin);
            this.tabUserAdmin.Location = new System.Drawing.Point(4, 25);
            this.tabUserAdmin.Name = "tabUserAdmin";
            this.tabUserAdmin.Padding = new System.Windows.Forms.Padding(3);
            this.tabUserAdmin.Size = new System.Drawing.Size(1265, 692);
            this.tabUserAdmin.TabIndex = 0;
            this.tabUserAdmin.Text = "人员权限";
            this.tabUserAdmin.UseVisualStyleBackColor = true;
            // 
            // ucUserAdmin
            // 
            this.ucUserAdmin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucUserAdmin.Location = new System.Drawing.Point(3, 3);
            this.ucUserAdmin.Name = "ucUserAdmin";
            this.ucUserAdmin.Size = new System.Drawing.Size(1259, 686);
            this.ucUserAdmin.TabIndex = 0;
            // 
            // tabAuditLog
            // 
            this.tabAuditLog.Controls.Add(this.ucSystemAuditLog);
            this.tabAuditLog.Location = new System.Drawing.Point(4, 25);
            this.tabAuditLog.Name = "tabAuditLog";
            this.tabAuditLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabAuditLog.Size = new System.Drawing.Size(1265, 692);
            this.tabAuditLog.TabIndex = 1;
            this.tabAuditLog.Text = "系统日志";
            this.tabAuditLog.UseVisualStyleBackColor = true;
            // 
            // ucSystemAuditLog
            // 
            this.ucSystemAuditLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSystemAuditLog.Location = new System.Drawing.Point(3, 3);
            this.ucSystemAuditLog.Name = "ucSystemAuditLog";
            this.ucSystemAuditLog.Size = new System.Drawing.Size(1259, 686);
            this.ucSystemAuditLog.TabIndex = 0;
            // 
            // UcSystemOps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabMain);
            this.Name = "UcSystemOps";
            this.Size = new System.Drawing.Size(1273, 721);
            this.tabMain.ResumeLayout(false);
            this.tabUserAdmin.ResumeLayout(false);
            this.tabAuditLog.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabUserAdmin;
        private UcUserAdmin ucUserAdmin;
        private System.Windows.Forms.TabPage tabAuditLog;
        private UcSystemAuditLog ucSystemAuditLog;
    }
}
