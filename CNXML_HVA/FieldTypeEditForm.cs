using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using CNXML_HVA.Models;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNXML_HVA
{
    public partial class FieldTypeEditForm : Form
    {
        private FieldType originalFieldType;
        public FieldType EditedFieldType { get; private set; }

        // Controls
        private TextBox txtId, txtName, txtCode, txtDescription, txtFeatures;
        private NumericUpDown numLength, numWidth, numGoalHeight, numGoalWidth;
        private NumericUpDown numCapacity, numDefaultPrice, numPlayersPerTeam;
        private NumericUpDown numPeakMultiplier, numWeekendMultiplier;
        private NumericUpDown numMinBookingHours, numMaxBookingHours;
        private ComboBox cmbStatus, cmbSurfaceType;
        private Button btnSaveType, btnCancelType;
        private Label lblValidation, lblNameError, lblDimensionError, lblPriceError;

        public FieldTypeEditForm(FieldType fieldType)
        {
            originalFieldType = fieldType;
            InitializeComponent();
            InitializeFormControls();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(700, 780);
            this.Name = "FieldTypeEditForm";
            this.ResumeLayout(false);
        }

        private void InitializeFormControls()
        {
            this.Text = originalFieldType == null ? "Thêm Loại Sân Mới" : "Chỉnh Sửa Loại Sân";
            this.Size = new Size(700, 780);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(247, 251, 247);
            this.AutoScroll = true;

            int y = 20;
            int labelX = 30;
            int controlX = 200;
            int controlWidth = 450;
            int halfWidth = 215;

            // Title
            var lblFormTitle = new Label();
            lblFormTitle.Text = originalFieldType == null ? "📋 THÊM LOẠI SÂN MỚI" : "📋 CHỈNH SỬA LOẠI SÂN";
            lblFormTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.FromArgb(30, 140, 58);
            lblFormTitle.Location = new Point(labelX, y);
            lblFormTitle.Size = new Size(620, 30);
            this.Controls.Add(lblFormTitle);
            y += 45;

            // ========== THÔNG TIN CƠ BẢN ==========
            AddSectionHeader("THÔNG TIN CƠ BẢN", labelX, y);
            y += 35;

            // ID
            AddLabel("Mã loại:", labelX, y, true);
            txtId = AddTextBox(controlX, y, controlWidth);
            txtId.ReadOnly = originalFieldType != null;
            txtId.BackColor = originalFieldType != null ? Color.FromArgb(240, 240, 240) : Color.White;
            y += 45;

            // Name
            AddLabel("Tên loại:", labelX, y, true);
            txtName = AddTextBox(controlX, y, controlWidth);
            y += 30;
            lblNameError = AddErrorLabel(controlX, y);
            y += 25;

            // Code
            AddLabel("Mã code:", labelX, y);
            txtCode = AddTextBox(controlX, y, controlWidth);
            //txtCode.PlaceholderText = "VD: S5, S7, S11";
            y += 50;

            // ========== KÍCH THƯỚC SÂN ==========
            AddSectionHeader("KÍCH THƯỚC SÂN", labelX, y);
            y += 35;

            // Length & Width
            AddLabel("Chiều dài (m):", labelX, y, true);
            numLength = CreateNumericUpDown(controlX, y, halfWidth, 0, 200, 1);
            AddLabel("Chiều rộng (m):", controlX + halfWidth + 20, y, true);
            numWidth = CreateNumericUpDown(controlX + halfWidth + 20 + 150, y, halfWidth - 150, 0, 200, 1);
            y += 30;
            lblDimensionError = AddErrorLabel(controlX, y);
            y += 25;

            // Goal Size
            AddLabel("Khung thành - Cao (m):", labelX, y);
            numGoalHeight = CreateNumericUpDown(controlX, y, halfWidth, 0, 10, 0.1m);
            AddLabel("Rộng (m):", controlX + halfWidth + 20, y);
            numGoalWidth = CreateNumericUpDown(controlX + halfWidth + 20 + 100, y, halfWidth - 100, 0, 20, 0.1m);
            y += 50;

            // ========== THÔNG TIN CẦU THỦ ==========
            AddSectionHeader("THÔNG TIN CẦU THỦ", labelX, y);
            y += 35;

            // Players Per Team
            AddLabel("Số cầu thủ/đội:", labelX, y, true);
            numPlayersPerTeam = CreateNumericUpDown(controlX, y, halfWidth, 1, 50, 1);

            // Total Capacity
            AddLabel("Tổng sức chứa:", controlX + halfWidth + 20, y, true);
            numCapacity = CreateNumericUpDown(controlX + halfWidth + 20 + 150, y, halfWidth - 150, 1, 100, 1);
            y += 50;

            // ========== LOẠI BỀ MẶT ==========
            AddSectionHeader("LOẠI BỀ MẶT", labelX, y);
            y += 35;

            AddLabel("Loại bề mặt:", labelX, y, true);
            cmbSurfaceType = new ComboBox();
            cmbSurfaceType.Location = new Point(controlX, y);
            cmbSurfaceType.Size = new Size(controlWidth, 25);
            cmbSurfaceType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSurfaceType.Font = new Font("Segoe UI", 10F);
            cmbSurfaceType.Items.AddRange(new object[] {
                "Cỏ nhân tạo",
                "Cỏ tự nhiên",
                "Sân xi măng",
                "Sân đất"
            });
            this.Controls.Add(cmbSurfaceType);
            y += 50;

            // ========== GIÁ VÀ HỆ SỐ ==========
            AddSectionHeader("GIÁ VÀ HỆ SỐ NHÂN", labelX, y);
            y += 35;

            // Base Price
            AddLabel("Giá cơ bản (VNĐ):", labelX, y, true);
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

            // Peak Hour Multiplier
            AddLabel("Hệ số giờ cao điểm:", labelX, y);
            numPeakMultiplier = CreateNumericUpDown(controlX, y, halfWidth, 1, 3, 0.1m);
            numPeakMultiplier.Value = 1.5m;

            // Weekend Multiplier
            AddLabel("Hệ số cuối tuần:", controlX + halfWidth + 20, y);
            numWeekendMultiplier = CreateNumericUpDown(controlX + halfWidth + 20 + 150, y, halfWidth - 150, 1, 3, 0.1m);
            numWeekendMultiplier.Value = 1.3m;
            y += 50;

            // ========== THỜI GIAN ĐẶT SÂN ==========
            AddSectionHeader("THỜI GIAN ĐẶT SÂN", labelX, y);
            y += 35;

            // Min/Max Booking Hours
            AddLabel("Số giờ tối thiểu:", labelX, y);
            numMinBookingHours = CreateNumericUpDown(controlX, y, halfWidth, 1, 24, 1);
            numMinBookingHours.Value = 1;

            AddLabel("Số giờ tối đa:", controlX + halfWidth + 20, y);
            numMaxBookingHours = CreateNumericUpDown(controlX + halfWidth + 20 + 150, y, halfWidth - 150, 1, 24, 1);
            numMaxBookingHours.Value = 4;
            y += 50;

            // ========== MÔ TẢ & TÍNH NĂNG ==========
            AddSectionHeader("MÔ TẢ & TÍNH NĂNG", labelX, y);
            y += 35;

            // Description
            AddLabel("Mô tả:", labelX, y);
            txtDescription = new TextBox();
            txtDescription.Location = new Point(controlX, y);
            txtDescription.Size = new Size(controlWidth, 60);
            txtDescription.Multiline = true;
            txtDescription.Font = new Font("Segoe UI", 10F);
            txtDescription.ScrollBars = ScrollBars.Vertical;
            this.Controls.Add(txtDescription);
            y += 70;

            // Features
            AddLabel("Tính năng:", labelX, y);
            txtFeatures = new TextBox();
            txtFeatures.Location = new Point(controlX, y);
            txtFeatures.Size = new Size(controlWidth, 60);
            txtFeatures.Multiline = true;
            txtFeatures.Font = new Font("Segoe UI", 10F);
            txtFeatures.ScrollBars = ScrollBars.Vertical;
            //txtFeatures.PlaceholderText = "VD: Mái che, Đèn chiếu sáng, Phòng thay đồ";
            this.Controls.Add(txtFeatures);
            y += 70;

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

            // Validation Label
            lblValidation = new Label();
            lblValidation.Location = new Point(labelX, y);
            lblValidation.Size = new Size(620, 20);
            lblValidation.ForeColor = Color.Red;
            lblValidation.Font = new Font("Segoe UI", 9F);
            lblValidation.Visible = false;
            this.Controls.Add(lblValidation);
            y += 30;

            // Buttons
            btnSaveType = new Button();
            btnSaveType.Text = "💾 Lưu";
            btnSaveType.Location = new Point(280, y);
            btnSaveType.Size = new Size(150, 42);
            btnSaveType.BackColor = Color.FromArgb(30, 140, 58);
            btnSaveType.ForeColor = Color.White;
            btnSaveType.FlatStyle = FlatStyle.Flat;
            btnSaveType.FlatAppearance.BorderSize = 0;
            btnSaveType.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSaveType.Cursor = Cursors.Hand;
            btnSaveType.Click += btnSaveType_Click;
            this.Controls.Add(btnSaveType);

            btnCancelType = new Button();
            btnCancelType.Text = "❌ Hủy";
            btnCancelType.Location = new Point(445, y);
            btnCancelType.Size = new Size(150, 42);
            btnCancelType.BackColor = Color.White;
            btnCancelType.ForeColor = Color.FromArgb(43, 43, 43);
            btnCancelType.FlatStyle = FlatStyle.Flat;
            btnCancelType.FlatAppearance.BorderColor = Color.FromArgb(30, 140, 58);
            btnCancelType.Font = new Font("Segoe UI", 11F);
            btnCancelType.Cursor = Cursors.Hand;
            btnCancelType.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            this.Controls.Add(btnCancelType);
        }

        private void AddSectionHeader(string text, int x, int y)
        {
            var panel = new Panel();
            panel.Location = new Point(x, y);
            panel.Size = new Size(620, 25);
            panel.BackColor = Color.FromArgb(223, 247, 230);

            var label = new Label();
            label.Text = text;
            label.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label.ForeColor = Color.FromArgb(30, 140, 58);
            label.Location = new Point(10, 3);
            label.AutoSize = true;
            panel.Controls.Add(label);

            this.Controls.Add(panel);
        }

        private NumericUpDown CreateNumericUpDown(int x, int y, int width, decimal min, decimal max, decimal increment)
        {
            var num = new NumericUpDown();
            num.Location = new Point(x, y);
            num.Size = new Size(width, 25);
            num.Minimum = min;
            num.Maximum = max;
            num.Increment = increment;
            num.DecimalPlaces = increment < 1 ? 2 : 0;
            num.Font = new Font("Segoe UI", 10F);
            this.Controls.Add(num);
            return num;
        }

        private Label AddLabel(string text, int x, int y, bool required = false)
        {
            var label = new Label();
            label.Text = text + (required ? " *" : "");
            label.Location = new Point(x, y + 3);
            label.Size = new Size(160, 20);
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
            label.Size = new Size(450, 18);
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

                numLength.Value = originalFieldType.Length;
                numWidth.Value = originalFieldType.Width;
                numGoalHeight.Value = originalFieldType.GoalHeight;
                numGoalWidth.Value = originalFieldType.GoalWidth;

                numPlayersPerTeam.Value = originalFieldType.PlayersPerTeam;
                numCapacity.Value = originalFieldType.TotalCapacity;

                cmbSurfaceType.SelectedItem = originalFieldType.SurfaceType;
                if (cmbSurfaceType.SelectedIndex == -1 && !string.IsNullOrEmpty(originalFieldType.SurfaceType))
                {
                    cmbSurfaceType.Items.Add(originalFieldType.SurfaceType);
                    cmbSurfaceType.SelectedItem = originalFieldType.SurfaceType;
                }

                numDefaultPrice.Value = originalFieldType.BasePrice;
                numPeakMultiplier.Value = originalFieldType.PeakHourMultiplier;
                numWeekendMultiplier.Value = originalFieldType.WeekendMultiplier;

                numMinBookingHours.Value = originalFieldType.MinimumBookingHours;
                numMaxBookingHours.Value = originalFieldType.MaximumBookingHours;

                txtDescription.Text = originalFieldType.Description;
                txtFeatures.Text = originalFieldType.Features;
                cmbStatus.SelectedItem = originalFieldType.Status;
            }
            else
            {
                // Add mode - generate ID
                txtId.Text = GenerateNewId();
                cmbStatus.SelectedIndex = 0;
                cmbSurfaceType.SelectedIndex = 0;

                numPlayersPerTeam.Value = 5;
                numCapacity.Value = 10;
                numLength.Value = 40;
                numWidth.Value = 20;
                numGoalHeight.Value = 2;
                numGoalWidth.Value = 3;
                numDefaultPrice.Value = 100000;
                numPeakMultiplier.Value = 1.5m;
                numWeekendMultiplier.Value = 1.3m;
                numMinBookingHours.Value = 1;
                numMaxBookingHours.Value = 4;
            }
        }

        private string GenerateNewId()
        {
            return "FT" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private void btnSaveType_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            EditedFieldType = new FieldType
            {
                Id = txtId.Text.Trim(),
                Name = txtName.Text.Trim(),
                Code = txtCode.Text.Trim(),

                Length = numLength.Value,
                Width = numWidth.Value,
                DimensionUnit = "mét",
                SizeDisplay = $"{numWidth.Value}m x {numLength.Value}m",

                GoalHeight = numGoalHeight.Value,
                GoalWidth = numGoalWidth.Value,
                GoalUnit = "mét",

                PlayersPerTeam = (int)numPlayersPerTeam.Value,
                TotalCapacity = (int)numCapacity.Value,

                SurfaceType = cmbSurfaceType.SelectedItem?.ToString() ?? "Cỏ nhân tạo",
                BasePrice = numDefaultPrice.Value,
                PeakHourMultiplier = numPeakMultiplier.Value,
                WeekendMultiplier = numWeekendMultiplier.Value,

                MinimumBookingHours = (int)numMinBookingHours.Value,
                MaximumBookingHours = (int)numMaxBookingHours.Value,

                Description = txtDescription.Text.Trim(),
                Features = txtFeatures.Text.Trim(),
                Status = cmbStatus.SelectedItem?.ToString() ?? "Active"
            };

            this.DialogResult = DialogResult.OK;
        }

        private bool ValidateInput()
        {
            // Reset error labels
            lblNameError.Visible = false;
            lblDimensionError.Visible = false;
            lblPriceError.Visible = false;
            lblValidation.Visible = false;

            bool isValid = true;

            // Validate Name
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowFieldError(lblNameError, "⚠️ Vui lòng nhập tên loại sân!");
                isValid = false;
            }

            // Validate Dimensions
            if (numLength.Value <= 0 || numWidth.Value <= 0)
            {
                ShowFieldError(lblDimensionError, "⚠️ Chiều dài và chiều rộng phải lớn hơn 0!");
                isValid = false;
            }

            // Validate Price
            if (numDefaultPrice.Value <= 0)
            {
                ShowFieldError(lblPriceError, "⚠️ Giá cơ bản phải lớn hơn 0!");
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