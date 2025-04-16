using System;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseManagement.Model
{
    internal class CategoryModel
    {
        private readonly Connect db = new Connect();

        public DataTable GetAllCategoriesWithGroupInfo()
        {
            string query = @"
                SELECT *
                FROM Categories c
                INNER JOIN CategoryGroups g ON c.category_group_id = g.category_group_id";
            return db.ExecuteQuery(query);
        }
        public DataTable GetAllCategoriesGroup()
        {
            string query = @"
                SELECT *
                FROM CategoryGroups ";
            return db.ExecuteQuery(query);
        }

        public DataTable GetCategoriesByType(int userId, string categoryType)
        {
            string query = @"
                SELECT *
                FROM Categories c
                INNER JOIN CategoryGroups g ON c.category_group_id = g.category_group_id
                WHERE c.category_type = @categoryType
                AND c.user_id = @userId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@categoryType", categoryType),
                new SqlParameter("@userId", userId)
            };

            return db.ExecuteQuery(query, parameters);
        }

        public bool AddCategory(int userId, string name, string type, int groupId, string iconChar, string iconUrl)
        {
            string query = @"
                INSERT INTO Categories (user_id, category_name, category_type, category_group_id, category_icon_char, category_icon_url)
                VALUES (@userId, @name, @type, @groupId, @iconChar, @iconUrl)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@name", name),
                new SqlParameter("@type", type),
                new SqlParameter("@groupId", groupId),
                new SqlParameter("@iconChar", iconChar),
                new SqlParameter("@iconUrl", iconUrl)
            };
            return db.ExecuteNonQuery(query, parameters);
        }
        public int CreateCategoryQuickly(int userId, string name, string type, int groupId, string iconChar, string iconUrl)
        {
            string query = @"
                INSERT INTO Categories (user_id, category_name, category_type, category_group_id, category_icon_char, category_icon_url)
                OUTPUT INSERTED.category_id
                VALUES (@userId, @name, @type, @groupId, @iconChar, @iconUrl)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@name", name),
                new SqlParameter("@type", type),
                new SqlParameter("@groupId", groupId),
                new SqlParameter("@iconChar", iconChar),
                new SqlParameter("@iconUrl", iconUrl)
            };

            object result = db.ExecuteScalar(query, parameters);
            return (result != null) ? Convert.ToInt32(result) : -1;
        }

        public bool UpdateCategory(int userId, int categoryId, string name, string type, int groupId, string iconChar, string iconUrl)
        {
            string query = @"
                UPDATE Categories 
                SET 
                    category_name = @name, 
                    category_type = @type, 
                    category_group_id = @groupId, 
                    category_icon_char = ISNULL(NULLIF(@iconChar, ''), category_icon_char), 
                    category_icon_url = ISNULL(NULLIF(@iconUrl, ''), category_icon_url)
                WHERE user_id = @userId AND category_id = @categoryId";

                    SqlParameter[] parameters =
                    {
                new SqlParameter("@categoryId", categoryId),
                new SqlParameter("@userId", userId),
                new SqlParameter("@name", name),
                new SqlParameter("@type", type),
                new SqlParameter("@groupId", groupId),
                new SqlParameter("@iconChar", (object)iconChar ?? DBNull.Value),
                new SqlParameter("@iconUrl", (object)iconUrl ?? DBNull.Value)
            };

            return db.ExecuteNonQuery(query, parameters);
        }


        public bool IsAdmin(int userId)
        {
            string query = "SELECT role FROM Users WHERE user_id = @userId";
            SqlParameter[] parameters = { new SqlParameter("@userId", userId) };
            object result = db.ExecuteScalar(query, parameters);
            return result != null && result.ToString().ToLower() == "admin";
        }

        public int GetCategoryOwner(int categoryId)
        {
            string query = "SELECT user_id FROM Categories WHERE category_id = @categoryId";
            SqlParameter[] parameters = { new SqlParameter("@categoryId", categoryId) };
            object result = db.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }

        public int DeleteCategory(int categoryId)
        {
            string query = "DELETE FROM Categories WHERE category_id = @categoryId";
            SqlParameter[] parameters = { new SqlParameter("@categoryId", categoryId) };

            object result = db.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }

        public int HideCategory(int categoryId)
        {
            string query = "UPDATE Categories SET is_hidden = 1 WHERE category_id = @categoryId";
            SqlParameter[] parameters = { new SqlParameter("@categoryId", categoryId) };

            object result = db.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }

        public DataTable GetCategoriesWithGroupByType(int userId, string categoryType)
        {
            string query = @"
                SELECT *
                FROM Categories c
                JOIN CategoryGroups g ON c.category_group_id = g.category_group_id
                WHERE c.user_id = @user_id AND c.category_type = @category_type";

                    SqlParameter[] parameters = {
                new SqlParameter("@user_id", userId),
                new SqlParameter("@category_type", categoryType)
            };

            return db.ExecuteQuery(query, parameters);
        }

        public DataTable GetAllCategory(int userId)
        {
            string query = @"
                SELECT *
                FROM Categories c
                JOIN CategoryGroups g ON c.category_group_id = g.category_group_id
                WHERE c.user_id = @user_id";

            SqlParameter[] parameters = {
                new SqlParameter("@user_id", userId),
            };

            return db.ExecuteQuery(query, parameters);
        }

        public DataTable GetCategoryById(int userId,int categoryId)
        {
            string query = @"
                SELECT *
                FROM Categories c
                JOIN CategoryGroups g ON c.category_group_id = g.category_group_id
                WHERE c.user_id = @user_id AND c.category_id =@category_id";

            SqlParameter[] parameters = {
                new SqlParameter("@user_id", userId),
                new SqlParameter("@category_id", categoryId),
            };

            return db.ExecuteQuery(query, parameters);
        }

    }
}
