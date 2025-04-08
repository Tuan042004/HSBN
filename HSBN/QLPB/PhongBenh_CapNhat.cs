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

namespace QLBN
{
    public partial class PhongBenh_CapNhat : Form
    {
        public PhongBenh_CapNhat(string mp, string tp, string mt, string tg, string tk, string lp, string sg, string tt)
        {
            InitializeComponent();

            // Load dữ liệu cho các ComboBox
            cboMaToa();  // Load danh sách mã tòa
            cboMalop();  // Load danh sách khoa

            // Hiển thị dữ liệu lên các trường nhập liệu
            txtMp.Text = mp;
            txtTen.Text = tp;
            txtgiuong.Text = sg;

            //// Debug để kiểm tra dữ liệu truyền vào
            //Console.WriteLine($"Form 2 - mt: {mt}, tg: {tg}");

            // Gán giá trị cho cboToa và cboTang
            if (!string.IsNullOrEmpty(mt))
            {
                cboToa.SelectedValue = mt; // Gán mã tòa
                if (cboToa.SelectedValue != null && cboToa.SelectedValue.ToString() == mt)
                {
                    LoadTangTheoToa(mt); // Load tầng dựa trên mã tòa
                    if (!string.IsNullOrEmpty(tg) && cboTang.Items.Contains(tg))
                    {
                        cboTang.SelectedItem = tg; // Gán tầng
                    }
                    else
                    {
                        cboTang.SelectedIndex = 0; // Nếu tầng không hợp lệ, chọn mặc định
                    }
                }
                else
                {
                    cboToa.SelectedIndex = 0; // Nếu mã tòa không hợp lệ, chọn mặc định
                }
            }
            else
            {
                cboToa.SelectedIndex = 0;
            }

            // Gán giá trị cho các ComboBox khác
            cboKhoa.SelectedValue = tk;
            cboLoai.SelectedItem = lp;
            cboTT.SelectedItem = tt;
        }

        public delegate void DataUpdatedEventHandler(string mp, string tp, string mt,string tg, string tk, string lp, string sg, string tt);
        public event DataUpdatedEventHandler OnDataUpdated;


        SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=QLBN_NHUNG;Integrated Security=True");

        public PhongBenh_CapNhat()
        {
            InitializeComponent();
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

            cmd.Dispose();
            con.Close();

            // Thêm dòng mặc định
            DataRow row = tb.NewRow();
            row["MaToa"] = "-- Chọn Mã Tòa --";
            tb.Rows.InsertAt(row, 0);

            cboToa.DataSource = tb;
            cboToa.DisplayMember = "MaToa";
            cboToa.ValueMember = "MaToa";
            // Không cần gán SelectedIndex ở đây, để constructor xử lý
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

            cmd.Dispose();
            con.Close();

            // Thêm dòng mặc định
            DataRow row = tb.NewRow();
            row["TenKhoa"] = "-- Chọn Tên Khoa --";
            tb.Rows.InsertAt(row, 0);

            cboKhoa.DataSource = tb;
            cboKhoa.DisplayMember = "TenKhoa";
            cboKhoa.ValueMember = "TenKhoa"; // Dùng TenKhoa làm giá trị
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
        private void LoadTangTheoToa(string maToa)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();

            string sql = "SELECT SoTang FROM ToaNha WHERE MaToa = @maToa";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@maToa", maToa);
            SqlDataReader reader = cmd.ExecuteReader();

            cboTang.Items.Clear();
            cboTang.Items.Add("-- Chọn số tầng --");

            if (reader.Read())
            {
                int soTang = Convert.ToInt32(reader["SoTang"]);
                for (int i = 1; i <= soTang; i++)
                {
                    cboTang.Items.Add(i.ToString());
                }
            }

            reader.Close();
            con.Close();
        }
        private void PhongBenh_CapNhat_Load(object sender, EventArgs e)
        {
            txtMp.Enabled = false; 
            txtMp.BackColor = Color.White;
            // Nếu có dữ liệu trong cboToa, chọn tòa đầu tiên và tự động load tầng
            if (cboToa.Items.Count > 0)
            {
                cboToa.SelectedIndex = 0; // Chọn tòa đầu tiên
                LoadTangTheoToa(cboToa.SelectedValue.ToString()); // Load số tầng của tòa đó
            }
        }

        private void btnCn_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ các ô nhập liệu
                string mp = txtMp.Text.Trim();
                string tp = txtTen.Text.Trim();
                string mt = cboToa.SelectedValue?.ToString();
                string tg = cboTang.SelectedItem?.ToString();
                string tk = cboKhoa.SelectedValue?.ToString();
                string lp = cboLoai.SelectedItem?.ToString();
                string sg = txtgiuong.Text.Trim();
                string tt = cboTT.SelectedItem?.ToString();

                // Kiểm tra nếu có trường nào bị thiếu
                if (string.IsNullOrEmpty(mp) || string.IsNullOrEmpty(tp) || string.IsNullOrEmpty(mt) || string.IsNullOrEmpty(tg) ||
                    string.IsNullOrEmpty(tk) || string.IsNullOrEmpty(lp) || string.IsNullOrEmpty(sg) || string.IsNullOrEmpty(tt))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (con.State == ConnectionState.Closed)
                    con.Open();

                // Câu lệnh SQL UPDATE
                string sql = "UPDATE PhongBenh SET TenPhong=@tp, MaToa=@mt, Tang=@tg, TenKhoa=@tk, LoaiPhong=@lp, SoGiuong=@sg, TrangThai=@tt WHERE MaPhong=@mp";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@mp", SqlDbType.VarChar, 50).Value = mp;
                    cmd.Parameters.Add("@tp", SqlDbType.NVarChar, 100).Value = tp;
                    cmd.Parameters.Add("@mt", SqlDbType.VarChar, 50).Value = mt;
                    cmd.Parameters.Add("@tg", SqlDbType.VarChar, 50).Value = tg;
                    cmd.Parameters.Add("@tk", SqlDbType.NVarChar, 50).Value = tk;
                    cmd.Parameters.Add("@lp", SqlDbType.NVarChar, 50).Value = lp;
                    cmd.Parameters.Add("@sg", SqlDbType.Int).Value = int.Parse(sg);
                    cmd.Parameters.Add("@tt", SqlDbType.NVarChar, 50).Value = tt;

                    // Thực thi lệnh UPDATE
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Gửi dữ liệu về Form 1 qua sự kiện
                        OnDataUpdated?.Invoke(mp, tp, mt, tg, tk, lp, sg, tt);

                        // Đóng Form 2
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy phòng để cập nhật! Kiểm tra mã phòng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
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

        private void cboToa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboToa.SelectedValue != null)
            {
                string maToa = cboToa.SelectedValue.ToString(); // Lấy mã tòa được chọn
                LoadTangTheoToa(maToa); // Gọi hàm để load tầng theo tòa nhà
            }
        }
    }
}
