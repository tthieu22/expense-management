using System;
using System.Data;
using System.Windows.Forms;

namespace ExpenseManagement.Views
{
    public partial class BudgetChatItem : UserControl
    {
        private int _id;
        public event Action<int, string> OnActionClick;

        public BudgetChatItem(DataTable budget)
        {
            InitializeComponent();

            if (budget.Rows.Count > 0)
            {
                DataRow row = budget.Rows[0];

                lbStartDate.Text = "Ngày bắt đầu: " + Convert.ToDateTime(row["start_date"]).ToString("dd/MM/yyyy");
                lbEndDtate.Text = "Ngày kết thúc: " + Convert.ToDateTime(row["end_date"]).ToString("dd/MM/yyyy");
                lbTotal.Text = Convert.ToDecimal(row["amount"]).ToString("N0") + " VND";

                _id = Convert.ToInt32(row["budget_id"]);
            }

            plEdit.Height = 0;
            plEdit.Visible = false;
            plEdit.AutoSize = true;

            lbStartDate.Click += BudgetChatItem_Click;
            lbEndDtate.Click += BudgetChatItem_Click;
            lbCategory.Click += BudgetChatItem_Click;
            lbTotal.Click += BudgetChatItem_Click;
            this.Click += BudgetChatItem_Click;
        }

        private void ToggleEditPanel()
        {
            if (plEdit.Visible)
            {
                plEdit.Height = 0;
                plEdit.AutoSize = true;
                plEdit.Visible = false;
            }
            else
            {
                plEdit.Height = 40;
                plEdit.AutoSize = false;
                plEdit.Visible = true;
            }
        }

        private void BudgetChatItem_Click(object sender, EventArgs e)
        {
            ToggleEditPanel();
        }

        private void textDelete_Click(object sender, EventArgs e)
        {
            OnActionClick?.Invoke(_id, "delete");
        }

        private void picDelete_Click(object sender, EventArgs e)
        {
            OnActionClick?.Invoke(_id, "delete");
        }

        private void textEdit_Click(object sender, EventArgs e)
        {
            OnActionClick?.Invoke(_id, "edit");
        }

        private void picEdit_Click(object sender, EventArgs e)
        {
            OnActionClick?.Invoke(_id, "edit");
        }
    }
}
