using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExpenseManagement.Views
{
    public partial class NotificationItem : UserControl
    {
        public NotificationItem(string message, string status, DateTime dateCreated)
        {
            InitializeComponent();
            lblMessage.Text = message;
            lblStatus.Text = status == "Unread" ? "🔴 Chưa đọc" : "✔️ Đã đọc";
            lblDate.Text = dateCreated.ToString("dd/MM/yyyy HH:mm");

            if (status == "Unread")
            {
                this.BackColor = Color.LightYellow;
            }
            else
            {
                this.BackColor = Color.White;
            }
        }
    }
}
