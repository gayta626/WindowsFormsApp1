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
        Stack<string> undoStack = new Stack<string>();
        void UpdateUndoBtn()
        {
            btnUndo.Enabled = undoStack.Count > 0;
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
            DialogResult result = MessageBox.Show(
                "Bạn chắc chắn muốn xóa môn học này",
                "Xác nhận",
                MessageBoxButtons.YesNo
                );
            if(result == DialogResult.No)
            {
                return;
            }
            try
            {
                conn.Open();
                string sql = "insert into MONHOC(MAMH, TENMH, SOTIET_LT, SOTIET_TH) values(" + "N'" + txtMaMH.Text + "', " +
                "N'" + txtTenMH.Text + "', " +
                "N'" + txtTietLT.Text + "', " +
                "N'" + txtTietTH.Text + "') ";
                undoStack.Push(sql);
                UpdateUndoBtn();
                SqlCommand cmd = new SqlCommand("delete from MONHOC where MAMH = @mamh", conn);
                cmd.Parameters.AddWithValue("@mamh", txtMaMH.Text);
                cmd.ExecuteNonQuery();
                LoadMonHoc();
                MessageBox.Show("Đã xóa thành công");
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

        string oldMaMH = "";
        string oldTenMH = "";
        string oldSoTietLT = "";
        string oldSoTietTH = "";
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            conn.Open();
            if(mode == 1)
            {
                string sql = "delete from MONHOC where MAMH = N'" +txtMaMH.Text+"'";
                undoStack.Push(sql);
                UpdateUndoBtn();
                SqlCommand cmd = new SqlCommand("insert into MONHOC(MAMH , TENMH , SOTIET_LT, SOTIET_TH) values(@mamh , @tenmh, @sotietlt, @sotietth)", conn);
                cmd.Parameters.AddWithValue("@mamh", txtMaMH.Text);
                cmd.Parameters.AddWithValue("@tenmh", txtTenMH.Text);
                if (!int.TryParse(txtTietLT.Text, out int lt) ||!int.TryParse(txtTietTH.Text, out int th))
                {
                    MessageBox.Show("Số tiết phải là số hợp lệ");
                    return;
                }
                cmd.Parameters.AddWithValue("@sotietlt", int.Parse(txtTietLT.Text));
                cmd.Parameters.AddWithValue("@sotietth", int.Parse(txtTietTH.Text));
                cmd.ExecuteNonQuery();
                LoadMonHoc();
                MessageBox.Show("Đã thêm thành công");

            }
            else if(mode == 2)
            {
                string sql = "update MONHOC set " +
    "TENMH = N'" + oldTenMH + "', " +
    "SOTIET_LT = " + oldSoTietLT + ", " +
    "SOTIET_TH = " + oldSoTietTH + " " +
    "where MAMH = N'" + oldMaMH + "'";

                undoStack.Push(sql);
                UpdateUndoBtn();
                SqlCommand cmd = new SqlCommand("update MONHOC set TENMH = @tenmh , SOTIET_LT = @sotietlt, SOTIET_TH = @sotietth where MAMH =@mamh", conn);
                cmd.Parameters.AddWithValue("@mamh", txtMaMH.Text);
                cmd.Parameters.AddWithValue("@tenmh", txtTenMH.Text);
                if (!int.TryParse(txtTietLT.Text, out int lt) ||!int.TryParse(txtTietTH.Text, out int th))
                {
                    MessageBox.Show("Số tiết phải là số hợp lệ");
                    return;
                }
                cmd.Parameters.AddWithValue("@sotietlt", int.Parse(txtTietLT.Text));
                cmd.Parameters.AddWithValue("@sotietth",int.Parse(txtTietTH.Text))  ;
                cmd.ExecuteNonQuery();
                LoadMonHoc();
                MessageBox.Show("Cập nhật thành công");
            }
            conn.Close();
            txtMaMH.Text = "";
            txtTenMH.Text = "";
            txtTietLT.Text = "";
            txtTietTH.Text = "";
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
            oldMaMH = txtMaMH.Text;
            oldTenMH = txtTenMH.Text;
            oldSoTietLT = txtTietLT.Text;
            oldSoTietTH = txtTietTH.Text;

        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            string sql = undoStack.Pop();
            UpdateUndoBtn();
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            LoadMonHoc();
            conn.Close();
        }

        private void txtTietLT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTietTH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
