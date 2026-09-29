using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 操作日志业务。负责条件校验、写日志，以及生成 Excel 可打开的表格内容。
    /// </summary>
    public class AppLogBLL
    {
        private readonly AppLogDAL dal = new AppLogDAL();

        /// <summary>
        /// 记录一条操作。内容不能为空，类型必须是系统约定的日志类型。
        /// </summary>
        public void Add(int? itemId, string logType, string content)
        {
            if (string.IsNullOrWhiteSpace(logType))
            {
                throw new ArgumentException("日志类型不能为空");
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException("日志内容不能为空");
            }

            dal.Add(itemId, logType.Trim(), content.Trim());
        }

        /// <summary>
        /// 每页显示的日志条数。
        /// </summary>
        public const int PageSize = 20;

        /// <summary>
        /// 按界面条件查询指定页。页码从 1 开始，结束日期包含当天整天。
        /// 日期或类型留空表示不按该项过滤。
        /// </summary>
        public AppLogPage SearchPage(DateTime? startDate, DateTime? endDate, string prescriptionIdText, string logType, int pageIndex)
        {
            if (pageIndex < 1)
            {
                throw new ArgumentException("页码必须从 1 开始");
            }

            DateTime? startTime;
            DateTime? endTime;
            int? prescriptionId;
            string normalizedType;
            PrepareSearch(startDate, endDate, prescriptionIdText, logType, out startTime, out endTime, out prescriptionId, out normalizedType);
            return dal.SearchPage(startTime, endTime, prescriptionId, normalizedType, pageIndex, PageSize);
        }

        /// <summary>
        /// 按界面条件查询全部结果，供导出使用。结束日期包含当天整天。
        /// </summary>
        public List<AppLog> Search(DateTime? startDate, DateTime? endDate, string prescriptionIdText, string logType)
        {
            DateTime? startTime;
            DateTime? endTime;
            int? prescriptionId;
            string normalizedType;
            PrepareSearch(startDate, endDate, prescriptionIdText, logType, out startTime, out endTime, out prescriptionId, out normalizedType);
            return dal.Search(startTime, endTime, prescriptionId, normalizedType);
        }

        private static void PrepareSearch(
            DateTime? startDate,
            DateTime? endDate,
            string prescriptionIdText,
            string logType,
            out DateTime? startTime,
            out DateTime? endTime,
            out int? prescriptionId,
            out string normalizedType)
        {
            if (startDate.HasValue && endDate.HasValue && startDate.Value.Date > endDate.Value.Date)
            {
                throw new ArgumentException("开始时间不能晚于结束时间");
            }

            startTime = startDate.HasValue ? startDate.Value.Date : (DateTime?)null;
            endTime = endDate.HasValue ? endDate.Value.Date.AddDays(1) : (DateTime?)null;
            prescriptionId = ParsePrescriptionId(prescriptionIdText);
            normalizedType = NormalizeType(logType);
        }

        /// <summary>
        /// 把当前查询结果转成 Excel 2003 XML。用系统自带方式打开，不依赖第三方组件。
        /// </summary>
        public string BuildExcelXml(IList<AppLog> logs)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            xml.Append("<?mso-application progid=\"Excel.Sheet\"?>");
            xml.Append("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">");
            xml.Append("<Worksheet ss:Name=\"操作日志\"><Table>");
            xml.Append("<Row>");
            AppendCell(xml, "时间");
            AppendCell(xml, "处方号");
            AppendCell(xml, "事件类型");
            AppendCell(xml, "详细内容");
            xml.Append("</Row>");

            if (logs != null)
            {
                foreach (AppLog log in logs)
                {
                    xml.Append("<Row>");
                    AppendCell(xml, log.LogTimeText);
                    AppendCell(xml, log.PrescriptionText);
                    AppendCell(xml, log.LogType);
                    AppendCell(xml, log.Content);
                    xml.Append("</Row>");
                }
            }

            xml.Append("</Table></Worksheet></Workbook>");
            return xml.ToString();
        }

        private static void AppendCell(StringBuilder xml, string value)
        {
            xml.Append("<Cell><Data ss:Type=\"String\">");
            xml.Append(Escape(value));
            xml.Append("</Data></Cell>");
        }

        private static string Escape(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            return value.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
        }

        /// <summary>
        /// 空文本表示不按处方过滤。有内容时必须是正整数。
        /// </summary>
        private static int? ParsePrescriptionId(string prescriptionIdText)
        {
            if (string.IsNullOrWhiteSpace(prescriptionIdText))
            {
                return null;
            }

            int prescriptionId;
            if (!int.TryParse(prescriptionIdText.Trim(), out prescriptionId) || prescriptionId <= 0)
            {
                throw new ArgumentException("处方编号必须是正整数");
            }
            return prescriptionId;
        }

        /// <summary>
        /// “全部”和空白都表示不按类型过滤。
        /// </summary>
        private static string NormalizeType(string logType)
        {
            if (string.IsNullOrWhiteSpace(logType) || logType.Trim() == "全部")
            {
                return null;
            }
            return logType.Trim();
        }
    }
}