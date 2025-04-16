using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseManagement.Model
{
    public class NotificationModel
    {
        private Connect db = new Connect();

        public List<string> GetUnreadNotifications(int userId)
        {
            List<string> notifications = new List<string>();
            string query = "SELECT [message] FROM [dbo].[Notifications] WHERE [user_id] = @UserId AND [status] = 'Unread' ORDER BY [date_created] DESC";
            SqlParameter[] parameters = { new SqlParameter("@UserId", userId) };
            DataTable dt = db.ExecuteQuery(query, parameters);

            foreach (DataRow row in dt.Rows)
            {
                notifications.Add(row["message"].ToString());
            }
            return notifications;
        }

        public void MarkNotificationsAsRead(int userId)
        {
            string query = "UPDATE [dbo].[Notifications] SET [status] = 'Read' WHERE [user_id] = @UserId AND [status] = 'Unread'";
            SqlParameter[] parameters = { new SqlParameter("@UserId", userId) };
            db.ExecuteNonQuery(query, parameters);
        }

        public List<Dictionary<string, object>> GetAllNotifications(int userId)
        {
            List<Dictionary<string, object>> notifications = new List<Dictionary<string, object>>();
            string query = "SELECT * FROM [dbo].[Notifications] WHERE [user_id] = @UserId ORDER BY [date_created] DESC";
            SqlParameter[] parameters = { new SqlParameter("@UserId", userId) };
            DataTable dt = db.ExecuteQuery(query, parameters);

            foreach (DataRow row in dt.Rows)
            {
                Dictionary<string, object> notification = new Dictionary<string, object>();
                foreach (DataColumn column in dt.Columns)
                {
                    notification[column.ColumnName] = row[column];
                }
                notifications.Add(notification);
            }
            return notifications;
        }

        public List<Dictionary<string, object>> GetReadNotifications(int userId)
        {
            List<Dictionary<string, object>> notifications = new List<Dictionary<string, object>>();
            string query = "SELECT * FROM [dbo].[Notifications] WHERE [user_id] = @UserId AND [status] = 'Read' ORDER BY [date_created] DESC";
            SqlParameter[] parameters = { new SqlParameter("@UserId", userId) };
            DataTable dt = db.ExecuteQuery(query, parameters);

            foreach (DataRow row in dt.Rows)
            {
                Dictionary<string, object> notification = new Dictionary<string, object>();
                foreach (DataColumn column in dt.Columns)
                {
                    notification[column.ColumnName] = row[column];
                }
                notifications.Add(notification);
            }
            return notifications;
        }
        public int DeleteNotificationsByUserId(int userId)
        {
            string query = "DELETE FROM Notifications WHERE user_id = @UserId; SELECT @@ROWCOUNT;";
            SqlParameter[] parameters = { new SqlParameter("@UserId", userId) };

            return (int)db.ExecuteScalar(query, parameters);
        }


        // Hàm mới: Lấy danh sách thông báo của admin
        public DataTable GetAdminNotifications()
        {
            string query = "SELECT * FROM [dbo].[Notifications] WHERE is_admin = 1 ORDER BY [date_created] DESC;";
            return db.ExecuteQuery(query); // Trả về DataTable trực tiếp
        }


        // Hàm mới: Thêm thông báo mới
        public void AddNotification(int userId, string message, bool isAdmin)
        {
            string query = "INSERT INTO Notifications (user_id, message, date_created, status, is_admin) " +
                           "VALUES (@UserId, @Message, GETDATE(), 'Unread', @IsAdmin)";
            SqlParameter[] parameters = {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@Message", message),
                new SqlParameter("@IsAdmin", isAdmin ? 1 : 0)
            };
            db.ExecuteNonQuery(query, parameters);
        }


        // Hàm mới: Xóa thông báo theo ID (chỉ admin mới có quyền)
        public void DeleteNotificationById(int notificationId)
        {
            string query = "DELETE FROM Notifications WHERE notification_id = @NotificationId";
            SqlParameter[] parameters = { new SqlParameter("@NotificationId", notificationId) };
            db.ExecuteNonQuery(query, parameters);
        }

        // Hàm mới: Cập nhật nội dung thông báo
        public void UpdateNotification(int notificationId, string newMessage)
        {
            string query = "UPDATE Notifications SET message = @NewMessage, date_updated = GETDATE() WHERE notification_id = @NotificationId";
            SqlParameter[] parameters = {
                new SqlParameter("@NotificationId", notificationId),
                new SqlParameter("@NewMessage", newMessage)
            };
            db.ExecuteNonQuery(query, parameters);
        }
    }
}
