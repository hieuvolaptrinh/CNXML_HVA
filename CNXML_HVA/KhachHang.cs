using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
// using Excel = Microsoft.Office.Interop.Excel; // Mở comment nếu bạn đã thêm Reference Excel

namespace CNXML_HVA
{
    public partial class KhachHang : Form
    {
        private string xmlFilePath;
        private XmlDocument xmlDoc;
        private bool isEditing = false;
        private bool isAdding = false;
        private DataTable customerTable;

        // CẤU HÌNH CHUỖI KẾT NỐI ĐẾN DB MỚI (dbSANBONG)
        string strConnect = @"Data Source=LAPTOP-DR52OL6S;Initial Catalog=dbSANBONG;Integrated Security=True";

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
            // DataTable giữ nguyên Tiếng Việt để hiển thị trên Grid cho đẹp
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

        // --- CÁC HÀM XỬ LÝ XML ---
        private void LoadCustomersFromXML()
        {
            try
            {
                customerTable.Clear();
                if (!File.Exists(xmlFilePath)) return;

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
                MessageBox.Show("Lỗi tải XML: " + ex.Message);
            }
        }

        private void AddXmlElement(XmlElement parent, string name, string value)
        {
            XmlElement el = xmlDoc.CreateElement(name);
            el.InnerText = value ?? "";
            parent.AppendChild(el);
        }

