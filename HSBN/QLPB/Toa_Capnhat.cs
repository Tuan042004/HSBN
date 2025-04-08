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

namespace QLBN
{
    public partial class Toa_Capnhat : Form
    {
        // Constructor nhận tham số
        //public Toa_CapNhat(string mt, string st)
        //{
        //    this.mt = mt;
        //    // Hiển thị dữ liệu lên các ô nhập liệu
        //    txtmt.Text = mt;
        //    txtt.Text = st;
        //}
        public Toa_Capnhat(string mt, string st, string tp, string pt)
        {
            InitializeComponent();
            // Hiển thị dữ liệu lên các ô nhập liệu
            txtmt.Text = mt;
            txtt.Text = st;
            txtTp.Text = tp;
            txtPt.Text = pt;
            
        }
        SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=QLBN_NHUNG;Integrated Security=True");

        // Khai báo delegate và sự kiện
        public delegate void DataUpdatedHandler(string newMaToa, string newSoTang, string newTongPhong, string newPhongTang);
        public event DataUpdatedHandler OnDataUpdated;

        public Toa_Capnhat()
        {
            InitializeComponent();
        }

        private void Toa_Capnhat_Load(object sender, EventArgs e)
        {
            txtmt.ReadOnly = true;
            txtmt.Enabled = false;
            txtmt.BackColor = Color.White;
        }

        private void btnCn_Click(object sender, EventArgs e)
        {
            string maToa = txtmt.Text.Trim();
            string soTang = txtt.Text.Trim();
            string tp = txtTp.Text.Trim();
            string pt = txtPt.Text.Trim();

            if (con.State == ConnectionState.Closed)
                con.Open();

            string sql = "UPDATE ToaNha SET SoTang=@soTang, SoPhongMax=@SoPhongMax, SoPhongMaxMoiTang=@SoPhongMaxMoiTang WHERE MaToa=@maToa";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@soTang", SqlDbType.NVarChar, 50).Value = soTang;
            cmd.Parameters.Add("@maToa", SqlDbType.NVarChar, 50).Value = maToa;
            cmd.Parameters.Add("@SoPhongMax", SqlDbType.NVarChar, 50).Value = tp;
            cmd.Parameters.Add("@SoPhongMaxMoiTang", SqlDbType.NVarChar, 50).Value = pt;

            cmd.ExecuteNonQuery();
            MessageBox.Show("Cập nhật thành công!", "Thông báo");

            // Gửi dữ liệu vừa cập nhật về Form 1
            OnDataUpdated?.Invoke(maToa, soTang, tp, pt);

            cmd.Dispose();
            con.Close();
            this.Close();
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

            // Nếu người dùng nhấn Cancel thì dừng lại, không làm gì cả
            if (result == DialogResult.Cancel)
                return;
            Dispose();
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
