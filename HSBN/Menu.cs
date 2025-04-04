﻿using HSBN.QLBN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HSBN.HSXV;
using HSBN.QTDT;

namespace HSBN
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        private Form currentFormChild;

        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BenhNhan());
            txt1.Text = button1.Text;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Khoa());
            txt1.Text = button2.Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BacSi());
            txt1.Text = button3.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Thuoc());
            txt1.Text = button6.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Quatrinhdieutri());
            txt1.Text = button5.Text;
        }
    }
}
