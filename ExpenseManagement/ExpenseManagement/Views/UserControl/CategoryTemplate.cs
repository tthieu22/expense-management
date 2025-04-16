using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExpenseManagement.Views
{
    public partial class CategoryTemplate : UserControl
    {
        public int CategoryId { get; private set; }
        public event Action<int> CategoryClicked;

        private Color defaultBackColor;
        private static CategoryTemplate selectedItem = null;

        public CategoryTemplate(int categoryId, string name, string icon, string url, int width)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(icon) && Enum.TryParse(icon, out IconChar iconChar))
            {
                icIcon.IconChar = iconChar;
                icIcon.IconFont = IconFont.Auto;
            }
            else
            {
                icIcon.IconChar = IconChar.QuestionCircle;
            }

            CategoryId = categoryId;
            lbName.Text = name;

            this.Click += CategoryTemplate_Click;
            icIcon.Click += CategoryTemplate_Click;
            lbName.Click += CategoryTemplate_Click;

            this.MinimumSize = new Size(width, this.Height);
            this.Width = width;

            // Lưu lại màu nền mặc định
            defaultBackColor = this.BackColor;
        }

        private void CategoryTemplate_Click(object sender, EventArgs e)
        {
            // Đặt lại màu nền cho item trước đó nếu có
            if (selectedItem != null && selectedItem != this)
            {
                selectedItem.BackColor = selectedItem.defaultBackColor;
            }

            // Đổi màu nền cho item hiện tại
            this.BackColor = Color.LightBlue; // Màu khi chọn
            selectedItem = this;

            // Kích hoạt sự kiện khi click
            CategoryClicked?.Invoke(CategoryId);
        }
    }
}
