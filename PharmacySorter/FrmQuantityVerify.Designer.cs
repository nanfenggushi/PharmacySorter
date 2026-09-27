namespace PharmacySorter
{
    partial class FrmQuantityVerify
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDrug = new System.Windows.Forms.Label();
            this.lblRequiredQty = new System.Windows.Forms.Label();
            this.lblActualQty = new System.Windows.Forms.Label();
            this.nudActualQty = new System.Windows.Forms.NumericUpDown();
            this.lblMatchIcon = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.lblMatch = new System.Windows.Forms.Label();
            this.lblShortageIcon = new System.Windows.Forms.Label();
            this.lblShortage = new System.Windows.Forms.Label();
            this.btnRefill = new System.Windows.Forms.Button();
            this.lblExcess = new System.Windows.Forms.Label();
            this.btnCommitQty = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.chkExcessCleared = new System.Windows.Forms.CheckBox();
            this.pnlAlert = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.nudActualQty)).BeginInit();
            this.pnlAlert.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDrug
            // 
            this.lblDrug.Font = new System.Drawing.Font("Microsoft YaHei UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblDrug.Location = new System.Drawing.Point(24, 58);
            this.lblDrug.Name = "lblDrug";
            this.lblDrug.Size = new System.Drawing.Size(620, 36);
            this.lblDrug.TabIndex = 0;
            this.lblDrug.Text = "请清点分拣槽内的药品";
            // 
            // lblRequiredQty
            // 
            this.lblRequiredQty.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.lblRequiredQty.Location = new System.Drawing.Point(28, 108);
            this.lblRequiredQty.Name = "lblRequiredQty";
            this.lblRequiredQty.Size = new System.Drawing.Size(360, 28);
            this.lblRequiredQty.TabIndex = 1;
            this.lblRequiredQty.Text = "处方应发数量：[ 0 ] 盒";
            // 
            // lblActualQty
            // 
            this.lblActualQty.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.lblActualQty.Location = new System.Drawing.Point(28, 162);
            this.lblActualQty.Name = "lblActualQty";
            this.lblActualQty.Size = new System.Drawing.Size(110, 28);
            this.lblActualQty.TabIndex = 2;
            this.lblActualQty.Text = "实收数量：";
            // 
            // nudActualQty
            // 
            this.nudActualQty.Font = new System.Drawing.Font("Microsoft YaHei UI", 16F);
            this.nudActualQty.Location = new System.Drawing.Point(150, 154);
            this.nudActualQty.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            this.nudActualQty.Name = "nudActualQty";
            this.nudActualQty.Size = new System.Drawing.Size(120, 42);
            this.nudActualQty.TabIndex = 3;
            this.nudActualQty.ValueChanged += new System.EventHandler(this.nudActualQty_ValueChanged);
            // 
            // lblMatchIcon
            // 
            this.lblMatchIcon.Font = new System.Drawing.Font("Microsoft YaHei UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblMatchIcon.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblMatchIcon.Location = new System.Drawing.Point(16, 12);
            this.lblMatchIcon.Name = "lblMatchIcon";
            this.lblMatchIcon.Size = new System.Drawing.Size(36, 32);
            this.lblMatchIcon.TabIndex = 4;
            this.lblMatchIcon.Text = "√";
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.SeaGreen;
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(430, 400);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(210, 42);
            this.btnConfirm.TabIndex = 8;
            this.btnConfirm.Text = "确认并继续";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // lblMatch
            // 
            this.lblMatch.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMatch.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblMatch.Location = new System.Drawing.Point(58, 14);
            this.lblMatch.Name = "lblMatch";
            this.lblMatch.Size = new System.Drawing.Size(520, 28);
            this.lblMatch.TabIndex = 5;
            this.lblMatch.Text = "数量一致";
            // 
            // lblShortageIcon
            // 
            this.lblShortageIcon.Font = new System.Drawing.Font("Microsoft YaHei UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblShortageIcon.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblShortageIcon.Location = new System.Drawing.Point(16, 12);
            this.lblShortageIcon.Name = "lblShortageIcon";
            this.lblShortageIcon.Size = new System.Drawing.Size(36, 32);
            this.lblShortageIcon.TabIndex = 6;
            this.lblShortageIcon.Text = "!";
            // 
            // lblShortage
            // 
            this.lblShortage.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblShortage.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblShortage.Location = new System.Drawing.Point(58, 14);
            this.lblShortage.Name = "lblShortage";
            this.lblShortage.Size = new System.Drawing.Size(520, 28);
            this.lblShortage.TabIndex = 7;
            this.lblShortage.Text = "数量短缺";
            // 
            // btnRefill
            // 
            this.btnRefill.BackColor = System.Drawing.Color.DarkOrange;
            this.btnRefill.ForeColor = System.Drawing.Color.White;
            this.btnRefill.Location = new System.Drawing.Point(250, 400);
            this.btnRefill.Name = "btnRefill";
            this.btnRefill.Size = new System.Drawing.Size(390, 42);
            this.btnRefill.TabIndex = 9;
            this.btnRefill.Text = "让机械臂自动补抓";
            this.btnRefill.UseVisualStyleBackColor = false;
            this.btnRefill.Click += new System.EventHandler(this.btnRefill_Click);
            // 
            // lblExcess
            // 
            this.lblExcess.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblExcess.ForeColor = System.Drawing.Color.Firebrick;
            this.lblExcess.Location = new System.Drawing.Point(16, 12);
            this.lblExcess.Name = "lblExcess";
            this.lblExcess.Size = new System.Drawing.Size(560, 28);
            this.lblExcess.TabIndex = 10;
            this.lblExcess.Text = "超量抓取！请物理剔除";
            // 
            // btnCommitQty
            // 
            this.btnCommitQty.Location = new System.Drawing.Point(290, 158);
            this.btnCommitQty.Name = "btnCommitQty";
            this.btnCommitQty.Size = new System.Drawing.Size(90, 36);
            this.btnCommitQty.TabIndex = 4;
            this.btnCommitQty.Text = "清点完成";
            this.btnCommitQty.UseVisualStyleBackColor = true;
            this.btnCommitQty.Visible = false;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(24, 406);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(210, 42);
            this.btnReset.TabIndex = 11;
            this.btnReset.Text = "该明细作废，重新抓取";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            //
            // chkExcessCleared
            //
            this.chkExcessCleared.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.chkExcessCleared.Location = new System.Drawing.Point(16, 78);
            this.chkExcessCleared.Name = "chkExcessCleared";
            this.chkExcessCleared.Size = new System.Drawing.Size(600, 48);
            this.chkExcessCleared.TabIndex = 12;
            this.chkExcessCleared.Text = "我已手动从分拣槽拿走多余药品并放回原处";
            this.chkExcessCleared.CheckedChanged += new System.EventHandler(this.chkExcessCleared_CheckedChanged);
            //
            // pnlAlert
            //
            this.pnlAlert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAlert.Controls.Add(this.lblMatchIcon);
            this.pnlAlert.Controls.Add(this.lblMatch);
            this.pnlAlert.Controls.Add(this.lblShortageIcon);
            this.pnlAlert.Controls.Add(this.lblShortage);
            this.pnlAlert.Controls.Add(this.lblExcess);
            this.pnlAlert.Controls.Add(this.chkExcessCleared);
            this.pnlAlert.Location = new System.Drawing.Point(24, 230);
            this.pnlAlert.Name = "pnlAlert";
            this.pnlAlert.Size = new System.Drawing.Size(640, 150);
            this.pnlAlert.TabIndex = 13;
            // 
            // FrmQuantityVerify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightYellow;
            this.ClientSize = new System.Drawing.Size(690, 470);
            this.ControlBox = false;
            this.Controls.Add(this.pnlAlert);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnRefill);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.nudActualQty);
            this.Controls.Add(this.lblActualQty);
            this.Controls.Add(this.lblRequiredQty);
            this.Controls.Add(this.lblDrug);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmQuantityVerify";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "人工数量复核";
            this.pnlAlert.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudActualQty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDrug;
        private System.Windows.Forms.Label lblRequiredQty;
        private System.Windows.Forms.Label lblActualQty;
        private System.Windows.Forms.NumericUpDown nudActualQty;
        private System.Windows.Forms.Label lblMatchIcon;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label lblMatch;
        private System.Windows.Forms.Label lblShortageIcon;
        private System.Windows.Forms.Label lblShortage;
        private System.Windows.Forms.Button btnRefill;
        private System.Windows.Forms.Label lblExcess;
        private System.Windows.Forms.Button btnCommitQty;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox chkExcessCleared;
        private System.Windows.Forms.Panel pnlAlert;
    }
}