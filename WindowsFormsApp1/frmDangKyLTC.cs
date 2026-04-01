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

namespace WindowsFormsApp1
{
    public partial class frmDangKyLTC : Form
    {
        
        public frmDangKyLTC()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=QLDSV_HTC;Integrated Security=True");
        void UpdateDangKyBtn()
        {
            btnDangKy.Enabled = dgvLTC.CurrentRow != null
                                && !dgvLTC.CurrentRow.IsNewRow;
        }

        private void dgvLTC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateDangKyBtn();
        }

        private void frmDangKyLTC_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            txtHoTen.Enabled = false;
            txtMaLop.Enabled = false;
        }

        private void btnTimSV_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select HO+ ' ' +TEN as HOTEN, MALOP from SINHVIEN where MASV = @masv",conn);
            cmd.Parameters.AddWithValue("@masv", txtMaSV.Text);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()){
                txtHoTen.Text = reader["HOTEN"].ToString();
                txtMaLop.Text = reader["MALOP"].ToString();
            }
            else
            {
                MessageBox.Show("Không tồn tại");
            }
            conn.Close();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
           
            SqlCommand cmd = new SqlCommand("sp_LTC_Filter", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NIENKHOA", txtNienKhoa.Text);
            cmd.Parameters.AddWithValue("@HOCKY", int.Parse(txtHocKy.Text));

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvLTC.DataSource = dt;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (dgvLTC.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn lớp tín chỉ!");
                return;
            }

            int i = dgvLTC.CurrentRow.Index;
            string maltc = dgvLTC.Rows[i].Cells["MALTC"].Value.ToString();

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng kí lớp tín chỉ có mã: " + maltc + " không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("sp_DangKy", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MASV", txtMaSV.Text);
                    cmd.Parameters.AddWithValue("@MALTC", maltc);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Đăng ký thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

        }
    }
}
