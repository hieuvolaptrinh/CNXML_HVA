using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using CNXML_HVA.Models;

namespace CNXML_HVA
{
    public partial class LoaiSan : Form
    {
        private string xmlFilePath;
        private BindingSource bindingFieldTypes;
        private List<FieldType> allFieldTypes;
        private const string SEARCH_PLACEHOLDER = "Tìm loại sân...";

        public LoaiSan()
        {
            InitializeComponent();
            xmlFilePath = Path.Combine(Application.StartupPath, "FieldTypes.xml");
            bindingFieldTypes = new BindingSource();
            allFieldTypes = new List<FieldType>();
        }

        private void LoaiSan_Load(object sender, EventArgs e)
        {
            SetupTooltips();
            SetupDataGridView();
            LoadFieldTypesFromXml();
        }

        #region Setup Methods

        private void SetupTooltips()
        {
            toolTip.SetToolTip(btnAddType, "Thêm loại sân mới (Ctrl+N)");
            toolTip.SetToolTip(btnEditType, "Chỉnh sửa loại sân đã chọn");
            toolTip.SetToolTip(btnDeleteType, "Xóa loại sân đã chọn (Delete)");
            toolTip.SetToolTip(txtTypeSearch, "Tìm kiếm loại sân theo tên hoặc mã");
        }

        private void SetupDataGridView()
        {
            dgvFieldTypes.AutoGenerateColumns = false;
            dgvFieldTypes.DataSource = bindingFieldTypes;
        }

        #endregion

        #region Load Data from XML

