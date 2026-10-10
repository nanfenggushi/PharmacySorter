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
            this.tabOps = new System.Windows.Forms.TabControl();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.ucSystemAuditLog = new PharmacySorter.UcSystemAuditLog();
            this.tabPassword = new System.Windows.Forms.TabPage();
            this.grpPassword = new System.Windows.Forms.GroupBox();
            this.lblPasswordHint = new System.Windows.Forms.Label();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            this.lblOldPassword = new System.Windows.Forms.Label();
            this.tabOps.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.tabPassword.SuspendLayout();
            this.grpPassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabOps
            // 
            this.tabOps.Controls.Add(this.tabLog);
            this.tabOps.Controls.Add(this.tabPassword);
            this.tabOps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOps.Location = new System.Drawing.Point(0, 0);
            this.tabOps.Name = "tabOps";
            this.tabOps.SelectedIndex = 0;
            this.tabOps.Size = new System.Drawing.Size(1273, 721);
            this.tabOps.TabIndex = 0;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.ucSystemAuditLog);
            this.tabLog.Location = new System.Drawing.Point(4, 25);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(1265, 692);
            this.tabLog.TabIndex = 0;
            this.tabLog.Text = "系统日志";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // ucSystemAuditLog
            // 
            this.ucSystemAuditLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSystemAuditLog.Location = new System.Drawing.Point(3, 3);
            this.ucSystemAuditLog.Name = "ucSystemAuditLog";
            this.ucSystemAuditLog.Size = new System.Drawing.Size(1259, 686);
            this.ucSystemAuditLog.TabIndex = 0;
            // 
            // tabPassword
            // 
            this.tabPassword.Controls.Add(this.grpPassword);
            this.tabPassword.Location = new System.Drawing.Point(4, 25);
            this.tabPassword.Name = "tabPassword";
            this.tabPassword.Padding = new System.Windows.Forms.Padding(3);
            this.tabPassword.Size = new System.Drawing.Size(1265, 692);
            this.tabPassword.TabIndex = 1;
            this.tabPassword.Text = "修改密码";
            this.tabPassword.UseVisualStyleBackColor = true;
            // 
            // grpPassword
            // 
            this.grpPassword.Controls.Add(this.lblPasswordHint);
            this.grpPassword.Controls.Add(this.btnChangePassword);
            this.grpPassword.Controls.Add(this.txtConfirmPassword);
            this.grpPassword.Controls.Add(this.lblConfirmPassword);
            this.grpPassword.Controls.Add(this.txtNewPassword);
            this.grpPassword.Controls.Add(this.lblNewPassword);
            this.grpPassword.Controls.Add(this.txtOldPassword);
            this.grpPassword.Controls.Add(this.lblOldPassword);
            this.grpPassword.Location = new System.Drawing.Point(40, 30);
            this.grpPassword.Name = "grpPassword";
            this.grpPassword.Size = new System.Drawing.Size(425, 240);
            this.grpPassword.TabIndex = 0;
            this.grpPassword.TabStop = false;
            this.grpPassword.Text = "修改登录密码";
            // 
            // lblPasswordHint
            // 
            this.lblPasswordHint.AutoSize = true;
            this.lblPasswordHint.ForeColor = System.Drawing.Color.Gray;
            this.lblPasswordHint.Location = new System.Drawing.Point(227, 183);
            this.lblPasswordHint.Name = "lblPasswordHint";
            this.lblPasswordHint.Size = new System.Drawing.Size(153, 15);
            this.lblPasswordHint.TabIndex = 7;
            this.lblPasswordHint.Text = "密码长度 8 到 50 位";
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Location = new System.Drawing.Point(99, 175);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(110, 30);
            this.btnChangePassword.TabIndex = 6;
            this.btnChangePassword.Text = "修改密码";
            this.btnChangePassword.UseVisualStyleBackColor = true;
            this.btnChangePassword.Click += new System.EventHandler(this.BtnChangePassword_Click);
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(118, 127);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '●';
            this.txtConfirmPassword.Size = new System.Drawing.Size(240, 25);
            this.txtConfirmPassword.TabIndex = 5;
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Location = new System.Drawing.Point(20, 130);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(82, 15);
            this.lblConfirmPassword.TabIndex = 4;
            this.lblConfirmPassword.Text = "确认新密码";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(118, 82);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '●';
            this.txtNewPassword.Size = new System.Drawing.Size(240, 25);
            this.txtNewPassword.TabIndex = 3;
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Location = new System.Drawing.Point(20, 85);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(52, 15);
            this.lblNewPassword.TabIndex = 2;
            this.lblNewPassword.Text = "新密码";
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Location = new System.Drawing.Point(118, 37);
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.PasswordChar = '●';
            this.txtOldPassword.Size = new System.Drawing.Size(240, 25);
            this.txtOldPassword.TabIndex = 1;
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.AutoSize = true;
            this.lblOldPassword.Location = new System.Drawing.Point(20, 40);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(52, 15);
            this.lblOldPassword.TabIndex = 0;
            this.lblOldPassword.Text = "原密码";
            // 
            // UcSystemOps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabOps);
            this.Name = "UcSystemOps";
            this.Size = new System.Drawing.Size(1273, 721);
            this.tabOps.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tabPassword.ResumeLayout(false);
            this.grpPassword.ResumeLayout(false);
            this.grpPassword.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabOps;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TabPage tabPassword;
        private UcSystemAuditLog ucSystemAuditLog;
        private System.Windows.Forms.GroupBox grpPassword;
        private System.Windows.Forms.TextBox txtOldPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Label lblOldPassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.Label lblPasswordHint;
        private System.Windows.Forms.Button btnChangePassword;
    }
}
