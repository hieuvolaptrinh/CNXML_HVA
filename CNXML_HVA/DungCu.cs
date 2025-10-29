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
using System.IO;
using System.Text.RegularExpressions;

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
            xmlFilePath = Path.Combine(Application.StartupPath, "Equipments.xml");
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
            equipmentTable.Columns.Add("SL khả dụng", typeof(int));
            equipmentTable.Columns.Add("Giá thuê", typeof(decimal));
            equipmentTable.Columns.Add("Giá mua", typeof(decimal));
            equipmentTable.Columns.Add("Tình trạng", typeof(string));
            equipmentTable.Columns.Add("Mô tả", typeof(string));
            equipmentTable.Columns.Add("Chi nhánh", typeof(string));
            equipmentTable.Columns.Add("Nhà cung cấp", typeof(string));
            equipmentTable.Columns.Add("Ngày mua", typeof(DateTime));
            equipmentTable.Columns.Add("BH (tháng)", typeof(int));
            equipmentTable.Columns.Add("Trạng thái", typeof(string));

            dataGridViewEquipments.DataSource = equipmentTable;
        }

        private void DungCu_Load(object sender, EventArgs e)
        {
            LoadEquipmentsFromXML();
            SetupDataGridView();
            SetEditMode(false);
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
                        row["Tổng SL"] = Convert.ToInt32(GetNodeValue(equipmentNode, "quantity_total"));
                        row["SL khả dụng"] = Convert.ToInt32(GetNodeValue(equipmentNode, "quantity_available"));
                        row["Giá thuê"] = Convert.ToDecimal(GetNodeValue(equipmentNode, "rental_price"));
                        row["Giá mua"] = Convert.ToDecimal(GetNodeValue(equipmentNode, "purchase_price"));
                        row["Tình trạng"] = GetNodeValue(equipmentNode, "condition");
                        row["Mô tả"] = GetNodeValue(equipmentNode, "description");
                        row["Chi nhánh"] = GetNodeValue(equipmentNode, "branch_id");
                        row["Nhà cung cấp"] = GetNodeValue(equipmentNode, "supplier");
                        row["Ngày mua"] = DateTime.Parse(GetNodeValue(equipmentNode, "purchase_date"));
                        row["BH (tháng)"] = Convert.ToInt32(GetNodeValue(equipmentNode, "warranty_period"));
                        row["Trạng thái"] = GetNodeValue(equipmentNode, "status");

                        equipmentTable.Rows.Add(row);
                    }
                }
                else
                {
                    MessageBox.Show("File XML không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void SetupDataGridView()
        {
            dataGridViewEquipments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewEquipments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEquipments.MultiSelect = false;
            dataGridViewEquipments.ReadOnly = true;
            
            // Thiết lập màu sắc
            dataGridViewEquipments.BackgroundColor = Color.White;
            dataGridViewEquipments.GridColor = Color.FromArgb(76, 175, 80);
            dataGridViewEquipments.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 201);
            dataGridViewEquipments.DefaultCellStyle.SelectionForeColor = Color.FromArgb(27, 94, 32);
            dataGridViewEquipments.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(76, 175, 80);
            dataGridViewEquipments.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewEquipments.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
        }

        private void dataGridViewEquipments_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewEquipments.CurrentRow != null && !isAdding && !isEditing)
            {
                LoadSelectedEquipmentToForm();
            }
        }

        private void LoadSelectedEquipmentToForm()
        {
            if (dataGridViewEquipments.CurrentRow != null)
            {
                DataGridViewRow row = dataGridViewEquipments.CurrentRow;
                textBoxId.Text = row.Cells["Mã DC"].Value?.ToString();
                textBoxName.Text = row.Cells["Tên dụng cụ"].Value?.ToString();
                comboBoxCategory.Text = row.Cells["Danh mục"].Value?.ToString();
                textBoxBrand.Text = row.Cells["Thương hiệu"].Value?.ToString();
                textBoxModel.Text = row.Cells["Model"].Value?.ToString();
                numericUpDownQuantityTotal.Value = Convert.ToDecimal(row.Cells["Tổng SL"].Value ?? 0);
                numericUpDownQuantityAvailable.Value = Convert.ToDecimal(row.Cells["SL khả dụng"].Value ?? 0);
                numericUpDownRentalPrice.Value = Convert.ToDecimal(row.Cells["Giá thuê"].Value ?? 0);
                numericUpDownPurchasePrice.Value = Convert.ToDecimal(row.Cells["Giá mua"].Value ?? 0);
                comboBoxCondition.Text = row.Cells["Tình trạng"].Value?.ToString();
                textBoxDescription.Text = row.Cells["Mô tả"].Value?.ToString();
                textBoxBranchId.Text = row.Cells["Chi nhánh"].Value?.ToString();
                textBoxSupplier.Text = row.Cells["Nhà cung cấp"].Value?.ToString();
                if (row.Cells["Ngày mua"].Value != null)
                    dateTimePickerPurchaseDate.Value = Convert.ToDateTime(row.Cells["Ngày mua"].Value);
                numericUpDownWarrantyPeriod.Value = Convert.ToDecimal(row.Cells["BH (tháng)"].Value ?? 0);
                comboBoxStatus.Text = row.Cells["Trạng thái"].Value?.ToString();
            }
        }

        private void ClearForm()
        {
            textBoxId.Clear();
            textBoxName.Clear();
            comboBoxCategory.SelectedIndex = -1;
            textBoxBrand.Clear();
            textBoxModel.Clear();
            numericUpDownQuantityTotal.Value = 0;
            numericUpDownQuantityAvailable.Value = 0;
            numericUpDownRentalPrice.Value = 0;
            numericUpDownPurchasePrice.Value = 0;
            comboBoxCondition.SelectedIndex = -1;
            textBoxDescription.Clear();
            textBoxBranchId.Clear();
            textBoxSupplier.Clear();
            dateTimePickerPurchaseDate.Value = DateTime.Now;
            numericUpDownWarrantyPeriod.Value = 0;
            comboBoxStatus.SelectedIndex = -1;
        }

        private void SetEditMode(bool editMode)
        {
            isEditing = editMode;
            
            groupBoxEquipmentInfo.Enabled = editMode;
            buttonSave.Enabled = editMode;
            buttonCancel.Enabled = editMode;
            
            buttonAdd.Enabled = !editMode;
            buttonEdit.Enabled = !editMode && dataGridViewEquipments.CurrentRow != null;
            buttonDelete.Enabled = !editMode && dataGridViewEquipments.CurrentRow != null;
            buttonRefresh.Enabled = !editMode;
            buttonExportExcel.Enabled = !editMode;
            buttonImportXml.Enabled = !editMode;
            
            dataGridViewEquipments.Enabled = !editMode;
            textBoxSearch.Enabled = !editMode;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            isAdding = true;
            ClearForm();
            SetEditMode(true);
            textBoxId.Text = GenerateNewId();
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
            if (dataGridViewEquipments.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dụng cụ cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            isAdding = false;
            SetEditMode(true);
            textBoxName.Focus();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewEquipments.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dụng cụ cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string equipmentName = dataGridViewEquipments.CurrentRow.Cells["Tên dụng cụ"].Value?.ToString();
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa dụng cụ '{equipmentName}'?", 
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string equipmentId = dataGridViewEquipments.CurrentRow.Cells["Mã DC"].Value?.ToString();
                    
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
            if (string.IsNullOrWhiteSpace(textBoxId.Text))
            {
                MessageBox.Show("Vui lòng nhập mã dụng cụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxId.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên dụng cụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxName.Focus();
                return false;
            }

            if (comboBoxCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn danh mục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxCategory.Focus();
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
            XmlElement newEquipment = xmlDoc.CreateElement("equipment");
            newEquipment.SetAttribute("id", textBoxId.Text);

            AddXmlElement(newEquipment, "name", textBoxName.Text);
            AddXmlElement(newEquipment, "category", comboBoxCategory.Text);
            AddXmlElement(newEquipment, "brand", textBoxBrand.Text);
            AddXmlElement(newEquipment, "model", textBoxModel.Text);
            AddXmlElement(newEquipment, "quantity_total", numericUpDownQuantityTotal.Value.ToString());
            AddXmlElement(newEquipment, "quantity_available", numericUpDownQuantityAvailable.Value.ToString());
            AddXmlElement(newEquipment, "rental_price", numericUpDownRentalPrice.Value.ToString());
            AddXmlElement(newEquipment, "purchase_price", numericUpDownPurchasePrice.Value.ToString());
            AddXmlElement(newEquipment, "condition", comboBoxCondition.Text);
            AddXmlElement(newEquipment, "description", textBoxDescription.Text);
            AddXmlElement(newEquipment, "branch_id", textBoxBranchId.Text);
            AddXmlElement(newEquipment, "supplier", textBoxSupplier.Text);
            AddXmlElement(newEquipment, "purchase_date", dateTimePickerPurchaseDate.Value.ToString("yyyy-MM-dd"));
            AddXmlElement(newEquipment, "warranty_period", numericUpDownWarrantyPeriod.Value.ToString());
            AddXmlElement(newEquipment, "status", comboBoxStatus.Text);

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
                UpdateXmlElement(equipmentNode, "category", comboBoxCategory.Text);
                UpdateXmlElement(equipmentNode, "brand", textBoxBrand.Text);
                UpdateXmlElement(equipmentNode, "model", textBoxModel.Text);
                UpdateXmlElement(equipmentNode, "quantity_total", numericUpDownQuantityTotal.Value.ToString());
                UpdateXmlElement(equipmentNode, "quantity_available", numericUpDownQuantityAvailable.Value.ToString());
                UpdateXmlElement(equipmentNode, "rental_price", numericUpDownRentalPrice.Value.ToString());
                UpdateXmlElement(equipmentNode, "purchase_price", numericUpDownPurchasePrice.Value.ToString());
                UpdateXmlElement(equipmentNode, "condition", comboBoxCondition.Text);
                UpdateXmlElement(equipmentNode, "description", textBoxDescription.Text);
                UpdateXmlElement(equipmentNode, "branch_id", textBoxBranchId.Text);
                UpdateXmlElement(equipmentNode, "supplier", textBoxSupplier.Text);
                UpdateXmlElement(equipmentNode, "purchase_date", dateTimePickerPurchaseDate.Value.ToString("yyyy-MM-dd"));
                UpdateXmlElement(equipmentNode, "warranty_period", numericUpDownWarrantyPeriod.Value.ToString());
                UpdateXmlElement(equipmentNode, "status", comboBoxStatus.Text);

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
            row["Mã DC"] = textBoxId.Text;
            row["Tên dụng cụ"] = textBoxName.Text;
            row["Danh mục"] = comboBoxCategory.Text;
            row["Thương hiệu"] = textBoxBrand.Text;
            row["Model"] = textBoxModel.Text;
            row["Tổng SL"] = (int)numericUpDownQuantityTotal.Value;
            row["SL khả dụng"] = (int)numericUpDownQuantityAvailable.Value;
            row["Giá thuê"] = numericUpDownRentalPrice.Value;
            row["Giá mua"] = numericUpDownPurchasePrice.Value;
            row["Tình trạng"] = comboBoxCondition.Text;
            row["Mô tả"] = textBoxDescription.Text;
            row["Chi nhánh"] = textBoxBranchId.Text;
            row["Nhà cung cấp"] = textBoxSupplier.Text;
            row["Ngày mua"] = dateTimePickerPurchaseDate.Value;
            row["BH (tháng)"] = (int)numericUpDownWarrantyPeriod.Value;
            row["Trạng thái"] = comboBoxStatus.Text;
        }

        private void AddXmlElement(XmlElement parent, string elementName, string value)
        {
            XmlElement element = xmlDoc.CreateElement(elementName);
            element.InnerText = value;
            parent.AppendChild(element);
        }

        private void UpdateXmlElement(XmlNode parent, string elementName, string value)
        {
            XmlNode element = parent.SelectSingleNode(elementName);
            if (element != null)
            {
                element.InnerText = value;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            isAdding = false;
            if (dataGridViewEquipments.CurrentRow != null)
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
            string searchText = textBoxSearch.Text.ToLower();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                equipmentTable.DefaultView.RowFilter = "";
            }
            else
            {
                equipmentTable.DefaultView.RowFilter = $"[Tên dụng cụ] LIKE '%{searchText}%' OR [Danh mục] LIKE '%{searchText}%' OR [Thương hiệu] LIKE '%{searchText}%'";
            }
        }

        private void buttonExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV Files (*.csv)|*.csv|Excel Files (*.xls)|*.xls|All Files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = $"DungCu_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportToCSV(saveFileDialog.FileName);
                    MessageBox.Show($"Xuất file thành công!\nĐã xuất {equipmentTable.Rows.Count} dụng cụ.", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Hỏi người dùng có muốn mở file không
                    DialogResult result = MessageBox.Show("Bạn có muốn mở file vừa xuất?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToCSV(string filePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, false, new UTF8Encoding(true)))
                {
                    // Header - viết từng cột rõ ràng
                    var headers = new List<string>
                    {
                        "Mã DC", "Tên dụng cụ", "Danh mục", "Thương hiệu", "Model",
                        "Tổng SL", "SL khả dụng", "Giá thuê", "Giá mua", "Tình trạng",
                        "Mô tả", "Chi nhánh", "Nhà cung cấp", "Ngày mua", 
                        "BH (tháng)", "Trạng thái"
                    };
                    
                    // Ghi header với dấu phân cách rõ ràng
                    sw.WriteLine(string.Join(";", headers));

                    // Dữ liệu - ghi từng dòng
                    foreach (DataRow row in equipmentTable.Rows)
                    {
                        var values = new List<string>();
                        
                        // Lấy giá trị từng cột theo thứ tự
                        values.Add(CleanValue(row["Mã DC"]));
                        values.Add(CleanValue(row["Tên dụng cụ"]));
                        values.Add(CleanValue(row["Danh mục"]));
                        values.Add(CleanValue(row["Thương hiệu"]));
                        values.Add(CleanValue(row["Model"]));
                        values.Add(CleanValue(row["Tổng SL"]));
                        values.Add(CleanValue(row["SL khả dụng"]));
                        values.Add(CleanValue(row["Giá thuê"]));
                        values.Add(CleanValue(row["Giá mua"]));
                        values.Add(CleanValue(row["Tình trạng"]));
                        values.Add(CleanValue(row["Mô tả"]));
                        values.Add(CleanValue(row["Chi nhánh"]));
                        values.Add(CleanValue(row["Nhà cung cấp"]));
                        
                        // Format ngày tháng
                        if (row["Ngày mua"] != DBNull.Value)
                        {
                            DateTime date = Convert.ToDateTime(row["Ngày mua"]);
                            values.Add(date.ToString("dd/MM/yyyy"));
                        }
                        else
                        {
                            values.Add("");
                        }
                        
                        values.Add(CleanValue(row["BH (tháng)"]));
                        values.Add(CleanValue(row["Trạng thái"]));

                        // Ghi dòng dữ liệu với dấu phân cách ;
                        sw.WriteLine(string.Join(";", values));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi ghi file CSV: {ex.Message}");
            }
        }

        private string CleanValue(object value)
        {
            if (value == null || value == DBNull.Value)
                return "";
            
            string strValue = value.ToString().Trim();
            
            // Loại bỏ dấu xuống dòng và ký tự đặc biệt
            strValue = strValue.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " ");
            
            // Nếu có dấu phẩy, chấm phẩy hoặc dấu ngoặc kép thì bao quanh bằng dấu ngoặc kép
            if (strValue.Contains(";") || strValue.Contains(",") || strValue.Contains("\""))
            {
                strValue = "\"" + strValue.Replace("\"", "\"\"") + "\"";
            }
            
            return strValue;
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
    }
}
