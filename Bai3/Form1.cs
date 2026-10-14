using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Cài đặt ComboBox Đơn vị tính
            cboDonViTinh.Items.Clear();
            cboDonViTinh.Items.AddRange(new string[] { "Cái", "Bộ", "Kg", "Mét" });
            cboDonViTinh.SelectedIndex = 0;

            // Cấu hình ListView
            lvwVatTu.View = View.Details;
            lvwVatTu.FullRowSelect = true;
            lvwVatTu.GridLines = true;

            // Thêm các cột cho ListView nếu chưa tạo trong Design
            if (lvwVatTu.Columns.Count == 0)
            {
                lvwVatTu.Columns.Add("Mã VT", 100);
                lvwVatTu.Columns.Add("Tên VT", 150);
                lvwVatTu.Columns.Add("Đơn vị tính", 100);
                lvwVatTu.Columns.Add("Đơn giá", 120);
            }
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maVT = txtMaVT.Text.Trim();
            string tenVT = txtTenVT.Text.Trim();
            string donViTinh = cboDonViTinh.SelectedItem?.ToString();
            string strDonGia = txtDonGiaNhap.Text.Trim();

            // Kiểm tra để trống
            if (string.IsNullOrEmpty(maVT) || string.IsNullOrEmpty(tenVT) || string.IsNullOrEmpty(strDonGia))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra đơn giá hợp lệ
            if (!decimal.TryParse(strDonGia, out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá nhập phải là số hợp lệ >= 0!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDonGiaNhap.Focus();
                return;
            }

            // Validate: Không cho phép thêm nếu Mã VT đã tồn tại
            foreach (ListViewItem item in lvwVatTu.Items)
            {
                if (item.Text.Equals(maVT, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Mã vật tư đã tồn tại trong danh sách!", "Lỗi trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaVT.Focus();
                    return;
                }
            }

            // Thêm dòng mới vào ListView
            ListViewItem newItem = new ListViewItem(maVT);
            newItem.SubItems.Add(tenVT);
            newItem.SubItems.Add(donViTinh);
            newItem.SubItems.Add(donGia.ToString("N0"));

            lvwVatTu.Items.Add(newItem);
            LamMoiForm();
        }





        private void lvwVatTu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwVatTu.SelectedItems.Count > 0)
            {
                ListViewItem item = lvwVatTu.SelectedItems[0];
                txtMaVT.Text = item.Text;
                txtTenVT.Text = item.SubItems[1].Text;
                cboDonViTinh.SelectedItem = item.SubItems[2].Text;
                // Loại bỏ dấu phẩy phân cách nghìn khi nạp lại vào ô nhập
                txtDonGiaNhap.Text = item.SubItems[3].Text.Replace(",", "").Replace(".", "");

                // Không cho sửa Mã VT khi đang chọn để cập nhật
                txtMaVT.Enabled = false;
            }
        }

        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            if (lvwVatTu.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần cập nhật từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenVT = txtTenVT.Text.Trim();
            string donViTinh = cboDonViTinh.SelectedItem?.ToString();
            string strDonGia = txtDonGiaNhap.Text.Trim();

            if (string.IsNullOrEmpty(tenVT) || string.IsNullOrEmpty(strDonGia))
            {
                MessageBox.Show("Vui lòng không để trống thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(strDonGia, out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá nhập không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Cập nhật lại các cột của dòng đang chọn
            ListViewItem item = lvwVatTu.SelectedItems[0];
            item.SubItems[1].Text = tenVT;
            item.SubItems[2].Text = donViTinh;
            item.SubItems[3].Text = donGia.ToString("N0");

            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LamMoiForm();
        }

        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (lvwVatTu.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng muốn xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa vật tư này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                lvwVatTu.Items.Remove(lvwVatTu.SelectedItems[0]);
                LamMoiForm();
            }
        }

        private void btnXoaToanBo_Click(object sender, EventArgs e)
        {
            if (lvwVatTu.Items.Count == 0) return;

            DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa TẤT CẢ danh mục vật tư?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                lvwVatTu.Items.Clear();
                LamMoiForm();
            }
        }

        private void LamMoiForm()
        {
            txtMaVT.Clear();
            txtTenVT.Clear();
            txtDonGiaNhap.Clear();
            if (cboDonViTinh.Items.Count > 0)
                cboDonViTinh.SelectedIndex = 0;

            txtMaVT.Enabled = true; // Mở lại quyền nhập mã
            txtMaVT.Focus();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
