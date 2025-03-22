namespace HSBN
{
    partial class Menu
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.txt1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.panel3.Controls.Add(this.txt1);
            this.panel3.Location = new System.Drawing.Point(200, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(869, 59);
            this.panel3.TabIndex = 2;
            // 
            // txt1
            // 
            this.txt1.AutoSize = true;
            this.txt1.Location = new System.Drawing.Point(41, 19);
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(51, 16);
            this.txt1.TabIndex = 0;
            this.txt1.Text = "Hiện thị";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(9, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 495);
            this.panel1.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 311);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(179, 63);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 216);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(179, 63);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 63);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(200, 81);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(869, 430);
            this.panel2.TabIndex = 4;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 527);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "Menu";
            this.Text = "Menu";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label txt1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
    }
}