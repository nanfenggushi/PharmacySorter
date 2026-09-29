using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PharmacySorter
{
    /// <summary>
    /// 系统操作日志审计。
    /// 按时间、处方号和日志类型查询，结果按时间倒序显示，并可以导出为 Excel。
    /// 本页只查询，不提供修改和删除。
    /// </summary>
    public partial class UcSystemAuditLog : UserControl
    {
        /// <summary>
        /// 日志业务对象。界面不直接拼接 SQL。
        /// </summary>
        private readonly AppLogBLL logBll = new AppLogBLL();

        /// <summary>
        /// 当前表格中的查询结果。导出时使用这份数据，避免和界面再次不一致。
        /// </summary>
        private List<AppLog> currentLogs = new List<AppLog>();

        public UcSystemAuditLog()
        {
            InitializeComponent();
            dgvLog.AutoGenerateColumns = false;
            Load += UcSystemAuditLog_Load;
        }

        /// <summary>
        /// 初始化筛选条件：默认查最近 7 天，日志类型默认全部，然后自动查询一次。
        /// </summary>
        private void UcSystemAuditLog_Load(object sender, EventArgs e)
        {
            dtpStart.Value = DateTime.Today.AddDays(-6);
            dtpEnd.Value = DateTime.Today;
            dtpStart.Checked = false;
            dtpEnd.Checked = false;

            cmbLogType.Items.Clear();
            cmbLogType.Items.Add("全部");
            cmbLogType.Items.AddRange(AppLogType.All);
            cmbLogType.SelectedIndex = 0;

            SearchLogs();
        }

        /// <summary>
        /// 按当前筛选条件重新查询。
        /// </summary>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchLogs();
        }

        /// <summary>
        /// 把当前查询结果导出为 Excel 可打开的 XML 文件。
        /// </summary>
        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (currentLogs.Count == 0)
            {
                MessageBox.Show("当前没有可导出的日志", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "Excel 文件 (*.xls)|*.xls";
                dialog.FileName = "操作日志_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                if (dialog.ShowDialog(FindForm()) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    File.WriteAllText(dialog.FileName, logBll.BuildExcelXml(currentLogs), Encoding.UTF8);
                    MessageBox.Show("导出完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "导出失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// 读取筛选条件并绑定表格。日期勾选框未勾选时，不按该日期过滤。
        /// </summary>
        private void SearchLogs()
        {
            try
            {
                DateTime? startDate = dtpStart.Checked ? dtpStart.Value : (DateTime?)null;
                DateTime? endDate = dtpEnd.Checked ? dtpEnd.Value : (DateTime?)null;
                currentLogs = logBll.Search(startDate, endDate, txtPrescriptionId.Text, Convert.ToString(cmbLogType.SelectedItem));
                dgvLog.DataSource = null;
                dgvLog.DataSource = currentLogs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "查询失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
