namespace CNXML_HVA
{
    partial class DungCu
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewEquipments = new System.Windows.Forms.DataGridView();
            this.groupBoxEquipmentInfo = new System.Windows.Forms.GroupBox();
            this.labelId = new System.Windows.Forms.Label();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelCategory = new System.Windows.Forms.Label();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.labelBrand = new System.Windows.Forms.Label();
            this.textBoxBrand = new System.Windows.Forms.TextBox();
            this.labelModel = new System.Windows.Forms.Label();
            this.textBoxModel = new System.Windows.Forms.TextBox();
            this.labelQuantityTotal = new System.Windows.Forms.Label();
            this.numericUpDownQuantityTotal = new System.Windows.Forms.NumericUpDown();
            this.labelQuantityAvailable = new System.Windows.Forms.Label();
            this.numericUpDownQuantityAvailable = new System.Windows.Forms.NumericUpDown();
            this.labelRentalPrice = new System.Windows.Forms.Label();
            this.numericUpDownRentalPrice = new System.Windows.Forms.NumericUpDown();
            this.labelPurchasePrice = new System.Windows.Forms.Label();
            this.numericUpDownPurchasePrice = new System.Windows.Forms.NumericUpDown();
            this.labelCondition = new System.Windows.Forms.Label();
            this.comboBoxCondition = new System.Windows.Forms.ComboBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelBranchId = new System.Windows.Forms.Label();
            this.textBoxBranchId = new System.Windows.Forms.TextBox();
            this.labelSupplier = new System.Windows.Forms.Label();
            this.textBoxSupplier = new System.Windows.Forms.TextBox();
            this.labelPurchaseDate = new System.Windows.Forms.Label();
            this.dateTimePickerPurchaseDate = new System.Windows.Forms.DateTimePicker();
            this.labelWarrantyPeriod = new System.Windows.Forms.Label();
            this.numericUpDownWarrantyPeriod = new System.Windows.Forms.NumericUpDown();
            this.labelStatus = new System.Windows.Forms.Label();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.labelSearch = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEquipments)).BeginInit();
            this.groupBoxEquipmentInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantityTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantityAvailable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRentalPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPurchasePrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWarrantyPeriod)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewEquipments
            // 
            this.dataGridViewEquipments.AllowUserToAddRows = false;
            this.dataGridViewEquipments.AllowUserToDeleteRows = false;
            this.dataGridViewEquipments.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewEquipments.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewEquipments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEquipments.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.dataGridViewEquipments.Location = new System.Drawing.Point(20, 140);
            this.dataGridViewEquipments.MultiSelect = false;
            this.dataGridViewEquipments.Name = "dataGridViewEquipments";
            this.dataGridViewEquipments.ReadOnly = true;
            this.dataGridViewEquipments.RowHeadersVisible = false;
            this.dataGridViewEquipments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEquipments.Size = new System.Drawing.Size(760, 300);
            this.dataGridViewEquipments.TabIndex = 0;
            this.dataGridViewEquipments.SelectionChanged += new System.EventHandler(this.dataGridViewEquipments_SelectionChanged);
            // 
            // groupBoxEquipmentInfo
            // 
            this.groupBoxEquipmentInfo.BackColor = System.Drawing.Color.White;
            this.groupBoxEquipmentInfo.Controls.Add(this.labelId);
            this.groupBoxEquipmentInfo.Controls.Add(this.textBoxId);
            this.groupBoxEquipmentInfo.Controls.Add(this.labelName);
            this.groupBoxEquipmentInfo.Controls.Add(this.textBoxName);
            this.groupBoxEquipmentInfo.Controls.Add(this.labelCategory);
            this.groupBoxEquipmentInfo.Controls.Add(this.comboBoxCategory);
            this.groupBoxEquipmentInfo.Controls.Add(this.labelBrand);
            this.groupBoxEquipmentInfo.Controls.Add(this.textBoxBrand);
            this.groupBoxEquipmentInfo.Controls.Add(this.labelModel);
            this.groupBoxEquipmentInfo.Controls.Add(this.textBoxModel);
            this.groupBoxEquipmentInfo.Controls.Add(this.labelQuantityTotal);
            this.groupBoxEquipmentInfo.Controls.Add(this.numericUpDownQuantityTotal);
            this.groupBoxEquipmentInfo.Controls.Add(this.labelQuantityAvailable);
            this.groupBoxEquipmentInfo.Controls.Add(this.numericUpDownQuantityAvailable);
            this.groupBoxEquipmentInfo.Controls.Add(this.labelRentalPrice);
            this.groupBoxEquipmentInfo.Controls.Add(this.numericUpDownRentalPrice);
            this.groupBoxEquipmentInfo.Controls.Add(this.labelPurchasePrice);
            this.groupBoxEquipmentInfo.Controls.Add(this.numericUpDownPurchasePrice);
            this.groupBoxEquipmentInfo.Controls.Add(this.labelCondition);
            this.groupBoxEquipmentInfo.Controls.Add(this.comboBoxCondition);
            this.groupBoxEquipmentInfo.Controls.Add(this.labelDescription);
            this.groupBoxEquipmentInfo.Controls.Add(this.textBoxDescription);
            this.groupBoxEquipmentInfo.Controls.Add(this.labelBranchId);
            this.groupBoxEquipmentInfo.Controls.Add(this.textBoxBranchId);
            this.groupBoxEquipmentInfo.Controls.Add(this.labelSupplier);
            this.groupBoxEquipmentInfo.Controls.Add(this.textBoxSupplier);
            this.groupBoxEquipmentInfo.Controls.Add(this.labelPurchaseDate);
            this.groupBoxEquipmentInfo.Controls.Add(this.dateTimePickerPurchaseDate);
            this.groupBoxEquipmentInfo.Controls.Add(this.labelWarrantyPeriod);
            this.groupBoxEquipmentInfo.Controls.Add(this.numericUpDownWarrantyPeriod);
            this.groupBoxEquipmentInfo.Controls.Add(this.labelStatus);
            this.groupBoxEquipmentInfo.Controls.Add(this.comboBoxStatus);
            this.groupBoxEquipmentInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxEquipmentInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.groupBoxEquipmentInfo.Location = new System.Drawing.Point(800, 80);
            this.groupBoxEquipmentInfo.Name = "groupBoxEquipmentInfo";
            this.groupBoxEquipmentInfo.Size = new System.Drawing.Size(380, 500);
            this.groupBoxEquipmentInfo.TabIndex = 1;
            this.groupBoxEquipmentInfo.TabStop = false;
            this.groupBoxEquipmentInfo.Text = "Thông Tin Dụng Cụ";
            // 
            // labelId
            // 
            this.labelId.AutoSize = true;
            this.labelId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.labelId.Location = new System.Drawing.Point(15, 25);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(48, 13);
            this.labelId.TabIndex = 0;
            this.labelId.Text = "Mã DC:";
            // 
            // textBoxId
            // 
            this.textBoxId.Location = new System.Drawing.Point(120, 22);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(240, 21);
            this.textBoxId.TabIndex = 1;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.labelName.Location = new System.Drawing.Point(15, 50);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(57, 13);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "Tên DC:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(120, 47);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(240, 21);
            this.textBoxName.TabIndex = 3;
            // 
            // labelCategory
            // 
            this.labelCategory.AutoSize = true;
            this.labelCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.labelCategory.Location = new System.Drawing.Point(15, 75);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(62, 13);
            this.labelCategory.TabIndex = 4;
            this.labelCategory.Text = "Danh mục:";
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Items.AddRange(new object[] {
            "Bóng",
            "Trang phục",
            "Thiết bị sân",
            "Phụ kiện",
            "Thiết bị tập luyện"});
            this.comboBoxCategory.Location = new System.Drawing.Point(120, 72);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(240, 23);
            this.comboBoxCategory.TabIndex = 5;
            // 
            // labelBrand
            // 
            this.labelBrand.AutoSize = true;
            this.labelBrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelBrand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.labelBrand.Location = new System.Drawing.Point(15, 100);
            this.labelBrand.Name = "labelBrand";
            this.labelBrand.Size = new System.Drawing.Size(70, 13);
            this.labelBrand.TabIndex = 6;
            this.labelBrand.Text = "Thương hiệu:";
            // 
            // textBoxBrand
            // 
            this.textBoxBrand.Location = new System.Drawing.Point(120, 97);
            this.textBoxBrand.Name = "textBoxBrand";
            this.textBoxBrand.Size = new System.Drawing.Size(240, 21);
            this.textBoxBrand.TabIndex = 7;
            // 
            // labelModel
            // 
            this.labelModel.AutoSize = true;
            this.labelModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.labelModel.Location = new System.Drawing.Point(15, 125);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(37, 13);
            this.labelModel.TabIndex = 8;
            this.labelModel.Text = "Model:";
            // 
            // textBoxModel
            // 
            this.textBoxModel.Location = new System.Drawing.Point(120, 122);
            this.textBoxModel.Name = "textBoxModel";
            this.textBoxModel.Size = new System.Drawing.Size(240, 21);
            this.textBoxModel.TabIndex = 9;
            // 
            // labelQuantityTotal
            // 
            this.labelQuantityTotal.AutoSize = true;
            this.labelQuantityTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelQuantityTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.labelQuantityTotal.Location = new System.Drawing.Point(15, 150);
            this.labelQuantityTotal.Name = "labelQuantityTotal";
            this.labelQuantityTotal.Size = new System.Drawing.Size(55, 13);
            this.labelQuantityTotal.TabIndex = 10;
            this.labelQuantityTotal.Text = "Tổng SL:";
            // 
            // numericUpDownQuantityTotal
            // 
            this.numericUpDownQuantityTotal.Location = new System.Drawing.Point(120, 147);
            this.numericUpDownQuantityTotal.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownQuantityTotal.Name = "numericUpDownQuantityTotal";
            this.numericUpDownQuantityTotal.Size = new System.Drawing.Size(110, 21);
            this.numericUpDownQuantityTotal.TabIndex = 11;
            // 
            // labelQuantityAvailable
            // 
            this.labelQuantityAvailable.AutoSize = true;
            this.labelQuantityAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelQuantityAvailable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.labelQuantityAvailable.Location = new System.Drawing.Point(15, 175);
            this.labelQuantityAvailable.Name = "labelQuantityAvailable";
            this.labelQuantityAvailable.Size = new System.Drawing.Size(72, 13);
            this.labelQuantityAvailable.TabIndex = 12;
            this.labelQuantityAvailable.Text = "SL Khả dụng:";
            // 
            // numericUpDownQuantityAvailable
            // 
            this.numericUpDownQuantityAvailable.Location = new System.Drawing.Point(120, 172);
            this.numericUpDownQuantityAvailable.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownQuantityAvailable.Name = "numericUpDownQuantityAvailable";
            this.numericUpDownQuantityAvailable.Size = new System.Drawing.Size(110, 21);
            this.numericUpDownQuantityAvailable.TabIndex = 13;
            // 
            // labelRentalPrice
            // 
            this.labelRentalPrice.AutoSize = true;
            this.labelRentalPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelRentalPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.labelRentalPrice.Location = new System.Drawing.Point(15, 200);
            this.labelRentalPrice.Name = "labelRentalPrice";
            this.labelRentalPrice.Size = new System.Drawing.Size(53, 13);
            this.labelRentalPrice.TabIndex = 14;
            this.labelRentalPrice.Text = "Giá thuê:";
            // 
            // numericUpDownRentalPrice
            // 
            this.numericUpDownRentalPrice.Location = new System.Drawing.Point(120, 197);
            this.numericUpDownRentalPrice.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numericUpDownRentalPrice.Name = "numericUpDownRentalPrice";
            this.numericUpDownRentalPrice.Size = new System.Drawing.Size(110, 21);
            this.numericUpDownRentalPrice.TabIndex = 15;
            // 
            // labelPurchasePrice
            // 
            this.labelPurchasePrice.AutoSize = true;
            this.labelPurchasePrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelPurchasePrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.labelPurchasePrice.Location = new System.Drawing.Point(15, 225);
            this.labelPurchasePrice.Name = "labelPurchasePrice";
            this.labelPurchasePrice.Size = new System.Drawing.Size(56, 13);
            this.labelPurchasePrice.TabIndex = 16;
            this.labelPurchasePrice.Text = "Giá mua:";
            // 
            // numericUpDownPurchasePrice
            // 
            this.numericUpDownPurchasePrice.Location = new System.Drawing.Point(120, 222);
            this.numericUpDownPurchasePrice.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownPurchasePrice.Name = "numericUpDownPurchasePrice";
            this.numericUpDownPurchasePrice.Size = new System.Drawing.Size(110, 21);
            this.numericUpDownPurchasePrice.TabIndex = 17;
            // 
            // labelCondition
            // 
            this.labelCondition.AutoSize = true;
            this.labelCondition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelCondition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.labelCondition.Location = new System.Drawing.Point(15, 250);
            this.labelCondition.Name = "labelCondition";
            this.labelCondition.Size = new System.Drawing.Size(62, 13);
            this.labelCondition.TabIndex = 18;
            this.labelCondition.Text = "Tình trạng:";
            // 
            // comboBoxCondition
            // 
            this.comboBoxCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCondition.FormattingEnabled = true;
            this.comboBoxCondition.Items.AddRange(new object[] {
            "Tốt",
            "Khá",
            "Trung bình",
            "Cần sửa chữa",
            "Hỏng"});
            this.comboBoxCondition.Location = new System.Drawing.Point(120, 247);
            this.comboBoxCondition.Name = "comboBoxCondition";
            this.comboBoxCondition.Size = new System.Drawing.Size(110, 23);
            this.comboBoxCondition.TabIndex = 19;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.labelDescription.Location = new System.Drawing.Point(15, 275);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(40, 13);
            this.labelDescription.TabIndex = 20;
            this.labelDescription.Text = "Mô tả:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(120, 272);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(240, 40);
            this.textBoxDescription.TabIndex = 21;
            // 
            // labelBranchId
            // 
            this.labelBranchId.AutoSize = true;
            this.labelBranchId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelBranchId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.labelBranchId.Location = new System.Drawing.Point(15, 320);
            this.labelBranchId.Name = "labelBranchId";
            this.labelBranchId.Size = new System.Drawing.Size(78, 13);
            this.labelBranchId.TabIndex = 22;
            this.labelBranchId.Text = "Mã chi nhánh:";
            // 
            // textBoxBranchId
            // 
            this.textBoxBranchId.Location = new System.Drawing.Point(120, 317);
            this.textBoxBranchId.Name = "textBoxBranchId";
            this.textBoxBranchId.Size = new System.Drawing.Size(110, 21);
            this.textBoxBranchId.TabIndex = 23;
            // 
            // labelSupplier
            // 
            this.labelSupplier.AutoSize = true;
            this.labelSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelSupplier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.labelSupplier.Location = new System.Drawing.Point(15, 345);
            this.labelSupplier.Name = "labelSupplier";
            this.labelSupplier.Size = new System.Drawing.Size(75, 13);
            this.labelSupplier.TabIndex = 24;
            this.labelSupplier.Text = "Nhà cung cấp:";
            // 
            // textBoxSupplier
            // 
            this.textBoxSupplier.Location = new System.Drawing.Point(120, 342);
            this.textBoxSupplier.Name = "textBoxSupplier";
            this.textBoxSupplier.Size = new System.Drawing.Size(240, 21);
            this.textBoxSupplier.TabIndex = 25;
            // 
            // labelPurchaseDate
            // 
            this.labelPurchaseDate.AutoSize = true;
            this.labelPurchaseDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelPurchaseDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.labelPurchaseDate.Location = new System.Drawing.Point(15, 370);
            this.labelPurchaseDate.Name = "labelPurchaseDate";
            this.labelPurchaseDate.Size = new System.Drawing.Size(61, 13);
            this.labelPurchaseDate.TabIndex = 26;
            this.labelPurchaseDate.Text = "Ngày mua:";
            // 
            // dateTimePickerPurchaseDate
            // 
            this.dateTimePickerPurchaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerPurchaseDate.Location = new System.Drawing.Point(120, 367);
            this.dateTimePickerPurchaseDate.Name = "dateTimePickerPurchaseDate";
            this.dateTimePickerPurchaseDate.Size = new System.Drawing.Size(110, 21);
            this.dateTimePickerPurchaseDate.TabIndex = 27;
            // 
            // labelWarrantyPeriod
            // 
            this.labelWarrantyPeriod.AutoSize = true;
            this.labelWarrantyPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelWarrantyPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.labelWarrantyPeriod.Location = new System.Drawing.Point(15, 395);
            this.labelWarrantyPeriod.Name = "labelWarrantyPeriod";
            this.labelWarrantyPeriod.Size = new System.Drawing.Size(101, 13);
            this.labelWarrantyPeriod.TabIndex = 28;
            this.labelWarrantyPeriod.Text = "Thời gian BH (tháng):";
            // 
            // numericUpDownWarrantyPeriod
            // 
            this.numericUpDownWarrantyPeriod.Location = new System.Drawing.Point(120, 392);
            this.numericUpDownWarrantyPeriod.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numericUpDownWarrantyPeriod.Name = "numericUpDownWarrantyPeriod";
            this.numericUpDownWarrantyPeriod.Size = new System.Drawing.Size(110, 21);
            this.numericUpDownWarrantyPeriod.TabIndex = 29;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.labelStatus.Location = new System.Drawing.Point(15, 420);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(58, 13);
            this.labelStatus.TabIndex = 30;
            this.labelStatus.Text = "Trạng thái:";
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Items.AddRange(new object[] {
            "Active",
            "Inactive"});
            this.comboBoxStatus.Location = new System.Drawing.Point(120, 417);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(110, 23);
            this.comboBoxStatus.TabIndex = 31;
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.Location = new System.Drawing.Point(20, 460);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(80, 35);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Thêm";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonEdit.ForeColor = System.Drawing.Color.White;
            this.buttonEdit.Location = new System.Drawing.Point(110, 460);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(80, 35);
            this.buttonEdit.TabIndex = 3;
            this.buttonEdit.Text = "Sửa";
            this.buttonEdit.UseVisualStyleBackColor = false;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonDelete.ForeColor = System.Drawing.Color.White;
            this.buttonDelete.Location = new System.Drawing.Point(200, 460);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(80, 35);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "Xóa";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.buttonSave.Enabled = false;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(290, 460);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(80, 35);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "Lưu";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.buttonCancel.Enabled = false;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.ForeColor = System.Drawing.Color.White;
            this.buttonCancel.Location = new System.Drawing.Point(380, 460);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 35);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Hủy";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonRefresh.ForeColor = System.Drawing.Color.White;
            this.buttonRefresh.Location = new System.Drawing.Point(470, 460);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(80, 35);
            this.buttonRefresh.TabIndex = 7;
            this.buttonRefresh.Text = "Làm mới";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(120, 100);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(300, 20);
            this.textBoxSearch.TabIndex = 8;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.labelSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.labelSearch.Location = new System.Drawing.Point(20, 103);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(66, 15);
            this.labelSearch.TabIndex = 9;
            this.labelSearch.Text = "Tìm kiếm:";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.panelHeader.Controls.Add(this.labelTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1200, 60);
            this.panelHeader.TabIndex = 10;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(15, 15);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(245, 29);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "QUẢN LÝ DỤNG CỤ";
            // 
            // DungCu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.groupBoxEquipmentInfo);
            this.Controls.Add(this.dataGridViewEquipments);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DungCu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Dụng Cụ";
            this.Load += new System.EventHandler(this.DungCu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEquipments)).EndInit();
            this.groupBoxEquipmentInfo.ResumeLayout(false);
            this.groupBoxEquipmentInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantityTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantityAvailable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRentalPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPurchasePrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWarrantyPeriod)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEquipments;
        private System.Windows.Forms.GroupBox groupBoxEquipmentInfo;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelCategory;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Label labelBrand;
        private System.Windows.Forms.TextBox textBoxBrand;
        private System.Windows.Forms.Label labelModel;
        private System.Windows.Forms.TextBox textBoxModel;
        private System.Windows.Forms.Label labelQuantityTotal;
        private System.Windows.Forms.NumericUpDown numericUpDownQuantityTotal;
        private System.Windows.Forms.Label labelQuantityAvailable;
        private System.Windows.Forms.NumericUpDown numericUpDownQuantityAvailable;
        private System.Windows.Forms.Label labelRentalPrice;
        private System.Windows.Forms.NumericUpDown numericUpDownRentalPrice;
        private System.Windows.Forms.Label labelPurchasePrice;
        private System.Windows.Forms.NumericUpDown numericUpDownPurchasePrice;
        private System.Windows.Forms.Label labelCondition;
        private System.Windows.Forms.ComboBox comboBoxCondition;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelBranchId;
        private System.Windows.Forms.TextBox textBoxBranchId;
        private System.Windows.Forms.Label labelSupplier;
        private System.Windows.Forms.TextBox textBoxSupplier;
        private System.Windows.Forms.Label labelPurchaseDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerPurchaseDate;
        private System.Windows.Forms.Label labelWarrantyPeriod;
        private System.Windows.Forms.NumericUpDown numericUpDownWarrantyPeriod;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;
    }
}