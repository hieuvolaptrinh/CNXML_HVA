
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace CNXML_HVA
{
    public partial class DungCu : Form
    {
        private string xmlFilePath;
        private XmlDocument xmlDoc;
        private bool isEditing = false;
        private bool isAdding = false;
        private DataTable equipmentTable;

        public DungCu()
        {
            InitializeComponent();
            xmlFilePath = DataPaths.GetXmlFilePath("Equipments.xml");
            DataPaths.EnsureXmlFileExists("Equipments.xml");
            xmlDoc = new XmlDocument();
            InitializeDataTable();
        }

        private void InitializeDataTable()
        {
            equipmentTable = new DataTable();
            equipmentTable.Columns.Add("Mã DC", typeof(string));
            equipmentTable.Columns.Add("Tên dụng cụ", typeof(string));
            equipmentTable.Columns.Add("Danh mục", typeof(string));
            equipmentTable.Columns.Add("Thương hiệu", typeof(string));
            equipmentTable.Columns.Add("Model", typeof(string));
            equipmentTable.Columns.Add("Tổng SL", typeof(int));
            equipmentTable.Columns.Add("SL có sẵn", typeof(int));
            equipmentTable.Columns.Add("Giá thuê", typeof(decimal));
            equipmentTable.Columns.Add("Giá mua", typeof(decimal));
            equipmentTable.Columns.Add("Tình trạng", typeof(string));
            equipmentTable.Columns.Add("Chi nhánh", typeof(string));
            equipmentTable.Columns.Add("Nhà cung cấp", typeof(string));
            equipmentTable.Columns.Add("Ngày mua", typeof(DateTime));
            equipmentTable.Columns.Add("BH (tháng)", typeof(int));
            equipmentTable.Columns.Add("Trạng thái", typeof(string));
            equipmentTable.Columns.Add("Mô tả", typeof(string));
            equipmentTable.Columns.Add("URL hình ảnh", typeof(string));

            dataGridViewEquipment.DataSource = equipmentTable;
        }

        private void DungCu_Load(object sender, EventArgs e)
        {
            LoadEquipmentsFromXML();
            SetupDataGridView();
            SetEditMode(false);
            AddImageUrlControl();
        }
        
        private void AddImageUrlControl()
        {
            // Add image URL textbox to Description tab if it doesn't exist
            if (this.Controls.Find("textBoxUrl2", true).Length == 0)
            {
                // Find the Description tab
                TabPage descTab = tabPageDescription;
                if (descTab != null)
                {
                    // Create label for image URL
                    Label lblImageUrl = new Label();
                    lblImageUrl.Name = "labelUrl2";
                    lblImageUrl.Text = "URL hình ảnh:";
                    lblImageUrl.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    lblImageUrl.ForeColor = Color.FromArgb(27, 94, 32);
                    lblImageUrl.AutoSize = true;
                    lblImageUrl.Location = new Point(20, 270);
                    
                    // Create textbox for image URL
                    TextBox txtImageUrl = new TextBox();
                    txtImageUrl.Name = "textBoxUrl2";
                    txtImageUrl.Font = new Font("Segoe UI", 9.5F);
                    txtImageUrl.Location = new Point(24, 295);
                    txtImageUrl.Size = new Size(481, 29);
                    txtImageUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    
                    descTab.Controls.Add(lblImageUrl);
                    descTab.Controls.Add(txtImageUrl);
                }
            }
        }

        private void LoadEquipmentsFromXML()
        {
            try
            {
                if (File.Exists(xmlFilePath))
                {
                    xmlDoc.Load(xmlFilePath);
                    equipmentTable.Clear();

                    XmlNodeList equipmentNodes = xmlDoc.SelectNodes("//equipment");
                    foreach (XmlNode equipmentNode in equipmentNodes)
                    {
                        DataRow row = equipmentTable.NewRow();
                        row["Mã DC"] = GetNodeValue(equipmentNode, "@id");
                        row["Tên dụng cụ"] = GetNodeValue(equipmentNode, "name");
                        row["Danh mục"] = GetNodeValue(equipmentNode, "category");
                        row["Thương hiệu"] = GetNodeValue(equipmentNode, "brand");
                        row["Model"] = GetNodeValue(equipmentNode, "model");
                        row["Tổng SL"] = ParseInt(GetNodeValue(equipmentNode, "quantity_total"));
                        row["SL có sẵn"] = ParseInt(GetNodeValue(equipmentNode, "quantity_available"));
                        row["Giá thuê"] = ParseDecimal(GetNodeValue(equipmentNode, "rental_price"));
                        row["Giá mua"] = ParseDecimal(GetNodeValue(equipmentNode, "purchase_price"));
                        row["Tình trạng"] = GetNodeValue(equipmentNode, "condition");
                        row["Chi nhánh"] = GetNodeValue(equipmentNode, "branch_id");
                        row["Nhà cung cấp"] = GetNodeValue(equipmentNode, "supplier");
                        
                        string purchaseDate = GetNodeValue(equipmentNode, "purchase_date");
                        row["Ngày mua"] = string.IsNullOrEmpty(purchaseDate) ? DateTime.Now : DateTime.Parse(purchaseDate);
                        
                        row["BH (tháng)"] = ParseInt(GetNodeValue(equipmentNode, "warranty_period"));
                        row["Trạng thái"] = GetNodeValue(equipmentNode, "status");
                        row["Mô tả"] = GetNodeValue(equipmentNode, "description");
                        row["URL hình ảnh"] = GetNodeValue(equipmentNode, "image_url");

                        equipmentTable.Rows.Add(row);
                    }
                }
                else
                {
                    LoadEquipmentsFromXML();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetNodeValue(XmlNode parentNode, string xpath)
        {
            XmlNode node = parentNode.SelectSingleNode(xpath);
            return node?.InnerText ?? "";
        }

        private decimal ParseDecimal(string value)
        {
            if (string.IsNullOrEmpty(value)) return 0;
            decimal result;
            return decimal.TryParse(value, out result) ? result : 0;
        }

        private int ParseInt(string value)
        {
            if (string.IsNullOrEmpty(value)) return 0;
            int result;
            return int.TryParse(value, out result) ? result : 0;
        }

        private void SetupDataGridView()
        {
            dataGridViewEquipment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewEquipment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEquipment.MultiSelect = false;
            dataGridViewEquipment.ReadOnly = true;
            
            // Thiết lập màu sắc
            dataGridViewEquipment.BackgroundColor = Color.White;
            dataGridViewEquipment.GridColor = Color.FromArgb(76, 175, 80);
            dataGridViewEquipment.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 201);
            dataGridViewEquipment.DefaultCellStyle.SelectionForeColor = Color.FromArgb(27, 94, 32);
            dataGridViewEquipment.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(76, 175, 80);
            dataGridViewEquipment.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewEquipment.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // Ẩn một số cột chi tiết trong DataGridView
            if (dataGridViewEquipment.Columns.Count > 0)
            {
                HideColumn("Nhà cung cấp");
                HideColumn("Ngày mua");
                HideColumn("BH (tháng)");
                HideColumn("Trạng thái");
                HideColumn("Mô tả");
            }
        }

        private void HideColumn(string columnName)
        {
            if (dataGridViewEquipment.Columns.Contains(columnName))
            {
                dataGridViewEquipment.Columns[columnName].Visible = false;
            }
        }

        private void dataGridViewEquipment_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewEquipment.CurrentRow != null && !isAdding && !isEditing)
            {
                LoadSelectedEquipmentToForm();
            }
        }

        private void LoadSelectedEquipmentToForm()
        {
            if (dataGridViewEquipment.CurrentRow != null)
            {
                DataGridViewRow row = dataGridViewEquipment.CurrentRow;

                // Tab 1 - Cơ bản: id, name, brand, category (Tình trạng dropdown), model (Model text)
                textBoxId.Text = row.Cells["Mã DC"].Value?.ToString();
                textBoxName.Text = row.Cells["Tên dụng cụ"].Value?.ToString();
                textBoxBranch.Text = row.Cells["Thương hiệu"].Value?.ToString(); // brand
                comboBoxCondition.Text = row.Cells["Danh mục"].Value?.ToString(); // category (Tình trạng dropdown: Bóng, Trang phục...)
                comboBox.Text = row.Cells["Model"].Value?.ToString(); // model (Model dropdown/text: Premier League...)

                // Tab 2 - Chi tiết: quantity_total, quantity_available, purchase_date, warranty_period  
                textBoxManufacturer.Text = row.Cells["Tổng SL"].Value?.ToString(); // Tổng SL (reuse as text display)
                textBoxModel.Text = row.Cells["SL có sẵn"].Value?.ToString(); // SL có sẵn (reuse as text display)
                if (row.Cells["Ngày mua"].Value != null && row.Cells["Ngày mua"].Value != DBNull.Value)
                    dateTimePickerPurchaseDate.Value = Convert.ToDateTime(row.Cells["Ngày mua"].Value);
                
                // textBoxSerialNumber hiển thị warranty_period (BH tháng)
                int warrantyMonths = Convert.ToInt32(row.Cells["BH (tháng)"].Value ?? 0);
                textBoxSerialNumber.Text = warrantyMonths.ToString();

                // Tab 3 - Giá cả: purchase_price, rental_price
                numericUpDownPurchasePrice.Value = Convert.ToDecimal(row.Cells["Giá mua"].Value ?? 0);
                numericUpDownRentalPriceHour.Value = Convert.ToDecimal(row.Cells["Giá thuê"].Value ?? 0);

                // Tab 4 - Tồn kho: quantity_total, quantity_available, (quantity_rented), branch_id
                numericUpDownQuantityTotal.Value = Convert.ToDecimal(row.Cells["Tổng SL"].Value ?? 0);
                numericUpDownQuantityAvailable.Value = Convert.ToDecimal(row.Cells["SL có sẵn"].Value ?? 0);
                // SL đang thuê = Tổng SL - SL có sẵn
                int totalQty = Convert.ToInt32(row.Cells["Tổng SL"].Value ?? 0);
                int availableQty = Convert.ToInt32(row.Cells["SL có sẵn"].Value ?? 0);
                textBoxBranchId.Text = row.Cells["Chi nhánh"].Value?.ToString();

                // Tab 5 - Mô tả (description)
                textBoxDescription.Text = row.Cells["Mô tả"].Value?.ToString();
                
                // Load image_url if textbox exists (will be added to Designer)
                if (this.Controls.Find("textBoxUrl2", true).Length > 0)
                {
                    TextBox txtImageUrl = (TextBox)this.Controls.Find("textBoxUrl2", true)[0];
                    txtImageUrl.Text = row.Cells["URL hình ảnh"].Value?.ToString();
                }
            }
        }

        private void ClearForm()
        {
            // Tab 1 - Cơ bản
            textBoxId.Clear();
            textBoxName.Clear();
            textBoxBranch.Clear(); // brand
            comboBoxCondition.SelectedIndex = -1; // category (Tình trạng dropdown)
            comboBox.Text = ""; // model (Model text/dropdown)

            // Tab 2 - Chi tiết
            textBoxManufacturer.Clear(); // Tổng SL (display)
            textBoxModel.Clear(); // SL có sẵn (display)
            textBoxSerialNumber.Clear(); // warranty_period
            dateTimePickerPurchaseDate.Value = DateTime.Now;

            // Tab 3 - Giá cả
            numericUpDownPurchasePrice.Value = 0;
            numericUpDownRentalPriceHour.Value = 0;

            // Tab 4 - Tồn kho
            numericUpDownQuantityTotal.Value = 0;
            numericUpDownQuantityAvailable.Value = 0;
            textBoxBranchId.Clear();

            // Tab 5 - Mô tả
            textBoxDescription.Clear();
            
            // Clear image URL if textbox exists
            if (this.Controls.Find("textBoxUrl2", true).Length > 0)
            {
                TextBox txtImageUrl = (TextBox)this.Controls.Find("textBoxUrl2", true)[0];
                txtImageUrl.Clear();
            }
        }

        private void SetEditMode(bool editMode)
        {
            isEditing = editMode;

            // Enable/Disable các controls trong tabs (không disable tab control)
            foreach (TabPage tab in tabControlEquipmentInfo.TabPages)
            {
                foreach (Control ctrl in tab.Controls)
                {
                    if (ctrl is TableLayoutPanel tlp)
                    {
                        foreach (Control c in tlp.Controls)
                        {
                            if (!(c is Label))
                            {
                                c.Enabled = editMode;
                            }
                        }
                    }
                    else if (!(ctrl is Label))
                    {
                        ctrl.Enabled = editMode;
                    }
                }
            }

            // Buttons trong form
            buttonSave.Enabled = editMode;
            buttonCancel.Enabled = editMode;

            // Buttons trong grid
            buttonAdd.Enabled = !editMode;
            buttonEdit.Enabled = !editMode && dataGridViewEquipment.CurrentRow != null;
            buttonDelete.Enabled = !editMode && dataGridViewEquipment.CurrentRow != null;

            // Toolbar buttons
            buttonRefresh.Enabled = !editMode;
            buttonExportExcel.Enabled = !editMode;
            buttonImportXml.Enabled = !editMode;

            // DataGridView và Search
            dataGridViewEquipment.Enabled = !editMode;
            textBoxSearch.Enabled = !editMode;

            // Readonly cho textBoxId - luôn readonly
            textBoxId.ReadOnly = true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            isAdding = true;
            ClearForm();
            SetEditMode(true);
            textBoxId.Text = GenerateNewId();
            tabControlEquipmentInfo.SelectedIndex = 0; // Chuyển về tab đầu
            textBoxName.Focus();
        }

        private string GenerateNewId()
        {
            int maxId = 0;
            foreach (DataRow row in equipmentTable.Rows)
            {
                string id = row["Mã DC"].ToString();
                if (id.StartsWith("EQ"))
                {
                    if (int.TryParse(id.Substring(2), out int numId))
                    {
                        if (numId > maxId) maxId = numId;
                    }
                }
            }
            return "EQ" + (maxId + 1).ToString("000");
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewEquipment.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dụng cụ cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isAdding = false;
            SetEditMode(true);
            tabControlEquipmentInfo.SelectedIndex = 0;
            textBoxName.Focus();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewEquipment.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dụng cụ cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string equipmentName = dataGridViewEquipment.CurrentRow.Cells["Tên dụng cụ"].Value?.ToString();
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa dụng cụ '{equipmentName}'?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string equipmentId = dataGridViewEquipment.CurrentRow.Cells["Mã DC"].Value?.ToString();

                    // Xóa khỏi XML
                    XmlNode equipmentNode = xmlDoc.SelectSingleNode($"//equipment[@id='{equipmentId}']");
                    if (equipmentNode != null)
                    {
                        equipmentNode.ParentNode.RemoveChild(equipmentNode);
                        xmlDoc.Save(xmlFilePath);
                    }

                    // Xóa khỏi DataTable
                    DataRow rowToDelete = equipmentTable.Rows.Cast<DataRow>()
                        .FirstOrDefault(r => r["Mã DC"].ToString() == equipmentId);
                    if (rowToDelete != null)
                    {
                        equipmentTable.Rows.Remove(rowToDelete);
                    }

                    ClearForm();
                    MessageBox.Show("Xóa dụng cụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa dụng cụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                if (isAdding)
                {
                    AddNewEquipment();
                }
                else
                {
                    UpdateEquipment();
                }

                SetEditMode(false);
                isAdding = false;
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên dụng cụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControlEquipmentInfo.SelectedIndex = 0;
                textBoxName.Focus();
                return false;
            }

            if (comboBoxCondition.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn tình trạng (danh mục)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControlEquipmentInfo.SelectedIndex = 0;
                comboBoxCondition.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(comboBox.Text))
            {
                MessageBox.Show("Vui lòng nhập model!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControlEquipmentInfo.SelectedIndex = 0;
                comboBox.Focus();
                return false;
            }

            if (isAdding)
            {
                // Kiểm tra trùng mã khi thêm mới
                foreach (DataRow row in equipmentTable.Rows)
                {
                    if (row["Mã DC"].ToString() == textBoxId.Text)
                    {
                        MessageBox.Show("Mã dụng cụ đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tabControlEquipmentInfo.SelectedIndex = 0;
                        textBoxId.Focus();
                        return false;
                    }
                }
            }

            return true;
        }

        private void AddNewEquipment()
        {
            // Thêm vào XML
            XmlNode equipmentsNode = xmlDoc.SelectSingleNode("equipments");
            if (equipmentsNode == null)
            {
                equipmentsNode = xmlDoc.CreateElement("equipments");
                xmlDoc.AppendChild(equipmentsNode);
            }

            XmlElement newEquipment = xmlDoc.CreateElement("equipment");
            newEquipment.SetAttribute("id", textBoxId.Text);

            AddXmlElement(newEquipment, "name", textBoxName.Text);
            AddXmlElement(newEquipment, "category", comboBoxCondition.Text); // category từ comboBoxCondition (Tình trạng dropdown)
            AddXmlElement(newEquipment, "brand", textBoxBranch.Text);
            AddXmlElement(newEquipment, "model", comboBox.Text); // model từ comboBox (Model text)
            AddXmlElement(newEquipment, "quantity_total", ((int)numericUpDownQuantityTotal.Value).ToString());
            AddXmlElement(newEquipment, "quantity_available", ((int)numericUpDownQuantityAvailable.Value).ToString());
            AddXmlElement(newEquipment, "rental_price", numericUpDownRentalPriceHour.Value.ToString("0"));
            AddXmlElement(newEquipment, "purchase_price", numericUpDownPurchasePrice.Value.ToString("0"));
            AddXmlElement(newEquipment, "condition", "Tốt"); // Default condition
            AddXmlElement(newEquipment, "description", textBoxDescription.Text);
            AddXmlElement(newEquipment, "branch_id", textBoxBranchId.Text);
            AddXmlElement(newEquipment, "supplier", ""); // Supplier không có trong form
            AddXmlElement(newEquipment, "purchase_date", dateTimePickerPurchaseDate.Value.ToString("yyyy-MM-dd"));
            
            // warranty_period từ textBoxSerialNumber
            int warrantyMonths;
            if (int.TryParse(textBoxSerialNumber.Text, out warrantyMonths))
            {
                AddXmlElement(newEquipment, "warranty_period", warrantyMonths.ToString());
            }
            else
            {
                // Fallback: tính từ dateTimePicker nếu textBoxSerialNumber không phải số
                AddXmlElement(newEquipment, "warranty_period", warrantyMonths.ToString());
            }
            
            AddXmlElement(newEquipment, "status", "Active");
            
            // Add image_url if textbox exists
            if (this.Controls.Find("textBoxUrl2", true).Length > 0)
            {
                TextBox txtImageUrl = (TextBox)this.Controls.Find("textBoxUrl2", true)[0];
                AddXmlElement(newEquipment, "image_url", txtImageUrl.Text);
            }
            else
            {
                AddXmlElement(newEquipment, "image_url", "");
            }

            equipmentsNode.AppendChild(newEquipment);
            xmlDoc.Save(xmlFilePath);

            // Thêm vào DataTable
            DataRow newRow = equipmentTable.NewRow();
            PopulateDataRow(newRow);
            equipmentTable.Rows.Add(newRow);
        }

        private void UpdateEquipment()
        {
            string equipmentId = textBoxId.Text;

            // Cập nhật XML
            XmlNode equipmentNode = xmlDoc.SelectSingleNode($"//equipment[@id='{equipmentId}']");
            if (equipmentNode != null)
            {
                UpdateXmlElement(equipmentNode, "name", textBoxName.Text);
                UpdateXmlElement(equipmentNode, "category", comboBoxCondition.Text); // category từ comboBoxCondition
                UpdateXmlElement(equipmentNode, "brand", textBoxBranch.Text);
                UpdateXmlElement(equipmentNode, "model", comboBox.Text); // model từ comboBox
                UpdateXmlElement(equipmentNode, "quantity_total", ((int)numericUpDownQuantityTotal.Value).ToString());
                UpdateXmlElement(equipmentNode, "quantity_available", ((int)numericUpDownQuantityAvailable.Value).ToString());
                UpdateXmlElement(equipmentNode, "rental_price", numericUpDownRentalPriceHour.Value.ToString("0"));
                UpdateXmlElement(equipmentNode, "purchase_price", numericUpDownPurchasePrice.Value.ToString("0"));
                UpdateXmlElement(equipmentNode, "condition", "Tốt"); // Default condition
                UpdateXmlElement(equipmentNode, "description", textBoxDescription.Text);
                UpdateXmlElement(equipmentNode, "branch_id", textBoxBranchId.Text);
                UpdateXmlElement(equipmentNode, "supplier", ""); // Supplier không có trong form
                UpdateXmlElement(equipmentNode, "purchase_date", dateTimePickerPurchaseDate.Value.ToString("yyyy-MM-dd"));
                
                // warranty_period từ textBoxSerialNumber
                int warrantyMonths;
                if (int.TryParse(textBoxSerialNumber.Text, out warrantyMonths))
                {
                    UpdateXmlElement(equipmentNode, "warranty_period", warrantyMonths.ToString());
                }
                else
                {
                    // Fallback: tính từ dateTimePicker nếu textBoxSerialNumber không phải số
                    UpdateXmlElement(equipmentNode, "warranty_period", warrantyMonths.ToString());
                }
                
                UpdateXmlElement(equipmentNode, "status", "Active");
                
                // Update image_url if textbox exists
                if (this.Controls.Find("textBoxUrl2", true).Length > 0)
                {
                    TextBox txtImageUrl = (TextBox)this.Controls.Find("textBoxUrl2", true)[0];
                    UpdateXmlElement(equipmentNode, "image_url", txtImageUrl.Text);
                }

                xmlDoc.Save(xmlFilePath);
            }

            // Cập nhật DataTable
            DataRow rowToUpdate = equipmentTable.Rows.Cast<DataRow>()
                .FirstOrDefault(r => r["Mã DC"].ToString() == equipmentId);
            if (rowToUpdate != null)
            {
                PopulateDataRow(rowToUpdate);
            }
        }

        private void PopulateDataRow(DataRow row)
        {
            row["Mã DC"] = textBoxId.Text; // id
            row["Tên dụng cụ"] = textBoxName.Text; // name
            row["Danh mục"] = comboBoxCondition.Text; // category từ comboBoxCondition
            row["Thương hiệu"] = textBoxBranch.Text; // brand
            row["Model"] = comboBox.Text; // model từ comboBox
            row["Tổng SL"] = (int)numericUpDownQuantityTotal.Value; // quantity_total (from Tab 4)
            row["SL có sẵn"] = (int)numericUpDownQuantityAvailable.Value; // quantity_available (from Tab 4)
            row["Giá thuê"] = numericUpDownRentalPriceHour.Value; // rental_price
            row["Giá mua"] = numericUpDownPurchasePrice.Value; // purchase_price
            row["Tình trạng"] = "Tốt"; // condition (default, không có control riêng)
            row["Chi nhánh"] = textBoxBranchId.Text; // branch_id
            row["Nhà cung cấp"] = ""; // supplier (không có trong form hiện tại)
            row["Ngày mua"] = dateTimePickerPurchaseDate.Value; // purchase_date
            
            // warranty_period từ textBoxSerialNumber hoặc tính từ dateTimePicker
            int warrantyMonths;
            if (int.TryParse(textBoxSerialNumber.Text, out warrantyMonths))
            {
                row["BH (tháng)"] = warrantyMonths;
            }
            row["Trạng thái"] = "Active"; // status
            row["Mô tả"] = textBoxDescription.Text; // description
            
            // Save image_url if textbox exists
            if (this.Controls.Find("textBoxUrl2", true).Length > 0)
            {
                TextBox txtImageUrl = (TextBox)this.Controls.Find("textBoxUrl2", true)[0];
                row["URL hình ảnh"] = txtImageUrl.Text;
            }
            else
            {
                row["URL hình ảnh"] = "";
            }
        }

        private void AddXmlElement(XmlElement parent, string elementName, string value)
        {
            XmlElement element = xmlDoc.CreateElement(elementName);
            element.InnerText = value ?? "";
            parent.AppendChild(element);
        }

        private void UpdateXmlElement(XmlNode parent, string elementName, string value)
        {
            XmlNode element = parent.SelectSingleNode(elementName);
            if (element != null)
            {
                element.InnerText = value ?? "";
            }
            else
            {
                XmlElement newElement = xmlDoc.CreateElement(elementName);
                newElement.InnerText = value ?? "";
                parent.AppendChild(newElement);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            isAdding = false;
            if (dataGridViewEquipment.CurrentRow != null)
            {
                LoadSelectedEquipmentToForm();
            }
            else
            {
                ClearForm();
            }
        }


        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadEquipmentsFromXML();
            ClearForm();
            textBoxSearch.Clear();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                equipmentTable.DefaultView.RowFilter = string.Empty;
            }
            else
            {
                equipmentTable.DefaultView.RowFilter = $"[Tên dụng cụ] LIKE '%{searchText}%' OR [Mã DC] LIKE '%{searchText}%' OR [Danh mục] LIKE '%{searchText}%' OR [Thương hiệu] LIKE '%{searchText}%'";
            }
        }

        private void buttonExportExcel_Click(object sender, EventArgs e)
        {
            if (equipmentTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls";
            saveDialog.Title = "Xuất danh sách thiết bị";
            saveDialog.FileName = $"DanhSachThietBi_{DateTime.Now:yyyyMMdd_HHmmss}";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application excelApp = null;
                Microsoft.Office.Interop.Excel.Workbook workbook = null;
                Microsoft.Office.Interop.Excel.Worksheet worksheet = null;

                try
                {
                    // Tạo ứng dụng Excel
                    excelApp = new Microsoft.Office.Interop.Excel.Application();
                    excelApp.Visible = false;
                    workbook = excelApp.Workbooks.Add();
                    worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
                    worksheet.Name = "Thiết Bị";

                    // Tiêu đề báo cáo
                    worksheet.Cells[1, 1] = "DANH SÁCH THIẾT BỊ";
                    Microsoft.Office.Interop.Excel.Range titleRange = worksheet.Range["A1", "P1"];
                    titleRange.Merge();
                    titleRange.Font.Bold = true;
                    titleRange.Font.Size = 16;
                    titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    titleRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    titleRange.RowHeight = 30;

                    // Ngày xuất
                    worksheet.Cells[2, 1] = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
                    Microsoft.Office.Interop.Excel.Range dateRange = worksheet.Range["A2", "P2"];
                    dateRange.Merge();
                    dateRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    dateRange.Font.Italic = true;

                    // Header (dòng 4)
                    int headerRow = 4;
                    for (int i = 0; i < equipmentTable.Columns.Count; i++)
                    {
                        worksheet.Cells[headerRow, i + 1] = equipmentTable.Columns[i].ColumnName;
                    }

                    // Format header
                    Microsoft.Office.Interop.Excel.Range headerRange = worksheet.Range[worksheet.Cells[headerRow, 1], worksheet.Cells[headerRow, equipmentTable.Columns.Count]];
                    headerRange.Font.Bold = true;
                    headerRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(156, 39, 176));
                    headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    headerRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    headerRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                    // Dữ liệu (từ dòng 5)
                    int startRow = 5;
                    for (int i = 0; i < equipmentTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < equipmentTable.Columns.Count; j++)
                        {
                            worksheet.Cells[startRow + i, j + 1] = equipmentTable.Rows[i][j]?.ToString() ?? "";
                        }
                    }

                    // Format dữ liệu
                    Microsoft.Office.Interop.Excel.Range dataRange = worksheet.Range[worksheet.Cells[startRow, 1], worksheet.Cells[startRow + equipmentTable.Rows.Count - 1, equipmentTable.Columns.Count]];
                    dataRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    dataRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    dataRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    // Tổng số bản ghi
                    int summaryRow = startRow + equipmentTable.Rows.Count + 1;
                    worksheet.Cells[summaryRow, 1] = $"Tổng số thiết bị: {equipmentTable.Rows.Count}";
                    Microsoft.Office.Interop.Excel.Range summaryRange = worksheet.Range[worksheet.Cells[summaryRow, 1], worksheet.Cells[summaryRow, 3]];
                    summaryRange.Merge();
                    summaryRange.Font.Bold = true;
                    summaryRange.Font.Italic = true;

                    // Auto-fit columns
                    worksheet.Columns.AutoFit();

                    // Lưu file
                    workbook.SaveAs(saveDialog.FileName);
                    workbook.Close();
                    excelApp.Quit();

                    MessageBox.Show($"Xuất Excel thành công!\nĐã lưu tại: {saveDialog.FileName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở file Excel vừa tạo
                    if (MessageBox.Show("Bạn có muốn mở file Excel vừa xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveDialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Giải phóng tài nguyên COM
                    if (worksheet != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                    if (workbook != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    if (excelApp != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }

        private void buttonImportXml_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    DialogResult result = MessageBox.Show(
                        "Bạn có chắc chắn muốn import dữ liệu từ file XML?\nDữ liệu hiện tại sẽ được thay thế!",
                        "Xác nhận Import",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        ImportFromXML(openFileDialog.FileName);
                        MessageBox.Show("Import dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi import file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImportFromXML(string filePath)
        {
            try
            {
                // Đọc file XML mới
                XmlDocument importDoc = new XmlDocument();
                importDoc.Load(filePath);

                // Kiểm tra cấu trúc XML
                XmlNodeList equipmentNodes = importDoc.SelectNodes("//equipment");
                if (equipmentNodes == null || equipmentNodes.Count == 0)
                {
                    throw new Exception("File XML không có dữ liệu dụng cụ hoặc cấu trúc không đúng!");
                }

                // Sao chép file hiện tại thành backup
                string backupPath = xmlFilePath.Replace(".xml", $"_backup_{DateTime.Now:yyyyMMdd_HHmmss}.xml");
                if (File.Exists(xmlFilePath))
                {
                    File.Copy(xmlFilePath, backupPath, true);
                }

                // Thay thế file XML hiện tại
                File.Copy(filePath, xmlFilePath, true);

                // Tải lại dữ liệu
                LoadEquipmentsFromXML();
                ClearForm();

                MessageBox.Show($"Import thành công {equipmentNodes.Count} dụng cụ!\nFile gốc đã được backup tại:\n{backupPath}",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi import XML: {ex.Message}");
            }
        }

        // Import dữ liệu từ SQL Server
        private void ImportSQLButtonDungCu_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dữ liệu từ SQL Server sẽ được thêm vào DataTable hiện tại.\nTiếp tục?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString);
            try
            {
                conn.Open();
                string sql = "SELECT * FROM Equipments";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dtSQL = new DataTable();
                da.Fill(dtSQL);

                if (dtSQL.Rows.Count == 0)
                {
                    MessageBox.Show("Bảng Equipments trong SQL Server đang trống!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Clear DataTable hiện tại
                equipmentTable.Clear();

                // Import từng dòng từ SQL vào DataTable
                foreach (DataRow sqlRow in dtSQL.Rows)
                {
                    DataRow newRow = equipmentTable.NewRow();
                    
                    // Mapping: SQL (English) -> DataTable (Tiếng Việt)
                    newRow["Mã DC"] = sqlRow["id"] ?? "";
                    newRow["Tên dụng cụ"] = sqlRow["name"] ?? "";
                    newRow["Danh mục"] = sqlRow["category"] ?? "";
                    newRow["Thương hiệu"] = sqlRow["brand"] ?? "";
                    newRow["Model"] = sqlRow["model"] ?? "";
                    newRow["Tổng SL"] = sqlRow["quantity_total"] != DBNull.Value ? Convert.ToInt32(sqlRow["quantity_total"]) : 0;
                    newRow["SL có sẵn"] = sqlRow["quantity_available"] != DBNull.Value ? Convert.ToInt32(sqlRow["quantity_available"]) : 0;
                    newRow["Giá thuê"] = sqlRow["rental_price"] != DBNull.Value ? Convert.ToDecimal(sqlRow["rental_price"]) : 0;
                    newRow["Giá mua"] = sqlRow["purchase_price"] != DBNull.Value ? Convert.ToDecimal(sqlRow["purchase_price"]) : 0;
                    newRow["Tình trạng"] = sqlRow["condition"] ?? "";
                    newRow["Chi nhánh"] = sqlRow["branch_id"] ?? "";
                    newRow["Nhà cung cấp"] = sqlRow["supplier"] ?? "";
                    newRow["Ngày mua"] = sqlRow["purchase_date"] != DBNull.Value ? Convert.ToDateTime(sqlRow["purchase_date"]) : DateTime.Now;
                    newRow["BH (tháng)"] = sqlRow["warranty_period"] != DBNull.Value ? Convert.ToInt32(sqlRow["warranty_period"]) : 0;
                    newRow["Trạng thái"] = sqlRow["status"] ?? "";
                    newRow["Mô tả"] = sqlRow["description"] ?? "";
                    newRow["URL hình ảnh"] = sqlRow["image_url"] ?? "";

                    equipmentTable.Rows.Add(newRow);
                }

                // Refresh DataGridView
                dataGridViewEquipment.DataSource = equipmentTable;
                if (dataGridViewEquipment.Rows.Count > 0)
                {
                    dataGridViewEquipment.Rows[0].Selected = true;
                    LoadSelectedEquipmentToForm();
                }

                MessageBox.Show($"Đã import thành công {dtSQL.Rows.Count} dụng cụ từ SQL Server!",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối SQL Server: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
