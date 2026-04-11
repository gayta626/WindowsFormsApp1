using DevExpress.XtraCharts.Native;
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
    public partial class XfrmBangDiem : Form
    {
        string role;
        string ma;
        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=QLDSV_HTC;Integrated Security=True");
        public XfrmBangDiem(string r, string m)
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
        void loadMonHoc()
        {
            SqlDataAdapter da = new SqlDataAdapter("select MAMH, MAMH +'-'+ TENMH as HIENTHI from MONHOC", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbMonHoc.DataSource = dt;
            cbMonHoc.DisplayMember = "HIENTHI";
            cbMonHoc.ValueMember = "MAMH";
            cbMonHoc.SelectedIndex = -1;
        }
        void loadNhom()
        {
            SqlDataAdapter da = new SqlDataAdapter("select distinct NHOM from LOPTINCHI", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbNhom.DataSource = dt;
            cbNhom.DisplayMember = "NHOM";
            cbNhom.ValueMember = "NHOM";
            cbNhom.SelectedIndex = -1;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (cbNienKhoa.SelectedValue == null ||
        cbHocKy.SelectedValue == null ||
        cbKhoa.SelectedValue == null ||
        cbNhom.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin!");
                return;
            }

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_BangDiemLTC_report", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NIENKHOA", cbNienKhoa.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@HOCKY", Convert.ToInt32(cbHocKy.SelectedValue));
                cmd.Parameters.AddWithValue("@MAKHOA", cbKhoa.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@MAMH", cbMonHoc.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@NHOM", Convert.ToInt32(cbHocKy.SelectedValue));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rptBangDiemMonLTC rpt = new rptBangDiemMonLTC();
                rpt.Parameters["pKhoa"].Value = cbKhoa.SelectedValue.ToString();
                rpt.Parameters["pNienKhoa"].Value = cbNienKhoa.Text;
                rpt.Parameters["pHocKy"].Value = cbHocKy.Text;
                rpt.Parameters["pMonHoc"].Value = cbMonHoc.Text;
                rpt.Parameters["pNhom"].Value = cbNhom.Text;
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

        private void XformBangDiem_Load(object sender, EventArgs e)
        {
            loadHocKy();
            loadMonHoc();
            loadNienKhoa();
            loadNhom();
            loadKhoa();
            cbKhoa.Enabled = false;
            cbHocKy.DropDownStyle = ComboBoxStyle.DropDownList;
            cbNienKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMonHoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cbNhom.DropDownStyle = ComboBoxStyle.DropDownList;


        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
