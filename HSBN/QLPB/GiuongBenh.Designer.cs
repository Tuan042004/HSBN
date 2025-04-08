namespace QLBN
{
    partial class GiuongBenh
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnTK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBack_Tk = new System.Windows.Forms.Button();
            this.cbott = new System.Windows.Forms.ComboBox();
            this.cbomp = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtmg = new System.Windows.Forms.TextBox();
            this.dgvG = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnluu = new System.Windows.Forms.Button();
            this.btnsua = new System.Windows.Forms.Button();
            this.btnxoa = new System.Windows.Forms.Button();
            this.btnBackCell = new System.Windows.Forms.Button();
            this.ThemMoi = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnthoat = new System.Windows.Forms.Button();
            this.btnXuat = new System.Windows.Forms.Button();
            this.btnNhap = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvG)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(370, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "THÔNG TIN GIƯỜNG BỆNH";
            // 
            // btnTK
            // 
            this.btnTK.Location = new System.Drawing.Point(698, 81);
            this.btnTK.Name = "btnTK";
            this.btnTK.Size = new System.Drawing.Size(96, 29);
            this.btnTK.TabIndex = 8;
            this.btnTK.Text = "Tìm kiếm";
            this.btnTK.UseVisualStyleBackColor = true;
            this.btnTK.Click += new System.EventHandler(this.btnTK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBack_Tk);
            this.groupBox2.Controls.Add(this.btnTK);
            this.groupBox2.Controls.Add(this.cbott);
            this.groupBox2.Controls.Add(this.cbomp);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtmg);
            this.groupBox2.Location = new System.Drawing.Point(55, 67);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(827, 128);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cập nhật thông tin";
            // 
            // btnBack_Tk
            // 
            this.btnBack_Tk.Location = new System.Drawing.Point(584, 81);
            this.btnBack_Tk.Name = "btnBack_Tk";
            this.btnBack_Tk.Size = new System.Drawing.Size(96, 29);
            this.btnBack_Tk.TabIndex = 9;
            this.btnBack_Tk.Text = "Tro ve";
            this.btnBack_Tk.UseVisualStyleBackColor = true;
            this.btnBack_Tk.Click += new System.EventHandler(this.btnBack_Tk_Click);
            // 
            // cbott
            // 
            this.cbott.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbott.FormattingEnabled = true;
            this.cbott.Items.AddRange(new object[] {
            "-- Chọn trạng thái --",
            "Đã có người",
            "Trống"});
            this.cbott.Location = new System.Drawing.Point(535, 21);
            this.cbott.Name = "cbott";
            this.cbott.Size = new System.Drawing.Size(259, 28);
            this.cbott.TabIndex = 5;
            // 
            // cbomp
            // 
            this.cbomp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbomp.FormattingEnabled = true;
            this.cbomp.Location = new System.Drawing.Point(138, 76);
            this.cbomp.Name = "cbomp";
            this.cbomp.Size = new System.Drawing.Size(259, 28);
            this.cbomp.TabIndex = 4;
            this.cbomp.SelectedIndexChanged += new System.EventHandler(this.cbomp_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(445, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Trạng thái:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mã phòng:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã giường:";
            // 
            // txtmg
            // 
            this.txtmg.Location = new System.Drawing.Point(138, 26);
            this.txtmg.Name = "txtmg";
            this.txtmg.Size = new System.Drawing.Size(259, 26);
            this.txtmg.TabIndex = 0;
            this.txtmg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtmg_KeyPress);
            // 
            // dgvG
            // 
            this.dgvG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvG.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgvG.Location = new System.Drawing.Point(55, 225);
            this.dgvG.Name = "dgvG";
            this.dgvG.RowHeadersWidth = 62;
            this.dgvG.RowTemplate.Height = 28;
            this.dgvG.Size = new System.Drawing.Size(827, 238);
            this.dgvG.TabIndex = 12;
            this.dgvG.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvG_CellClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "MaGiuong";
            this.Column1.HeaderText = "Mã giường";
            this.Column1.MinimumWidth = 8;
            this.Column1.Name = "Column1";
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "MaPhong";
            this.Column2.HeaderText = "Mã phòng";
            this.Column2.MinimumWidth = 8;
            this.Column2.Name = "Column2";
            this.Column2.Width = 150;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "TrangThai";
            this.Column3.HeaderText = "Trạng thái";
            this.Column3.MinimumWidth = 8;
            this.Column3.Name = "Column3";
            this.Column3.Width = 150;
            // 
            // btnluu
            // 
            this.btnluu.Location = new System.Drawing.Point(172, 488);
            this.btnluu.Name = "btnluu";
            this.btnluu.Size = new System.Drawing.Size(75, 31);
            this.btnluu.TabIndex = 5;
            this.btnluu.Text = "Lưu";
            this.btnluu.UseVisualStyleBackColor = true;
            this.btnluu.Click += new System.EventHandler(this.btnluu_Click);
            // 
            // btnsua
            // 
            this.btnsua.Location = new System.Drawing.Point(423, 488);
            this.btnsua.Name = "btnsua";
            this.btnsua.Size = new System.Drawing.Size(84, 31);
            this.btnsua.TabIndex = 7;
            this.btnsua.Text = "Cập nhật";
            this.btnsua.UseVisualStyleBackColor = true;
            this.btnsua.Click += new System.EventHandler(this.btnsua_Click);
            // 
            // btnxoa
            // 
            this.btnxoa.Location = new System.Drawing.Point(525, 488);
            this.btnxoa.Name = "btnxoa";
            this.btnxoa.Size = new System.Drawing.Size(75, 31);
            this.btnxoa.TabIndex = 9;
            this.btnxoa.Text = "Xóa";
            this.btnxoa.UseVisualStyleBackColor = true;
            this.btnxoa.Click += new System.EventHandler(this.btnxoa_Click);
            // 
            // btnBackCell
            // 
            this.btnBackCell.Location = new System.Drawing.Point(327, 488);
            this.btnBackCell.Name = "btnBackCell";
            this.btnBackCell.Size = new System.Drawing.Size(75, 31);
            this.btnBackCell.TabIndex = 15;
            this.btnBackCell.Text = "Back";
            this.btnBackCell.UseVisualStyleBackColor = true;
            this.btnBackCell.Click += new System.EventHandler(this.btnBackCell_Click);
            // 
            // ThemMoi
            // 
            this.ThemMoi.Location = new System.Drawing.Point(55, 488);
            this.ThemMoi.Name = "ThemMoi";
            this.ThemMoi.Size = new System.Drawing.Size(120, 35);
            this.ThemMoi.TabIndex = 13;
            this.ThemMoi.Text = "Thêm mới";
            this.ThemMoi.UseVisualStyleBackColor = true;
            this.ThemMoi.Click += new System.EventHandler(this.ThemMoi_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(79, 488);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 31);
            this.btnBack.TabIndex = 14;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnthoat
            // 
            this.btnthoat.Location = new System.Drawing.Point(807, 488);
            this.btnthoat.Name = "btnthoat";
            this.btnthoat.Size = new System.Drawing.Size(75, 31);
            this.btnthoat.TabIndex = 11;
            this.btnthoat.Text = "Thoát";
            this.btnthoat.UseVisualStyleBackColor = true;
            this.btnthoat.Click += new System.EventHandler(this.btnthoat_Click);
            // 
            // btnXuat
            // 
            this.btnXuat.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuat.Location = new System.Drawing.Point(193, 488);
            this.btnXuat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnXuat.Name = "btnXuat";
            this.btnXuat.Size = new System.Drawing.Size(95, 35);
            this.btnXuat.TabIndex = 38;
            this.btnXuat.Text = "Xuất file";
            this.btnXuat.UseVisualStyleBackColor = true;
            this.btnXuat.Click += new System.EventHandler(this.btnXuat_Click);
            // 
            // btnNhap
            // 
            this.btnNhap.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNhap.Location = new System.Drawing.Point(307, 489);
            this.btnNhap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnNhap.Name = "btnNhap";
            this.btnNhap.Size = new System.Drawing.Size(95, 32);
            this.btnNhap.TabIndex = 39;
            this.btnNhap.Text = "Nhập file";
            this.btnNhap.UseVisualStyleBackColor = true;
            this.btnNhap.Click += new System.EventHandler(this.btnNhap_Click);
            // 
            // GiuongBenh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 562);
            this.Controls.Add(this.btnNhap);
            this.Controls.Add(this.btnXuat);
            this.Controls.Add(this.btnBackCell);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.ThemMoi);
            this.Controls.Add(this.dgvG);
            this.Controls.Add(this.btnthoat);
            this.Controls.Add(this.btnxoa);
            this.Controls.Add(this.btnsua);
            this.Controls.Add(this.btnluu);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Name = "GiuongBenh";
            this.Text = "GiuongBenh";
            this.Load += new System.EventHandler(this.GiuongBenh_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvG)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbott;
        private System.Windows.Forms.ComboBox cbomp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtmg;
        private System.Windows.Forms.Button btnTK;
        private System.Windows.Forms.DataGridView dgvG;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Button btnBack_Tk;
        private System.Windows.Forms.Button btnluu;
        private System.Windows.Forms.Button btnsua;
        private System.Windows.Forms.Button btnxoa;
        private System.Windows.Forms.Button btnBackCell;
        private System.Windows.Forms.Button ThemMoi;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnthoat;
        private System.Windows.Forms.Button btnXuat;
        private System.Windows.Forms.Button btnNhap;
    }
}