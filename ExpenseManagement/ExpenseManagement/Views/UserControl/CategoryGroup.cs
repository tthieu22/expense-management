using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExpenseManagement.Views
{
    public partial class CategoryGroup : UserControl
    {
        public int CategoryId { get; private set; }

        public event Action<int> CategoryClicked;

        public CategoryGroup()
        {
            InitializeComponent();
        }

        public CategoryGroup(string nameGroup, IconChar imgGroup, List<Dictionary<string, object>> categories ,int width)
        {
            InitializeComponent();
            lbNameGroup.Text = nameGroup;
            iconGroup.IconChar = imgGroup;
            
            foreach (var item in categories)
            {
                int categoryId = Convert.ToInt32(item["category_id"]);
                string categoryName = item["category_name"].ToString();
                string iconName = item["category_icon_char"].ToString();
                string iconUrl = item["category_icon_url"].ToString();

                CategoryTemplate categoryTemplate = new CategoryTemplate(categoryId, categoryName, iconName, iconUrl, width);
                categoryTemplate.CategoryClicked += OnCategoryClicked;

                flListCategory.Controls.Add(categoryTemplate);
            }
        }

        private void OnCategoryClicked(int categoryId)
        {
            CategoryClicked?.Invoke(categoryId);
        }

        private void lbNameGroup_Click(object sender, EventArgs e)
        {
        }
    }
}
