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
    public partial class ToaNha : Form
    {
        public ToaNha()
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
            String sql = "Select * from ToaNha";
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
            dgvT.DataSource = tb;
            dgvT.Refresh();

        }
        public bool checktrungMg(string mg)
        {
            //ket noi db 
            if (con.State == ConnectionState.Closed)
                con.Open();

            string sql = "select count(*) from ToaNha where MaToa = '" + mg + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            int kq = (int)cmd.ExecuteScalar();
            if (kq > 0)
                return true; //trung mtg
            else
                return false; //ko trung


        }
        private void ToaNha_Load(object sender, EventArgs e)
        {
            loadTg();
            dgvT.ReadOnly = true; 

            btnluu.Visible = false;
            btnsua.Visible = false;
            btnxoa.Visible = false;
            btnBack.Visible = false;
            btnbackcell.Visible = false;
            btnBack_Tk.Visible = false;

            txtmt.Enabled = false; 
            txtmt.BackColor = Color.White;  
            txtt.Enabled = false;  
            txtt.BackColor = Color.White;
            txtPt.Enabled = false;
            txtPt.BackColor = Color.White;
            txtTp.Enabled = false;
            txtTp.BackColor = Color.White;
        }

        private void btnluu_Click(object sender, EventArgs e)
        {

            String mt = txtmt.Text.Trim();
            String st = txtt.Text.Trim();
            String tp= txtTp.Text.Trim();
            String pt= txtPt.Text.Trim();

            if (string.IsNullOrEmpty(mt))
            {
                MessageBox.Show("Vui lòng nhập mã của tòa nhà!", "Thông báo");
                txtmt.Focus();
                return;
            }
            if (string.IsNullOrEmpty(st))
            {
                MessageBox.Show("Vui lòng nhập số tầng của tòa nhà!", "Thông báo");
                txtmt.Focus();
                return;
            }
            if (string.IsNullOrEmpty(tp))
            {
                MessageBox.Show("Vui lòng nhập tổng số phòng của tòa nhà!", "Thông báo");
                txtmt.Focus();
                return;
            }
            if (string.IsNullOrEmpty(pt))
            {
                MessageBox.Show("Vui lòng nhập số phòng tối đa mỗi tầng của tòa nhà!", "Thông báo");
                txtmt.Focus();
                return;
            }

            // Check trùng mã giường
            if (checktrungMg(mt))
            {
                MessageBox.Show("Trùng mã!", "THÔNG BÁO");
                txtmt.Focus();
                return;
            }

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            String sql = "INSERT INTO ToaNha  VALUES (@mt, @st, @tp, @pt)";

            // Tạo đối tượng command để thực thi câu lệnh lưu dữ liệu vào bảng 
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@mt", SqlDbType.VarChar, 50).Value = mt;
            cmd.Parameters.Add("@st", SqlDbType.Int, 50).Value = st;
            cmd.Parameters.Add("@tp", SqlDbType.Int, 50).Value = tp;
            cmd.Parameters.Add("@pt", SqlDbType.Int, 50).Value = pt;

            cmd.ExecuteNonQuery();

            MessageBox.Show("Lưu thành công", "Thông báo");

            // Giải phóng cmd và đóng kết nối
            cmd.Dispose();
            con.Close();
            loadTg();

            txtmt.Text = "";
            txtt.Text = "";
            txtTp.Text = "";
            txtPt.Text = "";

            ThemMoi.Visible = true;
            btnTK.Visible = true;
            btnXuat.Visible = true;
            btnNhap.Visible = true;

            btnluu.Visible = false;
            btnBack.Visible = false;

            txtmt.Enabled = false;  
            txtmt.BackColor = Color.White;  

            txtt.Enabled = false;  
            txtt.BackColor = Color.White;

            txtPt.Enabled = false;
            txtPt.BackColor = Color.White;

            txtTp.Enabled = false;
            txtTp.BackColor = Color.White;

            // Kích hoạt lại sự kiện CellClick của dgvT
            dgvT.CellClick += dgvT_CellClick;
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (dgvT.CurrentRow != null)
            {
                string mt = dgvT.CurrentRow.Cells[0].Value.ToString(); 
                string st = dgvT.CurrentRow.Cells[1].Value.ToString();
                string tp= dgvT.CurrentRow.Cells[2].Value.ToString();
                string pt = dgvT.CurrentRow.Cells[3].Value.ToString();

                // Tạo form cập nhật và truyền dữ liệu vào constructor
                Toa_Capnhat toa_CapNhat = new Toa_Capnhat(mt, st, tp,pt);

                // Đăng ký sự kiện để nhận dữ liệu khi cập nhật xong
                toa_CapNhat.OnDataUpdated += (newMaToa, newSoTang,  newTongPhong,  newPhongTang) =>
                {
                    loadTg();

                    // Gán lại dữ liệu mới vào các ô nhập liệu
                    txtmt.Text = newMaToa;
                    txtt.Text = newSoTang;
                    txtTp.Text = newTongPhong;
                    txtPt.Text = newPhongTang;

                    // Không cho phép chỉnh sửa Mã Tòa
                    txtmt.ReadOnly = true;
                };

                // Hiển thị Form cập nhật dưới dạng hộp thoại
                toa_CapNhat.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tòa nhà để cập nhật!", "Thông báo");
            }

            ThemMoi.Visible = false;
            btnXuat.Visible = false;
            btnNhap.Visible = false;
            btnbackcell.Visible = true;
            btnsua.Visible = true;
            btnxoa.Visible = true;
        }

        private void dgvT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem chỉ số hàng có hợp lệ không
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvT.Rows[e.RowIndex];
                dgvT.ReadOnly = true;  // Mọi ô trong dgvPhong sẽ không thể chỉnh sửa khi nhấp vào

                txtmt.Text = row.Cells[0].Value?.ToString();
                txtt.Text = row.Cells[1].Value?.ToString();
                txtTp.Text = row.Cells[2].Value?.ToString();
                txtPt.Text = row.Cells[3].Value?.ToString();

                txtmt.ReadOnly = true;
                btnxoa.Visible = true;
                btnsua.Visible = true;
                btnbackcell.Visible = true;
                ThemMoi.Visible = false;
                btnBack.Visible = false;
                btnXuat.Visible = false;
                btnNhap.Visible = false;
                btnTK.Visible = false;
                btnBack_Tk.Visible = false;
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (dgvT.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo");
                return;
            }

            DialogResult kq = MessageBox.Show("Bạn có chắc chắn muốn xoá tòa nhà này? (Phòng bệnh sẽ bị xóa)", "THÔNG BÁO", MessageBoxButtons.OKCancel);

            if (kq == DialogResult.Cancel)
                return;

            try
            {
                string mg = dgvT.CurrentRow.Cells[0].Value.ToString();

                if (con.State == ConnectionState.Closed)
                    con.Open();

                string deletePhongBenhSql = "DELETE FROM PhongBenh WHERE MaToa = @mg";
                using (SqlCommand cmd = new SqlCommand(deletePhongBenhSql, con))
                {
                    cmd.Parameters.Add("@mg", SqlDbType.VarChar, 10).Value = mg;
                    cmd.ExecuteNonQuery();

                }

                string deleteToaNhaSql = "DELETE FROM ToaNha WHERE MaToa = @mg";
                using (SqlCommand cmd = new SqlCommand(deleteToaNhaSql, con))
                {
                    cmd.Parameters.Add("@mg", SqlDbType.VarChar, 10).Value = mg;
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Xóa tòa nhà thành công!", "Thông báo");
                loadTg();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message, "Lỗi");
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                txtmt.Text = "";
                txtmt.ReadOnly = false;

                txtt.Text = "";
                txtt.ReadOnly = false;

                txtPt.Text = "";
                txtPt.ReadOnly = false;

                txtTp.Text = "";
                txtTp.ReadOnly = false;

                ThemMoi.Visible =true;
                btnXuat.Visible = false;
                btnNhap.Visible = false;
                btnsua.Visible = false;
                btnbackcell.Visible = false;    
                btnTK.Visible = true;
                btnBack_Tk.Visible = false;
                btnxoa.Visible = false;
            }
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            //Form f = new Toa_Timkiem();
            //f.ShowDialog();

            // Vô hiệu hóa sự kiện CellClick của dgvT
            dgvT.CellClick -= dgvT_CellClick;

            btnBack_Tk.Visible = true;
            ThemMoi.Visible = false;
            btnNhap.Visible = false;
            btnXuat.Visible = false;

            // Nếu các ô nhập liệu đang bị khóa, thì mở khóa và dừng lại
            if (!txtmt.Enabled)
            {
                txtmt.Enabled = true;
                txtt.Enabled = true;
                txtmt.BackColor = Color.White;
                txtt.BackColor = Color.White;

                txtPt.Enabled = true;
                txtPt.BackColor = Color.White;
                txtTp.Enabled = true;
                txtTp.BackColor = Color.White;
                return; 
            }

            // Lấy dữ liệu từ các ô nhập
            string maToa = txtmt.Text.Trim();
            string soTang = txtt.Text.Trim();
            string tp=txtTp.Text.Trim();
            string pt=txtPt.Text.Trim();

            if (con.State == ConnectionState.Closed)
                con.Open();

            // Tạo câu lệnh SQL động
            string search = @"SELECT MaToa, SoTang, SoPhongMax, SoPhongMaxMoiTang
                      FROM ToaNha
                      WHERE 1=1"; // Điều kiện mặc định để nối chuỗi SQL dễ dàng hơn

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(maToa))
            {
                search += " AND MaToa LIKE @maToa";
                parameters.Add(new SqlParameter("@maToa", "%" + maToa + "%"));
            }
            if (!string.IsNullOrEmpty(soTang))
            {
                search += " AND SoTang LIKE @soTang";
                parameters.Add(new SqlParameter("@soTang", "%" + soTang + "%"));
            }
            if (!string.IsNullOrEmpty(tp))
            {
                search += " AND SoPhongMax LIKE @tp";
                parameters.Add(new SqlParameter("@tp", "%" + tp + "%"));
            }
            if (!string.IsNullOrEmpty(pt))
            {
                search += " AND  SoPhongMaxMoiTang LIKE @pt";
                parameters.Add(new SqlParameter("@pt", "%" + pt + "%"));
            }

            // Kiểm tra nếu không nhập gì thì báo lỗi
            if (parameters.Count == 0)
            {
                MessageBox.Show("Vui lòng nhập ít nhất một tiêu chí để tìm kiếm!", "Thông báo");
                return;
            }

            SqlCommand cmd = new SqlCommand(search, con);
            cmd.Parameters.AddRange(parameters.ToArray());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            da.Fill(tb);
            con.Close();

            if (tb.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy tòa nhà phù hợp với tiêu chí tìm kiếm.", "Thông báo");
            }
            else
            {
                dgvT.DataSource = tb;
                dgvT.Refresh();
            }
        }

        private void txtt_KeyPress(object sender, KeyPressEventArgs e)
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
            dgvT.CellClick -= dgvT_CellClick;

            dgvT.ReadOnly = true;  // Mọi ô trong dgvPhong sẽ không thể chỉnh sửa khi nhấp vào
            dgvT.SelectionChanged += (s, args) => dgvT.ClearSelection();
            dgvT.CurrentCell = null; // Không chọn ô nào ngay từ đầu
            dgvT.Enabled = true; // Vẫn cuộn được
            dgvT.ReadOnly = true; // Không chỉnh sửa

            btnsua.Visible = false;
            btnxoa.Visible = false;
            btnbackcell.Visible = false;
            btnBack_Tk.Visible = false;
            btnTK.Visible = false;
            btnXuat.Visible = false;
            btnNhap.Visible = false;

            ThemMoi.Visible = false;
            btnluu.Visible = true;
            btnBack.Visible = true;

            txtmt.Clear();  
            txtt.Clear();

            txtmt.Enabled = true;  
            txtmt.BackColor = Color.White;  
            txtmt.ForeColor = Color.Black;  

            txtt.Enabled = true;  
            txtt.BackColor = Color.White;  
            txtt.ForeColor = Color.Black;

            txtPt.Enabled = true;
            txtPt.BackColor = Color.White;
            txtPt.ForeColor = Color.Black;

            txtTp.Enabled = true;
            txtTp.BackColor = Color.White;
            txtTp.ForeColor = Color.Black;
        }

        private void btnBack_Click(object sender, EventArgs e)
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

            // Kích hoạt lại sự kiện CellClick của dgvT
            dgvT.CellClick += dgvT_CellClick;

            btnluu.Visible = false;
            btnBack.Visible = false;

            btnTK.Visible = true;
            ThemMoi.Visible = true;
            btnXuat.Visible = true;
            btnNhap.Visible = true;

            btnsua.Visible = false;
            btnxoa.Visible = false;

            txtmt.Clear(); 
            txtt.Clear();
            txtPt.Clear();
            txtTp.Clear();

            txtmt.Enabled = false;
            txtmt.BackColor = Color.White;
            txtmt.ForeColor = Color.Gray;

            txtt.Enabled = false;
            txtt.BackColor = Color.White;
            txtt.ForeColor = Color.Gray;

            txtPt.Enabled = false;
            txtPt.BackColor = Color.White;
            txtPt.ForeColor = Color.Gray;

            txtTp.Enabled = false;
            txtTp.BackColor = Color.White;
            txtTp.ForeColor = Color.Gray;
        }

        private void btnbackcell_Click(object sender, EventArgs e)
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
            btnbackcell.Visible = false;

            // Kích hoạt lại sự kiện CellClick của dgvG
            dgvT.CellClick += dgvT_CellClick;

            // Xóa dòng chọn & không hiển thị ô màu xanh
            dgvT.ClearSelection();
            dgvT.CurrentCell = null;

            ThemMoi.Visible = true;
            btnXuat.Visible = true;
            btnNhap.Visible = true;
            btnsua.Visible = false;
            btnxoa.Visible = false;
            btnTK.Visible = true;

            txtmt.Clear(); 
            txtt.Clear();
            txtTp.Clear();
            txtPt.Clear();


            txtmt.Enabled = false;  
            txtmt.BackColor = Color.White; 
            txtmt.ForeColor = Color.Gray;  

            txtt.Enabled = false;
            txtt.BackColor = Color.White;  
            txtt.ForeColor = Color.Gray;

            txtPt.Enabled = false;
            txtPt.BackColor = Color.White;
            txtPt.ForeColor = Color.Gray;

            txtTp.Enabled = false;
            txtTp.BackColor = Color.White;
            txtTp.ForeColor = Color.Gray;
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
            // Kích hoạt lại sự kiện CellClick của dgvT
            dgvT.CellClick += dgvT_CellClick;

            btnluu.Visible = false;
            btnBack.Visible = false;
            btnBack_Tk.Visible = false;
            btnbackcell.Visible = false;

            ThemMoi.Visible = true;
            btnXuat.Visible = true;
            btnNhap.Visible = true;
            btnsua.Visible = false;
            btnxoa.Visible = false;

            txtmt.Clear();  
            txtt.Clear();
            txtTp.Clear();  
            txtPt.Clear();

            txtmt.Enabled = false;
            txtmt.BackColor = Color.White;
            txtmt.ForeColor = Color.Gray;

            txtt.Enabled = false;
            txtt.BackColor = Color.White;
            txtt.ForeColor = Color.Gray;

            txtPt.Enabled = false;
            txtPt.BackColor = Color.White;
            txtPt.ForeColor = Color.Gray;

            txtTp.Enabled = false;
            txtTp.BackColor = Color.White;
            txtTp.ForeColor = Color.Gray;
            loadTg();
        }

        public void ExportExcel(DataTable tb, string sheetname)
        {
            // Khởi tạo Excel
            var oExcel = new Microsoft.Office.Interop.Excel.Application();
            oExcel.Visible = true;
            oExcel.DisplayAlerts = false;

            var oBooks = oExcel.Workbooks;
            var oBook = oBooks.Add(Type.Missing);
            var oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oBook.Worksheets[1];
            oSheet.Name = sheetname;

            // Tiêu đề chính
            var head = oSheet.get_Range("A1", "D1");
            head.MergeCells = true;
            head.Value2 = "DANH SÁCH TÒA NHÀ";
            head.Font.Bold = true;
            head.Font.Name = "Times New Roman";
            head.Font.Size = 18;
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tiêu đề cột
            string[] columnTitles = { "MÃ TÒA", "SỐ TẦNG", "TỔNG SỐ PHÒNG", "SỐ PHÒNG MỖI TẦNG" };
            double[] columnWidths = { 20.0, 15.0, 25.0, 25.0 };

            for (int i = 0; i < columnTitles.Length; i++)
            {
                var cell = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[3, i + 1];
                cell.Value2 = columnTitles[i];
                cell.ColumnWidth = columnWidths[i];
            }

            // Định dạng hàng tiêu đề
            var rowHead = oSheet.get_Range("A3", "D3");
            rowHead.Font.Bold = true;
            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            rowHead.Interior.ColorIndex = 15;
            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Chuyển dữ liệu DataTable vào mảng
            object[,] arr = new object[tb.Rows.Count, tb.Columns.Count];
            for (int r = 0; r < tb.Rows.Count; r++)
            {
                for (int c = 0; c < tb.Columns.Count; c++)
                {
                    arr[r, c] = tb.Rows[r][c];
                }
            }

            // Vùng điền dữ liệu
            int rowStart = 4;
            int rowEnd = rowStart + tb.Rows.Count - 1;
            int colStart = 1;
            int colEnd = tb.Columns.Count;

            var fromCell = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, colStart];
            var toCell = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, colEnd];
            var dataRange = oSheet.get_Range(fromCell, toCell);

            dataRange.Value2 = arr;
            dataRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            dataRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        }


        private void btnXuat_Click(object sender, EventArgs e)
        {
            // Tạo DataTable với cấu trúc phù hợp
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã Tòa", typeof(string));
            dt.Columns.Add("Số Tầng", typeof(int));
            dt.Columns.Add("Tổng số phòng", typeof(int));
            dt.Columns.Add("Số phòng mỗi tầng", typeof(int));

            // Lấy dữ liệu từ DataGridView
            foreach (DataGridViewRow row in dgvT.Rows)
            {
                if (!row.IsNewRow)
                {
                    string maToa = row.Cells[0]?.Value?.ToString() ?? "";
                    bool parse1 = int.TryParse(row.Cells[1]?.Value?.ToString(), out int soTang);
                    bool parse2 = int.TryParse(row.Cells[2]?.Value?.ToString(), out int tongPhong);
                    bool parse3 = int.TryParse(row.Cells[3]?.Value?.ToString(), out int phongMoiTang);

                    if (parse1 && parse2 && parse3)
                    {
                        dt.Rows.Add(maToa, soTang, tongPhong, phongMoiTang);
                    }
                }
            }

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gọi hàm export
            ExportExcel(dt, "Danh sách tòa nhà");
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
                    if (worksheet.Cells[i, 1].Value == null && worksheet.Cells[i, 2].Value == null)
                    {
                        break; // Nếu cả 2 cột đều null thì dừng lại
                    }

                    // Lấy giá trị từ Excel, kiểm tra null
                    string maToa = worksheet.Cells[i, 1].Value?.ToString().Trim() ?? "";
                    string soTangStr = worksheet.Cells[i, 2].Value?.ToString().Trim() ?? "0";
                    string tongSoPhongStr = worksheet.Cells[i, 3].Value?.ToString().Trim() ?? "0";  // Cột Tổng số phòng
                    string soPhongMoiTangStr = worksheet.Cells[i, 4].Value?.ToString().Trim() ?? "0";
                    
                    if (string.IsNullOrEmpty(maToa) || !int.TryParse(soTangStr, out int soTang) ||
                                    !int.TryParse(tongSoPhongStr, out int tongSoPhong) || !int.TryParse(soPhongMoiTangStr, out int soPhongMoiTang))
                    {
                        MessageBox.Show($"Dữ liệu không hợp lệ ở dòng {i}. Bỏ qua dòng này!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        i++;
                        continue;
                    }

                    // Thêm mới tòa nhà vào database
                    ThemMoiToaNha(maToa, soTang, tongSoPhong, soPhongMoiTang);
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

        private void ThemMoiToaNha(string maToa, int soTang, int tongSoPhong, int soPhongMoiTang)
        {
            string sql = "INSERT INTO ToaNha (MaToa, SoTang, SoPhongMax, SoPhongMaxMoiTang) " +
                "VALUES (@MaToa, @SoTang, @TongSoPhong, @SoPhongMoiTang)";

            using (SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=QLBN_NHUNG;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaToa", maToa);
                    cmd.Parameters.AddWithValue("@SoTang", soTang);
                    cmd.Parameters.AddWithValue("@TongSoPhong", tongSoPhong);  // Thêm tham số cho Tổng số phòng
                    cmd.Parameters.AddWithValue("@SoPhongMoiTang", soPhongMoiTang);

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

        private void txtPt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số và phím Backspace
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;  // Ngăn không cho nhập ký tự không hợp lệ
                MessageBox.Show("Vui lòng chỉ nhập số!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtTp_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số và phím Backspace
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;  // Ngăn không cho nhập ký tự không hợp lệ
                MessageBox.Show("Vui lòng chỉ nhập số!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
