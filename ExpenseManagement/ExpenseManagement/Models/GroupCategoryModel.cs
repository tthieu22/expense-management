using ExpenseManagement.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseManagement.Models
{
    internal class GroupCategoryModel
    {
        private readonly Connect db = new Connect();

        private bool IsAdmin(int userId)
        {
            string query = "SELECT role FROM Users WHERE user_id = @UserId";
            SqlParameter[] parameters = { new SqlParameter("@UserId", userId) };
            DataTable result = db.ExecuteQuery(query, parameters);

            if (result != null && result.Rows.Count > 0)
            {
                string role = result.Rows[0]["role"]?.ToString() ?? "";
                return role.Equals("admin", StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        private bool IsOwner(int userId, int categoryGroupId)
        {
            string query = "SELECT COUNT(*) FROM CategoryGroups WHERE category_group_id = @CategoryGroupId AND created_by = @UserId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@CategoryGroupId", categoryGroupId),
                new SqlParameter("@UserId", userId)
            };
            object result = db.ExecuteScalar(query, parameters);
            return (result != null && Convert.ToInt32(result) > 0);
        }

        public DataTable GetAllCategoryGroups(int userId)
        {
            string query = @"
                SELECT cg.category_group_id, cg.group_name, cg.description, cg.group_icon
                FROM CategoryGroups cg
                LEFT JOIN UserHiddenGroups uhg 
                    ON cg.category_group_id = uhg.category_group_id 
                    AND uhg.user_id = @UserId
                WHERE uhg.category_group_id IS NULL 
                    AND (cg.created_by = @UserId 
                        OR cg.created_by IN (SELECT user_id FROM Users WHERE role = 'admin'))";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId", userId)
            };

            return db.ExecuteQuery(query, parameters);
        }

        public bool AddCategoryGroup(int userId, string groupName, string description, string groupIcon)
        {
            string query = "INSERT INTO CategoryGroups (group_name, description, group_icon, created_by) VALUES (@GroupName, @Description, @GroupIcon, @UserId)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@GroupName", groupName),
                new SqlParameter("@Description", description),
                new SqlParameter("@GroupIcon", groupIcon),
                new SqlParameter("@UserId", userId)
            };
            return db.ExecuteNonQuery(query, parameters);
        }

        public bool UpdateCategoryGroup(int userId, int categoryGroupId, string groupName, string description, string groupIcon)
        {
            if (!IsAdmin(userId) && !IsOwner(userId, categoryGroupId))
                return false;

            string query = "UPDATE CategoryGroups SET group_name = @GroupName, description = @Description, group_icon = @GroupIcon WHERE category_group_id = @CategoryGroupId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@CategoryGroupId", categoryGroupId),
                new SqlParameter("@GroupName", groupName),
                new SqlParameter("@Description", description),
                new SqlParameter("@GroupIcon", groupIcon)
            };
            return db.ExecuteNonQuery(query, parameters);
        }

        public bool DeleteCategoryGroup(int userId, int categoryGroupId)
        {
            if (IsAdmin(userId))
            {
                string query = @"
                    DELETE FROM UserHiddenGroups WHERE category_group_id = @CategoryGroupId;
                    DELETE FROM CategoryGroups WHERE category_group_id = @CategoryGroupId;";
                SqlParameter[] parameters = { new SqlParameter("@CategoryGroupId", categoryGroupId) };
                return db.ExecuteNonQuery(query, parameters);
            }
            else if (IsOwner(userId, categoryGroupId))
            {
                string query = @"
                    DELETE FROM UserHiddenGroups WHERE category_group_id = @CategoryGroupId;
                    DELETE FROM CategoryGroups WHERE category_group_id = @CategoryGroupId;";
                SqlParameter[] parameters = { new SqlParameter("@CategoryGroupId", categoryGroupId) };
                return db.ExecuteNonQuery(query, parameters);
            }
            else
            {
                string query = "INSERT INTO UserHiddenGroups (user_id, category_group_id) VALUES (@UserId, @CategoryGroupId)";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@UserId", userId),
                    new SqlParameter("@CategoryGroupId", categoryGroupId)
                };
                return db.ExecuteNonQuery(query, parameters);
            }
        }

        public int AddCategoryGroupId(int userId, string groupName, string description, string groupIcon)
        {
            string query = @"
                INSERT INTO CategoryGroups (group_name, description, group_icon, created_by)
                OUTPUT INSERTED.category_group_id  -- Trả về ID của bản ghi vừa được chèn
                VALUES (@GroupName, @Description, @GroupIcon, @UserId)";

                    SqlParameter[] parameters =
                    {
                new SqlParameter("@GroupName", groupName),
                new SqlParameter("@Description", description),
                new SqlParameter("@GroupIcon", groupIcon),
                new SqlParameter("@UserId", userId)
            };

            var result = db.ExecuteScalar(query, parameters);

            return result != null ? Convert.ToInt32(result) : 0;
        }
        public int UpdateCategoryGroupId(int existingCategoryId, int suggestedGroupId, string description)
        {
            string query = @"
                UPDATE CategoryGroups
                SET description = @Description
                OUTPUT INSERTED.category_group_ids
                WHERE category_group_id = @ExistingCategoryId AND category_group_id != @SuggestedGroupId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Description", description),
                new SqlParameter("@ExistingCategoryId", existingCategoryId),
                new SqlParameter("@SuggestedGroupId", suggestedGroupId)
            };

            var result = db.ExecuteScalar(query, parameters);  

            return result != null ? Convert.ToInt32(result) : 0; 
        }

        public int CreateCategoryInGroup(int userId, int categoryGroupId, string categoryName, string categoryType, string categoryIconChar)
        {
            string query = @"
                INSERT INTO Categories (category_group_id, category_name, category_type, category_icon_char, user_id)
                OUTPUT INSERTED.category_id 
                VALUES (@CategoryGroupId, @CategoryName, @CategoryType, @CategoryIconChar, @UserId)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@CategoryGroupId", categoryGroupId),
                new SqlParameter("@CategoryName", categoryName),
                new SqlParameter("@CategoryType", categoryType),
                new SqlParameter("@CategoryIconChar", categoryIconChar),
                new SqlParameter("@UserId", userId)
            };

            var result = db.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public DataTable GetCategoryGroupById(int userId, int categoryGroupId)
        {
            string query = @"
                SELECT category_group_id, group_name, description, group_icon, created_by
                FROM CategoryGroups
                WHERE category_group_id = @CategoryGroupId 
                  AND (created_by = @UserId OR @UserId IN (SELECT user_id FROM Users WHERE role = 'admin'))";

            SqlParameter[] parameters =
            {
                new SqlParameter("@CategoryGroupId", categoryGroupId),
                new SqlParameter("@UserId", userId)
            };

            return db.ExecuteQuery(query, parameters);
        }

    }
}
