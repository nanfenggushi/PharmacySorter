namespace PharmacySorter
{
    partial class UcDrugStation
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
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.ucDrugDictionary = new PharmacySorter.UcDrugDictionary();
            this.ucDrugStationMapping = new PharmacySorter.UcDrugStationMapping();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitMain.Panel1.Controls.Add(this.ucDrugDictionary);
            this.splitMain.Panel1MinSize = 240;
            this.splitMain.Panel2.Controls.Add(this.ucDrugStationMapping);
            this.splitMain.Panel2MinSize = 120;
            this.splitMain.Size = new System.Drawing.Size(1273, 721);
            this.splitMain.SplitterDistance = 430;
            this.splitMain.TabIndex = 0;
            // 
            // ucDrugDictionary
            // 
            this.ucDrugDictionary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDrugDictionary.Location = new System.Drawing.Point(0, 0);
            this.ucDrugDictionary.Name = "ucDrugDictionary";
            this.ucDrugDictionary.Size = new System.Drawing.Size(1271, 430);
            this.ucDrugDictionary.TabIndex = 0;
            // 
            // ucDrugStationMapping
            // 
            this.ucDrugStationMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDrugStationMapping.Location = new System.Drawing.Point(0, 0);
            this.ucDrugStationMapping.Name = "ucDrugStationMapping";
            this.ucDrugStationMapping.Size = new System.Drawing.Size(1271, 287);
            this.ucDrugStationMapping.TabIndex = 0;
            // 
            // UcDrugStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitMain);
            this.Name = "UcDrugStation";
            this.Size = new System.Drawing.Size(1273, 721);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitMain;
        private UcDrugDictionary ucDrugDictionary;
        private UcDrugStationMapping ucDrugStationMapping;
    }
}
