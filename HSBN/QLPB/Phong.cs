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
using Excel = Microsoft.Office.Interop.Excel;

namespace QLBN
{
    public partial class Phong : Form
    {
        public Phong()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=QLBN_NHUNG;Integrated Security=True");
        private void loadTg()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();

            string sql = "select * from PhongBenh";

            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            con.Close();

            dgvPhong.DataSource = tb;
            dgvPhong.Refresh();
        }


        public bool checktrungMp(string mp)
        {
            //ket noi db 
            if (con.State == ConnectionState.Closed)
                con.Open();

            //tao doi tuong command de thuc hien den cac ban ghi co matacgia=mtg
            string sql = "select count(*) from PhongBenh where MaPhong = '" + mp + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            int kq = (int)cmd.ExecuteScalar();
            if (kq > 0)
                return true; //trung mtg
            else
                return false; //ko trung


        }
        private void cboMaToa()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();

            string sql = "SELECT * FROM ToaNha";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            da.Fill(tb);

            // Giải phóng tài nguyên
            cmd.Dispose();
            con.Close();

            // Thêm dòng tùy chọn mặc định vào DataTable
            DataRow row = tb.NewRow();
            row["MaToa"] = "-- Chọn Mã Tòa --";  // Nội dung hiển thị
            tb.Rows.InsertAt(row, 0);  // Chèn vào vị trí đầu tiên của DataTable

