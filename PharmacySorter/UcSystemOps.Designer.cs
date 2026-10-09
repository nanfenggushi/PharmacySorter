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
            this.ucSystemAuditLog = new PharmacySorter.UcSystemAuditLog();
            this.SuspendLayout();
            //
            // ucSystemAuditLog
            //
            this.ucSystemAuditLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSystemAuditLog.Location = new System.Drawing.Point(0, 0);
            this.ucSystemAuditLog.Name = "ucSystemAuditLog";
            this.ucSystemAuditLog.Size = new System.Drawing.Size(1273, 721);
            this.ucSystemAuditLog.TabIndex = 0;
            //
            // UcSystemOps
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucSystemAuditLog);
            this.Name = "UcSystemOps";
            this.Size = new System.Drawing.Size(1273, 721);
            this.ResumeLayout(false);
        }

        #endregion

        private UcSystemAuditLog ucSystemAuditLog;
    }
}
