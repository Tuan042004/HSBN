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
using DataTable = System.Data.DataTable;
using xls = Microsoft.Office.Interop.Excel;
namespace HSBN.QLBN
{
    public partial class BacSi : Form
    {
        public BacSi()
        {
            InitializeComponent();
            load_TenKhoa();
            load_NhanVienYTe();
            close();

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        public void close()
        {
            txtMaNhanVien.ReadOnly = true;
            txtBacSiDieuTri.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            txtSDT.ReadOnly = true;
            txtCCCD.ReadOnly = true;
            cbxGioiTinh.Enabled = false;
            cbxTenKhoa.Enabled = false;
            dtNgaySinh.Enabled = false;
            btnSua.Visible = false;
            btnXoa.Visible = false;
            btnLuu.Visible = false;
        }
        public void open()
        {
            txtMaNhanVien.ReadOnly = false;
            txtBacSiDieuTri.ReadOnly = false;
            txtDiaChi.ReadOnly = false;
            txtSDT.ReadOnly = false;
            txtCCCD.ReadOnly = false;
            cbxGioiTinh.Enabled = true;
            cbxTenKhoa.Enabled = true;
            dtNgaySinh.Enabled = true;
        }
        public void xoatrang()
        {
            txtMaNhanVien.Clear();
            txtBacSiDieuTri.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtCCCD.Clear();
            txtTkMaBN.Clear();
            txtTkHoTen.Clear();
            cbxTkGioiTinh.SelectedItem = null;
            cbxTkTenKhoa.SelectedItem = null;
            cbxGioiTinh.SelectedItem = null;
            cbxTenKhoa.SelectedItem = null;
            txtMaNhanVien.ReadOnly = false;
        }
        private void nhapSo(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho nhập ký tự
            }
        }
        public void load_TenKhoa()
        {
            string sql = "SELECT * FROM Khoa";
            data.hienThiComboBox(cbxTenKhoa, sql, "TenKhoa", "TenKhoa");
            data.hienThiComboBox(cbxTkTenKhoa, sql, "TenKhoa", "TenKhoa");
        }

