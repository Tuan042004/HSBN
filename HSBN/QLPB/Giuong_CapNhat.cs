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

namespace QLBN
{
    public partial class Giuong_CapNhat : Form
    {
        private string mg;

        public Giuong_CapNhat(string mg, string mp, string tt)
        {
            InitializeComponent();
            // Lưu mã phòng hiện tại
            currentMaPhong = mp;
            this.mg = mg;
            cboMalop();
            // Hiển thị dữ liệu lên các ô nhập liệu
            txtmg.Text = mg;
            cbomp.Text = mp;
            cbott.Text = tt;

            // BAN ĐẦU CHỈ HIỂN THỊ PHÒNG HIỆN TẠI
            LoadCurrentPhongOnly();

        }
        public Giuong_CapNhat()
        {
            InitializeComponent();
            
        }
        SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=QLBN_NHUNG;Integrated Security=True");

        private string currentMaPhong; // Lưu lại mã phòng hiện tại
        private bool isFirstClick = true;

        // Khai báo delegate và sự kiện
        public delegate void DataUpdatedHandler(string newMaGiuong, string newMaPhong, string newTrangThai);
        public event DataUpdatedHandler OnDataUpdated;

        // Chỉ hiển thị 1 phòng duy nhất
        private void LoadCurrentPhongOnly()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();

            // Chỉ lấy mỗi phòng hiện tại
            string sql = "SELECT MaPhong, TenPhong FROM PhongBenh WHERE MaPhong = @MaPhong";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@MaPhong", currentMaPhong);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            da.Fill(tb);

            cbomp.DataSource = tb;
            cbomp.DisplayMember = "MaPhong";
            cbomp.ValueMember = "MaPhong";
        }
        private void cboMalop()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();

            // Lấy tất cả phòng TRỐNG + phòng HIỆN TẠI (dù đầy hay trống)
            string sql = @"
        SELECT MaPhong, TenPhong 
        FROM PhongBenh 
        WHERE TrangThai = 'Trống'
        OR MaPhong = @CurrentMaPhong
        ORDER BY MaPhong";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@CurrentMaPhong", currentMaPhong);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            da.Fill(tb);

            cbomp.DataSource = tb;
            cbomp.DisplayMember = "MaPhong";
            cbomp.ValueMember = "MaPhong";

            // Vẫn chọn phòng hiện tại làm mặc định
            cbomp.SelectedValue = currentMaPhong;
        }

        private void btnCn_Click(object sender, EventArgs e)
        {
            try
            {
                String mg = txtmg.Text.Trim();
                String mp = cbomp.SelectedValue?.ToString();
                String tt = cbott.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(mg) || string.IsNullOrEmpty(mp) || string.IsNullOrEmpty(tt))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (con.State == ConnectionState.Closed)
                    con.Open();

                string sql = "UPDATE Giuong SET MaPhong=@mp, TrangThai=@tt WHERE MaGiuong=@mg";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@mg", SqlDbType.VarChar, 50).Value = mg;
                    cmd.Parameters.Add("@mp", SqlDbType.NVarChar, 50).Value = mp;
                    cmd.Parameters.Add("@tt", SqlDbType.NVarChar, 50).Value = tt;

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        OnDataUpdated?.Invoke(mg, mp, tt);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy giường bệnh để cập nhật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        
        private void Giuong_CapNhat_Load(object sender, EventArgs e)
        {
            txtmg.ReadOnly = true;
            //txtmg.Enabled = false;

        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại cảnh báo với biểu tượng Warning
            DialogResult result = MessageBox.Show(
                "Bạn chắc chắn muốn thoát?",
                "Cảnh báo",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
            );

            // Nếu người dùng nhấn Cancel thì dừng lại, không làm gì cả
            if (result == DialogResult.Cancel)
                return;
            Dispose();
        }

        private void cbomp_DropDown(object sender, EventArgs e)
        { // Khi click vào combobox mã phòng thì load danh sách phòng trống
            if (isFirstClick)
            {
                cboMalop();
                isFirstClick = false;
            }
        }
    }
}
