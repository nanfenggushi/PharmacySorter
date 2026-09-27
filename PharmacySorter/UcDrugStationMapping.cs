using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PharmacySorter
{
    /// <summary>
    /// 工位与药品配置。
    /// 上半部分维护三个固定工位的抓取指令、放置指令和动作延时。
    /// 下半部分只显示已启用药品，并把药品绑定到左侧药位或右侧药位。
    /// </summary>
    public partial class UcDrugStationMapping : UserControl
    {
        /// <summary>
        /// 工位业务对象，负责读取和保存宏指令。
        /// </summary>
        private readonly StationBLL stationBll = new StationBLL();

        /// <summary>
        /// 药品业务对象，负责读取已启用药品并保存工位绑定。
        /// </summary>
        private readonly DrugBLL drugBll = new DrugBLL();

        /// <summary>
        /// 药品绑定下拉的选项：未绑定、左侧药位、右侧药位。
        /// </summary>
        private List<Station> stationOptions = new List<Station>();

        public UcDrugStationMapping()
        {
            InitializeComponent();
            Load += UcDrugStationMapping_Load;
        }

        /// <summary>
        /// 进入页面时加载工位指令和已启用药品。
        /// </summary>
        private void UcDrugStationMapping_Load(object sender, EventArgs e)
        {
            BindStationOptions();
            LoadStations();
            LoadDrugs();
        }

        /// <summary>
        /// 保存上方工位宏指令配置。
        /// </summary>
        private void BtnSaveStation_Click(object sender, EventArgs e)
        {
            dgvStation.EndEdit();
            try
            {
                stationBll.SaveAll(CollectStations());
                LoadStations();
                MessageBox.Show("工位指令已保存", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存工位失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 保存下方药品与工位的绑定关系。
        /// </summary>
        private void BtnSaveBinding_Click(object sender, EventArgs e)
        {
            dgvDrugBinding.EndEdit();
            try
            {
                foreach (DataGridViewRow row in dgvDrugBinding.Rows)
                {
                    Drug drug = row.Tag as Drug;
                    if (drug == null)
                    {
                        continue;
                    }

                    int? stationId = row.Cells[colBindStation.Name].Value as int?;
                    drugBll.BindStation(drug.DrugId, stationId);
                }

                LoadDrugs();
                MessageBox.Show("药品绑定已保存", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存绑定失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 准备绑定下拉框的数据源。第一项工位编号为空，表示未绑定。
        /// </summary>
        private void BindStationOptions()
        {
            stationOptions = new List<Station>();
            stationOptions.Add(new Station { StationId = null, StationName = "未绑定" });
            stationOptions.AddRange(stationBll.GetBindableStations());

            colBindStation.DisplayMember = "StationName";
            colBindStation.ValueMember = "StationId";
            colBindStation.DataSource = stationOptions;
        }

        /// <summary>
        /// 把三个固定工位填入上方表格。工位名称不可改，指令和延时可以改。
        /// </summary>
        private void LoadStations()
        {
            dgvStation.Rows.Clear();
            foreach (Station station in stationBll.GetAll())
            {
                int rowIndex = dgvStation.Rows.Add(
                    station.StationName,
                    station.GrabCommand,
                    station.DropCommand,
                    station.EstTimeMs);
                dgvStation.Rows[rowIndex].Tag = station;
            }
        }

        /// <summary>
        /// 把已启用药品填入下方表格，并选中它当前绑定的工位。
        /// 停用药品不出现在这里，避免被重新绑到工位。
        /// </summary>
        private void LoadDrugs()
        {
            dgvDrugBinding.Rows.Clear();
            foreach (Drug drug in drugBll.GetEnabled())
            {
                int rowIndex = dgvDrugBinding.Rows.Add(drug.DrugName, drug.StationId);
                dgvDrugBinding.Rows[rowIndex].Tag = drug;
            }
        }

        /// <summary>
        /// 从上方表格收集待保存的工位。延时必须是整数。
        /// </summary>
        private List<Station> CollectStations()
        {
            List<Station> stations = new List<Station>();
            foreach (DataGridViewRow row in dgvStation.Rows)
            {
                Station source = row.Tag as Station;
                if (source == null)
                {
                    continue;
                }

                int estTimeMs;
                string timeText = Convert.ToString(row.Cells[colEstTimeMs.Name].Value);
                if (!int.TryParse(timeText, out estTimeMs))
                {
                    throw new ArgumentException(source.StationName + "的延时必须是整数");
                }

                stations.Add(new Station
                {
                    StationId = source.StationId,
                    StationName = source.StationName,
                    GrabCommand = Convert.ToString(row.Cells[colGrabCommand.Name].Value),
                    DropCommand = Convert.ToString(row.Cells[colDropCommand.Name].Value),
                    EstTimeMs = estTimeMs
                });
            }
            return stations;
        }
    }
}
