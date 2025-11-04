using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using CNXML_HVA.Models;

namespace CNXML_HVA
{
    public partial class San : Form
    {
        private string xmlFieldsPath;
        private string xmlFieldTypesPath;
        private BindingSource bindingFields;
        private List<Field> allFields;
        private List<FieldType> allFieldTypes;
        private string currentStatusFilter = "All";
        private const string SEARCH_PLACEHOLDER = "Tìm theo tên, mã sân...";

        public San()
        {
            InitializeComponent();
            // mở comment chỗ này là đc
            //             xmlFieldsPath = Path.Combine(Application.StartupPath, "Fields.xml");
            // xmlFieldTypesPath = Path.Combine(Application.StartupPath, "FieldTypes.xml");

              xmlFieldsPath = DataPaths.GetXmlFilePath("Fields.xml");
            xmlFieldTypesPath = DataPaths.GetXmlFilePath("FieldTypes.xml");
           
            bindingFields = new BindingSource();
            allFields = new List<Field>();
            allFieldTypes = new List<FieldType>();
        }

        private void San_Load(object sender, EventArgs e)
        {
            SetupTooltips();
            LoadFieldTypes();
            LoadFields();
            SetupTypeFilter();
            SetupDataGridView();
        }

        #region Setup Methods

        private void SetupTooltips()
        {
            toolTip.SetToolTip(btnAddField, "Thêm sân mới (Ctrl+N)");
            toolTip.SetToolTip(btnImportXml, "Nhập dữ liệu từ file XML");
            toolTip.SetToolTip(btnExportCsv, "Xuất dữ liệu ra file CSV");
            toolTip.SetToolTip(txtSearch, "Tìm kiếm theo tên hoặc mã sân (Ctrl+F)");
        }

        private void SetupTypeFilter()
        {
            cmbTypeFilter.Items.Add("-- Tất cả loại sân --");
            foreach (var type in allFieldTypes)
            {
                cmbTypeFilter.Items.Add(type);
            }
            cmbTypeFilter.SelectedIndex = 0;
        }

        private void SetupDataGridView()
        {
            dgvFields.AutoGenerateColumns = false;
            dgvFields.DataSource = bindingFields;
        }

        #endregion

        #region Load Data from XML

