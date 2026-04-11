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

namespace WindowsFormsApp1
{
    public partial class frmLogin : Form
    {
       
        public frmLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //string HashPassword(string pass)
        //{
        //    using (SHA256 sha = SHA256.Create())
        //    {
        //        byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(pass));
        //        return BitConverter.ToString(bytes).Replace("-", "");
        //    }
        //}
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            lbLogin.Text = "Login";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            lbLogin.Text = "Mã SV";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string role = "";

            if (rbSinhVien.Checked)
            {
                
                role = "SV";
                SqlConnection conn = new SqlConnection(
                    "Data Source=.;Initial Catalog=QLDSV_HTC;Integrated Security=True");

                conn.Open();

                string sql = "SELECT * FROM SINHVIEN WHERE MASV=@masv AND PASSWORD= HASHBYTES('SHA2_256', @pass)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@masv", txtUser.Text);
                cmd.Parameters.AddWithValue("@pass", txtPass.Text);

                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    MessageBox.Show("Đăng nhập SV thành công");

                    frmMain f = new frmMain(role,txtUser.Text);
                    f.Show();
                    this.Hide();
                    
                }
                else
                {
                    MessageBox.Show("Sai mã SV hoặc mật khẩu");
                }

                conn.Close();
            }

            // ===== GIẢNG VIÊN / KHOA =====
            else if (rbGiangVien.Checked)
            {
                string connStr = "Data Source=WINDOWS-11;Initial Catalog=QLDSV_HTC;User ID="
                                + txtUser.Text + ";Password=" + txtPass.Text;

                SqlConnection conn = new SqlConnection(connStr);

                try
                {
                    conn.Open();

                    

                    SqlCommand cmd = new SqlCommand(
                        "SELECT IS_MEMBER('PGV'), IS_MEMBER('KHOA')", conn);

                    SqlDataReader rd = cmd.ExecuteReader();

                    if (rd.Read())
                    {
                        if ((int)rd[0] == 1) role = "PGV";
                        else if ((int)rd[1] == 1) role = "KHOA";
                    }

                    frmMain f = new frmMain(role, txtUser.Text);
                    f.Show();
                    this.Hide();
                }
                catch
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu");
                }
            }


        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}