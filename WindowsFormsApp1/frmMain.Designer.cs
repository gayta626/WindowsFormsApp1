namespace WindowsFormsApp1
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hệThốngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đăngXuấtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thoátToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tạoTàiKhoảnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.danhMụcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lớpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sinhViênToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.sinhVienToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mônHọcToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.đăngKýTínChỉToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mởLớpTínChỉToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đăngKýLớpTínChỉToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.đăngKýLớpTínChỉToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nhậpĐiểmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nhậpĐiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xemĐiểmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inẤnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.danhSáchLớpTinChỉToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dSSinhViênĐăngKýLTCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bảngĐiểmMônToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.phiếuĐiểmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bảngĐiểmTổngKếtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbTitile = new System.Windows.Forms.Label();
            this.lbHelloUser = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Info;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hệThốngToolStripMenuItem,
            this.danhMụcToolStripMenuItem,
            this.đăngKýTínChỉToolStripMenuItem,
            this.nhậpĐiểmToolStripMenuItem,
            this.inẤnToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1330, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // hệThốngToolStripMenuItem
            // 
            this.hệThốngToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.đăngXuấtToolStripMenuItem,
            this.thoátToolStripMenuItem,
            this.tạoTàiKhoảnToolStripMenuItem});
            this.hệThốngToolStripMenuItem.Name = "hệThốngToolStripMenuItem";
            this.hệThốngToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.hệThốngToolStripMenuItem.Text = "Hệ thống";
            // 
            // đăngXuấtToolStripMenuItem
            // 
            this.đăngXuấtToolStripMenuItem.Name = "đăngXuấtToolStripMenuItem";
            this.đăngXuấtToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.đăngXuấtToolStripMenuItem.Text = "Đăng xuất ";
            this.đăngXuấtToolStripMenuItem.Click += new System.EventHandler(this.đăngXuấtToolStripMenuItem_Click);
            // 
            // thoátToolStripMenuItem
            // 
            this.thoátToolStripMenuItem.Name = "thoátToolStripMenuItem";
            this.thoátToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.thoátToolStripMenuItem.Text = "Thoát";
            this.thoátToolStripMenuItem.Click += new System.EventHandler(this.thoátToolStripMenuItem_Click);
            // 
            // tạoTàiKhoảnToolStripMenuItem
            // 
            this.tạoTàiKhoảnToolStripMenuItem.Name = "tạoTàiKhoảnToolStripMenuItem";
            this.tạoTàiKhoảnToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.tạoTàiKhoảnToolStripMenuItem.Text = "Tạo tài khoản";
            this.tạoTàiKhoảnToolStripMenuItem.Click += new System.EventHandler(this.tạoTàiKhoảnToolStripMenuItem_Click);
            // 
            // danhMụcToolStripMenuItem
            // 
            this.danhMụcToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lớpToolStripMenuItem,
            this.sinhViênToolStripMenuItem,
            this.sinhVienToolStripMenuItem,
            this.toolStripMenuItem1,
            this.mônHọcToolStripMenuItem1});
            this.danhMụcToolStripMenuItem.Name = "danhMụcToolStripMenuItem";
            this.danhMụcToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.danhMụcToolStripMenuItem.Text = "Danh mục";
            // 
            // lớpToolStripMenuItem
            // 
            this.lớpToolStripMenuItem.Name = "lớpToolStripMenuItem";
            this.lớpToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.lớpToolStripMenuItem.Text = "Lớp";
            this.lớpToolStripMenuItem.Click += new System.EventHandler(this.lớpToolStripMenuItem_Click);
            // 
            // sinhViênToolStripMenuItem
            // 
            this.sinhViênToolStripMenuItem.Name = "sinhViênToolStripMenuItem";
            this.sinhViênToolStripMenuItem.Size = new System.Drawing.Size(119, 6);
            // 
            // sinhVienToolStripMenuItem
            // 
            this.sinhVienToolStripMenuItem.Name = "sinhVienToolStripMenuItem";
            this.sinhVienToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.sinhVienToolStripMenuItem.Text = "Sinh viên";
            this.sinhVienToolStripMenuItem.Click += new System.EventHandler(this.sinhVienToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(119, 6);
            // 
            // mônHọcToolStripMenuItem1
            // 
            this.mônHọcToolStripMenuItem1.Name = "mônHọcToolStripMenuItem1";
            this.mônHọcToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.mônHọcToolStripMenuItem1.Text = "Môn học";
            this.mônHọcToolStripMenuItem1.Click += new System.EventHandler(this.mônHọcToolStripMenuItem1_Click);
            // 
            // đăngKýTínChỉToolStripMenuItem
            // 
            this.đăngKýTínChỉToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mởLớpTínChỉToolStripMenuItem,
            this.đăngKýLớpTínChỉToolStripMenuItem,
            this.đăngKýLớpTínChỉToolStripMenuItem1});
            this.đăngKýTínChỉToolStripMenuItem.ForeColor = System.Drawing.SystemColors.InfoText;
            this.đăngKýTínChỉToolStripMenuItem.Name = "đăngKýTínChỉToolStripMenuItem";
            this.đăngKýTínChỉToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.đăngKýTínChỉToolStripMenuItem.Text = "Đăng ký tín chỉ";
            // 
            // mởLớpTínChỉToolStripMenuItem
            // 
            this.mởLớpTínChỉToolStripMenuItem.Name = "mởLớpTínChỉToolStripMenuItem";
            this.mởLớpTínChỉToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.mởLớpTínChỉToolStripMenuItem.Text = "Mở lớp tín chỉ";
            this.mởLớpTínChỉToolStripMenuItem.Click += new System.EventHandler(this.mởLớpTínChỉToolStripMenuItem_Click);
            // 
            // đăngKýLớpTínChỉToolStripMenuItem
            // 
            this.đăngKýLớpTínChỉToolStripMenuItem.Name = "đăngKýLớpTínChỉToolStripMenuItem";
            this.đăngKýLớpTínChỉToolStripMenuItem.Size = new System.Drawing.Size(170, 6);
            // 
            // đăngKýLớpTínChỉToolStripMenuItem1
            // 
            this.đăngKýLớpTínChỉToolStripMenuItem1.Name = "đăngKýLớpTínChỉToolStripMenuItem1";
            this.đăngKýLớpTínChỉToolStripMenuItem1.Size = new System.Drawing.Size(173, 22);
            this.đăngKýLớpTínChỉToolStripMenuItem1.Text = "Đăng ký lớp tín chỉ";
            this.đăngKýLớpTínChỉToolStripMenuItem1.Click += new System.EventHandler(this.đăngKýLớpTínChỉToolStripMenuItem1_Click);
            // 
            // nhậpĐiểmToolStripMenuItem
            // 
            this.nhậpĐiểmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nhậpĐiToolStripMenuItem,
            this.xemĐiểmToolStripMenuItem});
            this.nhậpĐiểmToolStripMenuItem.ForeColor = System.Drawing.SystemColors.MenuText;
            this.nhậpĐiểmToolStripMenuItem.Name = "nhậpĐiểmToolStripMenuItem";
            this.nhậpĐiểmToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.nhậpĐiểmToolStripMenuItem.Text = "Nhập điểm";
            // 
            // nhậpĐiToolStripMenuItem
            // 
            this.nhậpĐiToolStripMenuItem.Name = "nhậpĐiToolStripMenuItem";
            this.nhậpĐiToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.nhậpĐiToolStripMenuItem.Text = "Nhập điểm";
            this.nhậpĐiToolStripMenuItem.Click += new System.EventHandler(this.nhậpĐiToolStripMenuItem_Click);
            // 
            // xemĐiểmToolStripMenuItem
            // 
            this.xemĐiểmToolStripMenuItem.Name = "xemĐiểmToolStripMenuItem";
            this.xemĐiểmToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.xemĐiểmToolStripMenuItem.Text = "Xem điểm";
            this.xemĐiểmToolStripMenuItem.Click += new System.EventHandler(this.xemĐiểmToolStripMenuItem_Click);
            // 
            // inẤnToolStripMenuItem
            // 
            this.inẤnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.danhSáchLớpTinChỉToolStripMenuItem,
            this.dSSinhViênĐăngKýLTCToolStripMenuItem,
            this.bảngĐiểmMônToolStripMenuItem,
            this.phiếuĐiểmToolStripMenuItem,
            this.bảngĐiểmTổngKếtToolStripMenuItem});
            this.inẤnToolStripMenuItem.Name = "inẤnToolStripMenuItem";
            this.inẤnToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.inẤnToolStripMenuItem.Text = "In  ấn";
            // 
            // danhSáchLớpTinChỉToolStripMenuItem
            // 
            this.danhSáchLớpTinChỉToolStripMenuItem.Name = "danhSáchLớpTinChỉToolStripMenuItem";
            this.danhSáchLớpTinChỉToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.danhSáchLớpTinChỉToolStripMenuItem.Text = "Danh sách lớp tin chỉ";
            this.danhSáchLớpTinChỉToolStripMenuItem.Click += new System.EventHandler(this.danhSáchLớpTinChỉToolStripMenuItem_Click);
            // 
            // dSSinhViênĐăngKýLTCToolStripMenuItem
            // 
            this.dSSinhViênĐăngKýLTCToolStripMenuItem.Name = "dSSinhViênĐăngKýLTCToolStripMenuItem";
            this.dSSinhViênĐăngKýLTCToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.dSSinhViênĐăngKýLTCToolStripMenuItem.Text = "DS sinh viên đăng ký LTC";
            this.dSSinhViênĐăngKýLTCToolStripMenuItem.Click += new System.EventHandler(this.dSSinhViênĐăngKýLTCToolStripMenuItem_Click);
            // 
            // bảngĐiểmMônToolStripMenuItem
            // 
            this.bảngĐiểmMônToolStripMenuItem.Name = "bảngĐiểmMônToolStripMenuItem";
            this.bảngĐiểmMônToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.bảngĐiểmMônToolStripMenuItem.Text = "Bảng điểm môn";
            this.bảngĐiểmMônToolStripMenuItem.Click += new System.EventHandler(this.bảngĐiểmMônToolStripMenuItem_Click);
            // 
            // phiếuĐiểmToolStripMenuItem
            // 
            this.phiếuĐiểmToolStripMenuItem.Name = "phiếuĐiểmToolStripMenuItem";
            this.phiếuĐiểmToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.phiếuĐiểmToolStripMenuItem.Text = "Phiếu điểm";
            // 
            // bảngĐiểmTổngKếtToolStripMenuItem
            // 
            this.bảngĐiểmTổngKếtToolStripMenuItem.Name = "bảngĐiểmTổngKếtToolStripMenuItem";
            this.bảngĐiểmTổngKếtToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.bảngĐiểmTổngKếtToolStripMenuItem.Text = "Bảng điểm tổng kết";
            // 
            // lbTitile
            // 
            this.lbTitile.AutoSize = true;
            this.lbTitile.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitile.Location = new System.Drawing.Point(323, 670);
            this.lbTitile.Name = "lbTitile";
            this.lbTitile.Size = new System.Drawing.Size(339, 24);
            this.lbTitile.TabIndex = 2;
            this.lbTitile.Text = "HỆ THỐNG QUẢN LÝ LỚP TÍN CHỈ";
            // 
            // lbHelloUser
            // 
            this.lbHelloUser.AutoSize = true;
            this.lbHelloUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHelloUser.Location = new System.Drawing.Point(971, 8);
            this.lbHelloUser.Name = "lbHelloUser";
            this.lbHelloUser.Size = new System.Drawing.Size(66, 16);
            this.lbHelloUser.TabIndex = 3;
            this.lbHelloUser.Text = "Xin chào";
            this.lbHelloUser.Click += new System.EventHandler(this.lbHelloUser_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1330, 703);
            this.Controls.Add(this.lbHelloUser);
            this.Controls.Add(this.lbTitile);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.InfoText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "HỆ THỐNG QUẢN LÝ ĐIỂM SINH VIÊN THEO HỆ TÍN CHỈ";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hệThốngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đăngXuấtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thoátToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem danhMụcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lớpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sinhVienToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đăngKýTínChỉToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mởLớpTínChỉToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nhậpĐiểmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nhậpĐiToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator sinhViênToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mônHọcToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator đăngKýLớpTínChỉToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đăngKýLớpTínChỉToolStripMenuItem1;
        private System.Windows.Forms.Label lbTitile;
        private System.Windows.Forms.Label lbHelloUser;
        private System.Windows.Forms.ToolStripMenuItem xemĐiểmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tạoTàiKhoảnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inẤnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem danhSáchLớpTinChỉToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dSSinhViênĐăngKýLTCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bảngĐiểmMônToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem phiếuĐiểmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bảngĐiểmTổngKếtToolStripMenuItem;
    }
}