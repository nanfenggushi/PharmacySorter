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
        private readonly DispenseOrderBLL orderBll = new DispenseOrderBLL();

        public FrmPrescriptionHistory()
        {
            InitializeComponent();
            dgvHistory.AutoGenerateColumns = false;
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
                List<DispenseOrder> list = orderBll.SearchHistory(txtPrescriptionId.Text, string.Empty, dtpStart.Value, dtpEnd.Value);
                dgvHistory.DataSource = null;
                dgvHistory.DataSource = list;
                lblCount.Text = "共 " + list.Count + " 条";
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
            DispenseOrder order = dgvHistory.CurrentRow == null ? null : dgvHistory.CurrentRow.DataBoundItem as DispenseOrder;
            if (order == null)
            {
                MessageBox.Show("请先选择一条配药记录", "再次配药", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "将按任务 " + order.OrderId + " 的患者和处方再生成一条待配任务，原记录保持不变。是否继续？",
                "再次配药",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                int newOrderId = orderBll.AddToQueue(order.PatientNo, order.PrescriptionId);
                MessageBox.Show("已加入待配队列，新任务号：" + newOrderId, "再次配药", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "再次配药失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
