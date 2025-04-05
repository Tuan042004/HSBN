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
using HSBN.QLBN;
using xls = Microsoft.Office.Interop.Excel;

namespace HSBN.QTDT
{
    public partial class Thuoc : Form
    {
        public Thuoc()
        {
            InitializeComponent();
        }

        private void xoa()
        {
            txtMathuoc.Text = "";
            txtTenthuoc.Text = "";
            txtDonvi.Text = "";
            txtDongia.Text = "";
            txtNsx.Text = "";
            txtMathuoctk.Text = "";
            txtTenthuoctk.Text = "";
            txtDongiatk.Text = "";
            txtNsxtk.Text = "";
            txtMathuoc.Enabled = true;
        }
        private void load_thuoc()
        {
            string sql = "SELECT * FROM Thuoc";
            thuvien.load(dataGridView1, sql);
        }
        public bool checktrungma(string ma)
        {
            if (thuvien.con.State == ConnectionState.Closed)
            {
                thuvien.con.Open();
            }
            string sql = "Select count(*) from Thuoc Where MaThuoc='" + ma + "'";
            SqlCommand cmd = new SqlCommand(sql, thuvien.con);
            int kq = (int)cmd.ExecuteScalar();
            if (kq > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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
            xls.Range head = oSheet.get_Range("A1", "G1");
            head.MergeCells = true;
            head.Value2 = "DANH SÁCH THUỐC";
            head.Font.Bold = true;
            head.Font.Name = "Tahoma";
            head.Font.Size = "18";
            head.HorizontalAlignment = xls.XlHAlign.xlHAlignCenter;
            // Tạo tiêu đề cột 
            xls.Range cl1 = oSheet.get_Range("A3", "A3");
            cl1.Value2 = "MÃ THUỐC";
            cl1.ColumnWidth = 15;

            xls.Range cl2 = oSheet.get_Range("B3", "B3");
            cl2.Value2 = "TÊN THUỐC";
            cl2.ColumnWidth = 25.0;

            xls.Range cl3 = oSheet.get_Range("C3", "C3");
            cl3.Value2 = "ĐƠN VỊ";
            cl3.ColumnWidth = 40.0;

            xls.Range cl4 = oSheet.get_Range("D3", "D3");
            cl4.Value2 = "ĐƠN GIÁ";
            cl4.ColumnWidth = 15.0;

            xls.Range cl5 = oSheet.get_Range("E3", "E3");
            cl5.Value2 = "SỐ LƯỢNG";
            cl5.ColumnWidth = 25.0;

            xls.Range cl6 = oSheet.get_Range("F3", "F3");
            cl6.Value2 = "HẠN SỬ DỤNG";
            cl6.ColumnWidth = 15.0;

            xls.Range cl7 = oSheet.get_Range("G3", "G3");
            cl7.Value2 = "NHÀ SẢN XUẤT";
            cl7.ColumnWidth = 15.0;

            //xls.Range cl6_1 = oSheet.get_Range("F4", "F1000");
            //cl6_1.Columns.NumberFormat = "dd/mm/yyyy";



            xls.Range rowHead = oSheet.get_Range("A3", "G3");
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
            //định dạng ngày
            //xls.Range cl6_1 = oSheet.get_Range("D" + (rowStart), "D"+(rowEnd));
            //cl6_1.Columns.NumberFormat = "dd/mm/yyyy";
        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            String ma = txtMathuoc.Text.Trim();
            String ten = txtTenthuoc.Text.Trim();
            String dv = txtDonvi.Text.Trim();
            String dg = txtDongia.Text.Trim();
            String sl = txtSoluong.Text.Trim();
            DateTime ngay = dateHsd.Value;
            String nsx = txtNsx.Text.Trim();

            if (checktrungma(ma))
            {
                MessageBox.Show("Trùng mã!");
                txtMathuoc.Focus();
                return;
            }
            //if (ma == "")
            //{
            //    MessageBox.Show("Chưa nhập mã!");
            //    txtMathuoc.Focus();
            //    return;
            //}

            if (string.IsNullOrWhiteSpace(ma) ||
               string.IsNullOrWhiteSpace(ten) ||
                string.IsNullOrWhiteSpace(dv) ||
                string.IsNullOrWhiteSpace(dg) ||
                string.IsNullOrWhiteSpace(sl) ||
                string.IsNullOrWhiteSpace(nsx))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            string sql = "Insert Thuoc Values('" + ma + "',N'" + ten + "',N'" + dv + "','" + dg + "','" + sl + "','" + ngay + "',N'" + nsx + "')";
            thuvien.insert(sql);
            MessageBox.Show("Thêm mới thuốc thành công!");
            load_thuoc();
            xoa();
        }

        private void Thuoc_Load(object sender, EventArgs e)
        {
            load_thuoc();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            String ma = txtMathuoc.Text.Trim();
            String ten = txtTenthuoc.Text.Trim();
            String dv = txtDonvi.Text.Trim();
            String dg = txtDongia.Text.Trim();
            String sl = txtSoluong.Text.Trim();
            DateTime ngay = dateHsd.Value;
            String nsx = txtNsx.Text.Trim();

            if (string.IsNullOrWhiteSpace(ma) ||
               string.IsNullOrWhiteSpace(ten) ||
                string.IsNullOrWhiteSpace(dv) ||
                string.IsNullOrWhiteSpace(dg) ||
                string.IsNullOrWhiteSpace(sl) ||
                string.IsNullOrWhiteSpace(nsx))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            string sql = "UPDATE Thuoc SET TenThuoc = N'" + ten +
             "', DonVi = N'" + dv +
             "', DonGia = '" + dg +
             "', SoLuong = '" + sl +
             "', HanSuDung = '" + ngay +
             "', NhaSanXuat = N'" + nsx +
             "' WHERE MaThuoc = '" + ma + "'";

            thuvien.insert(sql);
            MessageBox.Show("Cập nhật thuốc thành công!");
            load_thuoc();
            xoa();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String ma = txtMathuoc.Text.Trim();
            string sql = "DELETE FROM Thuoc WHERE MaThuoc = '" + ma + "'";
            thuvien.insert(sql);
            MessageBox.Show("Xoá thuốc thành công!");
            load_thuoc();
            xoa();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtMathuoc.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txtTenthuoc.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtDonvi.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtDongia.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            txtSoluong.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            dateHsd.Value = DateTime.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
            txtNsx.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
            txtMathuoc.Enabled = false;
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            String ma = txtMathuoctk.Text.Trim();
            String ten = txtTenthuoctk.Text.Trim();
            String dg = txtDongiatk.Text.Trim();
            String nsx = txtNsxtk.Text.Trim();

            string sql = "Select * From Thuoc Where MaThuoc like '%" + ma + "%' and" +
                " TenThuoc like N'%" + ten + "%' and" +
                " DonGia like '%" + dg + "%' and" +
                " NhaSanXuat like N'%" + nsx + "%'";

            thuvien.load(dataGridView1, sql);
            xoa();
        }

        private void btbXuatexcel_Click(object sender, EventArgs e)
        {
            String ma = txtMathuoctk.Text.Trim();
            String ten = txtTenthuoctk.Text.Trim();
            String dg = txtDongiatk.Text.Trim();
            String nsx = txtNsxtk.Text.Trim();

            string sql = "Select * From Thuoc Where MaThuoc like '%" + ma + "%' and" +
                " TenThuoc like N'%" + ten + "%' and" +
                " DonGia like '%" + dg + "%' and" +
                " NhaSanXuat like N'%" + nsx + "%'";
            DataTable tb = new DataTable();
            thuvien.excel(tb, sql);
            ExportExcel(tb, "DSTHUOC");
        }

        private void ReadExcel(string filename)
        {
            //kiểm tra xem filename đã có dữ liệu chưa
            if (filename == null)
            {
                MessageBox.Show("Chưa chọn file");
            }
            else
            {
                xls.Application Excel = new xls.Application();// tạp một app làm việc mới
                                                              // mở dữ liệu từ file
                Excel.Workbooks.Open(filename);
                //đọc dữ liệu từng sheet của excel
                foreach (xls.Worksheet wsheet in Excel.Worksheets)
                {
                    int i = 2;  //để đọc từng dòng của sheet bắt đầu từ dòng số 2
                    do
                    {
                        if (wsheet.Cells[i, 1].Value == null && wsheet.Cells[i, 2].Value == null && wsheet.Cells[i, 3].Value == null)
                        {
                            break;
                        }
                        else
                        {
                            //Đổ dòng dữ liệu vào DB
                            System.Console.WriteLine(wsheet.Cells[i, 1].Value);
                            thuochaha(
                                Convert.ToString(wsheet.Cells[i, 1].Value),
                                Convert.ToString(wsheet.Cells[i, 2].Value),
                                Convert.ToString(wsheet.Cells[i, 3].Value),
                                Convert.ToString(wsheet.Cells[i, 4].Value),
                                Convert.ToString(wsheet.Cells[i, 5].Value),
                                Convert.ToString(wsheet.Cells[i, 6].Value),
                                Convert.ToString(wsheet.Cells[i, 7].Value)
                                );
                            i++;
                        }
                    }
                    while (true);
                }
            }
        }

        private void thuochaha(string ma, string ten, string dv, string dg, string sl, string ngay, string nsx)
        {
            string sql = "Insert Thuoc Values('" + ma + "',N'" + ten + "',N'" + dv + "','" + dg + "','" + sl + "','" + ngay + "',N'" + nsx + "')";
            thuvien.insert(sql);

        }

        private void btnNhapexcel_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "excel file |*.xls;*.xlsx";
            openFileDialog1.FilterIndex = 1;//trỏ vào vị trí đầu tiên của bộ lọc
            openFileDialog1.RestoreDirectory = true;//nhớ đường dẫn của lần truy cập
            openFileDialog1.Multiselect = false;//ko cho phép chọn nhiều file 1 lần

            DialogResult kq = openFileDialog1.ShowDialog();
            if (kq == DialogResult.OK)
            {
                //textBox1.Text = openFileDialog1.FileName;
                string tenfile = openFileDialog1.FileName;
                ReadExcel(tenfile);
                MessageBox.Show("Upload ok!");
                load_thuoc();
            }
        }

        private void nhapSo(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho nhập ký tự
            }
        }

        private void txtDongia_TextChanged(object sender, EventArgs e)
        {
            //txtDongia.KeyPress += new KeyPressEventHandler(nhapSo);
            //MessageBox.Show("Nhập số");
        }

        private void txtDongiatk_TextChanged(object sender, EventArgs e)
        {
            //txtDongiatk.KeyPress += new KeyPressEventHandler(nhapSo);
            //MessageBox.Show("Nhập số");
        }

        private void txtSoluong_TextChanged(object sender, EventArgs e)
        {
            //txtSoluong.KeyPress += new KeyPressEventHandler(nhapSo);
            //MessageBox.Show("Nhập số");
        }
    }
}
