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
    public partial class frmMoLTC : Form
    {
        public frmMoLTC()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=QLDSV_HTC;Integrated Security=True");
        int mode = 0;

        void loadData()
        {
            string sql = "select *from LOPTINCHI";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();

            da.Fill(dt);
            dgvLTC.DataSource = dt;
        }
        private void dgvLTC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void frmDangKyLTC_Load(object sender, EventArgs e)
        {
            loadData();
            this.Dock = DockStyle.Fill;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtNienKhoa.Text = "";
            cbHocKy.Text = "";
            txtMaMH.Text = "";
            txtNhom.Text = "";
            txtMaGV.Text = "";
            txtMaKhoa.Text = "";
            txtSoSV.Text = "";
            cbSituation.Text = "";
            txtNienKhoa.Focus();

            mode = 1; // Thêm 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "Bạn có chắc muốn xóa không?",
        "Xác nhận",
        MessageBoxButtons.YesNo
    );

            if (result == DialogResult.No) return;

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_LTC_Delete", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                string mamh = dgvLTC.CurrentRow.Cells["MAMH"].Value.ToString();
                int nhom = int.Parse(dgvLTC.CurrentRow.Cells["NHOM"].Value.ToString());

                cmd.Parameters.AddWithValue("@MAMH", txtMaMH.Text);
                cmd.Parameters.AddWithValue("@NHOM", txtNhom.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Xóa thành công");

                loadData(); // reload lại grid
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(mode == 1)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_LTC_Insert", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@NIENKHOA",txtNienKhoa.Text);
                cmd.Parameters.AddWithValue("@HOCKY", cbHocKy.Text);
                cmd.Parameters.AddWithValue("@MAMH", txtMaMH.Text);
                cmd.Parameters.AddWithValue("@NHOM", txtNhom.Text);
                cmd.Parameters.AddWithValue("@MAGV", txtMaGV.Text);
                cmd.Parameters.AddWithValue("@MAKHOA", txtMaKhoa.Text);
                cmd.Parameters.AddWithValue("@SOSVTOITHIEU", txtSoSV.Text);
                cmd.Parameters.AddWithValue("@HUYLOP", cbSituation.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm lớp tín chỉ thành công");
                loadData();
                
            }

            else if(mode == 2)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_LTC_Update", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MALTC", txtMaLTC.Text);
                cmd.Parameters.AddWithValue("@NIENKHOA", txtNienKhoa.Text);
                cmd.Parameters.AddWithValue("@HOCKY", cbHocKy.Text);
                cmd.Parameters.AddWithValue("@MAMH", txtMaMH.Text);
                cmd.Parameters.AddWithValue("@NHOM", txtNhom.Text);
                cmd.Parameters.AddWithValue("@MAGV", txtMaGV.Text);
                cmd.Parameters.AddWithValue("@MAKHOA", txtMaKhoa.Text);
                cmd.Parameters.AddWithValue("@SOSVTOITHIEU", txtSoSV.Text);
                cmd.Parameters.AddWithValue("@HUYLOP", cbSituation.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công !");
                loadData();
            }

            conn.Close();
            mode = 0;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMaLTC_TextChanged(object sender, EventArgs e)
        {
            txtMaLTC.ReadOnly = true;
        }

        private void dgvLTC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;

            if (i >= 0)
            {

                txtNienKhoa.Text = dgvLTC.Rows[i].Cells["NIENKHOA"].Value.ToString();
                cbHocKy.Text = dgvLTC.Rows[i].Cells["HOCKY"].Value.ToString();
                txtMaMH.Text = dgvLTC.Rows[i].Cells["MAMH"].Value.ToString();
                txtNhom.Text = dgvLTC.Rows[i].Cells["NHOM"].Value.ToString();
                txtMaGV.Text = dgvLTC.Rows[i].Cells["MAGV"].Value.ToString();
                txtMaKhoa.Text = dgvLTC.Rows[i].Cells["MAKHOA"].Value.ToString();
                txtSoSV.Text = dgvLTC.Rows[i].Cells["SOSVTOITHIEU"].Value.ToString();
                cbSituation.Text = dgvLTC.Rows[i].Cells["HUYLOP"].Value.ToString();
                mode = 2; // Sửa

            }
        }
    }
}
