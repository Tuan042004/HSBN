using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSBN.QTDT
{
    internal class thuvien
    {
        public static SqlConnection con = new SqlConnection("Data Source=CONMEOHUDON\\SQLEXPRESS02;Initial Catalog=QuanLyBenhVien;Integrated Security=True");
        public static void insert(string sql)
        {
            if (thuvien.con.State == ConnectionState.Closed)
            {
                thuvien.con.Open();
            }

            SqlCommand cmd = new SqlCommand(sql, thuvien.con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            thuvien.con.Close();
        }
        public static void load(DataGridView dgv, string sql)
        {
            if (thuvien.con.State == ConnectionState.Closed)
            {
                thuvien.con.Open();
            }
            SqlCommand cmd = new SqlCommand(sql, thuvien.con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.SelectCommand = cmd;
            DataTable tb = new DataTable();
            adapter.Fill(tb);
            cmd.Dispose();
            thuvien.con.Close();
            dgv.DataSource = tb;
            dgv.Refresh();
        }

        public static void excel(DataTable tb, string sql)
        {
            if (thuvien.con.State == ConnectionState.Closed)
            {
                thuvien.con.Open();
            }
            SqlCommand cmd = new SqlCommand(sql, thuvien.con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.SelectCommand = cmd;
            adapter.Fill(tb);
            cmd.Dispose();
            thuvien.con.Close();
        }

        public static void cbo(ComboBox cbo, string sql, string cothienthi, string cotgiatri)
        {
            if (thuvien.con.State == ConnectionState.Closed)
            {
                thuvien.con.Open();
            }
            SqlCommand cmd = new SqlCommand(sql, thuvien.con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.SelectCommand = cmd;
            DataTable tb = new DataTable();
            adapter.Fill(tb);
            cmd.Dispose();
            thuvien.con.Close();
            // bo sung them 1 dong vao vị trí đầu tiên của bảng tb
            DataRow r = tb.NewRow();
            r[cotgiatri] = "";
            r[cothienthi] = "----------";
            tb.Rows.InsertAt(r, 0);

            cbo.DataSource = tb;
            cbo.DisplayMember = cothienthi; //  hien thi ten tren man hinh 
            cbo.ValueMember = cotgiatri;
        }
    }
}
