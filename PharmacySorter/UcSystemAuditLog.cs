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
        /// 当前表格中的查询结果。导出时重新按条件查询全部，不限于当前页。
        /// </summary>
        private List<AppLog> currentLogs = new List<AppLog>();

        /// <summary>
        /// 当前页码，从 1 开始。
        /// </summary>
        private int currentPage = 1;

        /// <summary>
        /// 当前筛选条件下的总页数。没有数据时按 1 页显示。
        /// </summary>
        private int totalPages = 1;

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

            SearchLogs(1);
        }

        /// <summary>
        /// 按当前筛选条件从第一页重新查询。
        /// </summary>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchLogs(1);
        }

        /// <summary>
        /// 翻到第一页。
        /// </summary>
        private void BtnFirst_Click(object sender, EventArgs e)
        {
            SearchLogs(1);
        }

        /// <summary>
        /// 翻到上一页。
        /// </summary>
        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            SearchLogs(currentPage - 1);
        }

        /// <summary>
        /// 翻到下一页。
        /// </summary>
        private void BtnNext_Click(object sender, EventArgs e)
        {
            SearchLogs(currentPage + 1);
        }

        /// <summary>
        /// 翻到最后一页。
        /// </summary>
        private void BtnLast_Click(object sender, EventArgs e)
        {
            SearchLogs(totalPages);
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
                    DateTime? startDate = dtpStart.Checked ? dtpStart.Value : (DateTime?)null;
                    DateTime? endDate = dtpEnd.Checked ? dtpEnd.Value : (DateTime?)null;
                    List<AppLog> logs = logBll.Search(startDate, endDate, txtPrescriptionId.Text, Convert.ToString(cmbLogType.SelectedItem));
                    File.WriteAllText(dialog.FileName, logBll.BuildExcelXml(logs), Encoding.UTF8);
                    MessageBox.Show("导出完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "导出失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// 读取筛选条件并绑定指定页。日期勾选框未勾选时，不按该日期过滤。
        /// </summary>
        private void SearchLogs(int pageIndex)
        {
            try
            {
                DateTime? startDate = dtpStart.Checked ? dtpStart.Value : (DateTime?)null;
                DateTime? endDate = dtpEnd.Checked ? dtpEnd.Value : (DateTime?)null;
                AppLogPage page = logBll.SearchPage(startDate, endDate, txtPrescriptionId.Text, Convert.ToString(cmbLogType.SelectedItem), pageIndex);
                currentLogs = page.Items ?? new List<AppLog>();
                currentPage = pageIndex;
                totalPages = Math.Max(1, (page.TotalCount + AppLogBLL.PageSize - 1) / AppLogBLL.PageSize);
                dgvLog.DataSource = null;
                dgvLog.DataSource = currentLogs;
                lblPage.Text = "第 " + currentPage + "/" + totalPages + " 页，共 " + page.TotalCount + " 条";
                btnFirst.Enabled = currentPage > 1;
                btnPrevious.Enabled = currentPage > 1;
                btnNext.Enabled = currentPage < totalPages;
                btnLast.Enabled = currentPage < totalPages;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "查询失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
