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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbhd = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaPhong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoaiPhong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.cbmathuoc = new System.Windows.Forms.ComboBox();
            this.grbThongTin = new System.Windows.Forms.GroupBox();
            this.btnhap = new System.Windows.Forms.Button();
            this.btxuat = new System.Windows.Forms.Button();
            this.txtloaiphong = new System.Windows.Forms.TextBox();
            this.txtmaphong = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt = new System.Windows.Forms.Label();
            this.txtmbn = new System.Windows.Forms.TextBox();
            this.cbmhs = new System.Windows.Forms.ComboBox();
            this.txttt = new System.Windows.Forms.TextBox();
            this.txtll = new System.Windows.Forms.TextBox();
            this.txtthuoc = new System.Windows.Forms.TextBox();
            this.cbloai = new System.Windows.Forms.ComboBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtngaytt = new System.Windows.Forms.DateTimePicker();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.txthoten = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grbTimKiem = new System.Windows.Forms.GroupBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txthotentk = new System.Windows.Forms.TextBox();
            this.txtmhstk = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbhd)).BeginInit();
            this.grbThongTin.SuspendLayout();
            this.grbTimKiem.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbhd
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tbhd.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.tbhd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tbhd.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.tbhd.BackgroundColor = System.Drawing.Color.LightGray;
            this.tbhd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tbhd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.MaPhong,
            this.LoaiPhong,
            this.Column6,
            this.Column8,
            this.Column3});
            this.tbhd.Location = new System.Drawing.Point(10, 91);
            this.tbhd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbhd.Name = "tbhd";
            this.tbhd.RowHeadersVisible = false;
            this.tbhd.RowHeadersWidth = 82;
            this.tbhd.RowTemplate.Height = 33;
            this.tbhd.Size = new System.Drawing.Size(1233, 244);
            this.tbhd.TabIndex = 16;
            this.tbhd.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tbhd_CellClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "MaHoSoXuatVien";
            this.Column1.HeaderText = "Mã hồ sơ";
            this.Column1.MinimumWidth = 10;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "MaBenhNhan";
            this.Column2.HeaderText = "Mã bệnh nhân";
            this.Column2.MinimumWidth = 10;
            this.Column2.Name = "Column2";
            // 
            // MaPhong
            // 
            this.MaPhong.DataPropertyName = "MaPhong";
            this.MaPhong.HeaderText = "Mã phòng";
            this.MaPhong.MinimumWidth = 6;
            this.MaPhong.Name = "MaPhong";
            // 
            // LoaiPhong
            // 
            this.LoaiPhong.DataPropertyName = "LoaiPhong";
            this.LoaiPhong.HeaderText = "Loại phòng";
            this.LoaiPhong.MinimumWidth = 6;
            this.LoaiPhong.Name = "LoaiPhong";
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "MaThuoc";
            this.Column6.HeaderText = "Mã thuốc";
            this.Column6.MinimumWidth = 10;
            this.Column6.Name = "Column6";
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "LieuLuong";
            this.Column8.HeaderText = "Liều lượng";
            this.Column8.MinimumWidth = 10;
            this.Column8.Name = "Column8";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "NgayThanhToan";
            this.Column3.HeaderText = "Ngày thanh toán";
            this.Column3.MinimumWidth = 10;
            this.Column3.Name = "Column3";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(28, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 16);
            this.label3.TabIndex = 40;
            this.label3.Text = "Mã bệnh nhân:";
            // 
            // cbmathuoc
            // 
            this.cbmathuoc.FormattingEnabled = true;
            this.cbmathuoc.Location = new System.Drawing.Point(525, 110);
            this.cbmathuoc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbmathuoc.Name = "cbmathuoc";
            this.cbmathuoc.Size = new System.Drawing.Size(220, 24);
            this.cbmathuoc.TabIndex = 36;
            this.cbmathuoc.SelectedIndexChanged += new System.EventHandler(this.cbmathuoc_SelectedIndexChanged);
            // 
            // grbThongTin
            // 
            this.grbThongTin.Controls.Add(this.btnhap);
            this.grbThongTin.Controls.Add(this.btxuat);
            this.grbThongTin.Controls.Add(this.txtloaiphong);
            this.grbThongTin.Controls.Add(this.txtmaphong);
            this.grbThongTin.Controls.Add(this.label4);
            this.grbThongTin.Controls.Add(this.txt);
            this.grbThongTin.Controls.Add(this.txtmbn);
            this.grbThongTin.Controls.Add(this.cbmhs);
            this.grbThongTin.Controls.Add(this.txttt);
            this.grbThongTin.Controls.Add(this.txtll);
            this.grbThongTin.Controls.Add(this.txtthuoc);
            this.grbThongTin.Controls.Add(this.label3);
            this.grbThongTin.Controls.Add(this.cbloai);
            this.grbThongTin.Controls.Add(this.cbmathuoc);
            this.grbThongTin.Controls.Add(this.btnThem);
            this.grbThongTin.Controls.Add(this.label9);
            this.grbThongTin.Controls.Add(this.label12);
            this.grbThongTin.Controls.Add(this.dtngaytt);
            this.grbThongTin.Controls.Add(this.btnReset);
            this.grbThongTin.Controls.Add(this.btnXoa);
            this.grbThongTin.Controls.Add(this.btnSua);
            this.grbThongTin.Controls.Add(this.btnLuu);
            this.grbThongTin.Controls.Add(this.txthoten);
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
            this.grbThongTin.Size = new System.Drawing.Size(1233, 305);
            this.grbThongTin.TabIndex = 17;
            this.grbThongTin.TabStop = false;
            this.grbThongTin.Text = "Nhập thông tin bệnh nhân";
            // 
            // btnhap
            // 
            this.btnhap.Location = new System.Drawing.Point(754, 256);
            this.btnhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnhap.Name = "btnhap";
            this.btnhap.Size = new System.Drawing.Size(111, 26);
            this.btnhap.TabIndex = 52;
            this.btnhap.Text = "Nhập Excel";
            this.btnhap.UseVisualStyleBackColor = true;
            this.btnhap.Click += new System.EventHandler(this.btnhap_Click);
            // 
            // btxuat
            // 
            this.btxuat.Location = new System.Drawing.Point(602, 256);
            this.btxuat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btxuat.Name = "btxuat";
            this.btxuat.Size = new System.Drawing.Size(111, 26);
            this.btxuat.TabIndex = 51;
            this.btxuat.Text = "Xuất Excel";
            this.btxuat.UseVisualStyleBackColor = true;
            this.btxuat.Click += new System.EventHandler(this.btxuat_Click);
            // 
            // txtloaiphong
            // 
            this.txtloaiphong.Location = new System.Drawing.Point(131, 199);
            this.txtloaiphong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtloaiphong.Name = "txtloaiphong";
            this.txtloaiphong.Size = new System.Drawing.Size(220, 22);
            this.txtloaiphong.TabIndex = 50;
            this.txtloaiphong.TextChanged += new System.EventHandler(this.txtloaiphong_TextChanged);
            // 
            // txtmaphong
            // 
            this.txtmaphong.Location = new System.Drawing.Point(131, 156);
            this.txtmaphong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtmaphong.Name = "txtmaphong";
            this.txtmaphong.Size = new System.Drawing.Size(220, 22);
            this.txtmaphong.TabIndex = 49;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(30, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 22);
            this.label4.TabIndex = 48;
            this.label4.Text = "Loại phòng:";
            // 
            // txt
            // 
            this.txt.Location = new System.Drawing.Point(30, 162);
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(114, 22);
            this.txt.TabIndex = 47;
            this.txt.Text = "Mã phòng:";
            // 
            // txtmbn
            // 
            this.txtmbn.Location = new System.Drawing.Point(131, 69);
            this.txtmbn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtmbn.Name = "txtmbn";
            this.txtmbn.Size = new System.Drawing.Size(220, 22);
            this.txtmbn.TabIndex = 46;
            // 
            // cbmhs
            // 
            this.cbmhs.FormattingEnabled = true;
            this.cbmhs.Location = new System.Drawing.Point(131, 25);
            this.cbmhs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbmhs.Name = "cbmhs";
            this.cbmhs.Size = new System.Drawing.Size(220, 24);
            this.cbmhs.TabIndex = 45;
            this.cbmhs.SelectedIndexChanged += new System.EventHandler(this.cbmhs_SelectedIndexChanged);
            // 
            // txttt
            // 
            this.txttt.Location = new System.Drawing.Point(926, 68);
            this.txttt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txttt.Name = "txttt";
            this.txttt.Size = new System.Drawing.Size(220, 22);
            this.txttt.TabIndex = 44;
            // 
            // txtll
            // 
            this.txtll.Location = new System.Drawing.Point(525, 27);
            this.txtll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtll.Name = "txtll";
            this.txtll.Size = new System.Drawing.Size(220, 22);
            this.txtll.TabIndex = 43;
            // 
            // txtthuoc
            // 
            this.txtthuoc.Location = new System.Drawing.Point(525, 156);
            this.txtthuoc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtthuoc.Name = "txtthuoc";
            this.txtthuoc.Size = new System.Drawing.Size(220, 22);
            this.txtthuoc.TabIndex = 42;
            // 
            // cbloai
            // 
            this.cbloai.FormattingEnabled = true;
            this.cbloai.Items.AddRange(new object[] {
            "90%",
            "70%",
            "50%"});
            this.cbloai.Location = new System.Drawing.Point(525, 68);
            this.cbloai.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbloai.Name = "cbloai";
            this.cbloai.Size = new System.Drawing.Size(220, 24);
            this.cbloai.TabIndex = 37;
            this.cbloai.Text = "-- Chọn loại  --";
            this.cbloai.SelectedIndexChanged += new System.EventHandler(this.cbloai_SelectedIndexChanged);
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
            this.label9.Location = new System.Drawing.Point(422, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 16);
            this.label9.TabIndex = 31;
            this.label9.Text = "BHYT:";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(422, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 16);
            this.label12.TabIndex = 30;
            this.label12.Text = "Liều lượng: ";
            // 
            // dtngaytt
            // 
            this.dtngaytt.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtngaytt.CustomFormat = "yyyy/MM/dd";
            this.dtngaytt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtngaytt.Location = new System.Drawing.Point(926, 23);
            this.dtngaytt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtngaytt.Name = "dtngaytt";
            this.dtngaytt.Size = new System.Drawing.Size(220, 22);
            this.dtngaytt.TabIndex = 29;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(1077, 256);
            this.btnReset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(135, 26);
            this.btnReset.TabIndex = 27;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(455, 256);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(111, 26);
            this.btnXoa.TabIndex = 26;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(305, 256);
            this.btnSua.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(111, 26);
            this.btnSua.TabIndex = 25;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(166, 256);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(111, 26);
            this.btnLuu.TabIndex = 24;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // txthoten
            // 
            this.txthoten.Location = new System.Drawing.Point(131, 112);
            this.txthoten.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txthoten.Name = "txthoten";
            this.txthoten.Size = new System.Drawing.Size(220, 22);
            this.txthoten.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(801, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 16);
            this.label10.TabIndex = 15;
            this.label10.Text = "Thanh toán: ";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(801, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(119, 24);
            this.label11.TabIndex = 14;
            this.label11.Text = "Ngày thanh toán: ";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(422, 164);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 22);
            this.label8.TabIndex = 12;
            this.label8.Text = "Thuốc";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(422, 108);
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
            this.grbTimKiem.Controls.Add(this.txthotentk);
            this.grbTimKiem.Controls.Add(this.txtmhstk);
            this.grbTimKiem.Controls.Add(this.label2);
            this.grbTimKiem.Controls.Add(this.label1);
            this.grbTimKiem.Location = new System.Drawing.Point(12, 11);
            this.grbTimKiem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grbTimKiem.Name = "grbTimKiem";
            this.grbTimKiem.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grbTimKiem.Size = new System.Drawing.Size(1231, 76);
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
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // txthotentk
            // 
            this.txthotentk.Location = new System.Drawing.Point(550, 29);
            this.txthotentk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txthotentk.Name = "txthotentk";
            this.txthotentk.Size = new System.Drawing.Size(204, 22);
            this.txthotentk.TabIndex = 5;
            // 
            // txtmhstk
            // 
            this.txtmhstk.Location = new System.Drawing.Point(164, 31);
            this.txtmhstk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtmhstk.Name = "txtmhstk";
            this.txtmhstk.Size = new System.Drawing.Size(204, 22);
            this.txtmhstk.TabIndex = 4;
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
            // Hoadon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1255, 681);
            this.Controls.Add(this.tbhd);
            this.Controls.Add(this.grbThongTin);
            this.Controls.Add(this.grbTimKiem);
            this.Name = "Hoadon";
            this.Text = "Hoadon";
            this.Load += new System.EventHandler(this.Hoadon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbhd)).EndInit();
            this.grbThongTin.ResumeLayout(false);
            this.grbThongTin.PerformLayout();
            this.grbTimKiem.ResumeLayout(false);
            this.grbTimKiem.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView tbhd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbmathuoc;
        private System.Windows.Forms.GroupBox grbThongTin;
        private System.Windows.Forms.ComboBox cbloai;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtngaytt;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.TextBox txthoten;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grbTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.TextBox txthotentk;
        private System.Windows.Forms.TextBox txtmhstk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txttt;
        private System.Windows.Forms.TextBox txtll;
        private System.Windows.Forms.TextBox txtthuoc;
        private System.Windows.Forms.TextBox txtmbn;
        private System.Windows.Forms.ComboBox cbmhs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label txt;
        private System.Windows.Forms.TextBox txtloaiphong;
        private System.Windows.Forms.TextBox txtmaphong;
        private System.Windows.Forms.Button btnhap;
        private System.Windows.Forms.Button btxuat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaPhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoaiPhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}