        private void LoadFieldTypes()
        {
            try
            {
                if (!File.Exists(xmlFieldTypesPath))
                {
                    MessageBox.Show("File FieldTypes.xml không tồn tại!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                XDocument doc = XDocument.Load(xmlFieldTypesPath);
                allFieldTypes = doc.Descendants("field_type").Select(ft => new FieldType
                {
                    Id = ft.Element("id")?.Value,
                    Name = ft.Element("name")?.Value,
                    Code = ft.Element("code")?.Value,
                    SizeDisplay = ft.Element("size_display")?.Value,
                    PlayersPerTeam = int.TryParse(ft.Element("players_per_team")?.Value, out int ppt) ? ppt : 0,
                    TotalCapacity = int.TryParse(ft.Element("total_capacity")?.Value, out int tc) ? tc : 0,
                    SurfaceType = ft.Element("surface_type")?.Value,
                    BasePrice = decimal.TryParse(ft.Element("base_price")?.Value, out decimal bp) ? bp : 0,
                    Description = ft.Element("description")?.Value,
                    Status = ft.Element("status")?.Value
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc FieldTypes.xml: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFields()
        {
            try
            {
                if (!File.Exists(xmlFieldsPath))
                {
                    MessageBox.Show("File Fields.xml không tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                XDocument doc = XDocument.Load(xmlFieldsPath);
                allFields = doc.Descendants("field").Select(f => new Field
                {
                    Id = f.Element("id")?.Value,
                    Name = f.Element("name")?.Value,
                    FieldTypeId = f.Element("field_type_id")?.Value,
                    BranchId = f.Element("branch_id")?.Value,
                    Address = new Address
                    {
                        City = f.Element("address")?.Element("city")?.Value,
                        District = f.Element("address")?.Element("district")?.Value,
                        Street = f.Element("address")?.Element("street")?.Value,
                        HouseNumber = f.Element("address")?.Element("house_number")?.Value
                    },
                    PricePerHour = decimal.TryParse(f.Element("price_per_hour")?.Value, out decimal price) ? price : 0,
                    Capacity = int.TryParse(f.Element("capacity")?.Value, out int cap) ? cap : 0,
                    Description = f.Element("description")?.Value,
                    Facilities = f.Element("facilities")?.Value,
                    Status = f.Element("status")?.Value,
                    CreatedDate = DateTime.TryParse(f.Element("created_date")?.Value, out DateTime cd) ? cd : DateTime.Now,
                    LastMaintenance = DateTime.TryParse(f.Element("last_maintenance")?.Value, out DateTime lm) ? lm : DateTime.Now,
                    NextBooking = "Chưa có lịch" // Mock data
                }).ToList();

                // Map Type Names
                foreach (var field in allFields)
                {
                    var fieldType = allFieldTypes.FirstOrDefault(ft => ft.Id == field.FieldTypeId);
                    field.TypeName = fieldType?.Name ?? "N/A";
                }

                bindingFields.DataSource = allFields;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc Fields.xml: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Save Data to XML

        private void SaveFieldsToXml()
        {
            try
            {
                XDocument doc = new XDocument(
                    new XDeclaration("1.0", "UTF-8", null),
                    new XElement("fields",
                        allFields.Select(f => new XElement("field",
                            new XElement("id", f.Id),
                            new XElement("name", f.Name),
                            new XElement("field_type_id", f.FieldTypeId),
                            new XElement("branch_id", f.BranchId),
                            new XElement("address",
                                new XElement("city", f.Address.City),
                                new XElement("district", f.Address.District),
                                new XElement("street", f.Address.Street),
                                new XElement("house_number", f.Address.HouseNumber)
                            ),
                            new XElement("price_per_hour", f.PricePerHour),
                            new XElement("capacity", f.Capacity),
                            new XElement("description", f.Description),
                            new XElement("facilities", f.Facilities),
                            new XElement("status", f.Status),
                            new XElement("created_date", f.CreatedDate.ToString("yyyy-MM-dd")),
                            new XElement("last_maintenance", f.LastMaintenance.ToString("yyyy-MM-dd"))
                        ))
                    )
                );

                doc.Save(xmlFieldsPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu Fields.xml: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Filter & Search

        private void ApplyFilters()
        {
            var filtered = allFields.AsEnumerable();

            // Filter by search text
            string searchText = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchText) && searchText != SEARCH_PLACEHOLDER)
            {
                filtered = filtered.Where(f =>
                    (f.Name?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (f.Id?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                );
            }

            // Filter by type
            if (cmbTypeFilter.SelectedIndex > 0 && cmbTypeFilter.SelectedItem is FieldType selectedType)
            {
                filtered = filtered.Where(f => f.FieldTypeId == selectedType.Id);
            }

            // Filter by status
            if (currentStatusFilter != "All")
            {
                filtered = filtered.Where(f => f.Status == currentStatusFilter);
            }

            bindingFields.DataSource = filtered.ToList();
            bindingFields.ResetBindings(false);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == SEARCH_PLACEHOLDER)
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.FromArgb(43, 43, 43);
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = SEARCH_PLACEHOLDER;
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void cmbTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void btnStatusFilter_Click(object sender, EventArgs e)
        {
            contextMenuStatus.Show(btnStatusFilter, new Point(0, btnStatusFilter.Height));
        }

        private void menuItemStatus_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem == null) return;

            if (menuItem == menuItemAll)
                currentStatusFilter = "All";
            else if (menuItem == menuItemAvailable)
                currentStatusFilter = "Available";
            else if (menuItem == menuItemBooked)
                currentStatusFilter = "Booked";
            else if (menuItem == menuItemMaintenance)
                currentStatusFilter = "Maintenance";

            btnStatusFilter.Text = $"📊 {menuItem.Text} ▼";
            ApplyFilters();
        }

        #endregion

        #region DataGridView Events

        private void dgvFields_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFields.SelectedRows.Count > 0)
            {
                var field = dgvFields.SelectedRows[0].DataBoundItem as Field;
                if (field != null)
                {
                    ShowFieldDetails(field);
                }
            }
        }

        private void dgvFields_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var field = dgvFields.Rows[e.RowIndex].DataBoundItem as Field;
            if (field == null) return;

            // Edit button
            if (dgvFields.Columns[e.ColumnIndex].Name == "colEdit")
            {
                OpenFieldForm(field);
            }
            // Delete button
            else if (dgvFields.Columns[e.ColumnIndex].Name == "colDelete")
            {
                DeleteField(field);
            }
            // Schedule button
            else if (dgvFields.Columns[e.ColumnIndex].Name == "colSchedule")
            {
                MessageBox.Show($"Chức năng xem lịch đặt sân '{field.Name}' đang được phát triển!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Field Details Panel

        private void ShowFieldDetails(Field field)
        {
            if (field == null)
            {
                pnlFieldDetails.Visible = false;
                return;
            }

            lblId.Text = field.Id ?? "N/A";
            lblName.Text = field.Name ?? "N/A";
            lblType.Text = field.TypeName ?? "N/A";
            lblStatus.Text = field.Status ?? "N/A";
            lblPrice.Text = field.PricePerHour.ToString("N0") + " VNĐ";
            txtDescription.Text = field.Description ?? "Không có mô tả";

            // Mock image - in production, load from file or database
            pictureBoxPhoto.Image = null;
            pictureBoxPhoto.BackColor = Color.LightGray;

            pnlFieldDetails.Visible = true;
            pnlFieldDetails.Tag = field; // Store reference
        }

        private void btnCloseDetail_Click(object sender, EventArgs e)
        {
            pnlFieldDetails.Visible = false;
        }

        private void btnEditField_Click(object sender, EventArgs e)
        {
            var field = pnlFieldDetails.Tag as Field;
            if (field != null)
            {
                OpenFieldForm(field);
            }
        }

        #endregion

        #region CRUD Operations

        private void btnAddField_Click(object sender, EventArgs e)
        {
            OpenFieldForm(null);
        }

        private void OpenFieldForm(Field fieldToEdit)
        {
            using (var form = new FieldForm(fieldToEdit, allFieldTypes))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (fieldToEdit == null)
                    {
                        // Add new
                        allFields.Add(form.EditedField);
                    }
                    else
                    {
                        // Update existing
                        var index = allFields.FindIndex(f => f.Id == fieldToEdit.Id);
                        if (index >= 0)
                        {
                            allFields[index] = form.EditedField;
                        }
                    }

                    SaveFieldsToXml();
                    ApplyFilters();
                    MessageBox.Show("Lưu thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void DeleteField(Field field)
        {
            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sân '{field.Name}'?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                allFields.Remove(field);
                SaveFieldsToXml();
                ApplyFilters();
                pnlFieldDetails.Visible = false;
                MessageBox.Show("Xóa thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Import/Export

        private void btnImportXml_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
                ofd.Title = "Chọn file XML để import";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.Copy(ofd.FileName, xmlFieldsPath, true);
                        LoadFields();
                        ApplyFilters();
                        MessageBox.Show("Import dữ liệu thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi import: {ex.Message}", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Files (*.csv)|*.csv";
                sfd.Title = "Xuất dữ liệu ra CSV";
                sfd.FileName = $"Fields_{DateTime.Now:yyyyMMdd}.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var csv = new StringBuilder();
                        csv.AppendLine("Mã sân,Tên sân,Loại sân,Trạng thái,Giá/giờ,Địa chỉ,Mô tả");

                        var currentList = bindingFields.DataSource as List<Field>;
                        if (currentList != null)
                        {
                            foreach (var field in currentList)
                            {
                                csv.AppendLine($"\"{field.Id}\",\"{field.Name}\",\"{field.TypeName}\"," +
                                    $"\"{field.Status}\",{field.PricePerHour}," +
                                    $"\"{field.Address}\",\"{field.Description}\"");
                            }
                        }

                        File.WriteAllText(sfd.FileName, csv.ToString(), Encoding.UTF8);
                        MessageBox.Show("Export CSV thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi export: {ex.Message}", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        #region Keyboard Shortcuts

        private void San_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
            {
                btnAddField_Click(sender, e);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                txtSearch.Focus();
                txtSearch.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        private void pnlFieldDetails_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    #region FieldForm Dialog

    public class FieldForm : Form
    {
        private Field originalField;
        private List<FieldType> fieldTypes;
        public Field EditedField { get; private set; }

        // Controls
        private TextBox txtId, txtName, txtCity, txtDistrict, txtStreet, txtHouseNumber, txtDescription;
        private ComboBox cmbType, cmbStatus;
        private NumericUpDown numPrice, numCapacity;
        private Button btnSave, btnCancel;
        private Label lblValidation;

        public FieldForm(Field field, List<FieldType> types)
        {
            originalField = field;
            fieldTypes = types;
            InitializeFormControls();
            LoadData();
        }

        private void InitializeFormControls()
        {
            this.Text = originalField == null ? "Thêm sân mới" : "Chỉnh sửa sân";
            this.Size = new Size(600, 650);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            int y = 20;
            int labelX = 20;
            int controlX = 150;
            int controlWidth = 400;

            // ID
            AddLabel("Mã sân:", labelX, y);
            txtId = AddTextBox(controlX, y, controlWidth);
            txtId.ReadOnly = originalField != null;
            y += 40;

            // Name
            AddLabel("Tên sân:", labelX, y);
            txtName = AddTextBox(controlX, y, controlWidth);
            y += 40;

            // Type
            AddLabel("Loại sân:", labelX, y);
            cmbType = new ComboBox();
            cmbType.Location = new Point(controlX, y);
            cmbType.Size = new Size(controlWidth, 25);
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.Font = new Font("Segoe UI", 10F);
            this.Controls.Add(cmbType);
            y += 40;

            // Address - City
            AddLabel("Thành phố:", labelX, y);
            txtCity = AddTextBox(controlX, y, controlWidth);
            y += 40;

            // District
            AddLabel("Quận/Huyện:", labelX, y);
            txtDistrict = AddTextBox(controlX, y, controlWidth);
            y += 40;

            // Street
            AddLabel("Đường:", labelX, y);
            txtStreet = AddTextBox(controlX, y, controlWidth);
            y += 40;

            // House Number
            AddLabel("Số nhà:", labelX, y);
            txtHouseNumber = AddTextBox(controlX, y, controlWidth);
            y += 40;

            // Price
            AddLabel("Giá/giờ (VNĐ):", labelX, y);
            numPrice = new NumericUpDown();
            numPrice.Location = new Point(controlX, y);
            numPrice.Size = new Size(controlWidth, 25);
            numPrice.Maximum = 10000000;
            numPrice.Minimum = 0;
            numPrice.Font = new Font("Segoe UI", 10F);
            this.Controls.Add(numPrice);
            y += 40;

            // Capacity
            AddLabel("Sức chứa:", labelX, y);
            numCapacity = new NumericUpDown();
            numCapacity.Location = new Point(controlX, y);
            numCapacity.Size = new Size(controlWidth, 25);
            numCapacity.Maximum = 100;
            numCapacity.Minimum = 1;
            numCapacity.Font = new Font("Segoe UI", 10F);
            this.Controls.Add(numCapacity);
            y += 40;

            // Status
            AddLabel("Trạng thái:", labelX, y);
            cmbStatus = new ComboBox();
            cmbStatus.Location = new Point(controlX, y);
            cmbStatus.Size = new Size(controlWidth, 25);
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Font = new Font("Segoe UI", 10F);
            cmbStatus.Items.AddRange(new object[] { "Available", "Booked", "Maintenance" });
            this.Controls.Add(cmbStatus);
            y += 40;

            // Description
            AddLabel("Mô tả:", labelX, y);
            txtDescription = new TextBox();
            txtDescription.Location = new Point(controlX, y);
            txtDescription.Size = new Size(controlWidth, 80);
            txtDescription.Multiline = true;
            txtDescription.Font = new Font("Segoe UI", 10F);
            txtDescription.ScrollBars = ScrollBars.Vertical;
            this.Controls.Add(txtDescription);
            y += 90;

            // Validation Label
            lblValidation = new Label();
            lblValidation.Location = new Point(labelX, y);
            lblValidation.Size = new Size(550, 20);
            lblValidation.ForeColor = Color.Red;
            lblValidation.Font = new Font("Segoe UI", 9F);
            lblValidation.Visible = false;
            this.Controls.Add(lblValidation);
            y += 30;

            // Buttons
            btnSave = new Button();
            btnSave.Text = "💾 Lưu";
            btnSave.Location = new Point(200, y);
            btnSave.Size = new Size(120, 40);
            btnSave.BackColor = Color.FromArgb(30, 140, 58);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.Click += btnSave_Click;
            this.Controls.Add(btnSave);

            btnCancel = new Button();
            btnCancel.Text = "❌ Hủy";
            btnCancel.Location = new Point(340, y);
            btnCancel.Size = new Size(120, 40);
            btnCancel.BackColor = Color.White;
            btnCancel.ForeColor = Color.FromArgb(43, 43, 43);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F);
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            this.Controls.Add(btnCancel);
        }

        private Label AddLabel(string text, int x, int y)
        {
            var label = new Label();
            label.Text = text;
            label.Location = new Point(x, y + 3);
            label.Size = new Size(120, 20);
            label.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label.ForeColor = Color.FromArgb(43, 43, 43);
            this.Controls.Add(label);
            return label;
        }

        private TextBox AddTextBox(int x, int y, int width)
        {
            var textBox = new TextBox();
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(width, 25);
            textBox.Font = new Font("Segoe UI", 10F);
            this.Controls.Add(textBox);
            return textBox;
        }

        private void LoadData()
        {
            // Load field types
            foreach (var type in fieldTypes)
            {
                cmbType.Items.Add(type);
            }

            if (originalField != null)
            {
                // Edit mode
                txtId.Text = originalField.Id;
                txtName.Text = originalField.Name;
                
                var selectedType = fieldTypes.FirstOrDefault(t => t.Id == originalField.FieldTypeId);
                if (selectedType != null)
                    cmbType.SelectedItem = selectedType;

                txtCity.Text = originalField.Address?.City;
                txtDistrict.Text = originalField.Address?.District;
                txtStreet.Text = originalField.Address?.Street;
                txtHouseNumber.Text = originalField.Address?.HouseNumber;
                numPrice.Value = originalField.PricePerHour;
                numCapacity.Value = originalField.Capacity;
                cmbStatus.SelectedItem = originalField.Status;
                txtDescription.Text = originalField.Description;
            }
            else
            {
                // Add mode - generate ID
                txtId.Text = GenerateNewFieldId();
                cmbType.SelectedIndex = 0;
                cmbStatus.SelectedIndex = 0;
                numPrice.Value = 100000;
                numCapacity.Value = 10;
            }
        }

        private string GenerateNewFieldId()
        {
            // Simple ID generation - in production, check existing IDs
            return "F" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            var selectedType = cmbType.SelectedItem as FieldType;

            EditedField = new Field
            {
                Id = txtId.Text.Trim(),
                Name = txtName.Text.Trim(),
                FieldTypeId = selectedType?.Id,
                TypeName = selectedType?.Name,
                BranchId = originalField?.BranchId ?? "B01", // Default or keep original
                Address = new Address
                {
                    City = txtCity.Text.Trim(),
                    District = txtDistrict.Text.Trim(),
                    Street = txtStreet.Text.Trim(),
                    HouseNumber = txtHouseNumber.Text.Trim()
                },
                PricePerHour = numPrice.Value,
                Capacity = (int)numCapacity.Value,
                Status = cmbStatus.SelectedItem?.ToString() ?? "Available",
                Description = txtDescription.Text.Trim(),
                Facilities = originalField?.Facilities ?? "Cơ bản",
                CreatedDate = originalField?.CreatedDate ?? DateTime.Now,
                LastMaintenance = DateTime.Now,
                NextBooking = "Chưa có lịch"
            };

            this.DialogResult = DialogResult.OK;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                ShowValidation("Vui lòng nhập mã sân!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowValidation("Vui lòng nhập tên sân!");
                return false;
            }

            if (cmbType.SelectedIndex < 0)
            {
                ShowValidation("Vui lòng chọn loại sân!");
                return false;
            }

            if (cmbStatus.SelectedIndex < 0)
            {
                ShowValidation("Vui lòng chọn trạng thái!");
                return false;
            }

            return true;
        }

        private void ShowValidation(string message)
        {
            lblValidation.Text = "⚠️ " + message;
            lblValidation.Visible = true;
        }
    }

    #endregion
}
