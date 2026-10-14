namespace Bai2
{
    partial class Form1
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
            this.txtMaPhieu = new System.Windows.Forms.TextBox();
            this.txtNguoiYeuCau = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpNgayGhiNhan = new System.Windows.Forms.DateTimePicker();
            this.radThap = new System.Windows.Forms.RadioButton();
            this.radTrungBinh = new System.Windows.Forms.RadioButton();
            this.radKhanCap = new System.Windows.Forms.RadioButton();
            this.cboLoaiSuCo = new System.Windows.Forms.ComboBox();
            this.chkMayTInh = new System.Windows.Forms.CheckBox();
            this.chkLapTop = new System.Windows.Forms.CheckBox();
            this.chkMayIn = new System.Windows.Forms.CheckBox();
            this.chkDienThoai = new System.Windows.Forms.CheckBox();
            this.picAnhLoi = new System.Windows.Forms.PictureBox();
            this.btnTaiAnhLoi = new System.Windows.Forms.Button();
            this.btnGuiYeuCau = new System.Windows.Forms.Button();
            this.btnNhapLai = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.picAnhLoi)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMaPhieu
            // 
            this.txtMaPhieu.Location = new System.Drawing.Point(207, 30);
            this.txtMaPhieu.Name = "txtMaPhieu";
            this.txtMaPhieu.Size = new System.Drawing.Size(247, 22);
            this.txtMaPhieu.TabIndex = 0;
            // 
            // txtNguoiYeuCau
            // 
            this.txtNguoiYeuCau.Location = new System.Drawing.Point(204, 78);
            this.txtNguoiYeuCau.Name = "txtNguoiYeuCau";
            this.txtNguoiYeuCau.Size = new System.Drawing.Size(250, 22);
            this.txtNguoiYeuCau.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mã Phiếu :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Người Yêu Cầu :";
            // 
            // dtpNgayGhiNhan
            // 
            this.dtpNgayGhiNhan.Location = new System.Drawing.Point(204, 124);
            this.dtpNgayGhiNhan.Name = "dtpNgayGhiNhan";
            this.dtpNgayGhiNhan.Size = new System.Drawing.Size(250, 22);
            this.dtpNgayGhiNhan.TabIndex = 4;
            // 
            // radThap
            // 
            this.radThap.AutoSize = true;
            this.radThap.Location = new System.Drawing.Point(204, 173);
            this.radThap.Name = "radThap";
            this.radThap.Size = new System.Drawing.Size(60, 20);
            this.radThap.TabIndex = 5;
            this.radThap.TabStop = true;
            this.radThap.Text = "Thấp";
            this.radThap.UseVisualStyleBackColor = true;
            // 
            // radTrungBinh
            // 
            this.radTrungBinh.AutoSize = true;
            this.radTrungBinh.Location = new System.Drawing.Point(270, 173);
            this.radTrungBinh.Name = "radTrungBinh";
            this.radTrungBinh.Size = new System.Drawing.Size(92, 20);
            this.radTrungBinh.TabIndex = 6;
            this.radTrungBinh.TabStop = true;
            this.radTrungBinh.Text = "Trung Bình";
            this.radTrungBinh.UseVisualStyleBackColor = true;
            // 
            // radKhanCap
            // 
            this.radKhanCap.AutoSize = true;
            this.radKhanCap.Location = new System.Drawing.Point(368, 173);
            this.radKhanCap.Name = "radKhanCap";
            this.radKhanCap.Size = new System.Drawing.Size(86, 20);
            this.radKhanCap.TabIndex = 7;
            this.radKhanCap.TabStop = true;
            this.radKhanCap.Text = "Khẩn Cấp";
            this.radKhanCap.UseVisualStyleBackColor = true;
            // 
            // cboLoaiSuCo
            // 
            this.cboLoaiSuCo.FormattingEnabled = true;
            this.cboLoaiSuCo.Items.AddRange(new object[] {
            "Phần Cứng",
            "Phần Mền",
            "Mạng",
            "Tài Khoản"});
            this.cboLoaiSuCo.Location = new System.Drawing.Point(45, 246);
            this.cboLoaiSuCo.Name = "cboLoaiSuCo";
            this.cboLoaiSuCo.Size = new System.Drawing.Size(153, 24);
            this.cboLoaiSuCo.TabIndex = 8;
            // 
            // chkMayTInh
            // 
            this.chkMayTInh.AutoSize = true;
            this.chkMayTInh.Location = new System.Drawing.Point(20, 33);
            this.chkMayTInh.Name = "chkMayTInh";
            this.chkMayTInh.Size = new System.Drawing.Size(84, 20);
            this.chkMayTInh.TabIndex = 9;
            this.chkMayTInh.Text = "Máy Tính";
            this.chkMayTInh.UseVisualStyleBackColor = true;
            // 
            // chkLapTop
            // 
            this.chkLapTop.AutoSize = true;
            this.chkLapTop.Location = new System.Drawing.Point(20, 71);
            this.chkLapTop.Name = "chkLapTop";
            this.chkLapTop.Size = new System.Drawing.Size(77, 20);
            this.chkLapTop.TabIndex = 10;
            this.chkLapTop.Text = "LapTop";
            this.chkLapTop.UseVisualStyleBackColor = true;
            // 
            // chkMayIn
            // 
            this.chkMayIn.AutoSize = true;
            this.chkMayIn.Location = new System.Drawing.Point(136, 33);
            this.chkMayIn.Name = "chkMayIn";
            this.chkMayIn.Size = new System.Drawing.Size(68, 20);
            this.chkMayIn.TabIndex = 11;
            this.chkMayIn.Text = "Máy in";
            this.chkMayIn.UseVisualStyleBackColor = true;
            // 
            // chkDienThoai
            // 
            this.chkDienThoai.AutoSize = true;
            this.chkDienThoai.Location = new System.Drawing.Point(136, 71);
            this.chkDienThoai.Name = "chkDienThoai";
            this.chkDienThoai.Size = new System.Drawing.Size(94, 20);
            this.chkDienThoai.TabIndex = 12;
            this.chkDienThoai.Text = "Điện Thoại";
            this.chkDienThoai.UseVisualStyleBackColor = true;
            // 
            // picAnhLoi
            // 
            this.picAnhLoi.Location = new System.Drawing.Point(48, 347);
            this.picAnhLoi.Name = "picAnhLoi";
            this.picAnhLoi.Size = new System.Drawing.Size(245, 120);
            this.picAnhLoi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAnhLoi.TabIndex = 13;
            this.picAnhLoi.TabStop = false;
            // 
            // btnTaiAnhLoi
            // 
            this.btnTaiAnhLoi.Location = new System.Drawing.Point(348, 347);
            this.btnTaiAnhLoi.Name = "btnTaiAnhLoi";
            this.btnTaiAnhLoi.Size = new System.Drawing.Size(106, 34);
            this.btnTaiAnhLoi.TabIndex = 14;
            this.btnTaiAnhLoi.Text = "Tải Ảnh Lỗi";
            this.btnTaiAnhLoi.UseVisualStyleBackColor = true;
            this.btnTaiAnhLoi.Click += new System.EventHandler(this.btnTaiAnhLoi_Click);
            // 
            // btnGuiYeuCau
            // 
            this.btnGuiYeuCau.Location = new System.Drawing.Point(348, 393);
            this.btnGuiYeuCau.Name = "btnGuiYeuCau";
            this.btnGuiYeuCau.Size = new System.Drawing.Size(106, 34);
            this.btnGuiYeuCau.TabIndex = 15;
            this.btnGuiYeuCau.Text = "Gửi yêu cầu";
            this.btnGuiYeuCau.UseVisualStyleBackColor = true;
            this.btnGuiYeuCau.Click += new System.EventHandler(this.btnGuiYeuCau_Click);
            // 
            // btnNhapLai
            // 
            this.btnNhapLai.Location = new System.Drawing.Point(348, 433);
            this.btnNhapLai.Name = "btnNhapLai";
            this.btnNhapLai.Size = new System.Drawing.Size(106, 34);
            this.btnNhapLai.TabIndex = 16;
            this.btnNhapLai.Text = "Nhập lại";
            this.btnNhapLai.UseVisualStyleBackColor = true;
            this.btnNhapLai.Click += new System.EventHandler(this.btnNhapLai_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "Ngày Yêu Cầu :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = "Mức độ ưu tiên :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "Loại sự cố :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkLapTop);
            this.groupBox1.Controls.Add(this.chkMayTInh);
            this.groupBox1.Controls.Add(this.chkMayIn);
            this.groupBox1.Controls.Add(this.chkDienThoai);
            this.groupBox1.Location = new System.Drawing.Point(207, 215);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 114);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thiết bị ảnh hưởng :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 507);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnNhapLai);
            this.Controls.Add(this.btnGuiYeuCau);
            this.Controls.Add(this.btnTaiAnhLoi);
            this.Controls.Add(this.picAnhLoi);
            this.Controls.Add(this.cboLoaiSuCo);
            this.Controls.Add(this.radKhanCap);
            this.Controls.Add(this.radTrungBinh);
            this.Controls.Add(this.radThap);
            this.Controls.Add(this.dtpNgayGhiNhan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNguoiYeuCau);
            this.Controls.Add(this.txtMaPhieu);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tiếp nhập & Phân loại sự cố ";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picAnhLoi)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMaPhieu;
        private System.Windows.Forms.TextBox txtNguoiYeuCau;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpNgayGhiNhan;
        private System.Windows.Forms.RadioButton radThap;
        private System.Windows.Forms.RadioButton radTrungBinh;
        private System.Windows.Forms.RadioButton radKhanCap;
        private System.Windows.Forms.ComboBox cboLoaiSuCo;
        private System.Windows.Forms.CheckBox chkMayTInh;
        private System.Windows.Forms.CheckBox chkLapTop;
        private System.Windows.Forms.CheckBox chkMayIn;
        private System.Windows.Forms.CheckBox chkDienThoai;
        private System.Windows.Forms.PictureBox picAnhLoi;
        private System.Windows.Forms.Button btnTaiAnhLoi;
        private System.Windows.Forms.Button btnGuiYeuCau;
        private System.Windows.Forms.Button btnNhapLai;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

