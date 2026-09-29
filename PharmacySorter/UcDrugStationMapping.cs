using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PharmacySorter
{
    /// <summary>
    /// 工位指令配置。
    /// 维护三个固定工位的抓取指令、放置指令和动作延时。
    /// 药品绑定已移到药品字典，本页不再维护药品与工位的对应关系。
    /// </summary>
    public partial class UcDrugStationMapping : UserControl
    {
        private readonly StationBLL stationBll = new StationBLL();

        public UcDrugStationMapping()
        {
            InitializeComponent();
            dgvStation.AutoGenerateColumns = false;
            Load += UcDrugStationMapping_Load;
        }

        private void UcDrugStationMapping_Load(object sender, EventArgs e)
        {
            LoadStations();
        }

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