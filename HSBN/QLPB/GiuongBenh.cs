using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace QLBN
{
    public partial class GiuongBenh : Form
    {
        public GiuongBenh()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=QLBN_NHUNG;Integrated Security=True");

        private void loadTg()
        {
            //ket noi db
            if (con.State == ConnectionState.Closed)
                con.Open();
            //b2 
            String sql = @"SELECT g.MaGiuong, g.MaPhong, pb.TenPhong, g.TrangThai
                   FROM Giuong g
                   LEFT JOIN PhongBenh pb ON g.MaPhong = pb.MaPhong";
            SqlCommand cmd = new SqlCommand(sql, con);

            //b3 lay dl tu cmd
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            //b4 khoi tao doi tuong, lay dl tu danh sach
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            con.Close();

            //hien thi len bang
            dgvG.DataSource = tb;
            dgvG.Refresh();

        }
        public bool checktrungMg(string mg)
        {
            //ket noi db 
            if (con.State == ConnectionState.Closed)
                con.Open();

            string sql = "select count(*) from Giuong where MaGiuong = '" + mg + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            int kq = (int)cmd.ExecuteScalar();
            if (kq > 0)
                return true; //trung mtg
            else
                return false; //ko trung


        }
        //private bool showEmptyRoomsOnly = true; // Mặc định là chỉ hiển thị phòng còn trống

        private bool loadAllRooms = true;  // Biến kiểm soát ComboBox (tìm tất cả phòng hay phòng trống)

        private void cboMalop()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();

            string sql;
            if (loadAllRooms)
            {
                // Lấy tất cả phòng (kể cả phòng đầy/bảo trì)
                sql = @"SELECT MaPhong, TrangThai FROM PhongBenh";
            }
            else
            {
                // Chỉ lấy phòng TRỐNG (trạng thái = 0) và còn giường trống
                sql = @"SELECT p.MaPhong 
                        FROM PhongBenh p
                        WHERE p.TrangThai =  N'Trống'
                        AND (SELECT COUNT(*) FROM Giuong g WHERE g.MaPhong = p.MaPhong) < p.SoGiuong";
            }
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            da.Fill(tb);

            cmd.Dispose();
            con.Close();

            // Thêm lựa chọn mặc định
            DataRow row = tb.NewRow();
            row["MaPhong"] = "-- Chọn mã phòng --";
            tb.Rows.InsertAt(row, 0);

            // Cập nhật lại ComboBox
            cbomp.DataSource = tb;
            cbomp.DisplayMember = "MaPhong";
            cbomp.ValueMember = "MaPhong";
            cbomp.SelectedIndex = 0;
        }

        // tải lại danh sách phòng trong ComboBox, nhưng chỉ hiển thị các phòng còn giường trống
        private void LoadRoomsForAddingNew()
        {
            // Chỉ hiển thị các phòng còn trống
            loadAllRooms = false; // Set false để chỉ hiển thị phòng trống
            cboMalop(); 
        }

        //kiểm tra xem một phòng bệnh (xác định bằng maPhong) đã hết giường trống hay chưa.
        private bool checkSoGiuong(string maPhong)
        {
            bool isFull = false;

            // Sử dụng using để đảm bảo kết nối được đóng và giải phóng đúng cách
            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=QLBN_NHUNG;Integrated Security=True"))
            {
                // Kiểm tra số giường hiện có
                string sql = @"SELECT COUNT(*) FROM Giuong WHERE MaPhong = @maPhong";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@maPhong", SqlDbType.VarChar, 50).Value = maPhong;

                con.Open();
                int currentBeds = (int)cmd.ExecuteScalar();

                // Kiểm tra số giường tối đa (sử dụng cùng kết nối)
                sql = @"SELECT SoGiuong FROM PhongBenh WHERE MaPhong = @maPhong";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@maPhong", SqlDbType.VarChar, 50).Value = maPhong;

                int maxBeds = (int)cmd.ExecuteScalar();

                // Kiểm tra điều kiện đầy
                isFull = currentBeds >= maxBeds;
            } // Kết nối tự động đóng khi ra khỏi using

            return isFull;
        }

        ////FIX CỨNG CBOBOX CHỈ ĐỂ 1 ITEM DUY NHẤT
        //private void LoadComboBoxTrangThai()
        //{
        //    // Thêm các trạng thái vào ComboBox
        //    cbott.Items.Clear();
        //    cbott.Items.Add("--Chọn Trạng Thái--");
        //    cbott.Items.Add("Trống");                         --đây nhé!
        //    cbott.Items.Add("Đang Sử Dụng");
        //    cbott.Items.Add("Đang Sửa Chữa");
        //    cbott.SelectedIndex = 0; // Hiển thị dòng mặc định
        //}
        private void btnluu_Click(object sender, EventArgs e)
        {
            // Vô hiệu hóa sự kiện CellClick của dgvG
            dgvG.CellClick -= dgvG_CellClick;

            // Lấy dữ liệu từ các Control đưa vào biến
            String mg = txtmg.Text.Trim();
            String mp = cbomp.SelectedValue != null ? cbomp.SelectedValue.ToString() : "";
            String tt = cbott.SelectedItem != null ? cbott.SelectedItem.ToString() : "";

            // Kiểm tra xem có trường nào trống không
            if (string.IsNullOrEmpty(mg))
            {
                MessageBox.Show("Vui lòng nhập mã giường!", "Thông báo");
                txtmg.Focus();
                return;
            }

            if (string.IsNullOrEmpty(mp) || mp == "-- Chọn mã phòng --")
            {
                MessageBox.Show("Vui lòng chọn mã phòng!", "Thông báo");
                cbomp.Focus();
                return;
            }

            if (cbott.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng chọn trạng thái!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbott.Focus();
                return;
            }

            // Check trùng mã giường
            if (checktrungMg(mg))
            {
                MessageBox.Show("Trùng mã giường", "THÔNG BÁO");
                txtmg.Focus();
                return;
            }

            // Kiểm tra số giường đã có trong phòng và so sánh với sức chứa
            if (checkSoGiuong(mp))
            {
                MessageBox.Show("Phòng này đã đầy giường, không thể thêm giường mới!", "Thông báo");
                return;
            }

            // Kết nối tới DB
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // Tạo câu lệnh truy vấn để lưu vào bảng
            String sql = "INSERT INTO Giuong (MaGiuong, MaPhong, TrangThai) VALUES (@mg, @mp, @tt)";

            // Tạo đối tượng command để thực thi câu lệnh lưu dữ liệu vào bảng 
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@mg", SqlDbType.VarChar, 50).Value = mg;
            cmd.Parameters.Add("@mp", SqlDbType.NVarChar, 50).Value = mp;
            cmd.Parameters.Add("@tt", SqlDbType.NVarChar, 50).Value = tt;
            cmd.ExecuteNonQuery();

            MessageBox.Show("Lưu thành công", "Thông báo");

            // Giải phóng cmd và đóng kết nối
            cmd.Dispose();
            con.Close();

            loadTg();

            txtmg.Text = "";
            cbomp.SelectedIndex = 0;
            cbott.SelectedIndex = 0;

            ThemMoi.Visible = true;
            btnBackCell.Visible = false;
            btnTK.Visible = true;
            btnBack_Tk.Visible = false;

            btnluu.Visible = false;
            btnBack.Visible = false;

            txtmg.Enabled = false;
            txtmg.BackColor = Color.White;

            cbomp.Enabled = false;
            cbomp.BackColor = Color.LightYellow;

            cbott.Enabled = false;
            cbott.BackColor = Color.LightCyan;
        }

        private void GiuongBenh_Load(object sender, EventArgs e)
        {
            btnluu.Visible = false;
            btnsua.Visible=false;
            btnxoa.Visible=false;
            btnBack.Visible=false;
            btnBack_Tk.Visible=false;
            btnBackCell.Visible=false;
            cboMalop();
            loadTg();
            cbott.SelectedIndex = 0; 

            // Xóa dòng chọn & không hiển thị ô màu xanh
            dgvG.ClearSelection();
            dgvG.CurrentCell = null;

            txtmg.Enabled = false;
            txtmg.BackColor = Color.White; 

            cbomp.Enabled = false;
            cbomp.BackColor = Color.LightYellow;  

            cbott.Enabled = false;
            cbott.BackColor = Color.LightCyan;  


        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (dgvG.CurrentRow != null)
            {
                try
                {
                    // Lấy dữ liệu từ hàng đang chọn
                    string mg = dgvG.CurrentRow.Cells[0]?.Value?.ToString(); 
                    string mp = dgvG.CurrentRow.Cells[1]?.Value?.ToString(); 
                    string tt = dgvG.CurrentRow.Cells[3]?.Value?.ToString(); 

                    if (string.IsNullOrEmpty(mg) || string.IsNullOrEmpty(mp) || string.IsNullOrEmpty(tt))
                    {
                        MessageBox.Show("Dữ liệu không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Khởi tạo form cập nhật và truyền dữ liệu
                    Giuong_CapNhat formCapNhat = new Giuong_CapNhat(mg, mp, tt);

                    // Đăng ký sự kiện nhận dữ liệu sau khi cập nhật
                    formCapNhat.OnDataUpdated += (newMaGiuong, newMaPhong, newTrangThai) =>
                    {
                        // Load lại danh sách giường bệnh sau khi cập nhật
                        loadTg();

                        // Gán lại dữ liệu mới vào các ô nhập liệu
                        txtmg.Text = newMaGiuong;
                        cbomp.SelectedValue = newMaPhong;
                        cbott.SelectedItem = newTrangThai;

                        // Không cho chỉnh sửa mã giường
                        txtmg.ReadOnly = true;
                    };

                    // Hiển thị form cập nhật
                    formCapNhat.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một giường để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            ThemMoi.Visible = false;
            btnXuat.Visible = false;
            btnNhap.Visible = false;
        }

        private void dgvG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvG.Rows[e.RowIndex];
                dgvG.ReadOnly = true;  // Mọi ô trong dgvPhong sẽ không thể chỉnh sửa khi nhấp vào

                // Hiển thị dữ liệu từ DataGridView lên các ô nhập
                txtmg.Text = row.Cells[0].Value?.ToString();
                cbomp.Text = row.Cells[1].Value?.ToString();
                cbott.Text = row.Cells[3].Value?.ToString();
                

                txtmg.ReadOnly = true;
                btnxoa.Visible = true;
                btnsua.Visible = true;
                btnBackCell.Visible = true;
                ThemMoi.Visible = false;
                btnTK.Visible = false;
                btnXuat.Visible = false;
                btnNhap.Visible = false;
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có chắc chắn muốn xoá giường bệnh này?", "THÔNG BÁO", MessageBoxButtons.OKCancel);

            if (kq == DialogResult.Cancel)
                return; 

            {
                string mg = dgvG.CurrentRow.Cells[0].Value.ToString();
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "DELETE FROM Giuong WHERE MaGiuong = @mg";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@mg", SqlDbType.VarChar, 10).Value = mg;
                cmd.ExecuteNonQuery();
                loadTg();
                cmd.Dispose();
                con.Close();

                cbott.Items.Insert(0, "--Chọn Trạng Thái--");
                cbott.SelectedIndex = 0;
                txtmg.Text = "";
                txtmg.ReadOnly = false;
                cbomp.SelectedIndex = 0;
                ThemMoi.Visible = true;
                btnXuat.Visible = true;
                btnluu.Visible = false;
                btnNhap.Visible = true;
                btnTK.Visible = true;
                btnBack.Visible = false;
                btnBack_Tk.Visible = false;
                btnBackCell.Visible = false;
                btnsua.Visible = false;
                btnxoa.Visible = false;

            }
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
            if (result == DialogResult.Cancel)
                return;
            Dispose();
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            //Form f = new GiuongBenh_TimKiem();
            //f.ShowDialog();
            // Vô hiệu hóa sự kiện CellClick của dgvT
            dgvG.CellClick -= dgvG_CellClick;

            dgvG.ReadOnly = true;  // Mọi ô trong dgvPhong sẽ không thể chỉnh sửa khi nhấp vào

            //// Khi tìm kiếm, thay đổi flag để hiển thị tất cả các phòng
            // showEmptyRoomsOnly = false;

            // Xác định kiểu tìm kiếm (tìm tất cả hay phòng trống)
            loadAllRooms = true;  // Nếu bạn muốn tìm tất cả các phòng
            cboMalop();  // Gọi lại để load dữ liệu mới

            // Ẩn các nút không cần thiết khi tìm kiếm
            btnBack_Tk.Visible = true;

            ThemMoi.Visible = false;
            btnBack.Visible = false;
            btnBackCell.Visible = false;
            btnsua.Visible = false;
            btnxoa.Visible = false;
            btnXuat.Visible = false;
            btnNhap.Visible = false;

            dgvG.ReadOnly = true;  // Mọi ô trong dgvPhong sẽ không thể chỉnh sửa khi nhấp vào

            // Nếu các ô nhập liệu đang bị khóa, mở khóa để nhập tìm kiếm
            if (!txtmg.Enabled)
            {
                txtmg.Enabled = true;
                cbomp.Enabled = true;
                cbott.Enabled = true;

                txtmg.BackColor = Color.White;
                cbomp.BackColor = SystemColors.Window;
                cbott.BackColor = SystemColors.Window;
                return; // Dừng lại để chờ nhập dữ liệu tìm kiếm
            }

            // Lấy dữ liệu từ các ô nhập liệu
            string mg = txtmg.Text.Trim();
            string mp = (cbomp.SelectedIndex > 0) ? cbomp.SelectedValue.ToString() : "";
            string tt = (cbott.SelectedIndex > 0) ? cbott.SelectedItem.ToString() : "";

            // Kiểm tra kết nối
            if (con.State == ConnectionState.Closed)
                con.Open();

            // Tạo câu lệnh SQL động
            string search = @"SELECT MaGiuong, MaPhong, TrangThai FROM Giuong WHERE 1=1";
            List<SqlParameter> parameters = new List<SqlParameter>();

            // Thêm điều kiện vào truy vấn nếu có giá trị nhập vào
            if (!string.IsNullOrEmpty(mg))
            {
                search += " AND MaGiuong LIKE @mg";
                parameters.Add(new SqlParameter("@mg", "%" + mg + "%"));
            }
            if (cbomp.SelectedIndex > 0 && mp != null)
            {
                search += " AND MaPhong = @mp";
                parameters.Add(new SqlParameter("@mp", mp));
            }
            if (!string.IsNullOrEmpty(tt))
            {
                search += " AND TrangThai LIKE @tt";
                parameters.Add(new SqlParameter("@tt", "%" + tt + "%"));
            }

            // Kiểm tra nếu không nhập gì thì báo lỗi
            if (parameters.Count == 0)
            {
                MessageBox.Show("Vui lòng nhập ít nhất một tiêu chí để tìm kiếm!", "Thông báo");
                return;
            }

            // Thực hiện truy vấn
            SqlCommand cmd = new SqlCommand(search, con);
            cmd.Parameters.AddRange(parameters.ToArray());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            da.Fill(tb);
            con.Close();

            // Hiển thị kết quả tìm kiếm
            if (tb.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy giường bệnh phù hợp với tiêu chí tìm kiếm.", "Thông báo");
            }
            else
            {
                dgvG.DataSource = tb;
                dgvG.Refresh();
            }


            // Đảm bảo ComboBox được bật và có thể tương tác
            cbomp.Enabled = true;  // Đảm bảo ComboBox có thể tương tác khi tìm kiếm
            cbomp.Visible = true;  // Đảm bảo ComboBox không bị ẩn
        }
        

        private void txt_tk_mg_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số và phím Backspace
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;  // Ngăn không cho nhập ký tự không hợp lệ
                MessageBox.Show("Vui lòng chỉ nhập số!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtmg_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số và phím Backspace
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;  // Ngăn không cho nhập ký tự không hợp lệ
                MessageBox.Show("Vui lòng chỉ nhập số!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private bool isAddingNew = false;

        private void ThemMoi_Click(object sender, EventArgs e)
        {
            // Vô hiệu hóa sự kiện CellClick của dgvT
            dgvG.CellClick -= dgvG_CellClick;
            dgvG.ReadOnly = true;  // Mọi ô trong dgvPhong sẽ không thể chỉnh sửa khi nhấp vào
            isAddingNew = true; // Đánh dấu đang ở chế độ Thêm mới

            // Vô hiệu hóa chọn dòng
            dgvG.ClearSelection();
            dgvG.CurrentCell = null;

            // Gọi hàm để load phòng trống vào ComboBox khi thêm mới
            LoadRoomsForAddingNew();

            // Không cho chọn dòng mới nhưng vẫn cuộn được
            dgvG.Enabled = true;
            dgvG.ReadOnly = true;


            btnsua.Visible = false;
            btnxoa.Visible = false;
            btnTK.Visible = false;
            btnBackCell.Visible = false;
            btnXuat.Visible = false;
            btnNhap.Visible = false;

            ThemMoi.Visible = false;
            btnluu.Visible = true;
            btnBack.Visible = true;

            txtmg.Clear();  
            cbomp.SelectedIndex = 0;  
            cbott.SelectedIndex = 0;  

            txtmg.Enabled = true;  
            txtmg.BackColor = Color.White;  
            txtmg.ForeColor = Color.Black;  

            cbomp.Enabled = true;  
            cbomp.BackColor = SystemColors.Window;  
            cbomp.ForeColor = Color.Black; 

            cbott.Enabled = true; 
            cbott.BackColor = SystemColors.Window;  
            cbott.ForeColor = Color.Black;  
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //// Xóa dòng chọn & không hiển thị ô màu xanh
            //dgvG.ClearSelection();
            //dgvG.CurrentCell = null;

            // Hiển thị hộp thoại cảnh báo với biểu tượng Warning
            DialogResult result = MessageBox.Show(
                "Bạn chắc chắn muốn thoát?",
                "Cảnh báo",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
            );
            if (result == DialogResult.Cancel)
                return;

            btnluu.Visible = false;
            btnBack.Visible = false;
            btnBackCell.Visible = false;
            btnBack_Tk.Visible = false;
            btnsua.Visible = false;
            btnxoa.Visible = false;

            // Kích hoạt lại sự kiện CellClick của dgvG
            dgvG.CellClick += dgvG_CellClick;

            ThemMoi.Visible = true;
            btnTK.Visible = true;
            btnXuat.Visible = true;
            btnNhap.Visible = true;
            
            txtmg.Clear();  
            cbomp.SelectedIndex = 0;  
            cbott.SelectedIndex = 0; 

            txtmg.Enabled = false; 
            txtmg.BackColor = Color.White; 
            txtmg.ForeColor = Color.Gray;  

            cbomp.Enabled = false;  
            cbomp.BackColor = SystemColors.Control;  
            cbomp.ForeColor = Color.Gray; 

            cbott.Enabled = false; 
            cbott.BackColor = SystemColors.Control; 
            cbott.ForeColor = Color.Gray; 
        }

        private void btnBack_Tk_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại cảnh báo với biểu tượng Warning
            DialogResult result = MessageBox.Show(
                "Bạn chắc chắn muốn quay lại?",
                "Cảnh báo",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
            );
            if (result == DialogResult.Cancel)
                return;

            // Xóa dòng chọn & không hiển thị ô màu xanh
            dgvG.ClearSelection();
            dgvG.CurrentCell = null;

            // Kích hoạt lại sự kiện CellClick của dgvG
            dgvG.CellClick += dgvG_CellClick;
             
            btnBack_Tk.Visible = false;

            ThemMoi.Visible = true;
            btnXuat.Visible = true;
            btnNhap.Visible = true;
            btnsua.Visible = false;
            btnxoa.Visible = false;

            txtmg.Clear();
            cbomp.SelectedIndex = 0;
            cbott.SelectedIndex =0;

            txtmg.Enabled = false;
            cbomp.Enabled = false;
            cbott.Enabled = false;

            txtmg.BackColor = Color.White;
            cbomp.BackColor = SystemColors.Control;
            cbott.BackColor = SystemColors.Control;

            loadTg();
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


            btnluu.Visible = false;
            btnBack.Visible = false;
            btnBackCell.Visible = false;
            btnBack_Tk.Visible = false;
            btnsua.Visible = false;
            btnxoa.Visible = false;

            // Kích hoạt lại sự kiện CellClick của dgvG
            dgvG.CellClick += dgvG_CellClick;

            // Xóa dòng chọn & không hiển thị ô màu xanh
            dgvG.ClearSelection();
            dgvG.CurrentCell = null;

            ThemMoi.Visible = true;
            btnTK.Visible = true;
            btnXuat.Visible = true;
            btnNhap.Visible = true;
            txtmg.Clear();
            cbomp.SelectedIndex = 0;
            cbott.SelectedIndex = 0;

            txtmg.Enabled = false;
            txtmg.BackColor = Color.White;
            txtmg.ForeColor = Color.Gray;

            cbomp.Enabled = false;
            cbomp.BackColor = SystemColors.Control;
            cbomp.ForeColor = Color.Gray;

            cbott.Enabled = false;
            cbott.BackColor = SystemColors.Control;
            cbott.ForeColor = Color.Gray;
        
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

            // Tiêu đề chính
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "C1");
            head.MergeCells = true;
            head.Value2 = "DANH SÁCH GIƯỜNG BỆNH";
            head.Font.Bold = true;
            head.Font.Name = "Times New Roman";
            head.Font.Size = "18";
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tiêu đề cột
            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");
            cl1.Value2 = "MÃ GIƯỜNG";
            cl1.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
            cl2.Value2 = "MÃ PHÒNG";
            cl2.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
            cl3.Value2 = "TRẠNG THÁI";
            cl3.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "C3");
            rowHead.Font.Bold = true;
            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            rowHead.Interior.ColorIndex = 15;
            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Chuyển dữ liệu từ DataTable vào mảng
            object[,] arr = new object[tb.Rows.Count, tb.Columns.Count];
            for (int r = 0; r < tb.Rows.Count; r++)
            {
                DataRow dr = tb.Rows[r];
                for (int c = 0; c < tb.Columns.Count; c++)
                {
                    arr[r, c] = dr[c];
                }
            }

            // Vùng điền dữ liệu
            int rowStart = 4;
            int columnStart = 1;
            int rowEnd = rowStart + tb.Rows.Count - 1;
            int columnEnd = tb.Columns.Count;

            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];
            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];
            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);
            range.Value2 = arr;
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
        }
        private void btnXuat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đang tải dữ liệu, vui lòng chờ...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Tạo DataTable để lưu dữ liệu từ DataGridView
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã Giường", typeof(string));
            dt.Columns.Add("Mã Phòng", typeof(string));
            dt.Columns.Add("Trạng Thái", typeof(string));

            // Lấy dữ liệu từ DataGridView
            foreach (DataGridViewRow row in dgvG.Rows)
            {
                if (!row.IsNewRow) // Bỏ qua dòng trống cuối cùng
                {
                    dt.Rows.Add(
                        row.Cells[0].Value?.ToString(),
                        row.Cells[1].Value?.ToString(),
                        row.Cells[3].Value?.ToString()
                    );
                }
            }

            // Gọi hàm xuất Excel
            ExportExcel(dt, "Danh sách giường bệnh");

            //MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ReadExcel(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                MessageBox.Show("Chưa chọn file!");
                return;
            }

            // Mở ứng dụng Excel
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbooks workbooks = excelApp.Workbooks;
            Excel.Workbook workbook = workbooks.Open(filename);

            foreach (Excel.Worksheet worksheet in workbook.Sheets)
            {
                int i = 2;  // Dữ liệu bắt đầu từ dòng số 2
                do
                {
                    if (worksheet.Cells[i, 1].Value == null && worksheet.Cells[i, 2].Value == null && worksheet.Cells[i, 3].Value == null)
                    {
                        break; // Nếu cả 3 cột đều null thì dừng lại
                    }

                    // Lấy giá trị từ Excel, kiểm tra null
                    string maGiuong = worksheet.Cells[i, 1].Value?.ToString().Trim() ?? "";
                    string maPhong = worksheet.Cells[i, 2].Value?.ToString().Trim() ?? "";
                    string trangThai = worksheet.Cells[i, 3].Value?.ToString().Trim() ?? "";

                    if (string.IsNullOrEmpty(maGiuong) || string.IsNullOrEmpty(maPhong))
                    {
                        MessageBox.Show($"Dữ liệu không hợp lệ ở dòng {i}. Bỏ qua dòng này!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        i++;
                        continue;
                    }

                    // Thêm mới giường bệnh vào database
                    ThemMoiGiuongBenh(maGiuong, maPhong, trangThai);
                    i++; // Chuyển sang dòng tiếp theo
                }
                while (true);
            }

            // Đóng workbook và ứng dụng Excel
            workbook.Close(false);
            excelApp.Quit();

            MessageBox.Show("Nhập dữ liệu thành công!", "Thông báo");
            loadTg(); // Cập nhật lại dữ liệu trên DataGridView
        }

        private void ThemMoiGiuongBenh(string maGiuong, string maPhong, string trangThai)
        {
            string sql = @"
                        IF EXISTS (SELECT 1 FROM PhongBenh WHERE MaPhong = @MaPhong) 
                        BEGIN
                            IF EXISTS (SELECT 1 FROM Giuong WHERE MaGiuong = @MaGiuong)
                                UPDATE Giuong SET MaPhong = @MaPhong, TrangThai = @TrangThai WHERE MaGiuong = @MaGiuong;
                            ELSE
                                INSERT INTO Giuong (MaGiuong, MaPhong, TrangThai) VALUES (@MaGiuong, @MaPhong, @TrangThai);
                        END";

            using (SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=QLBN_NHUNG;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaGiuong", maGiuong);
                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
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
            }
        }

        private void cbomp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbomp.SelectedIndex <= 0)
            {
                cbomp.BackColor = Color.LightGray;
            }
            else
            {
                cbomp.BackColor = SystemColors.Window;
            }
        }
    }
}
