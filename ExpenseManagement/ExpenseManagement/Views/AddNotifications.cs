using ExpenseManagement.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ExpenseManagement.Views
{
    public partial class AddNotifications : Form
    {
        private NotificationController _notifications = new NotificationController();
        private int _user_id;
        private int _notification_id = -1; // Lưu ID thông báo đang được chọn

        public AddNotifications(int userID)
        {
            InitializeComponent();
            _user_id = userID;
            DisplayNotiAdminAdd();
        }

      
        // Thêm thông báo mới
        private void btnAddCat_Click(object sender, EventArgs e)
        {
            string message = tbMessage.Text.Trim();
            if (string.IsNullOrEmpty(message))
            {
                MessageBox.Show("Vui lòng nhập nội dung thông báo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _notifications.AddNotification(_user_id, message, true);
            MessageBox.Show("Thêm thông báo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DisplayNotiAdminAdd(); // Cập nhật danh sách thông báo
            tbMessage.Clear();
        }


        // Cập nhật nội dung thông báo
        private void btnUpdateCat_Click(object sender, EventArgs e)
        {
            if (_notification_id == -1)
            {
                MessageBox.Show("Vui lòng chọn thông báo cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newMessage = tbMessage.Text.Trim();
            if (string.IsNullOrEmpty(newMessage))
            {
                MessageBox.Show("Vui lòng nhập nội dung mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _notifications.UpdateNotification(_notification_id, newMessage);
            MessageBox.Show("Cập nhật thông báo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DisplayNotiAdminAdd();
            tbMessage.Clear();
            _notification_id = -1; // Reset ID sau khi cập nhật
        }

        // Xóa thông báo
        private void btnDeleteCat_Click(object sender, EventArgs e)
        {
            if (_notification_id == -1)
            {
                MessageBox.Show("Vui lòng chọn thông báo cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa thông báo này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                _notifications.DeleteNotificationById(_notification_id);
                MessageBox.Show("Xóa thông báo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayNotiAdminAdd();
                tbMessage.Clear();
                _notification_id = -1; // Reset ID sau khi xóa
            }
        }

        // Sự kiện khi chọn một dòng trong DataGridView
        private void dataNoti_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Kiểm tra hàng và cột hợp lệ
            {
                // Lấy ô dữ liệu
                var cellNotificationId = dataNoti.Rows[e.RowIndex].Cells["notification_id"].Value;
                var cellMessage = dataNoti.Rows[e.RowIndex].Cells["message"].Value;

                // Kiểm tra nếu ô dữ liệu không rỗng
                if (cellNotificationId != null && cellMessage != null)
                {
                    _notification_id = Convert.ToInt32(cellNotificationId);
                    tbMessage.Text = cellMessage.ToString();
                }
            }
        }

        // Hiển thị danh sách thông báo của Admin
        public void DisplayNotiAdminAdd()
        {
            var notifications = _notifications.GetAdminNotifications();
            dataNoti.DataSource = notifications;

            // Đổi tên cột hiển thị
            dataNoti.Columns["user_id"].HeaderText = "Mã Người Dùng";
            dataNoti.Columns["message"].HeaderText = "Nội Dung";
            dataNoti.Columns["date_created"].HeaderText = "Ngày Tạo";

            // Ẩn các cột không cần thiết
            dataNoti.Columns["notification_id"].Visible = false;
            dataNoti.Columns["is_admin"].Visible = false;
            dataNoti.Columns["status"].Visible = false;

            // Căn chỉnh DataGridView cho full width
            dataNoti.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataNoti.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataNoti.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // Cho phép xuống dòng nếu quá dài
        }

        private void dataNoti_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
