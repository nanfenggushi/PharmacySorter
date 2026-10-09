using System;
using System.Windows.Forms;

namespace PharmacySorter
{
    /// <summary>
    /// 药品与工位模块。上下分区布局：上方药品字典维护，下方工位指令配置。
    /// 分区权限：药品字典供管理员和药师维护；工位指令区仅管理员可见，
    /// 药师进入本页时整个下半区隐藏。
    /// </summary>
    public partial class UcDrugStation : UserControl
    {
        public UcDrugStation()
        {
            InitializeComponent();
            Load += UcDrugStation_Load;
        }

        /// <summary>
        /// 按权限设置“工位指令”区可见性。
        /// 管理员看上下两区；药师只看上方药品字典区。
        /// </summary>
        /// <param name="visible">true 显示工位指令区，false 隐藏（区域折叠，药品字典占满全页）。</param>
        public void SetStationCommandsVisible(bool visible)
        {
            splitMain.Panel2Collapsed = !visible;
        }

        /// <summary>
        /// 首次显示时把下方工位区固定为 290 像素高，其余高度留给药品字典；
        /// 之后窗口缩放时下方区域高度不变（SplitContainer.FixedPanel=Panel2）。
        /// </summary>
        private void UcDrugStation_Load(object sender, EventArgs e)
        {
            int distance = splitMain.Height - 290;
            if (distance < splitMain.Panel1MinSize)
            {
                distance = splitMain.Panel1MinSize;
            }
            splitMain.SplitterDistance = distance;
        }
    }
}
