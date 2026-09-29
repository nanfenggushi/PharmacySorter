using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PharmacySorter
{
    /// <summary>
    /// 药品字典维护。
    /// 上方输入区既用于录入新药品，也用于回显并修改当前选中药品。
    /// 药品编号由数据库生成，界面只读，不允许手工修改。
    /// </summary>
    public partial class UcDrugDictionary : UserControl
    {
        /// <summary>
        /// 药品字典业务对象。界面不直接访问数据库。
        /// </summary>
        private readonly DrugBLL drugBll = new DrugBLL();

        /// <summary>
        /// 工位业务对象。下拉框中的药位来自工位表，不在界面写死。
        /// </summary>
        private readonly StationBLL stationBll = new StationBLL();

        /// <summary>
        /// 当前正在编辑的药品编号。
        /// 为 null 表示上方处于新增状态，保存时执行插入；有值时保存执行更新。
        /// </summary>
        private int? editingDrugId;

        public UcDrugDictionary()
        {
            InitializeComponent();
            dgvMedicineInfo.AutoGenerateColumns = false;
            dgvMedicineInfo.CellFormatting += DgvMedicineInfo_CellFormatting;
            dgvMedicineInfo.SelectionChanged += DgvMedicineInfo_SelectionChanged;
            Load += UcDrugDictionary_Load;
        }

        /// <summary>
        /// 页面首次显示时加载全部药品，并进入新增状态。
        /// </summary>
        private void UcDrugDictionary_Load(object sender, EventArgs e)
        {
            BindStations();
            BindDrugList(null);
            ClearEditor();
        }

        /// <summary>
        /// 加载可绑定工位，并在最前面增加“未绑定”选项。
        /// </summary>
        private void BindStations()
        {
            List<Station> stations = new List<Station>();
            stations.Add(new Station { StationId = null, StationName = "未绑定" });
            stations.AddRange(stationBll.GetBindableStations());

            cmbCurrentWorkstation.DisplayMember = "StationName";
            cmbCurrentWorkstation.ValueMember = "StationId";
            cmbCurrentWorkstation.DataSource = stations;
        }

        /// <summary>
        /// 按关键字查询。关键字可匹配药品编号、药品名称或规格，留空则查询全部。
        /// </summary>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            BindDrugList(null);
        }

        /// <summary>
        /// 清空上方输入区，准备录入一条新药品。
        /// </summary>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ClearEditor();
            txtDrugName.Focus();
        }

        /// <summary>
        /// 保存上方输入区。
        /// 编号为空时新增，编号有值时按该编号修改名称、规格和工位。
        /// </summary>
        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                int? stationId = SelectedStationId();
                int savedDrugId;
                if (editingDrugId.HasValue)
                {
                    savedDrugId = editingDrugId.Value;
                    drugBll.Update(savedDrugId, txtDrugName.Text, txtSpec.Text, stationId);
                }
                else
                {
                    savedDrugId = drugBll.Add(txtDrugName.Text, txtSpec.Text, stationId);
                }

                BindDrugList(savedDrugId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 停用或重新启用当前选中药品。
        /// 只修改 IsActive，不删除记录，历史处方仍能关联到该药品。
        /// </summary>
        private void Button3_Click(object sender, EventArgs e)
        {
            Drug drug = CurrentDrug();
            if (drug == null)
            {
                MessageBox.Show("请先在下方表格中选择一条药品", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool enable = !drug.IsActive;
            string action = enable ? "启用" : "停用";
            DialogResult confirm = MessageBox.Show(
                "确定要" + action + "【" + drug.DrugName + "】吗？停用后处方录入和工位绑定将不能再选择该药品，历史记录仍会保留。",
                action + "确认",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                drugBll.SetActive(drug.DrugId, enable);
                BindDrugList(drug.DrugId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, action + "失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 表格选中行变化时，把该行的编号、名称、规格回填到上方输入区。
        /// 工位同步选中下拉项。编号文本框只读，用户不能改成其他药品。
        /// </summary>
        private void DgvMedicineInfo_SelectionChanged(object sender, EventArgs e)
        {
            Drug drug = CurrentDrug();
            if (drug == null)
            {
                btnStop.Text = "停用";
                return;
            }

            editingDrugId = drug.DrugId;
            txtMedicineId.Text = drug.DrugId.ToString();
            txtDrugName.Text = drug.DrugName;
            txtSpec.Text = drug.Spec;
            SelectStation(drug.StationId);
            btnStop.Text = drug.IsActive ? "停用" : "启用";
        }

        /// <summary>
        /// 停用药品整行显示为灰色，便于和启用药品区分。
        /// </summary>
        private void DgvMedicineInfo_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvMedicineInfo.Rows.Count)
            {
                return;
            }

            Drug drug = dgvMedicineInfo.Rows[e.RowIndex].DataBoundItem as Drug;
            DataGridViewCellStyle style = dgvMedicineInfo.Rows[e.RowIndex].DefaultCellStyle;
            if (drug != null && !drug.IsActive)
            {
                if (style.ForeColor != Color.Gray)
                {
                    style.ForeColor = Color.Gray;
                }
            }
            else if (style.ForeColor == Color.Gray)
            {
                style.ForeColor = dgvMedicineInfo.DefaultCellStyle.ForeColor;
            }
        }

        /// <summary>
        /// 按关键字重新绑定表格。
        /// selectedDrugId 有值时刷新后重新选中该药品，从而把最新数据回填到上方。
        /// </summary>
        /// <param name="selectedDrugId">需要保持选中的药品编号，不需要时传 null。</param>
        private void BindDrugList(int? selectedDrugId)
        {
            try
            {
                dgvMedicineInfo.DataSource = drugBll.SearchByKeyword(txtKeyword.Text);
                if (selectedDrugId.HasValue)
                {
                    SelectDrug(selectedDrugId.Value);
                }
                else
                {
                    DgvMedicineInfo_SelectionChanged(null, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "查询失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 清空上方编辑区并切回新增状态。药品编号留空，保存时才会插入新记录。
        /// </summary>
        private void ClearEditor()
        {
            editingDrugId = null;
            txtMedicineId.Text = string.Empty;
            txtDrugName.Text = string.Empty;
            txtSpec.Text = string.Empty;
            SelectStation(null);
            dgvMedicineInfo.ClearSelection();
            dgvMedicineInfo.CurrentCell = null;
        }

        /// <summary>
        /// 读取工位下拉框当前值。选中“未绑定”或未选择时返回 null。
        /// </summary>
        private int? SelectedStationId()
        {
            Station station = cmbCurrentWorkstation.SelectedItem as Station;
            return station == null ? null : station.StationId;
        }

        /// <summary>
        /// 按工位编号选中下拉项。找不到对应工位时回到“未绑定”。
        /// </summary>
        /// <param name="stationId">要选中的工位编号，null 表示未绑定。</param>
        private void SelectStation(int? stationId)
        {
            foreach (object item in cmbCurrentWorkstation.Items)
            {
                Station station = item as Station;
                if (station != null && station.StationId == stationId)
                {
                    cmbCurrentWorkstation.SelectedItem = station;
                    return;
                }
            }

            if (cmbCurrentWorkstation.Items.Count > 0)
            {
                cmbCurrentWorkstation.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 取得表格当前行绑定的药品。没有选中行或绑定对象不是药品时返回 null。
        /// </summary>
        private Drug CurrentDrug()
        {
            if (dgvMedicineInfo.CurrentRow == null)
            {
                return null;
            }

            return dgvMedicineInfo.CurrentRow.DataBoundItem as Drug;
        }

        /// <summary>
        /// 按药品编号选中表格中的对应行，选中后会触发回填。
        /// </summary>
        /// <param name="drugId">要选中的药品编号。</param>
        private void SelectDrug(int drugId)
        {
            foreach (DataGridViewRow row in dgvMedicineInfo.Rows)
            {
                Drug drug = row.DataBoundItem as Drug;
                if (drug != null && drug.DrugId == drugId)
                {
                    row.Selected = true;
                    dgvMedicineInfo.CurrentCell = row.Cells[0];
                    return;
                }
            }
        }
    }
}
