using HSBN.QLBN;
using Microsoft.Office.Interop.Excel;
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

namespace HSBN
{
    public partial class BenhNhan : Form
    {
        public BenhNhan()
        {
            InitializeComponent();
            load_BenhNhan();
            close();

        }
        public void load_BenhNhan()
        {
            string sql = "SELECT MaBenhNhan, HoTenBenhNhan, NgaySinh, GioiTinh, DiaChi, SDT, CCCD, MBHYT FROM BenhNhan";
            data.hienThi(dgvBenhNhan, sql);
        }
        public void close()
        {
            txtMaBN.ReadOnly = true;
            txtHoten.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            txtSDT.ReadOnly = true;
            cbxGioiTinh.Enabled = false;
            dtNgaySinh.Enabled = false;
            txtBHYT.ReadOnly = true;
            txtCCCD.ReadOnly = true;
            btnSua.Visible = false;
            btnXoa.Visible = false;
            btnLuu.Visible = false;

        }
        public void open()
        {
            txtMaBN.ReadOnly = false;
            txtHoten.ReadOnly = false;
            txtDiaChi.ReadOnly = false;
            txtSDT.ReadOnly = false;
            cbxGioiTinh.Enabled = true;
            dtNgaySinh.Enabled = true;
            txtBHYT.ReadOnly = false;
            txtCCCD.ReadOnly = false;
        }
        public void xoatrang()
        {
            txtMaBN.Clear();
            txtHoten.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            cbxGioiTinh.SelectedItem = null;
            txtTkHoTen.Clear();
            txtTkMaBN.Clear();
            txtBHYT.Clear();
            txtCCCD.Clear();
            cbxTkGioiTinh.SelectedItem = null;
            txtTKCCCD.Clear();
            txtMaBN.ReadOnly = false;
        }
        public bool check_trungMaBenhNhan(string maBN)
        {
            if (data.con.State == ConnectionState.Closed)
                data.con.Open();

            // Tạo câu lệnh SQL kiểm tra mã bệnh nhân có tồn tại không
            string sql = "SELECT COUNT(*) FROM BenhNhan WHERE MaBenhNhan='" + maBN + "'";
            SqlCommand cmd = new SqlCommand(sql, data.con);
            int kqua = (int)cmd.ExecuteScalar();

            data.con.Close(); // Đóng kết nối sau khi kiểm tra

            if (kqua > 0)
                return true;
            else
                return false;
        }
        public void ExportExcel(DataTable tb, string sheetname)
        {
            //Tạo các đối tượng Excel

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
            head.Value2 = "THỐNG KÊ THÔNG TIN VỀ BỆNH NHẬP";
            head.Font.Bold = true;
            head.Font.Name = "Tahoma";
            head.Font.Size = "18";
            head.HorizontalAlignment = xls.XlHAlign.xlHAlignCenter;
            // Tạo tiêu đề cột 
            xls.Range cl1 = oSheet.get_Range("A3", "A3");
            cl1.Value2 = "STT";
            cl1.ColumnWidth = 6.0;
            xls.Range cl2 = oSheet.get_Range("B3", "B3");
            cl2.Value2 = "MÃ BỆNH NHÂN";

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
            cl6.Value2 = "ĐỊA CHỈ";
            cl6.ColumnWidth = 15.0;
            //xls.Range cl6_1 = oSheet.get_Range("F4", "F1000");
            //cl6_1.Columns.NumberFormat = "dd/mm/yyyy";


            xls.Range cl7 = oSheet.get_Range("G3", "G3");
            cl7.Value2 = "SĐT";
            cl7.ColumnWidth = 60;
            xls.Range cl8 = oSheet.get_Range("H3", "H3");
            cl8.Value2 = "CCCD";
            cl8.ColumnWidth = 60;
            xls.Range cl9 = oSheet.get_Range("I3", "I3");
            cl9.Value2 = "BHYT";
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
                        if (c == 6 || c ==5 || c==7) // Cột số điện thoại (G - cột thứ 7)
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
        public void ThemBenhNhan(String maBN, String hoTen,  DateTime ns, String gt, String dc, String sdt, String cccd, String mbhyt)
        {
            if (data.con.State == ConnectionState.Closed)
            {
                data.con.Open();
            }
            String ngaySinhSql = ns.ToString("yyyy-MM-dd");
            string sql = "INSERT INTO BenhNhan VALUES ('" + maBN + "',N'" + hoTen + "','" + ns + "',N'" + gt + "','" + dc + "','" + sdt + "','" + cccd + "','" + mbhyt + "')";
            SqlCommand cmd = new SqlCommand(sql, data.con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            data.con.Close();
        }
        private void ReadExcel_BenhNhan(string filename)
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
                foreach (xls.Worksheet wsheet in wb.Sheets)
                {
                    int i = 2; // Bắt đầu đọc từ dòng thứ 2 (bỏ qua tiêu đề)
                    while (wsheet.Cells[i, 1].Value != null)
                    {
                        string maBN = wsheet.Cells[i, 1].Value?.ToString().Trim();
                        string hoTen = wsheet.Cells[i, 2].Value?.ToString().Trim();
                        string gt = wsheet.Cells[i, 4].Value?.ToString().Trim();
                        string sdt = wsheet.Cells[i, 6].Value?.ToString().Trim();
                        string dc = wsheet.Cells[i, 5].Value?.ToString().Trim();
                        string cccd = wsheet.Cells[i, 7].Value?.ToString().Trim();
                        string mbhyt = wsheet.Cells[i, 8].Value?.ToString().Trim();

                        // Kiểm tra trùng mã bệnh nhân
                        if (check_trungMaBenhNhan(maBN))
                        {
                            MessageBox.Show($"Trùng mã bệnh nhân {maBN}, bỏ qua!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            i++;
                            continue;
                        }

                        // Xử lý ngày sinh
                        DateTime ngaySinh;
                        if (!DateTime.TryParse(wsheet.Cells[i, 3].Value?.ToString(), out ngaySinh))
                        {
                            ngaySinh = DateTime.MinValue;
                        }
                        // Thêm vào cơ sở dữ liệu
                        ThemBenhNhan(maBN, hoTen, ngaySinh, gt, dc, sdt, cccd, mbhyt);
                        i++;
                    }
                }

                MessageBox.Show("Nhập dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load_BenhNhan();
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

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void grbThongTin_Enter(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            {
                string maBN = txtMaBN.Text.Trim();
                string hoTen = txtHoten.Text.Trim();
                DateTime ns = dtNgaySinh.Value;
                string gt;
                if (cbxGioiTinh.SelectedItem == null)
                    gt = "";
                else
                    gt = cbxGioiTinh.SelectedItem.ToString();
                string sdt = txtSDT.Text.Trim();
                string dc = txtDiaChi.Text.Trim();
                string cccd = txtCCCD.Text.Trim();
                string mbhyt = txtBHYT.Text.Trim();

                // Kiểm tra thông tin bắt buộc
                if (string.IsNullOrWhiteSpace(maBN) ||
                    string.IsNullOrWhiteSpace(hoTen) ||
                    string.IsNullOrWhiteSpace(sdt) ||
                    string.IsNullOrWhiteSpace(gt) ||
                    string.IsNullOrWhiteSpace(dc) ||
                    string.IsNullOrWhiteSpace(cccd))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin bệnh nhân");
                    return;
                }

                // Kiểm tra trùng mã bệnh nhân
                if (check_trungMaBenhNhan(maBN))
                {
                    MessageBox.Show("Mã bệnh nhân đã tồn tại", "THÔNG BÁO");
                    txtMaBN.Focus();
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
                string patternBHYT = @"^\d{10}$";
                if (!Regex.IsMatch(mbhyt, patternBHYT))
                {
                    MessageBox.Show("Số BHYT không hợp lệ! Vui lòng nhập số có 10 chữ số!!");
                    return;
                }
                // Câu lệnh SQL để chèn dữ liệu
                string sql = "INSERT INTO BenhNhan VALUES ('" + maBN + "',N'" + hoTen + "','" + ns + "',N'" + gt + "','" + dc + "','" + sdt + "','" + cccd + "','" + mbhyt + "')";
                data.insert(sql);
                MessageBox.Show("Lưu thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                data.con.Close();
                load_BenhNhan();
            }
        }

        private void dgvBenhNhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                if (e.RowIndex >= 0) // Đảm bảo người dùng không click vào tiêu đề cột
                {
                    int i = e.RowIndex;  
                    txtMaBN.Text = dgvBenhNhan.Rows[i].Cells[0].Value.ToString();
                    txtHoten.Text = dgvBenhNhan.Rows[i].Cells[1].Value.ToString();
                    dtNgaySinh.Value = DateTime.Parse(dgvBenhNhan.Rows[i].Cells[2].Value.ToString());
                    cbxGioiTinh.Text = dgvBenhNhan.Rows[i].Cells[3].Value.ToString();
                    txtDiaChi.Text = dgvBenhNhan.Rows[i].Cells[4].Value.ToString();
                    txtSDT.Text = dgvBenhNhan.Rows[i].Cells[5].Value.ToString();
                    txtCCCD.Text = dgvBenhNhan.Rows[i].Cells[6].Value.ToString();
                    txtBHYT.Text = dgvBenhNhan.Rows[i].Cells[7].Value.ToString();
                    btnSua.Visible = true;
                    btnXoa.Visible = true;
                    btnLuu.Visible = false;
                    open();
                    btnThem.Visible = false;
                    txtMaBN.ReadOnly = true; // Khóa chỉnh sửa mã bệnh nhân
            }
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
                string maBN = txtMaBN.Text.Trim();
                string hoTen = txtHoten.Text.Trim();
                DateTime ns = dtNgaySinh.Value;
                string gt;
                if (cbxGioiTinh.SelectedItem == null)
                    gt = "";
                else
                    gt = cbxGioiTinh.SelectedItem.ToString();
                string sdt = txtSDT.Text.Trim();
                string dc = txtDiaChi.Text.Trim();
                string cccd = txtCCCD.Text.Trim();
                string mbhyt = txtBHYT.Text.Trim();

                if (string.IsNullOrWhiteSpace(maBN) ||
                   string.IsNullOrWhiteSpace(hoTen) ||
                   string.IsNullOrWhiteSpace(sdt) ||
                   string.IsNullOrWhiteSpace(gt) ||
                   string.IsNullOrWhiteSpace(dc) ||
                   string.IsNullOrWhiteSpace(cccd))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin bệnh nhân");
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
                string patternBHYT = @"^\d{10}$";
                if (!Regex.IsMatch(mbhyt, patternBHYT))
                    {
                        MessageBox.Show("Số BHYT không hợp lệ! Vui lòng nhập số có 10 chữ số!!");
                        return;
                    }
            string sql = "UPDATE BenhNhan SET HoTenBenhNhan = N'" + hoTen + "', " +
                "NgaySinh = '" + ns + "', " +
                "GioiTinh = N'" + gt + "', " +
                "DiaChi = N'" + dc + "', " +
                "SDT = '" + sdt + "', " +
                "CCCD = '" + cccd + "', " +
                "MBHYT = '" + mbhyt + "' " +
                "WHERE MaBenhNhan = '" + maBN + "'";
                data.update(sql);
                MessageBox.Show("Cập nhật thành công!");
                load_BenhNhan();
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có chắc chắn muốn xoá bệnh nhân?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (kq == DialogResult.OK)
            {
                string maBN = dgvBenhNhan.CurrentRow.Cells[0].Value.ToString();
                string sql = "DELETE FROM BenhNhan WHERE MaBenhNhan = '" + maBN + "'";
                data.delete(sql);
                MessageBox.Show("Xóa bệnh nhân thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load_BenhNhan(); // Load lại danh sách bệnh nhân
                xoatrang(); // Xóa dữ liệu trên form
            }
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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tim_ma = txtTkMaBN.Text.Trim();
            string tim_ten = txtTkHoTen.Text.Trim();
            string tim_gioitinh;
            if (cbxTkGioiTinh.SelectedItem == null)
                tim_gioitinh = "";
            else
                tim_gioitinh = cbxTkGioiTinh.SelectedItem.ToString();
            string tim_cccd = txtTKCCCD.Text.Trim();

            if (data.con.State == ConnectionState.Closed)
                data.con.Open();

            String search = "SELECT * FROM BenhNhan " +
                "WHERE MaBenhNhan LIKE '%" + tim_ma + "%' " +
                "AND HoTenBenhNhan LIKE N'%" + tim_ten + "%' " +
                "AND GioiTinh LIKE N'%" + tim_gioitinh + "%' " +
                "AND CCCD LIKE '%" + tim_cccd + "%'";

            SqlCommand cmd = new SqlCommand(search, data.con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            data.con.Close();

            dgvBenhNhan.DataSource = tb;
            dgvBenhNhan.Refresh();
            xoatrang();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Visible = false;
            btnXoa.Visible = false;
            btnLuu.Visible = true;
            btnThem.Visible = false;
            open();
        }
        private void nhapSo(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho nhập ký tự
            }
        }

        private void BenhNhan_Load(object sender, EventArgs e)
        {

        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
            txtSDT.KeyPress += new KeyPressEventHandler(nhapSo);
        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvBenhNhan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtCCCD_TextChanged(object sender, EventArgs e)
        {
            txtCCCD.KeyPress += new KeyPressEventHandler(nhapSo);
        }

        private void txtBHYT_TextChanged(object sender, EventArgs e)
        {
            txtBHYT.KeyPress += new KeyPressEventHandler(nhapSo);
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            // Tạo các cột từ DataGridView
            foreach (DataGridViewColumn column in dgvBenhNhan.Columns)
            {
                dt.Columns.Add(column.HeaderText);
            }

            // Lấy dữ liệu từ DataGridView
            foreach (DataGridViewRow row in dgvBenhNhan.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataRow dRow = dt.NewRow();
                    for (int i = 0; i < dgvBenhNhan.Columns.Count; i++)
                    {
                        dRow[i] = row.Cells[i].Value;
                    }
                    dt.Rows.Add(dRow);
                }
            }

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất Excel!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ExportExcel(dt, "Danh sách bệnh nhân đã tìm kiếm");
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
                ReadExcel_BenhNhan(openFileDialog.FileName);
                load_BenhNhan();
            }
        }
    }
}

