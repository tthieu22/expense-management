using ExpenseManagement.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseManagement.Models
{
    internal class SavingMainModel
    {
        private readonly Connect db = new Connect();

        private decimal GetSumFromQuery(string query, SqlParameter[] parameters)
        {
            object result = db.ExecuteScalar(query, parameters);
            return result != DBNull.Value && result != null ? Convert.ToDecimal(result) : 0;
        }

        public decimal GetMonthlyIncome(int userId, int month, int year)
        {
            string query = @"SELECT SUM(amount) FROM Incomes WHERE user_id = @userId AND MONTH(income_date) = @month AND YEAR(income_date) = @year";
            return GetSumFromQuery(query, new SqlParameter[] {
                new SqlParameter("@userId", userId),
                new SqlParameter("@month", month),
                new SqlParameter("@year", year)
            });
        }

        public decimal GetMonthlyExpenses(int userId, int month, int year)
        {
            string query = @"SELECT SUM(amount) FROM Expenses WHERE user_id = @userId AND MONTH(expense_date) = @month AND YEAR(expense_date) = @year";
            return GetSumFromQuery(query, new SqlParameter[] {
                new SqlParameter("@userId", userId),
                new SqlParameter("@month", month),
                new SqlParameter("@year", year)
            });
        }

        public decimal GetMonthlyBudget(int userId, int month, int year)
        {
            string query = @"SELECT SUM(amount) FROM Budgets WHERE user_id = @userId AND MONTH(start_date) = @month AND YEAR(start_date) = @year";
            return GetSumFromQuery(query, new SqlParameter[] {
                new SqlParameter("@userId", userId),
                new SqlParameter("@month", month),
                new SqlParameter("@year", year)
            });
        }

        public decimal GetSavedAmount(int userId, int month, int year)
        {
            string query = @"SELECT SUM(saved_amount) FROM SavingsGoals WHERE user_id = @userId AND MONTH(target_date) = @month AND YEAR(target_date) = @year";
            return GetSumFromQuery(query, new SqlParameter[] {
                new SqlParameter("@userId", userId),
                new SqlParameter("@month", month),
                new SqlParameter("@year", year)
            });
        }
        public decimal GetSuggestedFutureSaving(int userId, int month, int year)
        {
            string query = @"
                SELECT SUM(target_amount) 
                FROM SavingsGoals 
                WHERE user_id = @userId 
                AND (YEAR(target_date) > @year OR (YEAR(target_date) = @year AND MONTH(target_date) >= @month))";

                    return GetSumFromQuery(query, new SqlParameter[] {
                new SqlParameter("@userId", userId),
                new SqlParameter("@month", month),
                new SqlParameter("@year", year)
            });
        }

        public Dictionary<string, decimal> GetTopExpenseCategories(int userId, int month, int year, int topN = 5)
        {
            string query = @"
                SELECT TOP (@topN) c.category_name, SUM(e.amount) AS total_spent
                FROM Expenses e
                JOIN Categories c ON e.category_id = c.category_id
                WHERE e.user_id = @userId 
                      AND MONTH(e.expense_date) = @month 
                      AND YEAR(e.expense_date) = @year
                GROUP BY c.category_name
                ORDER BY total_spent DESC;";

                    DataTable dt = db.ExecuteQuery(query, new SqlParameter[] {
                new SqlParameter("@userId", userId),
                new SqlParameter("@month", month),
                new SqlParameter("@year", year),
                new SqlParameter("@topN", topN)
            });

            var result = new Dictionary<string, decimal>();
            foreach (DataRow row in dt.Rows)
            {
                string categoryName = row["category_name"].ToString();
                decimal totalSpent = row["total_spent"] != DBNull.Value ? Convert.ToDecimal(row["total_spent"]) : 0;
                result[categoryName] = totalSpent;
            }

            return result;
        }

        public Dictionary<string, Tuple<decimal, decimal>> GetSavingPlan(int userId, int year)
        {
            string query = @"
                SELECT MONTH(target_date) AS month, SUM(target_amount) AS total_target, SUM(saved_amount) AS total_saved
                FROM SavingsGoals
                WHERE user_id = @userId AND YEAR(target_date) = @year
                GROUP BY MONTH(target_date)
                ORDER BY month";

            DataTable dt = db.ExecuteQuery(query, new SqlParameter[] {
                new SqlParameter("@userId", userId),
                new SqlParameter("@year", year),
            });

            var savingPlan = new Dictionary<string, Tuple<decimal, decimal>>();
            foreach (DataRow row in dt.Rows)
            {
                string monthName = new DateTime(year, Convert.ToInt32(row["month"]), 1).ToString("MMMM");
                savingPlan[monthName] = Tuple.Create(
                    row["total_target"] != DBNull.Value ? Convert.ToDecimal(row["total_target"]) : 0,
                    row["total_saved"] != DBNull.Value ? Convert.ToDecimal(row["total_saved"]) : 0
                );
            }
            return savingPlan;
        }
        public bool AddSavingGoal(int userId, string goalName, decimal targetAmount, int months, string description)
        {
            string query = @"
                INSERT INTO SavingsGoals (user_id, goal_name, target_amount, saved_amount, target_date, status, description) 
                VALUES (@userId, @goalName, CAST(@targetAmount AS DECIMAL(18,2)), 0, DATEADD(MONTH, @months, GETDATE()), 'In Progress', @description)";

                    SqlParameter[] parameters = {
                new SqlParameter("@userId", userId),
                new SqlParameter("@goalName", goalName),
                new SqlParameter("@targetAmount", targetAmount),
                new SqlParameter("@months", months),
                new SqlParameter("@description", description)
            };

            return db.ExecuteNonQuery(query, parameters);
        }

        public bool UpdateSavingGoal(int goalId, string goalName, decimal targetAmount, decimal savedAmount, DateTime targetDate, string status, int categoryId, int priorityLevel, string description)
            {
                string query = @"UPDATE SavingsGoals
                          SET goal_name = @goalName, target_amount = @targetAmount, saved_amount = @savedAmount,
                              target_date = @targetDate, status = @status, category_id = @categoryId,
                              priority_level = @priorityLevel, description = @description
                          WHERE goal_id = @goalId";

                return db.ExecuteNonQuery(query, new SqlParameter[] {
                    new SqlParameter("@goalId", goalId),
                    new SqlParameter("@goalName", goalName),
                    new SqlParameter("@targetAmount", targetAmount),
                    new SqlParameter("@savedAmount", savedAmount),
                    new SqlParameter("@targetDate", targetDate),
                    new SqlParameter("@status", status),
                    new SqlParameter("@categoryId", categoryId),
                    new SqlParameter("@priorityLevel", priorityLevel),
                    new SqlParameter("@description", description)
                });
            }

            public bool DeleteSavingGoal(int goalId,int userid)
            {
                string query = "DELETE FROM SavingsGoals WHERE goal_id = @goalId AND user_id = @userID";

                return db.ExecuteNonQuery(query, new SqlParameter[] {
                    new SqlParameter("@goalId", goalId),
                    new SqlParameter("@userID", userid)
                });
            }
            public decimal GetYearlyIncome(int userId, int year)
            {
                string query = @"SELECT SUM(amount) FROM Incomes WHERE user_id = @userId AND YEAR(income_date) = @year";
                return GetSumFromQuery(query, new SqlParameter[] {
                    new SqlParameter("@userId", userId),
                    new SqlParameter("@year", year)
                });
            }

            public decimal GetYearlyExpenses(int userId, int year)
            {
                string query = @"SELECT SUM(amount) FROM Expenses WHERE user_id = @userId AND YEAR(expense_date) = @year";
                        return GetSumFromQuery(query, new SqlParameter[] {
                    new SqlParameter("@userId", userId),
                    new SqlParameter("@year", year)
                });
            }
            public decimal GetYearlySavedAmount(int userId, int year)
            {
                string query = @"SELECT SUM(saved_amount) FROM SavingsGoals WHERE user_id = @userId AND YEAR(target_date) = @year";
                return GetSumFromQuery(query, new SqlParameter[] {
                    new SqlParameter("@userId", userId),
                    new SqlParameter("@year", year)
                });
            }
            public DataTable GetExpenseDetails(int userId, int month, int year)
            {
                string query = @"
                    SELECT e.expense_date, c.category_name, e.amount, e.description
                    FROM Expenses e
                    JOIN Categories c ON e.category_id = c.category_id
                    WHERE e.user_id = @userId AND MONTH(e.expense_date) = @month AND YEAR(e.expense_date) = @year
                    ORDER BY e.expense_date";

                return db.ExecuteQuery(query, new SqlParameter[] {
                    new SqlParameter("@userId", userId),
                    new SqlParameter("@month", month),
                    new SqlParameter("@year", year)
                });
            }
            public DataTable GetSavingGoalsByUser(int userId)
            {
                string query = "SELECT * FROM SavingsGoals WHERE user_id = @userId ORDER BY created_at DESC";

                return db.ExecuteQuery(query, new SqlParameter[]
                {
                    new SqlParameter("@userId", userId)
                });
            }
        // 5 chi tiêu lớn nhất trong tháng
        public DataTable GetTop5ExpensesInMonth(int userId, int month, int year)
        {
            string query = @"
                SELECT TOP 5 
                    e.expense_id,
                    e.user_id,
                    c.category_name,
                    c.category_type,
                    e.amount,
                    e.expense_date,
                    e.description,
                    e.payment_method
                FROM Expenses e
                JOIN Categories c ON e.category_id = c.category_id
                WHERE e.user_id = @userId AND c.category_type = 'Expense'
                AND MONTH(e.expense_date) = @month 
                AND YEAR(e.expense_date) = @year 
                ORDER BY e.amount DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@userId", userId),
                new SqlParameter("@month", month),
                new SqlParameter("@year", year)
            };

            return db.ExecuteQuery(query, parameters);
        }
        public List<Dictionary<string, object>> GetSavingReminders(int userId, int month, int year)
        {
            // Kiểm tra tháng & năm hợp lệ
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month), "Tháng phải từ 1 đến 12.");
            }
            if (year < 1 || year > 9999)
            {
                throw new ArgumentOutOfRangeException(nameof(year), "Năm phải từ 1 đến 9999.");
            }

            // Ngày đầu tháng
            DateTime today = new DateTime(year, month, 1);

            // Truy vấn lấy các mục tiêu đang "In Progress"
            string query = @"
            SELECT 
                goal_id,
                goal_name, 
                target_amount, 
                saved_amount, 
                target_date,
                saved_this_month,
                last_saved_at
            FROM SavingsGoals
            WHERE user_id = @userId 
            AND status = 'In Progress'";

            SqlParameter[] parameters = { new SqlParameter("@userId", userId) };
            DataTable result = db.ExecuteQuery(query, parameters);

            List<Dictionary<string, object>> reminders = new List<Dictionary<string, object>>();

            foreach (DataRow row in result.Rows)
            {
                int goalId = Convert.ToInt32(row["goal_id"]);
                string goalName = row["goal_name"].ToString();
                decimal targetAmount = Convert.ToDecimal(row["target_amount"]);
                decimal savedAmount = Convert.ToDecimal(row["saved_amount"]);

                // Xử lý target_date an toàn
                DateTime targetDate;
                if (row["target_date"] == DBNull.Value || !DateTime.TryParse(row["target_date"].ToString(), out targetDate))
                {
                    targetDate = today; // Nếu lỗi, mặc định là ngày hiện tại
                }

                // Xử lý last_saved_at an toàn
                DateTime? lastSavedAt = null;
                if (row["last_saved_at"] != DBNull.Value)
                {
                    lastSavedAt = Convert.ToDateTime(row["last_saved_at"]);
                }

                // Lấy số tiền đã tiết kiệm trong tháng này
                decimal savedThisMonth = row["saved_this_month"] == DBNull.Value ? decimal.Zero : Convert.ToDecimal(row["saved_this_month"]);

                // Nếu chưa gửi tiền trong tháng này, đặt lại saved_this_month = 0
                if (!lastSavedAt.HasValue || lastSavedAt.Value.Year != year || lastSavedAt.Value.Month != month)
                {
                    savedThisMonth = decimal.Zero;
                }

                // Số tháng còn lại (ít nhất là 1 tháng)
                int remainingMonths = Math.Max(((targetDate.Year - today.Year) * 12) + (targetDate.Month - today.Month), 1);

                // Số tiền cần tiết kiệm mỗi tháng
                decimal monthlyRequired = Math.Max(Math.Ceiling((targetAmount - savedAmount) / remainingMonths), decimal.Zero);

                // Kiểm tra số tiền còn thiếu hoặc dư trong tháng này
                decimal remainingThisMonth = Math.Max(monthlyRequired - savedThisMonth, decimal.Zero);
                decimal extraAmount = Math.Max(savedThisMonth - monthlyRequired, decimal.Zero);
                bool isCompletedThisMonth = savedThisMonth >= monthlyRequired;

                // Ghi chú nếu đã tiết kiệm đủ
                string note = isCompletedThisMonth
                    ? string.Format("Tháng này đã đủ, tháng sau cần tiết kiệm {0:N0} VND.", monthlyRequired)
                    : string.Empty;

                // Thêm vào danh sách nhắc nhở
                reminders.Add(new Dictionary<string, object>
                {
                    { "goal_name", goalName },
                    { "target_amount", targetAmount },
                    { "saved_amount", savedAmount },
                    { "monthly_required_amount", monthlyRequired },
                    { "saved_this_month", savedThisMonth },
                    { "remaining_this_month", remainingThisMonth },
                    { "extra_amount", extraAmount },
                    { "is_completed_this_month", isCompletedThisMonth },
                    { "note", note }
                });
            }

            return reminders;
        }


        // 13. Dự đoán số dư tài chính cuối tháng
        public decimal PredictMonthEndBalance(int userId,int month,int year)
        {
            decimal income = GetMonthlyIncome(userId,month,year);
            decimal expenses = PredictNextMonthExpenses(userId);
            return income - expenses;
        }

        // 6. Dự đoán chi tiêu tháng tới (trung bình 3 tháng gần nhất)
        public decimal PredictNextMonthExpenses(int userId)
        {
            string query = "SELECT AVG(amount) FROM Expenses WHERE user_id = @userId AND expense_date >= DATEADD(MONTH, -3, GETDATE())";
            SqlParameter[] parameters = { new SqlParameter("@userId", userId) };
            object result = db.ExecuteScalar(query, parameters);
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }


        // 3. Gợi ý số tiền tiết kiệm
        public decimal SuggestSavingAmount(int userId, int year)
        {
            decimal income = GetYearlyIncome(userId,year);
            decimal expenses = GetYearlyExpenses(userId, year);
            return (income - expenses) * 0.2m; // Gợi ý tiết kiệm 20% số dư của năm
        }

        // Check and update goal status if saved_amount >= target_amount
        public int CheckSavingGoalStatus(int userId, int goalId)
        {
            string updateQuery = @"
                UPDATE SavingsGoals 
                SET status = 'Achieved' 
                WHERE goal_id = @goalId 
                    AND user_id = @userId 
                    AND saved_amount >= target_amount 
                    AND status <> 'Achieved'";

                SqlParameter[] parameters = {
                new SqlParameter("@goalId", goalId),
                new SqlParameter("@userId", userId)
            };

            // Thực hiện update và kiểm tra số dòng ảnh hưởng
            bool updateSuccess = db.ExecuteNonQuery(updateQuery, parameters);
            return updateSuccess ? 1 : 0;
        }




        // thêm tiền vào mục tiêu
        public bool UpdateSavedAmount(int userId, int goalId, decimal amount)
        {
            string query = @"
                UPDATE SavingsGoals 
                SET 
                    saved_amount = saved_amount + @amount,
                    saved_this_month = saved_this_month + @amount,
                    remaining_amount = target_amount - (saved_amount + @amount),
                    extra_amount = CASE 
                        WHEN (saved_this_month + @amount) > monthly_required_amount 
                        THEN (saved_this_month + @amount) - monthly_required_amount 
                        ELSE 0 
                    END,
                    is_completed_this_month = CASE 
                        WHEN (saved_this_month + @amount) >= monthly_required_amount THEN 1 
                        ELSE 0 
                    END,
                    last_saved_at = GETDATE()
                WHERE goal_id = @goalId AND user_id = @userId";

                    SqlParameter[] parameters = {
                new SqlParameter("@goalId", goalId),
                new SqlParameter("@userId", userId),
                new SqlParameter("@amount", amount)
            };

            return db.ExecuteNonQuery(query, parameters);
        }

        //danh sách các mục tiêu chưa hoàn thành 
        public DataTable GetUnfinishedGoals(int userId)
        {
            string query = @"
                SELECT * 
                FROM SavingsGoals 
                WHERE user_id = @userId 
                AND status = 'In Progress'";

            SqlParameter[] parameters = {
                new SqlParameter("@userId", userId)
            };

            return db.ExecuteQuery(query, parameters);
        }
        // Tỉ lệ hoàn thành mục tiêu trong năm
        public decimal CalculateCompletionRateByYear(int userId, int year)
        {
            string query = @"
        SELECT 
            COUNT(*) AS TotalGoals,
            SUM(CASE WHEN status = 'Achieved' THEN 1 ELSE 0 END) AS AchievedGoals
        FROM SavingsGoals
        WHERE user_id = @userId
        AND YEAR(target_date) = @year";

            SqlParameter[] parameters = {
        new SqlParameter("@userId", userId),
        new SqlParameter("@year", year)
    };

            DataTable dt = db.ExecuteQuery(query, parameters);

            if (dt == null || dt.Rows.Count == 0)
            {
                return 0m; // Không có mục tiêu nào
            }

            // Kiểm tra NULL trước khi chuyển đổi
            object totalGoalsObj = dt.Rows[0]["TotalGoals"];
            object achievedGoalsObj = dt.Rows[0]["AchievedGoals"];

            int totalGoals = (totalGoalsObj == DBNull.Value) ? 0 : Convert.ToInt32(totalGoalsObj);
            int achievedGoals = (achievedGoalsObj == DBNull.Value) ? 0 : Convert.ToInt32(achievedGoalsObj);

            if (totalGoals == 0) return 0m; // Tránh chia cho 0

            decimal completionRate = (decimal)achievedGoals / totalGoals * 100;
            return Math.Round(completionRate, 2);
        }


        // tỉ lệ tiết kiệm hoàn thành của các mục tiêu
        public decimal CalculateTotalCompletionRate(int userId)
        {
            string query = @"
                SELECT 
                    COUNT(*) AS TotalGoals,
                    SUM(CASE WHEN status = 'Achieved' THEN 1 ELSE 0 END) AS AchievedGoals
                FROM SavingsGoals
                WHERE user_id = @userId";

                    SqlParameter[] parameters = {
                new SqlParameter("@userId", userId)
            };

            DataTable dt = db.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                int totalGoals = Convert.ToInt32(dt.Rows[0]["TotalGoals"]);
                int achievedGoals = Convert.ToInt32(dt.Rows[0]["AchievedGoals"]);

                if (totalGoals == 0) return 0m; // Tránh lỗi chia cho 0

                decimal completionRate = (decimal)achievedGoals / totalGoals * 100;
                return Math.Round(completionRate, 2);
            }

            return 0m;
        }


    }
}
