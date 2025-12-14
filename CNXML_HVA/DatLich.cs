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
    public partial class DatLich : Form
    {
        private string xmlFilePath;
        private XmlDocument xmlDoc;
        private DataTable bookingsTable;
        private bool isAdding = false;
        private bool isEditing = false;

        public DatLich()
        {
            InitializeComponent();
            xmlFilePath = DataPaths.GetXmlFilePath("Bookings.xml");
            DataPaths.EnsureXmlFileExists("Bookings.xml");
            xmlDoc = new XmlDocument();
            InitializeDataTable();
        }

        private void InitializeDataTable()
        {
            bookingsTable = new DataTable();
            bookingsTable.Columns.Add("Mã đặt", typeof(string));
            bookingsTable.Columns.Add("Khách hàng", typeof(string));
            bookingsTable.Columns.Add("Sân", typeof(string));
            bookingsTable.Columns.Add("Loại sân", typeof(string));
            bookingsTable.Columns.Add("Ngày đặt", typeof(string));
            bookingsTable.Columns.Add("Giờ bắt đầu", typeof(string));
            bookingsTable.Columns.Add("Thời lượng", typeof(string));
            bookingsTable.Columns.Add("Ghi chú", typeof(string));

            dgvBookings.DataSource = bookingsTable;
        }

        private void DatLich_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadComboBoxes();
            LoadBookingsFromXML();
            SetEditMode(false);
            UpdateRecordCount();
        }

        private void SetupDataGridView()
        {
            dgvBookings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBookings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBookings.MultiSelect = false;
            dgvBookings.ReadOnly = true;

            dgvBookings.BackgroundColor = Color.White;
            dgvBookings.GridColor = Color.FromArgb(224, 224, 224);
            dgvBookings.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 204, 188);
            dgvBookings.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 33, 33);
            dgvBookings.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvBookings.DefaultCellStyle.Padding = new Padding(5);

            dgvBookings.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 87, 34);
            dgvBookings.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBookings.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvBookings.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBookings.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);

            dgvBookings.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgvBookings.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        private void LoadComboBoxes()
        {
            // Load customers from Customers.xml if exists
            string customersPath = DataPaths.GetXmlFilePath("Customers.xml");
            if (File.Exists(customersPath))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(customersPath);
                    XmlNodeList nodes = doc.SelectNodes("//customer");
                    foreach (XmlNode n in nodes)
                    {
                        string name = n.SelectSingleNode("name")?.InnerText ?? "";
                        if (!string.IsNullOrWhiteSpace(name) && !cboCustomer.Items.Contains(name))
                            cboCustomer.Items.Add(name);
                    }
                }
                catch { /* ignore */ }
            }
            else
            {
                if (cboCustomer.Items.Count == 0)
                {
                    cboCustomer.Items.AddRange(new object[] { "Nguyen Van A", "Tran Thi B", "Le Van C" });
                }
            }

            // Load fields from fields.xml if exists
            string fieldsPath = DataPaths.GetXmlFilePath("Fields.xml");
            if (File.Exists(fieldsPath))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(fieldsPath);
                    XmlNodeList nodes = doc.SelectNodes("//field");
                    foreach (XmlNode n in nodes)
                    {
                        string fname = n.SelectSingleNode("name")?.InnerText ?? "";
                        if (!string.IsNullOrWhiteSpace(fname) && !cboField.Items.Contains(fname))
                            cboField.Items.Add(fname);
                    }
                }
                catch { /* ignore */ }
            }
            else
            {
                if (cboField.Items.Count == 0)
                {
                    cboField.Items.AddRange(new object[] { "Sân A1", "Sân B2", "Sân C3" });
                }
            }

            if (cboType.Items.Count == 0)
            {
                cboType.Items.AddRange(new object[] { "5 người", "7 người", "11 người" });
            }
        }

        private void LoadBookingsFromXML()
        {
            try
            {
                bookingsTable.Clear();

                if (!File.Exists(xmlFilePath))
                {
                    return;
                }

                xmlDoc.Load(xmlFilePath);
                XmlNodeList nodes = xmlDoc.SelectNodes("//Booking");
                foreach (XmlNode node in nodes)
                {
                    DataRow r = bookingsTable.NewRow();
                    r["Mã đặt"] = node.Attributes?["id"]?.Value ?? "";
                    r["Khách hàng"] = node.SelectSingleNode("customer")?.InnerText ?? "";
                    r["Sân"] = node.SelectSingleNode("field")?.InnerText ?? "";
                    r["Loại sân"] = node.SelectSingleNode("type")?.InnerText ?? "";
                    r["Ngày đặt"] = node.SelectSingleNode("date")?.InnerText ?? "";
                    r["Giờ bắt đầu"] = node.SelectSingleNode("time")?.InnerText ?? "";
                    r["Thời lượng"] = node.SelectSingleNode("duration")?.InnerText ?? "";
                    r["Ghi chú"] = node.SelectSingleNode("note")?.InnerText ?? "";
                    bookingsTable.Rows.Add(r);
                }

                if (dgvBookings.Rows.Count > 0)
                {
                    dgvBookings.Rows[0].Selected = true;
                    LoadSelectedBookingToForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu đặt lịch: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvBookings_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBookings.CurrentRow != null && !isAdding && !isEditing)
            {
                LoadSelectedBookingToForm();
            }
        }

        private void LoadSelectedBookingToForm()
        {
            if (dgvBookings.CurrentRow == null) return;

            var row = dgvBookings.CurrentRow;
            txtID.Text = row.Cells["Mã đặt"].Value?.ToString() ?? "";
            cboCustomer.Text = row.Cells["Khách hàng"].Value?.ToString() ?? "";
            cboField.Text = row.Cells["Sân"].Value?.ToString() ?? "";
            cboType.Text = row.Cells["Loại sân"].Value?.ToString() ?? "";
            string dateStr = row.Cells["Ngày đặt"].Value?.ToString() ?? "";
            if (DateTime.TryParse(dateStr, out DateTime d))
                dtpDate.Value = d;
            else
                dtpDate.Value = DateTime.Now;
            txtTime.Text = row.Cells["Giờ bắt đầu"].Value?.ToString() ?? "";
            txtDuration.Text = row.Cells["Thời lượng"].Value?.ToString() ?? "";
            txtNote.Text = row.Cells["Ghi chú"].Value?.ToString() ?? "";
        }

        private void ClearForm()
        {
            txtID.Clear();
            cboCustomer.SelectedIndex = -1;
            cboField.SelectedIndex = -1;
            cboType.SelectedIndex = -1;
            dtpDate.Value = DateTime.Now;
            txtTime.Clear();
            txtDuration.Clear();
            txtNote.Clear();
        }

        private void UpdateRecordCount()
        {
            int count = bookingsTable.DefaultView.Count;
            labelRecordCount.Text = $"Tổng số: {count} lịch đặt";
        }

        private void SetEditMode(bool editMode)
        {
            isEditing = editMode;

            groupBoxInfo.Enabled = editMode;
            btnSave.Enabled = editMode;
            btnCancel.Enabled = editMode;

            btnAdd.Enabled = !editMode;
            btnEdit.Enabled = !editMode && dgvBookings.CurrentRow != null;
            btnDelete.Enabled = !editMode && dgvBookings.CurrentRow != null;
            btnRefresh.Enabled = !editMode;
            btnLoadXML.Enabled = !editMode;
            btnExportExcel.Enabled = !editMode;

            dgvBookings.Enabled = !editMode;
            textBoxSearch.Enabled = !editMode;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            isAdding = true;
            ClearForm();
            SetEditMode(true);
            txtID.Text = GenerateNewId();
            txtID.ReadOnly = true;
            cboCustomer.Focus();
        }

        private string GenerateNewId()
        {
            int max = 0;
            foreach (DataRow r in bookingsTable.Rows)
            {
                string id = r["Mã đặt"]?.ToString() ?? "";
                if (id.StartsWith("B"))
                {
                    if (int.TryParse(id.Substring(1), out int n))
                    {
                        if (n > max) max = n;
                    }
                }
            }
            return "B" + (max + 1).ToString("000");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvBookings.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn lịch cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            isAdding = false;
            SetEditMode(true);
            txtID.ReadOnly = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBookings.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn lịch cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string id = dgvBookings.CurrentRow.Cells["Mã đặt"].Value?.ToString() ?? "";
            string cust = dgvBookings.CurrentRow.Cells["Khách hàng"].Value?.ToString() ?? "";

            if (MessageBox.Show($"Bạn có chắc muốn xóa đặt lịch '{id}' của '{cust}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (File.Exists(xmlFilePath))
                    {
                        xmlDoc.Load(xmlFilePath);
                        XmlNode node = xmlDoc.SelectSingleNode($"//Booking[@id='{id}']");
                        if (node != null)
                        {
                            node.ParentNode.RemoveChild(node);
                            xmlDoc.Save(xmlFilePath);
                        }
                    }

                    DataRow dr = bookingsTable.Rows.Cast<DataRow>().FirstOrDefault(r => r["Mã đặt"].ToString() == id);
                    if (dr != null) bookingsTable.Rows.Remove(dr);

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                if (isAdding)
                    AddBooking();
                else
                    UpdateBooking();

                xmlDoc.Save(xmlFilePath);
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetEditMode(false);
                isAdding = false;
                txtID.ReadOnly = false;
                UpdateRecordCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Mã đặt không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtID.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(cboCustomer.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCustomer.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(cboField.Text))
            {
                MessageBox.Show("Vui lòng chọn sân.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboField.Focus();
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtTime.Text))
            {
                if (!TimeSpan.TryParse(txtTime.Text, out _))
                {
                    MessageBox.Show("Giờ bắt đầu không hợp lệ. Định dạng mong đợi hh:mm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTime.Focus();
                    return false;
                }
            }
            if (isAdding)
            {
                foreach (DataRow r in bookingsTable.Rows)
                {
                    if (r["Mã đặt"].ToString() == txtID.Text)
                    {
                        MessageBox.Show("Mã đặt đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtID.Focus();
                        return false;
                    }
                }
            }
            return true;
        }

        private void AddBooking()
        {
            if (File.Exists(xmlFilePath))
                xmlDoc.Load(xmlFilePath);
            else
            {
                XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDoc.AppendChild(dec);
                XmlElement root = xmlDoc.CreateElement("Bookings");
                xmlDoc.AppendChild(root);
            }

            XmlElement booking = xmlDoc.CreateElement("Booking");
            booking.SetAttribute("id", txtID.Text);

            AddXmlElement(booking, "customer", cboCustomer.Text);
            AddXmlElement(booking, "field", cboField.Text);
            AddXmlElement(booking, "type", cboType.Text);
            AddXmlElement(booking, "date", dtpDate.Value.ToString("yyyy-MM-dd"));
            AddXmlElement(booking, "time", txtTime.Text);
            AddXmlElement(booking, "duration", txtDuration.Text);
            AddXmlElement(booking, "note", txtNote.Text);

            xmlDoc.DocumentElement.AppendChild(booking);
            xmlDoc.Save(xmlFilePath);

            DataRow r = bookingsTable.NewRow();
            PopulateDataRow(r);
            bookingsTable.Rows.Add(r);
        }

        private void UpdateBooking()
        {
            string id = txtID.Text;
            if (File.Exists(xmlFilePath))
            {
                xmlDoc.Load(xmlFilePath);
                XmlNode booking = xmlDoc.SelectSingleNode($"//Booking[@id='{id}']");
                if (booking != null)
                {
                    UpdateXmlElement(booking, "customer", cboCustomer.Text);
                    UpdateXmlElement(booking, "field", cboField.Text);
                    UpdateXmlElement(booking, "type", cboType.Text);
                    UpdateXmlElement(booking, "date", dtpDate.Value.ToString("yyyy-MM-dd"));
                    UpdateXmlElement(booking, "time", txtTime.Text);
                    UpdateXmlElement(booking, "duration", txtDuration.Text);
                    UpdateXmlElement(booking, "note", txtNote.Text);
                    xmlDoc.Save(xmlFilePath);
                }
            }

            DataRow dr = bookingsTable.Rows.Cast<DataRow>().FirstOrDefault(r => r["Mã đặt"].ToString() == id);
            if (dr != null) PopulateDataRow(dr);
        }

        private void AddXmlElement(XmlElement parent, string name, string value)
        {
            XmlElement el = xmlDoc.CreateElement(name);
            el.InnerText = value ?? "";
            parent.AppendChild(el);
        }

        private void UpdateXmlElement(XmlNode parent, string name, string value)
        {
            if (parent == null) return;
            XmlNode node = parent.SelectSingleNode(name);
            if (node != null) node.InnerText = value ?? "";
            else
            {
                XmlElement el = xmlDoc.CreateElement(name);
                el.InnerText = value ?? "";
                parent.AppendChild(el);
            }
        }

        private void PopulateDataRow(DataRow r)
        {
            r["Mã đặt"] = txtID.Text;
            r["Khách hàng"] = cboCustomer.Text;
            r["Sân"] = cboField.Text;
            r["Loại sân"] = cboType.Text;
            r["Ngày đặt"] = dtpDate.Value.ToString("yyyy-MM-dd");
            r["Giờ bắt đầu"] = txtTime.Text;
            r["Thời lượng"] = txtDuration.Text;
            r["Ghi chú"] = txtNote.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            isAdding = false;
            txtID.ReadOnly = false;
            if (dgvBookings.CurrentRow != null)
                LoadSelectedBookingToForm();
            else
                ClearForm();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadBookingsFromXML();
            ClearForm();
            textBoxSearch.Clear();
            UpdateRecordCount();
        }

        private void btnLoadXML_Click(object sender, EventArgs e)
        {
            LoadBookingsFromXML();
            UpdateRecordCount();
            MessageBox.Show("Đã tải dữ liệu từ XML.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string q = textBoxSearch.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(q))
            {
                bookingsTable.DefaultView.RowFilter = "";
            }
            else
            {
                q = q.Replace("'", "''");
                bookingsTable.DefaultView.RowFilter =
                    $"[Mã đặt] LIKE '%{q}%' OR [Khách hàng] LIKE '%{q}%' OR [Sân] LIKE '%{q}%'";
            }
            UpdateRecordCount();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (bookingsTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveDialog.FilterIndex = 1;
            saveDialog.RestoreDirectory = true;
            saveDialog.FileName = "DanhSachDatLich_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");

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
                worksheet.Cells[1, 1] = "DANH SÁCH ĐẶT LỊCH THUÊ SÂN";
                Excel.Range titleRange = worksheet.Range["A1", "H1"];
                titleRange.Merge();
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 16;
                titleRange.Font.Color = Color.White;
                titleRange.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(255, 87, 34));
                titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                titleRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                titleRange.RowHeight = 30;

                // Headers
                for (int i = 0; i < bookingsTable.Columns.Count; i++)
                {
                    worksheet.Cells[3, i + 1] = bookingsTable.Columns[i].ColumnName;
                    Excel.Range headerCell = (Excel.Range)worksheet.Cells[3, i + 1];
                    headerCell.Font.Bold = true;
                    headerCell.Font.Color = Color.White;
                    headerCell.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(255, 152, 0));
                    headerCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                }

                // Data
                for (int i = 0; i < bookingsTable.Rows.Count; i++)
                {
                    for (int j = 0; j < bookingsTable.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 4, j + 1] = bookingsTable.Rows[i][j].ToString();
                    }
                }

                // Auto-fit columns
                worksheet.Columns.AutoFit();

                // Add borders
                Excel.Range dataRange = worksheet.Range["A3", $"H{bookingsTable.Rows.Count + 3}"];
                dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                dataRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

                // Alternate row colors
                for (int i = 4; i <= bookingsTable.Rows.Count + 3; i++)
                {
                    if (i % 2 == 0)
                    {
                        Excel.Range rowRange = worksheet.Range[$"A{i}", $"H{i}"];
                        rowRange.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(255, 243, 224));
                    }
                }

                // Add footer
                int footerRow = bookingsTable.Rows.Count + 5;
                worksheet.Cells[footerRow, 1] = $"Tổng số: {bookingsTable.Rows.Count} lịch đặt";
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