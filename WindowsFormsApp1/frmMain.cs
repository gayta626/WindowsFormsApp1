using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmMain : Form
    {
        string role;
        string ma;
        
        public frmMain(string r, string msv)
        {
            InitializeComponent();
            role = r;
            ma = msv;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lbHelloUser.Text = "Xin chào, " + ma;
            if (role == "SV")
            {
                lớpToolStripMenuItem.Enabled = false;
                sinhVienToolStripMenuItem.Enabled = false;
                mônHọcToolStripMenuItem1.Enabled = false;
                nhậpĐiToolStripMenuItem.Enabled = false;
                mởLớpTínChỉToolStripMenuItem.Enabled = false;
                tạoTàiKhoảnToolStripMenuItem.Enabled = false;
            }
            else if (role == "KHOA")
            {
                lớpToolStripMenuItem.Enabled = false;
                sinhVienToolStripMenuItem.Enabled = false;
                mônHọcToolStripMenuItem1.Enabled = false;
                mởLớpTínChỉToolStripMenuItem.Enabled = false;
            }
        }

        private void lớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLop f = new frmLop();
            f.MdiParent = this; // mở trong main
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Show();
        }

        private void lbHelloUser_Click(object sender, EventArgs e)
        {

        }

        private void sinhVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSinhVien f = new frmSinhVien();
            f.MdiParent = this;
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            lbTitile.Hide();
            f.Show();
        }

        private void mônHọcToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmMonHoc f = new frmMonHoc();
            f.MdiParent = this;
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;   
            f.Show();
            lbTitile.Hide();
        }

        private void nhậpĐiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNhapDiem f = new frmNhapDiem();
            f.MdiParent = this;
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Show();
            lbTitile.Hide();
        }

        private void mởLớpTínChỉToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMoLTC f = new frmMoLTC(role);
            f.MdiParent = this;
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Show();
            lbTitile.Hide();

        }

        private void tạoTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDangKyTK f = new frmDangKyTK(role);
            f.MdiParent = this;
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Show();
            lbTitile.Hide();
        }

        private void đăngKýLớpTínChỉToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDangKyLTC f = new frmDangKyLTC(role, ma);
            f.MdiParent = this;
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Show();
            lbTitile.Hide();
        }

        private void xemĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPhieuDiem f = new frmPhieuDiem(ma);
            f.MdiParent = this;
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Show();
            lbTitile.Hide();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin f = new frmLogin();
            f.Show();
            this.Close();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void danhSáchLớpTinChỉToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Xfrm_DanhSachLTC xf = new Xfrm_DanhSachLTC(role,ma);
            
            xf.Show();
        }

        private void dSSinhViênĐăngKýLTCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XfrmDanhSachSVDangKyLTC xf = new XfrmDanhSachSVDangKyLTC(role, ma);
            xf.Show();
        }

        private void bảngĐiểmMônToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XfrmBangDiem xf = new XfrmBangDiem(role, ma);
            xf.Show();
        }
    }
}
