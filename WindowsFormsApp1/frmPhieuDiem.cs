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
    public partial class frmPhieuDiem : Form
    {
        string masv;
        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=QLDSV_HTC;Integrated Security=True");
        public frmPhieuDiem(string msv)
        {
            InitializeComponent();
            masv = msv;
        }
        public frmPhieuDiem()
        {
            InitializeComponent();
        }
        void loadNienKhoa()
        {
            SqlDataAdapter da = new SqlDataAdapter("select distinct NIENKHOA from LOPTINCHI", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cbNienKhoa.DataSource = dt;
            cbNienKhoa.DisplayMember = "NIENKHOA";
            cbNienKhoa.ValueMember = "NIENKHOA";
            cbNienKhoa.SelectedIndex = -1;
            
        }
        void loadHocKy()
        {
            SqlDataAdapter da = new SqlDataAdapter("select distinct HOCKY from LOPTINCHI", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cbHocKy.DataSource = dt;
            cbHocKy.DisplayMember = "HOCKY";
            cbHocKy.ValueMember = "HOCKY";
            cbHocKy.SelectedIndex = -1;
        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            
            SqlCommand cmd = new SqlCommand("sp_PhieuDiem_Filter", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MASV", masv);
            cmd.Parameters.AddWithValue("@NIENKHOA", cbNienKhoa.Text);
            cmd.Parameters.AddWithValue("@HOCKY", int.Parse(cbHocKy.Text));

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvPhieuDiem.DataSource = dt;
            float tong = 0;
            int count = 0;

            foreach (DataGridViewRow row in dgvPhieuDiem.Rows)
            {
                if (row.Cells["DIEM_HET_MON"].Value != null &&
                    row.Cells["DIEM_HET_MON"].Value.ToString() != "")
                {
                    tong += float.Parse(row.Cells["DIEM_HET_MON"].Value.ToString());
                    count++;
                }
            }

            if (count > 0)
            {
                float gpa = tong / count;
                txtGPA.Text = Math.Round(gpa, 2).ToString();
            }
            else
            {
                txtGPA.Text = "0";
            }

        }

        private void frmPhieuDiem_Load(object sender, EventArgs e)
        {
            loadNienKhoa();
            loadHocKy();
            if (System.ComponentModel.LicenseManager.UsageMode
        == System.ComponentModel.LicenseUsageMode.Designtime)
                return; 
            lbXinChao.Text = "Xin chào " + masv;
            this.Dock = DockStyle.Fill;
            dgvPhieuDiem.ReadOnly = true;
            

        }
    }
}
