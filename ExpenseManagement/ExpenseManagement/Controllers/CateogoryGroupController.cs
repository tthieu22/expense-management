using System;
using System.Data;
using ExpenseManagement.Models;

namespace ExpenseManagement.Controllers
{
    internal class CategoryGroupController
    {
        private GroupCategoryModel model;

        public CategoryGroupController()
        {
            model = new GroupCategoryModel();
        }

        public DataTable GetAllCategoryGroups(int userId)
        {
            return model.GetAllCategoryGroups(userId);
        }
        public DataTable GetCategoryGroupById(int userId, int group_id)
        {
            return model.GetCategoryGroupById(userId,group_id);
        }
        
        public bool AddCategoryGroup(int userId, string groupName, string description, string groupIcon)
        {
            return model.AddCategoryGroup(userId, groupName, description, groupIcon);
        }

        public bool UpdateCategoryGroup(int userId, int categoryGroupId, string groupName, string description, string groupIcon)
        {
            return model.UpdateCategoryGroup(userId, categoryGroupId, groupName, description, groupIcon);
        }

        public bool DeleteCategoryGroup(int userId, int categoryGroupId)
        {
            return model.DeleteCategoryGroup(userId, categoryGroupId);
        }
        public int AddNewCategoryGroupId(int userId, string groupName, string description, string groupIcon)
        {
            return model.AddCategoryGroupId(userId, groupName, description, groupIcon);
        }

        // Cập nhật mô tả nhóm danh mục
        public int UpdateCategoryGroupDescriptionId(int existingCategoryId, int suggestedGroupId, string description)
        {
            return model.UpdateCategoryGroupId(existingCategoryId, suggestedGroupId, description);
        }

        // Thêm danh mục vào nhóm
        public int AddNewCategoryInGroupId(int userId, int categoryGroupId, string categoryName, string categoryType, string categoryIcon)
        {
            return model.CreateCategoryInGroup(userId, categoryGroupId, categoryName, categoryType, categoryIcon);
        }

    }
}
