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
    public partial class frmSinhVien : Form
    {
        public frmSinhVien()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(
        "Data Source=.;Initial Catalog=QLDSV_HTC;Integrated Security=True");

        int mode = 0; // 1 thêm, 2 sửa
        string malop_dangchon = "";

        void LoadLop()
        {
            string sql = "SELECT * FROM LOP";

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();

            da.Fill(dt);

            dgvLop.DataSource = dt;
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
            int i = e.RowIndex;

            if (i >= 0)
            {
                malop_dangchon = dgvLop.Rows[i].Cells["MALOP"].Value.ToString();

                txtMaLop.Text = malop_dangchon;

                LoadSinhVien(malop_dangchon);
            }
        }

        private void frmSinhVien_Load(object sender, EventArgs e)
        {
            LoadLop();
            this.Dock = DockStyle.Fill;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;

            if (i >= 0)
            {
                txtMaSV.Text = dgvSinhVien.Rows[i].Cells["MASV"].Value.ToString();
                txtHo.Text = dgvSinhVien.Rows[i].Cells["HO"].Value.ToString();
                txtTen.Text = dgvSinhVien.Rows[i].Cells["TEN"].Value.ToString();
                txtMaLop.Text = dgvSinhVien.Rows[i].Cells["MALOP"].Value.ToString();
                if(dgvSinhVien.Rows[i].Cells["PHAI"].Value.Equals(false))
                {
                    cbSex.Text = "Nam";
                }
                else
                {
                    cbSex.Text = "Nữ";
                }
                txtDiaChi.Text = dgvSinhVien.Rows[i].Cells["DIACHI"].Value.ToString();
                dateTime.Text = dgvSinhVien.Rows[i].Cells["NGAYSINH"].Value.ToString();
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
            }
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
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from SINHVIEN where MASV = @masv", conn);
            cmd.Parameters.AddWithValue("@masv", txtMaSV.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            LoadSinhVien(malop_dangchon);
            MessageBox.Show("Xóa thành công");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            conn.Open();
            if(mode == 1)
            {
                SqlCommand cmd = new SqlCommand("insert into SINHVIEN(MASV, HO, TEN, PHAI, DIACHI, NGAYSINH, MALOP, DANGHIHOC, PASSWORD)" +
                    " values(@masv, @ho, @ten, @phai,@diachi,@ngaysinh,@malop,@situation,@pass)", conn);
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
                LoadSinhVien(txtMaLop.Text);
                MessageBox.Show("Thêm sinh viên thành công");
            }
            else if(mode == 2)
            {
                SqlCommand cmd = new SqlCommand("update SINHVIEN set HO =@ho, TEN = @ten, PHAI = @phai, DIACHI = @diachi, NGAYSINH = @ngaysinh, MALOP =@malop , DANGHIHOC= @situation, PASSWORD = @pass where MASV=@masv" , conn);
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
                cmd.Parameters.AddWithValue("@password", txtPass.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công");
                LoadSinhVien(txtMaLop.Text);
            }
            conn.Close();
            mode = 0;
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            LoadSinhVien(malop_dangchon);

            txtMaSV.Text = "";
            txtHo.Text = "";
            txtTen.Text = "";
            cbSex.Text = "";
            txtDiaChi.Text = "";
            dateTime.Text = "";
            txtMaLop.Text = "";
            cbSituation.Text = "";
            txtPass.Text = "";
        }
    }
}
