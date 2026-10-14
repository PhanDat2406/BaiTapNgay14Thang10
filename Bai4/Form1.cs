using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai4
{
    public partial class Form1 : Form
    {
        private readonly Color MAU_TRONG = Color.LightGray;      // Trắng / Xám nhạt: Trống
        private readonly Color MAU_DANG_CHON = Color.LightGreen; // Xanh lá: Đang chọn
        private readonly Color MAU_DA_DAT = Color.Red;           // Đỏ: Đã có người đặt / Đã khóa

        // Danh sách lưu trữ các Button vị trí
        private List<Button> dsButtonViTri = new List<Button>();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // 1. Cài đặt ComboBox khung giờ
            cboKhungGio.Items.Clear();
            cboKhungGio.Items.Add("Sáng (100.000đ)");
            cboKhungGio.Items.Add("Tối (150.000đ)");
            cboKhungGio.SelectedIndex = 0; // Mặc định chọn ca Sáng

            // 2. Khởi tạo động 20 Button vị trí (ma trận 4 x 5)
            KhoiTaoSoDoViTri();

            // 3. Cập nhật thống kê ban đầu
            CapNhatThongKe();
        }
        private void tlpSoDo_Paint(object sender, PaintEventArgs e)
        {

        }
        // Tự động sinh 20 Button bằng vòng lặp for
        private void KhoiTaoSoDoViTri()
        {
            tlpSoDo.Controls.Clear();
            dsButtonViTri.Clear();

            for (int i = 1; i <= 20; i++)
            {
                Button btn = new Button();
                btn.Name = "btnViTri_" + i;
                btn.Text = "Bàn " + i;
                btn.Width = 85;
                btn.Height = 60;
                btn.Margin = new Padding(6);
                btn.BackColor = MAU_TRONG;
                btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                // Giả lập sẵn 1-2 vị trí đã được đặt trước (màu Đỏ)
                if (i == 3 || i == 7)
                {
                    btn.BackColor = MAU_DA_DAT;
                    btn.ForeColor = Color.White;
                }

                // Gán chung 1 hàm xử lý sự kiện Click cho toàn bộ 20 nút
                btn.Click += ViTri_Click;

                dsButtonViTri.Add(btn);
                tlpSoDo.Controls.Add(btn);
            }
        }

        // Hàm xử lý sự kiện dùng chung (Event Aggregation) cho tất cả các Button
        private void ViTri_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            // Nếu vị trí đã bị khóa/đã đặt -> Báo bận và không cho chọn
            if (btn.BackColor == MAU_DA_DAT)
            {
                MessageBox.Show("Vị trí này đã có người đặt, vui lòng chọn vị trí khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Đổi trạng thái qua lại giữa Trống và Đang chọn
            if (btn.BackColor == MAU_TRONG)
            {
                btn.BackColor = MAU_DANG_CHON; // Chuyển sang Xanh lá
            }
            else if (btn.BackColor == MAU_DANG_CHON)
            {
                btn.BackColor = MAU_TRONG;     // Bỏ chọn -> Quay về Xám nhạt
            }

            // Cập nhật lại số lượng và tiền realtime
            CapNhatThongKe();
        }
        // Lấy đơn giá theo khung giờ đang chọn
        private decimal LayDonGiaKhungGio()
        {
            // Ca Sáng: 100.000đ, Ca Tối: 150.000đ
            if (cboKhungGio.SelectedIndex == 1)
                return 150000m;
            return 100000m;
        }

        // Khi thay đổi khung giờ -> Tính lại tiền realtime
        private void cboKhungGio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatThongKe();
        }

        // Hàm cập nhật số vị trí và tạm tính tiền
        private void CapNhatThongKe()
        {
            int soLuongChon = 0;
            foreach (Button btn in dsButtonViTri)
            {
                if (btn.BackColor == MAU_DANG_CHON)
                {
                    soLuongChon++;
                }
            }

            decimal donGia = LayDonGiaKhungGio();
            decimal tongTien = soLuongChon * donGia;

            lblSoViTri.Text = $"Số vị trí đang chọn: {soLuongChon}";
            lblTamTinh.Text = $"Tạm tính tiền: {tongTien:N0} VNĐ";
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            List<string> viTriChon = new List<string>();

            foreach (Button btn in dsButtonViTri)
            {
                if (btn.BackColor == MAU_DANG_CHON)
                {
                    viTriChon.Add(btn.Text);
                }
            }

            if (viTriChon.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một vị trí trước khi xác nhận!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string danhSach = string.Join(", ", viTriChon);
            decimal tongTien = viTriChon.Count * LayDonGiaKhungGio();

            string thongBao = $"Đặt bàn thành công!\n\n" +
                              $"- Khung giờ: {cboKhungGio.SelectedItem}\n" +
                              $"- Danh sách vị trí: {danhSach}\n" +
                              $"- Tổng số: {viTriChon.Count} bàn\n" +
                              $"- Tổng tiền: {tongTien:N0} VNĐ";

            MessageBox.Show(thongBao, "Xác nhận đặt chỗ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Chuyển toàn bộ các nút vừa chọn sang trạng thái Đã khóa (Màu đỏ)
            foreach (Button btn in dsButtonViTri)
            {
                if (btn.BackColor == MAU_DANG_CHON)
                {
                    btn.BackColor = MAU_DA_DAT;
                    btn.ForeColor = Color.White;
                }
            }

            CapNhatThongKe();
        }

        private void lblSoViTri_Click(object sender, EventArgs e)
        {

        }

        private void btnHuyChon_Click(object sender, EventArgs e)
        {
            foreach (Button btn in dsButtonViTri)
            {
                // Chỉ hủy những nút đang được người dùng chọn (màu xanh lá)
                if (btn.BackColor == MAU_DANG_CHON)
                {
                    btn.BackColor = MAU_TRONG;
                }
            }

            CapNhatThongKe();
        }
    }
}