        private void load_NhanVienYTe()
        {
            if (data.con.State == ConnectionState.Closed)
                data.con.Open();

            string sql = "SELECT * FROM NhanVienYTe";
            SqlCommand cmd = new SqlCommand(sql, data.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvBenhNhan.DataSource = dt;
            dgvBenhNhan.Refresh();

            cmd.Dispose();
            data.con.Close();
        }
        public bool check_trungMaNhanVien(string maNV)
        {
            if (data.con.State == ConnectionState.Closed)
                data.con.Open();

            string sql = "SELECT COUNT(*) FROM NhanVienYTe WHERE MaNhanVien = @maNV";
            SqlCommand cmd = new SqlCommand(sql, data.con);
            cmd.Parameters.AddWithValue("@maNV", maNV);

            int kqua = (int)cmd.ExecuteScalar();

            data.con.Close();

            return kqua > 0;
        }
        public void ExportExcel_BacSi(DataTable tb, string sheetname)
        {
            xls.Application oExcel = new xls.Application();
            xls.Workbooks oBooks;
            xls.Sheets oSheets;
            xls.Workbook oBook;
            xls.Worksheet oSheet;
            //Tạo mới một Excel WorkBook 
            oExcel.Visible = true;
            oExcel.DisplayAlerts = false;
            oExcel.Application.SheetsInNewWorkbook = 1;
            oBooks = oExcel.Workbooks;
            oBook = (xls.Workbook)(oExcel.Workbooks.Add(Type.Missing));
            oSheets = oBook.Worksheets;
            oSheet = (xls.Worksheet)oSheets.get_Item(1);
            oSheet.Name = sheetname;
            // Tạo phần đầu nếu muốn
            xls.Range head = oSheet.get_Range("A1", "I1");
            head.MergeCells = true;
            head.Value2 = "THỐNG KÊ THÔNG TIN VỀ BÁC SĨ";
            head.Font.Bold = true;
            head.Font.Name = "Tahoma";
            head.Font.Size = "18";
            head.HorizontalAlignment = xls.XlHAlign.xlHAlignCenter;
            // Tạo tiêu đề cột 
            xls.Range cl1 = oSheet.get_Range("A3", "A3");
            cl1.Value2 = "STT";
            cl1.ColumnWidth = 6.0;
            xls.Range cl2 = oSheet.get_Range("B3", "B3");
            cl2.Value2 = "MÃ BÁC SĨ";

            cl2.ColumnWidth = 20.0;
            xls.Range cl3 = oSheet.get_Range("C3", "C3");
            cl3.Value2 = "HỌ VÀ TÊN";
            cl3.ColumnWidth = 30.0;
            xls.Range cl4 = oSheet.get_Range("D3", "D3");
            cl4.Value2 = "NGÀY SINH";
            cl4.ColumnWidth = 12.0;
            xls.Range cl5 = oSheet.get_Range("E3", "E3");
            cl5.Value2 = "GIỚI TÍNH";
            cl5.ColumnWidth = 12.0;
            xls.Range cl6 = oSheet.get_Range("F3", "F3");
            cl6.Value2 = "KHOA";
            cl6.ColumnWidth = 15.0;
            //xls.Range cl6_1 = oSheet.get_Range("F4", "F1000");
            //cl6_1.Columns.NumberFormat = "dd/mm/yyyy";


            xls.Range cl7 = oSheet.get_Range("G3", "G3");
            cl7.Value2 = "ĐỊA CHỈ";
            cl7.ColumnWidth = 60;
            xls.Range cl8 = oSheet.get_Range("H3", "H3");
            cl8.Value2 = "SĐT";
            cl8.ColumnWidth = 60;
            xls.Range cl9 = oSheet.get_Range("I3", "I3");
            cl9.Value2 = "CCCD";
            cl9.ColumnWidth = 60;
            //xls.Range cl8 = oSheet.get_Range("H3", "H3");
            //cl8.Value2 = "GHI CHÚ";
            //cl8.ColumnWidth = 15.0;
            xls.Range rowHead = oSheet.get_Range("A3", "I3");
            rowHead.Font.Bold = true;
            // Kẻ viền
            rowHead.Borders.LineStyle = xls.Constants.xlSolid;
            // Thiết lập màu nền
            rowHead.Interior.ColorIndex = 15;
            rowHead.HorizontalAlignment = xls.XlHAlign.xlHAlignCenter;
            // Tạo mảng đối tượng để lưu dữ toàn bồ dữ liệu trong DataTable,
            // Tạo mảng đối tượng để lưu toàn bộ dữ liệu trong DataTable
            object[,] arr = new object[tb.Rows.Count, tb.Columns.Count + 1]; // Thêm 1 cột cho STT

            // Chuyển dữ liệu từ DataTable vào mảng đối tượng
            for (int r = 0; r < tb.Rows.Count; r++)
            {
                arr[r, 0] = r + 1; // STT ở cột đầu tiên (A)
                DataRow dr = tb.Rows[r];

                for (int c = 0; c < tb.Columns.Count; c++)
                {
                    if (c == 4) // Cột ngày sinh (E) - chú ý: chỉ số mảng bắt đầu từ 0
                    {
                        DateTime tempDate;
                        if (DateTime.TryParse(dr[c].ToString(), out tempDate))
                        {
                            arr[r, c + 1] = tempDate.ToOADate(); // Chuyển thành kiểu số của Excel
                        }
                        else
                        {
                            arr[r, c + 1] = dr[c].ToString(); // Nếu không phải ngày hợp lệ, giữ nguyên
                        }
                    }
                    else
                    {
                        if (c == 6  || c == 7) // Cột số điện thoại (G - cột thứ 7)
                        {
                            arr[r, c + 1] = "'" + dr[c].ToString(); // Thêm dấu ' để giữ nguyên số 0
                        }
                        else
                        {
                            arr[r, c + 1] = dr[c]; // Các cột khác giữ nguyên
                        }

                    }
                }
            }
            //Thiết lập vùng điền dữ liệu
            int rowStart = 4;
            int columnStart = 1;
            int rowEnd = rowStart + tb.Rows.Count - 1;
            int columnEnd = tb.Columns.Count + 1;
            // Ô bắt đầu điền dữ liệu
            xls.Range c1 = (xls.Range)oSheet.Cells[rowStart, columnStart];
            // Ô kết thúc điền dữ liệu
            xls.Range c2 = (xls.Range)oSheet.Cells[rowEnd, columnEnd];
            // Lấy về vùng điền dữ liệu
            xls.Range range = oSheet.get_Range(c1, c2);
            //Điền dữ liệu vào vùng đã thiết lập
            range.Value2 = arr;
            // Định dạng lại cột ngày sinh (E)
            xls.Range dateColumn = oSheet.Range[oSheet.Cells[rowStart, 4], oSheet.Cells[rowEnd, 4]];
            dateColumn.NumberFormat = "dd/mm/yyyy";
            // Kẻ viền
            range.Borders.LineStyle = xls.Constants.xlSolid;
            // Căn giữa cột STT
            xls.Range c3 = (xls.Range)oSheet.Cells[rowEnd, columnStart];
            xls.Range c4 = oSheet.get_Range(c1, c3);
            oSheet.get_Range(c3, c4).HorizontalAlignment = xls.XlHAlign.xlHAlignCenter;
        }

        public void ThemBacSi(string maNV, string bacSi, DateTime ns, string gt, string tenKhoa, string dc, string sdt, string cccd)
        {
            if (data.con.State == ConnectionState.Closed)
            {
                data.con.Open();
            }
            String ngaySinhSql = ns.ToString("yyyy-MM-dd");
            string sql = "INSERT INTO NhanVienYTe VALUES ('" + maNV + "',N'" + bacSi + "','" + ns + "',N'" + gt + "',N'" + tenKhoa + "',N'" + dc + "','" + sdt + "','" + cccd + "')";
            SqlCommand cmd = new SqlCommand(sql, data.con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            data.con.Close();
        }

        public void ReadExcel_BacSi(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                MessageBox.Show("Chưa chọn file", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            xls.Application Excel = new xls.Application();
            xls.Workbook wb = Excel.Workbooks.Open(filename);

            try
            {
                foreach (xls.Worksheet ws in wb.Sheets)
                {
                    int i = 2; // Bắt đầu đọc từ dòng thứ 2 (bỏ qua tiêu đề)
                    while (ws.Cells[i, 1].Value != null)
                    {
                        string maBS = ws.Cells[i, 1].Value?.ToString().Trim();
                        string hoTen = ws.Cells[i, 2].Value?.ToString().Trim();
                        string gt = ws.Cells[i, 4].Value?.ToString().Trim();
                        string khoa = ws.Cells[i, 5].Value?.ToString().Trim();
                        string dc = ws.Cells[i, 6].Value?.ToString().Trim();
                        string sdt = ws.Cells[i, 7].Value?.ToString().Trim();
                        string cccd = ws.Cells[i, 8].Value?.ToString().Trim();

                        // Kiểm tra dữ liệu không rỗng
                        if (string.IsNullOrWhiteSpace(maBS) || string.IsNullOrWhiteSpace(hoTen) ||
                            string.IsNullOrWhiteSpace(gt) || string.IsNullOrWhiteSpace(khoa) ||
                            string.IsNullOrWhiteSpace(dc) || string.IsNullOrWhiteSpace(sdt) ||
                            string.IsNullOrWhiteSpace(cccd))
                        {
                            MessageBox.Show($"Dòng {i}: Thiếu dữ liệu, bỏ qua!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            i++;
                            continue;
                        }

                        // Kiểm tra trùng mã bác sĩ
                        if (check_trungMaNhanVien(maBS))
                        {
                            MessageBox.Show($"Trùng mã bác sĩ {maBS}, bỏ qua!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            i++;
                            continue;
                        }

                        // Xử lý ngày sinh
                        DateTime ngaySinh;
                        if (!DateTime.TryParse(ws.Cells[i, 3].Value?.ToString(), out ngaySinh))
                        {
                            ngaySinh = DateTime.MinValue; // Nếu lỗi, đặt giá trị mặc định
                        }

                        // Thêm vào cơ sở dữ liệu
                        ThemBacSi(maBS, hoTen, ngaySinh, gt, khoa, dc, sdt, cccd);
                        i++;
                    }
                }

                MessageBox.Show("Nhập dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load_NhanVienYTe();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                wb.Close(false);
                Excel.Quit();
            }
        }
        private void dgvBenhNhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int i = e.RowIndex;
                txtMaNhanVien.Text = dgvBenhNhan.Rows[i].Cells[0].Value.ToString();
                cbxTenKhoa.Text = dgvBenhNhan.Rows[i].Cells[4].Value.ToString();
                txtBacSiDieuTri.Text = dgvBenhNhan.Rows[i].Cells[1].Value.ToString();
                dtNgaySinh.Value = DateTime.Parse(dgvBenhNhan.Rows[i].Cells[2].Value.ToString());
                cbxGioiTinh.Text = dgvBenhNhan.Rows[i].Cells[3].Value.ToString();
                txtDiaChi.Text = dgvBenhNhan.Rows[i].Cells[5].Value.ToString();
                txtSDT.Text = dgvBenhNhan.Rows[i].Cells[6].Value.ToString();
                txtCCCD.Text = dgvBenhNhan.Rows[i].Cells[7].Value.ToString();
                btnSua.Visible = true;
                btnXoa.Visible = true;
                btnLuu.Visible = false;
                open();
                btnThem.Visible = false;
                txtMaNhanVien.ReadOnly = true; // Không cho sửa Mã bác sĩ

            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNhanVien.Text.Trim();
            string tenKhoa = cbxTenKhoa.SelectedValue?.ToString();
            string bacSi = txtBacSiDieuTri.Text.Trim();
            DateTime ns = dtNgaySinh.Value;
            string gt = cbxGioiTinh.SelectedItem == null ? "" : cbxGioiTinh.SelectedItem.ToString();
            string sdt = txtSDT.Text.Trim();
            string dc = txtDiaChi.Text.Trim();
            string cccd = txtCCCD.Text.Trim();
            string tenkhoa = cbxTenKhoa.SelectedValue.ToString();
            // Kiểm tra thông tin bắt buộc
            if (string.IsNullOrWhiteSpace(maNV) ||
                string.IsNullOrWhiteSpace(tenKhoa) ||
                string.IsNullOrWhiteSpace(bacSi) ||
                string.IsNullOrWhiteSpace(sdt) ||
                string.IsNullOrWhiteSpace(gt) ||
                string.IsNullOrWhiteSpace(dc) ||
                string.IsNullOrWhiteSpace(cccd) ||
                string.IsNullOrWhiteSpace(tenkhoa))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bác sĩ");
                return;
            }

            // Kiểm tra trùng mã bác sĩ
            if (check_trungMaNhanVien(maNV))
            {
                MessageBox.Show("Mã bác sĩ đã tồn tại", "THÔNG BÁO");
                txtMaNhanVien.Focus();
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

            // Câu lệnh SQL để chèn dữ liệu
            string sql = "INSERT INTO NhanVienYTe VALUES ('" + maNV + "',N'" + bacSi + "','" + ns + "',N'" + gt + "',N'" + tenKhoa + "',N'" + dc + "','" + sdt + "','" + cccd + "')";
            data.insert(sql);
            data.con.Close();
            load_NhanVienYTe();
        }

        private void cbxTenKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (data.con.State == ConnectionState.Closed)
            //    data.con.Open();

            //string sql = "SELECT MaKhoa, TenKhoa FROM Khoa";
            //SqlDataAdapter da = new SqlDataAdapter(sql, data.con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

            //cbxTenKhoa.DataSource = dt;
            //cbxTenKhoa.DisplayMember = "TenKhoa"; // Hiển thị tên khoa
            //cbxTenKhoa.ValueMember = "TenKhoa";    // Giá trị thực tế là  tên khoa

            //data.con.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNhanVien.Text.Trim();
            string bacSi = txtBacSiDieuTri.Text.Trim();
            DateTime ns = dtNgaySinh.Value;
            string gt;
            if (cbxGioiTinh.SelectedItem == null)
                gt = "";
            else
                gt = cbxGioiTinh.SelectedItem.ToString();
            string sdt = txtSDT.Text.Trim();
            string dc = txtDiaChi.Text.Trim();
            string cccd = txtCCCD.Text.Trim();
            string tenKhoa = cbxTenKhoa.SelectedValue?.ToString();

            if (string.IsNullOrWhiteSpace(maNV) ||
               string.IsNullOrWhiteSpace(tenKhoa) ||
                string.IsNullOrWhiteSpace(bacSi) ||
                string.IsNullOrWhiteSpace(sdt) ||
                string.IsNullOrWhiteSpace(gt) ||
                string.IsNullOrWhiteSpace(dc) ||
                string.IsNullOrWhiteSpace(cccd) ||
                string.IsNullOrWhiteSpace(tenKhoa))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bác sĩ");
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

            string sql = "UPDATE NhanVienYTe SET " +
                "BacSiDieuTri = N'" + bacSi + "', " +
                "NgaySinh = '" + ns + "', " +
                "GioiTinh = N'" + gt + "', " +
                "DiaChi = N'" + dc + "', " +
                "SDT = '" + sdt + "', " +
                "CCCD = '" + cccd + "', " +
                "TenKhoa = N'" + tenKhoa + "' " +
                "WHERE MaNhanVien = '" + maNV + "'";

            data.update(sql);
            MessageBox.Show("Cập nhật thành công!");
            load_NhanVienYTe();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có chắc chắn muốn xoá bác sĩ?",
                                  "THÔNG BÁO",
                                  MessageBoxButtons.OKCancel,
                                  MessageBoxIcon.Warning);

            if (kq == DialogResult.OK)
            {
                if (dgvBenhNhan.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn bác sĩ cần xóa!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maNV = dgvBenhNhan.CurrentRow.Cells[0].Value.ToString();
                string sql = "DELETE FROM NhanVienYTe WHERE MaNhanVien = '" + maNV + "'";
                data.delete(sql);
                MessageBox.Show("Xóa bác sĩ thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load_NhanVienYTe();
                xoatrang();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tim_ma = txtTkMaBN.Text.Trim();
            string tim_ten = txtTkHoTen.Text.Trim();
            string tim_gioitinh = cbxTkGioiTinh.SelectedItem == null ? "" : cbxTkGioiTinh.SelectedItem.ToString();
            string tim_khoa = cbxTkTenKhoa.SelectedValue == null ? "" : cbxTkTenKhoa.SelectedValue.ToString();

            if (data.con.State == ConnectionState.Closed)
                data.con.Open();

            string search = "SELECT * FROM NhanVienYTe " +
                            "WHERE MaNhanVien LIKE '%" + tim_ma + "%' " +
                            "AND BacSiDieuTri LIKE N'%" + tim_ten + "%' " +
                            "AND GioiTinh LIKE N'%" + tim_gioitinh + "%' " +
                            "AND TenKhoa LIKE N'%" + tim_khoa + "%'";

            SqlCommand cmd = new SqlCommand(search, data.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            da.Fill(tb);

            cmd.Dispose();
            data.con.Close();

            dgvBenhNhan.DataSource = tb;
            dgvBenhNhan.Refresh();
            xoatrang();
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Visible = false;
            btnXoa.Visible = false;
            btnLuu.Visible = true;
            btnThem.Visible = false;
            open();
        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
            txtSDT.KeyPress += new KeyPressEventHandler(nhapSo);
        }

        private void txtCCCD_TextChanged(object sender, EventArgs e)
        {
            txtCCCD.KeyPress += new KeyPressEventHandler(nhapSo);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cbxTkGioiTinh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxTkTenKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnNhapExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //txtDuongDan.Text = openFileDialog.FileName;
                ReadExcel_BacSi(openFileDialog.FileName);
                load_NhanVienYTe();
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            string tim_ma = txtTkMaBN.Text.Trim();
            string tim_ten = txtTkHoTen.Text.Trim();
            string tim_gioitinh = cbxTkGioiTinh.SelectedItem == null ? "" : cbxTkGioiTinh.SelectedItem.ToString();


            if (data.con.State == ConnectionState.Closed)
                data.con.Open();

            string search = "SELECT * FROM NhanVienYTe " +
                            "WHERE MaNhanVien LIKE '%" + tim_ma + "%' " +
                            "AND BacSiDieuTri LIKE N'%" + tim_ten + "%' " +
                            "AND GioiTinh LIKE N'%" + tim_gioitinh + "%' " ;

            SqlCommand cmd = new SqlCommand(search, data.con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            data.con.Close();

            ExportExcel_BacSi(tb, "Danh sách bác sĩ");
        }
    }
}

