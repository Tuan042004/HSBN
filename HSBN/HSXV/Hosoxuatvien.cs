using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HSBN.QLBN;
using HSBN.HSXV;
using Excel = Microsoft.Office.Interop.Excel;

namespace HSBN.HSXV
{
    public partial class Hosoxuatvien : Form
    {
        SqlConnection con = new SqlConnection("Data Source=TUANDEPZAI;Initial Catalog=QuanLyBenhNhan;Integrated Security=True");
        public Hosoxuatvien()
        {
            InitializeComponent();
        }

        private void xoatrang()
        {
            txtmhs.Text = "";
            txthoten.Text = "";
            txtld.Text = "";
            cbmbn.SelectedIndex = -1;
            txttenkhoa.Text = "";
            cbmakhoa.SelectedIndex = -1;
            cbbs.SelectedIndex = -1;


        }

        private bool checktrungmhs(string mhs)
        {

            if (con.State == ConnectionState.Closed)

                con.Open();

            string sql = "Select count(*) from HoSoXuatVien Where MaHoSoXuatVien = '" + mhs + "' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            int kq = (int)cmd.ExecuteScalar();
            if (kq > 0)
                return true;
            else
                return false;

        }

        private void Load_hsxv()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string sql = @"SELECT hsxv.MaHoSoXuatVien, hsxv.MaBenhNhan, hsnv.HoTenBenhNhan, hsnv.NgayNhapVien, 
                              hsxv.MaKhoa, k.TenKhoa, hsxv.NgayXuat, hsxv.BacSiDieuTri, hsxv.LyDoXuat
                       FROM HoSoXuatVien hsxv
                       INNER JOIN HoSoNhapVien hsnv ON hsxv.MaBenhNhan = hsnv.MaBenhNhan
                       INNER JOIN Khoa k ON hsxv.MaKhoa = k.MaKhoa";

                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable();
                da.Fill(tb);

                tbhsxv.DataSource = null; // Xóa dữ liệu cũ
                tbhsxv.Rows.Clear();
                tbhsxv.DataSource = tb;  // Cập nhật dữ liệu mới
                tbhsxv.Refresh();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string mhs = txtmhs.Text.Trim();
            //string mbn = cbmbn.SelectedValue.ToString();
            //string hoten = txthoten.Text.Trim();
            //string nn = dtngaynhap.Value.ToString("yyyy-MM-dd");
            //string nx = dtngayxuat.Value.ToString("yyyy-MM-dd");
            //string mk = cbmakhoa.SelectedValue.ToString();
            //string tk = txttenkhoa.Text.ToString();
            //string bs = cbbs.SelectedValue.ToString();
            //string ld = txtld.Text.Trim();

            if (checktrungmhs(mhs))
            {
                MessageBox.Show("Trùng mã hồ sơ");
                txtmhs.Focus();
                return;
            }
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string insertSql = @"INSERT INTO HoSoXuatVien (MaHoSoXuatVien, MaBenhNhan,HoTenBenhNhan, MaKhoa, TenKhoa, NgayNhapVien, NgayXuat, BacSiDieuTri, LyDoXuat) 
                             VALUES (@MaHoSoXuatVien, @MaBenhNhan, @HoTenBenhNhan, @MaKhoa, @TenKhoa, @NgayNhap, @NgayXuat, @BacSiDieuTri, @LyDoXuat)";

