namespace HSBN.HSXV
{
    partial class Hoadon
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.HoTenBenhNhan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvBenhNhan = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbmbn = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbmakhoa = new System.Windows.Forms.ComboBox();
            this.grbThongTin = new System.Windows.Forms.GroupBox();
            this.cbtenkhoa = new System.Windows.Forms.ComboBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtngaynhap = new System.Windows.Forms.DateTimePicker();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.txthoten = new System.Windows.Forms.TextBox();
            this.txtmhs = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grbTimKiem = new System.Windows.Forms.GroupBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtTkHoTen = new System.Windows.Forms.TextBox();
            this.txtTkMaBN = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBenhNhan)).BeginInit();
            this.grbThongTin.SuspendLayout();
            this.grbTimKiem.SuspendLayout();
            this.SuspendLayout();
            // 
            // HoTenBenhNhan
            // 
            this.HoTenBenhNhan.HeaderText = "Họ và tên";
            this.HoTenBenhNhan.MinimumWidth = 6;
            this.HoTenBenhNhan.Name = "HoTenBenhNhan";
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "MaHoSoXuatVien";
            this.Column1.HeaderText = "Mã hồ sơ";
            this.Column1.MinimumWidth = 10;
            this.Column1.Name = "Column1";
            // 
            // dgvBenhNhan
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvBenhNhan.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBenhNhan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBenhNhan.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvBenhNhan.BackgroundColor = System.Drawing.Color.LightGray;
            this.dgvBenhNhan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBenhNhan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.HoTenBenhNhan,
            this.Column3,
            this.Column4,
            this.Column6,
            this.Column5,
            this.Column7,
            this.Column8});
            this.dgvBenhNhan.Location = new System.Drawing.Point(10, 91);
            this.dgvBenhNhan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvBenhNhan.Name = "dgvBenhNhan";
            this.dgvBenhNhan.RowHeadersVisible = false;
            this.dgvBenhNhan.RowHeadersWidth = 82;
            this.dgvBenhNhan.RowTemplate.Height = 33;
            this.dgvBenhNhan.Size = new System.Drawing.Size(1133, 244);
            this.dgvBenhNhan.TabIndex = 16;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "MaBenhNhan";
            this.Column2.HeaderText = "Mã bệnh nhân";
            this.Column2.MinimumWidth = 10;
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "NgayNhapVien";
            this.Column3.HeaderText = "Ngày nhập viện";
            this.Column3.MinimumWidth = 10;
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "NgayXuat";
            this.Column4.HeaderText = "Ngày xuất viện";
            this.Column4.MinimumWidth = 10;
            this.Column4.Name = "Column4";
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "MaKhoa";
            this.Column6.HeaderText = "Mã khoa";
            this.Column6.MinimumWidth = 10;
            this.Column6.Name = "Column6";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "TenKhoa";
            this.Column5.HeaderText = "Tên khoa";
            this.Column5.MinimumWidth = 10;
            this.Column5.Name = "Column5";
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "BacSiDieuTri";
            this.Column7.HeaderText = "Bác sĩ điều trị";
            this.Column7.MinimumWidth = 10;
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "LyDoXuat";
            this.Column8.HeaderText = "Lý do xuất viện";
            this.Column8.MinimumWidth = 10;
            this.Column8.Name = "Column8";
            // 
            // cbmbn
            // 
            this.cbmbn.FormattingEnabled = true;
            this.cbmbn.Items.AddRange(new object[] {
            "Nam",
            "Nữ",
            "Khác"});
            this.cbmbn.Location = new System.Drawing.Point(166, 75);
            this.cbmbn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbmbn.Name = "cbmbn";
            this.cbmbn.Size = new System.Drawing.Size(220, 24);
            this.cbmbn.TabIndex = 41;
            this.cbmbn.Text = "-- Chọn giới tính --";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(28, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 16);
            this.label3.TabIndex = 40;
            this.label3.Text = "Mã bệnh nhân";
            // 
            // cbmakhoa
            // 
            this.cbmakhoa.FormattingEnabled = true;
            this.cbmakhoa.Items.AddRange(new object[] {
            "Nam",
            "Nữ",
            "Khác"});
            this.cbmakhoa.Location = new System.Drawing.Point(168, 157);
            this.cbmakhoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbmakhoa.Name = "cbmakhoa";
            this.cbmakhoa.Size = new System.Drawing.Size(220, 24);
            this.cbmakhoa.TabIndex = 36;
            this.cbmakhoa.Text = "-- Chọn giới tính --";
            // 
            // grbThongTin
            // 
            this.grbThongTin.Controls.Add(this.textBox3);
            this.grbThongTin.Controls.Add(this.textBox2);
            this.grbThongTin.Controls.Add(this.textBox1);
            this.grbThongTin.Controls.Add(this.cbmbn);
            this.grbThongTin.Controls.Add(this.label3);
            this.grbThongTin.Controls.Add(this.cbtenkhoa);
            this.grbThongTin.Controls.Add(this.cbmakhoa);
            this.grbThongTin.Controls.Add(this.btnThem);
            this.grbThongTin.Controls.Add(this.label9);
            this.grbThongTin.Controls.Add(this.label12);
            this.grbThongTin.Controls.Add(this.dtngaynhap);
            this.grbThongTin.Controls.Add(this.btnReset);
            this.grbThongTin.Controls.Add(this.btnXoa);
            this.grbThongTin.Controls.Add(this.btnSua);
            this.grbThongTin.Controls.Add(this.btnLuu);
            this.grbThongTin.Controls.Add(this.txthoten);
            this.grbThongTin.Controls.Add(this.txtmhs);
            this.grbThongTin.Controls.Add(this.label10);
            this.grbThongTin.Controls.Add(this.label11);
            this.grbThongTin.Controls.Add(this.label8);
            this.grbThongTin.Controls.Add(this.label7);
            this.grbThongTin.Controls.Add(this.label6);
            this.grbThongTin.Controls.Add(this.label5);
            this.grbThongTin.Location = new System.Drawing.Point(10, 349);
            this.grbThongTin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grbThongTin.Name = "grbThongTin";
            this.grbThongTin.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grbThongTin.Size = new System.Drawing.Size(1133, 305);
            this.grbThongTin.TabIndex = 17;
            this.grbThongTin.TabStop = false;
            this.grbThongTin.Text = "Nhập thông tin bệnh nhân";
            // 
            // cbtenkhoa
            // 
            this.cbtenkhoa.FormattingEnabled = true;
            this.cbtenkhoa.Items.AddRange(new object[] {
            "Nam",
            "Nữ",
            "Khác"});
            this.cbtenkhoa.Location = new System.Drawing.Point(664, 69);
            this.cbtenkhoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbtenkhoa.Name = "cbtenkhoa";
            this.cbtenkhoa.Size = new System.Drawing.Size(220, 24);
            this.cbtenkhoa.TabIndex = 37;
            this.cbtenkhoa.Text = "-- Chọn giới tính --";
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(27, 256);
            this.btnThem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(111, 26);
            this.btnThem.TabIndex = 35;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(539, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 16);
            this.label9.TabIndex = 31;
            this.label9.Text = "BHYT:";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(539, 36);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 16);
            this.label12.TabIndex = 30;
            this.label12.Text = "Liều lượng: ";
            // 
            // dtngaynhap
            // 
            this.dtngaynhap.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtngaynhap.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtngaynhap.Location = new System.Drawing.Point(664, 108);
            this.dtngaynhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtngaynhap.Name = "dtngaynhap";
            this.dtngaynhap.Size = new System.Drawing.Size(220, 22);
            this.dtngaynhap.TabIndex = 29;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(804, 256);
            this.btnReset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(135, 26);
            this.btnReset.TabIndex = 27;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(580, 256);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(111, 26);
            this.btnXoa.TabIndex = 26;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(380, 256);
            this.btnSua.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(111, 26);
            this.btnSua.TabIndex = 25;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(202, 256);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(111, 26);
            this.btnLuu.TabIndex = 24;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            // 
            // txthoten
            // 
            this.txthoten.Location = new System.Drawing.Point(168, 112);
            this.txthoten.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txthoten.Name = "txthoten";
            this.txthoten.Size = new System.Drawing.Size(220, 22);
            this.txthoten.TabIndex = 16;
            // 
            // txtmhs
            // 
            this.txtmhs.Location = new System.Drawing.Point(166, 31);
            this.txtmhs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtmhs.Name = "txtmhs";
            this.txtmhs.Size = new System.Drawing.Size(220, 22);
            this.txtmhs.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(539, 162);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 16);
            this.label10.TabIndex = 15;
            this.label10.Text = "Thanh toán: ";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(539, 115);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(119, 24);
            this.label11.TabIndex = 14;
            this.label11.Text = "Ngày thanh toán: ";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(30, 210);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 22);
            this.label8.TabIndex = 12;
            this.label8.Text = "Thuốc";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(30, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 22);
            this.label7.TabIndex = 11;
            this.label7.Text = "Mã thuốc: ";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(30, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Họ và tên:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(28, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Mã hồ sơ:";
            // 
            // grbTimKiem
            // 
            this.grbTimKiem.Controls.Add(this.btnTimKiem);
            this.grbTimKiem.Controls.Add(this.txtTkHoTen);
            this.grbTimKiem.Controls.Add(this.txtTkMaBN);
            this.grbTimKiem.Controls.Add(this.label2);
            this.grbTimKiem.Controls.Add(this.label1);
            this.grbTimKiem.Location = new System.Drawing.Point(12, 11);
            this.grbTimKiem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grbTimKiem.Name = "grbTimKiem";
            this.grbTimKiem.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grbTimKiem.Size = new System.Drawing.Size(1131, 76);
            this.grbTimKiem.TabIndex = 15;
            this.grbTimKiem.TabStop = false;
            this.grbTimKiem.Text = "Thông tin tìm kiếm";
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(807, 26);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(135, 24);
            this.btnTimKiem.TabIndex = 8;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            // 
            // txtTkHoTen
            // 
            this.txtTkHoTen.Location = new System.Drawing.Point(550, 29);
            this.txtTkHoTen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTkHoTen.Name = "txtTkHoTen";
            this.txtTkHoTen.Size = new System.Drawing.Size(204, 22);
            this.txtTkHoTen.TabIndex = 5;
            // 
            // txtTkMaBN
            // 
            this.txtTkMaBN.Location = new System.Drawing.Point(164, 31);
            this.txtTkMaBN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTkMaBN.Name = "txtTkMaBN";
            this.txtTkMaBN.Size = new System.Drawing.Size(204, 22);
            this.txtTkMaBN.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(26, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã hồ sơ:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(414, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Họ và tên: ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(166, 210);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(220, 22);
            this.textBox1.TabIndex = 42;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(664, 25);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(220, 22);
            this.textBox2.TabIndex = 43;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(664, 162);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(220, 22);
            this.textBox3.TabIndex = 44;
            // 
            // Hoadon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 681);
            this.Controls.Add(this.dgvBenhNhan);
            this.Controls.Add(this.grbThongTin);
            this.Controls.Add(this.grbTimKiem);
            this.Name = "Hoadon";
            this.Text = "Hoadon";
            this.Load += new System.EventHandler(this.Hoadon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBenhNhan)).EndInit();
            this.grbThongTin.ResumeLayout(false);
            this.grbThongTin.PerformLayout();
            this.grbTimKiem.ResumeLayout(false);
            this.grbTimKiem.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn HoTenBenhNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridView dgvBenhNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.ComboBox cbmbn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbmakhoa;
        private System.Windows.Forms.GroupBox grbThongTin;
        private System.Windows.Forms.ComboBox cbtenkhoa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtngaynhap;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.TextBox txthoten;
        private System.Windows.Forms.TextBox txtmhs;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grbTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.TextBox txtTkHoTen;
        private System.Windows.Forms.TextBox txtTkMaBN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
    }
}