namespace QLBN
{
    partial class Giuong_CapNhat
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCn = new System.Windows.Forms.Button();
            this.cbott = new System.Windows.Forms.ComboBox();
            this.cbomp = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtmg = new System.Windows.Forms.TextBox();
            this.btnthoat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCn);
            this.groupBox2.Controls.Add(this.cbott);
            this.groupBox2.Controls.Add(this.cbomp);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtmg);
            this.groupBox2.Location = new System.Drawing.Point(29, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(827, 128);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cập nhật thông tin";
            // 
            // btnCn
            // 
            this.btnCn.Location = new System.Drawing.Point(698, 76);
            this.btnCn.Name = "btnCn";
            this.btnCn.Size = new System.Drawing.Size(96, 32);
            this.btnCn.TabIndex = 9;
            this.btnCn.Text = "Cập nhật";
            this.btnCn.UseVisualStyleBackColor = true;
            this.btnCn.Click += new System.EventHandler(this.btnCn_Click);
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
            this.cbomp.DropDown += new System.EventHandler(this.cbomp_DropDown);
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
            // 
            // btnthoat
            // 
            this.btnthoat.Location = new System.Drawing.Point(760, 199);
            this.btnthoat.Name = "btnthoat";
            this.btnthoat.Size = new System.Drawing.Size(96, 27);
            this.btnthoat.TabIndex = 8;
            this.btnthoat.Text = "Thoát";
            this.btnthoat.UseVisualStyleBackColor = true;
            this.btnthoat.Click += new System.EventHandler(this.btnthoat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(294, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "CẬP NHẬT THÔNG TIN GIƯỜNG BỆNH";
            // 
            // Giuong_CapNhat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 249);
            this.Controls.Add(this.btnthoat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Name = "Giuong_CapNhat";
            this.Text = "Giuong_CapNhat";
            this.Load += new System.EventHandler(this.Giuong_CapNhat_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnthoat;
        private System.Windows.Forms.ComboBox cbott;
        private System.Windows.Forms.ComboBox cbomp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtmg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCn;
    }
}