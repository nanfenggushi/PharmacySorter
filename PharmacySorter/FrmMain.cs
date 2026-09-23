using Common;
using System;
using System.Windows.Forms;

namespace PharmacySorter
{
    public partial class FrmMain : Form
    {
        // 配药监控看板
        private UserControl ucDashboard = null;
        // 工位与药品配置
        private UserControl ucDrugStationMapping = null;
        // 药品字典管理
        private UserControl ucDrugDictionary = null;
        // 处方录入与队列管理
        private UserControl ucPrescriptionQueue = null;
        // 系统操作日志审计
        private UserControl ucSystemAuditLog = null;

        public FrmMain()
        {
            InitializeComponent();
        }

        // 跳转到配药监控看板
        private void button1_Click(object sender, EventArgs e)
        {
            PageHelper.SwitchPage<UcDashboard>(pnlPageContainer, ref ucDashboard);
        }

        // 跳转到工位与药品配置
        private void button2_Click(object sender, EventArgs e)
        {
            PageHelper.SwitchPage<UcDrugStationMapping>(pnlPageContainer, ref ucDrugStationMapping);
        }

        // 跳转到药品字典管理
        private void button3_Click(object sender, EventArgs e)
        {
            PageHelper.SwitchPage<UcDrugDictionary>(pnlPageContainer, ref ucDrugDictionary);
        }

        // 跳转到处方录入与队列管理
        private void button4_Click(object sender, EventArgs e)
        {
            PageHelper.SwitchPage<UcPrescriptionQueue>(pnlPageContainer, ref ucPrescriptionQueue);
        }

        // 跳转到系统操作日志审计
        private void button5_Click(object sender, EventArgs e)
        {
            PageHelper.SwitchPage<UcSystemAuditLog>(pnlPageContainer, ref ucSystemAuditLog);
        }
    }
}
