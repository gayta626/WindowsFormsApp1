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

namespace WindowsFormsApp1
{
    public partial class frmNhapDiem : Form
    {
        public frmNhapDiem()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=QLDSV_HTC;Integrated Security=True");
        int maltc;
        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        void loadMonHoc()
        {
            SqlDataAdapter da = new SqlDataAdapter("select MAMH from MONHOC", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cbMaMH.DataSource = dt;
            cbMaMH.DisplayMember = "MAMH";
            

        }
        private void btnStart_Click(object sender, EventArgs e)
        { 
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetSinhVienLTC", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NIENKHOA", txtNienKhoa.Text);
                cmd.Parameters.AddWithValue("@HOCKY", txtHocKy.Text);
                cmd.Parameters.AddWithValue("@MAMH", cbMaMH.Text);
                cmd.Parameters.AddWithValue("@NHOM", txtNhom.Text);
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvDiem.DataSource = dt;
                dgvDiem.Columns["MALTC"].Visible = false;
                if (dt.Rows.Count > 0)
{
    maltc = Convert.ToInt32(dt.Rows[0]["MALTC"]);
}
else
{
    MessageBox.Show("Không có dữ liệu!");
    return;
}
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi " + ex);
            }
            finally
            {
                conn.Close();
            }
        }
        private void Tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cho phép số, dấu chấm, backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

            // Không cho nhập nhiều dấu chấm
            TextBox tb = sender as TextBox;
            if (e.KeyChar == '.' && tb.Text.Contains("."))
            {
                e.Handled = true;
            }
        }
        private void dgvDiem_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvDiem.CurrentCell.ColumnIndex == dgvDiem.Columns["DIEM_CC"].Index ||
                dgvDiem.CurrentCell.ColumnIndex == dgvDiem.Columns["DIEM_GK"].Index ||
                dgvDiem.CurrentCell.ColumnIndex == dgvDiem.Columns["DIEM_CK"].Index)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress -= Tb_KeyPress;
                    tb.KeyPress += Tb_KeyPress;
                }
            }
        }

        private void dgvDiem_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;

            try
            {
                float cc = 0, gk = 0, ck = 0;

                float.TryParse(dgvDiem.Rows[i].Cells["DIEM_CC"].Value?.ToString(), out cc);
                float.TryParse(dgvDiem.Rows[i].Cells["DIEM_GK"].Value?.ToString(), out gk);
                float.TryParse(dgvDiem.Rows[i].Cells["DIEM_CK"].Value?.ToString(), out ck);

                // Check hợp lệ
                if (cc < 0 || cc > 10 || gk < 0 || gk > 10 || ck < 0 || ck > 10)
                {
                    MessageBox.Show("Điểm phải từ 0 đến 10");
                    dgvDiem.Rows[i].Cells[e.ColumnIndex].Value = 0;
                    return;
                }

                // Tính điểm tổng
                float tong = cc * 0.1f + gk * 0.3f + ck * 0.6f;
                dgvDiem.Rows[i].Cells["DIEM_HET_MON"].Value = tong;
            }
            catch
            {
                MessageBox.Show("Dữ liệu không hợp lệ");
                dgvDiem.Rows[i].Cells[e.ColumnIndex].Value = 0;
            }

        }

        private void btnGhiDiem_Click(object sender, EventArgs e)
        {
            conn.Open();

            foreach (DataGridViewRow row in dgvDiem.Rows)
            {
                if (row.IsNewRow) continue;

                SqlCommand cmd = new SqlCommand("sp_UpdateDiem", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MASV", row.Cells["MASV"].Value);
                cmd.Parameters.AddWithValue("@MALTC", maltc); // phải lưu sẵn
                cmd.Parameters.AddWithValue("@DIEM_CC", row.Cells["DIEM_CC"].Value);
                cmd.Parameters.AddWithValue("@DIEM_GK", row.Cells["DIEM_GK"].Value);
                cmd.Parameters.AddWithValue("@DIEM_CK", row.Cells["DIEM_CK"].Value);

                cmd.ExecuteNonQuery();
            }

            conn.Close();

            MessageBox.Show("Đã ghi điểm");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmNhapDiem_Load(object sender, EventArgs e)
        {
            loadMonHoc();
            this.Dock = DockStyle.Fill;
        }
    }
}
