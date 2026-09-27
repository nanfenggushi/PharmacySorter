using System;

namespace Model
{
    /// <summary>
    /// 系统操作日志。处方号来自关联的处方明细，系统级日志可以为空。
    /// </summary>
    public class AppLog
    {
        public int LogId { get; set; }

        /// <summary>
        /// 关联的处方明细编号。药品维护、工位配置等系统操作没有明细，保持为空。
        /// </summary>
        public int? ItemId { get; set; }

        /// <summary>
        /// 由明细反查到的处方号，仅用于界面展示。
        /// </summary>
        public int? PrescriptionId { get; set; }

        public string LogType { get; set; }

        public string Content { get; set; }

        public DateTime LogTime { get; set; }

        /// <summary>
        /// 表格中的时间文本。
        /// </summary>
        public string LogTimeText
        {
            get { return LogTime.ToString("yyyy-MM-dd HH:mm:ss"); }
        }

        /// <summary>
        /// 表格中的处方号。没有关联处方时显示空字符串。
        /// </summary>
        public string PrescriptionText
        {
            get { return PrescriptionId.HasValue ? PrescriptionId.Value.ToString() : string.Empty; }
        }
    }

    /// <summary>
    /// 日志类型。界面筛选和写日志都使用这里的固定名称。
    /// </summary>
    public static class AppLogType
    {
        public const string DrugMaintain = "药品维护";
        public const string StationConfig = "工位配置";
        public const string Command = "指令下发";
        public const string QuantityCheck = "数量核对";
        public const string ExceptionFix = "异常纠偏";
        public const string Prescription = "处方管理";
        public const string User = "账号管理";

        /// <summary>
        /// 筛选下拉框使用的全部类型，顺序与界面一致。
        /// </summary>
        public static readonly string[] All = new string[]
        {
            DrugMaintain,
            StationConfig,
            Command,
            QuantityCheck,
            ExceptionFix,
            Prescription,
            User
        };
    }
}