                SqlCommand cmd = new SqlCommand(insertSql, con);
                cmd.Parameters.AddWithValue("@MaHoSoXuatVien", txtmhs.Text);
                cmd.Parameters.AddWithValue("@MaBenhNhan", cbmbn.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@HoTenBenhNhan", txthoten.Text);
                cmd.Parameters.AddWithValue("@MaKhoa", cbmakhoa.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@TenKhoa", txttenkhoa.Text);
                cmd.Parameters.AddWithValue("@NgayNhap", dtngaynhap.Text);
                cmd.Parameters.AddWithValue("@NgayXuat", dtngayxuat.Text);
                cmd.Parameters.AddWithValue("@BacSiDieuTri", cbbs.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@LyDoXuat", txtld.Text);

                int result = cmd.ExecuteNonQuery(); // Thực hiện INSERT

                con.Close();

                if (result > 0)
                {
                    MessageBox.Show("Lưu thành công!");
                    Load_hsxv(); // Gọi lại hàm load dữ liệu
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

        }

        private void Hosoxuatvien_Load(object sender, EventArgs e)
        {
            // 1. Lấy danh sách bệnh nhân từ HoSoNhapVien
            string sqlHoSoNhapVien = "SELECT MaBenhNhan, HoTenBenhNhan, NgayNhapVien, MaKhoa, TenKhoa, BacSiDieuTri FROM HoSoNhapVien";
            SqlDataAdapter daHoSoNhapVien = new SqlDataAdapter(sqlHoSoNhapVien, con);
            DataTable tbHoSoNhapVien = new DataTable();
            daHoSoNhapVien.Fill(tbHoSoNhapVien);

            // Thêm dòng mặc định
            DataRow r = tbHoSoNhapVien.NewRow();
            r["MaBenhNhan"] = "---Chọn mã bệnh nhân---";
            r["HoTenBenhNhan"] = DBNull.Value;
            r["NgayNhapVien"] = DBNull.Value;
            r["MaKhoa"] = DBNull.Value;
            r["TenKhoa"] = DBNull.Value;
            r["BacSiDieuTri"] = DBNull.Value;
            tbHoSoNhapVien.Rows.InsertAt(r, 0);

            cbmbn.DataSource = tbHoSoNhapVien;
            cbmbn.DisplayMember = "MaBenhNhan";
            cbmbn.ValueMember = "MaBenhNhan";

            cbmbn.SelectedIndexChanged += cbmbn_SelectedIndexChanged;

            // 2. Lấy danh sách Khoa từ bảng Khoa
            string sqlKhoa = "SELECT MaKhoa, TenKhoa FROM Khoa";
            SqlDataAdapter daKhoa = new SqlDataAdapter(sqlKhoa, con);
            DataTable tbKhoa = new DataTable();
            daKhoa.Fill(tbKhoa);

            // Thêm dòng mặc định
            DataRow r1 = tbKhoa.NewRow();
            r1["MaKhoa"] = "---Chọn mã khoa---";
            r1["TenKhoa"] = "";
            tbKhoa.Rows.InsertAt(r1, 0);

            cbmakhoa.DataSource = tbKhoa;
            cbmakhoa.DisplayMember = "MaKhoa";
            cbmakhoa.ValueMember = "MaKhoa";

            // 3. Lấy danh sách Bác sĩ từ HoSoNhapVien (Tránh trùng lặp bằng DISTINCT)
            string sqlBacSi = "SELECT DISTINCT BacSiDieuTri FROM HoSoNhapVien WHERE BacSiDieuTri IS NOT NULL";
            SqlDataAdapter daBacSi = new SqlDataAdapter(sqlBacSi, con);
            DataTable tbBacSi = new DataTable();
            daBacSi.Fill(tbBacSi);

            // Thêm dòng mặc định
            DataRow r2 = tbBacSi.NewRow();
            r2["BacSiDieuTri"] = "---Chọn bác sĩ---";
            tbBacSi.Rows.InsertAt(r2, 0);

            cbbs.DataSource = tbBacSi;
            cbbs.DisplayMember = "BacSiDieuTri";
            cbbs.ValueMember = "BacSiDieuTri";

            con.Close();
            Load_hsxv();
        }

        private void cbmbn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbmbn.SelectedIndex > 0) // Kiểm tra nếu không phải dòng mặc định
            {
                DataRowView selectedRow = (DataRowView)cbmbn.SelectedItem;

                // Gán dữ liệu vào các controls
                txthoten.Text = selectedRow["HoTenBenhNhan"].ToString();
                dtngaynhap.Value = Convert.ToDateTime(selectedRow["NgayNhapVien"]);

                // Đặt giá trị cho cbmakhoa (theo MaKhoa) và txttenkhoa
                cbmakhoa.SelectedValue = selectedRow["MaKhoa"].ToString();
                txttenkhoa.Text = selectedRow["TenKhoa"].ToString();

                // Đặt giá trị cho cbbs (Bác sĩ điều trị)
                cbbs.SelectedValue = selectedRow["BacSiDieuTri"].ToString();
            }
            else
            {
                xoatrang();
            }
        }

        private void tbhsxv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;

            // Kiểm tra xem hàng có hợp lệ không
            if (i >= 0 && tbhsxv.Rows[i] != null)
            {
                txtmhs.Text = tbhsxv.Rows[i].Cells[0].Value.ToString();
                cbmbn.SelectedValue = tbhsxv.Rows[i].Cells[1].Value.ToString();
                if (tbhsxv.Rows[i].Cells[3].Value != null && !string.IsNullOrEmpty(tbhsxv.Rows[i].Cells[3].Value.ToString()))
                {
                    DateTime ngayNhap;
                    if (DateTime.TryParse(tbhsxv.Rows[i].Cells[3].Value.ToString(), out ngayNhap))
                    {
                        dtngaynhap.Value = ngayNhap;
                    }
                    else
                    {
                        dtngaynhap.Value = DateTime.Now; // Gán giá trị mặc định nếu lỗi
                    }
                }

                if (tbhsxv.Rows[i].Cells[6].Value != null && !string.IsNullOrEmpty(tbhsxv.Rows[i].Cells[4].Value.ToString()))
                {
                    DateTime ngayXuat;
                    if (DateTime.TryParse(tbhsxv.Rows[i].Cells[6].Value.ToString(), out ngayXuat))
                    {
                        dtngayxuat.Value = ngayXuat;
                    }
                    else
                    {
                        dtngayxuat.Value = DateTime.Now;
                    }
                }

                //cbmakhoa.SelectedValue = tbhsxv.Rows[i].Cells[6].Value.ToString();
                txttenkhoa.Text = tbhsxv.Rows[i].Cells[5].Value.ToString();
                cbbs.SelectedValue = tbhsxv.Rows[i].Cells[7].Value.ToString() ;
                txtld.Text = tbhsxv.Rows[i].Cells[8].Value.ToString() ;
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            
            string mhs = txtmhs.Text.Trim();

            // Kiểm tra nếu mã tác giả bị trống
            if (string.IsNullOrEmpty(mhs))
            {
                MessageBox.Show("Vui lòng nhập mã hồ sơ để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmhs.Focus();
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

            string sql = "DELETE FROM HoSoXuatVien WHERE MaHoSoXuatVien = @mhs";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@mhs", SqlDbType.NVarChar, 50).Value = mhs;


            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            con.Close();

            tbhsxv.DataSource = tb;
            tbhsxv.Refresh();

            Load_hsxv();
            xoatrang();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string updateSql = @"UPDATE HoSoXuatVien 
                     SET MaBenhNhan = @MaBenhNhan, 
                         HoTenBenhNhan = @HoTenBenhNhan, 
                         MaKhoa = @MaKhoa, 
                         TenKhoa = @TenKhoa, 
                         NgayNhapVien = @NgayNhap, 
                         NgayXuat = @NgayXuat, 
                         BacSiDieuTri = @BacSiDieuTri, 
                         LyDoXuat = @LyDoXuat
                     WHERE MaHoSoXuatVien = @MaHoSoXuatVien";

                SqlCommand cmd = new SqlCommand(updateSql, con);
                cmd.Parameters.AddWithValue("@MaHoSoXuatVien", txtmhs.Text);
                cmd.Parameters.AddWithValue("@MaBenhNhan", cbmbn.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@HoTenBenhNhan", txtmhs.Text);
                cmd.Parameters.AddWithValue("@MaKhoa", cbmakhoa.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@TenKhoa", txtmhs.Text);
                cmd.Parameters.AddWithValue("@NgayNhap", dtngaynhap.Text);
                cmd.Parameters.AddWithValue("@NgayXuat", dtngayxuat.Text);
                cmd.Parameters.AddWithValue("@BacSiDieuTri", cbbs.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@LyDoXuat", txtld.Text);

                int result = cmd.ExecuteNonQuery(); // Thực hiện INSERT

                con.Close();

                if (result > 0)
                {
                    MessageBox.Show("Lưu thành công!");
                    Load_hsxv(); // Gọi lại hàm load dữ liệu
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
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Load_hsxv();
            xoatrang();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();

            string sql = @"SELECT hsxv.MaHoSoXuatVien, hsxv.MaBenhNhan, hsnv.HoTenBenhNhan, hsnv.NgayNhapVien, 
                          hsxv.MaKhoa, k.TenKhoa, hsxv.NgayXuat, hsxv.BacSiDieuTri, hsxv.LyDoXuat
                   FROM HoSoXuatVien hsxv
                   INNER JOIN HoSoNhapVien hsnv ON hsxv.MaBenhNhan = hsnv.MaBenhNhan
                   INNER JOIN Khoa k ON hsxv.MaKhoa = k.MaKhoa
                   WHERE (hsnv.HoTenBenhNhan LIKE @HoTen OR @HoTen IS NULL)
                   AND (hsxv.MaHoSoXuatVien LIKE @MaHS OR @MaHS IS NULL)";


            SqlCommand cmd = new SqlCommand(sql, con);

            // Nếu ô tìm kiếm rỗng thì truyền NULL vào tham số
            string maHS = string.IsNullOrEmpty(txtmhstk.Text) ? null : "%" + txtmhstk.Text + "%";
            string hoTen = string.IsNullOrEmpty(txthotentk.Text) ? null : "%" + txthotentk.Text + "%";

            cmd.Parameters.AddWithValue("@HoTen", (object)hoTen ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaHS", (object)maHS ?? DBNull.Value);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            da.Fill(tb);

            tbhsxv.DataSource = tb;
            tbhsxv.Refresh();

            con.Close();
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
            Microsoft.Office.Interop.Excel.Range head = oSheet.Range["A1", "H1"];
            head.MergeCells = true;
            head.Value2 = "DANH SÁCH HỒ SƠ XUẤT VIỆN";
            head.Font.Bold = true;
            head.Font.Name = "Times New Roman";
            head.Font.Size = 18;
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tiêu đề cột
            string[] columnTitles = { "Mã Hồ Sơ Xuất Viện", "Mã Bệnh Nhân", "Họ Tên Bệnh Nhân", "Ngày Nhập Viện", "Mã Khoa", "Tên Khoa", "Ngày Xuất", "Bác Sĩ Điều Trị", "Lý Do Xuất" };
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
            dt.Columns.Add("Ngày Nhập Viện", typeof(DateTime));
            dt.Columns.Add("Mã Khoa", typeof(string));
            dt.Columns.Add("Tên Khoa", typeof(string));
            dt.Columns.Add("Ngày Xuất", typeof(DateTime));
            dt.Columns.Add("Bác Sĩ Điều Trị", typeof(string));
            dt.Columns.Add("Lý Do Xuất", typeof(string));

            // Lấy dữ liệu từ DataGridView
            foreach (DataGridViewRow row in tbhsxv.Rows)
            {
                if (!row.IsNewRow) // Bỏ qua dòng trống cuối cùng
                {
                    dt.Rows.Add(
                        row.Cells[0].Value?.ToString(),
                        row.Cells[1].Value?.ToString(),
                        row.Cells[2].Value?.ToString(),
                        Convert.ToDateTime(row.Cells[3].Value),
                        row.Cells[4].Value?.ToString(),
                        row.Cells[5].Value?.ToString(),
                        Convert.ToDateTime(row.Cells[6].Value),
                        row.Cells[7].Value?.ToString(),
                        row.Cells[8].Value?.ToString()
                    );
                }
            }

            // Gọi hàm xuất Excel
            ExportExcel(dt, "Danh sách hồ sơ xuất viện");
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
            if (tbhsxv.DataSource == null)
            {
                dt = new DataTable();
                dt.Columns.Add("Mã Hồ Sơ Xuất Viện", typeof(string));
                dt.Columns.Add("Mã Bệnh Nhân", typeof(string));
                dt.Columns.Add("Họ Tên Bệnh Nhân", typeof(string));
                dt.Columns.Add("Ngày Nhập Viện", typeof(DateTime));
                dt.Columns.Add("Mã Khoa", typeof(string));
                dt.Columns.Add("Tên Khoa", typeof(string));
                dt.Columns.Add("Ngày Xuất", typeof(DateTime));
                dt.Columns.Add("Bác Sĩ Điều Trị", typeof(string));
                dt.Columns.Add("Lý Do Xuất", typeof(string));
            }
            else
            {
                dt = (DataTable)tbhsxv.DataSource;
            }

            int i = 2; // Đọc từ dòng 2 (bỏ qua tiêu đề)

            while (Worksheet.Cells[i, 1].Value != null) // Kiểm tra có dữ liệu không
            {
                string maHSXuatVien = Worksheet.Cells[i, 1]?.Value?.ToString().Trim() ?? "";
                string maBenhNhan = Worksheet.Cells[i, 2]?.Value?.ToString().Trim() ?? "";
                string hoTenBenhNhan = Worksheet.Cells[i, 3]?.Value?.ToString().Trim() ?? "";

                if (!DateTime.TryParse(Worksheet.Cells[i, 4]?.Value?.ToString(), out DateTime ngayNhapVien))
                {
                    MessageBox.Show($"Lỗi ngày nhập viện ở dòng {i}");
                    break;
                }

                string maKhoa = Worksheet.Cells[i, 5]?.Value?.ToString().Trim() ?? "";
                string tenKhoa = Worksheet.Cells[i, 6]?.Value?.ToString().Trim() ?? "";

                if (!DateTime.TryParse(Worksheet.Cells[i, 7]?.Value?.ToString(), out DateTime ngayXuat))
                {
                    MessageBox.Show($"Lỗi ngày xuất viện ở dòng {i}");
                    break;
                }

                string bacSiDieuTri = Worksheet.Cells[i, 8]?.Value?.ToString().Trim() ?? "";
                string lyDoXuat = Worksheet.Cells[i, 9]?.Value?.ToString().Trim() ?? "";

                // Thêm dòng mới vào DataTable
                dt.Rows.Add(maHSXuatVien, maBenhNhan, hoTenBenhNhan, ngayNhapVien, maKhoa, tenKhoa, ngayXuat, bacSiDieuTri, lyDoXuat);
                ThemMoiHoSo(maHSXuatVien, maBenhNhan, hoTenBenhNhan, ngayNhapVien, maKhoa, tenKhoa, ngayXuat, bacSiDieuTri, lyDoXuat);
                i++; // Tăng dòng
            }

            // Đóng Workbook và giải phóng tài nguyên
            Workbook.Close(false);
            ExcelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(Worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(Workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelApp);

            // Cập nhật DataGridView mà không mất dữ liệu cũ
            tbhsxv.DataSource = dt;
        }

            private void ThemMoiHoSo(string maHSXuatVien, string maBenhNhan, string hoTenBenhNhan, DateTime ngayNhapVien,
                             string maKhoa, string tenKhoa, DateTime ngayXuat, string bacSiDieuTri, string lyDoXuat)
            {
                string query = "INSERT INTO HoSoXuatVien (MaHoSoXuatVien, MaBenhNhan, HoTenBenhNhan, NgayNhapVien, MaKhoa, TenKhoa, NgayXuat, BacSiDieuTri, LyDoXuat) " +
                   "VALUES (@MaHSXuatVien, @MaBenhNhan, @HoTenBenhNhan, @NgayNhapVien, @MaKhoa, @TenKhoa, @NgayXuat, @BacSiDieuTri, @LyDoXuat)";

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@MaHSXuatVien", maHSXuatVien);
                        cmd.Parameters.AddWithValue("@MaBenhNhan", maBenhNhan);
                        cmd.Parameters.AddWithValue("@HoTenBenhNhan", hoTenBenhNhan);
                        cmd.Parameters.AddWithValue("@NgayNhapVien", ngayNhapVien);
                        cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);
                        cmd.Parameters.AddWithValue("@TenKhoa", tenKhoa);
                        cmd.Parameters.AddWithValue("@NgayXuat", ngayXuat);
                        cmd.Parameters.AddWithValue("@BacSiDieuTri", bacSiDieuTri);
                        cmd.Parameters.AddWithValue("@LyDoXuat", lyDoXuat);

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        con.Close();
                
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
    }
}
