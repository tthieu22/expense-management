using AForge.Video;
using CloudinaryDotNet;
using ExpenseManagement.Controller;
using ExpenseManagement.Controllers;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using Windows.System;
using Windows.UI.Xaml.Controls.Primitives;

namespace ExpenseManagement.Views
{
    public partial class Category : Form
    {
        private int _userId;
        private IconButton _selectedIconButton;
        private CloudinaryHelper _cloudinaryHelper;
        CategoryController _categoryController;
        ExpenseIconsLoader _expenseIconLoader;
        private string _filePath;
        private string _iconSelect = null;
        private string _selectExpense = "Expense";
        private int _selectExpenseGroup = 0;
        private int _selectCategory = 0;
        private string _tbName = "";
        private int _categoryId = 0;

        public int CategoryId { get; private set; }
        public Category(int userId,int categoryId)
        {
            InitializeComponent();
            _userId = userId;

             _cloudinaryHelper = new CloudinaryHelper();
             _categoryController = new CategoryController();
        }

        private void Category_Load(object sender, EventArgs e)
        {
            _expenseIconLoader = new ExpenseIconsLoader(plListIcon);
            _expenseIconLoader.LoadIcons(OnIconSelected);

            LoadCategory();
            selectTypeChangeColor();

            LoadCategoryGroupsIntoComboBox();
        }
        private void OnIconSelected(IconChar? selectedIcon)
        {
            if (selectedIcon.HasValue)
            {
                _iconSelect = selectedIcon.Value.ToString();
                Console.WriteLine(_iconSelect);
            }
        }
        public void LoadCategory()
        {
            pfExpense.Controls.Clear();

            var categorizedData = _categoryController.CategorizeByGroup(_userId, "Expense");

            foreach (var group in categorizedData)
            {
                string groupName = group["group_name"].ToString();
                string groupIconName = group["group_icon"].ToString();

                if (!Enum.TryParse(groupIconName, out IconChar groupIcon))
                {
                    groupIcon = IconChar.LayerGroup;
                }

                List<Dictionary<string, object>> categories = (List<Dictionary<string, object>>)group["categories"];

                CategoryGroup categoryGroup = new CategoryGroup(groupName, groupIcon, categories, 500);
                categoryGroup.CategoryClicked += OnCategoryClicked;
                pfExpense.Controls.Add(categoryGroup);
            }

            pfIncome.Controls.Clear();

            var categorizedDataIncome = _categoryController.CategorizeByGroup(_userId, "Income");

            foreach (var group in categorizedDataIncome)
            {
                string groupName = group["group_name"].ToString();
                string groupIconName = group["group_icon"].ToString();

                if (!Enum.TryParse(groupIconName, out IconChar groupIcon))
                {
                    groupIcon = IconChar.LayerGroup;
                }

                List<Dictionary<string, object>> categories = (List<Dictionary<string, object>>)group["categories"];

                CategoryGroup categoryGroup = new CategoryGroup(groupName, groupIcon, categories, 500);
                categoryGroup.CategoryClicked += OnCategoryClicked;
                pfIncome.Controls.Add(categoryGroup);
            }
        }
        private void OnCategoryClicked(int categoryId)
        {
            _categoryId = categoryId;
            DataTable categorizedData = _categoryController.GetCategoryById(_userId, _categoryId);

            if (categorizedData.Rows.Count > 0)
            {
                DataRow row = categorizedData.Rows[0];

                tbName.Text = row["category_name"].ToString();

                if (int.TryParse(row["category_group_id"].ToString(), out int categoryGroupId))
                {
                    _selectExpenseGroup = categoryGroupId;
                }

                foreach (var item in cbGroup.Items)
                {
                    if (item.GetType().GetProperty("Value")?.GetValue(item) is int value && value == _selectExpenseGroup)
                    {
                        cbGroup.SelectedItem = item;
                        break;
                    }
                }

                if (row["category_icon_char"] != DBNull.Value)
                {
                    _iconSelect = row["category_icon_char"].ToString();
                    if (Enum.TryParse(row["category_icon_char"].ToString(), out FontAwesome.Sharp.IconChar iconChar))
                    {
                        OnIconSelected(iconChar);
                        _expenseIconLoader.SelectIconByName(iconChar.ToString());
                    }
                    else
                    {
                        _selectedIconButton = null;
                    }
                }
                else if (row["category_icon_url"] != DBNull.Value) {
                    picUpload.SizeMode = PictureBoxSizeMode.Zoom;
                    picUpload.ImageLocation = row["category_icon_url"].ToString();
                    _filePath = row["category_icon_url"].ToString();
                }
                else
                {
                    picUpload.Image = null;
                }

                if (row["category_type"].ToString() == "Expense")
                {
                    _selectExpense = "Expense";
                    selectTypeChangeColor();
                }else
                {
                    _selectExpense = "Income";
                    selectTypeChangeColor();
                }
            }
        }


