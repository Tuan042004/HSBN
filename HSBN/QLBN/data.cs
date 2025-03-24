using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSBN.QLBN
{
    internal class data
    {
        public static SqlConnection con = new SqlConnection("Data Source=YUNO\\SQLEXPRESS;Initial Catalog=QuanLyBenhVien;Integrated Security=True");
        public static void insert(string sql)
        {
            // ket noi database
            if (con.State == ConnectionState.Closed)
                con.Open();
            //Tao doi tuowng command thuc hien cau lenh chen
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public static void update(string sql)
        {
            // Kết nối database
            if (con.State == ConnectionState.Closed)
                con.Open();

            // Tạo đối tượng command để thực thi lệnh cập nhật
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            // Giải phóng tài nguyên
            cmd.Dispose();
            con.Close();
        }
        public static void delete(string sql)
        {
            // Kết nối database
            if (con.State == ConnectionState.Closed)
                con.Open();

            // Tạo đối tượng command để thực thi lệnh xóa
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            // Giải phóng tài nguyên
            cmd.Dispose();
            con.Close();
        }
        public static void hienThi(DataGridView dvgBenhNhan, string sql)
        {
            // ket noi database
            if (con.State == ConnectionState.Closed)
                con.Open();
            //Tao doi tuowng command thuc hien cau lenh chen
            SqlCommand cmd = new SqlCommand(sql, con);
            //tao doi tuong data adapter 
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //tao doi tuong data Table de lay du lieu tu da
            DataTable tb = new DataTable();
            da.Fill(tb);
            //hienthi tb len dgv
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            dvgBenhNhan.DataSource = tb;
            dvgBenhNhan.Refresh();

        }
        public static void hienThiComboBox(ComboBox cbx, string sql, string displayMember, string valueMember)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataTable tb = new DataTable();
            da.Fill(tb);
            // Thêm dòng "--Chọn lớp học--"
            DataRow r = tb.NewRow();
            r[valueMember] = "";
            r[displayMember] = "--Chọn mã --";
            tb.Rows.InsertAt(r, 0);
            cbx.DataSource = tb;
            cbx.DisplayMember = displayMember;
            cbx.ValueMember = valueMember;
            cbx.Refresh();
        }
    }
}

