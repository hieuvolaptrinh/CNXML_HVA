using System;

namespace CNXML_HVA
{
    partial class DatLich
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

            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.labelID = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.labelCustomer = new System.Windows.Forms.Label();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.labelField = new System.Windows.Forms.Label();
            this.cboField = new System.Windows.Forms.ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.labelDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.labelTime = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.labelDuration = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.labelNote = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();

            this.labelSearch = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();

            this.dgvBookings = new System.Windows.Forms.DataGridView();

            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnLoadXML = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).BeginInit();
            this.groupBoxInfo.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();

            // panelHeader
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Height = 60;
            this.panelHeader.Controls.Add(this.labelTitle);

            // labelTitle
            this.labelTitle.Text = "ĐẶT LỊCH THUÊ SÂN";
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(16, 15);

            // groupBoxInfo
            this.groupBoxInfo.Text = "Thông tin đặt lịch";
            this.groupBoxInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxInfo.ForeColor = System.Drawing.Color.FromArgb(27, 94, 32);
            this.groupBoxInfo.Location = new System.Drawing.Point(20, 70);
            this.groupBoxInfo.Size = new System.Drawing.Size(760, 260);

            // Labels & inputs inside groupBoxInfo
            // labelID & txtID
            this.labelID.Text = "Mã đặt:";
            this.labelID.AutoSize = true;
            this.labelID.Location = new System.Drawing.Point(20, 30);
            this.txtID.Location = new System.Drawing.Point(140, 27);
            this.txtID.Size = new System.Drawing.Size(180, 25);

            // labelCustomer & cboCustomer
            this.labelCustomer.Text = "Khách hàng:";
            this.labelCustomer.AutoSize = true;
            this.labelCustomer.Location = new System.Drawing.Point(360, 30);
            this.cboCustomer.Location = new System.Drawing.Point(460, 27);
            this.cboCustomer.Size = new System.Drawing.Size(280, 25);
            this.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // labelField & cboField
            this.labelField.Text = "Sân:";
            this.labelField.AutoSize = true;
            this.labelField.Location = new System.Drawing.Point(20, 70);
            this.cboField.Location = new System.Drawing.Point(140, 67);
            this.cboField.Size = new System.Drawing.Size(220, 25);
            this.cboField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // labelType & cboType
            this.labelType.Text = "Loại sân:";
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(380, 70);
            this.cboType.Location = new System.Drawing.Point(460, 67);
            this.cboType.Size = new System.Drawing.Size(150, 25);
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // labelDate & dtpDate
            this.labelDate.Text = "Ngày đặt:";
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(20, 110);
            this.dtpDate.Location = new System.Drawing.Point(140, 107);
            this.dtpDate.Size = new System.Drawing.Size(180, 25);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            // labelTime & txtTime
            this.labelTime.Text = "Giờ bắt đầu:";
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(360, 110);
            this.txtTime.Location = new System.Drawing.Point(460, 107);
            this.txtTime.Size = new System.Drawing.Size(150, 25);
           

            // labelDuration & txtDuration
            this.labelDuration.Text = "Thời lượng (giờ):";
            this.labelDuration.AutoSize = true;
            this.labelDuration.Location = new System.Drawing.Point(20, 150);
            this.txtDuration.Location = new System.Drawing.Point(140, 147);
            this.txtDuration.Size = new System.Drawing.Size(80, 25);

            // labelNote & txtNote
            this.labelNote.Text = "Ghi chú:";
            this.labelNote.AutoSize = true;
            this.labelNote.Location = new System.Drawing.Point(20, 190);
            this.txtNote.Location = new System.Drawing.Point(140, 187);
            this.txtNote.Size = new System.Drawing.Size(600, 25);

            // Add controls to groupBoxInfo
            this.groupBoxInfo.Controls.Add(this.labelID);
            this.groupBoxInfo.Controls.Add(this.txtID);
            this.groupBoxInfo.Controls.Add(this.labelCustomer);
            this.groupBoxInfo.Controls.Add(this.cboCustomer);
            this.groupBoxInfo.Controls.Add(this.labelField);
            this.groupBoxInfo.Controls.Add(this.cboField);
            this.groupBoxInfo.Controls.Add(this.labelType);
            this.groupBoxInfo.Controls.Add(this.cboType);
            this.groupBoxInfo.Controls.Add(this.labelDate);
            this.groupBoxInfo.Controls.Add(this.dtpDate);
            this.groupBoxInfo.Controls.Add(this.labelTime);
            this.groupBoxInfo.Controls.Add(this.txtTime);
            this.groupBoxInfo.Controls.Add(this.labelDuration);
            this.groupBoxInfo.Controls.Add(this.txtDuration);
            this.groupBoxInfo.Controls.Add(this.labelNote);
            this.groupBoxInfo.Controls.Add(this.txtNote);

            // labelSearch & textBoxSearch
            this.labelSearch.Text = "Tìm kiếm:";
            this.labelSearch.AutoSize = true;
            this.labelSearch.Location = new System.Drawing.Point(20, 340);
            this.textBoxSearch.Location = new System.Drawing.Point(90, 337);
            this.textBoxSearch.Size = new System.Drawing.Size(400, 23);
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);

            // dgvBookings
            this.dgvBookings.Location = new System.Drawing.Point(20, 370);
            this.dgvBookings.Size = new System.Drawing.Size(760, 150);
            this.dgvBookings.BackgroundColor = System.Drawing.Color.White;
            this.dgvBookings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvBookings.RowHeadersVisible = false;
            this.dgvBookings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBookings.MultiSelect = false;
            this.dgvBookings.AllowUserToAddRows = false;
            this.dgvBookings.AllowUserToDeleteRows = false;
            this.dgvBookings.SelectionChanged += new System.EventHandler(this.dgvBookings_SelectionChanged);

            // Buttons (Add/Edit/Delete/Save/Cancel/Refresh/LoadXML)
            this.btnAdd.Text = "Thêm";
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(20, 540);
            this.btnAdd.Size = new System.Drawing.Size(100, 36);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnEdit.Text = "Sửa";
            this.btnEdit.BackColor = System.Drawing.Color.Khaki;
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Location = new System.Drawing.Point(140, 540);
            this.btnEdit.Size = new System.Drawing.Size(100, 36);
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            this.btnDelete.Text = "Xóa";
            this.btnDelete.BackColor = System.Drawing.Color.LightCoral;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(260, 540);
            this.btnDelete.Size = new System.Drawing.Size(100, 36);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnSave.Text = "Lưu";
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(380, 540);
            this.btnSave.Size = new System.Drawing.Size(100, 36);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Text = "Hủy";
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(500, 540);
            this.btnCancel.Size = new System.Drawing.Size(100, 36);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(620, 540);
            this.btnRefresh.Size = new System.Drawing.Size(100, 36);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.btnLoadXML.Text = "Tải từ XML";
            this.btnLoadXML.BackColor = System.Drawing.Color.Plum;
            this.btnLoadXML.ForeColor = System.Drawing.Color.White;
            this.btnLoadXML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadXML.Location = new System.Drawing.Point(740, 540);
            this.btnLoadXML.Size = new System.Drawing.Size(100, 36);
            this.btnLoadXML.Click += new System.EventHandler(this.btnLoadXML_Click);

            // Form settings
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.dgvBookings);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnLoadXML);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DatLich";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đặt lịch thuê sân";
            this.Load += new System.EventHandler(this.DatLich_Load);
            this.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);

            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).EndInit();
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DgvBookings_SelectionChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;

        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label labelCustomer;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.Label labelField;
        private System.Windows.Forms.ComboBox cboField;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Label labelDuration;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Label labelNote;
        private System.Windows.Forms.TextBox txtNote;

        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.TextBox textBoxSearch;

        private System.Windows.Forms.DataGridView dgvBookings;

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnLoadXML;
    }
}
