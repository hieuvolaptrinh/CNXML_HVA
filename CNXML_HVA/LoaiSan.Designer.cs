﻿namespace CNXML_HVA
{
    partial class LoaiSan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabFieldTypes = new System.Windows.Forms.TabPage();
            this.panelMain = new System.Windows.Forms.Panel();
            this.dgvFieldTypes = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCapacity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDefaultPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSurfaceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.txtTypeSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.btnDeleteType = new System.Windows.Forms.Button();
            this.btnEditType = new System.Windows.Forms.Button();
            this.btnAddType = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panelHeader.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabFieldTypes.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFieldTypes)).BeginInit();
            this.panelToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.panelHeader.Controls.Add(this.lblUserInfo);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1200, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(310, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "⚽ QUẢN LÝ LOẠI SÂN";
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserInfo.AutoSize = true;
            this.lblUserInfo.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblUserInfo.ForeColor = System.Drawing.Color.White;
            this.lblUserInfo.Location = new System.Drawing.Point(1030, 20);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(150, 20);
            this.lblUserInfo.TabIndex = 1;
            this.lblUserInfo.Text = "👤 Admin User";
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabFieldTypes);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabMain.Location = new System.Drawing.Point(0, 60);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1200, 640);
            this.tabMain.TabIndex = 1;
            // 
            // tabFieldTypes
            // 
            this.tabFieldTypes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(247)))));
            this.tabFieldTypes.Controls.Add(this.panelMain);
            this.tabFieldTypes.Location = new System.Drawing.Point(4, 26);
            this.tabFieldTypes.Name = "tabFieldTypes";
            this.tabFieldTypes.Padding = new System.Windows.Forms.Padding(3);
            this.tabFieldTypes.Size = new System.Drawing.Size(1192, 610);
            this.tabFieldTypes.TabIndex = 0;
            this.tabFieldTypes.Text = "📋 Quản lý Loại Sân";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.dgvFieldTypes);
            this.panelMain.Controls.Add(this.panelToolbar);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(3, 3);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1186, 604);
            this.panelMain.TabIndex = 0;
            // 
            // dgvFieldTypes
            // 
            this.dgvFieldTypes.AllowUserToAddRows = false;
            this.dgvFieldTypes.AllowUserToDeleteRows = false;
            this.dgvFieldTypes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFieldTypes.BackgroundColor = System.Drawing.Color.White;
            this.dgvFieldTypes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFieldTypes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFieldTypes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFieldTypes.ColumnHeadersHeight = 40;
            this.dgvFieldTypes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colName,
            this.colDescription,
            this.colCapacity,
            this.colDefaultPrice,
            this.colSurfaceType,
            this.colStatus});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(247)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFieldTypes.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFieldTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFieldTypes.EnableHeadersVisualStyles = false;
            this.dgvFieldTypes.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvFieldTypes.Location = new System.Drawing.Point(0, 70);
            this.dgvFieldTypes.MultiSelect = false;
            this.dgvFieldTypes.Name = "dgvFieldTypes";
            this.dgvFieldTypes.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFieldTypes.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFieldTypes.RowHeadersVisible = false;
            this.dgvFieldTypes.RowTemplate.Height = 40;
            this.dgvFieldTypes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFieldTypes.Size = new System.Drawing.Size(1186, 534);
            this.dgvFieldTypes.TabIndex = 1;
            this.dgvFieldTypes.SelectionChanged += new System.EventHandler(this.dgvFieldTypes_SelectionChanged);
            this.dgvFieldTypes.DoubleClick += new System.EventHandler(this.dgvFieldTypes_DoubleClick);
            // 
            // colId
            // 
            this.colId.DataPropertyName = "Id";
            this.colId.FillWeight = 80F;
            this.colId.HeaderText = "Mã Loại";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.FillWeight = 120F;
            this.colName.HeaderText = "Tên Loại";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colDescription
            // 
            this.colDescription.DataPropertyName = "Description";
            this.colDescription.FillWeight = 180F;
            this.colDescription.HeaderText = "Mô Tả";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            // 
            // colCapacity
            // 
            this.colCapacity.DataPropertyName = "TotalCapacity";
            this.colCapacity.HeaderText = "Sức Chứa";
            this.colCapacity.Name = "colCapacity";
            this.colCapacity.ReadOnly = true;
            // 
            // colDefaultPrice
            // 
            this.colDefaultPrice.DataPropertyName = "BasePrice";
            this.colDefaultPrice.HeaderText = "Giá Mặc Định";
            this.colDefaultPrice.Name = "colDefaultPrice";
            this.colDefaultPrice.ReadOnly = true;
            // 
            // colSurfaceType
            // 
            this.colSurfaceType.DataPropertyName = "SurfaceType";
            this.colSurfaceType.HeaderText = "Loại Bề Mặt";
            this.colSurfaceType.Name = "colSurfaceType";
            this.colSurfaceType.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "Status";
            this.colStatus.HeaderText = "Trạng Thái";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // panelToolbar
            // 
            this.panelToolbar.BackColor = System.Drawing.Color.White;
            this.panelToolbar.Controls.Add(this.btnAddType);
            this.panelToolbar.Controls.Add(this.btnEditType);
            this.panelToolbar.Controls.Add(this.btnDeleteType);
            this.panelToolbar.Controls.Add(this.lblSearch);
            this.panelToolbar.Controls.Add(this.txtTypeSearch);
            this.panelToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelToolbar.Location = new System.Drawing.Point(0, 0);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.panelToolbar.Size = new System.Drawing.Size(1186, 70);
            this.panelToolbar.TabIndex = 0;
            // 
            // txtTypeSearch
            // 
            this.txtTypeSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTypeSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtTypeSearch.Location = new System.Drawing.Point(18, 33);
            this.txtTypeSearch.Name = "txtTypeSearch";
            this.txtTypeSearch.Size = new System.Drawing.Size(300, 25);
            this.txtTypeSearch.TabIndex = 1;
            this.txtTypeSearch.Text = "Tìm loại sân...";
            this.txtTypeSearch.Enter += new System.EventHandler(this.txtTypeSearch_Enter);
            this.txtTypeSearch.Leave += new System.EventHandler(this.txtTypeSearch_Leave);
            this.txtTypeSearch.TextChanged += new System.EventHandler(this.txtTypeSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.lblSearch.Location = new System.Drawing.Point(18, 13);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(62, 15);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "🔍 Tìm kiếm";
            // 
            // btnAddType
            // 
            this.btnAddType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.btnAddType.FlatAppearance.BorderSize = 0;
            this.btnAddType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddType.ForeColor = System.Drawing.Color.White;
            this.btnAddType.Location = new System.Drawing.Point(854, 23);
            this.btnAddType.Name = "btnAddType";
            this.btnAddType.Size = new System.Drawing.Size(100, 35);
            this.btnAddType.TabIndex = 2;
            this.btnAddType.Text = "➕ Thêm";
            this.btnAddType.UseVisualStyleBackColor = false;
            this.btnAddType.Click += new System.EventHandler(this.btnAddType_Click);
            // 
            // btnEditType
            // 
            this.btnEditType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditType.BackColor = System.Drawing.Color.White;
            this.btnEditType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(140)))), ((int)(((byte)(58)))));
            this.btnEditType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEditType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.btnEditType.Location = new System.Drawing.Point(966, 23);
            this.btnEditType.Name = "btnEditType";
            this.btnEditType.Size = new System.Drawing.Size(100, 35);
            this.btnEditType.TabIndex = 3;
            this.btnEditType.Text = "✏️ Sửa";
            this.btnEditType.UseVisualStyleBackColor = false;
            this.btnEditType.Click += new System.EventHandler(this.btnEditType_Click);
            // 
            // btnDeleteType
            // 
            this.btnDeleteType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteType.BackColor = System.Drawing.Color.White;
            this.btnDeleteType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDeleteType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDeleteType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDeleteType.Location = new System.Drawing.Point(1078, 23);
            this.btnDeleteType.Name = "btnDeleteType";
            this.btnDeleteType.Size = new System.Drawing.Size(100, 35);
            this.btnDeleteType.TabIndex = 4;
            this.btnDeleteType.Text = "🗑️ Xóa";
            this.btnDeleteType.UseVisualStyleBackColor = false;
            this.btnDeleteType.Click += new System.EventHandler(this.btnDeleteType_Click);
            // 
            // LoaiSan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.panelHeader);
            this.KeyPreview = true;
            this.Name = "LoaiSan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Loại Sân";
            this.Load += new System.EventHandler(this.LoaiSan_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoaiSan_KeyDown);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabFieldTypes.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFieldTypes)).EndInit();
            this.panelToolbar.ResumeLayout(false);
            this.panelToolbar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabFieldTypes;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.DataGridView dgvFieldTypes;
        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Button btnAddType;
        private System.Windows.Forms.Button btnEditType;
        private System.Windows.Forms.Button btnDeleteType;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtTypeSearch;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCapacity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDefaultPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSurfaceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
    }
}