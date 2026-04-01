using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class frmLop : Form
    {
        int mode = 0; // 0: k làm gì, 1: thêm, 2: sửa
        Stack<string> undoStack = new Stack<string>();
        static string oldMaLop ="";
        static string oldTenLop = "";
        static string oldKhoaHoc = "";
        static string oldMaKhoa = "";
        
        public frmLop()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        SqlDataAdapter da;
        DataTable dt;

        void LoadData()
        {
            conn = new SqlConnection(
            "Data Source=.;Initial Catalog=QLDSV_HTC;Integrated Security=True");

            string sql = "SELECT * FROM LOP";

            da = new SqlDataAdapter(sql, conn);

            dt = new DataTable();

            da.Fill(dt);

            dataGridViewLop.DataSource = dt;
            
        }

        void updateUndoBtn()
        {
            btnUndo.Enabled = undoStack.Count > 0;
        }
        private void frmLop_Load(object sender, EventArgs e)
        {
            LoadData();
            this.Dock = DockStyle.Fill;
            updateUndoBtn();
        }

        private void dataGridViewLop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void cbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            txtMaLop.Text = "";
            txtTenLop.Text = "";
            txtKhoaHoc.Text = "";
            cbKhoa.Text = "";
            txtMaLop.Focus();
            mode = 1;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sqlUndo = "insert into LOP(MALOP, TENLOP ,KHOAHOC,MAKHOA) values(N'" + txtMaLop.Text + "', N'" + txtTenLop.Text + "',N'" + txtKhoaHoc.Text + "', N'" + cbKhoa.Text + "')";
            undoStack.Push(sqlUndo);
            updateUndoBtn();
            SqlCommand cmd = new SqlCommand("delete from LOP where MALOP = @malop", conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@malop", txtMaLop.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            LoadData();
            MessageBox.Show("Xóa thành công");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            

            if(mode == 1)
            {
                string sqlUndo = "delete from LOP where MALOP = '" + txtMaLop.Text + "'";
                undoStack.Push(sqlUndo);
                updateUndoBtn();
                conn.Open();
                SqlCommand check = new SqlCommand("select count(*) from LOP where MALOP = @malop", conn);
                check.Parameters.AddWithValue("@malop", txtMaLop.Text);
                int count = (int)check.ExecuteScalar();
                
                if(count > 1)
                {
                    MessageBox.Show("Mã lớp đã tồn tại");
                    conn.Close();
                    return;
                }
                SqlCommand cmd = new SqlCommand("insert into LOP(MALOP , TENLOP , KHOAHOC, MAKHOA) values(@malop , @tenlop, @khoahoc, @khoa)", conn);
                cmd.Parameters.AddWithValue("@malop", txtMaLop.Text);
                cmd.Parameters.AddWithValue("@tenlop", txtTenLop.Text);
                cmd.Parameters.AddWithValue("@khoahoc", txtKhoaHoc.Text);
                cmd.Parameters.AddWithValue("@khoa", cbKhoa.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm lớp thành công");
                LoadData();
                conn.Close();


            }

            else if(mode == 2)
            {
                string sqlUndo = "update LOP set TENLOP =N'" + oldTenLop + "',KHOAHOC = N'" + oldKhoaHoc + "', MAKHOA = N'" + oldMaKhoa + "' where MALOP = N'" + oldMaLop + "'";
                undoStack.Push(sqlUndo);
                updateUndoBtn();
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                "UPDATE LOP SET TENLOP=@tenlop, KHOAHOC=@khoahoc, MAKHOA=@makhoa WHERE MALOP=@malop", conn);

                cmd.Parameters.AddWithValue("@malop", txtMaLop.Text);
                cmd.Parameters.AddWithValue("@tenlop", txtTenLop.Text);
                cmd.Parameters.AddWithValue("@khoahoc", txtKhoaHoc.Text);
                cmd.Parameters.AddWithValue("@makhoa", cbKhoa.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công");
                LoadData();
                conn.Close();
            }

            
            mode = 0;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();


        }
        
        private void btnUndo_Click(object sender, EventArgs e)
        {
            
            string sql = undoStack.Pop();
            updateUndoBtn();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                LoadData();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void dataGridViewLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaLop.Text = dataGridViewLop.CurrentRow.Cells["MALOP"].Value.ToString();
            txtTenLop.Text = dataGridViewLop.CurrentRow.Cells["TENLOP"].Value.ToString();
            txtKhoaHoc.Text = dataGridViewLop.CurrentRow.Cells["KHOAHOC"].Value.ToString();
            cbKhoa.Text = dataGridViewLop.CurrentRow.Cells["MAKHOA"].Value.ToString();
            oldMaLop = txtMaLop.Text;
            oldTenLop = txtTenLop.Text;
            oldKhoaHoc = txtKhoaHoc.Text;
            oldMaKhoa = cbKhoa.Text;
            mode = 2;
        }
    }
}
