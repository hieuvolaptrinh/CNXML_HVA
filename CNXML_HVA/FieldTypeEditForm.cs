using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CNXML_HVA.Models;

namespace CNXML_HVA
{
    public partial class FieldTypeEditForm : Form
    {
        private FieldType originalFieldType;
        public FieldType EditedFieldType { get; private set; }

        // Controls
        private TextBox txtId, txtName, txtCode, txtDescription, txtSizeDisplay, txtSurfaceType;
        private NumericUpDown numCapacity, numDefaultPrice, numPlayersPerTeam;
        private ComboBox cmbStatus, cmbColor, cmbIcon;
        private Button btnSaveType, btnCancelType, btnPickColor;
        private Label lblValidation, lblNameError, lblCapacityError, lblPriceError;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FieldTypeEditForm
            // 
            this.ClientSize = new System.Drawing.Size(986, 566);
            this.Name = "FieldTypeEditForm";
            this.ResumeLayout(false);

        }

        private ColorDialog colorDialog;

        public FieldTypeEditForm(FieldType fieldType)
        {
            originalFieldType = fieldType;
            InitializeFormControls();
            LoadData();
        }

        private void InitializeFormControls()
        {
            this.Text = originalFieldType == null ? "Thêm Loại Sân Mới" : "Chỉnh Sửa Loại Sân";
            this.Size = new Size(650, 700);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(247, 251, 247);

            int y = 20;
            int labelX = 30;
            int controlX = 180;
            int controlWidth = 420;

            // Title
            var lblFormTitle = new Label();
            lblFormTitle.Text = originalFieldType == null ? "📋 THÊM LOẠI SÂN MỚI" : "📋 CHỈNH SỬA LOẠI SÂN";
            lblFormTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.FromArgb(30, 140, 58);
            lblFormTitle.Location = new Point(labelX, y);
            lblFormTitle.Size = new Size(580, 30);
            this.Controls.Add(lblFormTitle);
            y += 45;

            // ID
            AddLabel("Mã loại:", labelX, y, true);
            txtId = AddTextBox(controlX, y, controlWidth);
            txtId.ReadOnly = originalFieldType != null;
            y += 50;

            // Name
            AddLabel("Tên loại:", labelX, y, true);
            txtName = AddTextBox(controlX, y, controlWidth);
            y += 30;
            lblNameError = AddErrorLabel(controlX, y);
            y += 25;

            // Code
            AddLabel("Mã code:", labelX, y);
            txtCode = AddTextBox(controlX, y, controlWidth);
            y += 50;

            // Players Per Team
            AddLabel("Số cầu thủ/đội:", labelX, y);
            numPlayersPerTeam = new NumericUpDown();
            numPlayersPerTeam.Location = new Point(controlX, y);
            numPlayersPerTeam.Size = new Size(controlWidth, 25);
            numPlayersPerTeam.Maximum = 50;
            numPlayersPerTeam.Minimum = 1;
            numPlayersPerTeam.Font = new Font("Segoe UI", 10F);
            this.Controls.Add(numPlayersPerTeam);
            y += 50;

            // Capacity (Total)
            AddLabel("Sức chứa:", labelX, y, true);
            numCapacity = new NumericUpDown();
            numCapacity.Location = new Point(controlX, y);
            numCapacity.Size = new Size(controlWidth, 25);
            numCapacity.Maximum = 100;
            numCapacity.Minimum = 1;
            numCapacity.Font = new Font("Segoe UI", 10F);
            this.Controls.Add(numCapacity);
            y += 30;
            lblCapacityError = AddErrorLabel(controlX, y);
            y += 25;

            // Default Price
            AddLabel("Giá mặc định (VNĐ):", labelX, y, true);
            numDefaultPrice = new NumericUpDown();
            numDefaultPrice.Location = new Point(controlX, y);
            numDefaultPrice.Size = new Size(controlWidth, 25);
            numDefaultPrice.Maximum = 10000000;
            numDefaultPrice.Minimum = 0;
            numDefaultPrice.Increment = 10000;
            numDefaultPrice.Font = new Font("Segoe UI", 10F);
            numDefaultPrice.ThousandsSeparator = true;
            this.Controls.Add(numDefaultPrice);
            y += 30;
            lblPriceError = AddErrorLabel(controlX, y);
            y += 25;

            // Size Display
            AddLabel("Kích thước:", labelX, y);
            txtSizeDisplay = AddTextBox(controlX, y, controlWidth);
            //txtSizeDisplay.PlaceholderText = "Ví dụ: 40m x 60m";
            y += 50;

            // Surface Type
            AddLabel("Loại bề mặt:", labelX, y);
            txtSurfaceType = AddTextBox(controlX, y, controlWidth);
            //txtSurfaceType.PlaceholderText = "Ví dụ: Cỏ nhân tạo";
            y += 50;

            // Color Picker
            AddLabel("Màu badge:", labelX, y);
            var pnlColor = new Panel();
            pnlColor.Location = new Point(controlX, y);
            pnlColor.Size = new Size(320, 30);
            
            cmbColor = new ComboBox();
            cmbColor.Location = new Point(0, 0);
            cmbColor.Size = new Size(220, 25);
            cmbColor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbColor.Font = new Font("Segoe UI", 10F);
            cmbColor.Items.AddRange(new object[] { 
                "Xanh lá - #1E8C3A", 
                "Xanh dương - #007BFF", 
                "Cam - #FF8C00",
                "Đỏ - #DC3545",
                "Tím - #6F42C1"
            });
            pnlColor.Controls.Add(cmbColor);

            btnPickColor = new Button();
            btnPickColor.Text = "🎨 Chọn màu";
            btnPickColor.Location = new Point(230, 0);
            btnPickColor.Size = new Size(90, 27);
            btnPickColor.FlatStyle = FlatStyle.Flat;
            btnPickColor.Font = new Font("Segoe UI", 9F);
            btnPickColor.Click += btnPickColor_Click;
            pnlColor.Controls.Add(btnPickColor);

            this.Controls.Add(pnlColor);
            y += 50;

            // Icon
            AddLabel("Icon:", labelX, y);
            cmbIcon = new ComboBox();
            cmbIcon.Location = new Point(controlX, y);
            cmbIcon.Size = new Size(controlWidth, 25);
            cmbIcon.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbIcon.Font = new Font("Segoe UI", 10F);
            cmbIcon.Items.AddRange(new object[] { 
                "⚽ Bóng đá", 
                "🏟️ Sân", 
                "👥 Nhóm",
                "🎯 Mục tiêu",
                "⭐ Ngôi sao",
                "🏆 Cúp"
            });
            this.Controls.Add(cmbIcon);
            y += 50;

            // Status
            AddLabel("Trạng thái:", labelX, y);
            cmbStatus = new ComboBox();
            cmbStatus.Location = new Point(controlX, y);
            cmbStatus.Size = new Size(controlWidth, 25);
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Font = new Font("Segoe UI", 10F);
            cmbStatus.Items.AddRange(new object[] { "Active", "Inactive" });
            this.Controls.Add(cmbStatus);
            y += 50;

            // Description
            AddLabel("Mô tả:", labelX, y);
            txtDescription = new TextBox();
            txtDescription.Location = new Point(controlX, y);
            txtDescription.Size = new Size(controlWidth, 70);
            txtDescription.Multiline = true;
            txtDescription.Font = new Font("Segoe UI", 10F);
            txtDescription.ScrollBars = ScrollBars.Vertical;
            this.Controls.Add(txtDescription);
            y += 80;

            // Validation Label
            lblValidation = new Label();
            lblValidation.Location = new Point(labelX, y);
            lblValidation.Size = new Size(580, 20);
            lblValidation.ForeColor = Color.Red;
            lblValidation.Font = new Font("Segoe UI", 9F);
            lblValidation.Visible = false;
            this.Controls.Add(lblValidation);
            y += 30;

            // Buttons
            btnSaveType = new Button();
            btnSaveType.Text = "💾 Lưu";
            btnSaveType.Location = new Point(240, y);
            btnSaveType.Size = new Size(130, 40);
            btnSaveType.BackColor = Color.FromArgb(30, 140, 58);
            btnSaveType.ForeColor = Color.White;
            btnSaveType.FlatStyle = FlatStyle.Flat;
            btnSaveType.FlatAppearance.BorderSize = 0;
            btnSaveType.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSaveType.Cursor = Cursors.Hand;
            btnSaveType.Click += btnSaveType_Click;
            this.Controls.Add(btnSaveType);

            btnCancelType = new Button();
            btnCancelType.Text = "❌ Hủy";
            btnCancelType.Location = new Point(380, y);
            btnCancelType.Size = new Size(130, 40);
            btnCancelType.BackColor = Color.White;
            btnCancelType.ForeColor = Color.FromArgb(43, 43, 43);
            btnCancelType.FlatStyle = FlatStyle.Flat;
            btnCancelType.FlatAppearance.BorderColor = Color.FromArgb(30, 140, 58);
            btnCancelType.Font = new Font("Segoe UI", 10F);
            btnCancelType.Cursor = Cursors.Hand;
            btnCancelType.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            this.Controls.Add(btnCancelType);

            // Color Dialog
            colorDialog = new ColorDialog();
        }

