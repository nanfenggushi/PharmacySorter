using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PharmacySorter
{
    /// <summary>
    /// 处方录入与待配队列。
    /// 左侧先把药品加入临时清单，确认后再整张提交。
    /// 右侧只显示状态为“待配药”的处方，可置顶或撤销。
    /// </summary>
    public partial class UcPrescriptionQueue : UserControl
    {
        /// <summary>
        /// 处方业务对象。界面不直接访问数据库。
        /// </summary>
        private readonly PrescriptionBLL prescriptionBll = new PrescriptionBLL();

        /// <summary>
        /// 尚未提交的药品清单。提交成功后清空。
        /// </summary>
        private readonly List<PrescriptionItem> draftItems = new List<PrescriptionItem>();

        public UcPrescriptionQueue()
        {
            InitializeComponent();
            Load += UcPrescriptionQueue_Load;
        }

        /// <summary>
        /// 进入页面时加载可选药品、预览处方号，并刷新待配队列。
        /// </summary>
        private void UcPrescriptionQueue_Load(object sender, EventArgs e)
        {
            BindDrugs(); // 下拉框绑定可选药品
            PreviewPrescriptionId(); // 显示下一个处方编号
            nudQuantity.Value = 1; // 药品数量初始值设为1
            BindDraft(); // 绑定尚未提交的药品清单
            LoadQueue(); // 读取待配药队列
        }

        /// <summary>
        /// 打开历史处方查询。待配队列仍只显示未开始的处方。
        /// </summary>
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

        /// <summary>
        /// 刷新自动生成的处方号。该编号只是预览，正式编号以提交时数据库生成的为准。
        /// </summary>
        private void BtnGenerateId_Click(object sender, EventArgs e)
        {
            PreviewPrescriptionId();
        }

        /// <summary>
        /// 把当前选择的药品和数量加入左侧临时清单。同一种药不能重复加入。
        /// </summary>
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
                    MessageBox.Show("该药品已在清单中，请先移除后再重新加入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        /// <summary>
        /// 从临时清单移除当前选中的药品。
        /// </summary>
        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            PrescriptionItem item = CurrentDraftItem();
            if (item == null)
            {
                MessageBox.Show("请先在待提交清单中选择一种药品", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            draftItems.Remove(item);
            BindDraft();
        }

        /// <summary>
        /// 校验患者和清单后正式提交，并刷新右侧队列。
        /// </summary>
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int prescriptionId = prescriptionBll.Submit(txtPatientNo.Text, draftItems);
                draftItems.Clear();
                txtPatientNo.Text = string.Empty;
                nudQuantity.Value = 1;
                BindDraft();
                PreviewPrescriptionId();
                LoadQueue();
                MessageBox.Show("处方 " + prescriptionId + " 已提交", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提交失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 把右侧选中的待配处方移到队列第一行。
        /// </summary>
        private void BtnMoveTop_Click(object sender, EventArgs e)
        {
            Prescription prescription = CurrentPrescription();
            if (prescription == null)
            {
                MessageBox.Show("请先选择一张待配处方", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                prescriptionBll.MoveToTop(prescription.PrescriptionId);
                LoadQueue();
                SelectPrescription(prescription.PrescriptionId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "置顶失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 撤销右侧选中的待配处方。已经开始配药的处方不允许撤销。
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Prescription prescription = CurrentPrescription();
            if (prescription == null)
            {
                MessageBox.Show("请先选择一张待配处方", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "确定撤销处方 " + prescription.PrescriptionId + " 吗？撤销后不再进入配药队列。",
                "撤销确认",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                prescriptionBll.Cancel(prescription.PrescriptionId);
                LoadQueue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "撤销失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 只加载已启用且已绑定左/右药位的药品。
        /// </summary>
        private void BindDrugs()
        {
            List<Drug> drugs = prescriptionBll.GetSelectableDrugs();
            cmbDrug.DisplayMember = "DrugName";
            cmbDrug.ValueMember = "DrugId";
            cmbDrug.DataSource = drugs;
        }

        /// <summary>
        /// 显示下一个处方号，文本框保持只读。
        /// </summary>
        private void PreviewPrescriptionId()
        {
            try
            {
                txtPrescriptionId.Text = prescriptionBll.PreviewNextId().ToString();
            }
            catch (Exception ex)
            {
                txtPrescriptionId.Text = string.Empty;
                MessageBox.Show(ex.Message, "读取处方号失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 重新绑定左侧临时清单。列在设计器中手工定义，不自动生成。
        /// </summary>
        private void BindDraft()
        {
            dgvDraft.DataSource = null;
            dgvDraft.DataSource = new List<PrescriptionItem>(draftItems);
        }

        /// <summary>
        /// 重新读取待配队列。
        /// </summary>
        private void LoadQueue()
        {
            try
            {
                dgvQueue.DataSource = null;
                dgvQueue.DataSource = prescriptionBll.GetWaitingQueue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "读取队列失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 返回当前选择的草稿处方药品对象
        /// </summary>
        /// <returns></returns>
        private PrescriptionItem CurrentDraftItem()
        {
            if (dgvDraft.CurrentRow == null)
            {
                return null;
            }
            return dgvDraft.CurrentRow.DataBoundItem as PrescriptionItem;
        }

        /// <summary>
        /// 返回当前选择的处方对象
        /// </summary>
        /// <returns></returns>
        private Prescription CurrentPrescription()
        {
            if (dgvQueue.CurrentRow == null)
            {
                return null;
            }
            return dgvQueue.CurrentRow.DataBoundItem as Prescription;
        }

        /// <summary>
        /// 操作完成后重新选中原来的处方。
        /// </summary>
        private void SelectPrescription(int prescriptionId)
        {
            foreach (DataGridViewRow row in dgvQueue.Rows)
            {
                Prescription prescription = row.DataBoundItem as Prescription;
                if (prescription != null && prescription.PrescriptionId == prescriptionId)
                {
                    row.Selected = true;
                    dgvQueue.CurrentCell = row.Cells[0];
                    return;
                }
            }
        }
    }
}
