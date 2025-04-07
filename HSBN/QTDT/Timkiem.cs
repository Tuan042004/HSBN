using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xls = Microsoft.Office.Interop.Excel;

namespace HSBN.QTDT
{
    public partial class Timkiem : Form
    {
        public Timkiem()
        {
            InitializeComponent();
        }

        private void xoa()
        {
            cboBenhnhantk.SelectedIndex = 0;
            cboBacsitk.SelectedIndex = 0;
            cboKhoatk.SelectedIndex = 0;
            txtChandoantk.Text = "";

            
        }

        private void load_cbo()
        {
            string sql = "Select * From Khoa";
            string sql2 = "Select * From BenhNhan";
            string sql3 = "Select * From NhanVienYTe";
            thuvien.cbo(cboKhoatk, sql, "TenKhoa", "MaKhoa");
            thuvien.cbo(cboBenhnhantk, sql2, "HoTenBenhNhan", "MaBenhNhan");
            thuvien.cbo(cboBacsitk, sql3, "BacSiDieuTri", "MaNhanVien");
        }

        private void load_qtdt()
        {
            string sql = "SELECT QuaTrinhDieuTri.*, NhanVienYTe.BacSiDieuTri " +
                "FROM QuaTrinhDieuTri " +
                "JOIN NhanVienYTe ON QuaTrinhDieuTri.MaNhanVien = NhanVienYTe.MaNhanVien ";

            thuvien.load(dataGridView1, sql);
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            String bn;
            if (cboBenhnhantk.SelectedItem == null)
            {
                bn = "";
            }
            else
            {
                bn = cboBenhnhantk.SelectedValue.ToString();
            }

            String bs;
            if (cboBacsitk.SelectedItem == null)
            {
                bs = "";
            }
            else
            {
                bs = cboBacsitk.SelectedValue.ToString();
            }

            String cd = txtChandoantk.Text.Trim();

            String khoa;
            if (cboKhoatk.SelectedItem == null)
            {
                khoa = "";
            }
            else
            {
                khoa = cboKhoatk.SelectedValue.ToString();
            }

            string sql = "SELECT QuaTrinhDieuTri.*, NhanVienYTe.BacSiDieuTri " +
                "FROM QuaTrinhDieuTri " +
                "JOIN NhanVienYTe ON QuaTrinhDieuTri.MaNhanVien = NhanVienYTe.MaNhanVien " +
                "JOIN BenhNhan ON QuaTrinhDieuTri.MaBenhNhan = BenhNhan.MaBenhNhan " +
                "JOIN Khoa ON QuaTrinhDieuTri.TenKhoa = Khoa.TenKhoa " + 
                "WHERE BenhNhan.MaBenhNhan LIKE N'%" + bn + "%' " +
                "AND NhanVienYTe.MaNhanVien LIKE N'%" + bs + "%' " +
                "AND QuaTrinhDieuTri.ChanDoanDieuTri LIKE N'%" + cd + "%' " +
                "AND Khoa.MaKhoa LIKE N'%" + khoa + "%'";

            thuvien.load(dataGridView1, sql);
            xoa();
            load_cbo();
        }

