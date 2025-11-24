using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;

namespace CNXML_HVA
{
    public partial class KhachHang : Form
    {
        private string xmlFilePath;
        private XmlDocument xmlDoc;
        private bool isEditing = false;
        private bool isAdding = false;
        private DataTable customerTable;

        public KhachHang()
        {
            InitializeComponent();
            xmlFilePath = DataPaths.GetXmlFilePath("Customers.xml");
            DataPaths.EnsureXmlFileExists("Customers.xml");
            xmlDoc = new XmlDocument();
            InitializeDataTable();
        }

        private void InitializeDataTable()
        {
            customerTable = new DataTable();
            customerTable.Columns.Add("Mã KH", typeof(string));
            customerTable.Columns.Add("Tên khách hàng", typeof(string));
            customerTable.Columns.Add("Số điện thoại", typeof(string));
            customerTable.Columns.Add("Email", typeof(string));
            customerTable.Columns.Add("Thành phố", typeof(string));
            customerTable.Columns.Add("Quận/Huyện", typeof(string));
            customerTable.Columns.Add("Đường", typeof(string));
            customerTable.Columns.Add("Hạng thành viên", typeof(string));
            customerTable.Columns.Add("Ghi chú", typeof(string));

            dataGridViewCustomers.DataSource = customerTable;
        }

        private void KhachHang_Load(object sender, EventArgs e)
        {
            LoadCustomersFromXML();
            SetupDataGridView();
            SetEditMode(false);
            UpdateRecordCount();
        }