        // --- CHỨC NĂNG ĐỒNG BỘ 1: SQL (English) -> XML (Pull) ---
        private void buttonSqlToXml_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dữ liệu từ SQL Server sẽ GHI ĐÈ lên file XML hiện tại.\nTiếp tục?",
                "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            SqlConnection conn = new SqlConnection(strConnect);
            try
            {
                conn.Open();
                // Lấy từ bảng Customers (Tiếng Anh)
                string sql = "SELECT * FROM Customers";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dtSQL = new DataTable();
                da.Fill(dtSQL);

                if (dtSQL.Rows.Count == 0)
                {
                    MessageBox.Show("Database SQL (Table Customers) đang trống!", "Thông báo");
                    return;
                }

                // Tạo mới XML
                xmlDoc = new XmlDocument();
                XmlElement root = xmlDoc.CreateElement("customers");
                xmlDoc.AppendChild(root);

                foreach (DataRow row in dtSQL.Rows)
                {
                    XmlElement customer = xmlDoc.CreateElement("customer");

                    // Mapping: Cột SQL (English) -> XML Attribute/Tag
                    customer.SetAttribute("id", row["id"].ToString());

                    AddXmlElement(customer, "name", row["name"].ToString());
                    AddXmlElement(customer, "phone", row["phone"].ToString());
                    AddXmlElement(customer, "email", row["email"].ToString());

                    XmlElement address = xmlDoc.CreateElement("address");
                    AddXmlElement(address, "city", row["city"].ToString());
                    AddXmlElement(address, "district", row["district"].ToString());
                    AddXmlElement(address, "street", row["street"].ToString());
                    customer.AppendChild(address);

                    AddXmlElement(customer, "membership", row["membership"].ToString());
                    AddXmlElement(customer, "notes", row["notes"].ToString());

                    root.AppendChild(customer);
                }

                xmlDoc.Save(xmlFilePath);
                LoadCustomersFromXML();
                UpdateRecordCount();
                MessageBox.Show($"Đã kéo {dtSQL.Rows.Count} khách hàng từ SQL về XML!", "Thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { conn.Close(); }
        }

        // --- CHỨC NĂNG ĐỒNG BỘ 2: XML -> SQL (Push) ---
        private void buttonXmlToSql_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Đồng bộ dữ liệu từ XML lên SQL Server?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            SqlConnection conn = new SqlConnection(strConnect);
            try
            {
                conn.Open();
                int inserted = 0, updated = 0;

                // Duyệt qua DataTable (đang có cột Tiếng Việt)
                foreach (DataRow row in customerTable.Rows)
                {
                    string id = row["Mã KH"].ToString(); // Lấy ID

                    // Kiểm tra tồn tại trong bảng Customers (English)
                    SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM Customers WHERE id = @id", conn);
                    cmdCheck.Parameters.AddWithValue("@id", id);
                    int exists = (int)cmdCheck.ExecuteScalar();

                    string sql = "";
                    if (exists > 0)
                    {
                        // Update bảng Customers (English)
                        sql = @"UPDATE Customers SET 
                                name=@name, phone=@phone, email=@email, 
                                city=@city, district=@district, street=@street, 
                                membership=@membership, notes=@notes 
                                WHERE id=@id";
                        updated++;
                    }
                    else
                    {
                        // Insert bảng Customers (English)
                        sql = @"INSERT INTO Customers (id, name, phone, email, city, district, street, membership, notes) 
                                VALUES (@id, @name, @phone, @email, @city, @district, @street, @membership, @notes)";
                        inserted++;
                    }

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Mapping: Tham số SQL (English) <- Dữ liệu Grid (Tiếng Việt)
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", row["Tên khách hàng"] ?? "");
                    cmd.Parameters.AddWithValue("@phone", row["Số điện thoại"] ?? "");
                    cmd.Parameters.AddWithValue("@email", row["Email"] ?? "");
                    cmd.Parameters.AddWithValue("@city", row["Thành phố"] ?? "");
                    cmd.Parameters.AddWithValue("@district", row["Quận/Huyện"] ?? "");
                    cmd.Parameters.AddWithValue("@street", row["Đường"] ?? "");
                    cmd.Parameters.AddWithValue("@membership", row["Hạng thành viên"] ?? "");
                    cmd.Parameters.AddWithValue("@notes", row["Ghi chú"] ?? "");

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"Đồng bộ xong!\nThêm mới: {inserted}\nCập nhật: {updated}", "Thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đồng bộ SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { conn.Close(); }
        }

        // --- CÁC HÀM GIAO DIỆN & CRUD CƠ BẢN (Giữ nguyên) ---
        private void SetupDataGridView()
        {
            dataGridViewCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCustomers.MultiSelect = false;
            dataGridViewCustomers.ReadOnly = true;

            // Style
            dataGridViewCustomers.BackgroundColor = Color.White;
            dataGridViewCustomers.GridColor = Color.FromArgb(224, 224, 224);
            dataGridViewCustomers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(179, 229, 252);
            dataGridViewCustomers.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 33, 33);
            dataGridViewCustomers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            dataGridViewCustomers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewCustomers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCustomers.EnableHeadersVisualStyles = false;
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

            // Khóa nút SQL khi đang nhập liệu để tránh lỗi
            buttonSqlToXml.Enabled = !editMode;
            buttonXmlToSql.Enabled = !editMode;
        }

        private void ClearForm()
        {
            textBoxId.Clear(); textBoxName.Clear(); textBoxPhone.Clear(); textBoxEmail.Clear();
            textBoxCity.Clear(); textBoxDistrict.Clear(); textBoxStreet.Clear();
            comboBoxMembership.SelectedIndex = -1; textBoxNote.Clear();
        }

        private void UpdateRecordCount()
        {
            labelRecordCount.Text = $"Tổng số: {customerTable.DefaultView.Count} khách hàng";
        }

        private string GenerateNewId()
        {
            int max = 0;
            foreach (DataRow r in customerTable.Rows)
            {
                string id = r["Mã KH"].ToString();
                if (id.StartsWith("C") && int.TryParse(id.Substring(1), out int num))
                {
                    if (num > max) max = num;
                }
            }
            return "C" + (max + 1).ToString("000");
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBoxId.Text) || string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã KH và Tên!");
                return false;
            }
            return true;
        }

