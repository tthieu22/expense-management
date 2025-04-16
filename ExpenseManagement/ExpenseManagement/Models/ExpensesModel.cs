using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseManagement.Model
{
    public class ExpensesModel
    {
        private readonly Connect db = new Connect();

        public int RecordExpense(int userId, int categoryId, decimal amount, DateTime expenseDate,
                         string description = null, string paymentMethod = null, int recurring = 0,
                         string location = null, string tags = null, string imagePath = null, DateTime? endDate = null)
        {
            string query = @"
                INSERT INTO Expenses (user_id, category_id, amount, expense_date, 
                                      description, payment_method, recurring, location, tags, image_path, created_at, end_date)
                VALUES (@user_id, @category_id, @amount, @expense_date, 
                        @description, @payment_method, @recurring, @location, @tags, @image_path, GETDATE(), @end_date);
                SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters =
            {
                new SqlParameter("@user_id", userId),
                new SqlParameter("@category_id", categoryId),
                new SqlParameter("@amount", amount),
                new SqlParameter("@expense_date", expenseDate),
                new SqlParameter("@description", description ?? (object)DBNull.Value),
                new SqlParameter("@payment_method", paymentMethod ?? (object)DBNull.Value),
                new SqlParameter("@recurring", recurring),
                new SqlParameter("@location", location ?? (object)DBNull.Value),
                new SqlParameter("@tags", tags ?? (object)DBNull.Value),
                new SqlParameter("@image_path", imagePath ?? (object)DBNull.Value),
                new SqlParameter("@end_date", endDate ?? (object)DBNull.Value),
            };

            object result = db.ExecuteScalar(query, parameters);

            return result != null ? Convert.ToInt32(result) : 0;  // Trả về ID hoặc 0 nếu có lỗi
        }

        public int UpdateExpense(int expenseId, int userId, int categoryId, decimal amount, DateTime expenseDate,
                           string description = null, string paymentMethod = null, int recurring = 0,
                           string location = null, string tags = null, string imagePath = null, DateTime? endDate = null)
        {
            string query = @"
                UPDATE Expenses 
                SET category_id = @category_id, amount = @amount, expense_date = @expense_date, 
                    description = @description, payment_method = @payment_method, recurring = @recurring, 
                    location = @location, tags = @tags, image_path = @image_path, updated_at = GETDATE(),
                    end_date = @end_date
                WHERE expense_id = @expense_id AND user_id = @user_id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@expense_id", expenseId),
                new SqlParameter("@user_id", userId),
                new SqlParameter("@category_id", categoryId),
                new SqlParameter("@amount", amount),
                new SqlParameter("@expense_date", expenseDate),
                new SqlParameter("@description", description ?? (object)DBNull.Value),
                new SqlParameter("@payment_method", paymentMethod ?? (object)DBNull.Value),
                new SqlParameter("@recurring", recurring),
                new SqlParameter("@location", location ?? (object)DBNull.Value),
                new SqlParameter("@tags", tags ?? (object)DBNull.Value),
                new SqlParameter("@image_path", imagePath ?? (object)DBNull.Value),
                new SqlParameter("@end_date", endDate ?? (object)DBNull.Value), 
            };
            bool success = db.ExecuteNonQuery(query, parameters);
            return success ? 1 : 0;
        }
        public int DeleteExpense(int expenseId, int userId)
        {
            string query = @"
                DELETE FROM Expenses 
                WHERE expense_id = @expense_id AND user_id = @user_id;
        
                IF @@ROWCOUNT > 0 
                    SELECT @expense_id 
                ELSE 
                    SELECT 0;";

            SqlParameter[] parameters =
            {
                new SqlParameter("@expense_id", expenseId),
                new SqlParameter("@user_id", userId)
            };

            object result = db.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public DataTable GetExpenses(int userId)
        {
            string query = "SELECT * FROM Expenses WHERE user_id = @user_id ORDER BY expense_date DESC";
            SqlParameter[] parameters = { new SqlParameter("@user_id", userId) };

            return db.ExecuteQuery(query, parameters);
        }

        public DataTable SearchExpenses(int userId, string keyword, DateTime? startDate = null, DateTime? endDate = null)
        {
            string query = @"
                SELECT * FROM Expenses 
                WHERE user_id = @user_id 
                AND (description LIKE @keyword OR location LIKE @keyword OR tags LIKE @keyword)";

            if (startDate.HasValue) query += " AND expense_date >= @start_date";
            if (endDate.HasValue) query += " AND expense_date <= @end_date";

            query += " ORDER BY expense_date DESC";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@user_id", userId),
                new SqlParameter("@keyword", "%" + keyword + "%")
            };

            if (startDate.HasValue) parameters.Add(new SqlParameter("@start_date", startDate.Value));
            if (endDate.HasValue) parameters.Add(new SqlParameter("@end_date", endDate.Value));

            return db.ExecuteQuery(query, parameters.ToArray());
        }
        public DataTable GetExpensesByDay(int userId, DateTime date)
        {
            string query = @"
                SELECT * FROM Expenses 
                WHERE user_id = @user_id AND CAST(expense_date AS DATE) = @date
                ORDER BY expense_date DESC";

                    SqlParameter[] parameters =
                    {
                new SqlParameter("@user_id", userId),
                new SqlParameter("@date", date.Date)
            };

            return db.ExecuteQuery(query, parameters);
        }

        public DataTable GetExpensesByMonth(int userId, int year, int month)
        {
            string query = @"
                SELECT * FROM Expenses 
                WHERE user_id = @user_id AND YEAR(expense_date) = @year AND MONTH(expense_date) = @month
                ORDER BY expense_date DESC";

                    SqlParameter[] parameters =
                    {
                new SqlParameter("@user_id", userId),
                new SqlParameter("@year", year),
                new SqlParameter("@month", month)
            };

            return db.ExecuteQuery(query, parameters);
        }

        public DataTable GetExpensesByQuarter(int userId, int year, int quarter)
        {
            string query = @"
                SELECT * FROM Expenses 
                WHERE user_id = @user_id AND YEAR(expense_date) = @year 
                AND DATEPART(QUARTER, expense_date) = @quarter
                ORDER BY expense_date DESC";

                    SqlParameter[] parameters =
                    {
                new SqlParameter("@user_id", userId),
                new SqlParameter("@year", year),
                new SqlParameter("@quarter", quarter)
            };

            return db.ExecuteQuery(query, parameters);
        }

        public DataTable GetExpensesByYear(int userId, int year)
        {
            string query = @"
                SELECT * FROM Expenses 
                WHERE user_id = @user_id AND YEAR(expense_date) = @year
                ORDER BY expense_date DESC";

                    SqlParameter[] parameters =
                    {
                new SqlParameter("@user_id", userId),
                new SqlParameter("@year", year)
            };

            return db.ExecuteQuery(query, parameters);
        }
        public DataTable GetExpenseByIdCreate(int userId, int expenseId)
        {
            string query = @"
                SELECT 
                    e.expense_id, e.user_id, e.category_id, e.amount, e.expense_date, 
                    e.description, e.payment_method, e.created_at, e.updated_at, 
                    e.recurring, e.location, e.tags, e.image_path, e.end_date,
                    c.category_name, c.category_type, c.category_group_id, 
                    c.category_icon_char, c.category_icon_url
                FROM Expenses e
                INNER JOIN Categories c ON e.category_id = c.category_id
                WHERE e.user_id = @user_id AND e.expense_id = @expense_id
                ORDER BY e.expense_date DESC";

            SqlParameter[] parameters =
            {
                new SqlParameter("@user_id", userId),
                new SqlParameter("@expense_id", expenseId)
            };

            return db.ExecuteQuery(query, parameters);
        }

        public void CheckRecuringExpense(int userId)
        {
            string query = @"
            DECLARE @CurrentDate DATE = CAST(GETDATE() AS DATE);

            -- Lặp từng bản ghi recurring của user
            DECLARE cur CURSOR FOR
            SELECT expense_id, user_id, category_id, amount, expense_date, description,
                   payment_method, created_at, recurring, location, tags, image_path, end_date
            FROM [expense_management].[dbo].[Expenses]
            WHERE recurring IS NOT NULL AND recurring <> 0
              AND CAST(GETDATE() AS DATE) <= CAST(end_date AS DATE)
              AND user_id = @user_id;

            DECLARE @expense_id INT, @user_id INT, @category_id INT, @recurring INT;
            DECLARE @amount DECIMAL(18, 2), @expense_date DATE, @description NVARCHAR(MAX);
            DECLARE @payment_method NVARCHAR(50), @created_at DATETIME, @location NVARCHAR(255);
            DECLARE @tags NVARCHAR(255), @image_path NVARCHAR(255), @end_date DATETIME;

            OPEN cur;
            FETCH NEXT FROM cur INTO @expense_id, @user_id, @category_id, @amount, @expense_date, @description,
                                    @payment_method, @created_at, @recurring, @location, @tags, @image_path, @end_date;

            WHILE @@FETCH_STATUS = 0
            BEGIN
                DECLARE @next_expense_date DATE = NULL;

                WHILE 1 = 1
                BEGIN
                    IF @recurring = 1 SET @next_expense_date = DATEADD(DAY, 1, @expense_date);
                    ELSE IF @recurring = 2 SET @next_expense_date = DATEADD(DAY, 7, @expense_date);
                    ELSE IF @recurring = 3 SET @next_expense_date = DATEADD(MONTH, 1, @expense_date);
                    ELSE IF @recurring = 4 SET @next_expense_date = DATEADD(MONTH, 3, @expense_date);
                    ELSE IF @recurring = 5 SET @next_expense_date = DATEADD(YEAR, 1, @expense_date);
                    ELSE BREAK;

                    IF @next_expense_date IS NULL 
                       OR @next_expense_date > CAST(@end_date AS DATE) 
                       OR @next_expense_date > @CurrentDate
                    BEGIN
                        BREAK;
                    END

                    IF NOT EXISTS (
                        SELECT 1 FROM [expense_management].[dbo].[Expenses]
                        WHERE user_id = @user_id
                          AND category_id = @category_id
                          AND amount = @amount
                          AND expense_date = @next_expense_date
                          AND description = @description
                          AND payment_method = @payment_method
                          AND recurring = @recurring
                          AND location = @location
                          AND tags = @tags
                          AND ISNULL(image_path, '') = ISNULL(@image_path, '')
                          AND CAST(end_date AS DATE) = CAST(@end_date AS DATE)
                    )
                    BEGIN
                        INSERT INTO [expense_management].[dbo].[Expenses]
                        ([user_id], [category_id], [amount], [expense_date], [description],
                         [payment_method], [created_at], [recurring], [location], [tags],
                         [image_path], [end_date])
                        VALUES
                        (@user_id, @category_id, @amount, @next_expense_date, @description,
                         @payment_method, GETDATE(), @recurring, @location, @tags,
                         @image_path, @end_date)
                    END

                    SET @expense_date = @next_expense_date;
                END

                IF @next_expense_date IS NOT NULL AND @next_expense_date > CAST(@end_date AS DATE)
                BEGIN
                    UPDATE [expense_management].[dbo].[Expenses]
                    SET recurring = 0, updated_at = GETDATE()
                    WHERE expense_id = @expense_id;
                END

                FETCH NEXT FROM cur INTO @expense_id, @user_id, @category_id, @amount, @expense_date, @description,
                                        @payment_method, @created_at, @recurring, @location, @tags, @image_path, @end_date;
            END

            CLOSE cur;
            DEALLOCATE cur;
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@user_id", userId)
            };

            db.ExecuteNonQuery(query, parameters); // dùng ExecuteNonQuery thay vì ExecuteQuery
        }
        public void CheckRecurringExpense()
        {
            string query = @"
            DECLARE @CurrentDate DATE = CAST(GETDATE() AS DATE);

            -- Duyệt qua toàn bộ bản ghi recurring còn hiệu lực trong bảng Expenses
            DECLARE cur CURSOR FOR
            SELECT expense_id, user_id, category_id, amount, expense_date, description,
                   payment_method, created_at, recurring, location, tags, image_path, end_date
            FROM [expense_management].[dbo].[Expenses]
            WHERE recurring IS NOT NULL AND recurring <> 0
              AND CAST(GETDATE() AS DATE) <= CAST(end_date AS DATE);

            DECLARE @expense_id INT, @user_id INT, @category_id INT, @recurring INT;
            DECLARE @amount DECIMAL(18, 2), @expense_date DATE, @description NVARCHAR(MAX);
            DECLARE @payment_method NVARCHAR(50), @created_at DATETIME, @location NVARCHAR(255);
            DECLARE @tags NVARCHAR(255), @image_path NVARCHAR(255), @end_date DATETIME;

            OPEN cur;
            FETCH NEXT FROM cur INTO @expense_id, @user_id, @category_id, @amount, @expense_date, @description,
                                        @payment_method, @created_at, @recurring, @location, @tags, @image_path, @end_date;

            WHILE @@FETCH_STATUS = 0
            BEGIN
                DECLARE @next_expense_date DATE = NULL;

                WHILE 1 = 1
                BEGIN
                    IF @recurring = 1 SET @next_expense_date = DATEADD(DAY, 1, @expense_date);
                    ELSE IF @recurring = 2 SET @next_expense_date = DATEADD(DAY, 7, @expense_date);
                    ELSE IF @recurring = 3 SET @next_expense_date = DATEADD(MONTH, 1, @expense_date);
                    ELSE IF @recurring = 4 SET @next_expense_date = DATEADD(MONTH, 3, @expense_date);
                    ELSE IF @recurring = 5 SET @next_expense_date = DATEADD(YEAR, 1, @expense_date);
                    ELSE BREAK;

                    IF @next_expense_date IS NULL 
                       OR @next_expense_date > CAST(@end_date AS DATE) 
                       OR @next_expense_date > @CurrentDate
                    BEGIN
                        BREAK;
                    END

                    IF NOT EXISTS (
                        SELECT 1 FROM [expense_management].[dbo].[Expenses]
                        WHERE user_id = @user_id
                          AND category_id = @category_id
                          AND amount = @amount
                          AND expense_date = @next_expense_date
                          AND description = @description
                          AND payment_method = @payment_method
                          AND recurring = @recurring
                          AND location = @location
                          AND tags = @tags
                          AND ISNULL(image_path, '') = ISNULL(@image_path, '')
                          AND CAST(end_date AS DATE) = CAST(@end_date AS DATE)
                    )
                    BEGIN
                        INSERT INTO [expense_management].[dbo].[Expenses]
                        ([user_id], [category_id], [amount], [expense_date], [description],
                         [payment_method], [created_at], [recurring], [location], [tags],
                         [image_path], [end_date])
                        VALUES
                        (@user_id, @category_id, @amount, @next_expense_date, @description,
                         @payment_method, GETDATE(), @recurring, @location, @tags,
                         @image_path, @end_date)
                    END

                    SET @expense_date = @next_expense_date;
                END

                IF @next_expense_date IS NOT NULL AND @next_expense_date > CAST(@end_date AS DATE)
                BEGIN
                    UPDATE [expense_management].[dbo].[Expenses]
                    SET recurring = 0, updated_at = GETDATE()
                    WHERE expense_id = @expense_id;
                END

                FETCH NEXT FROM cur INTO @expense_id, @user_id, @category_id, @amount, @expense_date, @description,
                                            @payment_method, @created_at, @recurring, @location, @tags, @image_path, @end_date;
            END

            CLOSE cur;
            DEALLOCATE cur;
        ";

            db.ExecuteNonQuery(query);
        }

    }
}
