namespace PharmacySorter
{
    partial class UcDrugDictionary
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.dgvMedicineInfo = new System.Windows.Forms.DataGridView();
            this.lblDrugList = new System.Windows.Forms.Label();
            this.lblDrugId = new System.Windows.Forms.Label();
            this.txtMedicineId = new System.Windows.Forms.TextBox();
            this.lblDrugName = new System.Windows.Forms.Label();
            this.txtDrugName = new System.Windows.Forms.TextBox();
            this.lblSpec = new System.Windows.Forms.Label();
            this.txtSpec = new System.Windows.Forms.TextBox();
            this.lblKeyword = new System.Windows.Forms.Label();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblStation = new System.Windows.Forms.Label();
            this.cmbCurrentWorkstation = new System.Windows.Forms.ComboBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicineInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(940, 88);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(82, 31);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(940, 36);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 31);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存修改";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.Button2_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(940, 140);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(82, 31);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "停用";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.Button3_Click);
            // 
            // dgvMedicineInfo
            // 
            this.dgvMedicineInfo.AllowUserToAddRows = false;
            this.dgvMedicineInfo.AllowUserToDeleteRows = false;
            this.dgvMedicineInfo.AutoGenerateColumns = false;
            this.dgvMedicineInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMedicineInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMedicineInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedicineInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dgvMedicineInfo.Location = new System.Drawing.Point(24, 250);
            this.dgvMedicineInfo.MultiSelect = false;
            this.dgvMedicineInfo.Name = "dgvMedicineInfo";
            this.dgvMedicineInfo.ReadOnly = true;
            this.dgvMedicineInfo.RowHeadersVisible = false;
            this.dgvMedicineInfo.RowHeadersWidth = 51;
            this.dgvMedicineInfo.RowTemplate.Height = 27;
            this.dgvMedicineInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMedicineInfo.Size = new System.Drawing.Size(1014, 414);
            this.dgvMedicineInfo.TabIndex = 3;
            // 
            // lblDrugList
            // 
            this.lblDrugList.AutoSize = true;
            this.lblDrugList.Location = new System.Drawing.Point(24, 220);
            this.lblDrugList.Name = "lblDrugList";
            this.lblDrugList.Size = new System.Drawing.Size(97, 15);
            this.lblDrugList.TabIndex = 4;
            this.lblDrugList.Text = "药品列表";
            // 
            // lblDrugId
            // 
            this.lblDrugId.AutoSize = true;
            this.lblDrugId.Location = new System.Drawing.Point(24, 48);
            this.lblDrugId.Name = "lblDrugId";
            this.lblDrugId.Size = new System.Drawing.Size(67, 15);
            this.lblDrugId.TabIndex = 5;
            this.lblDrugId.Text = "药品编号";
            // 
            // txtMedicineId
            // 
            this.txtMedicineId.Location = new System.Drawing.Point(120, 42);
            this.txtMedicineId.Name = "txtMedicineId";
            this.txtMedicineId.ReadOnly = true;
            this.txtMedicineId.Size = new System.Drawing.Size(165, 25);
            this.txtMedicineId.TabIndex = 6;
            // 
            // lblDrugName
            // 
            this.lblDrugName.AutoSize = true;
            this.lblDrugName.Location = new System.Drawing.Point(24, 112);
            this.lblDrugName.Name = "lblDrugName";
            this.lblDrugName.Size = new System.Drawing.Size(67, 15);
            this.lblDrugName.TabIndex = 7;
            this.lblDrugName.Text = "药品名称";
            // 
            // txtDrugName
            // 
            this.txtDrugName.Location = new System.Drawing.Point(120, 106);
            this.txtDrugName.MaxLength = 100;
            this.txtDrugName.Name = "txtDrugName";
            this.txtDrugName.Size = new System.Drawing.Size(165, 25);
            this.txtDrugName.TabIndex = 8;
            // 
            // lblSpec
            // 
            this.lblSpec.AutoSize = true;
            this.lblSpec.Location = new System.Drawing.Point(340, 112);
            this.lblSpec.Name = "lblSpec";
            this.lblSpec.Size = new System.Drawing.Size(37, 15);
            this.lblSpec.TabIndex = 9;
            this.lblSpec.Text = "规格";
            // 
            // txtSpec
            // 
            this.txtSpec.Location = new System.Drawing.Point(450, 106);
            this.txtSpec.MaxLength = 100;
            this.txtSpec.Name = "txtSpec";
            this.txtSpec.Size = new System.Drawing.Size(165, 25);
            this.txtSpec.TabIndex = 10;
            // 
            // lblKeyword
            // 
            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Location = new System.Drawing.Point(24, 176);
            this.lblKeyword.Name = "lblKeyword";
            this.lblKeyword.Size = new System.Drawing.Size(52, 15);
            this.lblKeyword.TabIndex = 12;
            this.lblKeyword.Text = "关键字";
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(120, 170);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(330, 25);
            this.txtKeyword.TabIndex = 13;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(470, 166);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(82, 31);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // lblStation
            // 
            this.lblStation.AutoSize = true;
            this.lblStation.Location = new System.Drawing.Point(340, 48);
            this.lblStation.Name = "lblStation";
            this.lblStation.Size = new System.Drawing.Size(97, 15);
            this.lblStation.TabIndex = 15;
            this.lblStation.Text = "绑定工位";
            // 
            // cmbCurrentWorkstation
            // 
            this.cmbCurrentWorkstation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrentWorkstation.FormattingEnabled = true;
            this.cmbCurrentWorkstation.Location = new System.Drawing.Point(450, 42);
            this.cmbCurrentWorkstation.Name = "cmbCurrentWorkstation";
            this.cmbCurrentWorkstation.Size = new System.Drawing.Size(165, 23);
            this.cmbCurrentWorkstation.TabIndex = 16;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "DrugId";
            this.Column1.HeaderText = "药品编号";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "colDrugId";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "DrugName";
            this.Column2.HeaderText = "药品名称";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "colDrugName";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Spec";
            this.Column3.HeaderText = "规格";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "colSpec";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "StationText";
            this.Column4.HeaderText = "绑定工位";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "colStationText";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "StatusText";
            this.Column5.HeaderText = "状态";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "colIsActive";
            this.Column5.ReadOnly = true;
            // 
            // UcDrugDictionary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbCurrentWorkstation);
            this.Controls.Add(this.lblStation);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.lblKeyword);
            this.Controls.Add(this.txtSpec);
            this.Controls.Add(this.lblSpec);
            this.Controls.Add(this.txtDrugName);
            this.Controls.Add(this.lblDrugName);
            this.Controls.Add(this.txtMedicineId);
            this.Controls.Add(this.lblDrugId);
            this.Controls.Add(this.lblDrugList);
            this.Controls.Add(this.dgvMedicineInfo);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Name = "UcDrugDictionary";
            this.Size = new System.Drawing.Size(1062, 688);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicineInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.DataGridView dgvMedicineInfo;
        private System.Windows.Forms.Label lblDrugList;
        private System.Windows.Forms.Label lblDrugId;
        private System.Windows.Forms.TextBox txtMedicineId;
        private System.Windows.Forms.Label lblDrugName;
        private System.Windows.Forms.TextBox txtDrugName;
        private System.Windows.Forms.Label lblSpec;
        private System.Windows.Forms.TextBox txtSpec;
        private System.Windows.Forms.Label lblKeyword;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblStation;
        private System.Windows.Forms.ComboBox cmbCurrentWorkstation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}
