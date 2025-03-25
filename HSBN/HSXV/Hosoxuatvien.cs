using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSBN.HSXV
{
    public partial class Hosoxuatvien : Form
    {
        SqlConnection con = new SqlConnection("Data Source=TUANDEPZAI;Initial Catalog=QuanLyBenhNhan;Integrated Security=True");
        public Hosoxuatvien()
        {
            InitializeComponent();
        }

        private void xoatrang()
        {
            txtmhs.Text = "";
            txthoten.Text = "";
            txtld.Text = "";
            cbmbn.SelectedItem = null;
            cbtenkhoa.SelectedItem = null;
            cbmakhoa.SelectedItem = null;
            cbbs.SelectedItem = null;
            
        }

        private bool checktrungmhs(string mhs)
        {

            if (con.State == ConnectionState.Closed)

                con.Open();

            string sql = "Select count(*) from HoSoXuatVien Where MaHoSoXuatVien = '" + mhs + "' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            int kq = (int)cmd.ExecuteScalar();
            if (kq > 0)
                return true;
            else
                return false;

        }

        private void Load_hsxv()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();

            string sql = "Select * From HosoXuatVien";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            con.Close();
            tbhsxv.DataSource = tb;
            tbhsxv.Refresh();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string mhs = txtmhs.Text.Trim();
            string mbn = cbmbn.SelectedValue.ToString();
            string hoten = txthoten.Text.Trim();
            DateTime nn = dtngaynhap.Value;
            DateTime nx = dtngayxuat.Value;
            string mk = cbmakhoa.SelectedValue.ToString(); 
            string tk = cbtenkhoa.SelectedValue.ToString();
            string bs = cbbs.SelectedValue.ToString();
            string ld = txtld.Text.Trim();

            if (checktrungmhs(mhs))
            {
                MessageBox.Show("Trùng mã độc giả");
                txtmhs.Focus();
                return;
            }

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string sql = "Insert HosoXuatVien Values(@mhs, @mbn ,@hoten, @nn, @nx, @mk,  @tk, @bs, @ld)";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@mhs", SqlDbType.NVarChar, 50).Value = mhs;
            cmd.Parameters.Add("@mbn", SqlDbType.NVarChar, 50).Value = mbn;
            cmd.Parameters.Add("@hoten", SqlDbType.NVarChar, 100).Value = hoten;
            cmd.Parameters.Add("@nn", SqlDbType.DateTime).Value = nn;
            cmd.Parameters.Add("@nx", SqlDbType.DateTime).Value = nx;
            cmd.Parameters.Add("@mk", SqlDbType.NVarChar, 100).Value = mk;
            cmd.Parameters.Add("@tk", SqlDbType.NVarChar, 100).Value = tk;
            cmd.Parameters.Add("@bs", SqlDbType.NVarChar, 100).Value = bs;
            cmd.Parameters.Add("@ld", SqlDbType.NVarChar, 200).Value = ld;


            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            MessageBox.Show("Thêm mới thành công!");
            Load_hsxv();
            xoatrang();
        }
    }
}
