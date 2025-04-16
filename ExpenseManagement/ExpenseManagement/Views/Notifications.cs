using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ExpenseManagement.Controller;
using Microsoft.Toolkit.Uwp.Notifications;

namespace ExpenseManagement.Views
{
    public partial class Notifications : Form
    {
        private NotificationController notificationController = new NotificationController();
        private int userId;
        private Timer notificationTimer;

        public Notifications(int userId)
        {
            InitializeComponent();
            this.userId = userId;

            LoadAllNotifications();
            CheckAndShowNotifications();

            notificationTimer = new Timer();
            notificationTimer.Interval = 10000; // 10 giây
            notificationTimer.Tick += (s, e) => CheckAndShowNotifications();
            notificationTimer.Start();
        }

        private void Notifications_Load(object sender, EventArgs e) { }

        private void CheckAndShowNotifications()
        {
            try
            {
                List<string> newNotifications = notificationController.CheckForNewNotifications(userId);

                if (newNotifications.Count > 0)
                {
                    foreach (string message in newNotifications)
                    {
                        ShowToastNotification("Thông báo mới", message);
                    }

                    notificationController.MarkAllAsRead(userId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kiểm tra thông báo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowToastNotification(string title, string message)
        {
            new ToastContentBuilder()
                .AddText(title)
                .AddText(message)
                .Show();
        }

        private void LoadAllNotifications()
        {
            try
            {
                List<Dictionary<string, object>> notifications = notificationController.GetAllNotifications(userId);

                flowLayoutPanelNotifications.Controls.Clear();

                foreach (var notification in notifications)
                {
                    string message = notification["message"].ToString();
                    string status = notification["status"].ToString();
                    DateTime dateCreated = Convert.ToDateTime(notification["date_created"]);

                    NotificationItem item = new NotificationItem(message, status, dateCreated);
                    flowLayoutPanelNotifications.Controls.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải thông báo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Notifications_FormClosing(object sender, FormClosingEventArgs e)
        {

            e.Cancel = true;
            this.Hide();

        }

        private void btnDeleteAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int deletedCount = notificationController.DeleteNotificationsByUserId(userId);

            if (deletedCount > 0)
            {
                LoadAllNotifications();
                MessageBox.Show($"Đã xóa {deletedCount} thông báo.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không có thông báo nào để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
