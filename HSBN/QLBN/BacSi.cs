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

namespace HSBN.QLBN
{
    public partial class BacSi : Form
    {
        public BacSi()
        {
            InitializeComponent();
            load_TenKhoa();
            load_NhanVienYTe();
            close();

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        public void close()
        {
            txtMaNhanVien.ReadOnly = true;
            txtBacSiDieuTri.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            txtSDT.ReadOnly = true;
            txtCCCD.ReadOnly = true;
            cbxGioiTinh.Enabled = false;
            cbxTenKhoa.Enabled = false;
            dtNgaySinh.Enabled = false;
            btnSua.Visible = false;
            btnXoa.Visible = false;
            btnLuu.Visible = false;
        }
        public void open()
        {
            txtMaNhanVien.ReadOnly = false;
            txtBacSiDieuTri.ReadOnly = false;
            txtDiaChi.ReadOnly = false;
            txtSDT.ReadOnly = false;
            txtCCCD.ReadOnly = false;
            cbxGioiTinh.Enabled = true;
            cbxTenKhoa.Enabled = true;
            dtNgaySinh.Enabled = true;
        }
        public void xoatrang()
        {
            txtMaNhanVien.Clear();
            txtBacSiDieuTri.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtCCCD.Clear();
            txtTkMaBN.Clear();
            txtTkHoTen.Clear();
            cbxTkGioiTinh.SelectedItem = null;
            cbxTkTenKhoa.SelectedItem = null;
            cbxGioiTinh.SelectedItem = null;
            cbxTenKhoa.SelectedItem = null;
            txtMaNhanVien.ReadOnly = false;
        }
        private void nhapSo(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho nhập ký tự
            }
        }
        public void load_TenKhoa()
        {
            string sql = "SELECT * FROM Khoa";
            data.hienThiComboBox(cbxTenKhoa, sql, "TenKhoa", "TenKhoa");
            data.hienThiComboBox(cbxTkTenKhoa, sql, "TenKhoa", "TenKhoa");
        }

        private void load_NhanVienYTe()
        {
            if (data.con.State == ConnectionState.Closed)
                data.con.Open();

            string sql = "SELECT * FROM NhanVienYTe";
            SqlCommand cmd = new SqlCommand(sql, data.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvBenhNhan.DataSource = dt;
            dgvBenhNhan.Refresh();

            cmd.Dispose();
            data.con.Close();
        }
        public bool check_trungMaNhanVien(string maNV)
        {
            if (data.con.State == ConnectionState.Closed)
                data.con.Open();

            string sql = "SELECT COUNT(*) FROM NhanVienYTe WHERE MaNhanVien = @maNV";
            SqlCommand cmd = new SqlCommand(sql, data.con);
            cmd.Parameters.AddWithValue("@maNV", maNV);

            int kqua = (int)cmd.ExecuteScalar();

            data.con.Close();

            return kqua > 0;
        }
        private void dgvBenhNhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int i = e.RowIndex;
                txtMaNhanVien.Text = dgvBenhNhan.Rows[i].Cells[0].Value.ToString();
                cbxTenKhoa.Text = dgvBenhNhan.Rows[i].Cells[4].Value.ToString();
                txtBacSiDieuTri.Text = dgvBenhNhan.Rows[i].Cells[1].Value.ToString();
                dtNgaySinh.Value = DateTime.Parse(dgvBenhNhan.Rows[i].Cells[2].Value.ToString());
                cbxGioiTinh.Text = dgvBenhNhan.Rows[i].Cells[3].Value.ToString();
                txtDiaChi.Text = dgvBenhNhan.Rows[i].Cells[5].Value.ToString();
                txtSDT.Text = dgvBenhNhan.Rows[i].Cells[6].Value.ToString();
                txtCCCD.Text = dgvBenhNhan.Rows[i].Cells[7].Value.ToString();
                btnSua.Visible = true;
                btnXoa.Visible = true;
                btnLuu.Visible = false;
                open();
                btnThem.Visible = false;
                txtMaNhanVien.ReadOnly = true; // Không cho sửa Mã bác sĩ

            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNhanVien.Text.Trim();
            string tenKhoa = cbxTenKhoa.SelectedValue?.ToString();
            string bacSi = txtBacSiDieuTri.Text.Trim();
            DateTime ns = dtNgaySinh.Value;
            string gt = cbxGioiTinh.SelectedItem == null ? "" : cbxGioiTinh.SelectedItem.ToString();
            string sdt = txtSDT.Text.Trim();
            string dc = txtDiaChi.Text.Trim();
            string cccd = txtCCCD.Text.Trim();
            string tenkhoa = cbxTenKhoa.SelectedValue.ToString();
            // Kiểm tra thông tin bắt buộc
            if (string.IsNullOrWhiteSpace(maNV) ||
                string.IsNullOrWhiteSpace(tenKhoa) ||
                string.IsNullOrWhiteSpace(bacSi) ||
                string.IsNullOrWhiteSpace(sdt) ||
                string.IsNullOrWhiteSpace(gt) ||
                string.IsNullOrWhiteSpace(dc) ||
                string.IsNullOrWhiteSpace(cccd) ||
                string.IsNullOrWhiteSpace(tenkhoa))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bác sĩ");
                return;
            }