        private void Timkiem_Load(object sender, EventArgs e)
        {
            load_qtdt();
            load_cbo();
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
            xls.Range head = oSheet.get_Range("A1", "L1");
            head.MergeCells = true;
            head.Value2 = "DANH SÁCH QTDT";
            head.Font.Bold = true;
            head.Font.Name = "Tahoma";
            head.Font.Size = "18";
            head.HorizontalAlignment = xls.XlHAlign.xlHAlignCenter;
            // Tạo tiêu đề cột 
            xls.Range cl1 = oSheet.get_Range("A3", "A3");
            cl1.Value2 = "MÃ ĐIỀU TRỊ";
            cl1.ColumnWidth = 15;

            xls.Range cl2 = oSheet.get_Range("B3", "B3");
            cl2.Value2 = "MÃ BỆNH NHÂN";
            cl2.ColumnWidth = 25.0;

            xls.Range cl3 = oSheet.get_Range("C3", "C3");
            cl3.Value2 = "TÊN BỆNH NHÂN";
            cl3.ColumnWidth = 40.0;

            xls.Range cl4 = oSheet.get_Range("D3", "D3");
            cl4.Value2 = "MÃ NHÂN VIÊN";
            cl4.ColumnWidth = 15.0;

            xls.Range cl5 = oSheet.get_Range("E3", "E3");
            cl5.Value2 = "NGÀY ĐIỀU TRỊ";
            cl5.ColumnWidth = 25.0;

            xls.Range cl6 = oSheet.get_Range("F3", "F3");
            cl6.Value2 = "CHẨN ĐOÁN";
            cl6.ColumnWidth = 15.0;

            xls.Range cl7 = oSheet.get_Range("G3", "G3");
            cl7.Value2 = "PHƯƠNG PHÁP";
            cl7.ColumnWidth = 15.0;

            xls.Range cl8 = oSheet.get_Range("H3", "H3");
            cl8.Value2 = "SỐ NGÀY NHẬP VIỆN";
            cl8.ColumnWidth = 15.0;

            xls.Range cl9 = oSheet.get_Range("I3", "I3");
            cl9.Value2 = "MÃ KHOA";
            cl9.ColumnWidth = 25.0;

            xls.Range cl10 = oSheet.get_Range("J3", "J3");
            cl10.Value2 = "TÊN KHOA";
            cl10.ColumnWidth = 15.0;

            xls.Range cl11 = oSheet.get_Range("K3", "K3");
            cl11.Value2 = "BÁC SĨ";
            cl11.ColumnWidth = 15.0;


            //xls.Range cl6_1 = oSheet.get_Range("F4", "F1000");
            //cl6_1.Columns.NumberFormat = "dd/mm/yyyy";



            xls.Range rowHead = oSheet.get_Range("A3", "K3");
            rowHead.Font.Bold = true;
            // Kẻ viền
            rowHead.Borders.LineStyle = xls.Constants.xlSolid;
            // Thiết lập màu nền
            rowHead.Interior.ColorIndex = 15;
            rowHead.HorizontalAlignment = xls.XlHAlign.xlHAlignCenter;
            // Tạo mảng đối tượng để lưu dữ toàn bồ dữ liệu trong DataTable,
            // vì dữ liệu được được gán vào các Cell trong Excel phải thông qua object thuần.
            object[,] arr = new object[tb.Rows.Count, tb.Columns.Count];
            //Chuyển dữ liệu từ DataTable vào mảng đối tượng
            for (int r = 0; r < tb.Rows.Count; r++)
            {
                DataRow dr = tb.Rows[r];
                for (int c = 0; c < tb.Columns.Count; c++)

                {
                    if (c == 4)
                        arr[r, c] = "'" + dr[c];
                    else
                        arr[r, c] = dr[c];
                }
            }
            //Thiết lập vùng điền dữ liệu
            int rowStart = 4;
            int columnStart = 1;
            int rowEnd = rowStart + tb.Rows.Count - 1;
            int columnEnd = tb.Columns.Count;
            // Ô bắt đầu điền dữ liệu
            xls.Range c1 = (xls.Range)oSheet.Cells[rowStart, columnStart];
            // Ô kết thúc điền dữ liệu
            xls.Range c2 = (xls.Range)oSheet.Cells[rowEnd, columnEnd];
            // Lấy về vùng điền dữ liệu
            xls.Range range = oSheet.get_Range(c1, c2);
            //Điền dữ liệu vào vùng đã thiết lập
            range.Value2 = arr;
            // Kẻ viền
            range.Borders.LineStyle = xls.Constants.xlSolid;
            // Căn giữa cột STT
            xls.Range c3 = (xls.Range)oSheet.Cells[rowEnd, columnStart];
            xls.Range c4 = oSheet.get_Range(c1, c3);
            oSheet.get_Range(c3, c4).HorizontalAlignment = xls.XlHAlign.xlHAlignCenter;
            xls.Range dateColumn = oSheet.Range[oSheet.Cells[rowStart, 4], oSheet.Cells[rowEnd, 4]];
            dateColumn.NumberFormat = "dd/mm/yyyy";
            //định dạng ngày
            //xls.Range cl6_1 = oSheet.get_Range("D" + (rowStart), "D"+(rowEnd));
            //cl6_1.Columns.NumberFormat = "dd/mm/yyyy";
        }

        private void btnXuatexcel_Click(object sender, EventArgs e)
        {
            //String bn;
            //if (cboBenhnhantk.SelectedItem == null)
            //{
            //    bn = "";
            //}
            //else
            //{
            //    bn = cboBenhnhantk.SelectedValue.ToString();
            //}

            //String bs;
            //if (cboBacsitk.SelectedItem == null)
            //{
            //    bs = "";
            //}
            //else
            //{
            //    bs = cboBacsitk.SelectedValue.ToString();
            //}

            //String cd = txtChandoantk.Text.Trim();

            //String khoa;
            //if (cboKhoatk.SelectedItem == null)
            //{
            //    khoa = "";
            //}
            //else
            //{
            //    khoa = cboKhoatk.SelectedValue.ToString();
            //}

            //string sql = "SELECT QuaTrinhDieuTri.*, BenhNhan.HotenBenhNhan, " +
            //         "NhanVienYTe.BacSiDieuTri, Khoa.TenKhoa, Thuoc.TenThuoc " +
            //         "FROM QuaTrinhDieuTri " +
            //         "JOIN BenhNhan ON QuaTrinhDieuTri.MaBenhNhan = BenhNhan.MaBenhNhan " +
            //         "JOIN NhanVienYTe ON QuaTrinhDieuTri.MaNhanVien = NhanVienYTe.MaNhanVien " +
            //         "JOIN Khoa ON QuaTrinhDieuTri.MaKhoa = Khoa.MaKhoa " +
            //         "LEFT JOIN Thuoc ON QuaTrinhDieuTri.MaThuoc = Thuoc.MaThuoc " +
            //         "WHERE BenhNhan.MaBenhNhan LIKE N'%" + bn + "%' " +
            //         "AND NhanVienYTe.MaNhanVien LIKE N'%" + bs + "%' " +
            //         "AND QuaTrinhDieuTri.ChanDoanDieuTri LIKE N'%" + cd + "%' " +
            //         "AND Khoa.MaKhoa LIKE N'%" + khoa + "%'";

            //xoa();
            //load_cbo();
            DataTable tb = new DataTable();
            //thuvien.excel(tb, sql);
            // Tạo các cột từ DataGridView
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                tb.Columns.Add(column.HeaderText);
            }

            // Lấy dữ liệu từ DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataRow dRow = tb.NewRow();
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dRow[i] = row.Cells[i].Value;
                    }
                    tb.Rows.Add(dRow);
                }
            }

            if (tb.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất Excel!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ExportExcel(tb, "DS QTDT");
        }
    }
}