        private void LoadFieldTypesFromXml()
        {
            try
            {
                // Load fieldtypes.xml với cấu trúc XML đầy đủ
                if (!File.Exists(xmlFilePath))
                {
                    MessageBox.Show("File FieldTypes.xml không tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                XDocument doc = XDocument.Load(xmlFilePath);
                allFieldTypes = doc.Descendants("field_type").Select(ft => new FieldType
                {
                    Id = ft.Attribute("id")?.Value ?? "",
                    Name = ft.Element("name")?.Value ?? "",
                    Code = ft.Element("code")?.Value ?? "",
                    
                    // Dimensions
                    Length = decimal.TryParse(ft.Element("dimensions")?.Element("length")?.Value, out var len) ? len : 0,
                    Width = decimal.TryParse(ft.Element("dimensions")?.Element("width")?.Value, out var wid) ? wid : 0,
                    DimensionUnit = ft.Element("dimensions")?.Element("unit")?.Value ?? "mét",
                    SizeDisplay = ft.Element("size_display")?.Value ?? "",
                    
                    // Players
                    PlayersPerTeam = int.TryParse(ft.Element("players_per_team")?.Value, out int ppt) ? ppt : 0,
                    TotalCapacity = int.TryParse(ft.Element("total_capacity")?.Value, out int tc) ? tc : 0,
                    
                    // Goal Size
                    GoalHeight = decimal.TryParse(ft.Element("goal_size")?.Element("height")?.Value, out var gh) ? gh : 0,
                    GoalWidth = decimal.TryParse(ft.Element("goal_size")?.Element("width")?.Value, out var gw) ? gw : 0,
                    GoalUnit = ft.Element("goal_size")?.Element("unit")?.Value ?? "mét",
                    
                    // Field Info
                    SurfaceType = ft.Element("surface_type")?.Value ?? "",
                    BasePrice = decimal.TryParse(ft.Element("base_price")?.Value, out decimal bp) ? bp : 0,
                    PeakHourMultiplier = decimal.TryParse(ft.Element("peak_hour_multiplier")?.Value, out var pm) ? pm : 1.5m,
                    WeekendMultiplier = decimal.TryParse(ft.Element("weekend_multiplier")?.Value, out var wm) ? wm : 1.3m,
                    
                    // Details
                    Description = ft.Element("description")?.Value ?? "",
                    Features = ft.Element("features")?.Value ?? "",
                    MinimumBookingHours = int.TryParse(ft.Element("minimum_booking_hours")?.Value, out var minh) ? minh : 1,
                    MaximumBookingHours = int.TryParse(ft.Element("maximum_booking_hours")?.Value, out var maxh) ? maxh : 4,
                    Status = ft.Element("status")?.Value ?? "Active"
                }).ToList();

                bindingFieldTypes.DataSource = allFieldTypes;
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc FieldTypes.xml: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Save Data to XML

        private void SaveFieldTypesToXml()
        {
            try
            {
                XDocument doc = new XDocument(
                    new XDeclaration("1.0", "UTF-8", null),
                    new XElement("field_types",
                        allFieldTypes.Select(ft => new XElement("field_type",
                            new XAttribute("id", ft.Id ?? ""),
                            new XElement("name", ft.Name ?? ""),
                            new XElement("code", ft.Code ?? ""),
                            new XElement("dimensions",
                                new XElement("length", ft.Length),
                                new XElement("width", ft.Width),
                                new XElement("unit", ft.DimensionUnit ?? "mét")
                            ),
                            new XElement("size_display", ft.SizeDisplay ?? ft.GetFullDimensions()),
                            new XElement("players_per_team", ft.PlayersPerTeam),
                            new XElement("total_capacity", ft.TotalCapacity),
                            new XElement("goal_size",
                                new XElement("height", ft.GoalHeight),
                                new XElement("width", ft.GoalWidth),
                                new XElement("unit", ft.GoalUnit ?? "mét")
                            ),
                            new XElement("surface_type", ft.SurfaceType ?? "Cỏ nhân tạo"),
                            new XElement("base_price", ft.BasePrice),
                            new XElement("peak_hour_multiplier", ft.PeakHourMultiplier),
                            new XElement("weekend_multiplier", ft.WeekendMultiplier),
                            new XElement("description", ft.Description ?? ""),
                            new XElement("features", ft.Features ?? ""),
                            new XElement("minimum_booking_hours", ft.MinimumBookingHours),
                            new XElement("maximum_booking_hours", ft.MaximumBookingHours),
                            new XElement("status", ft.Status ?? "Active")
                        ))
                    )
                );

                doc.Save(xmlFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu FieldTypes.xml: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Search & Filter

        private void ApplyFilter()
        {
            string searchText = txtTypeSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText) || searchText == SEARCH_PLACEHOLDER)
            {
                bindingFieldTypes.DataSource = allFieldTypes;
            }
            else
            {
                var filtered = allFieldTypes.Where(ft =>
                    (ft.Name?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (ft.Id?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (ft.Description?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                ).ToList();

                bindingFieldTypes.DataSource = filtered;
            }
            bindingFieldTypes.ResetBindings(false);
        }

        private void txtTypeSearch_TextChanged(object sender, EventArgs e)
        {
            // TODO: filter grid
            ApplyFilter();
        }

        private void txtTypeSearch_Enter(object sender, EventArgs e)
        {
            if (txtTypeSearch.Text == SEARCH_PLACEHOLDER)
            {
                txtTypeSearch.Text = "";
                txtTypeSearch.ForeColor = Color.FromArgb(43, 43, 43);
            }
        }

        private void txtTypeSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTypeSearch.Text))
            {
                txtTypeSearch.Text = SEARCH_PLACEHOLDER;
                txtTypeSearch.ForeColor = Color.Gray;
            }
        }

        #endregion

        #region Button Event Handlers

        private void btnAddType_Click(object sender, EventArgs e)
        {
            // TODO: open FieldTypeEditForm (Add)
            using (var form = new FieldTypeEditForm(null))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    allFieldTypes.Add(form.EditedFieldType);
                    SaveFieldTypesToXml();
                    ApplyFilter();
                    MessageBox.Show("Thêm loại sân thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnEditType_Click(object sender, EventArgs e)
        {
            // TODO: open FieldTypeEditForm (Edit) with selected record
            if (dgvFieldTypes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn loại sân cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedType = dgvFieldTypes.SelectedRows[0].DataBoundItem as FieldType;
            if (selectedType == null) return;

            using (var form = new FieldTypeEditForm(selectedType))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var index = allFieldTypes.FindIndex(ft => ft.Id == selectedType.Id);
                    if (index >= 0)
                    {
                        allFieldTypes[index] = form.EditedFieldType;
                    }

                    SaveFieldTypesToXml();
                    ApplyFilter();
                    MessageBox.Show("Cập nhật loại sân thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnDeleteType_Click(object sender, EventArgs e)
        {
            // TODO: confirmation dialog and delete placeholder
            if (dgvFieldTypes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn loại sân cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedType = dgvFieldTypes.SelectedRows[0].DataBoundItem as FieldType;
            if (selectedType == null) return;

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa loại sân '{selectedType.Name}'?\n\nLưu ý: Hành động này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                allFieldTypes.Remove(selectedType);
                SaveFieldTypesToXml();
                ApplyFilter();
                MessageBox.Show("Xóa loại sân thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region DataGridView Events

        private void dgvFieldTypes_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void dgvFieldTypes_DoubleClick(object sender, EventArgs e)
        {
            btnEditType_Click(sender, e);
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = dgvFieldTypes.SelectedRows.Count > 0;
            btnEditType.Enabled = hasSelection;
            btnDeleteType.Enabled = hasSelection;
        }

        #endregion

        #region Keyboard Shortcuts

        private void LoaiSan_KeyDown(object sender, KeyEventArgs e)
        {
            // Ctrl+N = Thêm loại
            if (e.Control && e.KeyCode == Keys.N)
            {
                btnAddType_Click(sender, e);
                e.Handled = true;
            }
            // Del = Xóa chọn
            else if (e.KeyCode == Keys.Delete)
            {
                if (dgvFieldTypes.Focused && dgvFieldTypes.SelectedRows.Count > 0)
                {
                    btnDeleteType_Click(sender, e);
                    e.Handled = true;
                }
            }
        }

        #endregion
    }
}
