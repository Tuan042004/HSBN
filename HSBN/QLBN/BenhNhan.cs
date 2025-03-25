using HSBN.QLBN;
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
    public partial class BenhNhan : Form
    {
        public BenhNhan()
        {
            InitializeComponent();
            load_BenhNhan();
            close();

        }
        public void load_BenhNhan()
        {
            string sql = "SELECT MaBenhNhan, HoTenBenhNhan, NgaySinh, GioiTinh, DiaChi, SDT, CCCD, MBHYT FROM BenhNhan";
            data.hienThi(dgvBenhNhan, sql);
        }
        public void close()
        {
            txtMaBN.ReadOnly = true;
            txtHoten.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            txtSDT.ReadOnly = true;
            cbxGioiTinh.Enabled = false;
            dtNgaySinh.Enabled = false;
            txtBHYT.ReadOnly = true;
            txtCCCD.ReadOnly = true;
            btnSua.Visible = false;
            btnXoa.Visible = false;
            btnLuu.Visible = false;

        }
        public void open()
        {
            txtMaBN.ReadOnly = false;
            txtHoten.ReadOnly = false;
            txtDiaChi.ReadOnly = false;
            txtSDT.ReadOnly = false;
            cbxGioiTinh.Enabled = true;
            dtNgaySinh.Enabled = true;
            txtBHYT.ReadOnly = false;
            txtCCCD.ReadOnly = false;
        }
        public void xoatrang()
        {
            txtMaBN.Clear();
            txtHoten.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            cbxGioiTinh.SelectedItem = null;
            txtTkHoTen.Clear();
            txtTkMaBN.Clear();
            txtBHYT.Clear();
            txtCCCD.Clear();
            cbxTkGioiTinh.SelectedItem = null;
            txtTKCCCD.Clear();
            txtMaBN.ReadOnly = false;
        }
        public bool check_trungMaBenhNhan(string maBN)
        {
            if (data.con.State == ConnectionState.Closed)
                data.con.Open();

            // Tạo câu lệnh SQL kiểm tra mã bệnh nhân có tồn tại không
            string sql = "SELECT COUNT(*) FROM BenhNhan WHERE MaBenhNhan='" + maBN + "'";
            SqlCommand cmd = new SqlCommand(sql, data.con);
            int kqua = (int)cmd.ExecuteScalar();

            data.con.Close(); // Đóng kết nối sau khi kiểm tra

            if (kqua > 0)
                return true;
            else
                return false;
        }
        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void grbThongTin_Enter(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            {
                string maBN = txtMaBN.Text.Trim();
                string hoTen = txtHoten.Text.Trim();
                DateTime ns = dtNgaySinh.Value;
                string gt;
                if (cbxGioiTinh.SelectedItem == null)
                    gt = "";
                else
                    gt = cbxGioiTinh.SelectedItem.ToString();
                string sdt = txtSDT.Text.Trim();
                string dc = txtDiaChi.Text.Trim();
                string cccd = txtCCCD.Text.Trim();
                string mbhyt = txtBHYT.Text.Trim();

                // Kiểm tra thông tin bắt buộc
                if (string.IsNullOrWhiteSpace(maBN) ||
                    string.IsNullOrWhiteSpace(hoTen) ||
                    string.IsNullOrWhiteSpace(sdt) ||
                    string.IsNullOrWhiteSpace(gt) ||
                    string.IsNullOrWhiteSpace(dc) ||
                    string.IsNullOrWhiteSpace(cccd))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin bệnh nhân");
                    return;
                }

                // Kiểm tra trùng mã bệnh nhân
                if (check_trungMaBenhNhan(maBN))
                {
                    MessageBox.Show("Mã bệnh nhân đã tồn tại", "THÔNG BÁO");
                    txtMaBN.Focus();
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
                string patternBHYT = @"^\d{10}$";
                if (!Regex.IsMatch(mbhyt, patternBHYT))
                {
                    MessageBox.Show("Số BHYT không hợp lệ! Vui lòng nhập số có 10 chữ số!!");
                    return;
                }
                // Câu lệnh SQL để chèn dữ liệu
                string sql = "INSERT INTO BenhNhan VALUES ('" + maBN + "',N'" + hoTen + "','" + ns + "',N'" + gt + "','" + dc + "','" + sdt + "','" + cccd + "','" + mbhyt + "')";
                data.insert(sql);
                MessageBox.Show("Lưu thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                data.con.Close();
                load_BenhNhan();
            }
        }

        private void dgvBenhNhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                if (e.RowIndex >= 0) // Đảm bảo người dùng không click vào tiêu đề cột
                {
                    int i = e.RowIndex;  
                    txtMaBN.Text = dgvBenhNhan.Rows[i].Cells[0].Value.ToString();
                    txtHoten.Text = dgvBenhNhan.Rows[i].Cells[1].Value.ToString();
                    dtNgaySinh.Value = DateTime.Parse(dgvBenhNhan.Rows[i].Cells[2].Value.ToString());
                    cbxGioiTinh.Text = dgvBenhNhan.Rows[i].Cells[3].Value.ToString();
                    txtDiaChi.Text = dgvBenhNhan.Rows[i].Cells[4].Value.ToString();
                    txtSDT.Text = dgvBenhNhan.Rows[i].Cells[5].Value.ToString();
                    txtCCCD.Text = dgvBenhNhan.Rows[i].Cells[6].Value.ToString();
                    txtBHYT.Text = dgvBenhNhan.Rows[i].Cells[7].Value.ToString();
                    btnSua.Visible = true;
                    btnXoa.Visible = true;
                    btnLuu.Visible = false;
                    open();
                    btnThem.Visible = false;
                    txtMaBN.ReadOnly = true; // Khóa chỉnh sửa mã bệnh nhân
            }
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
                string maBN = txtMaBN.Text.Trim();
                string hoTen = txtHoten.Text.Trim();
                DateTime ns = dtNgaySinh.Value;
                string gt;
                if (cbxGioiTinh.SelectedItem == null)
                    gt = "";
                else
                    gt = cbxGioiTinh.SelectedItem.ToString();
                string sdt = txtSDT.Text.Trim();
                string dc = txtDiaChi.Text.Trim();
                string cccd = txtCCCD.Text.Trim();
                string mbhyt = txtBHYT.Text.Trim();

                if (string.IsNullOrWhiteSpace(maBN) ||
                   string.IsNullOrWhiteSpace(hoTen) ||
                   string.IsNullOrWhiteSpace(sdt) ||
                   string.IsNullOrWhiteSpace(gt) ||
                   string.IsNullOrWhiteSpace(dc) ||
                   string.IsNullOrWhiteSpace(cccd))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin bệnh nhân");
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
                string patternBHYT = @"^\d{10}$";
                if (!Regex.IsMatch(mbhyt, patternBHYT))
                    {
                        MessageBox.Show("Số BHYT không hợp lệ! Vui lòng nhập số có 10 chữ số!!");
                        return;
                    }
            string sql = "UPDATE BenhNhan SET HoTenBenhNhan = N'" + hoTen + "', " +
                "NgaySinh = '" + ns + "', " +
                "GioiTinh = N'" + gt + "', " +
                "DiaChi = N'" + dc + "', " +
                "SDT = '" + sdt + "', " +
                "CCCD = '" + cccd + "', " +
                "MBHYT = '" + mbhyt + "' " +
                "WHERE MaBenhNhan = '" + maBN + "'";
                data.update(sql);
                MessageBox.Show("Cập nhật thành công!");
                load_BenhNhan();
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có chắc chắn muốn xoá bệnh nhân?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (kq == DialogResult.OK)
            {
                string maBN = dgvBenhNhan.CurrentRow.Cells[0].Value.ToString();
                string sql = "DELETE FROM BenhNhan WHERE MaBenhNhan = '" + maBN + "'";
                data.delete(sql);
                MessageBox.Show("Xóa bệnh nhân thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load_BenhNhan(); // Load lại danh sách bệnh nhân
                xoatrang(); // Xóa dữ liệu trên form
            }
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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tim_ma = txtTkMaBN.Text.Trim();
            string tim_ten = txtTkHoTen.Text.Trim();
            string tim_gioitinh;
            if (cbxTkGioiTinh.SelectedItem == null)
                tim_gioitinh = "";
            else
                tim_gioitinh = cbxTkGioiTinh.SelectedItem.ToString();
            string tim_cccd = txtTKCCCD.Text.Trim();

            if (data.con.State == ConnectionState.Closed)
                data.con.Open();

            String search = "SELECT * FROM BenhNhan " +
                "WHERE MaBenhNhan LIKE '%" + tim_ma + "%' " +
                "AND HoTenBenhNhan LIKE N'%" + tim_ten + "%' " +
                "AND GioiTinh LIKE N'%" + tim_gioitinh + "%' " +
                "AND CCCD LIKE '%" + tim_cccd + "%'";

            SqlCommand cmd = new SqlCommand(search, data.con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            data.con.Close();

            dgvBenhNhan.DataSource = tb;
            dgvBenhNhan.Refresh();
            xoatrang();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Visible = false;
            btnXoa.Visible = false;
            btnLuu.Visible = true;
            btnThem.Visible = false;
            open();
        }
        private void nhapSo(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho nhập ký tự
            }
        }

        private void BenhNhan_Load(object sender, EventArgs e)
        {

        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
            txtSDT.KeyPress += new KeyPressEventHandler(nhapSo);
        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvBenhNhan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtCCCD_TextChanged(object sender, EventArgs e)
        {
            txtCCCD.KeyPress += new KeyPressEventHandler(nhapSo);
        }

        private void txtBHYT_TextChanged(object sender, EventArgs e)
        {
            txtBHYT.KeyPress += new KeyPressEventHandler(nhapSo);
        }
    }
}

