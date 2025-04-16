using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseManagement.Model
{
    internal class SavingSuggestionModel
    {
        private readonly Connect db = new Connect();

        // 1. Tính tổng thu nhập
        public decimal GetTotalIncome(int userId)
        {
            string query = "SELECT SUM(amount) FROM Incomes WHERE user_id = @userId";
            SqlParameter[] parameters = { new SqlParameter("@userId", userId) };
            object result = db.ExecuteScalar(query, parameters);
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }

        // 2. Tính tổng chi tiêu
        public decimal GetTotalExpenses(int userId)
        {
            string query = "SELECT SUM(amount) FROM Expenses WHERE user_id = @userId";
            SqlParameter[] parameters = { new SqlParameter("@userId", userId) };
            object result = db.ExecuteScalar(query, parameters);
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }

        // 3. Gợi ý số tiền tiết kiệm
        public decimal SuggestSavingAmount(int userId)
        {
            decimal income = GetTotalIncome(userId);
            decimal expenses = GetTotalExpenses(userId);
            return (income - expenses) * 0.2m; // Gợi ý tiết kiệm 20% số dư
        }

        // 4. Liệt kê 5 khoản chi tiêu lớn nhất
        public DataTable GetTop5Expenses(int userId)
        {
            string query = "SELECT TOP 5 * FROM Expenses WHERE user_id = @userId ORDER BY amount DESC";
            SqlParameter[] parameters = { new SqlParameter("@userId", userId) };
            return db.ExecuteQuery(query, parameters);
        }

        // 5. Kiểm tra đạt mục tiêu tiết kiệm
        public bool CheckSavingGoal(int userId, decimal goalAmount)
        {
            decimal savedAmount = SuggestSavingAmount(userId);
            return savedAmount >= goalAmount;
        }

        // 6. Dự đoán chi tiêu tháng tới (trung bình 3 tháng gần nhất)
        public decimal PredictNextMonthExpenses(int userId)
        {
            string query = "SELECT AVG(amount) FROM Expenses WHERE user_id = @userId AND expense_date >= DATEADD(MONTH, -3, GETDATE())";
            SqlParameter[] parameters = { new SqlParameter("@userId", userId) };
            object result = db.ExecuteScalar(query, parameters);
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }

        // 7. Cảnh báo chi tiêu vượt mức
        public bool CheckOverspending(int userId, decimal budget)
        {
            decimal expenses = GetTotalExpenses(userId);
            return expenses > budget;
        }

        // 8. Gợi ý danh mục cần cắt giảm
        public DataTable SuggestCutbackCategories(int userId)
        {
            string query = "SELECT category_id, SUM(amount) AS total FROM Expenses WHERE user_id = @userId GROUP BY category_id ORDER BY total DESC";
            SqlParameter[] parameters = { new SqlParameter("@userId", userId) };
            return db.ExecuteQuery(query, parameters);
        }

        // 9. Theo dõi xu hướng tiết kiệm
        public DataTable TrackSavingTrends(int userId)
        {
            string query = "SELECT FORMAT(expense_date, 'yyyy-MM') AS month, SUM(amount) AS total FROM Expenses WHERE user_id = @userId GROUP BY FORMAT(expense_date, 'yyyy-MM') ORDER BY month";
            SqlParameter[] parameters = { new SqlParameter("@userId", userId) };
            return db.ExecuteQuery(query, parameters);
        }
        public bool CreateSavingPlan(int userId, string goalName, decimal targetAmount, int months)
        {
            string query = @"
                INSERT INTO SavingsGoals (user_id, goal_name, target_amount, saved_amount, target_date, status) 
                VALUES (@userId, @goalName, @targetAmount, 0, DATEADD(MONTH, @months, GETDATE()), 'In Progress')";

               SqlParameter[] parameters = {
                    new SqlParameter("@userId", userId),
                    new SqlParameter("@goalName", goalName),
                    new SqlParameter("@targetAmount", targetAmount),
                    new SqlParameter("@months", months)
                };

            return db.ExecuteNonQuery(query, parameters);
        }

        // 11. Theo dõi tiến độ tiết kiệm
        public decimal TrackSavingProgress(int userId, int? month = null, int? year = null)
        {
            string query = @"
                SELECT 
                    CASE 
                        WHEN COUNT(goal_id) = 0 THEN 0 
                        ELSE SUM(saved_amount * 100.0 / NULLIF(target_amount, 0)) / COUNT(goal_id) 
                    END AS progress
                FROM SavingsGoals 
                WHERE user_id = @userId";

                    // Nếu có giá trị tháng & năm, thêm điều kiện lọc
                    if (month.HasValue && year.HasValue)
                    {
                        query += " AND MONTH(target_date) = @month AND YEAR(target_date) = @year";
                    }

                    List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@userId", userId)
            };

            if (month.HasValue && year.HasValue)
            {
                parameters.Add(new SqlParameter("@month", month.Value));
                parameters.Add(new SqlParameter("@year", year.Value));
            }

            object result = db.ExecuteScalar(query, parameters.ToArray());

            return (result != DBNull.Value) ? Convert.ToDecimal(result) : 0;
        }


        // 12. Nhắc nhở khi chậm tiến độ
        public bool CheckSavingProgress(int userId, decimal expectedAmount)
        {
            decimal savedAmount = TrackSavingProgress(userId);
            return savedAmount >= expectedAmount;
        }

        // 13. Dự đoán số dư tài chính cuối tháng
        public decimal PredictMonthEndBalance(int userId)
        {
            decimal income = GetTotalIncome(userId);
            decimal expenses = PredictNextMonthExpenses(userId);
            return income - expenses;
        }

        public bool UpdateSavedAmount(int userId, int goalId, decimal amount)
        {
            string query = @"
                UPDATE SavingsGoals 
                SET saved_amount = saved_amount + @amount 
                WHERE goal_id = @goalId AND user_id = @userId";

                    SqlParameter[] parameters = {
                new SqlParameter("@goalId", goalId),
                new SqlParameter("@userId", userId),
                new SqlParameter("@amount", amount)
            };

            return db.ExecuteNonQuery(query, parameters);   
        }

        //Check Goal Status
        public bool CheckSavingGoalStatus(int userId, int goalId)
        {
            string query = @"
                UPDATE SavingsGoals 
                SET status = 'Achieved'
                WHERE goal_id = @goalId AND user_id = @userId AND saved_amount >= target_amount";

                    SqlParameter[] parameters = {
                new SqlParameter("@goalId", goalId),
                new SqlParameter("@userId", userId)
            };

            return db.ExecuteNonQuery(query, parameters);
        }
        public DataTable GetAllSavingGoals(int userId)
        {
            string query = @"
                SELECT goal_id, goal_name, target_amount, saved_amount, target_date, 
                (saved_amount * 100.0 / NULLIF(target_amount, 0)) AS progress
                FROM SavingsGoals 
                WHERE user_id = @userId";

            SqlParameter[] parameters = { new SqlParameter("@userId", userId) };
            return db.ExecuteQuery(query, parameters);
        }

        public DataTable GetSavingGoals(int userId)
        {
            string query = "SELECT * FROM SavingsGoals WHERE user_id = @userId";
            SqlParameter[] parameters = { new SqlParameter("@userId", userId) };
            return db.ExecuteQuery(query, parameters);
        }


    }
}
