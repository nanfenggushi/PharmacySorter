using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PharmacySorter
{
    /// <summary>
    /// 历史处方查询。按处方号和接收日期查看全部状态的处方。
    /// </summary>
    public partial class FrmPrescriptionHistory : Form
    {
        /// <summary>
        /// 处方业务。界面不直接访问数据库。
        /// </summary>
        private readonly PrescriptionBLL prescriptionBll = new PrescriptionBLL();

        public FrmPrescriptionHistory()
        {
            InitializeComponent();
            dtpStart.Value = DateTime.Today.AddDays(-30);
            dtpEnd.Value = DateTime.Today;
            Load += FrmPrescriptionHistory_Load;
        }

        /// <summary>
        /// 打开时先按默认日期查出最近记录。
        /// </summary>
        private void FrmPrescriptionHistory_Load(object sender, EventArgs e)
        {
            Search();
        }

        /// <summary>
        /// 按当前条件重新查询。
        /// </summary>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        /// <summary>
        /// 查询历史处方并绑定到表格。条件不合法时只提示，不清空上次结果。
        /// </summary>
        private void Search()
        {
            try
            {
                List<Prescription> list = prescriptionBll.SearchHistory(txtPrescriptionId.Text, dtpStart.Value, dtpEnd.Value);
                dgvHistory.DataSource = null;
                dgvHistory.DataSource = list;
                lblCount.Text = "共 " + list.Count + " 张";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "查询失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 按选中的历史处方再生成一张待配处方。原处方不会被修改。
        /// </summary>
        private void BtnRequeue_Click(object sender, EventArgs e)
        {
            Prescription prescription = dgvHistory.CurrentRow == null ? null : dgvHistory.CurrentRow.DataBoundItem as Prescription;
            if (prescription == null)
            {
                MessageBox.Show("请先选择一张历史处方", "再次配药", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "将按处方 " + prescription.PrescriptionId + " 的患者和药品生成一张新的待配处方，原处方保持不变。是否继续？",
                "再次配药",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                int newPrescriptionId = prescriptionBll.Requeue(prescription.PrescriptionId);
                MessageBox.Show("已加入待配队列，新处方号：" + newPrescriptionId, "再次配药", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "再次配药失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
