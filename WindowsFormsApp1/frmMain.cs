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
        public frmMain(string r)
        {
            InitializeComponent();
            role = r;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lbHelloUser.Text = "Xin chào, " + role;
            //if(role == "SV")
            //{
            //    lớpToolStripMenuItem.Enabled = false;
            //    sinhVienToolStripMenuItem.Enabled = false;
            //    mônHọcToolStripMenuItem1.Enabled = false;
            //    nhậpĐiToolStripMenuItem.Enabled = false;
            //    mởLớpTínChỉToolStripMenuItem.Enabled = false;
            //}
            //else if(role == "KHOA")
            //{
            //    lớpToolStripMenuItem.Enabled = false;
            //    sinhVienToolStripMenuItem.Enabled = false;
            //    mởLớpTínChỉToolStripMenuItem.Enabled = false;
            //}
        }

        private void lớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLop f = new frmLop();
            f.MdiParent = this; // mở trong main
            f.Show();
        }

        private void lbHelloUser_Click(object sender, EventArgs e)
        {

        }

        private void sinhVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSinhVien f = new frmSinhVien();
            f.MdiParent = this;
            f.Show();
            lbTitile.Hide();
        }

        private void mônHọcToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmMonHoc f = new frmMonHoc();
            f.MdiParent = this;
            f.Show();
            lbTitile.Hide();
        }

        private void nhậpĐiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mởLớpTínChỉToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDangKyLTC f = new frmDangKyLTC();
            f.MdiParent = this;
            f.Show();

        }
    }
}
