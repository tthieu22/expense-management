using System;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseManagement.Model
{
    internal class BudgetModel
    {
        private Connect db;

        public BudgetModel()
        {
            db = new Connect();
        }

        public bool AddBudget(int userId, int categoryId, decimal amount, DateTime startDate, DateTime endDate)
        {
            string query = "INSERT INTO Budgets (user_id, category_id, amount, start_date, end_date) VALUES (@userId, @categoryId, @amount, @startDate, @endDate)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@categoryId", categoryId),
                new SqlParameter("@amount", amount),
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate)
            };
            return db.ExecuteNonQuery(query, parameters);
        }

        public bool UpdateBudget(int budgetId, int userId, int categoryId, decimal amount, DateTime startDate, DateTime endDate)
        {
            string query = "UPDATE Budgets SET category_id = @categoryId, amount = @amount, start_date = @startDate, end_date = @endDate WHERE budget_id = @budgetId AND user_id = @userId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@budgetId", budgetId),
                new SqlParameter("@userId", userId),
                new SqlParameter("@categoryId", categoryId),
                new SqlParameter("@amount", amount),
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate)
            };
            return db.ExecuteNonQuery(query, parameters);
        }

        public bool DeleteBudget(int budgetId, int userId)
        {
            string query = "DELETE FROM Budgets WHERE budget_id = @budgetId AND user_id = @userId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@budgetId", budgetId),
                new SqlParameter("@userId", userId)
            };
            return db.ExecuteNonQuery(query, parameters);
        }

        public DataTable GetBudgetById(int budgetId, int userId)
        {
            string query = "SELECT * FROM Budgets WHERE budget_id = @budgetId AND user_id = @userId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@budgetId", budgetId),
                new SqlParameter("@userId", userId)
            };
            return db.ExecuteQuery(query, parameters);
        }

        public DataTable GetBudgetsByCategory(int userId, int categoryId)
        {
            string query = "SELECT * FROM Budgets WHERE user_id = @userId AND category_id = @categoryId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@categoryId", categoryId)
            };
            return db.ExecuteQuery(query, parameters);
        }

        public DataTable GetBudgetsInMonth(int userId, int month, int year)
        {
            string query = "SELECT B.*, C.category_name " +
                           "FROM Budgets B " +
                           "JOIN Categories C ON B.category_id = C.category_id " +
                           "WHERE B.user_id = @userId " +
                           "AND MONTH(B.start_date) = @month " +
                           "AND YEAR(B.start_date) = @year";

             SqlParameter[] parameters =
             {
                new SqlParameter("@userId", userId),
                new SqlParameter("@month", month),
                new SqlParameter("@year", year)
             };

            return db.ExecuteQuery(query, parameters);
        }

        public DataTable GetCategoriesWithBudgetsInMonth(int userId, int month, int year)
        {
            string query = "SELECT B.*, C.* " +
                           "FROM Budgets B " +
                           "JOIN Categories C ON B.category_id = C.category_id " +
                           "WHERE B.user_id = @userId " +
                           "AND MONTH(B.start_date) = @month " +
                           "AND YEAR(B.start_date) = @year";

            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@month", month),
                new SqlParameter("@year", year)
            };

            return db.ExecuteQuery(query, parameters);
        }

    }
}
