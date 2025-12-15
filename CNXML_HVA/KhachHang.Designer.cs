using System;
using System.Drawing;
using System.Windows.Forms;

namespace CNXML_HVA
{
    partial class KhachHang
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblUserAvatar = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
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
            this.panelSearch = new System.Windows.Forms.Panel();
            this.labelSearch = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.labelRecordCount = new System.Windows.Forms.Label();
            this.dataGridViewCustomers = new System.Windows.Forms.DataGridView();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonExportExcel = new System.Windows.Forms.Button();
            this.buttonSqlToXml = new System.Windows.Forms.Button();
            this.buttonXmlToSql = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.groupBoxCustomerInfo.SuspendLayout();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomers)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.panelHeader.Controls.Add(this.lblUserAvatar);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1180, 70);
            this.panelHeader.TabIndex = 0;
            // 
            // lblUserAvatar
            // 
            this.lblUserAvatar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserAvatar.AutoSize = true;
            this.lblUserAvatar.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblUserAvatar.ForeColor = System.Drawing.Color.White;
            this.lblUserAvatar.Location = new System.Drawing.Point(1004, 29);
            this.lblUserAvatar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserAvatar.Name = "lblUserAvatar";
            this.lblUserAvatar.Size = new System.Drawing.Size(136, 25);
            this.lblUserAvatar.TabIndex = 2;
            this.lblUserAvatar.Text = "👤 Admin User";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(13, 13);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(408, 41);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "⚽ QUẢN LÝ KHÁCH HÀNG";
            // 
            // groupBoxCustomerInfo
            // 
            this.groupBoxCustomerInfo.BackColor = System.Drawing.Color.White;
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
            this.groupBoxCustomerInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBoxCustomerInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxCustomerInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.groupBoxCustomerInfo.Location = new System.Drawing.Point(20, 90);
            this.groupBoxCustomerInfo.Name = "groupBoxCustomerInfo";
            this.groupBoxCustomerInfo.Size = new System.Drawing.Size(1140, 260);
            this.groupBoxCustomerInfo.TabIndex = 1;
            this.groupBoxCustomerInfo.TabStop = false;
            this.groupBoxCustomerInfo.Text = "   Thông tin khách hàng   ";
            this.groupBoxCustomerInfo.Enter += new System.EventHandler(this.groupBoxCustomerInfo_Enter);
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.labelID.Location = new System.Drawing.Point(20, 35);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(57, 20);
            this.labelID.TabIndex = 0;
            this.labelID.Text = "Mã KH:";
            // 
            // textBoxId
            // 
            this.textBoxId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxId.Location = new System.Drawing.Point(140, 32);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(200, 27);
            this.textBoxId.TabIndex = 1;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.labelName.Location = new System.Drawing.Point(380, 35);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(76, 20);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "Họ và tên:";
            // 
            // textBoxName
            // 
            this.textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxName.Location = new System.Drawing.Point(500, 32);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(420, 27);
            this.textBoxName.TabIndex = 3;
            // 
            // labelPhone
            // 
            this.labelPhone.AutoSize = true;
            this.labelPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.labelPhone.Location = new System.Drawing.Point(20, 75);
            this.labelPhone.Name = "labelPhone";
            this.labelPhone.Size = new System.Drawing.Size(100, 20);
            this.labelPhone.TabIndex = 4;
            this.labelPhone.Text = "Số điện thoại:";
            // 
            // textBoxPhone
            // 
            this.textBoxPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxPhone.Location = new System.Drawing.Point(140, 72);
            this.textBoxPhone.Name = "textBoxPhone";
            this.textBoxPhone.Size = new System.Drawing.Size(200, 27);
            this.textBoxPhone.TabIndex = 5;
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.labelEmail.Location = new System.Drawing.Point(380, 75);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(49, 20);
            this.labelEmail.TabIndex = 6;
            this.labelEmail.Text = "Email:";
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxEmail.Location = new System.Drawing.Point(500, 72);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(420, 27);
            this.textBoxEmail.TabIndex = 7;
            // 
            // labelCity
            // 
            this.labelCity.AutoSize = true;
            this.labelCity.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelCity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.labelCity.Location = new System.Drawing.Point(20, 115);
            this.labelCity.Name = "labelCity";
            this.labelCity.Size = new System.Drawing.Size(82, 20);
            this.labelCity.TabIndex = 8;
            this.labelCity.Text = "Thành phố:";
            // 
            // textBoxCity
            // 
            this.textBoxCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCity.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxCity.Location = new System.Drawing.Point(140, 112);
            this.textBoxCity.Name = "textBoxCity";
            this.textBoxCity.Size = new System.Drawing.Size(200, 27);
            this.textBoxCity.TabIndex = 9;
            // 
            // labelDistrict
            // 
            this.labelDistrict.AutoSize = true;
            this.labelDistrict.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelDistrict.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.labelDistrict.Location = new System.Drawing.Point(380, 115);
            this.labelDistrict.Name = "labelDistrict";
            this.labelDistrict.Size = new System.Drawing.Size(95, 20);
            this.labelDistrict.TabIndex = 10;
            this.labelDistrict.Text = "Quận/Huyện:";
            // 
            // textBoxDistrict
            // 
            this.textBoxDistrict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDistrict.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxDistrict.Location = new System.Drawing.Point(500, 112);
            this.textBoxDistrict.Name = "textBoxDistrict";
            this.textBoxDistrict.Size = new System.Drawing.Size(200, 27);
            this.textBoxDistrict.TabIndex = 11;
            // 
            // labelStreet
            // 
            this.labelStreet.AutoSize = true;
            this.labelStreet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelStreet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.labelStreet.Location = new System.Drawing.Point(20, 155);
            this.labelStreet.Name = "labelStreet";
            this.labelStreet.Size = new System.Drawing.Size(58, 20);
            this.labelStreet.TabIndex = 12;
            this.labelStreet.Text = "Đường:";
            // 
            // textBoxStreet
            // 
            this.textBoxStreet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxStreet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxStreet.Location = new System.Drawing.Point(140, 152);
            this.textBoxStreet.Name = "textBoxStreet";
            this.textBoxStreet.Size = new System.Drawing.Size(780, 27);
            this.textBoxStreet.TabIndex = 13;
            // 
            // labelMembership
            // 
            this.labelMembership.AutoSize = true;
            this.labelMembership.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelMembership.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.labelMembership.Location = new System.Drawing.Point(20, 195);
            this.labelMembership.Name = "labelMembership";
            this.labelMembership.Size = new System.Drawing.Size(120, 20);
            this.labelMembership.TabIndex = 14;
            this.labelMembership.Text = "Hạng thành viên:";
            // 
            // comboBoxMembership
            // 
            this.comboBoxMembership.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMembership.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxMembership.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboBoxMembership.Items.AddRange(new object[] {
            "None",
            "Silver",
            "Gold",
            "VIP"});
            this.comboBoxMembership.Location = new System.Drawing.Point(140, 192);
            this.comboBoxMembership.Name = "comboBoxMembership";
            this.comboBoxMembership.Size = new System.Drawing.Size(200, 28);
            this.comboBoxMembership.TabIndex = 15;
            // 
            // labelNote
            // 
            this.labelNote.AutoSize = true;
            this.labelNote.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.labelNote.Location = new System.Drawing.Point(380, 195);
            this.labelNote.Name = "labelNote";
            this.labelNote.Size = new System.Drawing.Size(61, 20);
            this.labelNote.TabIndex = 16;
            this.labelNote.Text = "Ghi chú:";
            // 
            // textBoxNote
            // 
            this.textBoxNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxNote.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxNote.Location = new System.Drawing.Point(500, 192);
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(420, 27);
            this.textBoxNote.TabIndex = 17;
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.White;
            this.panelSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearch.Controls.Add(this.labelSearch);
            this.panelSearch.Controls.Add(this.textBoxSearch);
            this.panelSearch.Controls.Add(this.labelRecordCount);
            this.panelSearch.Location = new System.Drawing.Point(20, 360);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(1140, 45);
            this.panelSearch.TabIndex = 2;
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.labelSearch.Location = new System.Drawing.Point(10, 12);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(104, 20);
            this.labelSearch.TabIndex = 0;
            this.labelSearch.Text = "🔍 Tìm kiếm:";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxSearch.Location = new System.Drawing.Point(119, 9);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(450, 27);
            this.textBoxSearch.TabIndex = 1;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // labelRecordCount
            // 
            this.labelRecordCount.AutoSize = true;
            this.labelRecordCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.labelRecordCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.labelRecordCount.Location = new System.Drawing.Point(900, 12);
            this.labelRecordCount.Name = "labelRecordCount";
            this.labelRecordCount.Size = new System.Drawing.Size(152, 20);
            this.labelRecordCount.TabIndex = 2;
            this.labelRecordCount.Text = "Tổng số: 0 khách hàng";
            // 
            // dataGridViewCustomers
            // 
            this.dataGridViewCustomers.AllowUserToAddRows = false;
            this.dataGridViewCustomers.AllowUserToDeleteRows = false;
            this.dataGridViewCustomers.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewCustomers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewCustomers.ColumnHeadersHeight = 40;
            this.dataGridViewCustomers.EnableHeadersVisualStyles = false;
            this.dataGridViewCustomers.Location = new System.Drawing.Point(20, 415);
            this.dataGridViewCustomers.MultiSelect = false;
            this.dataGridViewCustomers.Name = "dataGridViewCustomers";
            this.dataGridViewCustomers.ReadOnly = true;
            this.dataGridViewCustomers.RowHeadersVisible = false;
            this.dataGridViewCustomers.RowHeadersWidth = 51;
            this.dataGridViewCustomers.RowTemplate.Height = 35;
            this.dataGridViewCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCustomers.Size = new System.Drawing.Size(1140, 220);
            this.dataGridViewCustomers.TabIndex = 3;
            this.dataGridViewCustomers.SelectionChanged += new System.EventHandler(this.dataGridViewCustomers_SelectionChanged);
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelButtons.Controls.Add(this.buttonAdd);
            this.panelButtons.Controls.Add(this.buttonEdit);
            this.panelButtons.Controls.Add(this.buttonDelete);
            this.panelButtons.Controls.Add(this.buttonSave);
            this.panelButtons.Controls.Add(this.buttonCancel);
            this.panelButtons.Controls.Add(this.buttonRefresh);
            this.panelButtons.Controls.Add(this.buttonExportExcel);
            this.panelButtons.Controls.Add(this.buttonSqlToXml);
            this.panelButtons.Controls.Add(this.buttonXmlToSql);
            this.panelButtons.Location = new System.Drawing.Point(20, 645);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1140, 60);
            this.panelButtons.TabIndex = 4;
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.buttonAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.Location = new System.Drawing.Point(10, 10);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(110, 40);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "➕ Thêm";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.buttonEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonEdit.FlatAppearance.BorderSize = 0;
            this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonEdit.ForeColor = System.Drawing.Color.White;
            this.buttonEdit.Location = new System.Drawing.Point(130, 10);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(110, 40);
            this.buttonEdit.TabIndex = 1;
            this.buttonEdit.Text = "✏️ Sửa";
            this.buttonEdit.UseVisualStyleBackColor = false;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.buttonDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonDelete.ForeColor = System.Drawing.Color.White;
            this.buttonDelete.Location = new System.Drawing.Point(250, 10);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(110, 40);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "🗑️ Xóa";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.buttonSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSave.FlatAppearance.BorderSize = 0;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(370, 10);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(110, 40);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "💾 Lưu";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.buttonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.ForeColor = System.Drawing.Color.White;
            this.buttonCancel.Location = new System.Drawing.Point(490, 10);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(110, 40);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "❌ Hủy";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.buttonRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRefresh.FlatAppearance.BorderSize = 0;
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonRefresh.ForeColor = System.Drawing.Color.White;
            this.buttonRefresh.Location = new System.Drawing.Point(610, 10);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(110, 40);
            this.buttonRefresh.TabIndex = 5;
            this.buttonRefresh.Text = "🔄 Làm mới";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonExportExcel
            // 
            this.buttonExportExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.buttonExportExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonExportExcel.FlatAppearance.BorderSize = 0;
            this.buttonExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExportExcel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonExportExcel.ForeColor = System.Drawing.Color.White;
            this.buttonExportExcel.Location = new System.Drawing.Point(730, 10);
            this.buttonExportExcel.Name = "buttonExportExcel";
            this.buttonExportExcel.Size = new System.Drawing.Size(114, 40);
            this.buttonExportExcel.TabIndex = 6;
            this.buttonExportExcel.Text = "📊 Xuất Excel";
            this.buttonExportExcel.UseVisualStyleBackColor = false;
            this.buttonExportExcel.Click += new System.EventHandler(this.buttonExportExcel_Click);
            // 
            // buttonSqlToXml
            // 
            this.buttonSqlToXml.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.buttonSqlToXml.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSqlToXml.FlatAppearance.BorderSize = 0;
            this.buttonSqlToXml.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSqlToXml.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSqlToXml.ForeColor = System.Drawing.Color.White;
            this.buttonSqlToXml.Location = new System.Drawing.Point(850, 10);
            this.buttonSqlToXml.Name = "buttonSqlToXml";
            this.buttonSqlToXml.Size = new System.Drawing.Size(130, 40);
            this.buttonSqlToXml.TabIndex = 7;
            this.buttonSqlToXml.Text = "⬇️ SQL -> XML";
            this.buttonSqlToXml.UseVisualStyleBackColor = false;
            this.buttonSqlToXml.Click += new System.EventHandler(this.buttonSqlToXml_Click);
            // 
            // buttonXmlToSql
            // 
            this.buttonXmlToSql.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.buttonXmlToSql.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonXmlToSql.FlatAppearance.BorderSize = 0;
            this.buttonXmlToSql.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonXmlToSql.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonXmlToSql.ForeColor = System.Drawing.Color.White;
            this.buttonXmlToSql.Location = new System.Drawing.Point(990, 10);
            this.buttonXmlToSql.Name = "buttonXmlToSql";
            this.buttonXmlToSql.Size = new System.Drawing.Size(130, 40);
            this.buttonXmlToSql.TabIndex = 8;
            this.buttonXmlToSql.Text = "⬆️ XML -> SQL";
            this.buttonXmlToSql.UseVisualStyleBackColor = false;
            this.buttonXmlToSql.Click += new System.EventHandler(this.buttonXmlToSql_Click);
            // 
            // KhachHang
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1180, 720);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.groupBoxCustomerInfo);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.dataGridViewCustomers);
            this.Controls.Add(this.panelButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "KhachHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý khách hàng - XML & SQL Integration";
            this.Load += new System.EventHandler(this.KhachHang_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.groupBoxCustomerInfo.ResumeLayout(false);
            this.groupBoxCustomerInfo.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomers)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        // Khai báo biến giữ nguyên và thêm 2 button mới
        private Panel panelHeader;
        private GroupBox groupBoxCustomerInfo;
        private Label labelID;
        private TextBox textBoxId;
        private Label labelName;
        private TextBox textBoxName;
        private Label labelPhone;
        private TextBox textBoxPhone;
        private Label labelEmail;
        private TextBox textBoxEmail;
        private Label labelCity;
        private TextBox textBoxCity;
        private Label labelDistrict;
        private TextBox textBoxDistrict;
        private Label labelStreet;
        private TextBox textBoxStreet;
        private Label labelMembership;
        private ComboBox comboBoxMembership;
        private Label labelNote;
        private TextBox textBoxNote;
        private Panel panelSearch;
        private DataGridView dataGridViewCustomers;
        private Label labelSearch;
        private TextBox textBoxSearch;
        private Label labelRecordCount;
        private Panel panelButtons;
        private Button buttonAdd;
        private Button buttonEdit;
        private Button buttonDelete;
        private Button buttonSave;
        private Button buttonCancel;
        private Button buttonRefresh;
        private Button buttonExportExcel;

        // Mới
        private Button buttonSqlToXml;
        private Button buttonXmlToSql;
        private Label lblTitle;
        private Label lblUserAvatar;
    }
}