using DevExpress.XtraReports.UI;
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
    public partial class Xfrm_DanhSachLTC : Form
    {
        string role;
        string ma;
        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=QLDSV_HTC;Integrated Security=True");
        public Xfrm_DanhSachLTC(string r, string m)
        {
            InitializeComponent();
            role = r;
            ma = m;
        }
        void loadKhoa()
        {
            string sql = "select MAKHOA from GIANGVIEN where MAGV = @magv";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@magv", ma);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbKhoa.DataSource = dt;
            cbKhoa.DisplayMember = "MAKHOA";
            cbKhoa.ValueMember = "MAKHOA";
            
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

        private void Xfrm_DanhSachLTC_Load(object sender, EventArgs e)
        {
            loadHocKy();
            loadNienKhoa();
            loadKhoa();
            cbKhoa.Enabled = false;
            
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            

            if (cbNienKhoa.SelectedValue == null ||
        cbHocKy.SelectedValue == null ||
        cbKhoa.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin!");
                return;
            }

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_GetDanhSachLTC", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NIENKHOA", cbNienKhoa.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@HOCKY", cbHocKy.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@MAKHOA", cbKhoa.SelectedValue.ToString());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptDanhSachLTC rpt = new rptDanhSachLTC();
                rpt.Parameters["pKhoa"].Value = cbKhoa.SelectedValue.ToString();
                rpt.Parameters["pNienKhoa"].Value = cbNienKhoa.Text;
                rpt.Parameters["pHocKy"].Value = cbHocKy.Text;
                rpt.DataSource = dt;
                rpt.RequestParameters = false;
                rpt.ShowPreview(); 
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
