using System;
using System.Drawing;
using System.Collections.Generic;
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
        private TextBox txtId, txtName, txtCode, txtDescription, txtFeatures;
        private NumericUpDown numLength, numWidth, numGoalHeight, numGoalWidth;
        private NumericUpDown numCapacity, numDefaultPrice, numPlayersPerTeam;
        private NumericUpDown numPeakMultiplier, numWeekendMultiplier;
        private NumericUpDown numMinBookingHours, numMaxBookingHours;
        private ComboBox cmbStatus, cmbSurfaceType, cmbColor, cmbIcon;
        private Button btnSaveType, btnCancelType, btnPickColor;
        private Label lblValidation, lblNameError, lblDimensionError, lblPriceError, lblCapacityError;
        private ColorDialog colorDialog;

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
            this.ClientSize = new Size(700, 780);
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

            // Thêm các control ở đây (txtId, txtName, button, ...)
        }

        private void btnPickColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // ví dụ đổi màu panel hoặc label
                btnPickColor.BackColor = colorDialog.Color;
            }
        }

        private void LoadData()
        {
            if (originalFieldType == null) return;

            txtId.Text = originalFieldType.Id.ToString();
            txtName.Text = originalFieldType.Name;
            txtCode.Text = originalFieldType.Code;
            txtDescription.Text = originalFieldType.Description;
        }
    }
}
