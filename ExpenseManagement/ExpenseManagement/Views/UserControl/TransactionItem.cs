using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExpenseManagement.Views
{
    public partial class TransactionItem : UserControl
    {
        public event Action<int, string, string> OnActionClick;

        private int _id;
        private PictureBox boxImg;
        private string _type;
        public int TransactionId { get; private set; }

        public TransactionItem(int id, string category, string total, string type, string desc, string date, string icon, string imageUrl = "")
        {
            TransactionId = id;
            InitializeComponent();
            _id = id;
            _type = type.ToLower();

            lbCategory.Text = category ?? "";
            lbCategory.Visible = !string.IsNullOrEmpty(category);
            lbDate.Text = date ?? "";
            lbDate.Visible = !string.IsNullOrEmpty(date);
            FormatTotal(total, type);
            lbTotal.Visible = !string.IsNullOrEmpty(total);
            lbTotal.ForeColor = _type == "income" ? Color.Green : Color.Red;
            lbDesc.Text = desc ?? "";
            lbDesc.Visible = !string.IsNullOrEmpty(desc);
            pnDesc.Height = string.IsNullOrEmpty(desc) ? 0 : 20;

            plEdit.Height = 0;
            plEdit.Visible = false;
            plEdit.AutoSize = true;

            boxImg = new PictureBox
            {
                Size = new Size(32, 32),
                Location = iconA.Location,
                SizeMode = PictureBoxSizeMode.Zoom,
                Visible = false
            };
            Controls.Add(boxImg);

            LoadImageOrIcon(imageUrl, icon);
            RegisterClickEvents();
        }

        private void LoadImageOrIcon(string imageUrl, string icon)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                try
                {
                    boxImg.Load(imageUrl);
                    boxImg.Visible = true;
                    iconA.Visible = false;
                }
                catch
                {
                    ShowDefaultIcon(icon);
                }
            }
            else
            {
                ShowDefaultIcon(icon);
            }
        }

        private void ShowDefaultIcon(string icon)
        {
            iconA.IconChar = Enum.TryParse(icon, true, out IconChar iconChar) ? iconChar : IconChar.QuestionCircle;
            iconA.Visible = true;
            boxImg.Visible = false;
        }

        private void FormatTotal(string total, string type)
        {
            if (decimal.TryParse(total, out decimal amount))
            {
                string formattedAmount = Math.Abs(amount).ToString("#,##0") + "đ";
                lbTotal.Text = (type.ToLower() == "income" ? "+" : "-") + formattedAmount;
            }
            lbTotal.Text = total;
        }

        private void RegisterClickEvents()
        {
            foreach (Control ctrl in new Control[] { this, lbCategory, lbTotal, lbDesc, lbDate, iconA, boxImg })
            {
                ctrl.Click += TransactionItem_Click;
            }
        }

        private void TransactionItem_Click(object sender, EventArgs e)
        {
            if (!plEdit.Visible)
            {
                plEdit.Visible = true;
                plEdit.AutoSize = false;
                plEdit.Height = 40;
            }
            else
            {
                plEdit.Visible = false;
            }
        }
        private void textEdit_Click(object sender, EventArgs e) => OnActionClick?.Invoke(_id, "edit", _type);
        private void textDelete_Click(object sender, EventArgs e) => OnActionClick?.Invoke(_id, "delete", _type);
        private void picEdit_Click(object sender, EventArgs e) => OnActionClick?.Invoke(_id, "edit", _type);
        private void picDelete_Click(object sender, EventArgs e) => OnActionClick?.Invoke(_id, "delete", _type);
    }
}
