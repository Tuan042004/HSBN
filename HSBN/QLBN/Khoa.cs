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

namespace HSBN.QLBN
{
    public partial class Khoa : Form
    {
        public Khoa()
        {
            InitializeComponent();
            load_Khoa();
            btnSua.Visible = false;
            btnXoa.Visible = false;
            btnLuu.Visible = true;
        }
        public void load_Khoa()
        {
            string sql = "SELECT MaKhoa, TenKhoa FROM Khoa";
            data.hienThi(dgvKhoa, sql);
        }
        public bool check_trungMaKhoa(string maKhoa)
        {
            if (data.con.State == ConnectionState.Closed)
                data.con.Open();

            string sql = "SELECT COUNT(*) FROM Khoa WHERE MaKhoa = '" + maKhoa + "'";
            SqlCommand cmd = new SqlCommand(sql, data.con);
            int kqua = (int)cmd.ExecuteScalar();

            data.con.Close();
            return kqua > 0;
        }
        public void xoatrang()
        {
            txtTkMK.Clear();
            txtTKTenKhoa.Clear();
            txtMaKhoa.Clear();
            txtTenKhoa.Clear();
            txtMaKhoa.Focus();
        }

        private void txtMaBN_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maKhoa = txtMaKhoa.Text.Trim();
            string tenKhoa = txtTenKhoa.Text.Trim();

            // Kiểm tra thông tin bắt buộc
            if (string.IsNullOrWhiteSpace(maKhoa) || string.IsNullOrWhiteSpace(tenKhoa))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khoa", "THÔNG BÁO");
                return;
            }

            // Kiểm tra trùng mã khoa
            if (check_trungMaKhoa(maKhoa))
            {
                MessageBox.Show("Mã khoa đã tồn tại!", "THÔNG BÁO");
                txtMaKhoa.Focus();
                return;
            }

            // Câu lệnh SQL để chèn dữ liệu
            string sql = "INSERT INTO Khoa VALUES ('" + maKhoa + "', N'" + tenKhoa + "')";
            data.insert(sql);
            data.con.Close();
            MessageBox.Show("Thêm khoa thành công!", "THÔNG BÁO");

            // Load lại danh sách khoa
            load_Khoa();
            xoatrang();

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maKhoa = txtMaKhoa.Text.Trim();
            string tenKhoa = txtTenKhoa.Text.Trim();

            // Kiểm tra thông tin bắt buộc
            if (string.IsNullOrWhiteSpace(maKhoa) || string.IsNullOrWhiteSpace(tenKhoa))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khoa", "THÔNG BÁO");
                return;
            }

            // Câu lệnh SQL để cập nhật dữ liệu
            string sql = "UPDATE Khoa SET TenKhoa = N'" + tenKhoa + "' WHERE MaKhoa = '" + maKhoa + "'";
            data.update(sql);
            MessageBox.Show("Cập nhật khoa thành công!", "THÔNG BÁO");

            // Load lại danh sách khoa
            load_Khoa();
            xoatrang();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có chắc chắn muốn xoá khoa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (kq == DialogResult.OK)
            {
                string maKhoa = dgvKhoa.CurrentRow.Cells[0].Value.ToString();
                string sql = "DELETE FROM Khoa WHERE MaKhoa = '" + maKhoa + "'";
                data.delete(sql);
                MessageBox.Show("Xóa khoa thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load_Khoa(); // Load lại danh sách khoa
                xoatrang(); // Xóa dữ liệu trên form
            }

        }

        private void dgvKhoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int i = e.RowIndex;
                txtMaKhoa.ReadOnly = true; // Không cho sửa Mã Khoa
                txtMaKhoa.Text = dgvKhoa.Rows[i].Cells[0].Value.ToString();
                txtTenKhoa.Text = dgvKhoa.Rows[i].Cells[1].Value.ToString();
                btnLuu.Visible = false;
                btnSua.Visible = true;
                btnXoa.Visible = true;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            xoatrang();
            txtMaKhoa.ReadOnly = false;
            btnSua.Visible = false;
            btnXoa.Visible = false;
            btnLuu.Visible = true;
            
        }

        private void dgvKhoa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tim_maKhoa = txtTkMK.Text.Trim();
            string tim_tenKhoa = txtTKTenKhoa.Text.Trim();

            if (data.con.State == ConnectionState.Closed)
                data.con.Open();

            string search = "SELECT * FROM Khoa " +
                            "WHERE MaKhoa LIKE '%" + tim_maKhoa + "%' " +
                            "AND TenKhoa LIKE N'%" + tim_tenKhoa + "%'";

            SqlCommand cmd = new SqlCommand(search, data.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            data.con.Close();

            dgvKhoa.DataSource = tb;
            dgvKhoa.Refresh();
            xoatrang();

        }
    }
}
