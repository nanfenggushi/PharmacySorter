namespace PharmacySorter
{
    partial class UcPrescriptionCatalog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblDrug = new System.Windows.Forms.Label();
            this.cmbDrug = new System.Windows.Forms.ComboBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.colDrugName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblList = new System.Windows.Forms.Label();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrescriptionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(24, 28);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(67, 15);
            this.lblName.Text = "处方名称";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(110, 22);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(260, 25);
            // 
            // lblDrug
            // 
            this.lblDrug.AutoSize = true;
            this.lblDrug.Location = new System.Drawing.Point(24, 72);
            this.lblDrug.Name = "lblDrug";
            this.lblDrug.Size = new System.Drawing.Size(67, 15);
            this.lblDrug.Text = "药品";
            // 
            // cmbDrug
            // 
            this.cmbDrug.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDrug.Location = new System.Drawing.Point(110, 68);
            this.cmbDrug.Name = "cmbDrug";
            this.cmbDrug.Size = new System.Drawing.Size(260, 23);
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(390, 72);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(37, 15);
            this.lblQuantity.Text = "数量";
            // 
            // nudQuantity
            // 
            this.nudQuantity.Location = new System.Drawing.Point(440, 68);
            this.nudQuantity.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            this.nudQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(80, 25);
            this.nudQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(540, 64);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(90, 31);
            this.btnAddItem.Text = "加入药品";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.BtnAddItem_Click);
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.Location = new System.Drawing.Point(640, 64);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(90, 31);
            this.btnRemoveItem.Text = "移除药品";
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            this.btnRemoveItem.Click += new System.EventHandler(this.BtnRemoveItem_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(760, 18);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(90, 31);
            this.btnNew.Text = "新建";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(860, 18);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 31);
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(960, 18);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 31);
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AutoGenerateColumns = false;
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDrugName,
            this.colSpec,
            this.colQty});
            this.dgvItems.Location = new System.Drawing.Point(24, 116);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(1026, 220);
            // 
            // colDrugName
            // 
            this.colDrugName.DataPropertyName = "DrugName";
            this.colDrugName.HeaderText = "药品名称";
            this.colDrugName.Name = "colDrugName";
            this.colDrugName.ReadOnly = true;
            // 
            // colSpec
            // 
            this.colSpec.DataPropertyName = "Spec";
            this.colSpec.HeaderText = "规格";
            this.colSpec.Name = "colSpec";
            this.colSpec.ReadOnly = true;
            // 
            // colQty
            // 
            this.colQty.DataPropertyName = "RequiredQty";
            this.colQty.HeaderText = "数量";
            this.colQty.Name = "colQty";
            this.colQty.ReadOnly = true;
            // 
            // lblList
            // 
            this.lblList.AutoSize = true;
            this.lblList.Location = new System.Drawing.Point(24, 356);
            this.lblList.Name = "lblList";
            this.lblList.Size = new System.Drawing.Size(67, 15);
            this.lblList.Text = "已保存处方";
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AutoGenerateColumns = false;
            this.dgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colPrescriptionName,
            this.colItemCount,
            this.colCreateTime});
            this.dgvList.Location = new System.Drawing.Point(24, 384);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(1026, 300);
            this.dgvList.SelectionChanged += new System.EventHandler(this.DgvList_SelectionChanged);
            // 
            // colId
            // 
            this.colId.DataPropertyName = "PrescriptionId";
            this.colId.HeaderText = "处方号";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            // 
            // colPrescriptionName
            // 
            this.colPrescriptionName.DataPropertyName = "PrescriptionName";
            this.colPrescriptionName.HeaderText = "处方名称";
            this.colPrescriptionName.Name = "colPrescriptionName";
            this.colPrescriptionName.ReadOnly = true;
            // 
            // colItemCount
            // 
            this.colItemCount.DataPropertyName = "ItemCount";
            this.colItemCount.HeaderText = "药品种类数";
            this.colItemCount.Name = "colItemCount";
            this.colItemCount.ReadOnly = true;
            // 
            // colCreateTime
            // 
            this.colCreateTime.DataPropertyName = "CreateTimeText";
            this.colCreateTime.HeaderText = "建立时间";
            this.colCreateTime.Name = "colCreateTime";
            this.colCreateTime.ReadOnly = true;
            // 
            // UcPrescriptionCatalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblDrug);
            this.Controls.Add(this.cmbDrug);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.nudQuantity);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.btnRemoveItem);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.lblList);
            this.Controls.Add(this.dgvList);
            this.Name = "UcPrescriptionCatalog";
            this.Size = new System.Drawing.Size(1080, 710);
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblDrug;
        private System.Windows.Forms.ComboBox cmbDrug;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDrugName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;
        private System.Windows.Forms.Label lblList;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrescriptionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreateTime;
    }
}
