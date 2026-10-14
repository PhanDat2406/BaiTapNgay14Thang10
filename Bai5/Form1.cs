using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai5
{
    public partial class Form1 : Form
    {
        // Biến cờ ngăn vòng lặp sự kiện khi cập nhật giá trị ô tự động
        private bool isCalculating = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 1. Cập nhật đồng hồ thời gian thực ngay lập tức
            CapNhatThoiGian();

            // 2. Thiết lập cấu hình mặc định cho các ComboBox vận chuyển
            cboPhuongThuc.SelectedIndex = 1; // Mặc định: Giao hàng nhanh (24h)
            cboDonViVC.SelectedIndex = 0;    // Mặc định: GHTK
            cboHinhThucTT.SelectedIndex = 0;  // Mặc định: COD

            // 3. Tạo mã đơn hàng ban đầu
            SinhMaDonHang();

            // 4. Nạp dữ liệu khách hàng mẫu
            txtTenKH.Text = "Nguyễn Văn An";
            txtSDT.Text = "0988123456";
            txtDiaChi.Text = "Số 15, Đường Cầu Giấy, Phường Quan Hoa, Quận Cầu Giấy, Hà Nội";
            dtpNgayGiao.Value = DateTime.Now.AddDays(1);
            txtGhiChu.Text = "Gọi điện trước khi giao 15 phút. Giao giờ hành chính.";

            // 5. Nạp danh mục hàng hóa mẫu vào DataGridView
            NapDuLieuMauHangHoa();

            // 6. Cập nhật thống kê ban đầu lên StatusStrip
            CapNhatThongKeStatusStrip();
        }

        #region Xử lý Thời gian & Mã đơn hàng
        private void timerClock_Tick(object sender, EventArgs e)
        {
            CapNhatThoiGian();
        }

        private void CapNhatThoiGian()
        {
            lblStatusTime.Text = $"🕒 {DateTime.Now:HH:mm:ss - dd/MM/yyyy}";
        }

        private void btnTaoMa_Click(object sender, EventArgs e)
        {
            SinhMaDonHang();
        }

        private void SinhMaDonHang()
        {
            Random rnd = new Random();
            txtMaDonHang.Text = $"DH{DateTime.Now:yyMMdd}-{rnd.Next(100, 999)}";
        }
        #endregion

        #region Nạp dữ liệu mẫu
        private void NapDuLieuMauHangHoa()
        {
            dgvChiTiet.Rows.Clear();

            // Thêm các sản phẩm mẫu để test giao diện và tính toán
            ThemDongSanPham("Chuột máy tính không dây Logitech M331", 2, 0.25, 320000, "Màu đen");
            ThemDongSanPham("Bàn phím cơ DareU EK87 Tenkeyless", 1, 0.85, 650000, "Switch Red");
            ThemDongSanPham("Tai nghe Gaming chụp tai Havit H2002D", 1, 0.40, 480000, "Có micro");
            ThemDongSanPham("Lót chuột gaming cỡ lớn 80x30cm", 2, 0.35, 120000, "Bo viền");

            DanhSoThuTu();
        }

        private void ThemDongSanPham(string tenHang, int soLuong, double trongLuong, decimal donGia, string ghiChu)
        {
            decimal thanhTien = soLuong * donGia;
            int rowIndex = dgvChiTiet.Rows.Add(
                dgvChiTiet.Rows.Count + 1,
                tenHang,
                soLuong,
                trongLuong.ToString("0.00"),
                donGia.ToString("N0"),
                thanhTien.ToString("N0"),
                ghiChu
            );
        }
        #endregion

        #region Tính toán tự động và Cập nhật StatusStrip
        /// <summary>
        /// Kích hoạt commit dữ liệu ngay khi ô cell chuyển sang trạng thái bẩn
        /// giúp sự kiện CellValueChanged kích hoạt tức thì mà không cần click sang ô khác.
        /// </summary>
        private void dgvChiTiet_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvChiTiet.IsCurrentCellDirty)
            {
                dgvChiTiet.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        /// <summary>
        /// Tính toán lại Thành tiền khi Số lượng hoặc Đơn giá thay đổi
        /// và cập nhật thống kê lên StatusStrip.
        /// </summary>
        private void dgvChiTiet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isCalculating || e.RowIndex < 0 || e.RowIndex >= dgvChiTiet.Rows.Count)
                return;

            try
            {
                isCalculating = true;

                // Nếu người dùng thay đổi cột Số lượng hoặc Đơn giá -> Tự động tính lại Thành tiền của dòng đó
                if (e.ColumnIndex == colSoLuong.Index || e.ColumnIndex == colDonGia.Index)
                {
                    DataGridViewRow row = dgvChiTiet.Rows[e.RowIndex];

                    double soLuong = LayGiaTriDouble(row.Cells[colSoLuong.Index].Value);
                    decimal donGia = LayGiaTriDecimal(row.Cells[colDonGia.Index].Value);

                    decimal thanhTien = (decimal)soLuong * donGia;
                    row.Cells[colThanhTien.Index].Value = thanhTien.ToString("N0");
                }
            }
            finally
            {
                isCalculating = false;
            }

            // Cập nhật lại tổng số lượng, tổng trọng lượng và tổng tiền lên StatusStrip
            CapNhatThongKeStatusStrip();
        }

        /// <summary>
        /// Duyệt qua DataGridView để tính toán:
        /// - Tổng số lượng
        /// - Tổng trọng lượng (kg)
        /// - Tổng tiền hàng (VNĐ)
        /// </summary>
        private void CapNhatThongKeStatusStrip()
        {
            int tongSoLuong = 0;
            double tongTrongLuong = 0.0;
            decimal tongTienHang = 0m;

            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                if (row.IsNewRow) continue;

                // 1. Tính tổng số lượng
                int sl = (int)LayGiaTriDouble(row.Cells[colSoLuong.Index].Value);
                if (sl > 0) tongSoLuong += sl;

                // 2. Tính tổng trọng lượng
                double tl = LayGiaTriDouble(row.Cells[colTrongLuong.Index].Value);
                if (tl > 0) tongTrongLuong += tl;

                // 3. Tính tổng tiền
                decimal tt = LayGiaTriDecimal(row.Cells[colThanhTien.Index].Value);
                tongTienHang += tt;
            }

            // Hiển thị trực tiếp lên thanh trạng thái StatusStrip
            lblTongSoLuong.Text = $"📦 Tổng số lượng: {tongSoLuong:N0}";
            lblTongTrongLuong.Text = $"⚖ Tổng trọng lượng: {tongTrongLuong:F2} kg";
            lblTongTien.Text = $"💰 Tổng tiền: {tongTienHang:N0} VNĐ";
        }

        private void DanhSoThuTu()
        {
            for (int i = 0; i < dgvChiTiet.Rows.Count; i++)
            {
                if (!dgvChiTiet.Rows[i].IsNewRow)
                {
                    dgvChiTiet.Rows[i].Cells[colSTT.Index].Value = (i + 1).ToString();
                }
            }
        }

        private void dgvChiTiet_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DanhSoThuTu();
            CapNhatThongKeStatusStrip();
        }

        private void dgvChiTiet_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            DanhSoThuTu();
            CapNhatThongKeStatusStrip();
            KiemTraLoiToanBang();
        }
        #endregion

        #region Kiểm tra dữ liệu (Validation) & ErrorProvider
        /// <summary>
        /// Kiểm tra tính hợp lệ trên từng ô của DataGridView:
        /// - Số lượng <= 0: Hiển thị icon cảnh báo đỏ
        /// - Trọng lượng <= 0: Hiển thị icon cảnh báo đỏ
        /// </summary>
        private void dgvChiTiet_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvChiTiet.Rows.Count)
                return;

            DataGridViewCell cell = dgvChiTiet.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string valueStr = e.FormattedValue?.ToString().Trim();

            // 1. Kiểm tra cột Số lượng (Phải là số nguyên > 0)
            if (e.ColumnIndex == colSoLuong.Index)
            {
                if (string.IsNullOrEmpty(valueStr) || !int.TryParse(valueStr, out int sl) || sl <= 0)
                {
                    string msg = "Số lượng hàng hóa phải là số nguyên lớn hơn 0 (> 0)!";
                    cell.ErrorText = msg;
                    errorProvider1.SetError(dgvChiTiet, $"[Dòng {e.RowIndex + 1}] {msg}");
                }
                else
                {
                    cell.ErrorText = string.Empty;
                    KiemTraLoiToanBang();
                }
            }
            // 2. Kiểm tra cột Trọng lượng (Phải là số thực > 0)
            else if (e.ColumnIndex == colTrongLuong.Index)
            {
                // Thay thế dấu phẩy bằng dấu chấm hoặc hỗ trợ cả 2 chuẩn định dạng
                string normalized = valueStr?.Replace(',', '.');
                if (string.IsNullOrEmpty(valueStr) || 
                    !double.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out double tl) || 
                    tl <= 0)
                {
                    string msg = "Trọng lượng phải là số lớn hơn 0 (kg)!";
                    cell.ErrorText = msg;
                    errorProvider1.SetError(dgvChiTiet, $"[Dòng {e.RowIndex + 1}] {msg}");
                }
                else
                {
                    cell.ErrorText = string.Empty;
                    KiemTraLoiToanBang();
                }
            }
            // 3. Kiểm tra cột Đơn giá (Không được âm)
            else if (e.ColumnIndex == colDonGia.Index)
            {
                string cleanNumber = valueStr?.Replace(",", "").Replace(".", "");
                if (string.IsNullOrEmpty(valueStr) || !decimal.TryParse(cleanNumber, out decimal dg) || dg < 0)
                {
                    string msg = "Đơn giá phải là số không âm (>= 0)!";
                    cell.ErrorText = msg;
                    errorProvider1.SetError(dgvChiTiet, $"[Dòng {e.RowIndex + 1}] {msg}");
                }
                else
                {
                    cell.ErrorText = string.Empty;
                    KiemTraLoiToanBang();
                }
            }
        }

        /// <summary>
        /// Quét toàn bảng xem còn ô nào lỗi không. Nếu hết lỗi thì xóa cảnh báo của errorProvider1.
        /// </summary>
        private void KiemTraLoiToanBang()
        {
            bool coLoi = false;
            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                if (row.IsNewRow) continue;
                foreach (DataGridViewCell c in row.Cells)
                {
                    if (!string.IsNullOrEmpty(c.ErrorText))
                    {
                        coLoi = true;
                        errorProvider1.SetError(dgvChiTiet, $"Dòng {row.Index + 1}: {c.ErrorText}");
                        return;
                    }
                }
            }

            if (!coLoi)
            {
                errorProvider1.SetError(dgvChiTiet, string.Empty);
            }
        }

        // Bắt lỗi rỗng cho Tên khách hàng bằng ErrorProvider
        private void txtTenKH_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenKH.Text))
            {
                errorProvider1.SetError(txtTenKH, "Tên khách hàng không được để trống!");
            }
            else
            {
                errorProvider1.SetError(txtTenKH, string.Empty);
            }
        }

        // Bắt lỗi số điện thoại bằng ErrorProvider
        private void txtSDT_Validating(object sender, CancelEventArgs e)
        {
            string sdt = txtSDT.Text.Trim();
            if (string.IsNullOrWhiteSpace(sdt))
            {
                errorProvider1.SetError(txtSDT, "Số điện thoại không được để trống!");
            }
            else if (!Regex.IsMatch(sdt, @"^(0|\+84)[0-9]{9,10}$"))
            {
                errorProvider1.SetError(txtSDT, "Số điện thoại không hợp lệ! (Ví dụ: 0988123456)");
            }
            else
            {
                errorProvider1.SetError(txtSDT, string.Empty);
            }
        }
        #endregion

        #region Thao tác Hàng hóa & Phím tắt (F2, Delete)
        /// <summary>
        /// Xử lý phím tắt trên toàn Form:
        /// - F2: Thêm nhanh dòng mới vào DataGridView
        /// - Delete: Xóa dòng đang chọn
        /// </summary>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Phím tắt F2: Thêm nhanh dòng mới
            if (e.KeyCode == Keys.F2)
            {
                ThemDongMoi();
                e.Handled = true;
            }
            // Phím tắt Delete: Xóa dòng đang chọn (khi không trong chế độ sửa text của cell)
            else if (e.KeyCode == Keys.Delete)
            {
                // Chỉ xóa dòng nếu con trỏ không đang gõ chữ trong ô
                if (!dgvChiTiet.IsCurrentCellInEditMode)
                {
                    XoaDongDangChon();
                    e.Handled = true;
                }
            }
        }

        private void btnThemHang_Click(object sender, EventArgs e)
        {
            ThemDongMoi();
        }

        private void ThemDongMoi()
        {
            int stt = dgvChiTiet.Rows.Count + 1;
            int newRowIndex = dgvChiTiet.Rows.Add(
                stt,
                $"Hàng mới {stt}",
                1,
                "0.50",
                "50,000",
                "50,000",
                ""
            );

            // Focus vào ô Tên hàng của dòng mới và cho phép sửa ngay
            dgvChiTiet.ClearSelection();
            dgvChiTiet.Rows[newRowIndex].Selected = true;
            dgvChiTiet.CurrentCell = dgvChiTiet.Rows[newRowIndex].Cells[colTenHang.Index];
            dgvChiTiet.BeginEdit(true);

            CapNhatThongKeStatusStrip();
        }

        private void btnXoaHang_Click(object sender, EventArgs e)
        {
            XoaDongDangChon();
        }

        private void XoaDongDangChon()
        {
            if (dgvChiTiet.CurrentRow == null || dgvChiTiet.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Vui lòng chọn một dòng hàng hóa cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int rowIndex = dgvChiTiet.CurrentRow.Index;
            string tenHang = dgvChiTiet.Rows[rowIndex].Cells[colTenHang.Index].Value?.ToString();

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa mặt hàng:\n\"{tenHang}\" (Dòng {rowIndex + 1}) khỏi đơn hàng?",
                "Xác nhận xóa hàng",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                dgvChiTiet.Rows.RemoveAt(rowIndex);
                DanhSoThuTu();
                CapNhatThongKeStatusStrip();
                KiemTraLoiToanBang();
            }
        }

        private void btnXoaTatCa_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Danh sách hàng hóa hiện đang trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa TOÀN BỘ danh sách hàng hóa trong đơn?",
                "Xác nhận xóa tất cả",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                dgvChiTiet.Rows.Clear();
                errorProvider1.SetError(dgvChiTiet, string.Empty);
                CapNhatThongKeStatusStrip();
            }
        }
        #endregion

        #region Tác vụ Vận chuyển & Các nút bấm
        private void cboPhuongThuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tự động điều chỉnh cước phí vận chuyển mẫu theo hình thức
            switch (cboPhuongThuc.SelectedIndex)
            {
                case 0: // Tiêu chuẩn 3-5 ngày
                    txtPhiVanChuyen.Text = "25,000";
                    break;
                case 1: // Nhanh 24h
                    txtPhiVanChuyen.Text = "35,000";
                    break;
                case 2: // Hỏa tốc 2h
                    txtPhiVanChuyen.Text = "60,000";
                    break;
                case 3: // Tiết kiệm
                    txtPhiVanChuyen.Text = "18,000";
                    break;
                default:
                    txtPhiVanChuyen.Text = "30,000";
                    break;
            }
        }

        private void btnTaoDonMoi_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(
                "Bạn có muốn làm mới toàn bộ thông tin để tạo đơn giao hàng mới không?",
                "Tạo đơn mới",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (res == DialogResult.Yes)
            {
                SinhMaDonHang();
                txtTenKH.Clear();
                txtSDT.Clear();
                txtDiaChi.Clear();
                txtGhiChu.Clear();
                dtpNgayGiao.Value = DateTime.Now.AddDays(1);

                cboPhuongThuc.SelectedIndex = 1;
                cboDonViVC.SelectedIndex = 0;
                cboHinhThucTT.SelectedIndex = 0;
                radNguoiNhanTra.Checked = true;
                chkKiemHang.Checked = true;
                chkHangDeVo.Checked = false;
                chkGiaoHenGio.Checked = false;

                dgvChiTiet.Rows.Clear();
                errorProvider1.Clear();
                CapNhatThongKeStatusStrip();

                tabThongTin.SelectedIndex = 0;
                txtTenKH.Focus();
            }
        }

        private void btnLuuDonHang_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra thông tin bắt buộc
            if (string.IsNullOrWhiteSpace(txtTenKH.Text))
            {
                errorProvider1.SetError(txtTenKH, "Vui lòng nhập tên khách hàng!");
                tabThongTin.SelectedIndex = 0;
                txtTenKH.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                errorProvider1.SetError(txtSDT, "Vui lòng nhập số điện thoại!");
                tabThongTin.SelectedIndex = 0;
                txtSDT.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ nhận hàng!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabThongTin.SelectedIndex = 0;
                txtDiaChi.Focus();
                return;
            }

            // 2. Kiểm tra danh mục hàng hóa
            if (dgvChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Đơn hàng phải có ít nhất 1 mặt hàng! Nhấn [F2] để thêm nhanh mặt hàng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Kiểm tra xem có ô nào vi phạm số lượng <= 0 hoặc trọng lượng <= 0 không
            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                if (row.IsNewRow) continue;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!string.IsNullOrEmpty(cell.ErrorText))
                    {
                        MessageBox.Show(
                            $"Dòng {row.Index + 1} đang có dữ liệu không hợp lệ:\n{cell.ErrorText}\nVui lòng sửa trước khi lưu đơn!",
                            "Lỗi dữ liệu",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        dgvChiTiet.CurrentCell = cell;
                        return;
                    }
                }
            }

            // Tính tổng tiền thanh toán
            decimal tongTienHang = 0m;
            int tongSL = 0;
            double tongTL = 0;

            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                if (row.IsNewRow) continue;
                tongSL += (int)LayGiaTriDouble(row.Cells[colSoLuong.Index].Value);
                tongTL += LayGiaTriDouble(row.Cells[colTrongLuong.Index].Value);
                tongTienHang += LayGiaTriDecimal(row.Cells[colThanhTien.Index].Value);
            }

            decimal phiShip = LayGiaTriDecimal(txtPhiVanChuyen.Text);
            decimal tongThanhToan = radNguoiNhanTra.Checked ? (tongTienHang + phiShip) : tongTienHang;

            string tongKet = 
                $"🎉 LƯU ĐƠN GIAO HÀNG THÀNH CÔNG!\n" +
                $"------------------------------------------------------\n" +
                $"• Mã đơn: {txtMaDonHang.Text}\n" +
                $"• Khách hàng: {txtTenKH.Text}\n" +
                $"• Số điện thoại: {txtSDT.Text}\n" +
                $"• Địa chỉ giao: {txtDiaChi.Text}\n" +
                $"• Đơn vị VC: {cboDonViVC.SelectedItem} ({cboPhuongThuc.SelectedItem})\n" +
                $"• Hẹn giao lúc: {dtpNgayGiao.Value:dd/MM/yyyy HH:mm}\n" +
                $"------------------------------------------------------\n" +
                $"• Tổng số món: {tongSL} món ({dgvChiTiet.Rows.Count} loại mặt hàng)\n" +
                $"• Tổng trọng lượng: {tongTL:F2} kg\n" +
                $"• Tiền hàng: {tongTienHang:N0} VNĐ\n" +
                $"• Cước phí vận chuyển: {phiShip:N0} VNĐ ({(radNguoiNhanTra.Checked ? "Khách trả" : "Shop trả")})\n" +
                $"👉 TỔNG TIỀN THU (COD): {tongThanhToan:N0} VNĐ";

            MessageBox.Show(tongKet, "Thông báo lưu đơn hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Không có mặt hàng nào để in phiếu giao!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=====================================================");
            sb.AppendLine("           PHIẾU GIAO HÀNG TIÊU CHUẨN (DELIVERY NOTE)");
            sb.AppendLine("=====================================================");
            sb.AppendLine($"Mã đơn hàng: {txtMaDonHang.Text}      Ngày in: {DateTime.Now:dd/MM/yyyy HH:mm}");
            sb.AppendLine($"Người nhận: {txtTenKH.Text}         SĐT: {txtSDT.Text}");
            sb.AppendLine($"Địa chỉ nhận: {txtDiaChi.Text}");
            sb.AppendLine($"Đơn vị VC: {cboDonViVC.SelectedItem} | Gói: {cboPhuongThuc.SelectedItem}");
            sb.AppendLine("-----------------------------------------------------");
            sb.AppendLine(string.Format("{0,-4} | {1,-22} | {2,4} | {3,8} | {4,10}", "STT", "Tên Hàng", "SL", "TL(kg)", "T.Tiền"));
            sb.AppendLine("-----------------------------------------------------");

            decimal tongTien = 0m;
            double tongTL = 0;
            int tongSL = 0;

            foreach (DataGridViewRow r in dgvChiTiet.Rows)
            {
                if (r.IsNewRow) continue;
                string stt = r.Cells[colSTT.Index].Value?.ToString() ?? "";
                string ten = r.Cells[colTenHang.Index].Value?.ToString() ?? "";
                if (ten.Length > 20) ten = ten.Substring(0, 19) + ".";

                int sl = (int)LayGiaTriDouble(r.Cells[colSoLuong.Index].Value);
                double tl = LayGiaTriDouble(r.Cells[colTrongLuong.Index].Value);
                decimal tt = LayGiaTriDecimal(r.Cells[colThanhTien.Index].Value);

                tongSL += sl;
                tongTL += tl;
                tongTien += tt;

                sb.AppendLine(string.Format("{0,-4} | {1,-22} | {2,4} | {3,8:F2} | {4,10:N0}", stt, ten, sl, tl, tt));
            }

            sb.AppendLine("-----------------------------------------------------");
            sb.AppendLine($"Tổng SL: {tongSL} | Tổng TL: {tongTL:F2} kg | Tổng tiền: {tongTien:N0} VNĐ");
            sb.AppendLine($"Phí ship: {txtPhiVanChuyen.Text} VNĐ ({(radNguoiNhanTra.Checked ? "Thu khách" : "Shop trả")})");
            sb.AppendLine($"Ghi chú giao: {txtGhiChu.Text}");
            sb.AppendLine("=====================================================");
            sb.AppendLine("              CHỮ KÝ NGƯỜI NHẬN HÀNG");

            MessageBox.Show(sb.ToString(), "Bản in xem trước (Preview)", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Tiện ích hỗ trợ Parsing số
        private double LayGiaTriDouble(object value)
        {
            if (value == null) return 0;
            string s = value.ToString().Trim().Replace(',', '.');
            if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out double val))
            {
                return val;
            }
            return 0;
        }

        private decimal LayGiaTriDecimal(object value)
        {
            if (value == null) return 0m;
            string s = value.ToString().Trim().Replace(",", "").Replace(".", "");
            if (decimal.TryParse(s, out decimal val))
            {
                return val;
            }
            return 0m;
        }
        #endregion
    }
}
