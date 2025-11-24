namespace CNXML_HVA
{
    partial class San
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblUserAvatar = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabFieldManagement = new System.Windows.Forms.TabPage();
            this.dgvFields = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNextBooking = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colSchedule = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cmbTypeFilter = new System.Windows.Forms.ComboBox();
            this.btnStatusFilter = new System.Windows.Forms.Button();
            this.btnAddField = new System.Windows.Forms.Button();
            this.btnImportXml = new System.Windows.Forms.Button();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.btnExportToSQL = new System.Windows.Forms.Button();
            this.btnImportFromSQL = new System.Windows.Forms.Button();
            this.pnlFieldDetails = new System.Windows.Forms.Panel();
            this.lblDetailTitle = new System.Windows.Forms.Label();
            this.lblIdLabel = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.lblNameLabel = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblTypeLabel = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblStatusLabel = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPriceLabel = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblDescriptionLabel = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnEditField = new System.Windows.Forms.Button();
            this.btnCloseDetail = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStatus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAvailable = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemBooked = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMaintenance = new System.Windows.Forms.ToolStripMenuItem();

            this.panelHeader.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabFieldManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFields)).BeginInit();
            this.panelToolbar.SuspendLayout();
            this.pnlFieldDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.SuspendLayout();
            this.contextMenuStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.panelHeader.Controls.Add(this.lblUserAvatar);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1300, 74);
            this.panelHeader.TabIndex = 0;
            // 
            // lblUserAvatar
            // 
            this.lblUserAvatar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserAvatar.AutoSize = true;
            this.lblUserAvatar.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblUserAvatar.ForeColor = System.Drawing.Color.White;
            this.lblUserAvatar.Location = new System.Drawing.Point(1273, 25);
            this.lblUserAvatar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserAvatar.Name = "lblUserAvatar";
            this.lblUserAvatar.Size = new System.Drawing.Size(136, 25);
            this.lblUserAvatar.TabIndex = 1;
            this.lblUserAvatar.Text = "👤 Admin User";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(27, 18);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(432, 41);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "⚽ HỆ THỐNG QUẢN LÝ SÂN";
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabFieldManagement);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabMain.Location = new System.Drawing.Point(0, 74);
            this.tabMain.Margin = new System.Windows.Forms.Padding(4);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1300, 626);
            this.tabMain.TabIndex = 1;
            // 
            // tabFieldManagement
            // 
            this.tabFieldManagement.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabFieldManagement.Controls.Add(this.dgvFields);
            this.tabFieldManagement.Controls.Add(this.panelToolbar);
            this.tabFieldManagement.Controls.Add(this.pnlFieldDetails);
            this.tabFieldManagement.Location = new System.Drawing.Point(4, 32);
            this.tabFieldManagement.Margin = new System.Windows.Forms.Padding(4);
            this.tabFieldManagement.Name = "tabFieldManagement";
            this.tabFieldManagement.Padding = new System.Windows.Forms.Padding(4);
            this.tabFieldManagement.Size = new System.Drawing.Size(1292, 590);
            this.tabFieldManagement.TabIndex = 0;
            this.tabFieldManagement.Text = "🏟️ Quản lý Sân";
            // 
            // dgvFields
            // 
            this.dgvFields.AllowUserToAddRows = false;
            this.dgvFields.AllowUserToDeleteRows = false;
            this.dgvFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFields.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFields.BackgroundColor = System.Drawing.Color.White;
            this.dgvFields.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFields.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFields.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFields.ColumnHeadersHeight = 40;
            this.dgvFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colName,
            this.colType,
            this.colStatus,
            this.colNextBooking,
            this.colEdit,
            this.colDelete,
            this.colSchedule});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(247)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFields.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvFields.EnableHeadersVisualStyles = false;
            this.dgvFields.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvFields.Location = new System.Drawing.Point(10, 150);
            this.dgvFields.Margin = new System.Windows.Forms.Padding(4);
            this.dgvFields.MultiSelect = false;
            this.dgvFields.Name = "dgvFields";
            this.dgvFields.ReadOnly = true;
            this.dgvFields.RowHeadersVisible = false;
            this.dgvFields.RowHeadersWidth = 51;
            this.dgvFields.RowTemplate.Height = 40;
            this.dgvFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFields.Size = new System.Drawing.Size(900, 410);
            this.dgvFields.TabIndex = 1;
            this.dgvFields.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFields_CellContentClick);
            this.dgvFields.SelectionChanged += new System.EventHandler(this.dgvFields_SelectionChanged);
            // 
            // colId
            // 
            this.colId.DataPropertyName = "Id";
            this.colId.FillWeight = 80F;
            this.colId.HeaderText = "Mã sân";
            this.colId.MinimumWidth = 6;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.FillWeight = 150F;
            this.colName.HeaderText = "Tên sân";
            this.colName.MinimumWidth = 6;
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colType
            // 
            this.colType.DataPropertyName = "TypeName";
            this.colType.HeaderText = "Loại sân";
            this.colType.MinimumWidth = 6;
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "Status";
            this.colStatus.HeaderText = "Trạng thái";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colNextBooking
            // 
            this.colNextBooking.DataPropertyName = "NextBooking";
            this.colNextBooking.HeaderText = "Lịch tiếp theo";
            this.colNextBooking.MinimumWidth = 6;
            this.colNextBooking.Name = "colNextBooking";
            this.colNextBooking.ReadOnly = true;
            // 
            // colEdit
            // 
            this.colEdit.FillWeight = 70F;
            this.colEdit.HeaderText = "";
            this.colEdit.MinimumWidth = 6;
            this.colEdit.Name = "colEdit";
            this.colEdit.ReadOnly = true;
            this.colEdit.Text = "✏️ Sửa";
            this.colEdit.UseColumnTextForButtonValue = true;
            // 
            // colDelete
            // 
            this.colDelete.FillWeight = 70F;
            this.colDelete.HeaderText = "";
            this.colDelete.MinimumWidth = 6;
            this.colDelete.Name = "colDelete";
            this.colDelete.ReadOnly = true;
            this.colDelete.Text = "🗑️ Xóa";
            this.colDelete.UseColumnTextForButtonValue = true;
            // 
            // colSchedule
            // 
            this.colSchedule.FillWeight = 70F;
            this.colSchedule.HeaderText = "";
            this.colSchedule.MinimumWidth = 6;
            this.colSchedule.Name = "colSchedule";
            this.colSchedule.ReadOnly = true;
            this.colSchedule.Text = "📅 Lịch";
            this.colSchedule.UseColumnTextForButtonValue = true;
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.White;
            this.panelToolbar.Controls.Add(this.lblSearch);
            this.panelToolbar.Controls.Add(this.txtSearch);
            this.panelToolbar.Controls.Add(this.cmbTypeFilter);
            this.panelToolbar.Controls.Add(this.btnStatusFilter);
            this.panelToolbar.Controls.Add(this.btnExportToSQL);
            this.panelToolbar.Controls.Add(this.btnImportFromSQL);
            this.panelToolbar.Controls.Add(this.btnAddField);
            this.panelToolbar.Controls.Add(this.btnImportXml);
            this.panelToolbar.Controls.Add(this.btnExportCsv);
            this.panelToolbar.Location = new System.Drawing.Point(10, 10);
            this.panelToolbar.Margin = new System.Windows.Forms.Padding(4);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.panelToolbar.Size = new System.Drawing.Size(900, 135);
            this.panelToolbar.TabIndex = 0;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.lblSearch.Location = new System.Drawing.Point(13, 15);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(95, 20);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "🔍 Tìm kiếm";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(13, 42);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(280, 30);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.Text = "Tìm theo tên, mã sân...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // cmbTypeFilter
            // 
            this.cmbTypeFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTypeFilter.FormattingEnabled = true;
            this.cmbTypeFilter.Location = new System.Drawing.Point(307, 42);
            this.cmbTypeFilter.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTypeFilter.Name = "cmbTypeFilter";
            this.cmbTypeFilter.Size = new System.Drawing.Size(220, 31);
            this.cmbTypeFilter.TabIndex = 2;
            this.cmbTypeFilter.SelectedIndexChanged += new System.EventHandler(this.cmbTypeFilter_SelectedIndexChanged);
            // 
            // btnStatusFilter
            // 
            this.btnStatusFilter.BackColor = System.Drawing.Color.White;
            this.btnStatusFilter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.btnStatusFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatusFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnStatusFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.btnStatusFilter.Location = new System.Drawing.Point(540, 41);
            this.btnStatusFilter.Margin = new System.Windows.Forms.Padding(4);
            this.btnStatusFilter.Name = "btnStatusFilter";
            this.btnStatusFilter.Size = new System.Drawing.Size(160, 33);
            this.btnStatusFilter.TabIndex = 3;
            this.btnStatusFilter.Text = "📊 Trạng thái ▼";
            this.btnStatusFilter.UseVisualStyleBackColor = false;
            this.btnStatusFilter.Click += new System.EventHandler(this.btnStatusFilter_Click);
            // 
            // btnAddField
            // 
            this.btnAddField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.btnAddField.FlatAppearance.BorderSize = 0;
            this.btnAddField.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddField.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddField.ForeColor = System.Drawing.Color.White;
            this.btnAddField.Location = new System.Drawing.Point(8, 86);
            this.btnAddField.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddField.Name = "btnAddField";
            this.btnAddField.Size = new System.Drawing.Size(140, 38);
            this.btnAddField.TabIndex = 4;
            this.btnAddField.Text = "➕ Thêm sân";
            this.btnAddField.UseVisualStyleBackColor = false;
            this.btnAddField.Click += new System.EventHandler(this.btnAddField_Click);
            // 
            // btnImportXml
            // 
            this.btnImportXml.BackColor = System.Drawing.Color.White;
            this.btnImportXml.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.btnImportXml.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportXml.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnImportXml.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.btnImportXml.Location = new System.Drawing.Point(163, 85);
            this.btnImportXml.Margin = new System.Windows.Forms.Padding(4);
            this.btnImportXml.Name = "btnImportXml";
            this.btnImportXml.Size = new System.Drawing.Size(140, 38);
            this.btnImportXml.TabIndex = 5;
            this.btnImportXml.Text = "📥 Import XML";
            this.btnImportXml.UseVisualStyleBackColor = false;
            this.btnImportXml.Click += new System.EventHandler(this.btnImportXml_Click);
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.BackColor = System.Drawing.Color.White;
            this.btnExportCsv.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.btnExportCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportCsv.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExportCsv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.btnExportCsv.Location = new System.Drawing.Point(313, 85);
            this.btnExportCsv.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(140, 38);
            this.btnExportCsv.TabIndex = 6;
            this.btnExportCsv.Text = "📤 Export CSV";
            this.btnExportCsv.UseVisualStyleBackColor = false;
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
            // 
            // btnExportToSQL
            // 
            this.btnExportToSQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.btnExportToSQL.FlatAppearance.BorderSize = 0;
            this.btnExportToSQL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportToSQL.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExportToSQL.ForeColor = System.Drawing.Color.White;
            this.btnExportToSQL.Location = new System.Drawing.Point(463, 85);
            this.btnExportToSQL.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportToSQL.Name = "btnExportToSQL";
            this.btnExportToSQL.Size = new System.Drawing.Size(140, 38);
            this.btnExportToSQL.TabIndex = 7;
            this.btnExportToSQL.Text = "📤 Export SQL";
            this.btnExportToSQL.UseVisualStyleBackColor = false;
            this.btnExportToSQL.Click += new System.EventHandler(this.btnExportToSQL_Click);
            // 
            // btnImportFromSQL
            // 
            this.btnImportFromSQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnImportFromSQL.FlatAppearance.BorderSize = 0;
            this.btnImportFromSQL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportFromSQL.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnImportFromSQL.ForeColor = System.Drawing.Color.White;
            this.btnImportFromSQL.Location = new System.Drawing.Point(613, 85);
            this.btnImportFromSQL.Margin = new System.Windows.Forms.Padding(4);
            this.btnImportFromSQL.Name = "btnImportFromSQL";
            this.btnImportFromSQL.Size = new System.Drawing.Size(140, 38);
            this.btnImportFromSQL.TabIndex = 8;
            this.btnImportFromSQL.Text = "📥 Import SQL";
            this.btnImportFromSQL.UseVisualStyleBackColor = false;
            this.btnImportFromSQL.Click += new System.EventHandler(this.btnImportFromSQL_Click);
            // 
            // pnlFieldDetails
            // 
            this.pnlFieldDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFieldDetails.BackColor = System.Drawing.Color.White;
            this.pnlFieldDetails.Controls.Add(this.lblDetailTitle);
            this.pnlFieldDetails.Controls.Add(this.lblIdLabel);
            this.pnlFieldDetails.Controls.Add(this.lblId);
            this.pnlFieldDetails.Controls.Add(this.lblNameLabel);
            this.pnlFieldDetails.Controls.Add(this.lblName);
            this.pnlFieldDetails.Controls.Add(this.lblTypeLabel);
            this.pnlFieldDetails.Controls.Add(this.lblType);
            this.pnlFieldDetails.Controls.Add(this.lblStatusLabel);
            this.pnlFieldDetails.Controls.Add(this.lblStatus);
            this.pnlFieldDetails.Controls.Add(this.lblPriceLabel);
            this.pnlFieldDetails.Controls.Add(this.lblPrice);
            this.pnlFieldDetails.Controls.Add(this.lblDescriptionLabel);
            this.pnlFieldDetails.Controls.Add(this.txtDescription);
            this.pnlFieldDetails.Controls.Add(this.btnEditField);
            this.pnlFieldDetails.Controls.Add(this.btnCloseDetail);
            this.pnlFieldDetails.Location = new System.Drawing.Point(920, 10);
            this.pnlFieldDetails.Margin = new System.Windows.Forms.Padding(4);
            this.pnlFieldDetails.Name = "pnlFieldDetails";
            this.pnlFieldDetails.Padding = new System.Windows.Forms.Padding(20, 18, 20, 18);
            this.pnlFieldDetails.Size = new System.Drawing.Size(350, 550);
            this.pnlFieldDetails.TabIndex = 2;
            this.pnlFieldDetails.Visible = false;
            this.pnlFieldDetails.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlFieldDetails_Paint);
            // 
            // lblDetailTitle
            // 
            this.lblDetailTitle.AutoSize = true;
            this.lblDetailTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblDetailTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.lblDetailTitle.Location = new System.Drawing.Point(24, 22);
            this.lblDetailTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.Size = new System.Drawing.Size(182, 32);
            this.lblDetailTitle.TabIndex = 0;
            this.lblDetailTitle.Text = "📋 Chi tiết sân";
            // 
            // lblIdLabel
            // 
            this.lblIdLabel.AutoSize = true;
            this.lblIdLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblIdLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.lblIdLabel.Location = new System.Drawing.Point(31, 283);
            this.lblIdLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIdLabel.Name = "lblIdLabel";
            this.lblIdLabel.Size = new System.Drawing.Size(63, 20);
            this.lblIdLabel.TabIndex = 2;
            this.lblIdLabel.Text = "Mã sân:";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.lblId.Location = new System.Drawing.Point(31, 308);
            this.lblId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(17, 23);
            this.lblId.TabIndex = 3;
            this.lblId.Text = "-";
            // 
            // lblNameLabel
            // 
            this.lblNameLabel.AutoSize = true;
            this.lblNameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.lblNameLabel.Location = new System.Drawing.Point(31, 338);
            this.lblNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNameLabel.Name = "lblNameLabel";
            this.lblNameLabel.Size = new System.Drawing.Size(66, 20);
            this.lblNameLabel.TabIndex = 4;
            this.lblNameLabel.Text = "Tên sân:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.lblName.Location = new System.Drawing.Point(31, 363);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(17, 23);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "-";
            // 
            // lblTypeLabel
            // 
            this.lblTypeLabel.AutoSize = true;
            this.lblTypeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTypeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.lblTypeLabel.Location = new System.Drawing.Point(31, 394);
            this.lblTypeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTypeLabel.Name = "lblTypeLabel";
            this.lblTypeLabel.Size = new System.Drawing.Size(70, 20);
            this.lblTypeLabel.TabIndex = 6;
            this.lblTypeLabel.Text = "Loại sân:";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.lblType.Location = new System.Drawing.Point(31, 418);
            this.lblType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(17, 23);
            this.lblType.TabIndex = 7;
            this.lblType.Text = "-";
            // 
            // lblStatusLabel
            // 
            this.lblStatusLabel.AutoSize = true;
            this.lblStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.lblStatusLabel.Location = new System.Drawing.Point(31, 449);
            this.lblStatusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatusLabel.Name = "lblStatusLabel";
            this.lblStatusLabel.Size = new System.Drawing.Size(84, 20);
            this.lblStatusLabel.TabIndex = 8;
            this.lblStatusLabel.Text = "Trạng thái:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.lblStatus.Location = new System.Drawing.Point(31, 474);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(17, 23);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "-";
            // 
            // lblPriceLabel
            // 
            this.lblPriceLabel.AutoSize = true;
            this.lblPriceLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPriceLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.lblPriceLabel.Location = new System.Drawing.Point(31, 505);
            this.lblPriceLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPriceLabel.Name = "lblPriceLabel";
            this.lblPriceLabel.Size = new System.Drawing.Size(101, 20);
            this.lblPriceLabel.TabIndex = 10;
            this.lblPriceLabel.Text = "Giá thuê/giờ:";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.lblPrice.Location = new System.Drawing.Point(31, 529);
            this.lblPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(17, 23);
            this.lblPrice.TabIndex = 11;
            this.lblPrice.Text = "-";
            // 
            // lblDescriptionLabel
            // 
            this.lblDescriptionLabel.AutoSize = true;
            this.lblDescriptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.lblDescriptionLabel.Location = new System.Drawing.Point(31, 560);
            this.lblDescriptionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescriptionLabel.Name = "lblDescriptionLabel";
            this.lblDescriptionLabel.Size = new System.Drawing.Size(54, 20);
            this.lblDescriptionLabel.TabIndex = 12;
            this.lblDescriptionLabel.Text = "Mô tả:";
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDescription.Location = new System.Drawing.Point(31, 585);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(285, 67);
            this.txtDescription.TabIndex = 13;
            // 
            // btnEditField
            // 
            this.btnEditField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.btnEditField.FlatAppearance.BorderSize = 0;
            this.btnEditField.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditField.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEditField.ForeColor = System.Drawing.Color.White;
            this.btnEditField.Location = new System.Drawing.Point(31, 665);
            this.btnEditField.Margin = new System.Windows.Forms.Padding(4);
            this.btnEditField.Name = "btnEditField";
            this.btnEditField.Size = new System.Drawing.Size(285, 38);
            this.btnEditField.TabIndex = 14;
            this.btnEditField.Text = "✏️ Chỉnh sửa";
            this.btnEditField.UseVisualStyleBackColor = false;
            this.btnEditField.Click += new System.EventHandler(this.btnEditField_Click);
            // 
            // btnCloseDetail
            // 
            this.btnCloseDetail.BackColor = System.Drawing.Color.White;
            this.btnCloseDetail.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.btnCloseDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseDetail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCloseDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.btnCloseDetail.Location = new System.Drawing.Point(35, 140);
            this.btnCloseDetail.Margin = new System.Windows.Forms.Padding(4);
            this.btnCloseDetail.Name = "btnCloseDetail";
            this.btnCloseDetail.Size = new System.Drawing.Size(235, 38);
            this.btnCloseDetail.TabIndex = 15;
            this.btnCloseDetail.Text = "✖️ Đóng";
            this.btnCloseDetail.UseVisualStyleBackColor = false;
            this.btnCloseDetail.Click += new System.EventHandler(this.btnCloseDetail_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Size = new System.Drawing.Size(150, 100);
            this.splitContainer.TabIndex = 0;
            // 
            // panelLeft
            // 
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(200, 100);
            this.panelLeft.TabIndex = 0;
            // 
            // contextMenuStatus
            // 
            this.contextMenuStatus.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAll,
            this.menuItemAvailable,
            this.menuItemBooked,
            this.menuItemMaintenance});
            this.contextMenuStatus.Name = "contextMenuStatus";
            this.contextMenuStatus.Size = new System.Drawing.Size(222, 100);
            // 
            // menuItemAll
            // 
            this.menuItemAll.Name = "menuItemAll";
            this.menuItemAll.Size = new System.Drawing.Size(221, 24);
            this.menuItemAll.Text = "Tất cả";
            this.menuItemAll.Click += new System.EventHandler(this.menuItemStatus_Click);
            // 
            // menuItemAvailable
            // 
            this.menuItemAvailable.Name = "menuItemAvailable";
            this.menuItemAvailable.Size = new System.Drawing.Size(221, 24);
            this.menuItemAvailable.Text = "Available - Sẵn sàng";
            this.menuItemAvailable.Click += new System.EventHandler(this.menuItemStatus_Click);
            // 
            // menuItemBooked
            // 
            this.menuItemBooked.Name = "menuItemBooked";
            this.menuItemBooked.Size = new System.Drawing.Size(221, 24);
            this.menuItemBooked.Text = "Booked - Đã đặt";
            this.menuItemBooked.Click += new System.EventHandler(this.menuItemStatus_Click);
            // 
            // menuItemMaintenance
            // 
            this.menuItemMaintenance.Name = "menuItemMaintenance";
            this.menuItemMaintenance.Size = new System.Drawing.Size(221, 24);
            this.menuItemMaintenance.Text = "Maintenance - Bảo trì";
            this.menuItemMaintenance.Click += new System.EventHandler(this.menuItemStatus_Click);
            // 
            // San
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1300, 700);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.panelHeader);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "San";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Sân Bóng";
            this.Load += new System.EventHandler(this.San_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.San_KeyDown);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabFieldManagement.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFields)).EndInit();
            this.panelToolbar.ResumeLayout(false);
            this.panelToolbar.PerformLayout();
            this.pnlFieldDetails.ResumeLayout(false);
            this.pnlFieldDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.contextMenuStatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUserAvatar;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabFieldManagement;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbTypeFilter;
        private System.Windows.Forms.Button btnStatusFilter;
        private System.Windows.Forms.Button btnAddField;
        private System.Windows.Forms.Button btnImportXml;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.DataGridView dgvFields;
        private System.Windows.Forms.Panel pnlFieldDetails;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.Label lblIdLabel;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblNameLabel;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblTypeLabel;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblStatusLabel;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPriceLabel;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblDescriptionLabel;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnEditField;
        private System.Windows.Forms.Button btnCloseDetail;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ContextMenuStrip contextMenuStatus;
        private System.Windows.Forms.ToolStripMenuItem menuItemAvailable;
        private System.Windows.Forms.ToolStripMenuItem menuItemBooked;
        private System.Windows.Forms.ToolStripMenuItem menuItemMaintenance;
        private System.Windows.Forms.ToolStripMenuItem menuItemAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNextBooking;
        private System.Windows.Forms.DataGridViewButtonColumn colEdit;
        private System.Windows.Forms.DataGridViewButtonColumn colDelete;
        private System.Windows.Forms.DataGridViewButtonColumn colSchedule;
        private System.Windows.Forms.Button btnExportToSQL;
        private System.Windows.Forms.Button btnImportFromSQL;
    }
}