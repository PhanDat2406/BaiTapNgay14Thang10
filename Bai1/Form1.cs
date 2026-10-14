using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai1
{
    public partial class frmServiceChargeCalculator : Form
    {
        public frmServiceChargeCalculator()
        {
            InitializeComponent();
        }

        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtDonGiaDV.Text) ||
               string.IsNullOrWhiteSpace(txtSoLuongKhachHang.Text) ||
               string.IsNullOrWhiteSpace(txtMaGiamGia.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            if (!double.TryParse(txtDonGiaDV.Text.Trim(), out double donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá dịch vụ không hợp lệ(>=0)", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGiaDV.Focus();
                return;
            }
            if (!int.TryParse(txtSoLuongKhachHang.Text.Trim(), out int soLuong) || soLuong < 0)
            {
                MessageBox.Show("Số lượng khách hàng không hợp lệ(>=0)", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuongKhachHang.Focus();
                return;
            }
            if (!double.TryParse(txtMaGiamGia.Text.Trim(), out double maGiamGia) || maGiamGia < 0 || maGiamGia > 100)
            {
                MessageBox.Show("Mã giảm giá không hợp lệ(0%-100%)", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaGiamGia.Focus();
                return;
            }

            double tongTien = (donGia * soLuong) * (100 - maGiamGia) / 100;
            lblTongTienThanhToan.Text = tongTien.ToString("N0") + " VNĐ";
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtDonGiaDV.Clear();
            txtSoLuongKhachHang.Clear();
            txtMaGiamGia.Clear();
            lblTongTienThanhToan.Text = "0 VNĐ";
            txtDonGiaDV.Focus();
        }
    }
}
