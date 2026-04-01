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
using System.Xml.Serialization;

namespace WindowsFormsApp1
{
    public partial class frmMonHoc : Form
    {
        SqlConnection conn = new SqlConnection(
        "Data Source=.;Initial Catalog=QLDSV_HTC;Integrated Security=True");
        int mode = 0;
        public frmMonHoc()
        {
            InitializeComponent();
        }

        void LoadMonHoc()
        {
            string sql = "select *from MONHOC";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvMonHoc.DataSource = dt;
        }
        private void frmMonHoc_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            LoadMonHoc();
        }

        private void dgvMonHoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtMaMH.Text = "";
            txtTenMH.Text = "";
            txtTietLT.Text = "";
            txtTietTH.Text = "";

            txtMaMH.Focus();
            mode = 1;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from MONHOC where MAMH = @mamh", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Đã xóa thành công");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            conn.Open();
            if(mode == 1)
            {
                SqlCommand cmd = new SqlCommand("insert into MONHOC(MAMH , TENMH , SOTIET_LT, SOTIET_TH) values(@mamh , @tenmh, @sotietlt, @sotietth)", conn);
                cmd.Parameters.AddWithValue("@mamh", txtMaMH.Text);
                cmd.Parameters.AddWithValue("@tenmh", txtTenMH.Text);
                cmd.Parameters.AddWithValue("@sotietlt", txtTietLT.Text);
                cmd.Parameters.AddWithValue("@sotietth", txtTietTH.Text);
                cmd.ExecuteNonQuery();
                LoadMonHoc();
                MessageBox.Show("Đã thêm thành công");

            }
            else if(mode == 2)
            {
                SqlCommand cmd = new SqlCommand("update MONHOC set TENMH = @tenmh , SOTIET_LT = @sotietlt, SOTIET_TH = @sotietth where MAMH =@mamh", conn);
                cmd.Parameters.AddWithValue("@mamh", txtMaMH.Text);
                cmd.Parameters.AddWithValue("@tenmh", txtTenMH.Text);
                cmd.Parameters.AddWithValue("@sotietlt", txtTietLT.Text);
                cmd.Parameters.AddWithValue("@sotietth", txtTietTH.Text);
                cmd.ExecuteNonQuery();
                LoadMonHoc();
                MessageBox.Show("Cập nhật thành công");
            }
            conn.Close();
            mode = 0;
        }

        private void btnCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvMonHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;

            if (i >= 0)
            {
                txtMaMH.Text = dgvMonHoc.Rows[i].Cells["MAMH"].Value.ToString();
                txtTenMH.Text = dgvMonHoc.Rows[i].Cells["TENMH"].Value.ToString();
                txtTietLT.Text = dgvMonHoc.Rows[i].Cells["SOTIET_LT"].Value.ToString();
                txtTietTH.Text = dgvMonHoc.Rows[i].Cells["SOTIET_TH"].Value.ToString();
                mode = 2;

            }
        }
    }
}
