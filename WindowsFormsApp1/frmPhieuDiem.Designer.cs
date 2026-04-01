namespace WindowsFormsApp1
{
    partial class frmPhieuDiem
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
            this.dgvPhieuDiem = new System.Windows.Forms.DataGridView();
            this.lbXinChao = new System.Windows.Forms.Label();
            this.cbNienKhoa = new System.Windows.Forms.ComboBox();
            this.cbHocKy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGPA = new System.Windows.Forms.TextBox();
            this.btnFilter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuDiem)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPhieuDiem
            // 
            this.dgvPhieuDiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhieuDiem.Location = new System.Drawing.Point(2, 117);
            this.dgvPhieuDiem.Name = "dgvPhieuDiem";
            this.dgvPhieuDiem.Size = new System.Drawing.Size(847, 328);
            this.dgvPhieuDiem.TabIndex = 2;
            // 
            // lbXinChao
            // 
            this.lbXinChao.AutoSize = true;
            this.lbXinChao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbXinChao.Location = new System.Drawing.Point(12, 9);
            this.lbXinChao.Name = "lbXinChao";
            this.lbXinChao.Size = new System.Drawing.Size(66, 16);
            this.lbXinChao.TabIndex = 3;
            this.lbXinChao.Text = "Xin chào";
            // 
            // cbNienKhoa
            // 
            this.cbNienKhoa.FormattingEnabled = true;
            this.cbNienKhoa.Location = new System.Drawing.Point(70, 93);
            this.cbNienKhoa.Name = "cbNienKhoa";
            this.cbNienKhoa.Size = new System.Drawing.Size(121, 21);
            this.cbNienKhoa.TabIndex = 4;
            // 
            // cbHocKy
            // 
            this.cbHocKy.FormattingEnabled = true;
            this.cbHocKy.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cbHocKy.Location = new System.Drawing.Point(264, 93);
            this.cbHocKy.Name = "cbHocKy";
            this.cbHocKy.Size = new System.Drawing.Size(121, 21);
            this.cbHocKy.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(-1, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Niên khóa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(211, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Học kỳ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(-1, 448);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "GPA";
            // 
            // txtGPA
            // 
            this.txtGPA.Location = new System.Drawing.Point(37, 445);
            this.txtGPA.Name = "txtGPA";
            this.txtGPA.Size = new System.Drawing.Size(46, 20);
            this.txtGPA.TabIndex = 9;
            // 
            // btnFilter
            // 
            this.btnFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.Location = new System.Drawing.Point(406, 91);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 10;
            this.btnFilter.Text = "Lọc";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // frmPhieuDiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 519);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.txtGPA);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbHocKy);
            this.Controls.Add(this.cbNienKhoa);
            this.Controls.Add(this.lbXinChao);
            this.Controls.Add(this.dgvPhieuDiem);
            this.Name = "frmPhieuDiem";
            this.Text = "frmPhieuDiem";
            this.Load += new System.EventHandler(this.frmPhieuDiem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuDiem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvPhieuDiem;
        private System.Windows.Forms.Label lbXinChao;
        private System.Windows.Forms.ComboBox cbNienKhoa;
        private System.Windows.Forms.ComboBox cbHocKy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGPA;
        private System.Windows.Forms.Button btnFilter;
    }
}