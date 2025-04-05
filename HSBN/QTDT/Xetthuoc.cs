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

namespace HSBN.QTDT
{
    public partial class Xetthuoc : Form
    {
        public Xetthuoc()
        {
            InitializeComponent();
        }

        public static SqlConnection con = new SqlConnection("Data Source=CONMEOHUDON\\SQLEXPRESS02;Initial Catalog=QuanLyBenhVien;Integrated Security=True");
        void Connect()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        private void load_cbo()
        {
            string sql = "Select * From Khoa";
            thuvien.cbo(cboKhoa, sql, "TenKhoa", "MaKhoa");
        }

        private void Xetthuoc_Load(object sender, EventArgs e)
        {
            loadGRVDieuTri();
            load_cbo();
        }

        private void cboKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy giá trị MaKhoa hoặc TenKhoa tùy theo cách bạn bind dữ liệu
            string tenKhoa = cboKhoa.Text;
            string sql = "Select * From QuaTrinhDieuTri WHERE TenKhoa = N'" + tenKhoa + "'";
            thuvien.cbo(cboBenhnhan, sql, "TenBenhNhan", "MaBenhNhan");

            if (tenKhoa == "Khoa Da Liễu")
            {
                cbbThuoc.Items.Clear();
                cbbThuoc.Items.Add("Metoprolol");
                cbbThuoc.Items.Add("Atenolol");
            }
            if (tenKhoa == "Khoa Nội")
            {
                cbbThuoc.Items.Clear();
                cbbThuoc.Items.Add("Nebivolol");
                cbbThuoc.Items.Add("Amlodipine");
                cbbThuoc.Items.Add("Nebivolol");
                cbbThuoc.Items.Add("Divalproex");
            }
            if (tenKhoa == "Khoa Nhi")
            {
                cbbThuoc.Items.Clear();
                cbbThuoc.Items.Add("Olanzapine");
                cbbThuoc.Items.Add("Fluconazole");
                cbbThuoc.Items.Add("Zolpidem");
                cbbThuoc.Items.Add("Eszopiclone");
            }
            if (tenKhoa == "Khoa Ngoại")
            {
                cbbThuoc.Items.Clear();
                cbbThuoc.Items.Add("Ticagrelor");
                cbbThuoc.Items.Add("Vitamin");
                cbbThuoc.Items.Add("Tiotropium");
            }
            if (tenKhoa == "Khoa Sản")
            {
                cbbThuoc.Items.Clear();
                cbbThuoc.Items.Add("12");
                cbbThuoc.Items.Add("Tolterodine");
            }

        }

        private void txtSoNgayNhapVien_ValueChanged(object sender, EventArgs e)
        {
            if (txtSoNgayNhapVien.Value > 0)
            {
                cbbNgay.Items.Clear();
                for (int i = 1; i <= txtSoNgayNhapVien.Value; i++)
                {
                    cbbNgay.Items.Add(i.ToString());
                }
            }
        }

        private void cboBenhnhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Connect();

                string tenBenhNhan = cboBenhnhan.Text;

