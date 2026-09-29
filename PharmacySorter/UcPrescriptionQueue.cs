using BLL;
using Model;
using System;
using System.Windows.Forms;

namespace PharmacySorter
{
    /// <summary>
    /// 待配队列。只填写患者编号并选择已保存的固定处方，药品清单在加入时复制。
    /// </summary>
    public partial class UcPrescriptionQueue : UserControl
    {
        private readonly DispenseOrderBLL orderBll = new DispenseOrderBLL();
        private readonly PrescriptionBLL prescriptionBll = new PrescriptionBLL();

        public UcPrescriptionQueue()
        {
            InitializeComponent();
            ConfigureGrid(dgvDraft);
            ConfigureGrid(dgvQueue);
            Load += UcPrescriptionQueue_Load;
        }

        private static void ConfigureGrid(DataGridView grid)
        {
            grid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.EnableHeadersVisualStyles = true;
        }

        private void UcPrescriptionQueue_Load(object sender, EventArgs e)
        {
            BindPrescriptions();
            LoadQueue();
        }

        private void BtnHistory_Click(object sender, EventArgs e)
        {
            using (FrmPrescriptionHistory dialog = new FrmPrescriptionHistory())
            {
                if (dialog.ShowDialog(FindForm()) == DialogResult.OK)
                {
                    LoadQueue();
                }
            }
        }

        private void CmbPrescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPrescriptionItems();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            Prescription prescription = cmbDrug.SelectedItem as Prescription;
            if (prescription == null)
            {
                MessageBox.Show("请选择固定处方", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int orderId = orderBll.AddToQueue(txtPatientNo.Text, prescription.PrescriptionId);
                txtPatientNo.Text = string.Empty;
                LoadQueue();
                SelectOrder(orderId);
                MessageBox.Show("已加入待配队列，任务号：" + orderId, "加入队列", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "加入失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnMoveTop_Click(object sender, EventArgs e)
        {
            DispenseOrder order = CurrentOrder();
            if (order == null)
            {
                MessageBox.Show("请先选择一条待配任务", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                orderBll.MoveToTop(order.OrderId);
                LoadQueue();
                SelectOrder(order.OrderId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "置顶失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DispenseOrder order = CurrentOrder();
            if (order == null)
            {
                MessageBox.Show("请先选择一条待配任务", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "确定撤销任务 " + order.OrderId + " 吗？撤销后不再配药。",
                "撤销确认",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                orderBll.Cancel(order.OrderId);
                LoadQueue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "撤销失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BindPrescriptions()
        {
            try
            {
                cmbDrug.DisplayMember = "PrescriptionName";
                cmbDrug.ValueMember = "PrescriptionId";
                cmbDrug.DataSource = prescriptionBll.GetAll();
                ShowPrescriptionItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "读取处方失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowPrescriptionItems()
        {
            Prescription prescription = cmbDrug.SelectedItem as Prescription;
            dgvDraft.DataSource = null;
            if (prescription == null)
            {
                return;
            }

            try
            {
                dgvDraft.DataSource = prescriptionBll.GetItems(prescription.PrescriptionId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "读取处方明细失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadQueue()
        {
            try
            {
                dgvQueue.DataSource = null;
                dgvQueue.DataSource = orderBll.GetWaitingQueue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "读取队列失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
        }

        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
        }

        private void BtnGenerateId_Click(object sender, EventArgs e)
        {
        }

        private DispenseOrder CurrentOrder()
        {
            if (dgvQueue.CurrentRow == null)
            {
                return null;
            }
            return dgvQueue.CurrentRow.DataBoundItem as DispenseOrder;
        }

        private void SelectOrder(int orderId)
        {
            foreach (DataGridViewRow row in dgvQueue.Rows)
            {
                DispenseOrder order = row.DataBoundItem as DispenseOrder;
                if (order != null && order.OrderId == orderId)
                {
                    row.Selected = true;
                    dgvQueue.CurrentCell = row.Cells[0];
                    return;
                }
            }
        }
    }
}