            // Kiểm tra trùng mã bác sĩ
            if (check_trungMaNhanVien(maNV))
            {
                MessageBox.Show("Mã bác sĩ đã tồn tại", "THÔNG BÁO");
                txtMaNhanVien.Focus();
                return;
            }

            string patternCCCD = @"^\d{12}$";
            if (!Regex.IsMatch(cccd, patternCCCD))
            {
                MessageBox.Show("Số CCCD không hợp lệ! Vui lòng nhập số có 12 chữ số !!");
                return;
            }
            // Kiểm tra định dạng số điện thoại
            string patternSDT = @"^(0)[0-9]{9}$";
            if (!Regex.IsMatch(sdt, patternSDT))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Vui lòng nhập số có 10 chữ số, bắt đầu bằng số 0!");
                return;
            }

            // Câu lệnh SQL để chèn dữ liệu
            string sql = "INSERT INTO NhanVienYTe VALUES ('" + maNV + "',N'" + bacSi + "','" + ns + "',N'" + gt + "',N'" + tenKhoa + "',N'" + dc + "','" + sdt + "','" + cccd + "')";
            data.insert(sql);
            data.con.Close();
            load_NhanVienYTe();
        }

        private void cbxTenKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (data.con.State == ConnectionState.Closed)
            //    data.con.Open();

            //string sql = "SELECT MaKhoa, TenKhoa FROM Khoa";
            //SqlDataAdapter da = new SqlDataAdapter(sql, data.con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

            //cbxTenKhoa.DataSource = dt;
            //cbxTenKhoa.DisplayMember = "TenKhoa"; // Hiển thị tên khoa
            //cbxTenKhoa.ValueMember = "TenKhoa";    // Giá trị thực tế là  tên khoa

            //data.con.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNhanVien.Text.Trim();
            string bacSi = txtBacSiDieuTri.Text.Trim();
            DateTime ns = dtNgaySinh.Value;
            string gt;
            if (cbxGioiTinh.SelectedItem == null)
                gt = "";
            else
                gt = cbxGioiTinh.SelectedItem.ToString();
            string sdt = txtSDT.Text.Trim();
            string dc = txtDiaChi.Text.Trim();
            string cccd = txtCCCD.Text.Trim();
            string tenKhoa = cbxTenKhoa.SelectedValue?.ToString();

            if (string.IsNullOrWhiteSpace(maNV) ||
               string.IsNullOrWhiteSpace(tenKhoa) ||
                string.IsNullOrWhiteSpace(bacSi) ||
                string.IsNullOrWhiteSpace(sdt) ||
                string.IsNullOrWhiteSpace(gt) ||
                string.IsNullOrWhiteSpace(dc) ||
                string.IsNullOrWhiteSpace(cccd) ||
                string.IsNullOrWhiteSpace(tenKhoa))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bác sĩ");
                return;
            }

            string patternCCCD = @"^\d{12}$";
            if (!Regex.IsMatch(cccd, patternCCCD))
            {
                MessageBox.Show("Số CCCD không hợp lệ! Vui lòng nhập số có 12 chữ số !!");
                return;
            }
            // Kiểm tra định dạng số điện thoại
            string patternSDT = @"^(0)[0-9]{9}$";
            if (!Regex.IsMatch(sdt, patternSDT))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Vui lòng nhập số có 10 chữ số, bắt đầu bằng số 0!");
                return;
            }

            string sql = "UPDATE NhanVienYTe SET " +
                "BacSiDieuTri = N'" + bacSi + "', " +
                "NgaySinh = '" + ns + "', " +
                "GioiTinh = N'" + gt + "', " +
                "DiaChi = N'" + dc + "', " +
                "SDT = '" + sdt + "', " +
                "CCCD = '" + cccd + "', " +
                "TenKhoa = N'" + tenKhoa + "' " +
                "WHERE MaNhanVien = '" + maNV + "'";

            data.update(sql);
            MessageBox.Show("Cập nhật thành công!");
            load_NhanVienYTe();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có chắc chắn muốn xoá bác sĩ?",
                                  "THÔNG BÁO",
                                  MessageBoxButtons.OKCancel,
                                  MessageBoxIcon.Warning);

            if (kq == DialogResult.OK)
            {
                if (dgvBenhNhan.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn bác sĩ cần xóa!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maNV = dgvBenhNhan.CurrentRow.Cells[0].Value.ToString();
                string sql = "DELETE FROM NhanVienYTe WHERE MaNhanVien = '" + maNV + "'";
                data.delete(sql);
                MessageBox.Show("Xóa bác sĩ thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load_NhanVienYTe();
                xoatrang();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tim_ma = txtTkMaBN.Text.Trim();
            string tim_ten = txtTkHoTen.Text.Trim();
            string tim_gioitinh = cbxTkGioiTinh.SelectedItem == null ? "" : cbxTkGioiTinh.SelectedItem.ToString();
            string tim_khoa = cbxTkTenKhoa.SelectedValue == null ? "" : cbxTkTenKhoa.SelectedValue.ToString();

            if (data.con.State == ConnectionState.Closed)
                data.con.Open();

            string search = "SELECT * FROM NhanVienYTe " +
                            "WHERE MaNhanVien LIKE '%" + tim_ma + "%' " +
                            "AND BacSiDieuTri LIKE N'%" + tim_ten + "%' " +
                            "AND GioiTinh LIKE N'%" + tim_gioitinh + "%' " +
                            "AND TenKhoa LIKE N'%" + tim_khoa + "%'";

            SqlCommand cmd = new SqlCommand(search, data.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            da.Fill(tb);

            cmd.Dispose();
            data.con.Close();

            dgvBenhNhan.DataSource = tb;
            dgvBenhNhan.Refresh();
            xoatrang();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            xoatrang();
            close();
            btnSua.Visible = false;
            btnXoa.Visible = false;
            btnLuu.Visible = false;
            btnThem.Visible = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Visible = false;
            btnXoa.Visible = false;
            btnLuu.Visible = true;
            btnThem.Visible = false;
            open();
        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
            txtSDT.KeyPress += new KeyPressEventHandler(nhapSo);
        }

        private void txtCCCD_TextChanged(object sender, EventArgs e)
        {
            txtCCCD.KeyPress += new KeyPressEventHandler(nhapSo);
        }
    }
}

