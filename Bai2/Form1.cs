using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai2
{
    public partial class Form1 : Form
    {
        private string duongDanAnh = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboLoaiSuCo.Items.Clear();
            cboLoaiSuCo.Items.AddRange(new string[] { "Sự cố phần mềm", "Sự cố phần cứng", "Sự cố mạng" });
            cboLoaiSuCo.SelectedIndex = 0;

            radThap.Checked = true;

            picAnhLoi.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnTaiAnhLoi_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    duongDanAnh = openFileDialog.FileName;
                    picAnhLoi.Image = Image.FromFile(duongDanAnh);
                }
            }
        }

        private void btnGuiYeuCau_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaPhieu.Text) || string.IsNullOrWhiteSpace(txtNguoiYeuCau.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ Mã phiếu và Người yêu cầu!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mucDo = "Thấp";
            if (radTrungBinh.Checked) mucDo = "Trung bình";
            else if (radKhanCap.Checked) mucDo = "Khẩn cấp";

            List<string> thietBi = new List<string>();
            if (chkMayTInh.Checked) thietBi.Add("Máy tính bàn");
            if (chkLapTop.Checked) thietBi.Add("Laptop");
            if (chkMayIn.Checked) thietBi.Add("Máy in");
            if (chkDienThoai.Checked) thietBi.Add("Điện thoại");

            string danhSachThietBi = thietBi.Count > 0 ? string.Join(", ", thietBi) : "Không có";

            string noiDung = "=== THÔNG TIN PHIẾU YÊU CẦU IT ===\n\n" +
                             $"- Mã phiếu: {txtMaPhieu.Text.Trim()}\n" +
                             $"- Người yêu cầu: {txtNguoiYeuCau.Text.Trim()}\n" +
                             $"- Ngày ghi nhận: {dtpNgayGhiNhan.Value.ToString("dd/MM/yyyy")}\n" +
                             $"- Mức độ ưu tiên: {mucDo}\n" +
                             $"- Loại sự cố: {cboLoaiSuCo.SelectedItem?.ToString()}\n" +
                             $"- Thiết bị ảnh hưởng: {danhSachThietBi}\n" +
                             $"- Ảnh chụp lỗi: {(string.IsNullOrEmpty(duongDanAnh) ? "Chưa có ảnh đính kèm" : duongDanAnh)}";

            MessageBox.Show(noiDung, "Xác nhận gửi thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            txtMaPhieu.Clear();
            txtNguoiYeuCau.Clear();

            // Đặt lại ngày hiện tại
            dtpNgayGhiNhan.Value = DateTime.Now;

            // Đặt lại lựa chọn mặc định
            radThap.Checked = true;
            if (cboLoaiSuCo.Items.Count > 0)
                cboLoaiSuCo.SelectedIndex = 0;

            // Bỏ tick tất cả CheckBox
            chkMayTInh.Checked = false;
            chkLapTop.Checked = false;
            chkMayIn.Checked = false;
            chkDienThoai.Checked = false;

            // Xóa ảnh
            duongDanAnh = "";
            if (picAnhLoi.Image != null)
            {
                picAnhLoi.Image.Dispose();
                picAnhLoi.Image = null;
            }

            // Đưa con trỏ chuột về ô đầu tiên
            txtMaPhieu.Focus();
        }
    }
}
