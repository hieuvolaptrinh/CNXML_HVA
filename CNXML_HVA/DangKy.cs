using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CNXML_HVA
{
    public partial class DangKy : Form
    {
        private bool isPasswordVisible = false;
        private bool isConfirmPasswordVisible = false;

        public DangKy()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            btnRegister.Click += BtnRegister_Click;
            btnShowHidePassword.Click += BtnShowHidePassword_Click;
            btnShowHideConfirm.Click += BtnShowHideConfirm_Click;
            lblLogin.Click += LblLogin_Click;
        }

        private void BtnShowHidePassword_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            txtPassword.PasswordChar = isPasswordVisible ? '\0' : '●';
            btnShowHidePassword.Text = isPasswordVisible ? "🙈" : "👁";
        }

        private void BtnShowHideConfirm_Click(object sender, EventArgs e)
        {
            isConfirmPasswordVisible = !isConfirmPasswordVisible;
            txtConfirmPassword.PasswordChar = isConfirmPasswordVisible ? '\0' : '●';
            btnShowHideConfirm.Text = isConfirmPasswordVisible ? "🙈" : "👁";
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            // Validation
            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Vui lòng nhập họ và tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            if (!IsValidPhone(phone))
            {
                MessageBox.Show("Số điện thoại không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            if (!chkAgree.Checked)
            {
                MessageBox.Show("Bạn phải đồng ý với điều khoản sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string usersPath = DataPaths.GetXmlFilePath("Users.xml");
                DataPaths.EnsureXmlFileExists("Users.xml");

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(usersPath);

                // Kiểm tra email đã tồn tại
                XmlNode existingUser = xmlDoc.SelectSingleNode($"//user[email='{email}']");
                if (existingUser != null)
                {
                    MessageBox.Show("Email này đã được đăng ký!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tạo ID mới
                string newUserId = GenerateNewUserId(xmlDoc);

                // Tạo user mới
                XmlNode usersRoot = xmlDoc.SelectSingleNode("users");
                XmlNode newUser = xmlDoc.CreateElement("user");
                XmlAttribute idAttr = xmlDoc.CreateAttribute("id");
                idAttr.Value = newUserId;
                newUser.Attributes.Append(idAttr);

                AddElement(xmlDoc, newUser, "username", email.Split('@')[0]);
                AddElement(xmlDoc, newUser, "password", password);
                AddElement(xmlDoc, newUser, "full_name", fullName);
                AddElement(xmlDoc, newUser, "email", email);
                AddElement(xmlDoc, newUser, "phone", phone);
                AddElement(xmlDoc, newUser, "role", "Administrator");
                AddElement(xmlDoc, newUser, "branch_id", "B01");
                AddElement(xmlDoc, newUser, "status", "Active");
                AddElement(xmlDoc, newUser, "created_date", DateTime.Now.ToString("yyyy-MM-dd"));
                AddElement(xmlDoc, newUser, "last_login", "");

                usersRoot.AppendChild(newUser);
                xmlDoc.Save(usersPath);

                MessageBox.Show("Đăng ký thành công!\nBạn có thể đăng nhập ngay bây giờ.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đăng ký: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddElement(XmlDocument doc, XmlNode parent, string name, string value)
        {
            XmlNode node = doc.CreateElement(name);
            node.InnerText = value;
            parent.AppendChild(node);
        }

        private string GenerateNewUserId(XmlDocument xmlDoc)
        {
            XmlNodeList users = xmlDoc.SelectNodes("//user");
            int maxId = 0;

            foreach (XmlNode user in users)
            {
                string id = user.Attributes["id"]?.Value;
                if (!string.IsNullOrEmpty(id) && id.StartsWith("U"))
                {
                    if (int.TryParse(id.Substring(1), out int numId))
                    {
                        maxId = Math.Max(maxId, numId);
                    }
                }
            }

            return $"U{(maxId + 1):D3}";
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private bool IsValidPhone(string phone)
        {
            string pattern = @"^0\d{9}$";
            return Regex.IsMatch(phone, pattern);
        }

        private void LblLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
