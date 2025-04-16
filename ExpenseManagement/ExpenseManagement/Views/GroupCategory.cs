using ExpenseManagement.Controllers;
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
    public partial class GroupCategory : Form
    {
        private int _userId;
        private IconButton _selectedIconButton;
        private CloudinaryHelper _cloudinaryHelper;
        CategoryController _categoryController;
        ExpenseIconsLoader _expenseIconLoader;
        private string _filePath;
        private string _iconSelect ;
        private int _categoryId = 0;

        CategoryGroupController _categoryGroupcontroller = new CategoryGroupController();

        public int CategoryId { get; private set; }
        public GroupCategory(int userId)
        {
            this._userId = userId;
            InitializeComponent();

            _cloudinaryHelper = new CloudinaryHelper();
            _categoryController = new CategoryController();
            ShowData();
        }

        private void GroupCategory_Load(object sender, EventArgs e)
        {
            _expenseIconLoader = new ExpenseIconsLoader(plListIcon);
            _expenseIconLoader.LoadIcons(OnIconSelected);
        }

        private void OnIconSelected(IconChar? selectedIcon)
        {
            if (selectedIcon.HasValue)
            {
                _iconSelect = selectedIcon.Value.ToString();
            }
        }
        public void ShowData()
        {
            DataTable data = _categoryGroupcontroller.GetAllCategoryGroups(_userId);
            if (data != null)
            {
                dgvListGroupCategory.DataSource = data;
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = tbName.Text.Trim();
            string des = tbDesc.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Tên nhóm danh mục không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool success = _categoryGroupcontroller.AddCategoryGroup(_userId, name, des, _iconSelect);
            if (success)
            {
                MessageBox.Show("Thêm nhóm danh mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowData(); // Load lại danh sách
            }
            else
            {
                MessageBox.Show("Lỗi khi thêm nhóm danh mục!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_categoryId == 0)
            {
                MessageBox.Show("Vui lòng chọn nhóm danh mục cần cập nhật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = tbName.Text.Trim();
            string des = tbDesc.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Tên nhóm danh mục không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool success = _categoryGroupcontroller.UpdateCategoryGroup(_userId, _categoryId, name, des, _iconSelect);
            if (success)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowData(); // Load lại danh sách
            }
            else
            {
                MessageBox.Show("Bạn không có quyền cập nhật nhóm danh mục này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_categoryId == 0)
            {
                MessageBox.Show("Vui lòng chọn nhóm danh mục cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhóm danh mục này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                bool success = _categoryGroupcontroller.DeleteCategoryGroup(_userId, _categoryId);
                if (success)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowData(); // Load lại danh sách
                }
                else
                {
                    MessageBox.Show("Bạn không thể xóa nhóm danh mục này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void dgvListGroupCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow row = dgvListGroupCategory.Rows[e.RowIndex];

                tbName.Text = row.Cells["group_name"].Value?.ToString().Trim();
                tbDesc.Text = row.Cells["description"].Value?.ToString().Trim();

                // Lấy icon đã chọn
                _iconSelect = row.Cells["group_icon"].Value?.ToString();

                if (row.Cells["category_group_id"].Value != null && int.TryParse(row.Cells["category_group_id"].Value.ToString(), out int categoryId))
                {
                    _categoryId = categoryId;
                }
                else
                {
                    _categoryId = 0;
                }
            }
        }

    }
}
