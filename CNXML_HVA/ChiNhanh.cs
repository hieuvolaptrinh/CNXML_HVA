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
    public partial class ChiNhanh : Form
    {
        private string xmlFilePath;
        private XmlDocument xmlDoc;
        private bool isEditing = false;
        private bool isAdding = false;
        private DataTable branchTable;

        public ChiNhanh()
        {
            InitializeComponent();
            xmlFilePath = DataPaths.GetXmlFilePath("Branches.xml");
            DataPaths.EnsureXmlFileExists("Branches.xml");
            xmlDoc = new XmlDocument();
            InitializeDataTable();
        }

        private void InitializeDataTable()
        {
            branchTable = new DataTable();
            branchTable.Columns.Add("Mã CN", typeof(string));
            branchTable.Columns.Add("Tên chi nhánh", typeof(string));
            branchTable.Columns.Add("Mã code", typeof(string));
            branchTable.Columns.Add("Thành phố", typeof(string));
            branchTable.Columns.Add("Quận", typeof(string));
            branchTable.Columns.Add("Đường", typeof(string));
            branchTable.Columns.Add("Số nhà", typeof(string));
            branchTable.Columns.Add("Mã bưu chính", typeof(string));
            branchTable.Columns.Add("Điện thoại", typeof(string));
            branchTable.Columns.Add("Email", typeof(string));
            branchTable.Columns.Add("Fax", typeof(string));
            branchTable.Columns.Add("Mã quản lý", typeof(string));
            branchTable.Columns.Add("Tên quản lý", typeof(string));
            branchTable.Columns.Add("Giờ T2-T6", typeof(string));
            branchTable.Columns.Add("Giờ T7-CN", typeof(string));
            branchTable.Columns.Add("Số sân", typeof(int));
            branchTable.Columns.Add("Ngày thành lập", typeof(DateTime));
            branchTable.Columns.Add("Doanh thu tháng", typeof(decimal));
            branchTable.Columns.Add("Số nhân viên", typeof(int));
            branchTable.Columns.Add("Mô tả", typeof(string));
            branchTable.Columns.Add("Trạng thái", typeof(string));
            branchTable.Columns.Add("URL hình ảnh", typeof(string));

            dataGridViewBranches.DataSource = branchTable;
            HideUnwantedColumns();
        }

        private void ChiNhanh_Load(object sender, EventArgs e)
        {
            LoadBranchesFromXML();
            SetupDataGridView();
            SetEditMode(false);
            AddImageUrlControl();
        }
        
        private void AddImageUrlControl()
        {
            // Add image URL textbox to Description tab if it doesn't exist
            if (this.Controls.Find("textBoxUrl1", true).Length == 0)
            {
                // Find the Description tab
                TabPage descTab = tabPageDescription;
                if (descTab != null)
                {
                    // Create label for image URL
                    Label lblImageUrl = new Label();
                    lblImageUrl.Name = "labelUrl1";
                    lblImageUrl.Text = "URL hình ảnh:";
                    lblImageUrl.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    lblImageUrl.ForeColor = Color.FromArgb(27, 94, 32);
                    lblImageUrl.AutoSize = true;
                    lblImageUrl.Location = new Point(20, 270);
                    
                    // Create textbox for image URL
                    TextBox txtImageUrl = new TextBox();
                    txtImageUrl.Name = "textBoxUrl1";
                    txtImageUrl.Font = new Font("Segoe UI", 9.5F);
                    txtImageUrl.Location = new Point(24, 295);
                    txtImageUrl.Size = new Size(481, 29);
                    txtImageUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    
                    descTab.Controls.Add(lblImageUrl);
                    descTab.Controls.Add(txtImageUrl);
                }
            }
        }

        private void LoadBranchesFromXML()
        {
            try
            {
                if (File.Exists(xmlFilePath))
                {
                    xmlDoc.Load(xmlFilePath);
                    branchTable.Clear();

                    XmlNodeList branchNodes = xmlDoc.SelectNodes("//branch");
                    foreach (XmlNode branchNode in branchNodes)
                    {
                        DataRow row = branchTable.NewRow();
                        row["Mã CN"] = GetNodeValue(branchNode, "@id");
                        row["Tên chi nhánh"] = GetNodeValue(branchNode, "name");
                        row["Mã code"] = GetNodeValue(branchNode, "code");
                        row["Thành phố"] = GetNodeValue(branchNode, "address/city");
                        row["Quận"] = GetNodeValue(branchNode, "address/district");
                        row["Đường"] = GetNodeValue(branchNode, "address/street");
                        row["Số nhà"] = GetNodeValue(branchNode, "address/house_number");
                        row["Mã bưu chính"] = GetNodeValue(branchNode, "address/postal_code");
                        row["Điện thoại"] = GetNodeValue(branchNode, "contact/phone");
                        row["Email"] = GetNodeValue(branchNode, "contact/email");
                        row["Fax"] = GetNodeValue(branchNode, "contact/fax");
                        row["Mã quản lý"] = GetNodeValue(branchNode, "manager_id");
                        row["Tên quản lý"] = GetNodeValue(branchNode, "manager_name");
                        row["Giờ T2-T6"] = GetNodeValue(branchNode, "opening_hours/weekday");
                        row["Giờ T7-CN"] = GetNodeValue(branchNode, "opening_hours/weekend");
                        row["Số sân"] = Convert.ToInt32(GetNodeValue(branchNode, "total_fields"));
                        row["Ngày thành lập"] = DateTime.Parse(GetNodeValue(branchNode, "established_date"));
                        row["Doanh thu tháng"] = Convert.ToDecimal(GetNodeValue(branchNode, "monthly_revenue"));
                        row["Số nhân viên"] = Convert.ToInt32(GetNodeValue(branchNode, "staff_count"));
                        row["Mô tả"] = GetNodeValue(branchNode, "description");
                        row["Trạng thái"] = GetNodeValue(branchNode, "status");
                        row["URL hình ảnh"] = GetNodeValue(branchNode, "image_url");

                        branchTable.Rows.Add(row);
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
            dataGridViewBranches.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewBranches.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewBranches.MultiSelect = false;
            dataGridViewBranches.ReadOnly = true;
            
            // Thiết lập màu sắc
            dataGridViewBranches.BackgroundColor = Color.White;
            dataGridViewBranches.GridColor = Color.FromArgb(76, 175, 80);
            dataGridViewBranches.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 201);
            dataGridViewBranches.DefaultCellStyle.SelectionForeColor = Color.FromArgb(27, 94, 32);
            dataGridViewBranches.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(76, 175, 80);
            dataGridViewBranches.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewBranches.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);

        }
        private void HideUnwantedColumns()
        {
            // Danh sách cột cần hiển thị
            string[] visibleColumns = { "Mã CN", "Tên chi nhánh", "Thành phố", "Mã quản lý", "Số nhân viên", "Trạng thái" };

            foreach (DataGridViewColumn col in dataGridViewBranches.Columns)
            {
                // Ẩn những cột không nằm trong danh sách
                col.Visible = visibleColumns.Contains(col.HeaderText);
            }
        }


        private void dataGridViewBranches_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewBranches.CurrentRow != null && !isAdding && !isEditing)
            {
                LoadSelectedBranchToForm();
            }
        }

        private void LoadSelectedBranchToForm()
        {
            if (dataGridViewBranches.CurrentRow != null)
            {
                DataGridViewRow row = dataGridViewBranches.CurrentRow;
                textBoxId.Text = row.Cells["Mã CN"].Value?.ToString();
                textBoxName.Text = row.Cells["Tên chi nhánh"].Value?.ToString();
                textBoxCode.Text = row.Cells["Mã code"].Value?.ToString();
                textBoxCity.Text = row.Cells["Thành phố"].Value?.ToString();
                textBoxDistrict.Text = row.Cells["Quận"].Value?.ToString();
                textBoxStreet.Text = row.Cells["Đường"].Value?.ToString();
                textBoxHouseNumber.Text = row.Cells["Số nhà"].Value?.ToString();
                textBoxPostalCode.Text = row.Cells["Mã bưu chính"].Value?.ToString();
                textBoxPhone.Text = row.Cells["Điện thoại"].Value?.ToString();
                textBoxEmail.Text = row.Cells["Email"].Value?.ToString();
                textBoxFax.Text = row.Cells["Fax"].Value?.ToString();
                textBoxManagerId.Text = row.Cells["Mã quản lý"].Value?.ToString();
                textBoxManagerName.Text = row.Cells["Tên quản lý"].Value?.ToString();
                textBoxWeekdayHours.Text = row.Cells["Giờ T2-T6"].Value?.ToString();
                textBoxWeekendHours.Text = row.Cells["Giờ T7-CN"].Value?.ToString();
                numericUpDownTotalFields.Value = Convert.ToDecimal(row.Cells["Số sân"].Value ?? 0);
                if (row.Cells["Ngày thành lập"].Value != null)
                    dateTimePickerEstablishedDate.Value = Convert.ToDateTime(row.Cells["Ngày thành lập"].Value);
                numericUpDownMonthlyRevenue.Value = Convert.ToDecimal(row.Cells["Doanh thu tháng"].Value ?? 0);
                numericUpDownStaffCount.Value = Convert.ToDecimal(row.Cells["Số nhân viên"].Value ?? 0);
                textBoxDescription.Text = row.Cells["Mô tả"].Value?.ToString();
                comboBoxStatus.Text = row.Cells["Trạng thái"].Value?.ToString();
                
                // Load image_url if textbox exists (will be added to Designer)
                if (this.Controls.Find("textBoxUrl1", true).Length > 0)
                {
                    TextBox txtImageUrl = (TextBox)this.Controls.Find("textBoxUrl1", true)[0];
                    txtImageUrl.Text = row.Cells["URL hình ảnh"].Value?.ToString();
                }
            }
        }

        private void ClearForm()
        {
            textBoxId.Clear();
            textBoxName.Clear();
            textBoxCode.Clear();
            textBoxCity.Clear();
            textBoxDistrict.Clear();
            textBoxStreet.Clear();
            textBoxHouseNumber.Clear();
            textBoxPostalCode.Clear();
            textBoxPhone.Clear();
            textBoxEmail.Clear();
            textBoxFax.Clear();
            textBoxManagerId.Clear();
            textBoxManagerName.Clear();
            textBoxWeekdayHours.Clear();
            textBoxWeekendHours.Clear();
            numericUpDownTotalFields.Value = 0;
            dateTimePickerEstablishedDate.Value = DateTime.Now;
            numericUpDownMonthlyRevenue.Value = 0;
            numericUpDownStaffCount.Value = 0;
            textBoxDescription.Clear();
            comboBoxStatus.SelectedIndex = -1;
            
            // Clear image URL if textbox exists
            if (this.Controls.Find("textBoxUrl1", true).Length > 0)
            {
                TextBox txtImageUrl = (TextBox)this.Controls.Find("textBoxUrl1", true)[0];
                txtImageUrl.Clear();
            }
        }

        private void SetEditMode(bool editMode)
        {
            isEditing = editMode;
            
            //groupBoxBranchInfo.Enabled = editMode;
            buttonSave.Enabled = editMode;
            buttonCancel.Enabled = editMode;
            
            buttonAdd.Enabled = !editMode;
            buttonEdit.Enabled = !editMode && dataGridViewBranches.CurrentRow != null;
            buttonDelete.Enabled = !editMode && dataGridViewBranches.CurrentRow != null;
            buttonRefresh.Enabled = !editMode;
            buttonExportExcel.Enabled = !editMode;
            buttonImportXml.Enabled = !editMode;
            
            dataGridViewBranches.Enabled = !editMode;
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
            foreach (DataRow row in branchTable.Rows)
            {
                string id = row["Mã CN"].ToString();
                if (id.StartsWith("B"))
                {
                    if (int.TryParse(id.Substring(1), out int numId))
                    {
                        if (numId > maxId) maxId = numId;
                    }
                }
            }
            return "B" + (maxId + 1).ToString("00");
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewBranches.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn chi nhánh cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            isAdding = false;
            SetEditMode(true);
            textBoxName.Focus();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewBranches.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn chi nhánh cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string branchName = dataGridViewBranches.CurrentRow.Cells["Tên chi nhánh"].Value?.ToString();
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa chi nhánh '{branchName}'?", 
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string branchId = dataGridViewBranches.CurrentRow.Cells["Mã CN"].Value?.ToString();
                    
                    // Xóa khỏi XML
                    XmlNode branchNode = xmlDoc.SelectSingleNode($"//branch[@id='{branchId}']");
                    if (branchNode != null)
                    {
                        branchNode.ParentNode.RemoveChild(branchNode);
                        xmlDoc.Save(xmlFilePath);
                    }

                    // Xóa khỏi DataTable
                    DataRow rowToDelete = branchTable.Rows.Cast<DataRow>()
                        .FirstOrDefault(r => r["Mã CN"].ToString() == branchId);
                    if (rowToDelete != null)
                    {
                        branchTable.Rows.Remove(rowToDelete);
                    }

                    ClearForm();
                    MessageBox.Show("Xóa chi nhánh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa chi nhánh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    AddNewBranch();
                }
                else
                {
                    UpdateBranch();
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
                MessageBox.Show("Vui lòng nhập mã chi nhánh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxId.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên chi nhánh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPhone.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxEmail.Focus();
                return false;
            }

            if (comboBoxStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn trạng thái!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxStatus.Focus();
                return false;
            }

            if (isAdding)
            {
                // Kiểm tra trùng mã khi thêm mới
                foreach (DataRow row in branchTable.Rows)
                {
                    if (row["Mã CN"].ToString() == textBoxId.Text)
                    {
                        MessageBox.Show("Mã chi nhánh đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxId.Focus();
                        return false;
                    }
                }
            }

            return true;
        }

        private void AddNewBranch()
        {
            // Thêm vào XML
            XmlNode branchesNode = xmlDoc.SelectSingleNode("branches");
            XmlElement newBranch = xmlDoc.CreateElement("branch");
            newBranch.SetAttribute("id", textBoxId.Text);

            AddXmlElement(newBranch, "name", textBoxName.Text);
            AddXmlElement(newBranch, "code", textBoxCode.Text);

            // Address
            XmlElement addressElement = xmlDoc.CreateElement("address");
            AddXmlElement(addressElement, "city", textBoxCity.Text);
            AddXmlElement(addressElement, "district", textBoxDistrict.Text);
            AddXmlElement(addressElement, "street", textBoxStreet.Text);
            AddXmlElement(addressElement, "house_number", textBoxHouseNumber.Text);
            AddXmlElement(addressElement, "postal_code", textBoxPostalCode.Text);
            newBranch.AppendChild(addressElement);

            // Contact
            XmlElement contactElement = xmlDoc.CreateElement("contact");
            AddXmlElement(contactElement, "phone", textBoxPhone.Text);
            AddXmlElement(contactElement, "email", textBoxEmail.Text);
            AddXmlElement(contactElement, "fax", textBoxFax.Text);
            newBranch.AppendChild(contactElement);

            AddXmlElement(newBranch, "manager_id", textBoxManagerId.Text);
            AddXmlElement(newBranch, "manager_name", textBoxManagerName.Text);

            // Opening hours
            XmlElement openingHoursElement = xmlDoc.CreateElement("opening_hours");
            AddXmlElement(openingHoursElement, "weekday", textBoxWeekdayHours.Text);
            AddXmlElement(openingHoursElement, "weekend", textBoxWeekendHours.Text);
            newBranch.AppendChild(openingHoursElement);

            AddXmlElement(newBranch, "total_fields", numericUpDownTotalFields.Value.ToString());
            AddXmlElement(newBranch, "established_date", dateTimePickerEstablishedDate.Value.ToString("yyyy-MM-dd"));
            AddXmlElement(newBranch, "monthly_revenue", numericUpDownMonthlyRevenue.Value.ToString());
            AddXmlElement(newBranch, "staff_count", numericUpDownStaffCount.Value.ToString());
            AddXmlElement(newBranch, "description", textBoxDescription.Text);
            AddXmlElement(newBranch, "status", comboBoxStatus.Text);
            
            // Add image_url if textbox exists
            if (this.Controls.Find("textBoxUrl1", true).Length > 0)
            {
                TextBox txtImageUrl = (TextBox)this.Controls.Find("textBoxUrl1", true)[0];
                AddXmlElement(newBranch, "image_url", txtImageUrl.Text);
            }
            else
            {
                AddXmlElement(newBranch, "image_url", "");
            }

            branchesNode.AppendChild(newBranch);
            xmlDoc.Save(xmlFilePath);

            // Thêm vào DataTable
            DataRow newRow = branchTable.NewRow();
            PopulateDataRow(newRow);
            branchTable.Rows.Add(newRow);
        }

        private void UpdateBranch()
        {
            string branchId = textBoxId.Text;
            
            // Cập nhật XML
            XmlNode branchNode = xmlDoc.SelectSingleNode($"//branch[@id='{branchId}']");
            if (branchNode != null)
            {
                UpdateXmlElement(branchNode, "name", textBoxName.Text);
                UpdateXmlElement(branchNode, "code", textBoxCode.Text);
                UpdateXmlElement(branchNode, "address/city", textBoxCity.Text);
                UpdateXmlElement(branchNode, "address/district", textBoxDistrict.Text);
                UpdateXmlElement(branchNode, "address/street", textBoxStreet.Text);
                UpdateXmlElement(branchNode, "address/house_number", textBoxHouseNumber.Text);
                UpdateXmlElement(branchNode, "address/postal_code", textBoxPostalCode.Text);
                UpdateXmlElement(branchNode, "contact/phone", textBoxPhone.Text);
                UpdateXmlElement(branchNode, "contact/email", textBoxEmail.Text);
                UpdateXmlElement(branchNode, "contact/fax", textBoxFax.Text);
                UpdateXmlElement(branchNode, "manager_id", textBoxManagerId.Text);
                UpdateXmlElement(branchNode, "manager_name", textBoxManagerName.Text);
                UpdateXmlElement(branchNode, "opening_hours/weekday", textBoxWeekdayHours.Text);
                UpdateXmlElement(branchNode, "opening_hours/weekend", textBoxWeekendHours.Text);
                UpdateXmlElement(branchNode, "total_fields", numericUpDownTotalFields.Value.ToString());
                UpdateXmlElement(branchNode, "established_date", dateTimePickerEstablishedDate.Value.ToString("yyyy-MM-dd"));
                UpdateXmlElement(branchNode, "monthly_revenue", numericUpDownMonthlyRevenue.Value.ToString());
                UpdateXmlElement(branchNode, "staff_count", numericUpDownStaffCount.Value.ToString());
                UpdateXmlElement(branchNode, "description", textBoxDescription.Text);
                UpdateXmlElement(branchNode, "status", comboBoxStatus.Text);
                
                // Update image_url if textbox exists
                if (this.Controls.Find("textBoxUrl1", true).Length > 0)
                {
                    TextBox txtImageUrl = (TextBox)this.Controls.Find("textBoxUrl1", true)[0];
                    UpdateXmlElement(branchNode, "image_url", txtImageUrl.Text);
                }

                xmlDoc.Save(xmlFilePath);
            }

            // Cập nhật DataTable
            DataRow rowToUpdate = branchTable.Rows.Cast<DataRow>()
                .FirstOrDefault(r => r["Mã CN"].ToString() == branchId);
            if (rowToUpdate != null)
            {
                PopulateDataRow(rowToUpdate);
            }
        }

        private void PopulateDataRow(DataRow row)
        {
            row["Mã CN"] = textBoxId.Text;
            row["Tên chi nhánh"] = textBoxName.Text;
            row["Mã code"] = textBoxCode.Text;
            row["Thành phố"] = textBoxCity.Text;
            row["Quận"] = textBoxDistrict.Text;
            row["Đường"] = textBoxStreet.Text;
            row["Số nhà"] = textBoxHouseNumber.Text;
            row["Mã bưu chính"] = textBoxPostalCode.Text;
            row["Điện thoại"] = textBoxPhone.Text;
            row["Email"] = textBoxEmail.Text;
            row["Fax"] = textBoxFax.Text;
            row["Mã quản lý"] = textBoxManagerId.Text;
            row["Tên quản lý"] = textBoxManagerName.Text;
            row["Giờ T2-T6"] = textBoxWeekdayHours.Text;
            row["Giờ T7-CN"] = textBoxWeekendHours.Text;
            row["Số sân"] = (int)numericUpDownTotalFields.Value;
            row["Ngày thành lập"] = dateTimePickerEstablishedDate.Value;
            row["Doanh thu tháng"] = numericUpDownMonthlyRevenue.Value;
            row["Số nhân viên"] = (int)numericUpDownStaffCount.Value;
            row["Mô tả"] = textBoxDescription.Text;
            row["Trạng thái"] = comboBoxStatus.Text;
            
            // Save image_url if textbox exists
            if (this.Controls.Find("textBoxUrl1", true).Length > 0)
            {
                TextBox txtImageUrl = (TextBox)this.Controls.Find("textBoxUrl1", true)[0];
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
            if (dataGridViewBranches.CurrentRow != null)
            {
                LoadSelectedBranchToForm();
            }
            else
            {
                ClearForm();
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadBranchesFromXML();
            ClearForm();
            textBoxSearch.Clear();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text.ToLower();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                branchTable.DefaultView.RowFilter = "";
            }
            else
            {
                branchTable.DefaultView.RowFilter = $"[Tên chi nhánh] LIKE '%{searchText}%' OR [Thành phố] LIKE '%{searchText}%' OR [Mã code] LIKE '%{searchText}%'";
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
                saveFileDialog.FileName = $"ChiNhanh_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportToCSV(saveFileDialog.FileName);
                    MessageBox.Show($"Xuất file thành công!\nĐã xuất {branchTable.Rows.Count} chi nhánh.", 
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
                        "Mã CN", "Tên chi nhánh", "Mã code", "Thành phố", "Quận", "Đường",
                        "Số nhà", "Mã bưu chính", "Điện thoại", "Email", "Fax",
                        "Mã quản lý", "Tên quản lý", "Giờ T2-T6", "Giờ T7-CN",
                        "Số sân", "Ngày thành lập", "Doanh thu tháng", "Số nhân viên",
                        "Mô tả", "Trạng thái"
                    };
                    
                    // Ghi header với dấu phân cách rõ ràng
                    sw.WriteLine(string.Join(";", headers));

                    // Dữ liệu - ghi từng dòng
                    foreach (DataRow row in branchTable.Rows)
                    {
                        var values = new List<string>();
                        
                        // Lấy giá trị từng cột theo thứ tự
                        values.Add(CleanValue(row["Mã CN"]));
                        values.Add(CleanValue(row["Tên chi nhánh"]));
                        values.Add(CleanValue(row["Mã code"]));
                        values.Add(CleanValue(row["Thành phố"]));
                        values.Add(CleanValue(row["Quận"]));
                        values.Add(CleanValue(row["Đường"]));
                        values.Add(CleanValue(row["Số nhà"]));
                        values.Add(CleanValue(row["Mã bưu chính"]));
                        values.Add(CleanValue(row["Điện thoại"]));
                        values.Add(CleanValue(row["Email"]));
                        values.Add(CleanValue(row["Fax"]));
                        values.Add(CleanValue(row["Mã quản lý"]));
                        values.Add(CleanValue(row["Tên quản lý"]));
                        values.Add(CleanValue(row["Giờ T2-T6"]));
                        values.Add(CleanValue(row["Giờ T7-CN"]));
                        values.Add(CleanValue(row["Số sân"]));
                        
                        // Format ngày tháng
                        if (row["Ngày thành lập"] != DBNull.Value)
                        {
                            DateTime date = Convert.ToDateTime(row["Ngày thành lập"]);
                            values.Add(date.ToString("dd/MM/yyyy"));
                        }
                        else
                        {
                            values.Add("");
                        }
                        
                        values.Add(CleanValue(row["Doanh thu tháng"]));
                        values.Add(CleanValue(row["Số nhân viên"]));
                        values.Add(CleanValue(row["Mô tả"]));
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
                XmlNodeList branchNodes = importDoc.SelectNodes("//branch");
                if (branchNodes == null || branchNodes.Count == 0)
                {
                    throw new Exception("File XML không có dữ liệu chi nhánh hoặc cấu trúc không đúng!");
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
                LoadBranchesFromXML();
                ClearForm();

                MessageBox.Show($"Import thành công {branchNodes.Count} chi nhánh!\nFile gốc đã được backup tại:\n{backupPath}",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi import XML: {ex.Message}");
            }
        }

        private void dataGridViewBranches_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPageBasicInfo_Click(object sender, EventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabControlBranchInfo_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            TabPage tabPage = tabControlBranchInfo.TabPages[e.Index];
            Rectangle tabBounds = tabControlBranchInfo.GetTabRect(e.Index);

            // Màu nền cho tab
            Brush backgroundBrush;
            Brush textBrush;
            Font tabFont;

            if (e.Index == tabControlBranchInfo.SelectedIndex)
            {
                // Tab đang active - màu xanh lá
                backgroundBrush = new SolidBrush(Color.FromArgb(76, 175, 80));
                textBrush = new SolidBrush(Color.White);
                tabFont = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            }
            else
            {
                // Tab không active - màu xám nhạt
                backgroundBrush = new SolidBrush(Color.FromArgb(240, 240, 240));
                textBrush = new SolidBrush(Color.FromArgb(27, 94, 32));
                tabFont = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            }

            // Vẽ nền tab
            g.FillRectangle(backgroundBrush, tabBounds);

            // Vẽ viền dưới cho tab active
            if (e.Index == tabControlBranchInfo.SelectedIndex)
            {
                Pen borderPen = new Pen(Color.FromArgb(56, 142, 60), 3);
                g.DrawLine(borderPen, tabBounds.Left, tabBounds.Bottom - 1, 
                    tabBounds.Right, tabBounds.Bottom - 1);
                borderPen.Dispose();
            }

            // Vẽ text tab ở giữa
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            
            g.DrawString(tabPage.Text, tabFont, textBrush, tabBounds, stringFormat);

            // Dispose resources
            backgroundBrush.Dispose();
            textBrush.Dispose();
            stringFormat.Dispose();
        }

    }
}
