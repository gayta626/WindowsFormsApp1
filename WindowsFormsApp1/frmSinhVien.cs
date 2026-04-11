using DevExpress.XtraEditors.Design;
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
using System.Security.Cryptography;
using System.Text;

namespace WindowsFormsApp1
{
    public partial class frmSinhVien : Form
    {
        Stack<string> undoStack = new Stack<string>();
        public frmSinhVien()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(
        "Data Source=.;Initial Catalog=QLDSV_HTC;Integrated Security=True");

        int mode = 0; // 1 thêm, 2 sửa
        string malop_dangchon = "";

        string HashPassword(string pass)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(pass));
                return BitConverter.ToString(bytes).Replace("-", "");
            }
        }
        void UpdateUndoBtn()
        {
            btnUndo.Enabled = undoStack.Count > 0;
        }
        void LoadLop()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM LOP WHERE KHOAHOC = @khoahoc AND MAKHOA = @makhoa", conn);
            cmd.Parameters.AddWithValue("@khoahoc", cbKhoaHocFilter.Text);
            cmd.Parameters.AddWithValue("@makhoa", cbMaKhoaFilter.Text);
            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            dgvLop.DataSource = dt;
        }
        void LoadKhoaHoc()
        {
            SqlCommand cmd = new SqlCommand("select distinct KHOAHOC from LOP ", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbKhoaHocFilter.DataSource = dt;
            cbKhoaHocFilter.DisplayMember = "KHOAHOC";
            cbKhoaHocFilter.ValueMember = "KHOAHOC";
        }
        void LoadMaKhoa()
        {
            SqlCommand cmd = new SqlCommand("select distinct MAKHOA from LOP ", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbMaKhoaFilter.DataSource = dt;
            cbMaKhoaFilter.DisplayMember = "MAKHOA";
            cbMaKhoaFilter.ValueMember = "MAKHOA";
        }
        void LoadSinhVien(string malop)
        {
            string sql = "SELECT * FROM SINHVIEN WHERE MALOP = @malop";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@malop", malop);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            dgvSinhVien.DataSource = dt;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }


        private void dgvLop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void frmSinhVien_Load(object sender, EventArgs e)
        {
            LoadMaKhoa();
            LoadKhoaHoc();
            LoadSinhVien(malop_dangchon);
            this.Dock = DockStyle.Fill;
            UpdateUndoBtn();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtMaSV.Text = "";
            txtHo.Text = "";
            txtTen.Text = "";
            cbSex.Text = "";
            txtDiaChi.Text = "";
            dateTime.Text = "";
            txtMaLop.Text = "";
            cbSituation.Text = "";
            txtPass.Text = "";
            txtMaSV.Focus();
            mode = 1;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO SINHVIEN(MASV, HO, TEN, PHAI, DIACHI, NGAYSINH, MALOP, DANGHIHOC, PASSWORD) VALUES (" +"N'" + txtMaSV.Text + "', " +
             "N'" + txtHo.Text + "', " +
             "N'" + txtTen.Text + "', " +
             (cbSex.Text == "Nam" ? "0" : "1") + ", " +
             "N'" + txtDiaChi.Text + "', " +
             "'" + dateTime.Value.ToString("yyyy-MM-dd") + "', " +
             "N'" + txtMaLop.Text + "', " +
             (cbSituation.Text == "Đang học" ? "0" : "1") + ", " +
             "N'" + txtPass.Text + "')";

            undoStack.Push(sql);
            UpdateUndoBtn();
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from SINHVIEN where MASV = @masv", conn);
            cmd.Parameters.AddWithValue("@masv", txtMaSV.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            LoadSinhVien(malop_dangchon);
            MessageBox.Show("Xóa thành công");
        }
        
        string oldMaSV = "";
        string oldHo = "";
        string oldTen = "";
        string oldSex = "";
        string oldDiaChi = "";
        DateTime oldNgaySinh ;
        string oldMaLop = "";
        string oldSituation = "";
        string oldPass = "";
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            if(mode == 1)
            {
                conn.Open();
                string sql = "delete from SINHVIEN where MASV = N'"+txtMaSV.Text+"'";
                undoStack.Push(sql);
                UpdateUndoBtn();
                SqlCommand cmd = new SqlCommand("insert into SINHVIEN(MASV, HO, TEN, PHAI, DIACHI, NGAYSINH, MALOP, DANGHIHOC, PASSWORD)" +
                    " values(@masv, @ho, @ten, @phai,@diachi,@ngaysinh,@malop,@situation, HASHBYTES('SHA2_256',@pass))", conn);
                cmd.Parameters.AddWithValue("@masv", txtMaSV.Text);
                cmd.Parameters.AddWithValue("@ho", txtHo.Text);
                cmd.Parameters.AddWithValue("@ten", txtTen.Text);
                if(cbSex.Text == "Nam")
                {
                    cmd.Parameters.AddWithValue("@phai", false);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@phai", true);
                }
                cmd.Parameters.AddWithValue("@diachi", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@ngaysinh", dateTime.Value);
                cmd.Parameters.AddWithValue("@malop", txtMaLop.Text);
                if(cbSituation.Text == "Đang học")
                {
                    cmd.Parameters.AddWithValue("@situation", false);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@situation", true);
                }
                cmd.Parameters.AddWithValue("@pass", txtPass.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                malop_dangchon = txtMaLop.Text;
                LoadSinhVien(malop_dangchon);
                MessageBox.Show("Thêm sinh viên thành công");
            }
            else if(mode == 2)
            {
                conn.Open();
                string sql = "UPDATE SINHVIEN SET " +
             "HO = N'" + oldHo + "', " +
             "TEN = N'" + oldTen + "', " +
             "PHAI = " + (oldSex == "Nam" ? "0" : "1") + ", " +
             "DIACHI = N'" + oldDiaChi + "', " +
             "NGAYSINH = '" + oldNgaySinh.ToString("yyyy-MM-dd") + "', " +
             "MALOP = N'" + oldMaLop + "', " +
             "DANGHIHOC = " + (oldSituation == "Đang học" ? "0" : "1") + ", " +
             "PASSWORD = N'" + oldPass + "' " +
             "WHERE MASV = N'" + oldMaSV + "'";

                undoStack.Push(sql);
                UpdateUndoBtn();
                SqlCommand cmd = new SqlCommand("update SINHVIEN set HO =@ho, TEN = @ten, PHAI = @phai, DIACHI = @diachi, NGAYSINH = @ngaysinh, MALOP =@malop , DANGHIHOC= @situation, PASSWORD = HASHBYTES('SHA2_256',@pass) where MASV=@masv", conn);
                cmd.Parameters.AddWithValue("@masv", txtMaSV.Text);
                cmd.Parameters.AddWithValue("@ho", txtHo.Text);
                cmd.Parameters.AddWithValue("@ten", txtTen.Text);
                if (cbSex.Text == "Nam")
                {
                    cmd.Parameters.AddWithValue("@phai", false);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@phai", true);
                }
                cmd.Parameters.AddWithValue("@diachi", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@ngaysinh", dateTime.Value);
                cmd.Parameters.AddWithValue("@malop", txtMaLop.Text);
                if (cbSituation.Text == "Đang học")
                {
                    cmd.Parameters.AddWithValue("@situation", false);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@situation", true);
                }
                cmd.Parameters.AddWithValue("@pass", txtPass.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công");
                conn.Close();
                malop_dangchon = txtMaLop.Text;
                LoadSinhVien(malop_dangchon);
            }
            
            mode = 0;
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            string sql = undoStack.Pop();
            UpdateUndoBtn();
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            LoadSinhVien(malop_dangchon);
            conn.Close();
        }

        private void dgvLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            int i = e.RowIndex;

            if (i >= 0)
            {
                malop_dangchon = dgvLop.Rows[i].Cells["MALOP"].Value.ToString();

                txtMaLop.Text = malop_dangchon;

                LoadSinhVien(malop_dangchon);
            }
        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAdd.Enabled = false;
            int i = e.RowIndex;

            if (i >= 0)
            {
                txtMaSV.Text = dgvSinhVien.Rows[i].Cells["MASV"].Value.ToString();
                txtHo.Text = dgvSinhVien.Rows[i].Cells["HO"].Value.ToString();
                txtTen.Text = dgvSinhVien.Rows[i].Cells["TEN"].Value.ToString();
                txtMaLop.Text = dgvSinhVien.Rows[i].Cells["MALOP"].Value.ToString();
                if (dgvSinhVien.Rows[i].Cells["PHAI"].Value.Equals(false))
                {
                    cbSex.Text = "Nam";
                }
                else
                {
                    cbSex.Text = "Nữ";
                }
                txtDiaChi.Text = dgvSinhVien.Rows[i].Cells["DIACHI"].Value.ToString();
                dateTime.Value = Convert.ToDateTime(dgvSinhVien.Rows[i].Cells["NGAYSINH"].Value);

                if (dgvSinhVien.Rows[i].Cells["DANGHIHOC"].Value.Equals(false))
                {
                    cbSituation.Text = "Đang học";
                }
                else
                {
                    cbSituation.Text = "Đang nghỉ học";
                }
                txtPass.Text = dgvSinhVien.Rows[i].Cells["PASSWORD"].Value.ToString();
                mode = 2; // sửa
                oldMaSV = txtMaSV.Text ;
                oldHo = txtHo.Text;
                oldTen = txtTen.Text ;
                oldSex = cbSex.Text ;
                oldDiaChi = txtDiaChi.Text ;
                oldNgaySinh =  (DateTime)dgvSinhVien.CurrentRow.Cells["NGAYSINH"].Value;                ;
                oldMaLop = txtMaLop.Text;
                oldSituation = cbSituation.Text ;
                oldPass = txtPass.Text;
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadLop();
            btnAdd.Enabled = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            txtMaSV.Text = "";
            txtHo.Text = "";
            txtTen.Text = "";
            cbSex.Text = "";
            txtDiaChi.Text = "";
            
            txtMaLop.Text = "";
            cbSituation.Text = "";
            txtPass.Text = "";
        }
    }
}
