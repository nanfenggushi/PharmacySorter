using System.Windows.Forms;

namespace PharmacySorter
{
    /// <summary>
    /// 系统运维模块。把低频管理功能合并为一个入口，
    /// 页内用 Tab 页签切换“人员权限”和“系统日志”，仅管理员可见。
    /// </summary>
    public partial class UcSystemOps : UserControl
    {
        public UcSystemOps()
        {
            InitializeComponent();
        }
    }
}
