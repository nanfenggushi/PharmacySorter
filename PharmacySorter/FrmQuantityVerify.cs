using Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PharmacySorter
{
    /// <summary>
    /// 人工数量复核。一种药品抓完后必须由操作员清点，才能进入下一种药品。
    /// </summary>
    public partial class FrmQuantityVerify : Form
    {
        /// <summary>
        /// 当前正在核对的任务明细。
        /// </summary>
        private readonly DispenseOrderItem item;

        /// <summary>
        /// 操作员最终选择。关闭窗口前由按钮写入。
        /// </summary>
        public VerifyDecision Decision { get; private set; }

        /// <summary>
        /// 操作员输入的实收数量。
        /// </summary>
        public int ActualQty
        {
            get { return Convert.ToInt32(nudActualQty.Value); }
        }

        public FrmQuantityVerify(DispenseOrderItem currentItem)
        {
            if (currentItem == null)
            {
                throw new ArgumentNullException("currentItem");
            }

            InitializeComponent();
            item = currentItem;
            lblDrug.Text = "请清点分拣槽内的 【" + item.DrugName + "】";
            lblRequiredQty.Text = "应发数量：[ " + item.RequiredQty + " ] 盒";
            nudActualQty.Value = 0;
            RefreshHint();
        }

        /// <summary>
        /// 数量变化时立即切换一致、短缺和超量三种提示。
        /// </summary>
        private void NudActualQty_ValueChanged(object sender, EventArgs e)
        {
            RefreshHint();
        }

        /// <summary>
        /// 超量时只有勾选“已拿走多余药品”才能放行。
        /// </summary>
        private void ChkExcessCleared_CheckedChanged(object sender, EventArgs e)
        {
            RefreshHint();
        }

        /// <summary>
        /// 数量一致时直接通过。超量时这个按钮表示已经剔除多余药品。
        /// </summary>
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (ActualQty == item.RequiredQty)
            {
                Decision = VerifyDecision.Confirm;
            }
            else if (ActualQty > item.RequiredQty && chkExcessCleared.Checked)
            {
                Decision = VerifyDecision.ReleaseExcess;
            }
            else
            {
                return;
            }

            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 数量不足，返回补抓决定。补抓次数由业务层按差额计算。
        /// </summary>
        private void BtnRefill_Click(object sender, EventArgs e)
        {
            if (ActualQty >= item.RequiredQty)
            {
                return;
            }

            Decision = VerifyDecision.Refill;
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 严重异常。抓取次数清零后由看板重新抓取这一条。
        /// </summary>
        private void BtnReset_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show(
                "确定作废【" + item.DrugName + "】的当前进度吗？请先清空分拣槽。",
                "重新抓取",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (answer != DialogResult.Yes)
            {
                return;
            }

            Decision = VerifyDecision.Reset;
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 根据实收数量显示对应提示，并只放开当前允许的按钮。
        /// </summary>
        private void RefreshHint()
        {
            int actual = ActualQty;
            int diff = item.RequiredQty - actual;
            bool matched = diff == 0;
            bool shorted = diff > 0;
            bool excess = diff < 0;

            lblMatchIcon.Visible = matched;
            lblMatch.Visible = matched;
            lblShortageIcon.Visible = shorted;
            lblShortage.Visible = shorted;
            lblExcess.Visible = excess;
            chkExcessCleared.Visible = excess;

            if (matched)
            {
                pnlAlert.BackColor = Color.Honeydew;
                btnConfirm.Visible = true;
                btnConfirm.Enabled = true;
                btnConfirm.Text = "确认并继续";
                btnRefill.Visible = false;
            }
            else if (shorted)
            {
                pnlAlert.BackColor = Color.PapayaWhip;
                lblShortage.Text = "数量不足，还差 " + diff + " 盒";
                btnRefill.Text = "自动补抓 " + diff + " 盒";
                btnConfirm.Visible = false;
                btnRefill.Visible = true;
                btnRefill.Enabled = true;
            }
            else
            {
                pnlAlert.BackColor = Color.MistyRose;
                int extra = actual - item.RequiredQty;
                lblExcess.Text = "数量超出 " + extra + " 盒，请取走多余药品";
                chkExcessCleared.Text = "我已从分拣槽取走多余的 " + extra + " 盒并放回原处";
                btnRefill.Visible = false;
                btnConfirm.Visible = true;
                btnConfirm.Enabled = chkExcessCleared.Checked;
                btnConfirm.Text = "确认并放行";
            }
        }
    }
}