        private void picUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.ico";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _filePath = openFileDialog.FileName;
                    picUpload.SizeMode = PictureBoxSizeMode.Zoom;
                    picUpload.ImageLocation = _filePath;
                }

            }

        }


        private void picUpdloadImage_Click(object sender, EventArgs e)
        {
           
        }


        private void plAddNew_Paint(object sender, PaintEventArgs e)
        {

        }
        private void LoadCategoryGroupsIntoComboBox()
        {
            DataTable categoryGroupsData = _categoryController.GetAllCategoriesGroup();
            cbGroup.Items.Clear();

            var defaultItem = new ComboBoxItem { Text = "Select Category Group", Value = 0 };
            cbGroup.Items.Add(defaultItem);

            foreach (DataRow row in categoryGroupsData.Rows)
            {
                cbGroup.Items.Add(new
                {
                    Text = row["group_name"].ToString(),
                    Value = Convert.ToInt32(row["category_group_id"])
                });
            }
            cbGroup.DisplayMember = "Text";
            cbGroup.ValueMember = "Value";

            cbGroup.SelectedItem = defaultItem;

        }
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            string imageUrl = null;
            bool result;
            if (_filePath != null)
            {
                btnAdd.Text = "Adding";
                imageUrl = await _cloudinaryHelper.UploadImageAsync(_filePath);
                if (imageUrl != null)
                {
                    btnAdd.Text = "Add";
                }
            }
            else if (_iconSelect == null)
            {
                MessageBox.Show("Please select one icon or image!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                btnAdd.Text = "Add";
                MessageBox.Show($"Selected icon: {_iconSelect}", "Icon Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            _tbName = tbName.Text;
            if ( _tbName !="" || _selectExpenseGroup == 0 )
            {
                if (_iconSelect != null)
                {

                    result = _categoryController.AddCategory(_userId, _tbName, _selectExpense, _selectExpenseGroup, _iconSelect, "");
                    if(result)
                    {
                        tbName.Text = "";
                        _iconSelect = null;
                        LoadCategory();

                    } else
                    {
                        MessageBox.Show("Error create category");
                    }
                }
                else if (imageUrl != null)
                {
                    result = _categoryController.AddCategory(_userId, _tbName, _selectExpense, _selectExpenseGroup, "", imageUrl);
                    if (result)
                    {
                        tbName.Text = "";
                        imageUrl = null;
                        _filePath = null;
                        LoadCategory();
                    }
                    else
                    {
                        MessageBox.Show("Error create category");
                    }
                } else
                {
                    MessageBox.Show("Please enter info");
                }
            }

        }
        private void lbIncome_Click(object sender, EventArgs e)
        {
            _selectExpense = "Income";
            selectTypeChangeColor();
        }
        public void selectTypeChangeColor()
        {
            if (_selectExpense == "Expense")
            {
                blExpense.BackColor = Color.LightSalmon;
                blExpense.ForeColor = Color.Black;

                lbIncome.BackColor = Color.Bisque;
                lbIncome.ForeColor = Color.Black;
            }
            else
            {
                blExpense.BackColor = Color.Bisque;
                blExpense.ForeColor = Color.Black;

                lbIncome.BackColor = Color.LightSalmon;
                lbIncome.ForeColor = Color.Black;

            }
        }

        private void blExpense_Click(object sender, EventArgs e)
        {
            _selectExpense = "Expense";
            selectTypeChangeColor();
        }

        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = cbGroup.SelectedItem as dynamic;
            if (selectedItem != null && selectedItem.Value != 0)
            {
                _selectExpenseGroup = selectedItem.Value;
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            string imageUrl = null;
            bool result;

            if (_filePath != null)
            {
                btnUpdate.Text = "Updating...";
                imageUrl = await _cloudinaryHelper.UploadImageAsync(_filePath);

                if (imageUrl != null)
                {
                    btnUpdate.Text = "Update";
                }
            }
            else if (_iconSelect == null)
            {
                return;
            }
            else
            {
                btnUpdate.Text = "Update";
            }

            _tbName = tbName.Text;

            if (!string.IsNullOrEmpty(_tbName) || _selectExpenseGroup != 0)
            {
                if (_iconSelect != null)
                {
                    result = _categoryController.UpdateCategory(_userId, _categoryId, _tbName, _selectExpense, _selectExpenseGroup, _iconSelect, "");

                    if (result)
                    {
                        tbName.Text = "";
                        _iconSelect = null;
                        LoadCategory();
                    }
                    else
                    {
                        MessageBox.Show("Error updating category!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (imageUrl != null)
                {
                    result = _categoryController.UpdateCategory(_userId, _categoryId, _tbName, _selectExpense, _selectExpenseGroup, "", imageUrl);

                    if (result)
                    {
                        tbName.Text = "";
                        imageUrl = null;
                        _filePath = null;
                        LoadCategory();
                    }
                    else
                    {
                        MessageBox.Show("Error updating category with image!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter category information!", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_iconSelect == null)
            {
                MessageBox.Show("Please select a category to delete!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this category?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_categoryController.DeleteCategory(_userId, _categoryId) > 0)
                {
                    _iconSelect = null;
                    LoadCategory();
                    MessageBox.Show("Category deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error deleting category!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnOpenCategoryGroup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Home homeForm = Home.GetInstance(_userId);
            if (homeForm != null)
            {
                homeForm.ProcessToastAction("openCategoryGroup");
                this.Close();
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();

        }
    }
}