                // Sử dụng SqlCommand với tham số để tránh SQL Injection
                SqlCommand cmd = new SqlCommand("SELECT SoNgayNhapVien, ChanDoanDieuTri FROM QuaTrinhDieuTri WHERE TenBenhNhan = @TenBenhNhan", con);
                cmd.Parameters.AddWithValue("@TenBenhNhan", tenBenhNhan);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Dùng ToString để tránh lỗi null
                    txtSoNgayNhapVien.Text = reader["SoNgayNhapVien"].ToString();
                    txtChanDoan.Text = reader["ChanDoanDieuTri"].ToString();
                }
                else
                {
                    txtSoNgayNhapVien.Text = "";
                    txtChanDoan.Text = "";
                }

                reader.Close();
                TongTienThuoc();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                con.Close(); // luôn đóng kết nối trong finally
            }


        }

        private void _resert()
        {
            // Reset combo ngày
            cbbNgay.Items.Clear();

            int soNgay = 0;

            // Nếu txtSoNgayNhapVien là NumericUpDown
            if (int.TryParse(txtSoNgayNhapVien.Value.ToString(), out soNgay))
            {
                for (int i = 1; i <= soNgay; i++)
                {
                    cbbNgay.Items.Add(i.ToString());
                }
            }

            // Reset combo thuốc
            cbbThuoc.Items.Clear();
            cbbThuoc.Items.Add("Chọn thuốc"); // mục mặc định

            // Reset số lượng thuốc
            txtSoLuongThuoc.Text = string.Empty;
        }


        private void loadGRVDieuTri()
        {
            string sql = "select * from DieuTri order by TenBenhNhan asc";
            thuvien.load(dataGRVDieuTri, sql);
        }

        private void dataGRVDieuTri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGRVDieuTri.CurrentRow;
                if (row.Cells["TenBenhNhan"].Value.ToString() == string.Empty)
                {
                    _resert();
                }

                cboBenhnhan.Text = row.Cells["TenBenhNhan"].Value.ToString();
                txtSoNgayNhapVien.Text = row.Cells["SoNgayNhapVien"].Value.ToString();

                con.Open();
                SqlCommand cmd = new SqlCommand("select ChanDoanDieuTri from QuaTrinhDieuTri where TenBenhNhan = '" + cboBenhnhan.Text + "'", con);
                string ChuanDoanSoBo = (string)cmd.ExecuteScalar(); // Truy vấn dữ liệu và lấy giá trị trả về 
                txtChanDoan.Text = ChuanDoanSoBo; // Gán giá trị tên vào thuộc tính Text của textbox để hiển thị
                con.Close();

                cbbNgay.Text = row.Cells["Ngay"].Value.ToString();
                cbbThuoc.Text = row.Cells["TenThuoc"].Value.ToString();
                txtSoLuongThuoc.Text = row.Cells["SoLuongThuoc"].Value.ToString();

                loadGRVDieuTri();
            }
        }

        private void btnXetThuoc_Click(object sender, EventArgs e)
        {
            if (cboBenhnhan.Text == "" || cbbNgay.Text == "" || cbbThuoc.Text == "" || txtSoLuongThuoc.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin !", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                con.Open();

                // Lấy đơn giá thuốc
                string queryGiaTien = "SELECT DonGia FROM Thuoc WHERE TenThuoc = @TenThuoc";
                SqlCommand cmd = new SqlCommand(queryGiaTien, con);
                cmd.Parameters.AddWithValue("@TenThuoc", cbbThuoc.Text.Trim());

                object result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                {
                    MessageBox.Show("Không tìm thấy giá thuốc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                float donGia = Convert.ToSingle(result);
                int soLuong = Convert.ToInt32(txtSoLuongThuoc.Text);
                float giaTienThuoc = donGia * soLuong;

                // Thêm dữ liệu vào bảng DieuTri
                string queryInsert = "INSERT INTO DieuTri (TenBenhNhan, SoNgayNhapVien, Ngay, TenThuoc, SoLuongThuoc, ThanhTien) " +
                                     "VALUES (@TenBenhNhan, @SoNgayNhapVien, @Ngay, @TenThuoc, @SoLuongThuoc, @ThanhTien)";
                cmd = new SqlCommand(queryInsert, con);
                cmd.Parameters.AddWithValue("@TenBenhNhan", cboBenhnhan.Text);
                cmd.Parameters.AddWithValue("@SoNgayNhapVien", txtSoNgayNhapVien.Text);
                cmd.Parameters.AddWithValue("@Ngay", cbbNgay.Text);
                cmd.Parameters.AddWithValue("@TenThuoc", cbbThuoc.Text);
                cmd.Parameters.AddWithValue("@SoLuongThuoc", soLuong);
                cmd.Parameters.AddWithValue("@ThanhTien", giaTienThuoc);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Cấp thuốc thành công!", "Thông báo");

                // --> Sau đó cập nhật tồn kho
                string queryUpdateThuoc = "UPDATE Thuoc SET SoLuong = SoLuong - @soluong WHERE TenThuoc = @tenthuoc";
                SqlCommand cmdUpdate = new SqlCommand(queryUpdateThuoc, con);
                cmdUpdate.Parameters.AddWithValue("@soluong", Convert.ToInt32(txtSoLuongThuoc.Text));
                cmdUpdate.Parameters.AddWithValue("@tenthuoc", cbbThuoc.Text);
                cmdUpdate.ExecuteNonQuery();

                string checkQty = "SELECT SoLuong FROM Thuoc WHERE TenThuoc = @tenthuoc";
                SqlCommand cmdCheck = new SqlCommand(checkQty, con);
                cmdCheck.Parameters.AddWithValue("@tenthuoc", cbbThuoc.Text);
                int soLuongHienTai = Convert.ToInt32(cmdCheck.ExecuteScalar());

                int soLuongMuonCap = Convert.ToInt32(txtSoLuongThuoc.Text);

                if (soLuongHienTai < soLuongMuonCap)
                {
                    MessageBox.Show("Không đủ thuốc trong kho!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    con.Close();
                    return;
                }


                loadGRVDieuTri();
                TongTienThuoc();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void TongTienThuoc()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string query = "SELECT SUM(ThanhTien) FROM DieuTri WHERE TenBenhNhan = @TenBenhNhan";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TenBenhNhan", cboBenhnhan.Text);

                object result = cmd.ExecuteScalar();
                float tongGiaTien = 0;

                if (result != null && result != DBNull.Value)
                {
                    tongGiaTien = Convert.ToSingle(result);
                }

                txtTongTienThuoc.Text = tongGiaTien.ToString("N0"); // định dạng tiền
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tính tổng tiền: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
