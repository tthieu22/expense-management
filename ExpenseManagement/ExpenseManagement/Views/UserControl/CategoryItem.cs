using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ExpenseManagement.Views
{
    public partial class CategoryItem : UserControl
    {
        public int CategoryId { get; private set; }
        private Color _defaultBackColor;
        private Color _clickedBackColor = Color.LightBlue;
        private static List<CategoryItem> _allItems = new List<CategoryItem>();

        public event Action<int> CategoryClicked;

        public CategoryItem(int categoryId, string name, string icon, string url, int width)
        {
            InitializeComponent();
            _defaultBackColor = this.BackColor;
            _allItems.Add(this);

            CategoryId = categoryId;
            lbName.Text = name;
            ckSelect.Enabled = true;

            this.MinimumSize = new Size(width, this.Height);
            this.Width = width;
            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    pbIcon.Image = Image.FromFile(url);  
                    pbIcon.Visible = true;
                    iconpicbox.Visible = false;
                }
                catch
                {
                    pbIcon.Visible = false;
                    iconpicbox.Visible = true;
                    iconpicbox.IconChar = IconChar.QuestionCircle;
                }
            }
            else if (!string.IsNullOrEmpty(icon) && Enum.TryParse(icon, out IconChar iconChar))
            {
                iconpicbox.IconChar = iconChar;
                iconpicbox.IconFont = IconFont.Auto;
                iconpicbox.Visible = true;
                pbIcon.Visible = false;
            }
            else
            {
                iconpicbox.IconChar = IconChar.QuestionCircle;
                iconpicbox.IconFont = IconFont.Auto;
                iconpicbox.Visible = true;
                pbIcon.Visible = false;
            }

            this.Click += CategoryItem_Click;
            pbIcon.Click += CategoryItem_Click;
            lbName.Click += CategoryItem_Click;
            pnlContainer.Click += CategoryItem_Click;
            ckSelect.Click += CkSelect_Click;
        }

        private void CategoryItem_Click(object sender, EventArgs e)
        {
            foreach (var item in _allItems)
            {
                item.BackColor = item._defaultBackColor;
                item.ckSelect.Checked = false;
            }

            this.BackColor = _clickedBackColor;
            ckSelect.Checked = true;
            CategoryClicked?.Invoke(CategoryId);
        }

        private void CkSelect_Click(object sender, EventArgs e)
        {
            foreach (var item in _allItems)
            {
                if (item != this)
                {
                    item.ckSelect.Checked = false;
                }
            }
        }

        private void ckSelect_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
