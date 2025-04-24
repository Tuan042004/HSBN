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
using xls = Microsoft.Office.Interop.Excel;

namespace HSBN.QTDT
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
            txtSongaynv.Text = "";
            txtGioitinh.Text = "";
            txtLydokham.Text = "";
            txtMadt.Enabled = true;
        }
        private void load_qtdt()
        {
            string sql = "SELECT QuaTrinhDieuTri.*, NhanVienYTe.BacSiDieuTri " +
                "FROM QuaTrinhDieuTri " +
                "JOIN NhanVienYTe ON QuaTrinhDieuTri.MaNhanVien = NhanVienYTe.MaNhanVien ";

            thuvien.load(dataGridView1, sql);
        }

        private void load_cbo()
        {
            string sql = "Select * From Khoa";
            string sql2 = "Select * From BenhNhan";
            string sql3 = "Select * From NhanVienYTe";
            string sql4 = "Select * From PhongBenh";
            string sql5 = "Select * From DichVu";
            thuvien.cbo(cboKhoa, sql, "TenKhoa", "MaKhoa");
            thuvien.cbo(cboBenhnhan, sql2, "HoTenBenhNhan", "MaBenhNhan");
            thuvien.cbo(cboBacsi, sql3, "BacSiDieuTri", "MaNhanVien");
            thuvien.cbo(cboPhong, sql4, "TenPhong", "MaPhong");
            thuvien.cbo(cbopp, sql5, "TenDichVu", "TenDichVu");
        }

        private void load_Benhnhan()
        {
            string sql = "SELECT HoSoNhapVien.*, BenhNhan.HoTenBenhNhan, BenhNhan.NgaySinh, BenhNhan.GioiTinh, " +
                "PhongBenh.TenPhong, Khoa.TenKhoa " +
                "FROM HoSoNhapVien " +
                "JOIN BenhNhan ON HoSoNhapVien.MaBenhNhan = BenhNhan.MaBenhNhan " +
                "JOIN PhongBenh ON HoSoNhapVien.MaPhong = PhongBenh.MaPhong " +
                "JOIN Khoa ON HoSoNhapVien.MaKhoa = Khoa.MaKhoa";

            thuvien.load(dataGridView2, sql);
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
            String tenbn = txtTenbn.Text.Trim();
            String bs = cboBacsi.SelectedValue.ToString();
            DateTime ngay = dateNgaydt.Value;
            String cd = txtChandoan.Text.Trim();
            String pp = cbopp.SelectedValue.ToString();
            String songay = txtSongaynv.Text.Trim();
            String khoa = cboKhoa.SelectedValue.ToString();
            String tenkhoa = txtTenkhoa.Text.Trim();

            if (checktrungma(ma))
            {
                MessageBox.Show("Trùng mã!");
                txtMadt.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(ma) ||
               string.IsNullOrWhiteSpace(bn) ||
               string.IsNullOrWhiteSpace(tenbn) ||
                string.IsNullOrWhiteSpace(bs) ||
                string.IsNullOrWhiteSpace(cd) ||
                string.IsNullOrWhiteSpace(pp) ||
                string.IsNullOrWhiteSpace(songay) ||
                string.IsNullOrWhiteSpace(khoa) ||
                string.IsNullOrWhiteSpace(tenkhoa))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            string sql = "Insert QuaTrinhDieuTri Values('" + ma + "',N'" + bn + "',N'" + tenbn + "',N'" + bs + "','" + ngay + "',N'" + cd + "',N'" + pp + "','" + songay + "',N'" + khoa + "',N'" + tenkhoa + "')";
            thuvien.insert(sql);
            MessageBox.Show("Thêm mới quá trình điều trị thành công!");
            load_qtdt();
            load_cbo();
            xoa();
        }

        private void Quatrinhdieutri_Load(object sender, EventArgs e)
        {
            load_qtdt();
            load_Benhnhan();
            load_cbo();
            txtTenbn.Enabled = false;
            txtLydokham.Enabled = false;
            txtGioitinh.Enabled = false;
            cboBenhnhan.Enabled = false;
            dateNgaysinh.Enabled = false;
            cboPhong.Enabled = false;
            txtTenkhoa.Enabled = false;
            txtTenbn.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            String ma = txtMadt.Text.Trim();
            String bn = cboBenhnhan.SelectedValue.ToString();
            String tenbn = txtTenbn.Text.Trim();
            String bs = cboBacsi.SelectedValue.ToString();
            DateTime ngay = dateNgaydt.Value;
            String cd = txtChandoan.Text.Trim();
            String pp = cbopp.SelectedValue.ToString();
            String songay = txtSongaynv.Text.Trim();
            String khoa = cboKhoa.SelectedValue.ToString();
            String tenkhoa = txtTenkhoa.Text.Trim();

            if (string.IsNullOrWhiteSpace(ma) ||
               string.IsNullOrWhiteSpace(bn) ||
               string.IsNullOrWhiteSpace(tenbn) ||
                string.IsNullOrWhiteSpace(bs) ||
                string.IsNullOrWhiteSpace(cd) ||
                string.IsNullOrWhiteSpace(pp) ||
                string.IsNullOrWhiteSpace(songay) ||
                string.IsNullOrWhiteSpace(khoa) ||
                string.IsNullOrWhiteSpace(tenkhoa))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            string sql = "UPDATE QuaTrinhDieuTri SET MaBenhNhan = N'" + bn +
             "', TenBenhNhan = N'" + tenbn +
             "', MaNhanVien = N'" + bs +
             "', NgayDieuTri = '" + ngay +
             "', ChanDoanDieuTri = N'" + cd +
             "', PhuongPhapDieuTri = N'" + pp +
             "', SoNgayNhapVien = '" + songay +
             "', MaKhoa = '" + khoa +
             "', TenKhoa = N'" + tenkhoa +
             "' WHERE MaDieuTri = '" + ma + "'";

            thuvien.insert(sql);
            MessageBox.Show("Cập nhật qtdt thành công!");
            load_qtdt();
            load_cbo();
            xoa();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String ma = txtMadt.Text.Trim();
            string sql = "DELETE FROM QuaTrinhDieuTri WHERE MaDieuTri = '" + ma + "'";
            thuvien.insert(sql);
            MessageBox.Show("Xoá qtdt thành công!");
            load_qtdt();
            load_cbo();
            xoa();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtMadt.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            cboBenhnhan.SelectedValue = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtTenbn.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            cboBacsi.SelectedValue = dataGridView1.Rows[i].Cells[3].Value.ToString();
            dateNgaydt.Value = DateTime.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
            txtChandoan.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            cbopp.SelectedValue = dataGridView1.Rows[i].Cells[6].Value.ToString();
            txtSongaynv.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
            cboKhoa.SelectedValue = dataGridView1.Rows[i].Cells[8].Value.ToString();
            txtTenkhoa.Text = dataGridView1.Rows[i].Cells[9].Value.ToString();
            txtMadt.Enabled = false;
        }

        //private void btnTimkiem_Click(object sender, EventArgs e)
        //{
        //    String bn;
        //    if (cboBenhnhantk.SelectedItem == null)
        //    {
        //        bn = "";
        //    }
        //    else
        //    {
        //        bn = cboBenhnhantk.SelectedValue.ToString();
        //    }

        //    String bs;
        //    if (cboBacsitk.SelectedItem == null)
        //    {
        //        bs = "";
        //    }
        //    else
        //    {
        //        bs = cboBacsitk.SelectedValue.ToString();
        //    }

        //    String cd = txtChandoantk.Text.Trim();

        //    String khoa;
        //    if (cboKhoatk.SelectedItem == null)
        //    {
        //        khoa = "";
        //    }
        //    else
        //    {
        //        khoa = cboKhoatk.SelectedValue.ToString();
        //    }

        //    string sql = "SELECT QuaTrinhDieuTri.*, BenhNhan.HotenBenhNhan, " +
        //             "NhanVienYTe.BacSiDieuTri, Khoa.TenKhoa, Thuoc.TenThuoc " +
        //             "FROM QuaTrinhDieuTri " +
        //             "JOIN BenhNhan ON QuaTrinhDieuTri.MaBenhNhan = BenhNhan.MaBenhNhan " +
        //             "JOIN NhanVienYTe ON QuaTrinhDieuTri.MaNhanVien = NhanVienYTe.MaNhanVien " +
        //             "JOIN Khoa ON QuaTrinhDieuTri.MaKhoa = Khoa.MaKhoa " +
        //             "LEFT JOIN Thuoc ON QuaTrinhDieuTri.MaThuoc = Thuoc.MaThuoc " +
        //             "WHERE BenhNhan.MaBenhNhan LIKE N'%" + bn + "%' " +
        //             "AND NhanVienYTe.MaNhanVien LIKE N'%" + bs + "%' " +
        //             "AND QuaTrinhDieuTri.ChanDoanDieuTri LIKE N'%" + cd + "%' " +
        //             "AND Khoa.MaKhoa LIKE N'%" + khoa + "%'";

        //    thuvien.load(dataGridView1, sql);
        //    xoa();
        //    load_cbo();
        //}

        private void ReadExcel(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                MessageBox.Show("Chưa chọn file", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            xls.Application Excel = new xls.Application();
            xls.Workbook wb = Excel.Workbooks.Open(filename);

            try
            {
                foreach (xls.Worksheet ws in wb.Sheets)
                {
                    int i = 2; // Bắt đầu đọc từ dòng thứ 2 (bỏ qua tiêu đề)
                    while (ws.Cells[i, 1].Value != null)
                    {
                        string ma = ws.Cells[i, 1].Value?.ToString().Trim();
                        string bn = ws.Cells[i, 2].Value?.ToString().Trim();
                        string tenbn = ws.Cells[i, 3].Value?.ToString().Trim();
                        string bs = ws.Cells[i, 4].Value?.ToString().Trim();
                        string cd = ws.Cells[i, 6].Value?.ToString().Trim();
                        string pp = ws.Cells[i, 7].Value?.ToString().Trim();
                        string songay = ws.Cells[i, 8].Value?.ToString().Trim();
                        string khoa = ws.Cells[i, 9].Value?.ToString().Trim();
                        string tenkhoa = ws.Cells[i, 10].Value?.ToString().Trim();

                        // Xử lý ngày sinh
                        DateTime ngaySinh;
                        if (!DateTime.TryParse(ws.Cells[i, 4].Value?.ToString(), out ngaySinh))
                        {
                            ngaySinh = DateTime.MinValue; // Nếu lỗi, đặt giá trị mặc định
                        }

                        // Thêm vào cơ sở dữ liệu
                        qtdthaha(ma, bn, tenbn, bs, ngaySinh, cd, pp, songay, khoa, tenkhoa);
                        i++;
                    }
                }

                MessageBox.Show("Nhập dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load_qtdt();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                wb.Close(false);
                Excel.Quit();
            }
        }

        private void qtdthaha(string ma, string bn, string tenbn, string bs, DateTime ngaySinh, string cd, string pp, string songay, string khoa, string tenkhoa)
        {
            String ngaySinhSql = ngaySinh.ToString("yyyy-MM-dd");
            string sql = "Insert QuaTrinhDieuTri Values('" + ma + "',N'" + bn + "',N'" + tenbn + "',N'" + bs + "','" + ngaySinh + "',N'" + cd + "',N'" + pp + "','" + songay + "',N'" + khoa + "',N'" + tenkhoa + "')";
            thuvien.insert(sql);

        }

        private void btnXuatexcel_Click(object sender, EventArgs e)
        {
            ////String bn;
            ////if (cboBenhnhantk.SelectedItem == null)
            ////{
            ////    bn = "";
            ////}
            ////else
            ////{
            ////    bn = cboBenhnhantk.SelectedValue.ToString();
            ////}

            ////String bs;
            ////if (cboBacsitk.SelectedItem == null)
            ////{
            ////    bs = "";
            ////}
            ////else
            ////{
            ////    bs = cboBacsitk.SelectedValue.ToString();
            ////}

            ////String cd = txtChandoantk.Text.Trim();

            ////String khoa;
            ////if (cboKhoatk.SelectedItem == null)
            ////{
            ////    khoa = "";
            ////}
            ////else
            ////{
            ////    khoa = cboKhoatk.SelectedValue.ToString();
            ////}

            ////string sql = "SELECT QuaTrinhDieuTri.*, BenhNhan.HotenBenhNhan, " +
            ////         "NhanVienYTe.BacSiDieuTri, Khoa.TenKhoa, Thuoc.TenThuoc " +
            ////         "FROM QuaTrinhDieuTri " +
            ////         "JOIN BenhNhan ON QuaTrinhDieuTri.MaBenhNhan = BenhNhan.MaBenhNhan " +
            ////         "JOIN NhanVienYTe ON QuaTrinhDieuTri.MaNhanVien = NhanVienYTe.MaNhanVien " +
            ////         "JOIN Khoa ON QuaTrinhDieuTri.MaKhoa = Khoa.MaKhoa " +
            ////         "LEFT JOIN Thuoc ON QuaTrinhDieuTri.MaThuoc = Thuoc.MaThuoc " +
            ////         "WHERE BenhNhan.MaBenhNhan LIKE N'%" + bn + "%' " +
            ////         "AND NhanVienYTe.MaNhanVien LIKE N'%" + bs + "%' " +
            ////         "AND QuaTrinhDieuTri.ChanDoanDieuTri LIKE N'%" + cd + "%' " +
            ////         "AND Khoa.MaKhoa LIKE N'%" + khoa + "%'";

            ////xoa();
            ////load_cbo();
            //DataTable tb = new DataTable();
            ////thuvien.excel(tb, sql);
            //// Tạo các cột từ DataGridView
            //foreach (DataGridViewColumn column in dataGridView1.Columns)
            //{
            //    tb.Columns.Add(column.HeaderText);
            //}

            //// Lấy dữ liệu từ DataGridView
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    if (!row.IsNewRow)
            //    {
            //        DataRow dRow = tb.NewRow();
            //        for (int i = 0; i < dataGridView1.Columns.Count; i++)
            //        {
            //            dRow[i] = row.Cells[i].Value;
            //        }
            //        tb.Rows.Add(dRow);
            //    }
            //}

            //if (tb.Rows.Count == 0)
            //{
            //    MessageBox.Show("Không có dữ liệu để xuất Excel!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //ExportExcel(tb, "DS QTDT");
        }

        private void btnNhapexcel_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "excel file |*.xls;*.xlsx";
            openFileDialog1.FilterIndex = 1;//trỏ vào vị trí đầu tiên của bộ lọc
            openFileDialog1.RestoreDirectory = true;//nhớ đường dẫn của lần truy cập
            openFileDialog1.Multiselect = false;//ko cho phép chọn nhiều file 1 lần

            DialogResult kq = openFileDialog1.ShowDialog();
            if (kq == DialogResult.OK)
            {
                //textBox1.Text = openFileDialog1.FileName;
                string tenfile = openFileDialog1.FileName;
                ReadExcel(tenfile);
                load_qtdt();
                load_cbo();
                xoa();
            }
        }

        private void cboKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy giá trị MaKhoa hoặc TenKhoa tùy theo cách bạn bind dữ liệu
            string tenKhoa = cboKhoa.Text;

            string sql = "SELECT HoSoNhapVien.*, " +
                         "BenhNhan.HoTenBenhNhan, BenhNhan.NgaySinh, BenhNhan.GioiTinh, " +
                         "PhongBenh.TenPhong, Khoa.TenKhoa " +
                         "FROM HoSoNhapVien " +
                         "JOIN BenhNhan ON HoSoNhapVien.MaBenhNhan = BenhNhan.MaBenhNhan " +
                         "JOIN PhongBenh ON HoSoNhapVien.MaPhong = PhongBenh.MaPhong " +
                         "JOIN Khoa ON HoSoNhapVien.MaKhoa = Khoa.MaKhoa " +
                         "WHERE Khoa.TenKhoa = N'" + tenKhoa + "'";

            thuvien.load(dataGridView2, sql);

            string sql1 = "Select * From NhanVienYTe WHERE TenKhoa = N'" + tenKhoa + "'";
            thuvien.cbo(cboBacsi, sql1, "BacSiDieuTri", "MaNhanVien");

            txtTenkhoa.Text = cboKhoa.Text;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                // Lấy thông tin bệnh nhân được chọn
                DataGridViewRow row = dataGridView2.CurrentRow;

                // Lấy các giá trị cần thiết của bệnh nhân
                cboBenhnhan.Text = row.Cells["dataGridViewTextBoxColumn3"].Value.ToString();
                txtGioitinh.Text = row.Cells["Column18"].Value.ToString();
                txtLydokham.Text = row.Cells["dataGridViewTextBoxColumn11"].Value.ToString();
                cboPhong.Text = row.Cells["dataGridViewTextBoxColumn8"].Value.ToString();

                if (row.Cells["Column17"].Value != DBNull.Value)
                {
                    dateNgaysinh.Value = Convert.ToDateTime(row.Cells["Column17"].Value.ToString());
                }
                else
                {
                    dateNgaysinh.Value = DateTime.Now;
                }
            }
        }

        private void cboBenhnhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTenbn.Text = cboBenhnhan.Text;
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            // Tạo instance của Form2
            Timkiem tk = new Timkiem();

            // Ẩn form hiện tại (Form1)
            this.Hide();

            // Hiển thị Form2
            tk.ShowDialog();

            // Khi Form2 đóng lại, hiện lại Form1 nếu cần
            this.Show();
        }
    }
}
