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
        private void btnSearch_Click(object sender, EventArgs e)
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
    }
}
