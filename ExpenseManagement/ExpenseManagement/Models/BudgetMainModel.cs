using ExpenseManagement.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseManagement.Models
{
    internal class BudgetMainModel
    {
        private Connect db;

        public BudgetMainModel()
        {
            db = new Connect();
        }
        public bool AddTotalBudget(int userId, decimal amount, int warningThreshold, DateTime startDate, DateTime endDate)
        {
            string query = "INSERT INTO Budgets (user_id, category_id, amount, budget_type, warning_threshold, start_date, end_date) " +
                           "VALUES (@userId, @categoryId, @amount, 'total', @warningThreshold, @startDate, @endDate)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@categoryId", DBNull.Value),
                new SqlParameter("@amount", amount),
                new SqlParameter("@warningThreshold", warningThreshold),
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate)
            };
            return db.ExecuteNonQuery(query, parameters);
        }
        // Create buget fast
        public int AddTotalBudgetId(int userId, decimal amount, DateTime startDate, DateTime endDate)
        {
            string query = "INSERT INTO Budgets (user_id, category_id, amount, budget_type, warning_threshold, start_date, end_date) " +
                           "OUTPUT INSERTED.budget_id " +
                           "VALUES (@userId, @categoryId, @amount, 'total', 90, @startDate, @endDate)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@categoryId", DBNull.Value),
                new SqlParameter("@amount", amount),
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate)
            };

            return Convert.ToInt32(db.ExecuteScalar(query, parameters));
        }


        // Lấy tất cả ngân sách của tháng 
        public DataTable GetMonthlyBudgets(int userId)
        {
            string query = @"
                SELECT budget_id, user_id, category_id, amount, start_date, end_date, budget_type, warning_threshold
                FROM Budgets
                WHERE user_id = @userId 
                AND MONTH(start_date) = MONTH(GETDATE()) 
                AND YEAR(start_date) = YEAR(GETDATE())";

            SqlParameter[] parameters = { new SqlParameter("@userId", userId) };

            return db.ExecuteQuery(query, parameters);
        }

        // 🔵 Cập nhật ngân sách tổng
        public int UpdateTotalBudget(int userId, int budgetId, decimal amount, int warningThreshold, DateTime startDate, DateTime endDate)
        {
            if (amount < 0 || warningThreshold < 0 || startDate > endDate)
                return 0;

            string query = @"UPDATE Budgets 
                     SET amount = @amount, warning_threshold = @warningThreshold, start_date = @startDate, end_date = @endDate 
                     WHERE budget_id = @budgetId AND user_id = @userId AND budget_type = 'total';

                     SELECT @budgetId"; 
           SqlParameter[] parameters =
            {
                new SqlParameter("@budgetId", budgetId),
                new SqlParameter("@userId", userId),
                new SqlParameter("@amount", amount),
                new SqlParameter("@warningThreshold", warningThreshold),
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate)
            };

            return Convert.ToInt32(db.ExecuteScalar(query, parameters));
        }

        // 🔴 Xóa ngân sách tổng
        public int DeleteTotalBudget(int budgetId, int userId)
        {
            string query = @"DELETE FROM Budgets 
                     OUTPUT DELETED.budget_id 
                     WHERE budget_id = @budgetId AND user_id = @userId AND budget_type = 'total'";

            SqlParameter[] parameters =
            {
                new SqlParameter("@budgetId", budgetId),
                new SqlParameter("@userId", userId)
            };

            object result = db.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }


        // 🟡 Thêm ngân sách danh mục
        public bool AddCategoryBudget(int userId, int categoryId, decimal amount, int warningThreshold, DateTime startDate, DateTime endDate)
        {
            string query = "INSERT INTO Budgets (user_id, category_id, amount, budget_type, warning_threshold, start_date, end_date) " +
                           "VALUES (@userId, @categoryId, @amount, 'category', @warningThreshold, @startDate, @endDate)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@categoryId", categoryId),
                new SqlParameter("@amount", amount),
                new SqlParameter("@warningThreshold", warningThreshold),
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate)
            };
            return db.ExecuteNonQuery(query, parameters);
        }


        // 🟠 Cập nhật ngân sách danh mục
        public bool UpdateCategoryBudget(int budgetId, int userId, int categoryId, decimal amount, int warningThreshold, DateTime startDate, DateTime endDate)
        {
            string query = "UPDATE Budgets SET amount = @amount, warning_threshold = @warningThreshold, start_date = @startDate, end_date = @endDate " +
                           "WHERE budget_id = @budgetId AND user_id = @userId AND category_id = @categoryId AND budget_type = 'category'";
            SqlParameter[] parameters =
            {
                new SqlParameter("@budgetId", budgetId),
                new SqlParameter("@userId", userId),
                new SqlParameter("@categoryId", categoryId),
                new SqlParameter("@amount", amount),
                new SqlParameter("@warningThreshold", warningThreshold),
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate)
            };
            return db.ExecuteNonQuery(query, parameters);
        }


        // 🔴 Xóa ngân sách danh mục
        public bool DeleteCategoryBudget(int budgetId, int userId)
        {
            string query = "DELETE FROM Budgets WHERE budget_id = @budgetId AND user_id = @userId AND budget_type = 'category'";
            SqlParameter[] parameters =
            {
                new SqlParameter("@budgetId", budgetId),
                new SqlParameter("@userId", userId)
            };
            return db.ExecuteNonQuery(query, parameters);
        }
        public DataTable GetBudgetAll(int userId)
        {
            DateTime now = DateTime.Now;
            DateTime monthStart = new DateTime(now.Year, now.Month, 1);
            DateTime monthEnd = monthStart.AddMonths(1).AddDays(-1);

            string query = @"
            SELECT 
                b.budget_id, 
                b.budget_type, 
                b.amount, 
                b.start_date, 
                b.end_date, 
                b.warning_threshold, 
                c.category_name, 
                c.category_type, 
                cg.group_name
            FROM Budgets b
            LEFT JOIN Categories c ON b.category_id = c.category_id
            LEFT JOIN CategoryGroups cg ON c.category_group_id = cg.category_group_id
            WHERE 
                b.user_id = @userId AND 
                (
                    (b.start_date BETWEEN @MonthStart AND @MonthEnd)
                    OR
                    (b.end_date BETWEEN @MonthStart AND @MonthEnd)
                    OR
                    (b.start_date <= @MonthStart AND b.end_date >= @MonthEnd)
                )";

            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@MonthStart", monthStart),
                new SqlParameter("@MonthEnd", monthEnd)
            };

            return db.ExecuteQuery(query, parameters);
        }

        public DataTable GetBudgetById(int userId, int budgetId)
        {
            string query = @"
                SELECT budget_id, user_id, category_id, amount, start_date, end_date, budget_type, warning_threshold
                FROM Budgets 
                WHERE user_id = @userId AND budget_id = @budgetId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@budgetId", budgetId)
            };

            return db.ExecuteQuery(query, parameters);
        }


        // 🟣 Lấy tổng ngân sách của người dùng
        public decimal GetTotalBudget(int userId, int month, int year)
        {
            // Câu truy vấn để tính tổng ngân sách trong tháng và năm cụ thể
            string query = "SELECT COALESCE(SUM(amount), 0) FROM Budgets WHERE user_id = @userId " +
                           "AND budget_type = 'total' " +
                           "AND MONTH(start_date) = @month AND YEAR(start_date) = @year";

            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@month", month),
                new SqlParameter("@year", year)
            };

            return Convert.ToDecimal(db.ExecuteScalar(query, parameters));
        }


        // 🔵 Lấy tổng chi tiêu trong tháng
        public decimal GetTotalExpensesByMonth(int userId, int month, int year)
        {
            string query = "SELECT COALESCE(SUM(amount), 0) FROM Expenses " +
                           "WHERE user_id = @userId AND MONTH(expense_date) = @month AND YEAR(expense_date) = @year";
            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@month", month),
                new SqlParameter("@year", year)
            };
            return Convert.ToDecimal(db.ExecuteScalar(query, parameters));
        }
        public DataTable GetBudgetSummary(int userId, int month, int year)
        {
            string query = @"
                    SELECT 
                        b.category_id,
                        b.budget_id,
                        b.warning_threshold,
                        c.category_name AS DanhMuc,
                        b.amount AS NganSach,
                        COALESCE(SUM(e.amount), 0) AS DaChi,
                        (b.amount - COALESCE(SUM(e.amount), 0)) AS ConLai,
                        CASE 
                            WHEN COALESCE(SUM(e.amount), 0) > b.amount THEN 1 
                            WHEN COALESCE(SUM(e.amount), 0) >= b.amount * (b.warning_threshold / 100.0) THEN 2  -- Đạt mức cảnh báo
                            ELSE 0  -- Trong ngân sách
                        END AS TrangThai,
                        CAST((COALESCE(SUM(e.amount), 0) * 100.0 / NULLIF(b.amount, 0)) AS DECIMAL(5,2)) AS TyLeDaChi
                    FROM Budgets b
                    LEFT JOIN Categories c ON b.category_id = c.category_id
                    LEFT JOIN Expenses e 
                        ON b.user_id = e.user_id 
                        AND b.category_id = e.category_id 
                        AND MONTH(e.expense_date) = @month
                        AND YEAR(e.expense_date) = @year
                    WHERE b.user_id = @userId AND b.budget_type <> 'total' 
                    GROUP BY  b.budget_id, b.category_id, b.warning_threshold, c.category_name, b.amount;
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@userId", userId),
                    new SqlParameter("@month", month),
                    new SqlParameter("@year", year)
                };

            return db.ExecuteQuery(query, parameters);
        }

        // 🟢 Lấy chi tiêu tổng theo từng danh mục trong tháng
        public DataTable GetExpensesByCategory(int userId, int month, int year)
        {
            string query = @"
            SELECT 
                c.category_name, 
                SUM(e.amount) AS total_expense
            FROM Expenses e
            JOIN Categories c ON e.category_id = c.category_id
            WHERE e.user_id = @userId 
                AND MONTH(e.expense_date) = @month 
                AND YEAR(e.expense_date) = @year
            GROUP BY c.category_name
            ORDER BY c.category_name";

            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@month", month),
                new SqlParameter("@year", year)
            };

            return db.ExecuteQuery(query, parameters);
        }

        // 🟢 Lấy chi tiêu từng ngày trong tháng
        public DataTable GetDailyExpensesByMonth(int userId, int month, int year)
        {
            string query = @"
                SELECT 
                    DAY(e.expense_date) AS day,
                    SUM(e.amount) AS total_expense
                FROM Expenses e
                WHERE e.user_id = @userId 
                    AND MONTH(e.expense_date) = @month 
                    AND YEAR(e.expense_date) = @year
                GROUP BY DAY(e.expense_date)
                ORDER BY DAY(e.expense_date)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@month", month),
                new SqlParameter("@year", year)
            };

            return db.ExecuteQuery(query, parameters);
        }

        public DataTable GetDailyIncomeByMonth(int userId, int month, int year)
        {
            string query = @"
            SELECT 
                DAY(i.income_date) AS day,
                SUM(i.amount) AS total_income
            FROM Incomes i
            WHERE i.user_id = @userId 
                AND MONTH(i.income_date) = @month 
                AND YEAR(i.income_date) = @year
            GROUP BY DAY(i.income_date)
            ORDER BY DAY(i.income_date)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@month", month),
                new SqlParameter("@year", year)
            };

            return db.ExecuteQuery(query, parameters);
        }
        public DataTable GetTotalBudgets(int userId, int month, int year)
        {
            string query = @"
                SELECT * 
                FROM Budgets 
                WHERE user_id = @userId 
                AND budget_type = 'total'
                AND MONTH(start_date) = @month 
                AND YEAR(start_date) = @year";

            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@month", month),
                new SqlParameter("@year", year)
            };

            return db.ExecuteQuery(query, parameters);
        }
        public DataTable GetCategoryBudgets(int userId, int month, int year)
        {
            string query = @"
                SELECT 
                    b.budget_id,
                    b.user_id,
                    b.category_id,
                    c.category_name,
                    b.amount,
                    b.start_date,
                    b.end_date,
                    b.budget_type,
                    b.warning_threshold
                FROM Budgets b
                LEFT JOIN Categories c ON b.category_id = c.category_id
                WHERE b.user_id = @userId 
                AND b.budget_type = 'category'
                AND MONTH(b.start_date) = @month 
                AND YEAR(b.start_date) = @year";

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
