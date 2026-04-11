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
    public partial class frmDangKyTK : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=QLDSV_HTC;Integrated Security=True");
        string role = "";
        bool isLoading = false;
        public frmDangKyTK(string r)
        {
            InitializeComponent();
            role = r;
        }
        void ResetForm()
        {
            // ❌ clear dữ liệu
            txtTaiKhoan.Clear();
            txtPass.Clear();
            txtMaGV.Clear();

            // ❌ reset combobox
            cbGiangVien.SelectedIndex = -1;
            cbRole.SelectedIndex = -1;

            // ✔ mở lại control
            txtTaiKhoan.Enabled = true;
            txtPass.Enabled = true;
            cbRole.Enabled = true;

            // ✔ bật lại nút
            btnCreate.Enabled = true;

            // 🔄 load lại danh sách nhân viên
            LoadGiangVien();
        }
        void LoadGiangVien()
        {
            try
            {
                isLoading = true;

                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT MAGV, HO + ' ' + TEN AS HOTEN FROM GIANGVIEN", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cbGiangVien.DataSource = dt;
                cbGiangVien.DisplayMember = "HOTEN";
                cbGiangVien.ValueMember = "MAGV";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load giảng viên: " + ex.Message);
            }
            finally
            {
                isLoading = false;
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_CreateLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", txtTaiKhoan.Text);
                cmd.Parameters.AddWithValue("@password", txtPass.Text);
                cmd.Parameters.AddWithValue("@role", cbRole.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Tạo tài khoản thành công");
                ResetForm();
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

        private void frmDangKyTK_Load(object sender, EventArgs e)
        {
            LoadGiangVien();
            this.Dock = DockStyle.Fill;

            if (role == "KHOA")
            {
                cbRole.Items.Clear();
                cbRole.Items.Add("KHOA");
            }

            btnDelete.Enabled = false;
        }
        void LoadThongTinLogin(string login)
        {
            string sql = @"
                SELECT r.name
                FROM sys.database_role_members rm
                JOIN sys.database_principals r ON rm.role_principal_id = r.principal_id
                JOIN sys.database_principals u ON rm.member_principal_id = u.principal_id
                WHERE u.name = @login";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@login", login);
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    cbRole.SelectedItem = result.ToString();
                }
            }

            txtPass.Text = "******";
        }

        void KiemTraTaiKhoan(string magv)
        {
            try
            {
                conn.Open();

                string sql = "SELECT name FROM sys.server_principals WHERE name = @login";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@login", magv);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        // ĐÃ CÓ
                        txtTaiKhoan.Text = magv;

                        LoadThongTinLogin(magv);

                        txtTaiKhoan.Enabled = false;
                        txtPass.Enabled = false;
                        cbRole.Enabled = false;

                        btnCreate.Enabled = false;
                        btnDelete.Enabled = true;
                    }
                    else
                    {
                        // CHƯA CÓ
                        txtTaiKhoan.Text = magv;
                        txtPass.Clear();
                        cbRole.SelectedIndex = -1;

                        txtTaiKhoan.Enabled = true;
                        txtPass.Enabled = true;
                        cbRole.Enabled = true;

                        btnCreate.Enabled = true;
                        btnDelete.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra tài khoản: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void cbGiangVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            if (cbGiangVien.SelectedItem is DataRowView row)
            {
                string magv = row["MAGV"].ToString();
                txtMaGV.Text = magv;
                KiemTraTaiKhoan(magv);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn chắc chắn muốn xóa tài khoản này?",
                "Xác nhận",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.No) return;

            try
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("sp_DeleteLogin", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", txtTaiKhoan.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Đã xóa thành công");
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
