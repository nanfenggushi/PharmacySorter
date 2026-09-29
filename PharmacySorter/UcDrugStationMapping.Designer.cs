namespace PharmacySorter
{
    partial class UcDrugStationMapping
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
            this.grpStationAction = new System.Windows.Forms.GroupBox();
            this.btnSaveStation = new System.Windows.Forms.Button();
            this.dgvStation = new System.Windows.Forms.DataGridView();
            this.colStationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGrabCommand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDropCommand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstTimeMs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpStationAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStation)).BeginInit();
            this.SuspendLayout();
            // 
            // grpStationAction
            // 
            this.grpStationAction.Controls.Add(this.btnSaveStation);
            this.grpStationAction.Controls.Add(this.dgvStation);
            this.grpStationAction.Location = new System.Drawing.Point(43, 12);
            this.grpStationAction.Name = "grpStationAction";
            this.grpStationAction.Size = new System.Drawing.Size(951, 674);
            this.grpStationAction.TabIndex = 0;
            this.grpStationAction.TabStop = false;
            this.grpStationAction.Text = "工位指令";
            // 
            // btnSaveStation
            // 
            this.btnSaveStation.Location = new System.Drawing.Point(816, 595);
            this.btnSaveStation.Name = "btnSaveStation";
            this.btnSaveStation.Size = new System.Drawing.Size(82, 31);
            this.btnSaveStation.TabIndex = 1;
            this.btnSaveStation.Text = "保存指令";
            this.btnSaveStation.UseVisualStyleBackColor = true;
            this.btnSaveStation.Click += new System.EventHandler(this.BtnSaveStation_Click);
            // 
            // dgvStation
            // 
            this.dgvStation.AllowUserToAddRows = false;
            this.dgvStation.AllowUserToDeleteRows = false;
            this.dgvStation.AutoGenerateColumns = false;
            this.dgvStation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStation.ColumnHeadersHeight = 32;
            this.dgvStation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvStation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStationName,
            this.colGrabCommand,
            this.colDropCommand,
            this.colEstTimeMs});
            this.dgvStation.Location = new System.Drawing.Point(47, 37);
            this.dgvStation.MultiSelect = false;
            this.dgvStation.Name = "dgvStation";
            this.dgvStation.RowHeadersVisible = false;
            this.dgvStation.RowHeadersWidth = 51;
            this.dgvStation.RowTemplate.Height = 27;
            this.dgvStation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStation.Size = new System.Drawing.Size(851, 536);
            this.dgvStation.TabIndex = 0;
            //
            // colStationName
            //
            this.colStationName.HeaderText = "工位名称";
            this.colStationName.MinimumWidth = 6;
            this.colStationName.Name = "colStationName";
            this.colStationName.ReadOnly = true;
            //
            // colGrabCommand
            //
            this.colGrabCommand.HeaderText = "抓取指令";
            this.colGrabCommand.MinimumWidth = 6;
            this.colGrabCommand.Name = "colGrabCommand";
            //
            // colDropCommand
            //
            this.colDropCommand.HeaderText = "放置指令";
            this.colDropCommand.MinimumWidth = 6;
            this.colDropCommand.Name = "colDropCommand";
            //
            // colEstTimeMs
            //
            this.colEstTimeMs.HeaderText = "动作等待(毫秒)";
            this.colEstTimeMs.MinimumWidth = 6;
            this.colEstTimeMs.Name = "colEstTimeMs";
            // 
            // UcDrugStationMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpStationAction);
            this.Name = "UcDrugStationMapping";
            this.Size = new System.Drawing.Size(1073, 718);
            this.grpStationAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpStationAction;
        private System.Windows.Forms.Button btnSaveStation;
        private System.Windows.Forms.DataGridView dgvStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGrabCommand;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDropCommand;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstTimeMs;
    }
}