        private Label AddLabel(string text, int x, int y, bool required = false)
        {
            var label = new Label();
            label.Text = text + (required ? " *" : "");
            label.Location = new Point(x, y + 3);
            label.Size = new Size(140, 20);
            label.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label.ForeColor = Color.FromArgb(43, 43, 43);
            this.Controls.Add(label);
            return label;
        }

        private TextBox AddTextBox(int x, int y, int width)
        {
            var textBox = new TextBox();
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(width, 25);
            textBox.Font = new Font("Segoe UI", 10F);
            this.Controls.Add(textBox);
            return textBox;
        }

        private Label AddErrorLabel(int x, int y)
        {
            var label = new Label();
            label.Location = new Point(x, y);
            label.Size = new Size(420, 18);
            label.ForeColor = Color.Red;
            label.Font = new Font("Segoe UI", 8F);
            label.Visible = false;
            this.Controls.Add(label);
            return label;
        }

        private void LoadData()
        {
            if (originalFieldType != null)
            {
                // Edit mode
                txtId.Text = originalFieldType.Id;
                txtName.Text = originalFieldType.Name;
                txtCode.Text = originalFieldType.Code;
                numPlayersPerTeam.Value = originalFieldType.PlayersPerTeam;
                numCapacity.Value = originalFieldType.TotalCapacity;
                numDefaultPrice.Value = originalFieldType.BasePrice;
                txtSizeDisplay.Text = originalFieldType.SizeDisplay;
                txtSurfaceType.Text = originalFieldType.SurfaceType;
                txtDescription.Text = originalFieldType.Description;
                cmbStatus.SelectedItem = originalFieldType.Status;

                // Default selections
                if (cmbColor.Items.Count > 0) cmbColor.SelectedIndex = 0;
                if (cmbIcon.Items.Count > 0) cmbIcon.SelectedIndex = 0;
            }
            else
            {
                // Add mode - generate ID
                txtId.Text = GenerateNewId();
                cmbStatus.SelectedIndex = 0;
                numPlayersPerTeam.Value = 5;
                numCapacity.Value = 10;
                numDefaultPrice.Value = 100000;
                txtSurfaceType.Text = "Cỏ nhân tạo";
                
                if (cmbColor.Items.Count > 0) cmbColor.SelectedIndex = 0;
                if (cmbIcon.Items.Count > 0) cmbIcon.SelectedIndex = 0;
            }
        }

