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
            this.grpDrugBinding = new System.Windows.Forms.GroupBox();
            this.btnSaveBinding = new System.Windows.Forms.Button();
            this.dgvDrugBinding = new System.Windows.Forms.DataGridView();
            this.colDrugName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBindStation = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.grpStationAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStation)).BeginInit();
            this.grpDrugBinding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrugBinding)).BeginInit();
            this.SuspendLayout();
            // 
            // grpStationAction
            // 
            this.grpStationAction.Controls.Add(this.btnSaveStation);
            this.grpStationAction.Controls.Add(this.dgvStation);
            this.grpStationAction.Location = new System.Drawing.Point(43, 12);
            this.grpStationAction.Name = "grpStationAction";
            this.grpStationAction.Size = new System.Drawing.Size(951, 319);
            this.grpStationAction.TabIndex = 0;
            this.grpStationAction.TabStop = false;
            this.grpStationAction.Text = "工位指令配置表";
            // 
            // btnSaveStation
            // 
            this.btnSaveStation.Location = new System.Drawing.Point(816, 272);
            this.btnSaveStation.Name = "btnSaveStation";
            this.btnSaveStation.Size = new System.Drawing.Size(82, 31);
            this.btnSaveStation.TabIndex = 1;
            this.btnSaveStation.Text = "保存工位";
            this.btnSaveStation.UseVisualStyleBackColor = true;
            this.btnSaveStation.Click += new System.EventHandler(this.BtnSaveStation_Click);
            // 
            // dgvStation
            // 
            this.dgvStation.AllowUserToAddRows = false;
            this.dgvStation.AllowUserToDeleteRows = false;
            this.dgvStation.AutoGenerateColumns = false;
            this.dgvStation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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
            this.dgvStation.Size = new System.Drawing.Size(851, 220);
            this.dgvStation.TabIndex = 0;
            // 
            // colStationName
            // 
            this.colStationName.DataPropertyName = "StationName";
            this.colStationName.HeaderText = "工位名称";
            this.colStationName.MinimumWidth = 6;
            this.colStationName.Name = "colStationName";
            this.colStationName.ReadOnly = true;
            // 
            // colGrabCommand
            // 
            this.colGrabCommand.DataPropertyName = "GrabCommand";
            this.colGrabCommand.HeaderText = "抓取触发字符串";
            this.colGrabCommand.MaxInputLength = 100;
            this.colGrabCommand.MinimumWidth = 6;
            this.colGrabCommand.Name = "colGrabCommand";
            // 
            // colDropCommand
            // 
            this.colDropCommand.DataPropertyName = "DropCommand";
            this.colDropCommand.HeaderText = "放置触发字符串";
            this.colDropCommand.MaxInputLength = 100;
            this.colDropCommand.MinimumWidth = 6;
            this.colDropCommand.Name = "colDropCommand";
            // 
            // colEstTimeMs
            // 
            this.colEstTimeMs.DataPropertyName = "EstTimeMs";
            this.colEstTimeMs.HeaderText = "延时设定(毫秒)";
            this.colEstTimeMs.MaxInputLength = 5;
            this.colEstTimeMs.MinimumWidth = 6;
            this.colEstTimeMs.Name = "colEstTimeMs";
            // 
            // grpDrugBinding
            // 
            this.grpDrugBinding.Controls.Add(this.btnSaveBinding);
            this.grpDrugBinding.Controls.Add(this.dgvDrugBinding);
            this.grpDrugBinding.Location = new System.Drawing.Point(43, 364);
            this.grpDrugBinding.Name = "grpDrugBinding";
            this.grpDrugBinding.Size = new System.Drawing.Size(951, 339);
            this.grpDrugBinding.TabIndex = 1;
            this.grpDrugBinding.TabStop = false;
            this.grpDrugBinding.Text = "药品绑定设置";
            // 
            // btnSaveBinding
            // 
            this.btnSaveBinding.Location = new System.Drawing.Point(816, 292);
            this.btnSaveBinding.Name = "btnSaveBinding";
            this.btnSaveBinding.Size = new System.Drawing.Size(82, 31);
            this.btnSaveBinding.TabIndex = 1;
            this.btnSaveBinding.Text = "保存绑定";
            this.btnSaveBinding.UseVisualStyleBackColor = true;
            this.btnSaveBinding.Click += new System.EventHandler(this.BtnSaveBinding_Click);
            // 
            // dgvDrugBinding
            // 
            this.dgvDrugBinding.AllowUserToAddRows = false;
            this.dgvDrugBinding.AllowUserToDeleteRows = false;
            this.dgvDrugBinding.AutoGenerateColumns = false;
            this.dgvDrugBinding.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDrugBinding.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDrugBinding.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDrugName,
            this.colBindStation});
            this.dgvDrugBinding.Location = new System.Drawing.Point(47, 37);
            this.dgvDrugBinding.MultiSelect = false;
            this.dgvDrugBinding.Name = "dgvDrugBinding";
            this.dgvDrugBinding.RowHeadersVisible = false;
            this.dgvDrugBinding.RowHeadersWidth = 51;
            this.dgvDrugBinding.RowTemplate.Height = 27;
            this.dgvDrugBinding.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDrugBinding.Size = new System.Drawing.Size(851, 240);
            this.dgvDrugBinding.TabIndex = 0;
            // 
            // colDrugName
            // 
            this.colDrugName.DataPropertyName = "DrugName";
            this.colDrugName.HeaderText = "药品名称";
            this.colDrugName.MinimumWidth = 6;
            this.colDrugName.Name = "colDrugName";
            this.colDrugName.ReadOnly = true;
            // 
            // colBindStation
            // 
            this.colBindStation.DataPropertyName = "StationId";
            this.colBindStation.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.colBindStation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colBindStation.HeaderText = "绑定工位";
            this.colBindStation.MinimumWidth = 6;
            this.colBindStation.Name = "colBindStation";
            // 
            // UcDrugStationMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpDrugBinding);
            this.Controls.Add(this.grpStationAction);
            this.Name = "UcDrugStationMapping";
            this.Size = new System.Drawing.Size(1073, 718);
            this.grpStationAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStation)).EndInit();
            this.grpDrugBinding.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrugBinding)).EndInit();
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
        private System.Windows.Forms.GroupBox grpDrugBinding;
        private System.Windows.Forms.Button btnSaveBinding;
        private System.Windows.Forms.DataGridView dgvDrugBinding;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDrugName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colBindStation;
    }
}