        private void LoadSelectedCustomerToForm()
        {
            if (dataGridViewCustomers.CurrentRow == null) return;
            var row = dataGridViewCustomers.CurrentRow;
            textBoxId.Text = row.Cells["Mã KH"].Value.ToString();
            textBoxName.Text = row.Cells["Tên khách hàng"].Value.ToString();
            textBoxPhone.Text = row.Cells["Số điện thoại"].Value.ToString();
            textBoxEmail.Text = row.Cells["Email"].Value.ToString();
            textBoxCity.Text = row.Cells["Thành phố"].Value.ToString();
            textBoxDistrict.Text = row.Cells["Quận/Huyện"].Value.ToString();
            textBoxStreet.Text = row.Cells["Đường"].Value.ToString();
            comboBoxMembership.Text = row.Cells["Hạng thành viên"].Value.ToString();
            textBoxNote.Text = row.Cells["Ghi chú"].Value.ToString();
        }

        // --- CÁC SỰ KIỆN NÚT BẤM CRUD ---
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            isAdding = true; ClearForm(); SetEditMode(true);
            textBoxId.Text = GenerateNewId(); textBoxName.Focus();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewCustomers.CurrentRow == null) return;
            isAdding = false; SetEditMode(true); textBoxName.Focus();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewCustomers.CurrentRow == null) return;
            string id = dataGridViewCustomers.CurrentRow.Cells["Mã KH"].Value.ToString();
            if (MessageBox.Show("Xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                XmlNode node = xmlDoc.SelectSingleNode($"//customer[@id='{id}']");
                if (node != null)
                {
                    node.ParentNode.RemoveChild(node);
                    xmlDoc.Save(xmlFilePath);
                    LoadCustomersFromXML(); UpdateRecordCount();
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            if (isAdding)
            {
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

                XmlNode root = xmlDoc.SelectSingleNode("customers");
                if (root == null) { root = xmlDoc.CreateElement("customers"); xmlDoc.AppendChild(root); }
                root.AppendChild(customer);
            }
            else
            {
                string id = textBoxId.Text;
                XmlNode node = xmlDoc.SelectSingleNode($"//customer[@id='{id}']");
                if (node != null)
                {
                    UpdateXml(node, "name", textBoxName.Text);
                    UpdateXml(node, "phone", textBoxPhone.Text);
                    UpdateXml(node, "email", textBoxEmail.Text);

                    XmlNode address = node.SelectSingleNode("address");
                    if (address == null) { address = xmlDoc.CreateElement("address"); node.AppendChild(address); }

                    UpdateXml(address, "city", textBoxCity.Text);
                    UpdateXml(address, "district", textBoxDistrict.Text);
                    UpdateXml(address, "street", textBoxStreet.Text);

                    UpdateXml(node, "membership", comboBoxMembership.Text);
                    UpdateXml(node, "notes", textBoxNote.Text);
                }
            }

            xmlDoc.Save(xmlFilePath);
            SetEditMode(false);
            LoadCustomersFromXML();
            UpdateRecordCount();
            MessageBox.Show("Lưu thành công!");
        }

        private void UpdateXml(XmlNode parent, string name, string value)
        {
            XmlNode n = parent.SelectSingleNode(name);
            if (n == null) { n = xmlDoc.CreateElement(name); parent.AppendChild(n); }
            n.InnerText = value ?? "";
        }

        private void buttonCancel_Click(object sender, EventArgs e) { SetEditMode(false); LoadSelectedCustomerToForm(); }
        private void buttonRefresh_Click(object sender, EventArgs e) { LoadCustomersFromXML(); textBoxSearch.Clear(); }

        // Chức năng Export Excel (để trống nếu bạn chưa cài thư viện)
        private void buttonExportExcel_Click(object sender, EventArgs e) { /* Code excel cũ */ }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string search = textBoxSearch.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(search)) customerTable.DefaultView.RowFilter = "";
            else customerTable.DefaultView.RowFilter = $"[Tên khách hàng] LIKE '%{search}%' OR [Số điện thoại] LIKE '%{search}%'";
            UpdateRecordCount();
        }
        private void dataGridViewCustomers_SelectionChanged(object sender, EventArgs e) { if (!isAdding && !isEditing) LoadSelectedCustomerToForm(); }
        private void groupBoxCustomerInfo_Enter(object sender, EventArgs e) { }
    }
}