        private string GenerateNewId()
        {
            return "FT" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private void btnPickColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                var color = colorDialog.Color;
                var hexColor = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                cmbColor.Items.Add($"Tùy chỉnh - {hexColor}");
                cmbColor.SelectedIndex = cmbColor.Items.Count - 1;
            }
        }

        private void btnSaveType_Click(object sender, EventArgs e)
        {
            // TODO: validate and close dialog (no persistence code)
            if (!ValidateInput())
                return;

            EditedFieldType = new FieldType
            {
                Id = txtId.Text.Trim(),
                Name = txtName.Text.Trim(),
                Code = txtCode.Text.Trim(),
                PlayersPerTeam = (int)numPlayersPerTeam.Value,
                TotalCapacity = (int)numCapacity.Value,
                BasePrice = numDefaultPrice.Value,
                SizeDisplay = txtSizeDisplay.Text.Trim(),
                SurfaceType = txtSurfaceType.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                Status = cmbStatus.SelectedItem?.ToString() ?? "Active"
            };

            this.DialogResult = DialogResult.OK;
        }

        private bool ValidateInput()
        {
            // Reset error labels
            lblNameError.Visible = false;
            lblCapacityError.Visible = false;
            lblPriceError.Visible = false;
            lblValidation.Visible = false;

            bool isValid = true;

            // Validate Name
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowFieldError(lblNameError, "⚠️ Vui lòng nhập tên loại sân!");
                isValid = false;
            }

            // Validate Capacity
            if (numCapacity.Value <= 0)
            {
                ShowFieldError(lblCapacityError, "⚠️ Sức chứa phải lớn hơn 0!");
                isValid = false;
            }

            // Validate Price
            if (numDefaultPrice.Value <= 0)
            {
                ShowFieldError(lblPriceError, "⚠️ Giá mặc định phải lớn hơn 0!");
                isValid = false;
            }

            // General validation message
            if (!isValid)
            {
                ShowValidation("⚠️ Vui lòng kiểm tra và điền đầy đủ các thông tin bắt buộc!");
            }

            return isValid;
        }

        private void ShowFieldError(Label errorLabel, string message)
        {
            errorLabel.Text = message;
            errorLabel.Visible = true;
        }

        private void ShowValidation(string message)
        {
            lblValidation.Text = message;
            lblValidation.Visible = true;
        }
    }
}
