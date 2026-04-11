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
        string role ="";
        string ma = "";
        public frmDangKyLTC(string r,string m)
        {
            InitializeComponent();
            role = r;
            ma = m;
        }
        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=QLDSV_HTC;Integrated Security=True");
        string maLop = "";
        void loadLopTinChi()
        {
            string sql = "select * from LOPTINCHI";
            SqlCommand cmd = new SqlCommand(sql, conn);
            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvLTC.DataSource = dt;
        }
        void loadMaSV()
        {
            string sql = "select MASV, MASV+'-'+ HO +' '+ TEN as HIENTHI from SINHVIEN where MALOP = @malop";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@malop", maLop);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbMaSV.DataSource = dt;
            cbMaSV.DisplayMember = "HIENTHI";
            cbMaSV.ValueMember = "MASV";
            cbMaSV.SelectedIndex = -1;
        }
        void loadNienKhoa()
        {
            string sql = "select distinct NIENKHOA from LOPTINCHI";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbNienKhoa.DataSource = dt;
            cbNienKhoa.DisplayMember = "NIENKHOA";
            cbNienKhoa.ValueMember = "NIENKHOA";
            cbNienKhoa.SelectedIndex = -1;
        }
        void loadHocKy()
        {
            string sql = "select distinct HOCKY from LOPTINCHI";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbHocKy.DataSource = dt;
            cbHocKy.DisplayMember = "HOCKY";
            cbHocKy.ValueMember = "HOCKY";
            cbHocKy.SelectedIndex = -1;
        }
        void loadLop()
        {
            string sql = "select * from LOP";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvLop.DataSource = dt;
        }
        void UpdateDangKyBtn()
        {
            btnDangKy.Enabled = dgvLTC.CurrentRow != null
                                && !dgvLTC.CurrentRow.IsNewRow;
        }
        bool DaDangKyLTC(string masv, int maltc)
        {
            conn.Open();
            string sql = "SELECT COUNT(*) FROM DANGKY WHERE MASV = @MASV AND MALTC = @MALTC";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MASV", masv);
            cmd.Parameters.AddWithValue("@MALTC", maltc);

            int result = (int)cmd.ExecuteScalar();

            conn.Close();

            return result > 0;
        }
        bool DaDangKyMon(string masv, string mamh, string nienkhoa)
        {
            conn.Open();
            string sql = @"
        SELECT COUNT(*) 
        FROM DANGKY DK
        JOIN LOPTINCHI LTC ON DK.MALTC = LTC.MALTC
        WHERE DK.MASV = @MASV
        AND LTC.MAMH = @MAMH
        AND LTC.NIENKHOA = @NIENKHOA";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MASV", masv);
            cmd.Parameters.AddWithValue("@MAMH", mamh);
            cmd.Parameters.AddWithValue("@NIENKHOA", nienkhoa);

            int result = (int)cmd.ExecuteScalar();

            conn.Close();

            return result > 0;
        }


        private void dgvLTC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            UpdateDangKyBtn();
            string masv = ma;
            int maltc = int.Parse(dgvLTC.Rows[i].Cells["MALTC"].Value.ToString());
            string mamh = dgvLTC.Rows[i].Cells["MAMH"].Value.ToString();
            string nienkhoa = dgvLTC.Rows[i].Cells["NIENKHOA"].Value.ToString();
            if (DaDangKyLTC(masv, maltc) || DaDangKyMon(masv, mamh, nienkhoa))
            {
                btnDangKy.Enabled = false;
            }
            else
            {
                btnDangKy.Enabled = true;
            }

        }

        private void frmDangKyLTC_Load(object sender, EventArgs e)
        {
            loadNienKhoa();
            loadHocKy();
            loadLop();
            loadLopTinChi();
            this.Dock = DockStyle.Fill;
            txtHoTen.Enabled = false;
            txtMaLop.Enabled = false;
            dgvLop.ReadOnly = true;
            dgvLTC.ReadOnly = true;
            dgvLTC.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLTC.MultiSelect = false; 
            dgvLop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLop.MultiSelect = false;
            if(role == "SV")
            {
                dgvLop.Visible = false;
                cbMaSV.Text = ma;
                cbMaSV.Enabled = false;

            }
        }

        private void btnTimSV_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                if(role == "PGV")
                {
                    
                    SqlCommand cmd = new SqlCommand("Select HO+ ' ' +TEN as HOTEN, MALOP from SINHVIEN where MASV = @masv", conn);
                    cmd.Parameters.AddWithValue("@masv", cbMaSV.SelectedValue.ToString());
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtHoTen.Text = reader["HOTEN"].ToString();
                        txtMaLop.Text = reader["MALOP"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Không tồn tại");
                    }
                }
                else if(role == "SV")
                {
                    
                    SqlCommand cmd = new SqlCommand("Select HO+ ' ' +TEN as HOTEN, MALOP from SINHVIEN where MASV = @masv", conn);
                    cmd.Parameters.AddWithValue("@masv", ma);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtHoTen.Text = reader["HOTEN"].ToString();
                        txtMaLop.Text = reader["MALOP"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Không tồn tại");
                    }
                }
                
            }catch(Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            
           
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
           
            SqlCommand cmd = new SqlCommand("sp_LTC_Filter", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (cbNienKhoa.SelectedValue != null)
                cmd.Parameters.AddWithValue("@NIENKHOA", cbNienKhoa.SelectedValue.ToString());
            else
                cmd.Parameters.AddWithValue("@NIENKHOA", DBNull.Value);

            if (cbHocKy.SelectedValue != null)
                cmd.Parameters.AddWithValue("@HOCKY", int.Parse(cbHocKy.SelectedValue.ToString()));
            else
                cmd.Parameters.AddWithValue("@HOCKY", DBNull.Value);

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

                    cmd.Parameters.AddWithValue("@MASV", cbMaSV.SelectedValue.ToString());
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

        private void dgvLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (e.RowIndex < 0) return;
            if (i >= 0)
            {
                maLop = dgvLop.Rows[i].Cells["MALOP"].Value.ToString();
                
            }
            loadMaSV();
        }
    }
}
