using System;
using System.Drawing;

namespace CNXML_HVAusing System.Linq;

{using System.Text;

    public partial class FieldTypeEditForm : Formusing System.Threading.Tasks;

    {using System.Windows.Forms;

        private FieldType originalFieldType;using CNXML_HVA.Models;

        public FieldType EditedFieldType { get; private set; }

namespace CNXML_HVA

        // Controls{

        private TextBox txtId, txtName, txtCode, txtDescription, txtFeatures;    public partial class FieldTypeEditForm : Form

        private NumericUpDown numLength, numWidth, numGoalHeight, numGoalWidth;    {

        private NumericUpDown numCapacity, numDefaultPrice, numPlayersPerTeam;        private FieldType originalFieldType;

        private NumericUpDown numPeakMultiplier, numWeekendMultiplier;        public FieldType EditedFieldType { get; private set; }

        private NumericUpDown numMinBookingHours, numMaxBookingHours;

        private ComboBox cmbStatus, cmbSurfaceType;        // Controls

        private Button btnSaveType, btnCancelType;        private TextBox txtId, txtName, txtCode, txtDescription, txtFeatures;

        private Label lblValidation, lblNameError, lblDimensionError, lblPriceError;        private NumericUpDown numLength, numWidth, numGoalHeight, numGoalWidth;

        private NumericUpDown numCapacity, numDefaultPrice, numPlayersPerTeam;

        public FieldTypeEditForm(FieldType fieldType)        private NumericUpDown numPeakMultiplier, numWeekendMultiplier;

        {        private NumericUpDown numMinBookingHours, numMaxBookingHours;

            originalFieldType = fieldType;        private ComboBox cmbStatus, cmbSurfaceType;

            InitializeComponent();        private Button btnSaveType, btnCancelType;

            InitializeFormControls();        private Label lblValidation, lblNameError, lblDimensionError, lblPriceError;

            LoadData();

        }        private void InitializeComponent()

        {

        private void InitializeComponent()            this.SuspendLayout();

        {            // 

            this.SuspendLayout();            // FieldTypeEditForm

            this.ClientSize = new System.Drawing.Size(700, 780);            // 

            this.Name = "FieldTypeEditForm";            this.ClientSize = new System.Drawing.Size(986, 566);

            this.ResumeLayout(false);            this.Name = "FieldTypeEditForm";

        }            this.ResumeLayout(false);



        private void InitializeFormControls()
        {
            this.Text = originalFieldType == null ? "Thêm Loại Sân Mới" : "Chỉnh Sửa Loại Sân";
            this.Size = new Size(700, 780);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;

            // Title            this.MaximizeBox = false;

            var lblFormTitle = new Label();            this.MinimizeBox = false;

            lblFormTitle.Text = originalFieldType == null ? "📋 THÊM LOẠI SÂN MỚI" : "📋 CHỈNH SỬA LOẠI SÂN";            this.BackColor = Color.FromArgb(247, 251, 247);

            lblFormTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);

            lblFormTitle.ForeColor = Color.FromArgb(30, 140, 58);            int y = 20;

            lblFormTitle.Location = new Point(labelX, y);            int labelX = 30;

            lblFormTitle.Size = new Size(620, 30);            int controlX = 180;

            this.Controls.Add(lblFormTitle);            int controlWidth = 420;

            y += 45;

            // Title

            // ========== THÔNG TIN CƠ BẢN ==========            var lblFormTitle = new Label();

            AddSectionHeader("THÔNG TIN CƠ BẢN", labelX, y);            lblFormTitle.Text = originalFieldType == null ? "📋 THÊM LOẠI SÂN MỚI" : "📋 CHỈNH SỬA LOẠI SÂN";

            y += 35;            lblFormTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);

            lblFormTitle.ForeColor = Color.FromArgb(30, 140, 58);

            // ID            lblFormTitle.Location = new Point(labelX, y);

            AddLabel("Mã loại:", labelX, y, true);            lblFormTitle.Size = new Size(580, 30);

            txtId = AddTextBox(controlX, y, controlWidth);            this.Controls.Add(lblFormTitle);

            txtId.ReadOnly = originalFieldType != null;            y += 45;

            txtId.BackColor = originalFieldType != null ? Color.FromArgb(240, 240, 240) : Color.White;

            y += 45;            // ID

            AddLabel("Mã loại:", labelX, y, true);

            // Name            txtId = AddTextBox(controlX, y, controlWidth);

            AddLabel("Tên loại:", labelX, y, true);            txtId.ReadOnly = originalFieldType != null;

            txtName = AddTextBox(controlX, y, controlWidth);            y += 50;

            y += 30;

            lblNameError = AddErrorLabel(controlX, y);            // Name

            y += 25;            AddLabel("Tên loại:", labelX, y, true);

            txtName = AddTextBox(controlX, y, controlWidth);

            // Code            y += 30;

            AddLabel("Mã code:", labelX, y);            lblNameError = AddErrorLabel(controlX, y);

            txtCode = AddTextBox(controlX, y, controlWidth);            y += 25;

            txtCode.PlaceholderText = "VD: S5, S7, S11";

            y += 50;            // Code

            AddLabel("Mã code:", labelX, y);

            // ========== KÍCH THƯỚC SÂN ==========            txtCode = AddTextBox(controlX, y, controlWidth);

            AddSectionHeader("KÍCH THƯỚC SÂN", labelX, y);            y += 50;

            y += 35;

            // Players Per Team

            // Length & Width            AddLabel("Số cầu thủ/đội:", labelX, y);

            AddLabel("Chiều dài (m):", labelX, y, true);            numPlayersPerTeam = new NumericUpDown();

            numLength = CreateNumericUpDown(controlX, y, halfWidth, 0, 200, 1);            numPlayersPerTeam.Location = new Point(controlX, y);

            AddLabel("Chiều rộng (m):", controlX + halfWidth + 20, y, true);            numPlayersPerTeam.Size = new Size(controlWidth, 25);

            numWidth = CreateNumericUpDown(controlX + halfWidth + 20 + 150, y, halfWidth - 150, 0, 200, 1);            numPlayersPerTeam.Maximum = 50;

            y += 30;            numPlayersPerTeam.Minimum = 1;

            lblDimensionError = AddErrorLabel(controlX, y);            numPlayersPerTeam.Font = new Font("Segoe UI", 10F);

            y += 25;            this.Controls.Add(numPlayersPerTeam);

            y += 50;

            // Goal Size

            AddLabel("Khung thành - Cao (m):", labelX, y);            // Capacity (Total)

            numGoalHeight = CreateNumericUpDown(controlX, y, halfWidth, 0, 10, 0.1m);            AddLabel("Sức chứa:", labelX, y, true);

            AddLabel("Rộng (m):", controlX + halfWidth + 20, y);            numCapacity = new NumericUpDown();

            numGoalWidth = CreateNumericUpDown(controlX + halfWidth + 20 + 100, y, halfWidth - 100, 0, 20, 0.1m);            numCapacity.Location = new Point(controlX, y);

            y += 50;            numCapacity.Size = new Size(controlWidth, 25);

            numCapacity.Maximum = 100;

            // ========== THÔNG TIN CẦU THỦ ==========            numCapacity.Minimum = 1;

            AddSectionHeader("THÔNG TIN CẦU THỦ", labelX, y);            numCapacity.Font = new Font("Segoe UI", 10F);

            y += 35;            this.Controls.Add(numCapacity);

            y += 30;

            // Players Per Team            lblCapacityError = AddErrorLabel(controlX, y);

            AddLabel("Số cầu thủ/đội:", labelX, y, true);            y += 25;

            numPlayersPerTeam = CreateNumericUpDown(controlX, y, halfWidth, 1, 50, 1);

                        // Default Price

            // Total Capacity            AddLabel("Giá mặc định (VNĐ):", labelX, y, true);

            AddLabel("Tổng sức chứa:", controlX + halfWidth + 20, y, true);            numDefaultPrice = new NumericUpDown();

            numCapacity = CreateNumericUpDown(controlX + halfWidth + 20 + 150, y, halfWidth - 150, 1, 100, 1);            numDefaultPrice.Location = new Point(controlX, y);

            y += 50;            numDefaultPrice.Size = new Size(controlWidth, 25);

            numDefaultPrice.Maximum = 10000000;

            // ========== LOẠI BỀ MẶT ==========            numDefaultPrice.Minimum = 0;

            AddSectionHeader("LOẠI BỀ MẶT", labelX, y);            numDefaultPrice.Increment = 10000;

            y += 35;            numDefaultPrice.Font = new Font("Segoe UI", 10F);

            numDefaultPrice.ThousandsSeparator = true;

            AddLabel("Loại bề mặt:", labelX, y, true);            this.Controls.Add(numDefaultPrice);

            cmbSurfaceType = new ComboBox();            y += 30;

            cmbSurfaceType.Location = new Point(controlX, y);            lblPriceError = AddErrorLabel(controlX, y);

            cmbSurfaceType.Size = new Size(controlWidth, 25);            y += 25;

            cmbSurfaceType.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbSurfaceType.Font = new Font("Segoe UI", 10F);            // Size Display

            cmbSurfaceType.Items.AddRange(new object[] {             AddLabel("Kích thước:", labelX, y);

                "Cỏ nhân tạo",             txtSizeDisplay = AddTextBox(controlX, y, controlWidth);

                "Cỏ tự nhiên",            //txtSizeDisplay.PlaceholderText = "Ví dụ: 40m x 60m";

                "Sân xi măng",            y += 50;

                "Sân đất"

            });            // Surface Type

            this.Controls.Add(cmbSurfaceType);            AddLabel("Loại bề mặt:", labelX, y);

            y += 50;            txtSurfaceType = AddTextBox(controlX, y, controlWidth);

            //txtSurfaceType.PlaceholderText = "Ví dụ: Cỏ nhân tạo";

            // ========== GIÁ VÀ HỆ SỐ ==========            y += 50;

            AddSectionHeader("GIÁ VÀ HỆ SỐ NHÂN", labelX, y);

            y += 35;            // Color Picker

            AddLabel("Màu badge:", labelX, y);

            // Base Price            var pnlColor = new Panel();

            AddLabel("Giá cơ bản (VNĐ):", labelX, y, true);            pnlColor.Location = new Point(controlX, y);

            numDefaultPrice = new NumericUpDown();            pnlColor.Size = new Size(320, 30);

            numDefaultPrice.Location = new Point(controlX, y);            

            numDefaultPrice.Size = new Size(controlWidth, 25);            cmbColor = new ComboBox();

            numDefaultPrice.Maximum = 10000000;            cmbColor.Location = new Point(0, 0);

            numDefaultPrice.Minimum = 0;            cmbColor.Size = new Size(220, 25);

            numDefaultPrice.Increment = 10000;            cmbColor.DropDownStyle = ComboBoxStyle.DropDownList;

            numDefaultPrice.Font = new Font("Segoe UI", 10F);            cmbColor.Font = new Font("Segoe UI", 10F);

            numDefaultPrice.ThousandsSeparator = true;            cmbColor.Items.AddRange(new object[] { 

            this.Controls.Add(numDefaultPrice);                "Xanh lá - #1E8C3A", 

            y += 30;                "Xanh dương - #007BFF", 

            lblPriceError = AddErrorLabel(controlX, y);                "Cam - #FF8C00",

            y += 25;                "Đỏ - #DC3545",

                "Tím - #6F42C1"

            // Peak Hour Multiplier            });

            AddLabel("Hệ số giờ cao điểm:", labelX, y);            pnlColor.Controls.Add(cmbColor);

            numPeakMultiplier = CreateNumericUpDown(controlX, y, halfWidth, 1, 3, 0.1m);

            numPeakMultiplier.Value = 1.5m;            btnPickColor = new Button();

                        btnPickColor.Text = "🎨 Chọn màu";

            // Weekend Multiplier            btnPickColor.Location = new Point(230, 0);

            AddLabel("Hệ số cuối tuần:", controlX + halfWidth + 20, y);            btnPickColor.Size = new Size(90, 27);

            numWeekendMultiplier = CreateNumericUpDown(controlX + halfWidth + 20 + 150, y, halfWidth - 150, 1, 3, 0.1m);            btnPickColor.FlatStyle = FlatStyle.Flat;

            numWeekendMultiplier.Value = 1.3m;            btnPickColor.Font = new Font("Segoe UI", 9F);

            y += 50;            btnPickColor.Click += btnPickColor_Click;

            pnlColor.Controls.Add(btnPickColor);

            // ========== THỜI GIAN ĐẶT SÂN ==========

            AddSectionHeader("THỜI GIAN ĐẶT SÂN", labelX, y);            this.Controls.Add(pnlColor);

            y += 35;            y += 50;



            // Min/Max Booking Hours            // Icon

            AddLabel("Số giờ tối thiểu:", labelX, y);            AddLabel("Icon:", labelX, y);

            numMinBookingHours = CreateNumericUpDown(controlX, y, halfWidth, 1, 24, 1);            cmbIcon = new ComboBox();

            numMinBookingHours.Value = 1;            cmbIcon.Location = new Point(controlX, y);

                        cmbIcon.Size = new Size(controlWidth, 25);

            AddLabel("Số giờ tối đa:", controlX + halfWidth + 20, y);            cmbIcon.DropDownStyle = ComboBoxStyle.DropDownList;

            numMaxBookingHours = CreateNumericUpDown(controlX + halfWidth + 20 + 150, y, halfWidth - 150, 1, 24, 1);            cmbIcon.Font = new Font("Segoe UI", 10F);

            numMaxBookingHours.Value = 4;            cmbIcon.Items.AddRange(new object[] { 

            y += 50;                "⚽ Bóng đá", 

                "🏟️ Sân", 

            // ========== MÔ TẢ & TÍNH NĂNG ==========                "👥 Nhóm",

            AddSectionHeader("MÔ TẢ & TÍNH NĂNG", labelX, y);                "🎯 Mục tiêu",

            y += 35;                "⭐ Ngôi sao",

                "🏆 Cúp"

            // Description            });

            AddLabel("Mô tả:", labelX, y);            this.Controls.Add(cmbIcon);

            txtDescription = new TextBox();            y += 50;

            txtDescription.Location = new Point(controlX, y);

            txtDescription.Size = new Size(controlWidth, 60);            // Status

            txtDescription.Multiline = true;            AddLabel("Trạng thái:", labelX, y);

            txtDescription.Font = new Font("Segoe UI", 10F);            cmbStatus = new ComboBox();

            txtDescription.ScrollBars = ScrollBars.Vertical;            cmbStatus.Location = new Point(controlX, y);

            this.Controls.Add(txtDescription);            cmbStatus.Size = new Size(controlWidth, 25);

            y += 70;            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbStatus.Font = new Font("Segoe UI", 10F);

            // Features            cmbStatus.Items.AddRange(new object[] { "Active", "Inactive" });

            AddLabel("Tính năng:", labelX, y);            this.Controls.Add(cmbStatus);

            txtFeatures = new TextBox();            y += 50;

            txtFeatures.Location = new Point(controlX, y);

            txtFeatures.Size = new Size(controlWidth, 60);            // Description

            txtFeatures.Multiline = true;            AddLabel("Mô tả:", labelX, y);

            txtFeatures.Font = new Font("Segoe UI", 10F);            txtDescription = new TextBox();

            txtFeatures.ScrollBars = ScrollBars.Vertical;            txtDescription.Location = new Point(controlX, y);

            txtFeatures.PlaceholderText = "VD: Mái che, Đèn chiếu sáng, Phòng thay đồ";            txtDescription.Size = new Size(controlWidth, 70);

            this.Controls.Add(txtFeatures);            txtDescription.Multiline = true;

            y += 70;            txtDescription.Font = new Font("Segoe UI", 10F);

            txtDescription.ScrollBars = ScrollBars.Vertical;

            // Status            this.Controls.Add(txtDescription);

            AddLabel("Trạng thái:", labelX, y);            y += 80;

            cmbStatus = new ComboBox();

            cmbStatus.Location = new Point(controlX, y);            // Validation Label

            cmbStatus.Size = new Size(controlWidth, 25);            lblValidation = new Label();

            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;            lblValidation.Location = new Point(labelX, y);

            cmbStatus.Font = new Font("Segoe UI", 10F);            lblValidation.Size = new Size(580, 20);

            cmbStatus.Items.AddRange(new object[] { "Active", "Inactive" });            lblValidation.ForeColor = Color.Red;

            this.Controls.Add(cmbStatus);            lblValidation.Font = new Font("Segoe UI", 9F);

            y += 50;            lblValidation.Visible = false;

            this.Controls.Add(lblValidation);

            // Validation Label            y += 30;

            lblValidation = new Label();

            lblValidation.Location = new Point(labelX, y);            // Buttons

            lblValidation.Size = new Size(620, 20);            btnSaveType = new Button();

            lblValidation.ForeColor = Color.Red;            btnSaveType.Text = "💾 Lưu";

            lblValidation.Font = new Font("Segoe UI", 9F);            btnSaveType.Location = new Point(240, y);

            lblValidation.Visible = false;            btnSaveType.Size = new Size(130, 40);

            this.Controls.Add(lblValidation);            btnSaveType.BackColor = Color.FromArgb(30, 140, 58);

            y += 30;            btnSaveType.ForeColor = Color.White;

            btnSaveType.FlatStyle = FlatStyle.Flat;

            // Buttons            btnSaveType.FlatAppearance.BorderSize = 0;

            btnSaveType = new Button();            btnSaveType.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            btnSaveType.Text = "💾 Lưu";            btnSaveType.Cursor = Cursors.Hand;

            btnSaveType.Location = new Point(280, y);            btnSaveType.Click += btnSaveType_Click;

            btnSaveType.Size = new Size(150, 42);            this.Controls.Add(btnSaveType);

            btnSaveType.BackColor = Color.FromArgb(30, 140, 58);

            btnSaveType.ForeColor = Color.White;            btnCancelType = new Button();

            btnSaveType.FlatStyle = FlatStyle.Flat;            btnCancelType.Text = "❌ Hủy";

            btnSaveType.FlatAppearance.BorderSize = 0;            btnCancelType.Location = new Point(380, y);

            btnSaveType.Font = new Font("Segoe UI", 11F, FontStyle.Bold);            btnCancelType.Size = new Size(130, 40);

            btnSaveType.Cursor = Cursors.Hand;            btnCancelType.BackColor = Color.White;

            btnSaveType.Click += btnSaveType_Click;            btnCancelType.ForeColor = Color.FromArgb(43, 43, 43);

            this.Controls.Add(btnSaveType);            btnCancelType.FlatStyle = FlatStyle.Flat;

            btnCancelType.FlatAppearance.BorderColor = Color.FromArgb(30, 140, 58);

            btnCancelType = new Button();            btnCancelType.Font = new Font("Segoe UI", 10F);

            btnCancelType.Text = "❌ Hủy";            btnCancelType.Cursor = Cursors.Hand;

            btnCancelType.Location = new Point(445, y);            btnCancelType.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            btnCancelType.Size = new Size(150, 42);            this.Controls.Add(btnCancelType);

            btnCancelType.BackColor = Color.White;

            btnCancelType.ForeColor = Color.FromArgb(43, 43, 43);            // Color Dialog

            btnCancelType.FlatStyle = FlatStyle.Flat;            colorDialog = new ColorDialog();

            btnCancelType.FlatAppearance.BorderColor = Color.FromArgb(30, 140, 58);        }

            btnCancelType.Font = new Font("Segoe UI", 11F);

            btnCancelType.Cursor = Cursors.Hand;        private Label AddLabel(string text, int x, int y, bool required = false)

            btnCancelType.Click += (s, e) => this.DialogResult = DialogResult.Cancel;        {

            this.Controls.Add(btnCancelType);            var label = new Label();

        }            label.Text = text + (required ? " *" : "");

            label.Location = new Point(x, y + 3);

        private void AddSectionHeader(string text, int x, int y)            label.Size = new Size(140, 20);

        {            label.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            var panel = new Panel();            label.ForeColor = Color.FromArgb(43, 43, 43);

            panel.Location = new Point(x, y);            this.Controls.Add(label);

            panel.Size = new Size(620, 25);            return label;

            panel.BackColor = Color.FromArgb(223, 247, 230);        }

            

            var label = new Label();        private TextBox AddTextBox(int x, int y, int width)

            label.Text = text;        {

            label.Font = new Font("Segoe UI", 10F, FontStyle.Bold);            var textBox = new TextBox();

            label.ForeColor = Color.FromArgb(30, 140, 58);            textBox.Location = new Point(x, y);

            label.Location = new Point(10, 3);            textBox.Size = new Size(width, 25);

            label.AutoSize = true;            textBox.Font = new Font("Segoe UI", 10F);

            panel.Controls.Add(label);            this.Controls.Add(textBox);

                        return textBox;

            this.Controls.Add(panel);        }

        }

        private Label AddErrorLabel(int x, int y)

        private NumericUpDown CreateNumericUpDown(int x, int y, int width, decimal min, decimal max, decimal increment)        {

        {            var label = new Label();

            var num = new NumericUpDown();            label.Location = new Point(x, y);

            num.Location = new Point(x, y);            label.Size = new Size(420, 18);

            num.Size = new Size(width, 25);            label.ForeColor = Color.Red;

            num.Minimum = min;            label.Font = new Font("Segoe UI", 8F);

            num.Maximum = max;            label.Visible = false;

            num.Increment = increment;            this.Controls.Add(label);

            num.DecimalPlaces = increment < 1 ? 2 : 0;            return label;

            num.Font = new Font("Segoe UI", 10F);        }

            this.Controls.Add(num);

            return num;        private void LoadData()

        }        {

            if (originalFieldType != null)

        private Label AddLabel(string text, int x, int y, bool required = false)            {

        {                // Edit mode

            var label = new Label();                txtId.Text = originalFieldType.Id;

            label.Text = text + (required ? " *" : "");                txtName.Text = originalFieldType.Name;

            label.Location = new Point(x, y + 3);                txtCode.Text = originalFieldType.Code;

            label.Size = new Size(160, 20);                numPlayersPerTeam.Value = originalFieldType.PlayersPerTeam;

            label.Font = new Font("Segoe UI", 9F, FontStyle.Bold);                numCapacity.Value = originalFieldType.TotalCapacity;

            label.ForeColor = Color.FromArgb(43, 43, 43);                numDefaultPrice.Value = originalFieldType.BasePrice;

            this.Controls.Add(label);                txtSizeDisplay.Text = originalFieldType.SizeDisplay;

            return label;                txtSurfaceType.Text = originalFieldType.SurfaceType;

        }                txtDescription.Text = originalFieldType.Description;

                cmbStatus.SelectedItem = originalFieldType.Status;

        private TextBox AddTextBox(int x, int y, int width)

        {                // Default selections

            var textBox = new TextBox();                if (cmbColor.Items.Count > 0) cmbColor.SelectedIndex = 0;

            textBox.Location = new Point(x, y);                if (cmbIcon.Items.Count > 0) cmbIcon.SelectedIndex = 0;

            textBox.Size = new Size(width, 25);            }

            textBox.Font = new Font("Segoe UI", 10F);            else

            this.Controls.Add(textBox);            {

            return textBox;                // Add mode - generate ID

        }                txtId.Text = GenerateNewId();

                cmbStatus.SelectedIndex = 0;

        private Label AddErrorLabel(int x, int y)                numPlayersPerTeam.Value = 5;

        {                numCapacity.Value = 10;

            var label = new Label();                numDefaultPrice.Value = 100000;

            label.Location = new Point(x, y);                txtSurfaceType.Text = "Cỏ nhân tạo";

            label.Size = new Size(450, 18);                

            label.ForeColor = Color.Red;                if (cmbColor.Items.Count > 0) cmbColor.SelectedIndex = 0;

            label.Font = new Font("Segoe UI", 8F);                if (cmbIcon.Items.Count > 0) cmbIcon.SelectedIndex = 0;

            label.Visible = false;            }

            this.Controls.Add(label);        }

            return label;

        }        private string GenerateNewId()

        {

        private void LoadData()            return "FT" + DateTime.Now.ToString("yyyyMMddHHmmss");

        {        }

            if (originalFieldType != null)

            {        private void btnPickColor_Click(object sender, EventArgs e)

                // Edit mode        {

                txtId.Text = originalFieldType.Id;            if (colorDialog.ShowDialog() == DialogResult.OK)

                txtName.Text = originalFieldType.Name;            {

                txtCode.Text = originalFieldType.Code;                var color = colorDialog.Color;

                                var hexColor = $"#{color.R:X2}{color.G:X2}{color.B:X2}";

                numLength.Value = originalFieldType.Length;                cmbColor.Items.Add($"Tùy chỉnh - {hexColor}");

                numWidth.Value = originalFieldType.Width;                cmbColor.SelectedIndex = cmbColor.Items.Count - 1;

                numGoalHeight.Value = originalFieldType.GoalHeight;            }

                numGoalWidth.Value = originalFieldType.GoalWidth;        }

                

                numPlayersPerTeam.Value = originalFieldType.PlayersPerTeam;        private void btnSaveType_Click(object sender, EventArgs e)

                numCapacity.Value = originalFieldType.TotalCapacity;        {

                            // TODO: validate and close dialog (no persistence code)

                cmbSurfaceType.SelectedItem = originalFieldType.SurfaceType;            if (!ValidateInput())

                if (cmbSurfaceType.SelectedIndex == -1 && !string.IsNullOrEmpty(originalFieldType.SurfaceType))                return;

                {

                    cmbSurfaceType.Items.Add(originalFieldType.SurfaceType);            EditedFieldType = new FieldType

                    cmbSurfaceType.SelectedItem = originalFieldType.SurfaceType;            {

                }                Id = txtId.Text.Trim(),

                                Name = txtName.Text.Trim(),

                numDefaultPrice.Value = originalFieldType.BasePrice;                Code = txtCode.Text.Trim(),

                numPeakMultiplier.Value = originalFieldType.PeakHourMultiplier;                PlayersPerTeam = (int)numPlayersPerTeam.Value,

                numWeekendMultiplier.Value = originalFieldType.WeekendMultiplier;                TotalCapacity = (int)numCapacity.Value,

                                BasePrice = numDefaultPrice.Value,

                numMinBookingHours.Value = originalFieldType.MinimumBookingHours;                SizeDisplay = txtSizeDisplay.Text.Trim(),

                numMaxBookingHours.Value = originalFieldType.MaximumBookingHours;                SurfaceType = txtSurfaceType.Text.Trim(),

                                Description = txtDescription.Text.Trim(),

                txtDescription.Text = originalFieldType.Description;                Status = cmbStatus.SelectedItem?.ToString() ?? "Active"

                txtFeatures.Text = originalFieldType.Features;            };

                cmbStatus.SelectedItem = originalFieldType.Status;

            }            this.DialogResult = DialogResult.OK;

            else        }

            {

                // Add mode - generate ID        private bool ValidateInput()

                txtId.Text = GenerateNewId();        {

                cmbStatus.SelectedIndex = 0;            // Reset error labels

                cmbSurfaceType.SelectedIndex = 0;            lblNameError.Visible = false;

                            lblCapacityError.Visible = false;

                numPlayersPerTeam.Value = 5;            lblPriceError.Visible = false;

                numCapacity.Value = 10;            lblValidation.Visible = false;

                numLength.Value = 40;

                numWidth.Value = 20;            bool isValid = true;

                numGoalHeight.Value = 2;

                numGoalWidth.Value = 3;            // Validate Name

                numDefaultPrice.Value = 100000;            if (string.IsNullOrWhiteSpace(txtName.Text))

                numPeakMultiplier.Value = 1.5m;            {

                numWeekendMultiplier.Value = 1.3m;                ShowFieldError(lblNameError, "⚠️ Vui lòng nhập tên loại sân!");

                numMinBookingHours.Value = 1;                isValid = false;

                numMaxBookingHours.Value = 4;            }

            }

        }            // Validate Capacity

            if (numCapacity.Value <= 0)

        private string GenerateNewId()            {

        {                ShowFieldError(lblCapacityError, "⚠️ Sức chứa phải lớn hơn 0!");

            return "FT" + DateTime.Now.ToString("yyyyMMddHHmmss");                isValid = false;

        }            }



        private void btnSaveType_Click(object sender, EventArgs e)            // Validate Price

        {            if (numDefaultPrice.Value <= 0)

            if (!ValidateInput())            {

                return;                ShowFieldError(lblPriceError, "⚠️ Giá mặc định phải lớn hơn 0!");

                isValid = false;

            EditedFieldType = new FieldType            }

            {

                Id = txtId.Text.Trim(),            // General validation message

                Name = txtName.Text.Trim(),            if (!isValid)

                Code = txtCode.Text.Trim(),            {

                                ShowValidation("⚠️ Vui lòng kiểm tra và điền đầy đủ các thông tin bắt buộc!");

                Length = numLength.Value,            }

                Width = numWidth.Value,

                DimensionUnit = "mét",            return isValid;

                SizeDisplay = $"{numWidth.Value}m x {numLength.Value}m",        }

                

                GoalHeight = numGoalHeight.Value,        private void ShowFieldError(Label errorLabel, string message)

                GoalWidth = numGoalWidth.Value,        {

                GoalUnit = "mét",            errorLabel.Text = message;

                            errorLabel.Visible = true;

                PlayersPerTeam = (int)numPlayersPerTeam.Value,        }

                TotalCapacity = (int)numCapacity.Value,

                        private void ShowValidation(string message)

                SurfaceType = cmbSurfaceType.SelectedItem?.ToString() ?? "Cỏ nhân tạo",        {

                BasePrice = numDefaultPrice.Value,            lblValidation.Text = message;

                PeakHourMultiplier = numPeakMultiplier.Value,            lblValidation.Visible = true;

                WeekendMultiplier = numWeekendMultiplier.Value,        }

                    }

                MinimumBookingHours = (int)numMinBookingHours.Value,}

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