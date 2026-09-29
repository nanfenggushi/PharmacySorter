using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// 操作日志的一页查询结果。
    /// </summary>
    public class AppLogPage
    {
        /// <summary>
        /// 当前页的日志。
        /// </summary>
        public List<AppLog> Items { get; set; }

        /// <summary>
        /// 当前筛选条件下的总条数。
        /// </summary>
        public int TotalCount { get; set; }
    }
}
