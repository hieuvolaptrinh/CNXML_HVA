using System;

namespace CNXML_HVA
{
    partial class KhachHang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();

            this.groupBoxCustomerInfo = new System.Windows.Forms.GroupBox();
            this.labelID = new System.Windows.Forms.Label();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelPhone = new System.Windows.Forms.Label();
            this.textBoxPhone = new System.Windows.Forms.TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.labelCity = new System.Windows.Forms.Label();
            this.textBoxCity = new System.Windows.Forms.TextBox();
            this.labelDistrict = new System.Windows.Forms.Label();
            this.textBoxDistrict = new System.Windows.Forms.TextBox();
            this.labelStreet = new System.Windows.Forms.Label();
            this.textBoxStreet = new System.Windows.Forms.TextBox();
            this.labelMembership = new System.Windows.Forms.Label();
            this.comboBoxMembership = new System.Windows.Forms.ComboBox();
            this.labelNote = new System.Windows.Forms.Label();
            this.textBoxNote = new System.Windows.Forms.TextBox();

            this.dataGridViewCustomers = new System.Windows.Forms.DataGridView();
            this.labelSearch = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();

            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomers)).BeginInit();
            this.groupBoxCustomerInfo.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();

            // panelHeader
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Height = 60;
            this.panelHeader.Controls.Add(this.labelTitle);

            // labelTitle
            this.labelTitle.Text = "QUẢN LÝ KHÁCH HÀNG";
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(16, 15);

            // groupBoxCustomerInfo
            this.groupBoxCustomerInfo.Text = "Thông tin khách hàng";
            this.groupBoxCustomerInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxCustomerInfo.ForeColor = System.Drawing.Color.FromArgb(27, 94, 32);
            this.groupBoxCustomerInfo.Location = new System.Drawing.Point(20, 80);
            this.groupBoxCustomerInfo.Size = new System.Drawing.Size(760, 240);

            // labels & inputs inside groupBox
            // labelID & textBoxId
            this.labelID.Text = "Mã KH:";
            this.labelID.Location = new System.Drawing.Point(16, 30);
            this.labelID.AutoSize = true;
            this.textBoxId.Location = new System.Drawing.Point(120, 27);
            this.textBoxId.Size = new System.Drawing.Size(180, 21);

            // labelName & textBoxName
            this.labelName.Text = "Họ và tên:";
            this.labelName.Location = new System.Drawing.Point(320, 30);
            this.labelName.AutoSize = true;
            this.textBoxName.Location = new System.Drawing.Point(410, 27);
            this.textBoxName.Size = new System.Drawing.Size(320, 21);

            // labelPhone & textBoxPhone
            this.labelPhone.Text = "Số điện thoại:";
            this.labelPhone.Location = new System.Drawing.Point(16, 65);
            this.labelPhone.AutoSize = true;
            this.textBoxPhone.Location = new System.Drawing.Point(120, 62);
            this.textBoxPhone.Size = new System.Drawing.Size(180, 21);

            // labelEmail & textBoxEmail
            this.labelEmail.Text = "Email:";
            this.labelEmail.Location = new System.Drawing.Point(320, 65);
            this.labelEmail.AutoSize = true;
            this.textBoxEmail.Location = new System.Drawing.Point(410, 62);
            this.textBoxEmail.Size = new System.Drawing.Size(320, 21);

            // labelCity & textBoxCity
            this.labelCity.Text = "Thành phố:";
            this.labelCity.Location = new System.Drawing.Point(16, 100);
            this.labelCity.AutoSize = true;
            this.textBoxCity.Location = new System.Drawing.Point(120, 97);
            this.textBoxCity.Size = new System.Drawing.Size(180, 21);

            // labelDistrict & textBoxDistrict
            this.labelDistrict.Text = "Quận/Huyện:";
            this.labelDistrict.Location = new System.Drawing.Point(320, 100);
            this.labelDistrict.AutoSize = true;
            this.textBoxDistrict.Location = new System.Drawing.Point(410, 97);
            this.textBoxDistrict.Size = new System.Drawing.Size(180, 21);

            // labelStreet & textBoxStreet
            this.labelStreet.Text = "Đường:";
            this.labelStreet.Location = new System.Drawing.Point(16, 135);
            this.labelStreet.AutoSize = true;
            this.textBoxStreet.Location = new System.Drawing.Point(120, 132);
            this.textBoxStreet.Size = new System.Drawing.Size(610, 21);

            // labelMembership & comboBoxMembership
            this.labelMembership.Text = "Hạng thành viên:";
            this.labelMembership.Location = new System.Drawing.Point(16, 170);
            this.labelMembership.AutoSize = true;
            this.comboBoxMembership.Location = new System.Drawing.Point(120, 167);
            this.comboBoxMembership.Size = new System.Drawing.Size(180, 23);
            this.comboBoxMembership.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMembership.Items.AddRange(new object[] { "None", "Silver", "Gold", "VIP" });

            // labelNote & textBoxNote
            this.labelNote.Text = "Ghi chú:";
            this.labelNote.Location = new System.Drawing.Point(320, 170);
            this.labelNote.AutoSize = true;
            this.textBoxNote.Location = new System.Drawing.Point(410, 167);
            this.textBoxNote.Size = new System.Drawing.Size(320, 21);

            // add controls to groupbox
            this.groupBoxCustomerInfo.Controls.Add(this.labelID);
            this.groupBoxCustomerInfo.Controls.Add(this.textBoxId);
            this.groupBoxCustomerInfo.Controls.Add(this.labelName);
            this.groupBoxCustomerInfo.Controls.Add(this.textBoxName);
            this.groupBoxCustomerInfo.Controls.Add(this.labelPhone);
            this.groupBoxCustomerInfo.Controls.Add(this.textBoxPhone);
            this.groupBoxCustomerInfo.Controls.Add(this.labelEmail);
            this.groupBoxCustomerInfo.Controls.Add(this.textBoxEmail);
            this.groupBoxCustomerInfo.Controls.Add(this.labelCity);
            this.groupBoxCustomerInfo.Controls.Add(this.textBoxCity);
            this.groupBoxCustomerInfo.Controls.Add(this.labelDistrict);
            this.groupBoxCustomerInfo.Controls.Add(this.textBoxDistrict);
            this.groupBoxCustomerInfo.Controls.Add(this.labelStreet);
            this.groupBoxCustomerInfo.Controls.Add(this.textBoxStreet);
            this.groupBoxCustomerInfo.Controls.Add(this.labelMembership);
            this.groupBoxCustomerInfo.Controls.Add(this.comboBoxMembership);
            this.groupBoxCustomerInfo.Controls.Add(this.labelNote);
            this.groupBoxCustomerInfo.Controls.Add(this.textBoxNote);

            // dataGridViewCustomers
            this.dataGridViewCustomers.Location = new System.Drawing.Point(20, 350);
            this.dataGridViewCustomers.Size = new System.Drawing.Size(760, 180);
            this.dataGridViewCustomers.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewCustomers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewCustomers.ReadOnly = true;
            this.dataGridViewCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCustomers.MultiSelect = false;
            this.dataGridViewCustomers.RowHeadersVisible = false;
            this.dataGridViewCustomers.AllowUserToAddRows = false;
            this.dataGridViewCustomers.AllowUserToDeleteRows = false;
            this.dataGridViewCustomers.SelectionChanged += new System.EventHandler(this.dataGridViewCustomers_SelectionChanged);

            // labelSearch & textBoxSearch
            this.labelSearch.Text = "Tìm kiếm:";
            this.labelSearch.Location = new System.Drawing.Point(20, 320);
            this.labelSearch.AutoSize = true;
            this.textBoxSearch.Location = new System.Drawing.Point(90, 317);
            this.textBoxSearch.Size = new System.Drawing.Size(400, 21);
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);

            // Buttons
            this.buttonAdd.Text = "Thêm";
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Location = new System.Drawing.Point(20, 540);
            this.buttonAdd.Size = new System.Drawing.Size(90, 36);
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);

            this.buttonEdit.Text = "Sửa";
            this.buttonEdit.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this.buttonEdit.ForeColor = System.Drawing.Color.White;
            this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEdit.Location = new System.Drawing.Point(120, 540);
            this.buttonEdit.Size = new System.Drawing.Size(90, 36);
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);

            this.buttonDelete.Text = "Xóa";
            this.buttonDelete.BackColor = System.Drawing.Color.FromArgb(244, 67, 54);
            this.buttonDelete.ForeColor = System.Drawing.Color.White;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Location = new System.Drawing.Point(220, 540);
            this.buttonDelete.Size = new System.Drawing.Size(90, 36);
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);

            this.buttonSave.Text = "Lưu";
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Location = new System.Drawing.Point(320, 540);
            this.buttonSave.Size = new System.Drawing.Size(90, 36);
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);

            this.buttonCancel.Text = "Hủy";
            this.buttonCancel.BackColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.buttonCancel.ForeColor = System.Drawing.Color.White;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Location = new System.Drawing.Point(420, 540);
            this.buttonCancel.Size = new System.Drawing.Size(90, 36);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);

            this.buttonRefresh.Text = "Làm mới";
            this.buttonRefresh.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.buttonRefresh.ForeColor = System.Drawing.Color.White;
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Location = new System.Drawing.Point(520, 540);
            this.buttonRefresh.Size = new System.Drawing.Size(90, 36);
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);

            // Form settings
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.groupBoxCustomerInfo);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.dataGridViewCustomers);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonRefresh);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "KhachHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý khách hàng";
            this.Load += new System.EventHandler(this.KhachHang_Load);
            this.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomers)).EndInit();
            this.groupBoxCustomerInfo.ResumeLayout(false);
            this.groupBoxCustomerInfo.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void DataGridViewCustomers_SelectionChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;

        private System.Windows.Forms.GroupBox groupBoxCustomerInfo;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelPhone;
        private System.Windows.Forms.TextBox textBoxPhone;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.Label labelCity;
        private System.Windows.Forms.TextBox textBoxCity;
        private System.Windows.Forms.Label labelDistrict;
        private System.Windows.Forms.TextBox textBoxDistrict;
        private System.Windows.Forms.Label labelStreet;
        private System.Windows.Forms.TextBox textBoxStreet;
        private System.Windows.Forms.Label labelMembership;
        private System.Windows.Forms.ComboBox comboBoxMembership;
        private System.Windows.Forms.Label labelNote;
        private System.Windows.Forms.TextBox textBoxNote;

        private System.Windows.Forms.DataGridView dataGridViewCustomers;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.TextBox textBoxSearch;

        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonRefresh;
    }
}
