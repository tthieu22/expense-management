using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ExpenseManagement.Model;

namespace ExpenseManagement.Controllers
{
    internal class CategoryController
    {
        private readonly CategoryModel categoryModel = new CategoryModel();

        public DataTable GetAllCategories(int userId)
        {
            return categoryModel.GetAllCategory(userId);
        }

        public DataTable GetAllCategoriesGroup()
        {
            return categoryModel.GetAllCategoriesGroup();
        }

        public DataTable GetCategoryById(int userId, int categoryId)
        {
            return categoryModel.GetCategoryById(userId, categoryId);
        }

        public DataTable GetCategoriesByType(int userId, string categoryType)
        {
            return categoryModel.GetCategoriesByType(userId ,categoryType);
        }

        public bool AddCategory(int userId, string name, string type, int groupId, string iconChar, string iconUrl)
        {
            return categoryModel.AddCategory(userId, name, type, groupId, iconChar, iconUrl);
        }


        public int CreateCategoryQuickly(int userId, string name, string type, int groupId, string iconChar, string iconUrl)
        {
            return categoryModel.CreateCategoryQuickly(userId, name, type, groupId, iconChar, iconUrl);
        }
        public bool UpdateCategory(int userId, int categoryId, string name, string type, int groupId, string iconChar, string iconUrl)
        {
            return categoryModel.UpdateCategory(userId,categoryId, name, type, groupId, iconChar, iconUrl);
        }

        public int DeleteCategory(int userId, int categoryId)
        {
            bool isAdmin = categoryModel.IsAdmin(userId);
            int ownerId = categoryModel.GetCategoryOwner(categoryId);

            if (ownerId == -1)
            {
                return 0;
            }

            if (isAdmin || ownerId == userId)
            {
                return categoryModel.DeleteCategory(categoryId);
            }
            else
            {
                return categoryModel.HideCategory(categoryId);
            }
        }


        public List<Dictionary<string, object>> CategorizeByGroup(int userId, string categoryType)
        {
            DataTable categories = categoryModel.GetCategoriesWithGroupByType(userId, categoryType);
            List<Dictionary<string, object>> categorizedData = new List<Dictionary<string, object>>();

            var groupedData = categories.AsEnumerable()
                .GroupBy(row => new
                {
                    GroupId = row["category_group_id"],
                    GroupName = row["group_name"],
                    GroupIcon = row["group_icon"],
                    GroupDescription = row["description"]
                });

            foreach (var group in groupedData)
            {
                Dictionary<string, object> groupInfo = new Dictionary<string, object>
                {
                    ["category_group_id"] = group.Key.GroupId,
                    ["group_name"] = group.Key.GroupName,
                    ["group_icon"] = group.Key.GroupIcon,
                    ["description"] = group.Key.GroupDescription,
                    ["categories"] = group.Select(row => new Dictionary<string, object>
                    {
                        ["category_id"] = row["category_id"],
                        ["category_name"] = row["category_name"],
                        ["category_type"] = row["category_type"],
                        ["category_icon_char"] = row["category_icon_char"],
                        ["category_icon_url"] = row["category_icon_url"]
                    }).ToList()
                };

                categorizedData.Add(groupInfo);
            }

            return categorizedData;
        }

    }
}
