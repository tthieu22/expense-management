using System;
using System.Windows.Forms;

namespace ExpenseManagement.Views
{
    public partial class MessageItem : UserControl
    {
        public event Action<string> MessageClicked; 

        private string _message;

        public MessageItem(string text, string message)
        {
            InitializeComponent();
            lblText.Text = text;
            _message = message;
            this.Click += MessageItem_Click;
            lblText.Click += MessageItem_Click;
        }

        private void MessageItem_Click(object sender, EventArgs e)
        {
            MessageClicked?.Invoke(_message);
        }
    }
}
