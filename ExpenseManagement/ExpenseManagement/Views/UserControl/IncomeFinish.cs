using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseManagement.Views
{
    public partial class IncomeFinish : UserControl
    {
        public event Action<int, string, string> OnActionClick;
        private int _id;

        public IncomeFinish(DataTable data)
        {
            InitializeComponent();

            if (data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                _id = Convert.ToInt32(row["income_id"]);

                lbTotal.Text = Convert.ToDecimal(row["amount"]).ToString("N0") + "đ";
                lbCategory.Text = row["category_name"].ToString();
                lbDesc.Text = row["description"].ToString();

                pnDesc.AutoSize = false;
                pnDesc.Height = 20;

                DateTime expenseDate = Convert.ToDateTime(row["income_date"]);
                lbDate.Text = expenseDate.ToString("yyyy/MM/dd HH:mm");

                string categoryType = row["category_type"].ToString().ToLower();
                lbTotal.ForeColor = categoryType == "income" ? Color.Green : Color.Red;

                string iconChar = row["category_icon_char"].ToString();
                ShowDefaultIcon(iconChar);
            }

            this.Click += TransactionItem_Click;
            lbCategory.Click += TransactionItem_Click;
            lbTotal.Click += TransactionItem_Click;
            lbDesc.Click += TransactionItem_Click;
            lbDate.Click += TransactionItem_Click;
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

        private void textEdit_Click(object sender, EventArgs e) => OnActionClick?.Invoke(_id, "edit", "income");

        private void textDelete_Click(object sender, EventArgs e) => OnActionClick?.Invoke(_id, "delete", "income");

        private void picEdit_Click(object sender, EventArgs e) => OnActionClick?.Invoke(_id, "edit", "income");

        private void picDelete_Click(object sender, EventArgs e) => OnActionClick?.Invoke(_id, "delete","income");
    }
}