        private void LoadCustomersFromXML()
        {
            try
            {
                customerTable.Clear();

                if (!File.Exists(xmlFilePath))
                {
                    return;
                }

                xmlDoc.Load(xmlFilePath);

                XmlNodeList nodes = xmlDoc.SelectNodes("//customer");
                foreach (XmlNode node in nodes)
                {
                    DataRow r = customerTable.NewRow();
                    r["Mã KH"] = node.Attributes["id"]?.Value ?? "";
                    r["Tên khách hàng"] = node.SelectSingleNode("name")?.InnerText ?? "";
                    r["Số điện thoại"] = node.SelectSingleNode("phone")?.InnerText ?? "";
                    r["Email"] = node.SelectSingleNode("email")?.InnerText ?? "";
                    r["Thành phố"] = node.SelectSingleNode("address/city")?.InnerText ?? "";
                    r["Quận/Huyện"] = node.SelectSingleNode("address/district")?.InnerText ?? "";
                    r["Đường"] = node.SelectSingleNode("address/street")?.InnerText ?? "";
                    r["Hạng thành viên"] = node.SelectSingleNode("membership")?.InnerText ?? "";
                    r["Ghi chú"] = node.SelectSingleNode("notes")?.InnerText ?? "";
                    customerTable.Rows.Add(r);
                }

                if (dataGridViewCustomers.Rows.Count > 0)
                {
                    dataGridViewCustomers.Rows[0].Selected = true;
                    LoadSelectedCustomerToForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            dataGridViewCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCustomers.MultiSelect = false;
            dataGridViewCustomers.ReadOnly = true;

            dataGridViewCustomers.BackgroundColor = Color.White;
            dataGridViewCustomers.GridColor = Color.FromArgb(224, 224, 224);
            dataGridViewCustomers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(179, 229, 252);
            dataGridViewCustomers.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 33, 33);
            dataGridViewCustomers.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dataGridViewCustomers.DefaultCellStyle.Padding = new Padding(5);

            dataGridViewCustomers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            dataGridViewCustomers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewCustomers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCustomers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCustomers.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);

            dataGridViewCustomers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dataGridViewCustomers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        private void SetEditMode(bool editMode)
        {
            isEditing = editMode;

            groupBoxCustomerInfo.Enabled = editMode;
            buttonSave.Enabled = editMode;
            buttonCancel.Enabled = editMode;

            buttonAdd.Enabled = !editMode;
            buttonEdit.Enabled = !editMode && dataGridViewCustomers.CurrentRow != null;
            buttonDelete.Enabled = !editMode && dataGridViewCustomers.CurrentRow != null;
            buttonRefresh.Enabled = !editMode;
            buttonExportExcel.Enabled = !editMode;

            dataGridViewCustomers.Enabled = !editMode;
            textBoxSearch.Enabled = !editMode;
        }

        private void ClearForm()
        {
            textBoxId.Clear();
            textBoxName.Clear();
            textBoxPhone.Clear();
            textBoxEmail.Clear();
            textBoxCity.Clear();
            textBoxDistrict.Clear();
            textBoxStreet.Clear();
            comboBoxMembership.SelectedIndex = -1;
            textBoxNote.Clear();
        }

        private void UpdateRecordCount()
        {
            int count = customerTable.DefaultView.Count;
            labelRecordCount.Text = $"Tổng số: {count} khách hàng";
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
            int max = 0;
            foreach (DataRow r in customerTable.Rows)
            {
                string id = r["Mã KH"]?.ToString() ?? "";
                if (id.StartsWith("C"))
                {
                    if (int.TryParse(id.Substring(1), out int num))
                    {
                        if (num > max) max = num;
                    }
                }
            }
            return "C" + (max + 1).ToString("000");
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewCustomers.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isAdding = false;
            SetEditMode(true);
            textBoxName.Focus();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewCustomers.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string id = dataGridViewCustomers.CurrentRow.Cells["Mã KH"].Value?.ToString() ?? "";
            string name = dataGridViewCustomers.CurrentRow.Cells["Tên khách hàng"].Value?.ToString() ?? "";

            if (MessageBox.Show($"Bạn có chắc muốn xóa khách hàng '{name}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    XmlNode node = xmlDoc.SelectSingleNode($"//customer[@id='{id}']");
                    if (node != null)
                    {
                        node.ParentNode.RemoveChild(node);
                        xmlDoc.Save(xmlFilePath);
                    }

                    DataRow dr = customerTable.Rows.Cast<DataRow>().FirstOrDefault(r => r["Mã KH"].ToString() == id);
                    if (dr != null) customerTable.Rows.Remove(dr);

                    ClearForm();
                    UpdateRecordCount();
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                if (isAdding)
                    AddCustomer();
                else
                    UpdateCustomer();

                xmlDoc.Save(xmlFilePath);
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetEditMode(false);
                isAdding = false;
                UpdateRecordCount();
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
                MessageBox.Show("Mã khách hàng không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxId.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Họ và tên không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxName.Focus();
                return false;
            }

            if (isAdding)
            {
                foreach (DataRow r in customerTable.Rows)
                {
                    if (r["Mã KH"].ToString() == textBoxId.Text)
                    {
                        MessageBox.Show("Mã khách hàng đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxId.Focus();
                        return false;
                    }
                }
            }

            return true;
        }

        private void AddCustomer()
        {
            XmlNode root = xmlDoc.SelectSingleNode("customers");
            if (root == null)
            {
                root = xmlDoc.CreateElement("customers");
                xmlDoc.AppendChild(root);
            }

            XmlElement customer = xmlDoc.CreateElement("customer");
            customer.SetAttribute("id", textBoxId.Text);

            AddXmlElement(customer, "name", textBoxName.Text);
            AddXmlElement(customer, "phone", textBoxPhone.Text);
            AddXmlElement(customer, "email", textBoxEmail.Text);

            XmlElement address = xmlDoc.CreateElement("address");
            AddXmlElement(address, "city", textBoxCity.Text);
            AddXmlElement(address, "district", textBoxDistrict.Text);
            AddXmlElement(address, "street", textBoxStreet.Text);
            customer.AppendChild(address);

            AddXmlElement(customer, "membership", comboBoxMembership.Text);
            AddXmlElement(customer, "notes", textBoxNote.Text);

            xmlDoc.DocumentElement.AppendChild(customer);

            DataRow row = customerTable.NewRow();
            PopulateDataRow(row);
            customerTable.Rows.Add(row);
        }

        private void UpdateCustomer()
        {
            string id = textBoxId.Text;
            XmlNode node = xmlDoc.SelectSingleNode($"//customer[@id='{id}']");
            if (node == null) return;

            UpdateXml(node, "name", textBoxName.Text);
            UpdateXml(node, "phone", textBoxPhone.Text);
            UpdateXml(node, "email", textBoxEmail.Text);
            UpdateXml(node.SelectSingleNode("address"), "city", textBoxCity.Text);
            UpdateXml(node.SelectSingleNode("address"), "district", textBoxDistrict.Text);
            UpdateXml(node.SelectSingleNode("address"), "street", textBoxStreet.Text);
            UpdateXml(node, "membership", comboBoxMembership.Text);
            UpdateXml(node, "notes", textBoxNote.Text);

            DataRow row = customerTable.Rows.Cast<DataRow>().FirstOrDefault(r => r["Mã KH"].ToString() == id);
            if (row != null) PopulateDataRow(row);
        }

        private void AddXmlElement(XmlElement parent, string name, string value)
        {
            XmlElement el = xmlDoc.CreateElement(name);
            el.InnerText = value ?? "";
            parent.AppendChild(el);
        }

        private void UpdateXml(XmlNode parent, string name, string value)
        {
            if (parent == null) return;
            XmlNode n = parent.SelectSingleNode(name);
            if (n != null) n.InnerText = value ?? "";
            else
            {
                if (parent is XmlElement pe)
                {
                    XmlElement el = xmlDoc.CreateElement(name);
                    el.InnerText = value ?? "";
                    parent.AppendChild(el);
                }
            }
        }

        private void PopulateDataRow(DataRow r)
        {
            r["Mã KH"] = textBoxId.Text;
            r["Tên khách hàng"] = textBoxName.Text;
            r["Số điện thoại"] = textBoxPhone.Text;
            r["Email"] = textBoxEmail.Text;
            r["Thành phố"] = textBoxCity.Text;
            r["Quận/Huyện"] = textBoxDistrict.Text;
            r["Đường"] = textBoxStreet.Text;
            r["Hạng thành viên"] = comboBoxMembership.Text;
            r["Ghi chú"] = textBoxNote.Text;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            isAdding = false;
            if (dataGridViewCustomers.CurrentRow != null)
                LoadSelectedCustomerToForm();
            else
                ClearForm();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomersFromXML();
            ClearForm();
            textBoxSearch.Clear();
            UpdateRecordCount();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string search = textBoxSearch.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(search))
                customerTable.DefaultView.RowFilter = "";
            else
                customerTable.DefaultView.RowFilter = $"[Tên khách hàng] LIKE '%{search}%' OR [Số điện thoại] LIKE '%{search}%'";

            UpdateRecordCount();
        }

        private void dataGridViewCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewCustomers.CurrentRow != null && !isAdding && !isEditing)
            {
                LoadSelectedCustomerToForm();
            }
        }

        private void LoadSelectedCustomerToForm()
        {
            if (dataGridViewCustomers.CurrentRow == null) return;

            var row = dataGridViewCustomers.CurrentRow;
            textBoxId.Text = row.Cells["Mã KH"].Value?.ToString() ?? "";
            textBoxName.Text = row.Cells["Tên khách hàng"].Value?.ToString() ?? "";
            textBoxPhone.Text = row.Cells["Số điện thoại"].Value?.ToString() ?? "";
            textBoxEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
            textBoxCity.Text = row.Cells["Thành phố"].Value?.ToString() ?? "";
            textBoxDistrict.Text = row.Cells["Quận/Huyện"].Value?.ToString() ?? "";
            textBoxStreet.Text = row.Cells["Đường"].Value?.ToString() ?? "";
            comboBoxMembership.Text = row.Cells["Hạng thành viên"].Value?.ToString() ?? "";
            textBoxNote.Text = row.Cells["Ghi chú"].Value?.ToString() ?? "";
        }

        private void buttonExportExcel_Click(object sender, EventArgs e)
        {
            if (customerTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveDialog.FilterIndex = 1;
            saveDialog.RestoreDirectory = true;
            saveDialog.FileName = "DanhSachKhachHang_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportToExcel(saveDialog.FileName);
                    MessageBox.Show("Xuất Excel thành công!\nĐường dẫn: " + saveDialog.FileName,
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportToExcel(string filePath)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];

            try
            {
                // Title
                worksheet.Cells[1, 1] = "DANH SÁCH KHÁCH HÀNG";
                Excel.Range titleRange = worksheet.Range["A1", "I1"];
                titleRange.Merge();
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 16;
                titleRange.Font.Color = Color.White;
                titleRange.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(33, 150, 243));
                titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                titleRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                titleRange.RowHeight = 30;

                // Headers
                for (int i = 0; i < customerTable.Columns.Count; i++)
                {
                    worksheet.Cells[3, i + 1] = customerTable.Columns[i].ColumnName;
                    Excel.Range headerCell = (Excel.Range)worksheet.Cells[3, i + 1];
                    headerCell.Font.Bold = true;
                    headerCell.Font.Color = Color.White;
                    headerCell.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(76, 175, 80));
                    headerCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                }

                // Data
                for (int i = 0; i < customerTable.Rows.Count; i++)
                {
                    for (int j = 0; j < customerTable.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 4, j + 1] = customerTable.Rows[i][j].ToString();
                    }
                }

                // Auto-fit columns
                worksheet.Columns.AutoFit();

                // Add borders
                Excel.Range dataRange = worksheet.Range["A3", $"I{customerTable.Rows.Count + 3}"];
                dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                dataRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

                // Alternate row colors
                for (int i = 4; i <= customerTable.Rows.Count + 3; i++)
                {
                    if (i % 2 == 0)
                    {
                        Excel.Range rowRange = worksheet.Range[$"A{i}", $"I{i}"];
                        rowRange.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(248, 248, 248));
                    }
                }

                // Add footer
                int footerRow = customerTable.Rows.Count + 5;
                worksheet.Cells[footerRow, 1] = $"Tổng số: {customerTable.Rows.Count} khách hàng";
                worksheet.Cells[footerRow + 1, 1] = $"Ngày xuất: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";

                Excel.Range footerRange = worksheet.Range[$"A{footerRow}", $"A{footerRow + 1}"];
                footerRange.Font.Italic = true;
                footerRange.Font.Color = ColorTranslator.ToOle(Color.Gray);

                workbook.SaveAs(filePath);
                workbook.Close();
                excelApp.Quit();
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
        }
    }
}