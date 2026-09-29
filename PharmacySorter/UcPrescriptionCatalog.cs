using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PharmacySorter
{
    /// <summary>
    /// 固定处方维护。处方名称唯一，药品和数量保存在模板里，加入队列时再复制。
    /// </summary>
    public partial class UcPrescriptionCatalog : UserControl
    {
        private readonly PrescriptionBLL prescriptionBll = new PrescriptionBLL();
        private readonly List<PrescriptionItem> draftItems = new List<PrescriptionItem>();
        private int editingId;

        public UcPrescriptionCatalog()
        {
            InitializeComponent();
            dgvItems.AutoGenerateColumns = false;
            dgvList.AutoGenerateColumns = false;
            ConfigureGrid(dgvItems);
            ConfigureGrid(dgvList);
            Load += UcPrescriptionCatalog_Load;
        }

        private static void ConfigureGrid(DataGridView grid)
        {
            grid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.EnableHeadersVisualStyles = true;
        }

        private void UcPrescriptionCatalog_Load(object sender, EventArgs e)
        {
            BindDrugs();
            BindList(0);
            ClearEditor();
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearEditor();
            txtName.Focus();
        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            Drug drug = cmbDrug.SelectedItem as Drug;
            if (drug == null)
            {
                MessageBox.Show("请选择药品", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (PrescriptionItem exists in draftItems)
            {
                if (exists.DrugId == drug.DrugId)
                {
                    exists.RequiredQty = Convert.ToInt32(nudQuantity.Value);
                    BindDraft();
                    return;
                }
            }

            draftItems.Add(new PrescriptionItem
            {
                DrugId = drug.DrugId,
                DrugName = drug.DrugName,
                Spec = drug.Spec,
                RequiredQty = Convert.ToInt32(nudQuantity.Value)
            });
            BindDraft();
        }

        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            PrescriptionItem item = dgvItems.CurrentRow == null ? null : dgvItems.CurrentRow.DataBoundItem as PrescriptionItem;
            if (item == null)
            {
                MessageBox.Show("请先选择一种药品", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            draftItems.Remove(item);
            BindDraft();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int savedId = prescriptionBll.Save(editingId, txtName.Text, draftItems);
                BindList(savedId);
                MessageBox.Show("处方已保存", "保存", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (editingId <= 0)
            {
                MessageBox.Show("请先选择一张处方", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show("确定删除这张固定处方吗？已有配药记录的处方不能删除。", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                prescriptionBll.Delete(editingId);
                BindList(0);
                ClearEditor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DgvList_SelectionChanged(object sender, EventArgs e)
        {
            Prescription prescription = dgvList.CurrentRow == null ? null : dgvList.CurrentRow.DataBoundItem as Prescription;
            if (prescription == null || prescription.PrescriptionId == editingId)
            {
                return;
            }

            try
            {
                editingId = prescription.PrescriptionId;
                txtName.Text = prescription.PrescriptionName;
                draftItems.Clear();
                draftItems.AddRange(prescriptionBll.GetItems(prescription.PrescriptionId));
                BindDraft();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "读取处方失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BindDrugs()
        {
            cmbDrug.DisplayMember = "DrugName";
            cmbDrug.ValueMember = "DrugId";
            cmbDrug.DataSource = prescriptionBll.GetSelectableDrugs();
        }

        private void BindList(int selectedId)
        {
            dgvList.DataSource = null;
            dgvList.DataSource = prescriptionBll.GetAll();
            if (selectedId <= 0)
            {
                return;
            }

            foreach (DataGridViewRow row in dgvList.Rows)
            {
                Prescription prescription = row.DataBoundItem as Prescription;
                if (prescription != null && prescription.PrescriptionId == selectedId)
                {
                    row.Selected = true;
                    dgvList.CurrentCell = row.Cells[0];
                    return;
                }
            }
        }

        private void BindDraft()
        {
            dgvItems.DataSource = null;
            dgvItems.DataSource = new List<PrescriptionItem>(draftItems);
        }

        private void ClearEditor()
        {
            editingId = 0;
            txtName.Text = string.Empty;
            nudQuantity.Value = 1;
            draftItems.Clear();
            BindDraft();
            dgvList.ClearSelection();
            dgvList.CurrentCell = null;
        }
    }
}
