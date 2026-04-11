using DevExpress.DataAccess.Native.Sql;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmMoLTC : Form
    {
        string role;
        int namHienTai = DateTime.Now.Year;
        public frmMoLTC(string r)
        {
            InitializeComponent();
            role = r;
        }
        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=QLDSV_HTC;Integrated Security=True");
        int mode = 0;
        Stack<UndoAction> undoStack = new Stack<UndoAction>();
        string oldMaLTC = "";
        string oldNienKhoa = "";
        string oldhocKy = "";
        string oldMonHoc = "";
        string oldMaGV = "";
        string oldMaKhoa = "";
        int oldNhom;
        int oldSoSV ;
        string oldTrangThai = "";
        class UndoAction
        {
            public string actionType;
            public string maLTC;
            public string nienKhoa;
            public string hocKy;
            public string monHoc;
            public string maGV;
            public string maKhoa;
            public int nhom;
            public int soSV;
            public string trangThai;
        }
        void UpdateUndoBtn()
        {
            btnUndo.Enabled = undoStack.Count > 0;
        }
        void loadLopTinChiFilter()
        {
            string sql = "SELECT * FROM LOPTINCHI WHERE 1=1";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            if (cbNienKhoa.SelectedIndex != -1)
            {
                sql += " AND NIENKHOA = @nk";
                cmd.Parameters.AddWithValue("@nk", cbNienKhoa.SelectedValue.ToString().Trim());
            }

            if (cbHocKyFilter.SelectedIndex != -1)
            {
                sql += " AND HOCKY = @hk";
                cmd.Parameters.AddWithValue("@hk", cbHocKyFilter.SelectedValue.ToString().Trim());
            }

            if (cbMonHocFilter.SelectedIndex != -1)
            {
                sql += " AND MAMH = @mh";
                cmd.Parameters.AddWithValue("@mh", cbMonHocFilter.SelectedValue.ToString().Trim());
            }

            if (cbMaKhoaFilter.SelectedIndex != -1)
            {
                sql += " AND MAKHOA = @mk";
                cmd.Parameters.AddWithValue("@mk", cbMaKhoaFilter.SelectedValue.ToString().Trim());
            }

            cmd.CommandText = sql;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvLTC.DataSource = dt;
            

        }

        void loadData()
        {
            string sql = "select *from LOPTINCHI";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();

            da.Fill(dt);
            dgvLTC.DataSource = dt;
        }
        void loadGiaoVien()
        {
            string sql = "select MAGV,MAGV +'-'+HO+' '+TEN AS HIENTHI from GIANGVIEN";
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbMaGV.DataSource = dt;
            cbMaGV.DisplayMember = "HIENTHI";
            cbMaGV.ValueMember = "MAGV";
            cbMaGV.SelectedIndex = -1; //chưa chọn gì
        }
        
        void loadHocKy()
        {
            string sql = "select distinct HOCKY from LOPTINCHI";
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbHocKyFilter.DataSource = dt;
            cbHocKyFilter.DisplayMember = "HOCKY";
            cbHocKyFilter.ValueMember = "HOCKY";
            cbHocKyFilter.SelectedIndex = -1; //chưa chọn gì
        }
        void loadMaKhoa()
        {
            string sql = "select MAKHOA,MAKHOA+'-'+TENKHOA AS HIENTHI from KHOA";
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbMaKhoa.DataSource = dt;
            cbMaKhoa.DisplayMember = "HIENTHI";
            cbMaKhoa.ValueMember = "MAKHOA";
            
            cbMaKhoaFilter.DataSource = dt;
            cbMaKhoaFilter.DisplayMember = "HIENTHI";
            cbMaKhoaFilter.ValueMember = "MAKHOA";
            cbMaKhoaFilter.SelectedIndex = -1;// chưa chọn gì
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
        
        
        void loadMonHoc()
        {
            string sql = "select MAMH, MAMH + '-'+ TENMH as HIENTHI from MONHOC";
            
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbTenMH.DataSource = dt;
            cbTenMH.DisplayMember = "HIENTHI";
            cbTenMH.ValueMember = "MAMH";
            cbMonHocFilter.DataSource = dt;
            cbMonHocFilter.DisplayMember = "HIENTHI";
            cbMonHocFilter.ValueMember = "MAMH";
            cbMonHocFilter.SelectedIndex = -1;

        }
        private void dgvLTC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void frmDangKyLTC_Load(object sender, EventArgs e)
        {
            loadData();
            loadMonHoc();
            loadGiaoVien();
            loadMaKhoa();
            loadNienKhoa();
            loadHocKy();
            this.Dock = DockStyle.Fill;
            btnDelete.Enabled = false;
            txtMaLTC.Enabled = false;
            UpdateUndoBtn();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtNienKhoa.Text = "";
            cbHocKy.Text = "";
            cbTenMH.Text = "";
            txtNhom.Text = "";
            cbMaGV.Text = "";
            cbMaKhoa.Text = "";
            txtSoSV.Text = "";
            cbSituation.Text = "";
            txtNienKhoa.Focus();

            mode = 1; // Thêm 
            btnDelete.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "Bạn có chắc muốn xóa không?",
        "Xác nhận",
        MessageBoxButtons.YesNo
    );

            if (result == DialogResult.No) return;

            try
            {
                UndoAction action = new UndoAction()
                {
                    actionType = "DELETE",
                    maLTC = txtMaLTC.Text,
                    nienKhoa = txtNienKhoa.Text,
                    hocKy = cbHocKy.Text,
                    monHoc = cbTenMH.SelectedValue?.ToString(),
                    nhom = string.IsNullOrWhiteSpace(txtNhom.Text) ? 0 : int.Parse(txtNhom.Text),
                    maGV =cbMaGV.SelectedValue?.ToString(),
                    maKhoa = cbMaKhoa.SelectedValue?.ToString(),
                    soSV = string.IsNullOrWhiteSpace(txtSoSV.Text) ? 0 : int.Parse(txtSoSV.Text),
                    trangThai = cbSituation.Text

                };
                undoStack.Push(action);
                UpdateUndoBtn();

                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_LTC_Delete1", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@maltc", txtMaLTC.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Xóa thành công");

                loadData(); // reload lại grid
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open(); 

                if (mode == 1)
                {
                    SqlCommand cmd = new SqlCommand("sp_LTC_Insert", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    string nienKhoa = txtNienKhoa.Text.Trim();
                    int namBatDau = int.Parse(nienKhoa.Substring(0, 4));
                    if(namBatDau < namHienTai-1)
                    {
                        MessageBox.Show("Niên khóa không hợp lệ !!");
                        return;
                    }
                    if (!Regex.IsMatch(nienKhoa, @"^\d{4}-\d{4}$"))
                    {
                        MessageBox.Show("Niên khóa sai định dạng!");
                        return;
                    }
                    int nam1 = int.Parse(nienKhoa.Substring(0, 4));
                    int nam2 = int.Parse(nienKhoa.Substring(5, 4));

                    if (nam2 != nam1 + 1)
                    {
                        MessageBox.Show("Niên khóa phải dạng YYYY-(YYYY+1)");
                        return;
                    }
                    cmd.Parameters.AddWithValue("@NIENKHOA", txtNienKhoa.Text);
                    cmd.Parameters.AddWithValue("@HOCKY", cbHocKy.Text);
                    cmd.Parameters.AddWithValue("@MAMH", cbTenMH.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@NHOM", int.Parse(txtNhom.Text));
                    cmd.Parameters.AddWithValue("@MAGV", cbMaGV.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@MAKHOA", cbMaKhoa.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@SOSVTOITHIEU", int.Parse(txtSoSV.Text));
                    cmd.Parameters.AddWithValue("@HUYLOP", cbSituation.Text);

                    object result = cmd.ExecuteScalar();
                    int newMaLTC = Convert.ToInt32(result);

                    UndoAction action = new UndoAction()
                    {
                        actionType = "INSERT",
                        maLTC = newMaLTC.ToString() 
                    };
                    undoStack.Push(action);
                    UpdateUndoBtn();

                    MessageBox.Show("Thêm lớp tín chỉ thành công");
                }

                else if (mode == 2)
                {
                    UndoAction action = new UndoAction()
                    {
                        actionType = "UPDATE",
                        maLTC = oldMaLTC,
                        nienKhoa = oldNienKhoa,
                        hocKy = oldhocKy,
                        monHoc = oldMonHoc,
                        nhom = oldNhom,
                        maGV = oldMaGV,
                        maKhoa = oldMaKhoa,
                        soSV = oldSoSV,
                        trangThai = oldTrangThai
                    };
                    undoStack.Push(action);
                    UpdateUndoBtn();

                    SqlCommand cmd = new SqlCommand("sp_LTC_Update", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MALTC", txtMaLTC.Text);
                    string nienKhoa = txtNienKhoa.Text.Trim();
                    int namBatDau = int.Parse(nienKhoa.Substring(0, 4));
                    if (namBatDau < namHienTai-1)
                    {
                        MessageBox.Show("Niên khóa không hợp lệ !!");
                        return;
                    }
                    if (!Regex.IsMatch(nienKhoa, @"^\d{4}-\d{4}$"))
                    {
                        MessageBox.Show("Niên khóa sai định dạng!");
                        return;
                    }
                    int nam1 = int.Parse(nienKhoa.Substring(0, 4));
                    int nam2 = int.Parse(nienKhoa.Substring(5, 4));

                    if (nam2 != nam1 + 1)
                    {
                        MessageBox.Show("Niên khóa phải dạng YYYY-(YYYY+1)");
                        return;
                    }
                    cmd.Parameters.AddWithValue("@NIENKHOA", txtNienKhoa.Text);
                    cmd.Parameters.AddWithValue("@HOCKY", cbHocKy.Text);
                    cmd.Parameters.AddWithValue("@MAMH", cbTenMH.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@NHOM", int.Parse(txtNhom.Text));
                    cmd.Parameters.AddWithValue("@MAGV", cbMaGV.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@MAKHOA", cbMaKhoa.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@SOSVTOITHIEU", int.Parse(txtSoSV.Text));
                    cmd.Parameters.AddWithValue("@HUYLOP", cbSituation.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật thành công !");
                }

                loadData();
                mode = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: Kiểm tra lại thông tin trùng lặp !!");
            }
            finally
            {
                conn.Close(); 
            }

        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMaLTC_TextChanged(object sender, EventArgs e)
        {
            txtMaLTC.ReadOnly = true;
        }

        private void dgvLTC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;

            if (i >= 0)
            {
                txtMaLTC.Text = dgvLTC.Rows[i].Cells["MALTC"].Value.ToString();
                txtNienKhoa.Text = dgvLTC.Rows[i].Cells["NIENKHOA"].Value.ToString();
                cbHocKy.Text = dgvLTC.Rows[i].Cells["HOCKY"].Value.ToString();

                cbTenMH.SelectedValue = dgvLTC.Rows[i].Cells["MAMH"].Value ?? "";
                txtNhom.Text = dgvLTC.Rows[i].Cells["NHOM"].Value.ToString();

                cbMaGV.SelectedValue = dgvLTC.Rows[i].Cells["MAGV"].Value ?? "";
                cbMaKhoa.SelectedValue = dgvLTC.Rows[i].Cells["MAKHOA"].Value ?? "";

                txtSoSV.Text = dgvLTC.Rows[i].Cells["SOSVTOITHIEU"].Value.ToString();
                cbSituation.Text = dgvLTC.Rows[i].Cells["HUYLOP"].Value.ToString();
                mode = 2; // Sửa
                
            }
            btnDelete.Enabled = dgvLTC.CurrentRow != null &&
                    dgvLTC.CurrentRow.Cells["MAMH"].Value != null;

            oldMaLTC = txtMaLTC.Text;
            oldNienKhoa = txtNienKhoa.Text;
            oldhocKy = cbHocKy.Text;
            oldMonHoc = cbTenMH.SelectedValue?.ToString();
            oldMaGV = cbMaGV.SelectedValue?.ToString();
            oldMaKhoa = cbMaKhoa.SelectedValue?.ToString();
            oldNhom = string.IsNullOrWhiteSpace(txtNhom.Text) ? 0 : int.Parse(txtNhom.Text);
            oldSoSV = string.IsNullOrWhiteSpace(txtSoSV.Text) ? 0 : int.Parse(txtSoSV.Text);
            oldTrangThai = cbSituation.Text;
            string nienKhoa = txtNienKhoa.Text;
            int namBatDau = int.Parse(nienKhoa.Substring(0, 4));
            if(namBatDau < namHienTai - 1)
            {
                btnUpdate.Enabled = false;
            }
            else
            {
                btnUpdate.Enabled = true;
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            loadLopTinChiFilter();
            btnDelete.Enabled = false;
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (undoStack.Count == 0) return;
            UndoAction action = undoStack.Pop();
            UpdateUndoBtn();
            try
            {
                conn.Open();
                if(action.actionType == "DELETE")
                {
                    SqlCommand cmd = new SqlCommand("insert into LOPTINCHI(NIENKHOA , HOCKY , MAMH, NHOM,MAGV,MAKHOA,SOSVTOITHIEU,HUYLOP)" +
                        " values(@nienkhoa , @hocky, @mamh, @nhom , @magv, @makhoa, @sosvtoithieu, @huylop)", conn);
                    
                    cmd.Parameters.AddWithValue("@nienkhoa", action.nienKhoa);
                    cmd.Parameters.AddWithValue("@hocky", action.hocKy);
                    cmd.Parameters.AddWithValue("@mamh", action.monHoc);
                    cmd.Parameters.AddWithValue("@nhom", action.nhom);
                    cmd.Parameters.AddWithValue("@magv", action.maGV);
                    cmd.Parameters.AddWithValue("@makhoa", action.maKhoa);
                    cmd.Parameters.AddWithValue("@sosvtoithieu", action.soSV);
                    cmd.Parameters.AddWithValue("@huylop", action.trangThai);
                    cmd.ExecuteNonQuery();
                }
                else if(action.actionType == "INSERT")
                {
                    SqlCommand cmd = new SqlCommand("delete from LOPTINCHI where MALTC = @maltc",conn);
                    cmd.Parameters.AddWithValue("@maltc", action.maLTC);
                    cmd.ExecuteNonQuery();
                }
                else if(action.actionType == "UPDATE")
                {
                    SqlCommand cmd = new SqlCommand("update LOPTINCHI set NIENKHOA = @nienkhoa, HOCKY = @hocky, MAMH = @mamh ," +
                        " NHOM = @nhom, MAGV = @magv, MAKHOA = @makhoa,SOSVTOITHIEU = @sosvtoithieu, HUYLOP = @huylop where MALTC = @maltc",conn);
                    cmd.Parameters.AddWithValue("@maltc", action.maLTC);
                    cmd.Parameters.AddWithValue("@nienkhoa", action.nienKhoa);
                    cmd.Parameters.AddWithValue("@hocky", action.hocKy);
                    cmd.Parameters.AddWithValue("@mamh", action.monHoc);
                    cmd.Parameters.AddWithValue("@nhom", action.nhom);
                    cmd.Parameters.AddWithValue("@magv", action.maGV);
                    cmd.Parameters.AddWithValue("@makhoa", action.maKhoa);
                    cmd.Parameters.AddWithValue("@sosvtoithieu", action.soSV);
                    cmd.Parameters.AddWithValue("@huylop", action.trangThai);
                    cmd.ExecuteNonQuery();
                    
                }
                loadData();
            }catch(Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message);
            }
            finally
            {
                conn.Close();
                btnDelete.Enabled = false;
            }
        }

        private void txtSoSV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNhom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
