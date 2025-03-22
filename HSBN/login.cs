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

namespace HSBN
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=TUANDEPZAI; Initial Catalog=QuanLyBenhNhan; Integrated Security=True"))
            {
                try
                {
                    con.Open();
                    string tk = txttk.Text;
                    string mk = txtmk.Text;

                    string sql = "SELECT * FROM Users WHERE Username = @username AND Password = @password";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@username", tk);
                    cmd.Parameters.AddWithValue("@password", mk);

                    SqlDataReader da = cmd.ExecuteReader();
                    if (da.HasRows) // Nếu có tài khoản hợp lệ
                    {
                        MessageBox.Show("ĐĂNG NHẬP THÀNH CÔNG!");

                        // Ẩn form Login và mở Menu
                        this.Hide();
                        Menu formMain = new Menu();
                        formMain.ShowDialog();

                        // Sau khi đóng Form1, thoát ứng dụng
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("ĐĂNG NHẬP THẤT BẠI!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("LỖI KẾT NỐI: " + ex.Message);
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
    
}
