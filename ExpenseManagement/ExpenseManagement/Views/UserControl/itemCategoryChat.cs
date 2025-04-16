using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseManagement.Views
{
    public partial class itemCategoryChat : UserControl
    {
        public event Action<int, string> OnActionClick;
        private int _id;
        public itemCategoryChat(DataTable data)
        {
            InitializeComponent();
            if (data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                _id = Convert.ToInt32(row["category_id"]);

                lbCategoryName.Text = row["category_name"].ToString();
                string categoryType = row["category_type"].ToString();
                lbType.Text = categoryType;
                string iconChar = row["category_icon_char"].ToString();
                ShowDefaultIcon(iconChar);
            }

            this.Click += TransactionItem_Click;
            lbCategoryName.Click += TransactionItem_Click;
            lbType.Click += TransactionItem_Click;
            iconChar.Click += TransactionItem_Click;
        }

        private void ShowDefaultIcon(string icon)
        {
            if (!string.IsNullOrEmpty(icon) && Enum.TryParse(icon, true, out IconChar iconChar))
            {
                this.iconChar.IconChar = iconChar;
            }
            else
            {
                this.iconChar.IconChar = IconChar.QuestionCircle;
            }
            this.iconChar.Visible = true;
        }

        private void TransactionItem_Click(object sender, EventArgs e)
        {
            plEdit.Visible = !plEdit.Visible;
            plEdit.AutoSize = false;
            plEdit.Height = plEdit.Visible ? 40 : 0;
        }


        private void textEdit_Click(object sender, EventArgs e) => OnActionClick?.Invoke(_id, "edit");

        private void textDelete_Click(object sender, EventArgs e) => OnActionClick?.Invoke(_id, "delete");

        private void picEdit_Click(object sender, EventArgs e) => OnActionClick?.Invoke(_id, "edit");

        private void picDelete_Click(object sender, EventArgs e) => OnActionClick?.Invoke(_id, "delete");
    }
}
