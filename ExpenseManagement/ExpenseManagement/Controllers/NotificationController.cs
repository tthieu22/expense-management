using System.Collections.Generic;
using System.Data;
using ExpenseManagement.Model;

namespace ExpenseManagement.Controller
{
    public class NotificationController
    {
        private NotificationModel notificationModel = new NotificationModel();

        // Lấy danh sách thông báo chưa đọc
        public List<string> CheckForNewNotifications(int userId)
        {
            return notificationModel.GetUnreadNotifications(userId);
        }

        // Đánh dấu tất cả thông báo là đã đọc
        public void MarkAllAsRead(int userId)
        {
            notificationModel.MarkNotificationsAsRead(userId);
        }

        // Lấy danh sách thông báo đã đọc
        public List<Dictionary<string, object>> GetReadNotifications(int userId)
        {
            return notificationModel.GetReadNotifications(userId);
        }

        // Lấy tất cả thông báo của một user
        public List<Dictionary<string, object>> GetAllNotifications(int userId)
        {
            return notificationModel.GetAllNotifications(userId);
        }

        // Xóa tất cả thông báo của một user
        public int DeleteNotificationsByUserId(int userId)
        {
            return notificationModel.DeleteNotificationsByUserId(userId);
        }

        // Lấy danh sách thông báo của Admin
        public DataTable GetAdminNotifications()
        {
            return notificationModel.GetAdminNotifications();
        }

        // Thêm một thông báo mới
        public void AddNotification(int userId, string message, bool isAdmin)
        {
            notificationModel.AddNotification(userId, message, isAdmin);
        }

        // Xóa một thông báo theo ID
        public void DeleteNotificationById(int notificationId)
        {
            notificationModel.DeleteNotificationById(notificationId);
        }

        // Cập nhật nội dung thông báo
        public void UpdateNotification(int notificationId, string newMessage)
        {
            notificationModel.UpdateNotification(notificationId, newMessage);
        }
    }
}
