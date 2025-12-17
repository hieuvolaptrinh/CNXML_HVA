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
    public partial class QuenMatKhau : Form
    {
        private string verificationCode = "";
        private string userEmail = "";
        private List<TextBox> codeTextBoxes;

        public QuenMatKhau()
        {
            InitializeComponent();
            SetupEventHandlers();
            SetupCodeTextBoxes();
        }

        private void SetupEventHandlers()
        {
            btnSendCode.Click += BtnSendCode_Click;
            lblBackToLogin.Click += LblBackToLogin_Click;
        }

        private void SetupCodeTextBoxes()
        {
            codeTextBoxes = new List<TextBox> { txtCode1, txtCode2, txtCode3, txtCode4, txtCode5, txtCode6 };

            for (int i = 0; i < codeTextBoxes.Count; i++)
            {
                int index = i;
                codeTextBoxes[i].TextChanged += (s, e) => CodeTextBox_TextChanged(s, e, index);
                codeTextBoxes[i].KeyPress += CodeTextBox_KeyPress;
            }
        }

        private void CodeTextBox_TextChanged(object sender, EventArgs e, int index)
        {
            TextBox currentTextBox = (TextBox)sender;

            if (currentTextBox.Text.Length == 1 && index < codeTextBoxes.Count - 1)
            {
                codeTextBoxes[index + 1].Focus();
            }

            // Kiểm tra khi đã nhập đủ 6 số
            if (index == 5 && currentTextBox.Text.Length == 1)
            {
                VerifyCode();
            }
        }

        private void CodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void BtnSendCode_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            try
            {
                string usersPath = DataPaths.GetXmlFilePath("Users.xml");
                DataPaths.EnsureXmlFileExists("Users.xml");

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(usersPath);

                XmlNode user = xmlDoc.SelectSingleNode($"//user[email='{email}']");

                if (user == null)
                {
                    MessageBox.Show("Email không tồn tại trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra role
                string role = user.SelectSingleNode("role")?.InnerText;
                if (role != "Administrator")
                {
                    MessageBox.Show("Chỉ tài khoản Administrator mới có thể khôi phục mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tạo mã xác thực 6 số ngẫu nhiên
                Random random = new Random();
                verificationCode = random.Next(100000, 999999).ToString();
                userEmail = email;

                // Hiển thị mã (trong thực tế sẽ gửi qua email)
                MessageBox.Show($"Mã xác thực của bạn là: {verificationCode}\n\n(Trong thực tế, mã này sẽ được gửi qua email)", "Mã xác thực", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Chuyển sang giao diện nhập mã
                pnlEmail.Visible = false;
                btnSendCode.Visible = false;
                lblInstruction.Visible = false;
                pnlVerificationCode.Visible = true;
                txtCode1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VerifyCode()
        {
            string enteredCode = string.Join("", codeTextBoxes.Select(tb => tb.Text));

            if (enteredCode.Length != 6)
            {
                return;
            }

            if (enteredCode == verificationCode)
            {
                try
                {
                    string usersPath = DataPaths.GetXmlFilePath("Users.xml");
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(usersPath);

                    XmlNode user = xmlDoc.SelectSingleNode($"//user[email='{userEmail}']");
                    if (user != null)
                    {
                        string userName = user.SelectSingleNode("full_name")?.InnerText;
                        string password = user.SelectSingleNode("password")?.InnerText;

                        MessageBox.Show($"Xác thực thành công!\n\nThông tin đăng nhập:\nEmail: {userEmail}\nMật khẩu: {password}\n\nVui lòng đăng nhập và đổi mật khẩu ngay.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Mã xác thực không đúng!\nVui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Clear các ô nhập
                foreach (var tb in codeTextBoxes)
                {
                    tb.Clear();
                }
                txtCode1.Focus();
            }
        }

        private void LblBackToLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void QuenMatKhau_Load(object sender, EventArgs e)
        {

        }

        private void QuenMatKhau_Load_1(object sender, EventArgs e)
        {

        }
    }
}
