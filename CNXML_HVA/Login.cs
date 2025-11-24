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

namespace CNXML_HVA
{
    public partial class Login : Form
    {
        private bool isPasswordVisible = false;

        public Login()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            btnLogin.Click += BtnLogin_Click;
            btnShowHide.Click += BtnShowHide_Click;
            lblForgotPassword.Click += LblForgotPassword_Click;
            lblRegister.Click += LblRegister_Click;
            txtPassword.KeyPress += TxtPassword_KeyPress;
        }

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnLogin_Click(sender, e);
            }
        }

        private void BtnShowHide_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            txtPassword.PasswordChar = isPasswordVisible ? '\0' : '●';
            btnShowHide.Text = isPasswordVisible ? "🙈" : "👁";
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            try
            {
                string usersPath = DataPaths.GetXmlFilePath("Users.xml");
                DataPaths.EnsureXmlFileExists("Users.xml");

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(usersPath);

                XmlNodeList users = xmlDoc.SelectNodes("//user");
                
                foreach (XmlNode user in users)
                {
                    string userName = user.SelectSingleNode("username")?.InnerText;
                    string userPassword = user.SelectSingleNode("password")?.InnerText;
                    string userRole = user.SelectSingleNode("role")?.InnerText;
                    string userStatus = user.SelectSingleNode("status")?.InnerText;

                    if (userName == username && userPassword == password)
                    {
                        if (userStatus != "Active")
                        {
                            MessageBox.Show("Tài khoản của bạn đã bị vô hiệu hóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (userRole != "Administrator")
                        {
                            MessageBox.Show("Bạn không có quyền truy cập hệ thống!\nChỉ Administrator mới có thể đăng nhập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Cập nhật last_login
                        XmlNode lastLoginNode = user.SelectSingleNode("last_login");
                        if (lastLoginNode != null)
                        {
                            lastLoginNode.InnerText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            xmlDoc.Save(usersPath);
                        }

                        MessageBox.Show($"Đăng nhập thành công!\nChào mừng {user.SelectSingleNode("full_name")?.InnerText}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        this.Hide();
                        DashBoard dashboard = new DashBoard();
                        dashboard.FormClosed += (s, args) => this.Close();
                        dashboard.Show();
                        return;
                    }
                }

                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đăng nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LblForgotPassword_Click(object sender, EventArgs e)
        {
            this.Hide();
            QuenMatKhau quenMatKhau = new QuenMatKhau();
            quenMatKhau.FormClosed += (s, args) => this.Show();
            quenMatKhau.Show();
        }

        private void LblRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            DangKy dangKy = new DangKy();
            dangKy.FormClosed += (s, args) => this.Show();
            dangKy.Show();
        }

        private void picLogo_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
