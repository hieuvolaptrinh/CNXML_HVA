using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

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
            xmlFilePath = Path.Combine(Application.StartupPath, "Customers.xml");
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
        }

        private void LoadCustomersFromXML()
        {
            try
            {
                customerTable.Clear();

                if (!File.Exists(xmlFilePath))
                {
                    // không thông báo lỗi ở load — sẽ tạo file khi lưu bản ghi đầu tiên
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

                // nếu có hàng, chọn dòng đầu để hiển thị
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

            dataGridViewCustomers.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCustomers.GridColor = System.Drawing.Color.FromArgb(76, 175, 80);
            dataGridViewCustomers.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(200, 230, 201);
            dataGridViewCustomers.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(27, 94, 32);
            dataGridViewCustomers.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            dataGridViewCustomers.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dataGridViewCustomers.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
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

                // Save XML to disk
                xmlDoc.Save(xmlFilePath);
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetEditMode(false);
                isAdding = false;
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

            // kiểm tra trùng id khi thêm
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
            // đảm bảo có node gốc
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
                // nếu chưa có node, tạo mới
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
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string search = textBoxSearch.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(search))
                customerTable.DefaultView.RowFilter = "";
            else
                customerTable.DefaultView.RowFilter = $"[Tên khách hàng] LIKE '%{search}%' OR [Số điện thoại] LIKE '%{search}%'";
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
    }
}
