using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSBN
{
    public partial class Thuoc : Form
    {
        public Thuoc()
        {
            InitializeComponent();
        }

        private void dateNgaysinh_ValueChanged(object sender, EventArgs e)
        {

        }

        private void xoa()
        {
            txtMathuoc.Text = "";
            txtTenthuoc.Text = "";
            txtDonvi.Text = "";
            txtDongia.Text = "";
            txtNsx.Text = "";
            txtMathuoctk.Text = "";
            txtTenthuoctk.Text = "";
            txtDongiatk.Text = "";
            txtNsxtk.Text = "";
            txtMathuoc.Enabled = true;
        }
        private void load_thuoc()
        {
            string sql = "SELECT * FROM Thuoc";
            thuvien.load(dataGridView1, sql);
        }
        public bool checktrungma(string ma)
        {
            if (thuvien.con.State == ConnectionState.Closed)
            {
                thuvien.con.Open();
            }
            string sql = "Select count(*) from Thuoc Where MaThuoc='" + ma + "'";
            SqlCommand cmd = new SqlCommand(sql, thuvien.con);
            int kq = (int)cmd.ExecuteScalar();
            if (kq > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            load_thuoc();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String ma = txtMathuoc.Text.Trim();
            String ten = txtTenthuoc.Text.Trim();
            String dv = txtDonvi.Text.Trim();
            String dg = txtDongia.Text.Trim();
            DateTime ngay = dateHsd.Value;
            String nsx = txtNsx.Text.Trim();

            if (checktrungma(ma))
            {
                MessageBox.Show("Trùng mã!");
                txtMathuoc.Focus();
                return;
            }
            if (ma == "")
            {
                MessageBox.Show("Chưa nhập mã!");
                txtMathuoc.Focus();
                return;
            }

            string sql = "Insert Thuoc Values('" + ma + "',N'" + ten + "',N'" + dv + "','" + dg + "','" + ngay + "',N'" + nsx + "')";
            thuvien.insert(sql);
            MessageBox.Show("Thêm mới thuốc thành công!");
            load_thuoc();
            xoa();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String ma = txtMathuoc.Text.Trim();
            String ten = txtTenthuoc.Text.Trim();
            String dv = txtDonvi.Text.Trim();
            String dg = txtDongia.Text.Trim();
            DateTime ngay = dateHsd.Value;
            String nsx = txtNsx.Text.Trim();
            
            string sql = "UPDATE Thuoc SET TenThuoc = N'" + ten +
             "', DonVi = N'" + dv +
             "', DonGia = '" + dg +
             "', HanSuDung = '" + ngay +
             "', NhaSanXuat = N'" + nsx +
             "' WHERE MaThuoc = '" + ma + "'";

            thuvien.insert(sql);
            MessageBox.Show("Cập nhật thuốc thành công!");
            load_thuoc();
            xoa();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String ma = txtMathuoc.Text.Trim();
            string sql = "DELETE FROM Thuoc WHERE MaThuoc = '" + ma + "'";
            thuvien.insert(sql);
            MessageBox.Show("Xoá thuốc thành công!");
            load_thuoc();
            xoa();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtMathuoc.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txtTenthuoc.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtDonvi.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtDongia.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            dateHsd.Value = DateTime.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
            txtNsx.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            txtMathuoc.Enabled = false;
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            String ma = txtMathuoctk.Text.Trim();
            String ten = txtTenthuoctk.Text.Trim();
            String dg = txtDongiatk.Text.Trim();
            String nsx = txtNsxtk.Text.Trim();

            string sql = "Select * From Thuoc Where MaThuoc like '%" + ma + "%' and" +
                " TenThuoc like N'%" + ten + "%' and" +
                " DonGia like '%" + dg + "%' and" +
                " NhaSanXuat like N'%" + nsx + "%'";

            thuvien.load(dataGridView1, sql);
            xoa();
        }
    }
}
