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
namespace HSBN.HSXV
{
    public partial class Hoadon : Form
    {
        SqlConnection con = new SqlConnection("Data Source=TUANDEPZAI;Initial Catalog=QuanLyBenhNhan;Integrated Security=True");
        public Hoadon()
        {
            InitializeComponent();
        }

        private void xoatrang()
        {
            cbmhs.Text = "";
            txtmbn.Text = "";
            txthoten.Text = "";
            txtmaphong.Text = "";
            txtloaiphong.Text = "";
            txtll.Text = "";
            cbloai.SelectedItem = null;
            cbmathuoc.SelectedItem = null;
            txtthuoc.Text = "";
            txttt.Text = "";

        }

        private bool checktrungmhs(string mhs)
        {

            if (con.State == ConnectionState.Closed)

                con.Open();

            string sql = "Select count(*) from DieuTriThanhToan Where MaHoSoXuatVien = '" + mhs + "' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            int kq = (int)cmd.ExecuteScalar();
            if (kq > 0)
                return true;
            else
                return false;

        }
        private void Load_hd()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string sql = "Select * from DieuTriThanhToan";

                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable();
                da.Fill(tb);

                tbhd.DataSource = null;
                tbhd.Rows.Clear();
                tbhd.DataSource = tb;
                tbhd.Refresh();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void Hoadon_Load(object sender, EventArgs e)
        {
            try
            {

                // Lấy danh sách Mã Hồ Sơ Xuất Viện
                string sqlHoSoNhapVien = @"
                            SELECT hsx.MaHoSoXuatVien, hsnv.MaBenhNhan, hsnv.HoTenBenhNhan, 
                                   hsnv.MaPhong, pb.LoaiPhong
                            FROM HoSoNhapVien hsnv
                            JOIN HoSoXuatVien hsx ON hsx.MaBenhNhan = hsnv.MaBenhNhan
                            JOIN PhongBenh pb ON hsnv.MaPhong = pb.MaPhong";

                SqlDataAdapter daHoSoNhapVien = new SqlDataAdapter(sqlHoSoNhapVien, con);
                DataTable tbHoSoNhapVien = new DataTable();
                daHoSoNhapVien.Fill(tbHoSoNhapVien);

                DataRow r = tbHoSoNhapVien.NewRow();
                r["MaHoSoXuatVien"] = "---Chọn mã hồ sơ---";
                r["MaBenhNhan"] = DBNull.Value;
                r["HoTenBenhNhan"] = DBNull.Value;
                r["MaPhong"] = DBNull.Value;
                r["LoaiPhong"] = DBNull.Value;
                tbHoSoNhapVien.Rows.InsertAt(r, 0);

                cbmhs.DataSource = tbHoSoNhapVien;
                cbmhs.DisplayMember = "MaHoSoXuatVien";
                cbmhs.ValueMember = "MaHoSoXuatVien";

                cbmhs.SelectedIndexChanged += cbmhs_SelectedIndexChanged;

                // Lấy danh sách thuốc
                string sqlThuoc = "SELECT MaThuoc, TenThuoc FROM Thuoc";
                SqlDataAdapter daThuoc = new SqlDataAdapter(sqlThuoc, con);
                DataTable tbThuoc = new DataTable();
                daThuoc.Fill(tbThuoc);

                DataRow r1 = tbThuoc.NewRow();
                r1["MaThuoc"] = "---Chọn mã thuốc---";
                r1["TenThuoc"] = DBNull.Value;
                tbThuoc.Rows.InsertAt(r1, 0);

                cbmathuoc.DataSource = tbThuoc;
                cbmathuoc.DisplayMember = "MaThuoc";
                cbmathuoc.ValueMember = "MaThuoc";

                cbmathuoc.SelectedIndexChanged += cbmathuoc_SelectedIndexChanged;

                Load_hd();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }


        }

