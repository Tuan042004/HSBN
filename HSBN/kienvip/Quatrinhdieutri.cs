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
    public partial class Quatrinhdieutri : Form
    {
        public Quatrinhdieutri()
        {
            InitializeComponent();
        }

        private void xoa()
        {
            txtMadt.Text = "";
            txtChandoan.Text = "";
            txtPhuongphap.Text = "";
            txtChandoantk.Text = "";
            txtMadt.Enabled = true;
        }
        private void load_qtdt()
        {
            string sql = "SELECT QuaTrinhDieuTri.*, BenhNhan.HoTenBenhNhan, NhanVienYTe.BacSiDieuTri, Khoa.TenKhoa, Thuoc.TenThuoc " +
                "FROM QuaTrinhDieuTri " +
                "JOIN BenhNhan ON QuaTrinhDieuTri.MaBenhNhan = BenhNhan.MaBenhNhan " +
                "JOIN NhanVienYTe ON QuaTrinhDieuTri.MaNhanVien = NhanVienYTe.MaNhanVien " +
                "JOIN Khoa ON QuaTrinhDieuTri.MaKhoa = Khoa.MaKhoa " +
                "LEFT JOIN Thuoc ON QuaTrinhDieuTri.MaThuoc = Thuoc.MaThuoc";

            // lấy nhiều loại thuốc
            //string sql = "SELECT QuaTrinhDieuTri.MaDieuTri, BenhNhan.HoTenBenhNhan, " +
            //    "NhanVienYTe.BacSiDieuTri, Khoa.TenKhoa, " +
            //    "STRING_AGG(Thuoc.TenThuoc, ', ') AS DanhSachThuoc " +
            //    "FROM QuaTrinhDieuTri " +
            //    "JOIN BenhNhan ON QuaTrinhDieuTri.MaBenhNhan = BenhNhan.MaBenhNhan " +
            //    "JOIN NhanVienYTe ON QuaTrinhDieuTri.MaNhanVien = NhanVienYTe.MaNhanVien " +
            //    "JOIN Khoa ON QuaTrinhDieuTri.MaKhoa = Khoa.MaKhoa " +
            //    "LEFT JOIN Thuoc ON QuaTrinhDieuTri.MaThuoc = Thuoc.MaThuoc " +
            //    "GROUP BY QuaTrinhDieuTri.MaDieuTri, BenhNhan.HoTenBenhNhan, " +
            //    "NhanVienYTe.BacSiDieuTri, Khoa.TenKhoa";

            thuvien.load(dataGridView1, sql);
        }

        private void load_cbo()
        {
            string sql = "Select * From Khoa";
            string sql1 = "Select * From Thuoc";
            string sql2 = "Select * From BenhNhan";
            string sql3 = "Select * From NhanVienYTe";
            thuvien.cbo(cboKhoa, sql, "TenKhoa", "MaKhoa");
            thuvien.cbo(cboThuoc, sql1, "TenThuoc", "MaThuoc");
            thuvien.cbo(cboKhoatk, sql, "TenKhoa", "MaKhoa");
            thuvien.cbo(cboBenhnhan, sql2, "HoTenBenhNhan", "MaBenhNhan");
            thuvien.cbo(cboBacsi, sql3, "BacSiDieuTri", "MaNhanVien");
            thuvien.cbo(cboBenhnhantk, sql2, "HoTenBenhNhan", "MaBenhNhan");
            thuvien.cbo(cboBacsitk, sql3, "BacSiDieuTri", "MaNhanVien");
        }

        public bool checktrungma(string ma)
        {
            if (thuvien.con.State == ConnectionState.Closed)
            {
                thuvien.con.Open();
            }
            string sql = "Select count(*) from QuaTrinhDieuTri Where MaDieuTri='" + ma + "'";
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            String ma = txtMadt.Text.Trim();
            String bn = cboBenhnhan.SelectedValue.ToString();
            String bs = cboBacsi.SelectedValue.ToString();
            DateTime ngay = dateNgaydt.Value;
            String cd = txtChandoan.Text.Trim();
            String pp = txtPhuongphap.Text.Trim();
            String khoa = cboKhoa.SelectedValue.ToString();
            String thuoc = cboThuoc.SelectedValue.ToString();

            if (checktrungma(ma))
            {
                MessageBox.Show("Trùng mã!");
                txtMadt.Focus();
                return;
            }
            if (ma == "")
            {
                MessageBox.Show("Chưa nhập mã!");
                txtMadt.Focus();
                return;
            }

            string sql = "Insert QuaTrinhDieuTri Values('" + ma + "',N'" + bn + "',N'" + bs + "','" + ngay + "',N'" + cd + "',N'" + pp + "',N'" + khoa + "',N'" + thuoc + "')";
            thuvien.insert(sql);
            MessageBox.Show("Thêm mới quá trình điều trị thành công!");
            load_qtdt();
            load_cbo();
            xoa();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            String ma = txtMadt.Text.Trim();
            String bn = cboBenhnhan.SelectedValue.ToString();
            String bs = cboBacsi.SelectedValue.ToString();
            DateTime ngay = dateNgaydt.Value;
            String cd = txtChandoan.Text.Trim();
            String pp = txtPhuongphap.Text.Trim();
            String khoa = cboKhoa.SelectedValue.ToString();
            String thuoc = cboThuoc.SelectedValue.ToString();

            string sql = "UPDATE QuaTrinhDieuTri SET MaBenhNhan = N'" + bn +
             "', MaNhanVien = N'" + bs +
             "', NgayDieuTri = '" + ngay +
             "', ChanDoanDieuTri = N'" + cd +
             "', PhuongPhapDieuTri = N'" + pp +
             "', MaKhoa = N'" + khoa +
             "', MaThuoc = N'" + thuoc +
             "' WHERE MaDieuTri = '" + ma + "'";

            thuvien.insert(sql);
            MessageBox.Show("Cập nhật thuốc thành công!");
            load_qtdt();
            load_cbo();
            xoa();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String ma = txtMadt.Text.Trim();
            string sql = "DELETE FROM QuaTrinhDieuTri WHERE MaDieuTri = '" + ma + "'";
            thuvien.insert(sql);
            MessageBox.Show("Xoá thuốc thành công!");
            load_qtdt();
            load_cbo();
            xoa();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtMadt.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            cboBenhnhan.SelectedValue = dataGridView1.Rows[i].Cells[1].Value.ToString();
            cboBacsi.SelectedValue = dataGridView1.Rows[i].Cells[2].Value.ToString();
            dateNgaydt.Value = DateTime.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
            txtChandoan.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            txtPhuongphap.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            cboKhoa.SelectedValue = dataGridView1.Rows[i].Cells[6].Value.ToString();
            cboThuoc.SelectedValue = dataGridView1.Rows[i].Cells[7].Value.ToString();
            txtMadt.Enabled = false;
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            String bn;
            if (cboBenhnhantk.SelectedItem == null)
            {
                bn = "";
            }
            else
            {
                bn = cboBenhnhantk.SelectedValue.ToString();
            }

            String bs;
            if (cboBacsitk.SelectedItem == null)
            {
                bs = "";
            }
            else
            {
                bs = cboBacsitk.SelectedValue.ToString();
            }

            String cd = txtChandoantk.Text.Trim();

            String khoa;
            if (cboKhoatk.SelectedItem == null)
            {
                khoa = "";
            }
            else
            {
                khoa = cboKhoatk.SelectedValue.ToString();
            }

            string sql = "SELECT QuaTrinhDieuTri.*, BenhNhan.HoTenBenhNhan, " +
                     "NhanVienYTe.BacSiDieuTri, Khoa.TenKhoa, Thuoc.TenThuoc " +
                     "FROM QuaTrinhDieuTri " +
                     "JOIN BenhNhan ON QuaTrinhDieuTri.MaBenhNhan = BenhNhan.MaBenhNhan " +
                     "JOIN NhanVienYTe ON QuaTrinhDieuTri.MaNhanVien = NhanVienYTe.MaNhanVien " +
                     "JOIN Khoa ON QuaTrinhDieuTri.MaKhoa = Khoa.MaKhoa " +
                     "LEFT JOIN Thuoc ON QuaTrinhDieuTri.MaThuoc = Thuoc.MaThuoc " +
                     "WHERE BenhNhan.HoTenBenhNhan LIKE N'%" + bn + "%' " +
                     "AND NhanVienYTe.BacSiDieuTri LIKE N'%" + bs + "%' " +
                     "AND QuaTrinhDieuTri.ChanDoanDieuTri LIKE N'%" + cd + "%' " +
                     "AND Khoa.TenKhoa LIKE N'%" + khoa + "%'";

            thuvien.load(dataGridView1, sql);
            xoa();
            load_cbo();
        }

        private void Quatrinhdieutri_Load(object sender, EventArgs e)
        {
            load_qtdt();
            load_cbo();
        }
    }
}
