using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Drawing.Printing;
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
            btnExportPDF.Enabled = !editMode && dgvBookings.CurrentRow != null;
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

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            if (dgvBookings.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một đặt lịch để xuất PDF!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dgvBookings.CurrentRow;
            string bookingId = row.Cells["Mã đặt"].Value?.ToString() ?? "";
            string customer = row.Cells["Khách hàng"].Value?.ToString() ?? "";
            string field = row.Cells["Sân"].Value?.ToString() ?? "";
            string fieldType = row.Cells["Loại sân"].Value?.ToString() ?? "";
            string date = row.Cells["Ngày đặt"].Value?.ToString() ?? "";
            string time = row.Cells["Giờ bắt đầu"].Value?.ToString() ?? "";
            string duration = row.Cells["Thời lượng"].Value?.ToString() ?? "";
            string note = row.Cells["Ghi chú"].Value?.ToString() ?? "";

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveDialog.FilterIndex = 1;
            saveDialog.RestoreDirectory = true;
            saveDialog.FileName = $"DatLich_{bookingId}_{DateTime.Now:yyyyMMdd_HHmmss}";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Create PDF content
                    ExportBookingToPdf(saveDialog.FileName, bookingId, customer, field, fieldType, date, time, duration, note);
                    MessageBox.Show($"Xuất PDF thành công!\nĐường dẫn: {saveDialog.FileName}",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất PDF: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportBookingToPdf(string filePath, string bookingId, string customer, string field, 
            string fieldType, string date, string time, string duration, string note)
        {
            // Create traditional office-style HTML content
            string htmlContent = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Phiếu đặt sân - {bookingId}</title>
    <style>
        body {{
            font-family: 'Times New Roman', Times, serif;
            background-color: #1a1a2e;
            padding: 30px;
            margin: 0;
        }}
        
        .paper {{
            max-width: 700px;
            margin: 0 auto;
            background: #fffef5;
            border: 3px solid #2d3748;
            box-shadow: 8px 8px 0 #000;
        }}
        
        .header {{
            background: #1e3a5f;
            color: #fff;
            padding: 25px 30px;
            border-bottom: 4px solid #c9a227;
            text-align: center;
        }}
        
        .header h1 {{
            font-size: 24px;
            font-weight: bold;
            margin: 0 0 8px 0;
            letter-spacing: 3px;
        }}
        
        .header h2 {{
            font-size: 16px;
            font-weight: normal;
            margin: 0;
            border: 2px solid #fff;
            display: inline-block;
            padding: 5px 20px;
        }}
        
        .booking-code {{
            background: #c9a227;
            color: #1e3a5f;
            padding: 8px 15px;
            font-size: 14px;
            font-weight: bold;
            display: inline-block;
            margin-top: 15px;
            border: 2px solid #1e3a5f;
        }}
        
        .content {{
            padding: 30px;
        }}
        
        .info-table {{
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 25px;
        }}
        
        .info-table th,
        .info-table td {{
            border: 2px solid #2d3748;
            padding: 12px 15px;
            text-align: left;
        }}
        
        .info-table th {{
            background: #e8e4d9;
            font-weight: bold;
            width: 35%;
            color: #1e3a5f;
        }}
        
        .info-table td {{
            background: #fff;
            font-size: 15px;
        }}
        
        .info-table .highlight {{
            background: #fff8dc;
            font-weight: bold;
            color: #8b4513;
        }}
        
        .note-box {{
            background: #fff8dc;
            border: 2px solid #c9a227;
            padding: 15px;
            margin-bottom: 30px;
        }}
        
        .note-box strong {{
            color: #8b4513;
        }}
        
        .signature-section {{
            margin-top: 40px;
            display: flex;
            justify-content: space-between;
        }}
        
        .sign-box {{
            width: 45%;
            text-align: center;
        }}
        
        .sign-box .title {{
            font-weight: bold;
            margin-bottom: 80px;
            font-size: 14px;
            border-bottom: 1px solid #2d3748;
            padding-bottom: 5px;
        }}
        
        .sign-box .line {{
            border-bottom: 2px solid #2d3748;
            margin-bottom: 8px;
        }}
        
        .sign-box .hint {{
            font-size: 12px;
            font-style: italic;
            color: #666;
        }}
        
        .footer {{
            background: #2d3748;
            color: #fff;
            padding: 12px 30px;
            text-align: center;
            font-size: 12px;
            border-top: 3px solid #c9a227;
        }}
        
        @media print {{
            body {{ background: none; padding: 0; }}
            .paper {{ box-shadow: none; border: 2px solid #000; }}
        }}
    </style>
</head>
<body>
    <div class='paper'>
        <div class='header'>
            <h1>⚽ SÂN BÓNG MINI ⚽</h1>
            <h2>PHIẾU XÁC NHẬN ĐẶT SÂN</h2>
            <div class='booking-code'>MÃ PHIẾU: {bookingId}</div>
        </div>
        
        <div class='content'>
            <table class='info-table'>
                <tr>
                    <th>Khách hàng</th>
                    <td class='highlight'>{customer}</td>
                </tr>
                <tr>
                    <th>Sân bóng</th>
                    <td class='highlight'>{field}</td>
                </tr>
                <tr>
                    <th>Loại sân</th>
                    <td>{fieldType}</td>
                </tr>
                <tr>
                    <th>Ngày đặt</th>
                    <td class='highlight'>{date}</td>
                </tr>
                <tr>
                    <th>Giờ bắt đầu</th>
                    <td class='highlight'>{time}</td>
                </tr>
                <tr>
                    <th>Thời lượng</th>
                    <td>{duration} giờ</td>
                </tr>
            </table>
            
            {(string.IsNullOrWhiteSpace(note) ? "" : $@"
            <div class='note-box'>
                <strong>📝 GHI CHÚ:</strong> {note}
            </div>
            ")}
            
            <div class='signature-section'>
                <div class='sign-box'>
                    <div class='title'>NGƯỜI ĐẶT SÂN</div>
                    <div class='line'></div>
                    <div class='hint'>(Ký và ghi rõ họ tên)</div>
                </div>
                <div class='sign-box'>
                    <div class='title'>NHÂN VIÊN</div>
                    <div class='line'></div>
                    <div class='hint'>(Ký và ghi rõ họ tên)</div>
                </div>
            </div>
        </div>
        
        <div class='footer'>
            Ngày xuất phiếu: {DateTime.Now:dd/MM/yyyy HH:mm} | Cảm ơn quý khách đã sử dụng dịch vụ!
        </div>
    </div>
</body>
</html>";

            // Save as HTML first
            string htmlPath = Path.ChangeExtension(filePath, ".html");
            File.WriteAllText(htmlPath, htmlContent, System.Text.Encoding.UTF8);

            // Try to convert to PDF using Microsoft Edge headless mode
            bool pdfCreated = false;
            try
            {
                // Try using Microsoft Edge to convert HTML to PDF
                string edgePath = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
                if (!File.Exists(edgePath))
                {
                    edgePath = @"C:\Program Files\Microsoft\Edge\Application\msedge.exe";
                }

                if (File.Exists(edgePath))
                {
                    var psi = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = edgePath,
                        Arguments = $"--headless --disable-gpu --print-to-pdf=\"{filePath}\" \"{htmlPath}\"",
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };

                    using (var process = System.Diagnostics.Process.Start(psi))
                    {
                        process.WaitForExit(10000); // Wait max 10 seconds
                        
                        // Check if PDF was created
                        if (File.Exists(filePath))
                        {
                            pdfCreated = true;
                            // Delete the HTML file since PDF was created
                            try { File.Delete(htmlPath); } catch { }
                        }
                    }
                }
            }
            catch
            {
                // Edge conversion failed, fall back to HTML
            }

            if (!pdfCreated)
            {
                // If PDF creation failed, open HTML in browser for manual print
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = htmlPath,
                        UseShellExecute = true
                    });

                    MessageBox.Show(
                        $"⚠️ Không thể tự động tạo PDF.\n\n" +
                        $"Đã tạo file HTML tại:\n{htmlPath}\n\n" +
                        $"Để lưu dưới dạng PDF:\n" +
                        $"1. File HTML đã mở trong trình duyệt\n" +
                        $"2. Nhấn Ctrl+P để in\n" +
                        $"3. Chọn 'Save as PDF' hoặc 'Microsoft Print to PDF'",
                        "Hướng dẫn xuất PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show(
                        $"Đã tạo file HTML tại:\n{htmlPath}\n\n" +
                        $"Vui lòng mở file này trong trình duyệt và in dưới dạng PDF.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}