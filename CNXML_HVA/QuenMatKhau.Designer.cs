namespace CNXML_HVA
{
    partial class QuenMatKhau
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlForgotContainer = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitleForgot = new System.Windows.Forms.Label();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.pnlEmail = new System.Windows.Forms.Panel();
            this.picEmail = new System.Windows.Forms.PictureBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.btnSendCode = new System.Windows.Forms.Button();
            this.pnlVerificationCode = new System.Windows.Forms.Panel();
            this.lblVerificationTitle = new System.Windows.Forms.Label();
            this.pnlCodeInput = new System.Windows.Forms.Panel();
            this.txtCode1 = new System.Windows.Forms.TextBox();
            this.txtCode2 = new System.Windows.Forms.TextBox();
            this.txtCode3 = new System.Windows.Forms.TextBox();
            this.txtCode4 = new System.Windows.Forms.TextBox();
            this.txtCode5 = new System.Windows.Forms.TextBox();
            this.txtCode6 = new System.Windows.Forms.TextBox();
            this.lblBackToLogin = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.pnlRight.SuspendLayout();
            this.pnlForgotContainer.SuspendLayout();
            this.pnlEmail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmail)).BeginInit();
            this.pnlVerificationCode.SuspendLayout();
            this.pnlCodeInput.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.Controls.Add(this.pnlForgotContainer);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(467, 0);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(4);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(600, 554);
            this.pnlRight.TabIndex = 1;
            // 
            // pnlForgotContainer
            // 
            this.pnlForgotContainer.Controls.Add(this.lblTitle);
            this.pnlForgotContainer.Controls.Add(this.lblSubtitleForgot);
            this.pnlForgotContainer.Controls.Add(this.lblInstruction);
            this.pnlForgotContainer.Controls.Add(this.pnlEmail);
            this.pnlForgotContainer.Controls.Add(this.btnSendCode);
            this.pnlForgotContainer.Controls.Add(this.pnlVerificationCode);
            this.pnlForgotContainer.Controls.Add(this.lblBackToLogin);
            this.pnlForgotContainer.Location = new System.Drawing.Point(47, 49);
            this.pnlForgotContainer.Margin = new System.Windows.Forms.Padding(4);
            this.pnlForgotContainer.Name = "pnlForgotContainer";
            this.pnlForgotContainer.Size = new System.Drawing.Size(507, 468);
            this.pnlForgotContainer.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.lblTitle.Location = new System.Drawing.Point(8, 9);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(307, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quên Mật Khẩu?";
            // 
            // lblSubtitleForgot
            // 
            this.lblSubtitleForgot.AutoSize = true;
            this.lblSubtitleForgot.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitleForgot.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitleForgot.Location = new System.Drawing.Point(11, 68);
            this.lblSubtitleForgot.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtitleForgot.Name = "lblSubtitleForgot";
            this.lblSubtitleForgot.Size = new System.Drawing.Size(246, 20);
            this.lblSubtitleForgot.TabIndex = 1;
            this.lblSubtitleForgot.Text = "Đừng lo lắng, chúng tôi sẽ giúp bạn";
            // 
            // lblInstruction
            // 
            this.lblInstruction.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblInstruction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblInstruction.Location = new System.Drawing.Point(11, 105);
            this.lblInstruction.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(480, 45);
            this.lblInstruction.TabIndex = 2;
            this.lblInstruction.Text = "Nhập địa chỉ email của bạn và chúng tôi sẽ gửi mã xác thực để đặt lại mật khẩu.";
            // 
            // pnlEmail
            // 
            this.pnlEmail.BackColor = System.Drawing.Color.White;
            this.pnlEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEmail.Controls.Add(this.picEmail);
            this.pnlEmail.Controls.Add(this.txtEmail);
            this.pnlEmail.Controls.Add(this.lblEmail);
            this.pnlEmail.Location = new System.Drawing.Point(0, 165);
            this.pnlEmail.Margin = new System.Windows.Forms.Padding(4);
            this.pnlEmail.Name = "pnlEmail";
            this.pnlEmail.Size = new System.Drawing.Size(506, 80);
            this.pnlEmail.TabIndex = 3;
            // 
            // picEmail
            // 
            this.picEmail.BackColor = System.Drawing.Color.Transparent;
            this.picEmail.Location = new System.Drawing.Point(16, 37);
            this.picEmail.Margin = new System.Windows.Forms.Padding(4);
            this.picEmail.Name = "picEmail";
            this.picEmail.Size = new System.Drawing.Size(32, 30);
            this.picEmail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picEmail.TabIndex = 0;
            this.picEmail.TabStop = false;
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtEmail.Location = new System.Drawing.Point(60, 37);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(427, 25);
            this.txtEmail.TabIndex = 2;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.lblEmail.Location = new System.Drawing.Point(56, 7);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(96, 20);
            this.lblEmail.TabIndex = 1;
            this.lblEmail.Text = "Địa chỉ Email";
            // 
            // btnSendCode
            // 
            this.btnSendCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.btnSendCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSendCode.FlatAppearance.BorderSize = 0;
            this.btnSendCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendCode.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSendCode.ForeColor = System.Drawing.Color.White;
            this.btnSendCode.Location = new System.Drawing.Point(0, 268);
            this.btnSendCode.Margin = new System.Windows.Forms.Padding(4);
            this.btnSendCode.Name = "btnSendCode";
            this.btnSendCode.Size = new System.Drawing.Size(507, 55);
            this.btnSendCode.TabIndex = 4;
            this.btnSendCode.Text = "GỬI MÃ XÁC THỰC";
            this.btnSendCode.UseVisualStyleBackColor = false;
            // 
            // pnlVerificationCode
            // 
            this.pnlVerificationCode.Controls.Add(this.lblVerificationTitle);
            this.pnlVerificationCode.Controls.Add(this.pnlCodeInput);
            this.pnlVerificationCode.Location = new System.Drawing.Point(0, 105);
            this.pnlVerificationCode.Name = "pnlVerificationCode";
            this.pnlVerificationCode.Size = new System.Drawing.Size(507, 280);
            this.pnlVerificationCode.TabIndex = 5;
            this.pnlVerificationCode.Visible = false;
            // 
            // lblVerificationTitle
            // 
            this.lblVerificationTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVerificationTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblVerificationTitle.Location = new System.Drawing.Point(11, 10);
            this.lblVerificationTitle.Name = "lblVerificationTitle";
            this.lblVerificationTitle.Size = new System.Drawing.Size(480, 50);
            this.lblVerificationTitle.TabIndex = 0;
            this.lblVerificationTitle.Text = "Nhập mã xác thực 6 chữ số đã được gửi đến email của bạn";
            this.lblVerificationTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCodeInput
            // 
            this.pnlCodeInput.Controls.Add(this.txtCode1);
            this.pnlCodeInput.Controls.Add(this.txtCode2);
            this.pnlCodeInput.Controls.Add(this.txtCode3);
            this.pnlCodeInput.Controls.Add(this.txtCode4);
            this.pnlCodeInput.Controls.Add(this.txtCode5);
            this.pnlCodeInput.Controls.Add(this.txtCode6);
            this.pnlCodeInput.Location = new System.Drawing.Point(15, 70);
            this.pnlCodeInput.Name = "pnlCodeInput";
            this.pnlCodeInput.Size = new System.Drawing.Size(477, 70);
            this.pnlCodeInput.TabIndex = 1;
            // 
            // txtCode1
            // 
            this.txtCode1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCode1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.txtCode1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.txtCode1.Location = new System.Drawing.Point(10, 10);
            this.txtCode1.MaxLength = 1;
            this.txtCode1.Name = "txtCode1";
            this.txtCode1.Size = new System.Drawing.Size(60, 52);
            this.txtCode1.TabIndex = 0;
            this.txtCode1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCode2
            // 
            this.txtCode2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCode2.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.txtCode2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.txtCode2.Location = new System.Drawing.Point(80, 10);
            this.txtCode2.MaxLength = 1;
            this.txtCode2.Name = "txtCode2";
            this.txtCode2.Size = new System.Drawing.Size(60, 52);
            this.txtCode2.TabIndex = 1;
            this.txtCode2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCode3
            // 
            this.txtCode3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCode3.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.txtCode3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.txtCode3.Location = new System.Drawing.Point(150, 10);
            this.txtCode3.MaxLength = 1;
            this.txtCode3.Name = "txtCode3";
            this.txtCode3.Size = new System.Drawing.Size(60, 52);
            this.txtCode3.TabIndex = 2;
            this.txtCode3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCode4
            // 
            this.txtCode4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCode4.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.txtCode4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.txtCode4.Location = new System.Drawing.Point(237, 10);
            this.txtCode4.MaxLength = 1;
            this.txtCode4.Name = "txtCode4";
            this.txtCode4.Size = new System.Drawing.Size(60, 52);
            this.txtCode4.TabIndex = 3;
            this.txtCode4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCode5
            // 
            this.txtCode5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCode5.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.txtCode5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.txtCode5.Location = new System.Drawing.Point(307, 10);
            this.txtCode5.MaxLength = 1;
            this.txtCode5.Name = "txtCode5";
            this.txtCode5.Size = new System.Drawing.Size(60, 52);
            this.txtCode5.TabIndex = 4;
            this.txtCode5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCode6
            // 
            this.txtCode6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCode6.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.txtCode6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.txtCode6.Location = new System.Drawing.Point(377, 10);
            this.txtCode6.MaxLength = 1;
            this.txtCode6.Name = "txtCode6";
            this.txtCode6.Size = new System.Drawing.Size(60, 52);
            this.txtCode6.TabIndex = 5;
            this.txtCode6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblBackToLogin
            // 
            this.lblBackToLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblBackToLogin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBackToLogin.ForeColor = System.Drawing.Color.Gray;
            this.lblBackToLogin.Location = new System.Drawing.Point(0, 410);
            this.lblBackToLogin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBackToLogin.Name = "lblBackToLogin";
            this.lblBackToLogin.Size = new System.Drawing.Size(507, 25);
            this.lblBackToLogin.TabIndex = 6;
            this.lblBackToLogin.Text = "← Quay lại đăng nhập";
            this.lblBackToLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.pnlLeft.Controls.Add(this.label1);
            this.pnlLeft.Controls.Add(this.lblWelcome);
            this.pnlLeft.Controls.Add(this.lblSubTitle);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(4);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(467, 554);
            this.pnlLeft.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 56F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(162, 105);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 125);
            this.label1.TabIndex = 3;
            this.label1.Text = "⚽";
            // 
            // lblWelcome
            // 
            this.lblWelcome.BackColor = System.Drawing.Color.Transparent;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(117, 234);
            this.lblWelcome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(254, 120);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Khôi Phục Tài Khoản";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.lblSubTitle.Location = new System.Drawing.Point(40, 360);
            this.lblSubTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(387, 80);
            this.lblSubTitle.TabIndex = 2;
            this.lblSubTitle.Text = "Chúng tôi luôn sẵn sàng hỗ trợ bạn\r\ntruy cập lại tài khoản một cách\r\nan toàn và b" +
    "ảo mật";
            this.lblSubTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // QuenMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "QuenMatKhau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quên mật khẩu - Quản lý sân bóng";
            this.Load += new System.EventHandler(this.QuenMatKhau_Load_1);
            this.pnlRight.ResumeLayout(false);
            this.pnlForgotContainer.ResumeLayout(false);
            this.pnlForgotContainer.PerformLayout();
            this.pnlEmail.ResumeLayout(false);
            this.pnlEmail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmail)).EndInit();
            this.pnlVerificationCode.ResumeLayout(false);
            this.pnlCodeInput.ResumeLayout(false);
            this.pnlCodeInput.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlForgotContainer;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitleForgot;
        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.Panel pnlEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.PictureBox picEmail;
        private System.Windows.Forms.Button btnSendCode;
        private System.Windows.Forms.Panel pnlVerificationCode;
        private System.Windows.Forms.Label lblBackToLogin;
        private System.Windows.Forms.Label lblVerificationTitle;
        private System.Windows.Forms.Panel pnlCodeInput;
        private System.Windows.Forms.TextBox txtCode1;
        private System.Windows.Forms.TextBox txtCode2;
        private System.Windows.Forms.TextBox txtCode3;
        private System.Windows.Forms.TextBox txtCode4;
        private System.Windows.Forms.TextBox txtCode5;
        private System.Windows.Forms.TextBox txtCode6;
        private System.Windows.Forms.Label label1;
    }
}