            // Đổ dữ liệu vào ComboBox cboToa
            cboToa.DataSource = tb;
            cboToa.DisplayMember = "MaToa";    // Hiển thị MaToa trong ComboBox
            cboToa.ValueMember = "MaToa";       // Giá trị chọn là MaToa
            cboToa.SelectedIndex = 0;           // Đặt mục chọn mặc định là dòng vừa thêm
        }
       
        private void LoadTangTheoToa(string maToa)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();

            string sql = "SELECT SoTang FROM ToaNha WHERE MaToa = @maToa";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@maToa", maToa);
            SqlDataReader reader = cmd.ExecuteReader();

            cboTang.Items.Clear(); // Xóa dữ liệu cũ trước khi load mới

            // Thêm dòng mặc định đầu tiên
            cboTang.Items.Add("-- Chọn số tầng --");

            if (reader.Read()) // Nếu tìm thấy tòa nhà
            {
                int soTang = Convert.ToInt32(reader["SoTang"]);

                for (int i = 1; i <= soTang; i++) // Thêm các tầng vào ComboBox
                {
                    cboTang.Items.Add(i.ToString());
                }
            }

            reader.Close();
            con.Close();

            // Kiểm tra nếu có tầng thì chọn tầng đầu tiên mặc định
            if (cboTang.Items.Count > 0)
            {
                cboTang.SelectedIndex = 0;
            }
        }


        private void cboMalop()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();

            string sql = "SELECT * FROM Khoa";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            da.Fill(tb);

            // Giải phóng tài nguyên
            cmd.Dispose();
            con.Close();

            // Thêm một hàng trống hoặc hàng hiển thị mặc định vào DataTable
            DataRow row = tb.NewRow();
            row["TenKhoa"] = "-- Chọn Tên Khoa --";  // Nội dung hiển thị
            tb.Rows.InsertAt(row, 0);  // Chèn vào vị trí đầu tiên của DataTable

            // Đổ dữ liệu vào ComboBox cbolop
            cboKhoa.DataSource = tb;
            cboKhoa.DisplayMember = "TenKhoa";    // Hiển thị cột TenKhoa
            cboKhoa.ValueMember = "TenKhoa";       // Giá trị là TenKhoa
            cboKhoa.SelectedIndex = 0;             // Đặt mục chọn mặc định là hàng đầu tiên
        }
        private void Phong_Load(object sender, EventArgs e)
        {
            loadTg();
            cboMalop();
            cboMaToa();
            
            // Nếu có dữ liệu trong cboToa, chọn tòa đầu tiên và tự động load tầng
            if (cboToa.Items.Count > 0)
            {
                cboToa.SelectedIndex = 0; // Chọn tòa đầu tiên
                LoadTangTheoToa(cboToa.SelectedValue.ToString()); // Load số tầng của tòa đó
            }
        

            btnBack_Tk.Visible = false;
            btnLuu.Visible = false;
            btnSua.Visible = false;
            btnXoa.Visible = false;
            btnBack.Visible = false;
            btnBackCell.Visible = false;

            cboTT.SelectedIndex = 0;
            cboKhoa.SelectedIndex = 0;
            cboLoai.SelectedIndex = 0;
            cboToa.SelectedIndex = 0;
            cboTang.SelectedIndex = 0;
            


            txtMp.Enabled = false;  
            txtMp.BackColor = Color.White;   
            txtTen.Enabled = false;  
            txtTen.BackColor = Color.White;
            txtgiuong.Enabled = false;  
            txtgiuong.BackColor = Color.White;


            cboTT.Enabled = false;
            cboToa.Enabled = false;
            cboLoai.Enabled = false;
            cboKhoa.Enabled = false;
            cboTang.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            String mp = txtMp.Text.Trim();
            String ten = txtTen.Text.Trim();
            String sg = txtgiuong.Text.Trim();
            String khoa = cboKhoa.SelectedValue?.ToString() ?? "";  
            String toa = cboToa.SelectedValue?.ToString() ?? "";    
            String loai = cboLoai.SelectedItem?.ToString() ?? "";   
            String tt = cboTT.SelectedItem?.ToString() ?? "";
            String tg = cboTang.SelectedItem?.ToString() ?? "";

            // Kiểm tra ô nhập liệu không được để trống
            if (string.IsNullOrEmpty(mp))
            {
                MessageBox.Show("Mã phòng không được để trống!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMp.Focus();
                return;
            }
            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Tên phòng không được để trống!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }
            if (string.IsNullOrEmpty(sg) || !int.TryParse(sg, out _)) // Kiểm tra số giường phải là số
            {
                MessageBox.Show("Số giường phải là số nguyên!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtgiuong.Focus();
                return;
            }
            if (cboKhoa.SelectedIndex == 0 || string.IsNullOrEmpty(khoa))
            {
                MessageBox.Show("Vui lòng chọn Khoa!", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboKhoa.Focus();
                return;
            }
            if (cboToa.SelectedIndex == 0 || string.IsNullOrEmpty(toa))
            {
                MessageBox.Show("Vui lòng chọn Tòa!", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboToa.Focus();
                return;
            }
            if (cboLoai.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng chọn loại phòng!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoai.Focus();
                return;
            }
            if (cboTT.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng chọn trạng thái!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTT.Focus();
                return;
            }
            if (cboTang.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng chọn số tầng", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTang.Focus();
                return;
            }

            // --- THÊM PHẦN KIỂM TRA SỐ PHÒNG TỐI ĐA ---
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                // 1. Lấy thông tin tòa nhà (SoPhongMax, SoPhongMaxMoiTang)
                string sqlCheckToa = "SELECT SoPhongMax, SoPhongMaxMoiTang FROM ToaNha WHERE MaToa = @maToa";
                SqlCommand cmdCheckToa = new SqlCommand(sqlCheckToa, con);
                cmdCheckToa.Parameters.AddWithValue("@maToa", toa);
                SqlDataReader reader = cmdCheckToa.ExecuteReader();

                if (reader.Read())
                {
                    int soPhongMax = Convert.ToInt32(reader["SoPhongMax"]);
                    int soPhongMaxMoiTang = Convert.ToInt32(reader["SoPhongMaxMoiTang"]);

                    reader.Close();

                    // 2. Đếm số phòng hiện có trong TÒA NHÀ
                    string sqlCountPhongToa = "SELECT COUNT(*) FROM PhongBenh WHERE MaToa = @maToa";
                    SqlCommand cmdCountPhongToa = new SqlCommand(sqlCountPhongToa, con);
                    cmdCountPhongToa.Parameters.AddWithValue("@maToa", toa);
                    int currentPhongInToa = (int)cmdCountPhongToa.ExecuteScalar();

                    // 3. Đếm số phòng hiện có trong TẦNG CỤ THỂ
                    string sqlCountPhongTang = "SELECT COUNT(*) FROM PhongBenh WHERE MaToa = @maToa AND Tang = @tang";
                    SqlCommand cmdCountPhongTang = new SqlCommand(sqlCountPhongTang, con);
                    cmdCountPhongTang.Parameters.AddWithValue("@maToa", toa);
                    cmdCountPhongTang.Parameters.AddWithValue("@tang", tg);
                    int currentPhongInTang = (int)cmdCountPhongTang.ExecuteScalar();

                    // 4. Kiểm tra ràng buộc
                    if (currentPhongInToa >= soPhongMax)
                    {
                        MessageBox.Show($"Tòa {toa} đã đạt tối đa {soPhongMax} phòng!", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                        return;
                    }

                    if (currentPhongInTang >= soPhongMaxMoiTang)
                    {
                        MessageBox.Show($"Tầng {tg} của tòa {toa} đã đạt tối đa {soPhongMaxMoiTang} phòng!", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin tòa nhà!", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra số phòng: " + ex.Message, "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
                return;
            }

            if (checktrungMp(mp))
            {
                MessageBox.Show("Trùng mã phòng!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMp.Focus();
                return;
            }



            //  Thực hiện lưu vào SQL
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                String sql = "INSERT INTO PhongBenh (MaPhong, TenPhong, MaToa, Tang, TenKhoa, LoaiPhong, SoGiuong, TrangThai) " +
                             "VALUES (@mp, @ten, @toa,@tg, @khoa, @loai, @sg, @tt)";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@mp", mp);
                cmd.Parameters.AddWithValue("@ten", ten);
                cmd.Parameters.AddWithValue("@toa", toa);
                cmd.Parameters.AddWithValue("@tg", tg);
                cmd.Parameters.AddWithValue("@khoa", khoa);
                cmd.Parameters.AddWithValue("@loai", loai);
                cmd.Parameters.AddWithValue("@sg", sg);
                cmd.Parameters.AddWithValue("@tt", tt);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Lưu thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Reset giao diện
            loadTg();
            txtMp.Clear();
            txtTen.Clear();
            txtgiuong.Clear();
            cboTT.SelectedIndex = 0;
            cboKhoa.SelectedIndex = 0;
            cboLoai.SelectedIndex = 0;
            cboToa.SelectedIndex = 0;
            if (cboTang.Items.Count > 0)
            {
                cboTang.SelectedIndex = 0;
            }


            btnLuu.Visible = false;
            btnSua.Visible = false;
            btnXoa.Visible = false;
            btnBack.Visible = false;
            btnBackCell.Visible = false;
            txtMp.Enabled = false;
            txtTen.Enabled = false;
            txtgiuong.Enabled = false;
            cboTT.Enabled = false;
            cboToa.Enabled = false;
            cboLoai.Enabled = false;
            cboKhoa.Enabled = false;
            cboTang.Enabled = false;

            btnTk.Visible = true;
            ThemMoi.Visible = true;
            // Kích hoạt lại sự kiện CellClick của DataGridView
            dgvPhong.CellClick += dgvPhong_CellClick;
        }

        private void dgvPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvPhong.Rows[e.RowIndex].Cells[0].Value != null)
            {
                DataGridViewRow row = dgvPhong.Rows[e.RowIndex];
                dgvPhong.ReadOnly = true; // Không cho chỉnh sửa DataGridView

                // Lấy dữ liệu từ DataGridView và làm sạch (loại bỏ khoảng trắng thừa)
                string maPhong = row.Cells["Mp"].Value?.ToString().Trim() ?? "";
                string tenPhong = row.Cells["Tp"].Value?.ToString().Trim() ?? "";
                string toa = row.Cells["Mt"].Value?.ToString().Trim() ?? "";
                string khoa = row.Cells["Tk"].Value?.ToString().Trim() ?? "";
                string loai = row.Cells["Lp"].Value?.ToString().Trim() ?? "";
                string soGiuong = row.Cells["Sg"].Value?.ToString().Trim() ?? "";
                string trangThai = row.Cells["Tt"].Value?.ToString().Trim() ?? "";
                string tang = row.Cells["T"].Value?.ToString().Trim() ?? "";


                // Gán dữ liệu vào các ô
                txtMp.Text = maPhong; // Mã phòng
                txtTen.Text = tenPhong; // Tên phòng
                txtgiuong.Text = soGiuong; // Số giường

                // Gán dữ liệu vào ComboBox
                cboToa.Text = toa; // Tòa
                cboTang.Text = tang; // Tầng
                cboKhoa.Text = khoa; // Khoa
                cboLoai.Text = loai; // Loại phòng
                cboTT.Text = trangThai; // Trạng thái

                // Đặt các TextBox chỉ đọc, không chỉnh sửa
                txtMp.ReadOnly = true;
                txtMp.BackColor = Color.White;
                txtTen.ReadOnly = true;
                txtTen.BackColor = Color.White;
                txtgiuong.ReadOnly = true;
                txtgiuong.BackColor = Color.White;

                // Đảm bảo các ComboBox được bật
                cboToa.Enabled = false;
                cboTang.Enabled = false;
                cboKhoa.Enabled = false;
                cboLoai.Enabled = false;
                cboTT.Enabled = false;

                // Hiển thị hoặc ẩn các nút
                btnXoa.Visible = true;
                btnSua.Visible = true;
                btnBackCell.Visible = true;
                ThemMoi.Visible = false;
                btnBack.Visible = false;
                btnTk.Visible = false;
                btnXuat.Visible = false;
                btnNhap.Visible = false;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvPhong.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult kq = MessageBox.Show("Bạn có chắc chắn muốn xoá phòng bệnh này?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (kq == DialogResult.Cancel)
                return;

            string mp = dgvPhong.CurrentRow.Cells[0].Value.ToString();

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                //  Xóa tất cả giường có MaPhong này trước
                string sqlXoaGiuong = "DELETE FROM Giuong WHERE MaPhong = @mp";
                using (SqlCommand cmdXoaGiuong = new SqlCommand(sqlXoaGiuong, con))
                {
                    cmdXoaGiuong.Parameters.AddWithValue("@mp", mp);
                    cmdXoaGiuong.ExecuteNonQuery();
                }

                //  Sau khi xóa giường, tiến hành xóa phòng
                string sqlXoaPhong = "DELETE FROM PhongBenh WHERE MaPhong = @mp";
                using (SqlCommand cmdXoaPhong = new SqlCommand(sqlXoaPhong, con))
                {
                    cmdXoaPhong.Parameters.AddWithValue("@mp", mp);
                    cmdXoaPhong.ExecuteNonQuery();
                }

                MessageBox.Show("Xóa phòng và các giường thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadTg(); 
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
                txtgiuong.Text = "";
                txtMp.Text = "";
                txtTen.Text = "";
                cboKhoa.SelectedIndex = 0;
                cboLoai.SelectedIndex = 0;  
                cboToa.SelectedIndex = 0;
                cboTT.SelectedIndex = 0;
                if (cboTang.Items.Count > 0)
                {
                    cboTang.SelectedIndex = 0;
                }


            }
            btnXoa.Visible = false;
            btnSua.Visible = false;
            btnBackCell.Visible = false;
            btnBack_Tk.Visible = false;
            btnBack.Visible=false;
            btnLuu.Visible = false;

            ThemMoi.Visible = true;
            btnTk.Visible = true;

            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại cảnh báo với biểu tượng Warning
            DialogResult result = MessageBox.Show(
                "Bạn chắc chắn muốn thoát?",
                "Cảnh báo",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
            );
            if (result == DialogResult.Cancel)
                return;
            Dispose();
        }

        private void txtgiuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu không phải là số và không phải phím xóa (Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho nhập
                MessageBox.Show("Vui lòng chỉ nhập số vào ô Số giường!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTk_Click(object sender, EventArgs e)
        {
            //Form f = new PhongBenh_TimKiem();
            //f.ShowDialog();
            dgvPhong.CellClick -= dgvPhong_CellClick;
            btnBack_Tk.Visible = true;
            ThemMoi.Visible = false;
            btnNhap.Visible = false;
            btnXuat.Visible = false;

            if (!txtMp.Enabled)
            {
                txtMp.Enabled = true;

                txtTen.Enabled = true;
                cboKhoa.Enabled = true;
                cboLoai.Enabled = true;
                txtgiuong.Enabled = true;
                cboTT.Enabled = true;
                cboToa.Enabled = true;
                cboTang.Enabled = true;

                txtMp.BackColor = Color.White;
                txtTen.BackColor = Color.White;
                cboKhoa.BackColor = SystemColors.Window;
                cboLoai.BackColor = SystemColors.Window;
                txtgiuong.BackColor = Color.White;
                cboTT.BackColor = SystemColors.Window;
                cboToa.BackColor = SystemColors.Window;
                cboTang.BackColor = SystemColors.Window;

                return;
            }

            string mp = txtMp.Text.Trim();
            string ten = txtTen.Text.Trim();
            string khoa = cboKhoa.SelectedIndex > 0 ? cboKhoa.SelectedValue.ToString() : "";
            string loai = cboLoai.SelectedIndex > 0 ? cboLoai.SelectedItem.ToString() : "";
            string tt = cboTT.SelectedIndex > 0 ? cboTT.SelectedItem.ToString() : "";
            string toa = cboToa.SelectedIndex > 0 ? cboToa.SelectedValue?.ToString() : "";
            string tenToa = cboToa.SelectedIndex > 0 ? cboToa.Text.Trim() : "";
            string tg = cboTang.SelectedIndex > 0 ? cboTang.Text.Trim() : "";

            int soGiuong;
            bool isSoGiuongValid = int.TryParse(txtgiuong.Text.Trim(), out soGiuong);

            if (con.State == ConnectionState.Closed)
                con.Open();

            string search = @"SELECT pb.MaPhong, pb.TenPhong, pb.MaToa, pb.Tang, pb.TenKhoa, 
            pb.LoaiPhong, pb.SoGiuong, pb.TrangThai, k.MaKhoa
     FROM PhongBenh pb
     LEFT JOIN Khoa k ON pb.TenKhoa = k.TenKhoa
     LEFT JOIN ToaNha t ON pb.MaToa = t.MaToa
     WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(mp))
            {
                search += " AND pb.MaPhong LIKE @mp";
                parameters.Add(new SqlParameter("@mp", "%" + mp + "%"));
            }
            if (!string.IsNullOrEmpty(ten))
            {
                search += " AND pb.TenPhong LIKE @ten";
                parameters.Add(new SqlParameter("@ten", "%" + ten + "%"));
            }
            if (!string.IsNullOrEmpty(toa))
            {
                search += " AND pb.MaToa LIKE @toa";
                parameters.Add(new SqlParameter("@toa", "%" + toa + "%"));
            }
            if (!string.IsNullOrEmpty(tg))
            {
                search += " AND pb.Tang LIKE @tg";
                parameters.Add(new SqlParameter("@tg", "%" + tg + "%"));
            }
            if (!string.IsNullOrEmpty(khoa))
            {
                search += " AND pb.TenKhoa LIKE @khoa";
                parameters.Add(new SqlParameter("@khoa", "%" + khoa + "%"));
            }
            if (!string.IsNullOrEmpty(loai))
            {
                search += " AND pb.LoaiPhong LIKE @loai";
                parameters.Add(new SqlParameter("@loai", "%" + loai + "%"));
            }
            if (isSoGiuongValid)
            {
                search += " AND pb.SoGiuong = @sg";
                parameters.Add(new SqlParameter("@sg", soGiuong));
            }
            if (!string.IsNullOrEmpty(tt))
            {
                search += " AND pb.TrangThai LIKE @tt";
                parameters.Add(new SqlParameter("@tt", "%" + tt + "%"));
            }

            if (parameters.Count == 0)
            {
                MessageBox.Show("Vui lòng nhập ít nhất một tiêu chí để tìm kiếm!", "Thông báo");
                return;
            }

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                using (SqlCommand cmd = new SqlCommand(search, con))
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable tb = new DataTable();
                    da.Fill(tb);

                    if (tb.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy phòng bệnh phù hợp với tiêu chí tìm kiếm.", "Thông báo");
                    }
                    else
                    {
                        dgvPhong.DataSource = tb;
                        dgvPhong.Refresh();
                    }
                }
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void ThemMoi_Click(object sender, EventArgs e)
        {
            // Vô hiệu hóa sự kiện CellClick của dgvT
            dgvPhong.CellClick -= dgvPhong_CellClick;

            // Nếu có dữ liệu trong cboToa, chọn tòa đầu tiên và tự động load tầng
            if (cboToa.Items.Count > 0)
            {
                cboToa.SelectedIndex = 0; // Chọn tòa đầu tiên
                LoadTangTheoToa(cboToa.SelectedValue.ToString()); // Load số tầng của tòa đó
            }

            btnSua.Visible = false;
            btnXoa.Visible = false;
            btnBackCell.Visible = false;
            btnTk.Visible = false;
            btnXuat.Visible = false;
            btnNhap.Visible = false;

            ThemMoi.Visible = false;
            btnLuu.Visible = true;
            btnBack.Visible = true;

            txtMp.Clear();
            txtTen.Clear();
            txtgiuong.Clear();
            cboKhoa.SelectedIndex = 0;
            cboLoai.SelectedIndex = 0;
            cboToa.SelectedIndex = 0;
            cboTT.SelectedIndex = 0;
            if (cboTang.Items.Count > 0)
            {
                cboTang.SelectedIndex = 0;
            }

            txtMp.Enabled = txtTen.Enabled = txtgiuong.Enabled = true;
            txtMp.BackColor = txtTen.BackColor = txtgiuong.BackColor = Color.White;
            txtMp.ForeColor = txtTen.ForeColor = txtgiuong.ForeColor = Color.Black;

            cboKhoa.Enabled = cboLoai.Enabled = cboToa.Enabled = cboTT.Enabled = cboTang.Enabled = true;
            cboKhoa.BackColor = cboLoai.BackColor = cboToa.BackColor = cboTT.BackColor = cboTang.BackColor = SystemColors.Window;
            cboKhoa.ForeColor = cboLoai.ForeColor = cboToa.ForeColor = cboTT.ForeColor = cboTang.ForeColor = Color.Black;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại cảnh báo để xác nhận trước khi thoát
            DialogResult result = MessageBox.Show(
                "Bạn chắc chắn muốn thoát?",
                "Cảnh báo",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
            );
            if (result == DialogResult.Cancel)
                return;

            // Kích hoạt lại sự kiện CellClick của DataGridView
            dgvPhong.CellClick += dgvPhong_CellClick;

            btnLuu.Visible = false;
            btnBack.Visible = false;

            btnTk.Visible = true;
            btnXuat.Visible = true;
            btnNhap.Visible = true;
            ThemMoi.Visible = true; 
            btnXoa.Visible = false; 

            txtgiuong.Clear();
            txtMp.Clear();
            txtTen.Clear();

            cboKhoa.SelectedIndex = 0;
            cboLoai.SelectedIndex = 0;
            cboToa.SelectedIndex = 0;
            cboTT.SelectedIndex = 0;
            if (cboTang.Items.Count > 0)
            {
                cboTang.SelectedIndex = 0;
            }



            txtMp.Enabled = false;
            txtMp.BackColor = Color.White;
            txtTen.Enabled = false;
            txtTen.BackColor = Color.White;
            txtgiuong.Enabled = false;
            txtgiuong.BackColor = Color.White;

            cboTT.Enabled = false;
            cboToa.Enabled = false;
            cboLoai.Enabled = false;
            cboKhoa.Enabled = false;
            cboTang.Enabled = false;

        }

        private void btnBackCell_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại cảnh báo với biểu tượng Warning
            DialogResult result = MessageBox.Show(
                "Bạn chắc chắn muốn thoát?",
                "Cảnh báo",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
            );
            if (result == DialogResult.Cancel)
                return;

            btnLuu.Visible = false;
            btnBack.Visible = false;
            btnBackCell.Visible = false;

            ThemMoi.Visible = true;
            btnNhap.Visible = true;
            btnXuat.Visible = true;
            btnSua.Visible = false;
            btnXoa.Visible = false;

            txtgiuong.Clear();
            txtMp.Clear();
            txtTen.Clear();

            cboKhoa.SelectedIndex = 0;
            cboLoai.SelectedIndex = 0;
            cboToa.SelectedIndex = 0;
            cboTT.SelectedIndex = 0;
            if (cboTang.Items.Count > 0)
            {
                cboTang.SelectedIndex = 0;
            }



            txtMp.Enabled = false;
            txtMp.BackColor = Color.White;
            txtTen.Enabled = false;
            txtTen.BackColor = Color.White;
            txtgiuong.Enabled = false;
            txtgiuong.BackColor = Color.White; 

            cboTT.Enabled = false;
            cboToa.Enabled = false;
            cboLoai.Enabled = false;
            cboKhoa.Enabled = false;
            cboTang.Enabled = false;
            btnBack_Tk.Visible = false;


            btnTk.Visible = true;

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvPhong.CurrentRow != null)
            {
                // Lấy dữ liệu từ hàng đang chọn
                string mp = dgvPhong.CurrentRow.Cells["Mp"].Value.ToString(); 
                string tp = dgvPhong.CurrentRow.Cells["Tp"].Value.ToString(); 
                string mt = dgvPhong.CurrentRow.Cells["Mt"].Value.ToString(); 
                string tk = dgvPhong.CurrentRow.Cells["Tk"].Value.ToString(); 
                string lp = dgvPhong.CurrentRow.Cells["Lp"].Value.ToString(); 
                string sg = dgvPhong.CurrentRow.Cells["Sg"].Value.ToString(); 
                string tt = dgvPhong.CurrentRow.Cells["Tt"].Value.ToString();
                string tg = dgvPhong.CurrentRow.Cells["T"].Value.ToString();


                // Khởi tạo form cập nhật và truyền dữ liệu vào constructor
                PhongBenh_CapNhat phongBenh_CapNhat = new PhongBenh_CapNhat(mp, tp, mt,tg, tk, lp, sg, tt);
                

                // Bắt sự kiện khi Form2 cập nhật xong
                phongBenh_CapNhat.OnDataUpdated += (newMp, newTp, newMt, newTang, newTk, newLp, newSg, newTt) =>
                {
                    loadTg();

                    // Giữ nguyên giá trị vừa cập nhật trong Form1
                    txtMp.Text = newMp;
                    txtTen.Text = newTp;
                    cboToa.SelectedValue = newMt;   // Dùng SelectedValue vì cboToa có ValueMember
                    LoadTangTheoToa(newMt);         // Load lại tầng dựa trên mã tòa mới
                    cboTang.SelectedItem = newTang;
                    cboKhoa.SelectedItem = newTk;
                    cboLoai.SelectedItem = newLp;
                    txtgiuong.Text = newSg;
                    cboTT.SelectedItem = newTt;

                    txtMp.ReadOnly = true;
                };

                // Hiển thị form cập nhật dưới dạng dialog
                phongBenh_CapNhat.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để cập nhật!", "Thông báo");
            }
            ThemMoi.Visible = false;
            btnBackCell.Visible = true;
            btnSua.Visible = true;
            btnXoa.Visible = true;
        }

        private void btnBack_Tk_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại cảnh báo với biểu tượng Warning
            DialogResult result = MessageBox.Show(
                "Bạn chắc chắn muốn thoát?",
                "Cảnh báo",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
            );
            if (result == DialogResult.Cancel)
                return;
            // Kích hoạt lại sự kiện CellClick của DataGridView
            dgvPhong.CellClick += dgvPhong_CellClick;
            loadTg();

            btnLuu.Visible = false;
            btnBack.Visible = false;

            btnXuat.Visible = true;
            btnNhap.Visible = true;
            ThemMoi.Visible = true;
            btnSua.Visible = false;  
            btnXoa.Visible = false;  

            txtgiuong.Clear();
            txtMp.Clear();
            txtTen.Clear();

            cboKhoa.SelectedIndex = 0;
            cboLoai.SelectedIndex = 0;
            cboToa.SelectedIndex = 0;
            cboTT.SelectedIndex = 0;
            if (cboTang.Items.Count > 0)
            {
                cboTang.SelectedIndex = 0;
            }



            txtMp.Enabled = false;
            txtMp.BackColor = Color.White; 
            txtTen.Enabled = false;
            txtTen.BackColor = Color.White; 
            txtgiuong.Enabled = false;
            txtgiuong.BackColor = Color.White; 

            cboTT.Enabled = false;
            cboToa.Enabled = false;
            cboTang.Enabled = false;
            cboLoai.Enabled = false;
            cboKhoa.Enabled = false;
            btnBack_Tk.Visible = false;
        }


        public void ExportExcel(DataTable tb, string sheetname)
        {
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks oBooks;
            Microsoft.Office.Interop.Excel.Sheets oSheets;
            Microsoft.Office.Interop.Excel.Workbook oBook;
            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            oExcel.Visible = true;
            oExcel.DisplayAlerts = false;
            oExcel.Application.SheetsInNewWorkbook = 1;
            oBooks = oExcel.Workbooks;
            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));
            oSheets = oBook.Worksheets;
            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);
            oSheet.Name = sheetname;

            // Cập nhật phạm vi tiêu đề chính cho 8 cột (A1:H1)
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "H1");
            head.MergeCells = true;
            head.Value2 = "DANH SÁCH PHÒNG BỆNH";
            head.Font.Bold = true;
            head.Font.Name = "Times New Roman";
            head.Font.Size = "18";
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Thêm cột "SỐ TẦNG"
            string[] columnTitles = { "MÃ PHÒNG", "TÊN PHÒNG", "MÃ TÒA", "TÊN KHOA", "LOẠI PHÒNG", "SỐ GIƯỜNG", "TRẠNG THÁI", "SỐ TẦNG" };
            double[] columnWidths = { 15.0, 20.0, 10.0, 30.0, 15.0, 15.0, 15.0, 12.0 };

            for (int i = 0; i < columnTitles.Length; i++)
            {
                Microsoft.Office.Interop.Excel.Range col = oSheet.Cells[3, i + 1];
                col.Value2 = columnTitles[i];
                col.ColumnWidth = columnWidths[i];
            }

            // Cập nhật phạm vi tiêu đề cột (A3:H3)
            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "H3");
            rowHead.Font.Bold = true;
            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            rowHead.Interior.ColorIndex = 15;
            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Mảng dữ liệu từ DataTable
            object[,] arr = new object[tb.Rows.Count, tb.Columns.Count];
            for (int r = 0; r < tb.Rows.Count; r++)
            {
                DataRow dr = tb.Rows[r];
                for (int c = 0; c < tb.Columns.Count; c++)
                {
                    arr[r, c] = dr[c];
                }
            }

            int rowStart = 4;
            int columnStart = 1;
            int rowEnd = rowStart + tb.Rows.Count - 1;
            int columnEnd = tb.Columns.Count;

            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];
            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];
            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);

            range.Value2 = arr;
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        }


        private void btnXuat_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu DataGridView không có dữ liệu
            if (dgvPhong.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo DataTable từ DataGridView
            DataTable dt = new DataTable();

            // Thêm cột từ DataGridView
            foreach (DataGridViewColumn col in dgvPhong.Columns)
            {
                dt.Columns.Add(col.HeaderText);
            }

            // Thêm hàng từ DataGridView
            foreach (DataGridViewRow row in dgvPhong.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < dgvPhong.Columns.Count; i++)
                    {
                        dr[i] = row.Cells[i].Value;
                    }
                    dt.Rows.Add(dr);
                }
            }

            // Gọi phương thức xuất Excel
            ExportExcel(dt, "DanhSachPhongBenh");
        }

        private void ReadExcel(string filename)
        {
            if (filename == null)
            {
                MessageBox.Show("Chưa chọn file");
            }
            else
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbooks workbooks = excelApp.Workbooks;
                Excel.Workbook workbook = workbooks.Open(filename);

                foreach (Excel.Worksheet wsheet in workbook.Sheets)
                {
                    int i = 2;  // Dữ liệu bắt đầu từ dòng số 2
                    do
                    {
                        if (wsheet.Cells[i, 1].Value == null &&
                            wsheet.Cells[i, 2].Value == null &&
                            wsheet.Cells[i, 3].Value == null &&
                            wsheet.Cells[i, 4].Value == null &&
                            wsheet.Cells[i, 5].Value == null &&
                            wsheet.Cells[i, 6].Value == null &&
                            wsheet.Cells[i, 7].Value == null)
                        {
                            break;
                        }
                        else
                        {
                            // Đổ dữ liệu từ file Excel vào DB
                            ThemmoiPhongBenh(
                                wsheet.Cells[i, 1].Value?.ToString() ?? "",
                                wsheet.Cells[i, 2].Value?.ToString() ?? "",
                                wsheet.Cells[i, 3].Value?.ToString() ?? "",
                                wsheet.Cells[i, 4].Value?.ToString() ?? "",
                                wsheet.Cells[i, 5].Value?.ToString() ?? "",
                                wsheet.Cells[i, 6].Value?.ToString() ?? "",
                                wsheet.Cells[i, 7].Value?.ToString() ?? "",
                                wsheet.Cells[i, 8].Value?.ToString() ?? ""
                            );

                            i++; // Chuyển sang dòng tiếp theo
                        }
                    }
                    while (true);
                }

                // Đóng workbook và ứng dụng Excel
                workbook.Close(false);
                excelApp.Quit();
                MessageBox.Show("Nhập dữ liệu thành công!", "Thông báo");
            }
        }

        private void ThemmoiPhongBenh(string mp, string tp, string mt, string tk, string lp, string sg, string tt, String tg)
        {
            string sql = "INSERT INTO PhongBenh VALUES ('" + mp + "', N'" + tp + "', '" + mt + "', N'" + tk + "', N'" + lp + "', '" + sg + "', N'" + tt + "', '" + tg + "')";

            using (SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=QLBN_NHUNG;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            loadTg();
        }


        private void btnNhap_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Chọn file Excel",
                Filter = "Excel Files|*.xls;*.xlsx"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                ReadExcel(filePath);
                MessageBox.Show("Nhập dữ liệu từ Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cboToa_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cboToa.SelectedValue != null)
            {
                string maToa = cboToa.SelectedValue.ToString(); // Lấy mã tòa được chọn
                LoadTangTheoToa(maToa); // Gọi hàm để load tầng theo tòa nhà
            }
        }
    }
}
