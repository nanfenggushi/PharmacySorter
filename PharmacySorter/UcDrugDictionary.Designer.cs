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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMedicineId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDrugName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSpec = new System.Windows.Forms.TextBox();
            this.lblKeyword = new System.Windows.Forms.Label();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbCurrentWorkstation = new System.Windows.Forms.ComboBox();
            this.colDrugId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDrugName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStationText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicineInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(853, 155);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(82, 31);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(853, 40);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 31);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存修改";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(853, 98);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(82, 31);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "停用";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.button3_Click);
            // 
            // dgvMedicineInfo
            // 
            this.dgvMedicineInfo.AllowUserToAddRows = false;
            this.dgvMedicineInfo.AllowUserToDeleteRows = false;
            this.dgvMedicineInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMedicineInfo.AutoGenerateColumns = false;
            this.dgvMedicineInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedicineInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDrugId,
            this.colDrugName,
            this.colSpec,
            this.colStationText,
            this.colIsActive});
            this.dgvMedicineInfo.Location = new System.Drawing.Point(51, 262);
            this.dgvMedicineInfo.MultiSelect = false;
            this.dgvMedicineInfo.Name = "dgvMedicineInfo";
            this.dgvMedicineInfo.ReadOnly = true;
            this.dgvMedicineInfo.RowHeadersVisible = false;
            this.dgvMedicineInfo.RowHeadersWidth = 51;
            this.dgvMedicineInfo.RowTemplate.Height = 27;
            this.dgvMedicineInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMedicineInfo.Size = new System.Drawing.Size(962, 397);
            this.dgvMedicineInfo.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "所有药品信息";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "药品编号";
            // 
            // txtMedicineId
            // 
            this.txtMedicineId.Location = new System.Drawing.Point(131, 41);
            this.txtMedicineId.Name = "txtMedicineId";
            this.txtMedicineId.ReadOnly = true;
            this.txtMedicineId.Size = new System.Drawing.Size(165, 25);
            this.txtMedicineId.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "药品名称";
            // 
            // txtDrugName
            // 
            this.txtDrugName.Location = new System.Drawing.Point(131, 106);
            this.txtDrugName.MaxLength = 100;
            this.txtDrugName.Name = "txtDrugName";
            this.txtDrugName.Size = new System.Drawing.Size(165, 25);
            this.txtDrugName.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(356, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "规格";
            // 
            // txtSpec
            // 
            this.txtSpec.Location = new System.Drawing.Point(474, 104);
            this.txtSpec.MaxLength = 100;
            this.txtSpec.Name = "txtSpec";
            this.txtSpec.Size = new System.Drawing.Size(165, 25);
            this.txtSpec.TabIndex = 10;
            // 
            // lblKeyword
            // 
            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Location = new System.Drawing.Point(49, 196);
            this.lblKeyword.Name = "lblKeyword";
            this.lblKeyword.Size = new System.Drawing.Size(52, 15);
            this.lblKeyword.TabIndex = 12;
            this.lblKeyword.Text = "关键字";
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(131, 191);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(330, 25);
            this.txtKeyword.TabIndex = 13;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(474, 186);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(82, 31);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(356, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 15);
            this.label5.TabIndex = 15;
            this.label5.Text = "当前绑定工位";
            // 
            // cmbCurrentWorkstation
            // 
            this.cmbCurrentWorkstation.FormattingEnabled = true;
            this.cmbCurrentWorkstation.Location = new System.Drawing.Point(474, 43);
            this.cmbCurrentWorkstation.Name = "cmbCurrentWorkstation";
            this.cmbCurrentWorkstation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrentWorkstation.Size = new System.Drawing.Size(165, 23);
            this.cmbCurrentWorkstation.TabIndex = 16;
            // 
            // colDrugId
            // 
            this.colDrugId.DataPropertyName = "DrugId";
            this.colDrugId.HeaderText = "药品编号";
            this.colDrugId.MinimumWidth = 6;
            this.colDrugId.Name = "colDrugId";
            this.colDrugId.ReadOnly = true;
            // 
            // colDrugName
            // 
            this.colDrugName.DataPropertyName = "DrugName";
            this.colDrugName.HeaderText = "药品名称";
            this.colDrugName.MinimumWidth = 6;
            this.colDrugName.Name = "colDrugName";
            this.colDrugName.ReadOnly = true;
            // 
            // colSpec
            // 
            this.colSpec.DataPropertyName = "Spec";
            this.colSpec.HeaderText = "规格";
            this.colSpec.MinimumWidth = 6;
            this.colSpec.Name = "colSpec";
            this.colSpec.ReadOnly = true;
            // 
            // colStationText
            // 
            this.colStationText.DataPropertyName = "StationText";
            this.colStationText.HeaderText = "当前绑定工位";
            this.colStationText.MinimumWidth = 6;
            this.colStationText.Name = "colStationText";
            this.colStationText.ReadOnly = true;
            // 
            // colIsActive
            // 
            this.colIsActive.DataPropertyName = "StatusText";
            this.colIsActive.HeaderText = "是否启用";
            this.colIsActive.MinimumWidth = 6;
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.ReadOnly = true;
            // 
            // UcDrugDictionary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbCurrentWorkstation);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.lblKeyword);
            this.Controls.Add(this.txtSpec);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDrugName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMedicineId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMedicineId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDrugName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSpec;
        private System.Windows.Forms.Label lblKeyword;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbCurrentWorkstation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDrugId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDrugName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStationText;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsActive;
    }
}