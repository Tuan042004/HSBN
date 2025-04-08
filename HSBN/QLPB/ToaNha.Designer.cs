namespace QLBN
{
    partial class ToaNha
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
            this.btnBack = new System.Windows.Forms.Button();
            this.ThemMoi = new System.Windows.Forms.Button();
            this.dgvT = new System.Windows.Forms.DataGridView();
            this.btnthoat = new System.Windows.Forms.Button();
            this.btnxoa = new System.Windows.Forms.Button();
            this.btnsua = new System.Windows.Forms.Button();
            this.btnluu = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPt = new System.Windows.Forms.TextBox();
            this.txtTp = new System.Windows.Forms.TextBox();
            this.btnBack_Tk = new System.Windows.Forms.Button();
            this.txtt = new System.Windows.Forms.TextBox();
            this.btnTK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtmt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnbackcell = new System.Windows.Forms.Button();
            this.btnXuat = new System.Windows.Forms.Button();
            this.btnNhap = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvT)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(101, 489);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 31);
            this.btnBack.TabIndex = 23;
            this.btnBack.Text = "Trở về";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // ThemMoi
            // 
            this.ThemMoi.Location = new System.Drawing.Point(48, 490);
            this.ThemMoi.Name = "ThemMoi";
            this.ThemMoi.Size = new System.Drawing.Size(120, 31);
            this.ThemMoi.TabIndex = 22;
            this.ThemMoi.Text = "Thêm mới";
            this.ThemMoi.UseVisualStyleBackColor = true;
            this.ThemMoi.Click += new System.EventHandler(this.ThemMoi_Click);
            // 
            // dgvT
            // 
            this.dgvT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvT.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgvT.Location = new System.Drawing.Point(49, 221);
            this.dgvT.Name = "dgvT";
            this.dgvT.RowHeadersWidth = 62;
            this.dgvT.RowTemplate.Height = 28;
            this.dgvT.Size = new System.Drawing.Size(773, 249);
            this.dgvT.TabIndex = 21;
            this.dgvT.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvT_CellClick);
            // 
            // btnthoat
            // 
            this.btnthoat.Location = new System.Drawing.Point(747, 487);
            this.btnthoat.Name = "btnthoat";
            this.btnthoat.Size = new System.Drawing.Size(75, 30);
            this.btnthoat.TabIndex = 20;
            this.btnthoat.Text = "Thoát";
            this.btnthoat.UseVisualStyleBackColor = true;
            this.btnthoat.Click += new System.EventHandler(this.btnthoat_Click);
            // 
            // btnxoa
            // 
            this.btnxoa.Location = new System.Drawing.Point(580, 489);
            this.btnxoa.Name = "btnxoa";
            this.btnxoa.Size = new System.Drawing.Size(75, 30);
            this.btnxoa.TabIndex = 19;
            this.btnxoa.Text = "Xóa";
            this.btnxoa.UseVisualStyleBackColor = true;
            this.btnxoa.Click += new System.EventHandler(this.btnxoa_Click);
            // 
            // btnsua
            // 
            this.btnsua.Location = new System.Drawing.Point(452, 487);
            this.btnsua.Name = "btnsua";
            this.btnsua.Size = new System.Drawing.Size(105, 30);
            this.btnsua.TabIndex = 18;
            this.btnsua.Text = "Cập nhật";
            this.btnsua.UseVisualStyleBackColor = true;
            this.btnsua.Click += new System.EventHandler(this.btnsua_Click);
            // 
            // btnluu
            // 
            this.btnluu.Location = new System.Drawing.Point(186, 490);
            this.btnluu.Name = "btnluu";
            this.btnluu.Size = new System.Drawing.Size(75, 29);
            this.btnluu.TabIndex = 17;
            this.btnluu.Text = "Lưu";
            this.btnluu.UseVisualStyleBackColor = true;
            this.btnluu.Click += new System.EventHandler(this.btnluu_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtPt);
            this.groupBox2.Controls.Add(this.txtTp);
            this.groupBox2.Controls.Add(this.btnBack_Tk);
            this.groupBox2.Controls.Add(this.txtt);
            this.groupBox2.Controls.Add(this.btnTK);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtmt);
            this.groupBox2.Location = new System.Drawing.Point(49, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(773, 118);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cập nhật thông tin";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(332, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 20);
            this.label5.TabIndex = 40;
            this.label5.Text = "Phòng/Tầng:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 20);
            this.label3.TabIndex = 39;
            this.label3.Text = "Tổng phòng:";
            // 
            // txtPt
            // 
            this.txtPt.Location = new System.Drawing.Point(437, 83);
            this.txtPt.Name = "txtPt";
            this.txtPt.Size = new System.Drawing.Size(153, 26);
            this.txtPt.TabIndex = 38;
            this.txtPt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPt_KeyPress);
            // 
            // txtTp
            // 
            this.txtTp.Location = new System.Drawing.Point(137, 83);
            this.txtTp.Name = "txtTp";
            this.txtTp.Size = new System.Drawing.Size(162, 26);
            this.txtTp.TabIndex = 37;
            this.txtTp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTp_KeyPress);
            // 
            // btnBack_Tk
            // 
            this.btnBack_Tk.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack_Tk.Location = new System.Drawing.Point(653, 71);
            this.btnBack_Tk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnBack_Tk.Name = "btnBack_Tk";
            this.btnBack_Tk.Size = new System.Drawing.Size(95, 32);
            this.btnBack_Tk.TabIndex = 36;
            this.btnBack_Tk.Text = "Trở về";
            this.btnBack_Tk.UseVisualStyleBackColor = true;
            this.btnBack_Tk.Click += new System.EventHandler(this.btnBack_Tk_Click);
            // 
            // txtt
            // 
            this.txtt.Location = new System.Drawing.Point(437, 39);
            this.txtt.Name = "txtt";
            this.txtt.Size = new System.Drawing.Size(153, 26);
            this.txtt.TabIndex = 9;
            this.txtt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtt_KeyPress);
            // 
            // btnTK
            // 
            this.btnTK.Location = new System.Drawing.Point(653, 32);
            this.btnTK.Name = "btnTK";
            this.btnTK.Size = new System.Drawing.Size(95, 33);
            this.btnTK.TabIndex = 8;
            this.btnTK.Text = "Tìm kiếm";
            this.btnTK.UseVisualStyleBackColor = true;
            this.btnTK.Click += new System.EventHandler(this.btnTK_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(362, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Số tầng:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã tòa:";
            // 
            // txtmt
            // 
            this.txtmt.Location = new System.Drawing.Point(137, 39);
            this.txtmt.Name = "txtmt";
            this.txtmt.Size = new System.Drawing.Size(162, 26);
            this.txtmt.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(215, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "THÔNG TIN TÒA NHÀ BỆNH VIỆN\r\n";
            // 
            // btnbackcell
            // 
            this.btnbackcell.Location = new System.Drawing.Point(371, 489);
            this.btnbackcell.Name = "btnbackcell";
            this.btnbackcell.Size = new System.Drawing.Size(75, 30);
            this.btnbackcell.TabIndex = 24;
            this.btnbackcell.Text = "Trở về";
            this.btnbackcell.UseVisualStyleBackColor = true;
            this.btnbackcell.Click += new System.EventHandler(this.btnbackcell_Click);
            // 
            // btnXuat
            // 
            this.btnXuat.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuat.Location = new System.Drawing.Point(195, 487);
            this.btnXuat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnXuat.Name = "btnXuat";
            this.btnXuat.Size = new System.Drawing.Size(95, 32);
            this.btnXuat.TabIndex = 37;
            this.btnXuat.Text = "Xuất file";
            this.btnXuat.UseVisualStyleBackColor = true;
            this.btnXuat.Click += new System.EventHandler(this.btnXuat_Click);
            // 
            // btnNhap
            // 
            this.btnNhap.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNhap.Location = new System.Drawing.Point(309, 487);
            this.btnNhap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnNhap.Name = "btnNhap";
            this.btnNhap.Size = new System.Drawing.Size(95, 32);
            this.btnNhap.TabIndex = 38;
            this.btnNhap.Text = "Nhập file";
            this.btnNhap.UseVisualStyleBackColor = true;
            this.btnNhap.Click += new System.EventHandler(this.btnNhap_Click);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "MaToa";
            this.Column1.HeaderText = "Mã tòa";
            this.Column1.MinimumWidth = 8;
            this.Column1.Name = "Column1";
            this.Column1.Width = 80;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "SoTang";
            this.Column2.HeaderText = "Số tầng";
            this.Column2.MinimumWidth = 8;
            this.Column2.Name = "Column2";
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "SoPhongMax";
            this.Column3.HeaderText = "Tổng số phòng";
            this.Column3.MinimumWidth = 8;
            this.Column3.Name = "Column3";
            this.Column3.Width = 120;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "SoPhongMaxMoiTang";
            this.Column4.HeaderText = "Số phòng mỗi tầng";
            this.Column4.MinimumWidth = 8;
            this.Column4.Name = "Column4";
            this.Column4.Width = 120;
            // 
            // ToaNha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 558);
            this.Controls.Add(this.btnNhap);
            this.Controls.Add(this.btnXuat);
            this.Controls.Add(this.btnbackcell);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.ThemMoi);
            this.Controls.Add(this.dgvT);
            this.Controls.Add(this.btnthoat);
            this.Controls.Add(this.btnxoa);
            this.Controls.Add(this.btnsua);
            this.Controls.Add(this.btnluu);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Name = "ToaNha";
            this.Text = "ToaNha";
            this.Load += new System.EventHandler(this.ToaNha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvT)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button ThemMoi;
        private System.Windows.Forms.DataGridView dgvT;
        private System.Windows.Forms.Button btnthoat;
        private System.Windows.Forms.Button btnxoa;
        private System.Windows.Forms.Button btnsua;
        private System.Windows.Forms.Button btnluu;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtmt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtt;
        private System.Windows.Forms.Button btnbackcell;
        private System.Windows.Forms.Button btnBack_Tk;
        private System.Windows.Forms.Button btnXuat;
        private System.Windows.Forms.Button btnNhap;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPt;
        private System.Windows.Forms.TextBox txtTp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}