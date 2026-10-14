namespace Bai1
{
    partial class frmServiceChargeCalculator
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
            this.txtDonGiaDV = new System.Windows.Forms.TextBox();
            this.txtSoLuongKhachHang = new System.Windows.Forms.TextBox();
            this.txtMaGiamGia = new System.Windows.Forms.TextBox();
            this.lblDonGiaDV = new System.Windows.Forms.Label();
            this.lblSoLuongKhachHang = new System.Windows.Forms.Label();
            this.lblMaGiamGIa = new System.Windows.Forms.Label();
            this.lblTongTienThanhToan = new System.Windows.Forms.Label();
            this.btnTinhTien = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDonGiaDV
            // 
            this.txtDonGiaDV.Location = new System.Drawing.Point(217, 49);
            this.txtDonGiaDV.Name = "txtDonGiaDV";
            this.txtDonGiaDV.Size = new System.Drawing.Size(210, 22);
            this.txtDonGiaDV.TabIndex = 1;
            // 
            // txtSoLuongKhachHang
            // 
            this.txtSoLuongKhachHang.Location = new System.Drawing.Point(217, 107);
            this.txtSoLuongKhachHang.Name = "txtSoLuongKhachHang";
            this.txtSoLuongKhachHang.Size = new System.Drawing.Size(210, 22);
            this.txtSoLuongKhachHang.TabIndex = 3;
            // 
            // txtMaGiamGia
            // 
            this.txtMaGiamGia.Location = new System.Drawing.Point(217, 162);
            this.txtMaGiamGia.Name = "txtMaGiamGia";
            this.txtMaGiamGia.Size = new System.Drawing.Size(210, 22);
            this.txtMaGiamGia.TabIndex = 5;
            // 
            // lblDonGiaDV
            // 
            this.lblDonGiaDV.AutoSize = true;
            this.lblDonGiaDV.Location = new System.Drawing.Point(37, 55);
            this.lblDonGiaDV.Name = "lblDonGiaDV";
            this.lblDonGiaDV.Size = new System.Drawing.Size(104, 16);
            this.lblDonGiaDV.TabIndex = 0;
            this.lblDonGiaDV.Text = "Đơn giá dịch vụ :";
            // 
            // lblSoLuongKhachHang
            // 
            this.lblSoLuongKhachHang.AutoSize = true;
            this.lblSoLuongKhachHang.Location = new System.Drawing.Point(37, 110);
            this.lblSoLuongKhachHang.Name = "lblSoLuongKhachHang";
            this.lblSoLuongKhachHang.Size = new System.Drawing.Size(138, 16);
            this.lblSoLuongKhachHang.TabIndex = 2;
            this.lblSoLuongKhachHang.Text = "Số lượng khách hàng :";
            // 
            // lblMaGiamGIa
            // 
            this.lblMaGiamGIa.AutoSize = true;
            this.lblMaGiamGIa.Location = new System.Drawing.Point(37, 165);
            this.lblMaGiamGIa.Name = "lblMaGiamGIa";
            this.lblMaGiamGIa.Size = new System.Drawing.Size(123, 16);
            this.lblMaGiamGIa.TabIndex = 4;
            this.lblMaGiamGIa.Text = "Nhập mã giảm giá :";
            // 
            // lblTongTienThanhToan
            // 
            this.lblTongTienThanhToan.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblTongTienThanhToan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTienThanhToan.Location = new System.Drawing.Point(40, 224);
            this.lblTongTienThanhToan.Name = "lblTongTienThanhToan";
            this.lblTongTienThanhToan.Size = new System.Drawing.Size(387, 53);
            this.lblTongTienThanhToan.TabIndex = 6;
            this.lblTongTienThanhToan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTinhTien
            // 
            this.btnTinhTien.Location = new System.Drawing.Point(40, 307);
            this.btnTinhTien.Name = "btnTinhTien";
            this.btnTinhTien.Size = new System.Drawing.Size(96, 29);
            this.btnTinhTien.TabIndex = 7;
            this.btnTinhTien.Text = "Tính tiền";
            this.btnTinhTien.UseVisualStyleBackColor = true;
            this.btnTinhTien.Click += new System.EventHandler(this.btnTinhTien_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(331, 307);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(96, 29);
            this.btnLamMoi.TabIndex = 8;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // frmServiceChargeCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 384);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnTinhTien);
            this.Controls.Add(this.lblTongTienThanhToan);
            this.Controls.Add(this.lblMaGiamGIa);
            this.Controls.Add(this.lblSoLuongKhachHang);
            this.Controls.Add(this.lblDonGiaDV);
            this.Controls.Add(this.txtMaGiamGia);
            this.Controls.Add(this.txtSoLuongKhachHang);
            this.Controls.Add(this.txtDonGiaDV);
            this.Name = "frmServiceChargeCalculator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Service Charge Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDonGiaDV;
        private System.Windows.Forms.TextBox txtSoLuongKhachHang;
        private System.Windows.Forms.TextBox txtMaGiamGia;
        private System.Windows.Forms.Label lblDonGiaDV;
        private System.Windows.Forms.Label lblSoLuongKhachHang;
        private System.Windows.Forms.Label lblMaGiamGIa;
        private System.Windows.Forms.Label lblTongTienThanhToan;
        private System.Windows.Forms.Button btnTinhTien;
        private System.Windows.Forms.Button btnLamMoi;
    }
}