        private void cbmhs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbmhs.SelectedIndex > 0) // Kiểm tra nếu không phải dòng mặc định
            {
                DataRowView selectedRow = (DataRowView)cbmhs.SelectedItem;

                // Gán dữ liệu vào các controls
                txtmbn.Text = selectedRow["MaBenhNhan"].ToString();
                txthoten.Text = selectedRow["HoTenBenhNhan"].ToString();
                txtmaphong.Text = selectedRow["MaPhong"].ToString();
                txtloaiphong.Text = selectedRow["LoaiPhong"].ToString();

            }
            else
            {
                xoatrang();
            }
        }

        private void cbmathuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbmathuoc.SelectedIndex > 0) // Kiểm tra nếu không phải dòng mặc định
            {
                DataRowView selectedRow = (DataRowView)cbmathuoc.SelectedItem;

                // Gán dữ liệu vào các controls
                txtthuoc.Text = selectedRow["TenThuoc"].ToString();

            }
            else
            {
                xoatrang();
            }
            // Lấy mã thuốc chính xác
            string maThuoc = cbmathuoc.SelectedValue?.ToString(); // Dùng SelectedValue để lấy MaThuoc trực tiếp
            if (string.IsNullOrEmpty(maThuoc)) return;

            // Truy vấn lấy giá thuốc
            string query = "SELECT DonGia FROM Thuoc WHERE MaThuoc = @MaThuoc";

            using (SqlConnection conn = new SqlConnection("Data Source=TUANDEPZAI;Initial Catalog=QuanLyBenhNhan;Integrated Security=True"))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaThuoc", maThuoc);
                        object result = cmd.ExecuteScalar();
                        if (result != null && decimal.TryParse(result.ToString(), out decimal gia))
                        {
                            giaThuoc = gia;
                            CapNhatTongTien();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy giá thuốc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tbhd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            cbmhs.SelectedValue = tbhd.Rows[i].Cells[0].Value.ToString();
            txtmbn.Text = tbhd.Rows[i].Cells[1].Value.ToString();
            txthoten.Text = tbhd.Rows[i].Cells[2].Value.ToString();
            txtmaphong.Text = tbhd.Rows[i].Cells[3].Value.ToString();
            txtloaiphong.Text = tbhd.Rows[i].Cells[4].Value.ToString();
            cbmathuoc.SelectedValue = tbhd.Rows[i].Cells[5].Value.ToString();
            txtthuoc.Text = tbhd.Rows[i].Cells[6].Value.ToString();
            txtll.Text = tbhd.Rows[i].Cells[7].Value.ToString();
            txttt.Text = tbhd.Rows[i].Cells[8].Value.ToString();
            dtngaytt.Text = tbhd.Rows[i].Cells[9].Value.ToString();

        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string mhs = cbmhs.SelectedValue.ToString();

            if (checktrungmhs(mhs))
            {
                MessageBox.Show("Trùng mã hồ sơ");
                cbmhs.Focus();
                return;
            }
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string insertSql = @"INSERT INTO DieuTriThanhToan (MaHoSoXuatVien, MaBenhNhan, HoTenBenhNhan, MaPhong, LoaiPhong, MaThuoc, TenThuoc, LieuLuong, SoTien, NgayThanhToan) 
                             VALUES (@MaHoSoXuatVien, @MaBenhNhan, @HoTenBenhNhan, @MaPhong, @LoaiPhong, @MaThuoc, @TenThuoc, @LieuLuong, @SoTien, @NgayThanhToan)";

                SqlCommand cmd = new SqlCommand(insertSql, con);
                cmd.Parameters.AddWithValue("@MaHoSoXuatVien", cbmhs.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@MaBenhNhan", txtmbn.Text);
                cmd.Parameters.AddWithValue("@HoTenBenhNhan", txthoten.Text);
                cmd.Parameters.AddWithValue("@MaPhong", txtmaphong.Text);
                cmd.Parameters.AddWithValue("@LoaiPhong", txtloaiphong.Text);
                cmd.Parameters.AddWithValue("@MaThuoc", cbmathuoc.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@TenThuoc", txtthuoc.Text);
                cmd.Parameters.AddWithValue("@LieuLuong", txtll.Text);
                cmd.Parameters.AddWithValue("@SoTien", txttt.Text);
                cmd.Parameters.AddWithValue("@NgayThanhToan", dtngaytt.Text);

                int result = cmd.ExecuteNonQuery(); // Thực hiện INSERT

                con.Close();

                if (result > 0)
                {
                    MessageBox.Show("Lưu thành công!");
                    Load_hd();
                }
                else
                {
                    MessageBox.Show("Không thể lưu dữ liệu!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            xoatrang();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string mhs = cbmhs.SelectedValue.ToString();

            // Kiểm tra nếu mã tác giả bị trống
            if (string.IsNullOrEmpty(mhs))
            {
                MessageBox.Show("Vui lòng nhập mã hồ sơ để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbmhs.Focus();
                return;
            }


            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hồ sơ này không?", "Xác nhận",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }


            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string sql = "DELETE FROM DieuTriThanhToan WHERE MaHoSoXuatVien = @mhs";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@mhs", SqlDbType.NVarChar, 50).Value = mhs;


            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            con.Close();

            tbhd.DataSource = tb;
            tbhd.Refresh();

            Load_hd();
            xoatrang();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string updateSql = @"UPDATE DieuTriThanhToan 
                     SET MaBenhNhan = @MaBenhNhan, 
                         HoTenBenhNhan = @HoTenBenhNhan, 
                         MaPhong = @MaPhong, 
                         LoaiPhong = @LoaiPhong, 
                         MaThuoc = @MaThuoc, 
                         TenThuoc = @TenThuoc, 
                         LieuLuong = @LieuLuong, 
                         SoTien = @SoTien,
                         NgayThanhToan = @NgayThanhToan
                     WHERE MaHoSoXuatVien = @MaHoSoXuatVien";

                SqlCommand cmd = new SqlCommand(updateSql, con);
                cmd.Parameters.AddWithValue("@MaHoSoXuatVien", cbmhs.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@MaBenhNhan", txtmbn.Text);
                cmd.Parameters.AddWithValue("@HoTenBenhNhan", txthoten.Text);
                cmd.Parameters.AddWithValue("@MaPhong", txtmaphong.Text);
                cmd.Parameters.AddWithValue("@LoaiPhong", txtloaiphong.Text);
                cmd.Parameters.AddWithValue("@MaThuoc", cbmathuoc.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@TenThuoc", txtthuoc.Text);
                cmd.Parameters.AddWithValue("@LieuLuong", txtll.Text);
                cmd.Parameters.AddWithValue("@SoTien", txttt.Text);
                cmd.Parameters.AddWithValue("@NgayThanhToan", dtngaytt.Text);

                int result = cmd.ExecuteNonQuery(); // Thực hiện INSERT

                con.Close();

                if (result > 0)
                {
                    MessageBox.Show("Lưu thành công!");
                    Load_hd(); // Gọi lại hàm load dữ liệu
                }
                else
                {
                    MessageBox.Show("Không thể lưu dữ liệu!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            xoatrang();
        }

        public void ExportExcel(DataTable tb, string sheetname)
        {
            // Khởi tạo ứng dụng Excel
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();
            oExcel.Visible = true;
            oExcel.DisplayAlerts = false;
            oExcel.Application.SheetsInNewWorkbook = 1;

            // Tạo Workbook, Worksheet
            Microsoft.Office.Interop.Excel.Workbook oBook = oExcel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oBook.Sheets[1];
            oSheet.Name = sheetname;

            // Tiêu đề
            Microsoft.Office.Interop.Excel.Range head = oSheet.Range["A1", "J1"];
            head.MergeCells = true;
            head.Value2 = "DANH SÁCH ĐIỀU TRỊ THANH TOÁN";
            head.Font.Bold = true;
            head.Font.Name = "Times New Roman";
            head.Font.Size = 18;
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tiêu đề cột
            string[] columnTitles = { "Mã Hồ Sơ Xuất Viện", "Mã Bệnh Nhân", "Họ Tên Bệnh Nhân", "Mã Phòng", "Loại Phòng", "Mã Thuốc", "Tên Thuốc", "Liều Lượng", "Số Tiền", "Ngày Thanh Toán" };
            int columnCount = columnTitles.Length;

            for (int i = 0; i < columnCount; i++)
            {
                Microsoft.Office.Interop.Excel.Range cl = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[3, i + 1];
                cl.Value2 = columnTitles[i];
                cl.ColumnWidth = 20.0;
            }

            // Định dạng tiêu đề cột
            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.Range[oSheet.Cells[3, 1], oSheet.Cells[3, columnCount]];
            rowHead.Font.Bold = true;
            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            rowHead.Interior.ColorIndex = 15;
            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Chuyển dữ liệu từ DataTable vào mảng
            object[,] arr = new object[tb.Rows.Count, tb.Columns.Count];
            for (int r = 0; r < tb.Rows.Count; r++)
            {
                DataRow dr = tb.Rows[r];
                for (int c = 0; c < tb.Columns.Count; c++)
                {
                    if (tb.Columns[c].DataType == typeof(DateTime)) // Nếu là ngày tháng
                    {
                        arr[r, c] = ((DateTime)dr[c]).ToString("dd/MM/yyyy"); // Chuyển về dd/MM/yyyy
                    }
                    else
                    {
                        arr[r, c] = dr[c];
                    }
                }
            }

            // Vùng điền dữ liệu
            int rowStart = 4;
            int columnStart = 1;
            int rowEnd = rowStart + tb.Rows.Count - 1;
            int columnEnd = tb.Columns.Count;

            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];
            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];
            Microsoft.Office.Interop.Excel.Range range = oSheet.Range[c1, c2];
            range.Value2 = arr;
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
        }


        private void btxuat_Click(object sender, EventArgs e)
        {
            // Tạo DataTable để lưu dữ liệu từ DataGridView
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã Hồ Sơ Xuất Viện", typeof(string));
            dt.Columns.Add("Mã Bệnh Nhân", typeof(string));
            dt.Columns.Add("Họ Tên Bệnh Nhân", typeof(string));
            dt.Columns.Add("Mã Phòng", typeof(string));
            dt.Columns.Add("Loại Phòng", typeof(string));
            dt.Columns.Add("Mã Thuốc", typeof(string));
            dt.Columns.Add("Tên Thuốc", typeof(string));
            dt.Columns.Add("Liều Lượng", typeof(string));
            dt.Columns.Add("Số Tiền", typeof(float));
            dt.Columns.Add("Ngày Thanh Toán", typeof(string)); // Để giữ định dạng chuỗi ngày

            // Lấy dữ liệu từ DataGridView
            foreach (DataGridViewRow row in tbhd.Rows)
            {
                if (!row.IsNewRow) // Bỏ qua dòng trống cuối cùng
                {
                    dt.Rows.Add(
                        row.Cells[0].Value?.ToString(),
                        row.Cells[1].Value?.ToString(),
                        row.Cells[2].Value?.ToString(),
                        row.Cells[3].Value?.ToString(),
                        row.Cells[4].Value?.ToString(),
                        row.Cells[5].Value?.ToString(),
                        row.Cells[6].Value?.ToString(),
                        row.Cells[7].Value?.ToString(),
                        Convert.ToSingle(row.Cells[8].Value), // Chuyển sang kiểu float
                        row.Cells[9].Value?.ToString() // Lưu ngày dưới dạng chuỗi
                    );
                }
            }

            // Gọi hàm xuất Excel với tên sheet phù hợp
            ExportExcel(dt, "Danh sách điều trị thanh toán");
        }


        private void ReadExcel(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                MessageBox.Show("Chưa chọn file!");
                return;
            }

            Excel.Application ExcelApp = new Excel.Application();
            Excel.Workbook Workbook = ExcelApp.Workbooks.Open(filename);
            Excel.Worksheet Worksheet = (Excel.Worksheet)Workbook.Sheets[1];

            // Kiểm tra nếu DataGridView chưa có dữ liệu thì tạo mới DataTable
            DataTable dt;
            if (tbhd.DataSource == null)
            {
                dt = new DataTable();
                dt.Columns.Add("Mã Hồ Sơ Xuất Viện", typeof(string));
                dt.Columns.Add("Mã Bệnh Nhân", typeof(string));
                dt.Columns.Add("Họ Tên Bệnh Nhân", typeof(string));
                dt.Columns.Add("Mã Phòng", typeof(string));
                dt.Columns.Add("Loại Phòng", typeof(string));
                dt.Columns.Add("Mã Thuốc", typeof(string));
                dt.Columns.Add("Tên Thuốc", typeof(string));
                dt.Columns.Add("Liều Lượng", typeof(string));
                dt.Columns.Add("Số Tiền", typeof(float));
                dt.Columns.Add("Ngày Thanh Toán", typeof(string));
            }
            else
            {
                dt = (DataTable)tbhd.DataSource;
            }

            int i = 2; // Đọc từ dòng 2 (bỏ qua tiêu đề)

            while (Worksheet.Cells[i, 1].Value != null) // Kiểm tra có dữ liệu không
            {
                string maHSXuatVien = Worksheet.Cells[i, 1]?.Value?.ToString().Trim() ?? "";
                string maBenhNhan = Worksheet.Cells[i, 2]?.Value?.ToString().Trim() ?? "";
                string hoTenBenhNhan = Worksheet.Cells[i, 3]?.Value?.ToString().Trim() ?? "";
                string maPhong = Worksheet.Cells[i, 4]?.Value?.ToString().Trim() ?? "";
                string loaiPhong = Worksheet.Cells[i, 5]?.Value?.ToString().Trim() ?? "";
                string maThuoc = Worksheet.Cells[i, 6]?.Value?.ToString().Trim() ?? "";
                string tenThuoc = Worksheet.Cells[i, 7]?.Value?.ToString().Trim() ?? "";
                string lieuLuong = Worksheet.Cells[i, 8]?.Value?.ToString().Trim() ?? "";

                if (!float.TryParse(Worksheet.Cells[i, 9]?.Value?.ToString(), out float soTien))
                {
                    MessageBox.Show($"Lỗi số tiền ở dòng {i}");
                    break;
                }

                string ngayThanhToan = Worksheet.Cells[i, 10]?.Value?.ToString().Trim() ?? "";

                // Thêm dòng mới vào DataTable
                dt.Rows.Add(maHSXuatVien, maBenhNhan, hoTenBenhNhan, maPhong, loaiPhong, maThuoc, tenThuoc, lieuLuong, soTien, ngayThanhToan);
                ThemMoiDieuTriThanhToan(maHSXuatVien, maBenhNhan, hoTenBenhNhan, maPhong, loaiPhong, maThuoc, tenThuoc, lieuLuong, soTien, ngayThanhToan);
                i++; // Tăng dòng
            }

            // Đóng Workbook và giải phóng tài nguyên
            Workbook.Close(false);
            ExcelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(Worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(Workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelApp);

            // Cập nhật DataGridView mà không mất dữ liệu cũ
            tbhd.DataSource = dt;
        }
        private void ThemMoiDieuTriThanhToan(string maHSXuatVien, string maBenhNhan, string hoTenBenhNhan,
                                     string maPhong, string loaiPhong, string maThuoc, string tenThuoc,
                                     string lieuLuong, float soTien, string ngayThanhToan)
        {
            string query = "INSERT INTO DieuTriThanhToan (MaHoSoXuatVien, MaBenhNhan, HoTenBenhNhan, " +
                           "MaPhong, LoaiPhong, MaThuoc, TenThuoc, LieuLuong, SoTien, NgayThanhToan) " +
                           "VALUES (@MaHSXuatVien, @MaBenhNhan, @HoTenBenhNhan, @MaPhong, @LoaiPhong, " +
                           "@MaThuoc, @TenThuoc, @LieuLuong, @SoTien, @NgayThanhToan)";

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@MaHSXuatVien", maHSXuatVien);
                    cmd.Parameters.AddWithValue("@MaBenhNhan", maBenhNhan);
                    cmd.Parameters.AddWithValue("@HoTenBenhNhan", hoTenBenhNhan);
                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                    cmd.Parameters.AddWithValue("@LoaiPhong", loaiPhong);
                    cmd.Parameters.AddWithValue("@MaThuoc", maThuoc);
                    cmd.Parameters.AddWithValue("@TenThuoc", tenThuoc);
                    cmd.Parameters.AddWithValue("@LieuLuong", lieuLuong);
                    cmd.Parameters.AddWithValue("@SoTien", soTien);
                    cmd.Parameters.AddWithValue("@NgayThanhToan", ngayThanhToan);

                    cmd.ExecuteNonQuery();
                }
            }

        private void btnhap_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                ReadExcel(filePath); // Gọi hàm đọc Excel
            }
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            xoatrang();
            Load_hd();
        }

        decimal giaThuoc = 0;
        decimal mucGiamGia = 1;
        decimal giaPhong = 0;

        private void cbloai_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbloai.SelectedItem != null)
            {
                switch (cbloai.SelectedItem.ToString())
                {
                    case "90%":
                        mucGiamGia = 0.1m;
                        break;
                    case "70%":
                        mucGiamGia = 0.3m;
                        break;
                    case "50%":
                        mucGiamGia = 0.5m;
                        break;
                    default:
                        mucGiamGia = 1m;
                        break;
                }
                CapNhatTongTien();
            }
        }

        private void txtloaiphong_TextChanged(object sender, EventArgs e)
        {
            // Kiểm tra loại phòng và đặt giá tương ứng
            if (txtloaiphong.Text.Trim().Equals("Thường", StringComparison.OrdinalIgnoreCase))
            {
                giaPhong = 500000;
            }
            else if (txtloaiphong.Text.Trim().Equals("VIP", StringComparison.OrdinalIgnoreCase))
            {
                giaPhong = 1000000;
            }
            CapNhatTongTien();
        }
        private void CapNhatTongTien()
        {
            decimal tongTien = (giaThuoc + giaPhong) * mucGiamGia;
            txttt.Text = tongTien.ToString(""); 
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sql = @"SELECT * FROM DieuTriThanhToan 
                   WHERE(@MaHoSoXuatVien IS NULL OR MaHoSoXuatVien LIKE @MaHoSoXuatVien)
                   AND(@HoTenBenhNhan IS NULL OR HoTenBenhNhan LIKE @HoTenBenhNhan)";



            SqlCommand cmd = new SqlCommand(sql, con);

            // Nếu ô tìm kiếm rỗng thì truyền NULL vào tham số
            string maHS = string.IsNullOrEmpty(txtmhstk.Text) ? null : "%" + txtmhstk.Text + "%";
            string hoTen = string.IsNullOrEmpty(txthotentk.Text) ? null : "%" + txthotentk.Text + "%";

            cmd.Parameters.AddWithValue("@HoTenBenhNhan ", (object)hoTen ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaHoSoXuatVien", (object)maHS ?? DBNull.Value);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            da.Fill(tb);

            tbhd.DataSource = tb;
            tbhd.Refresh();

            con.Close();
        }
    }
}
