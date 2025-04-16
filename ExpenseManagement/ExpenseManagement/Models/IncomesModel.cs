using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseManagement.Model
{
    public class IncomesModel
    {
        private readonly Connect db = new Connect();

        public int RecordIncome(int userId, int categoryId, decimal amount, DateTime incomeDate,
                        string description = null, string paymentMethod = null, int recurring = 0,
                        string location = null, string tags = null, string imagePath = null, DateTime? endDate = null)
        {
            string query = @"
                INSERT INTO Incomes (user_id, category_id, amount, income_date, 
                                      description, payment_method, recurring, location, tags, image_path, created_at, end_date)
                VALUES (@user_id, @category_id, @amount, @income_date, 
                        @description, @payment_method, @recurring, @location, @tags, @image_path, GETDATE(), @end_date);
                SELECT SCOPE_IDENTITY();"
            ;

            SqlParameter[] parameters =
            {
                        new SqlParameter("@user_id", userId),
                new SqlParameter("@category_id", categoryId),
                new SqlParameter("@amount", amount),
                new SqlParameter("@income_date", incomeDate),
                new SqlParameter("@description", description ?? (object)DBNull.Value),
                new SqlParameter("@payment_method", paymentMethod ?? (object)DBNull.Value),
                new SqlParameter("@recurring", recurring),
                new SqlParameter("@location", location ?? (object)DBNull.Value),
                new SqlParameter("@tags", tags ?? (object)DBNull.Value),
                new SqlParameter("@image_path", imagePath ?? (object)DBNull.Value),
                new SqlParameter("@end_date", endDate.HasValue ? (object)DBNull.Value : DBNull.Value),
            };

            object result = db.ExecuteScalar(query, parameters);  

            return Convert.ToInt32(result);
        }

        public int UpdateIncome(int incomeId, int userId, int categoryId, decimal amount, DateTime incomeDate,
                        string description = null, string paymentMethod = null, int recurring = 0,
                        string location = null, string tags = null, string imagePath = null, DateTime? endDate = null)
        {
            string query = @"
            UPDATE Incomes 
            SET category_id = @category_id, amount = @amount, income_date = @income_date, 
                description = @description, payment_method = @payment_method, recurring = @recurring, 
                location = @location, tags = @tags, image_path = @image_path, updated_at = GETDATE(),
                end_date = @end_date
            WHERE income_id = @income_id AND user_id = @user_id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@income_id", incomeId),
                new SqlParameter("@user_id", userId),
                new SqlParameter("@category_id", categoryId),
                new SqlParameter("@amount", amount),
                new SqlParameter("@income_date", incomeDate),
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

        public int DeleteIncome(int incomeId, int userId)
        {
            string query = @"
                DECLARE @DeletedId TABLE (income_id INT);
                DELETE FROM Incomes 
                OUTPUT DELETED.income_id INTO @DeletedId
                WHERE income_id = @income_id AND user_id = @user_id;
                SELECT income_id FROM @DeletedId;
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@income_id", incomeId),
                new SqlParameter("@user_id", userId)
            };

            object result = db.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }


        public DataTable GetIncomes(int userId)
        {
            string query = "SELECT * FROM Incomes WHERE user_id = @user_id ORDER BY income_date DESC";
            SqlParameter[] parameters = { new SqlParameter("@user_id", userId) };

            return db.ExecuteQuery(query, parameters);
        }

        public DataTable SearchIncomes(int userId, string keyword, DateTime? startDate = null, DateTime? endDate = null)
        {
            string query = @"
                SELECT * FROM Incomes 
                WHERE user_id = @user_id 
                AND (description LIKE @keyword OR location LIKE @keyword OR tags LIKE @keyword)";

            if (startDate.HasValue) query += " AND income_date >= @start_date";
            if (endDate.HasValue) query += " AND income_date <= @end_date";

            query += " ORDER BY income_date DESC";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@user_id", userId),
                new SqlParameter("@keyword", "%" + keyword + "%")
            };

            if (startDate.HasValue) parameters.Add(new SqlParameter("@start_date", startDate.Value));
            if (endDate.HasValue) parameters.Add(new SqlParameter("@end_date", endDate.Value));

            return db.ExecuteQuery(query, parameters.ToArray());
        }

        public DataTable GetIncomesByDay(int userId, DateTime date)
        {
            string query = @"
                SELECT * FROM Incomes 
                WHERE user_id = @user_id AND CAST(income_date AS DATE) = @date
                ORDER BY income_date DESC";

            SqlParameter[] parameters =
            {
                new SqlParameter("@user_id", userId),
                new SqlParameter("@date", date.Date)
            };

            return db.ExecuteQuery(query, parameters);
        }

        public DataTable GetIncomesByMonth(int userId, int year, int month)
        {
            string query = @"
                SELECT * FROM Incomes 
                WHERE user_id = @user_id AND YEAR(income_date) = @year AND MONTH(income_date) = @month
                ORDER BY income_date DESC";

            SqlParameter[] parameters =
            {
                new SqlParameter("@user_id", userId),
                new SqlParameter("@year", year),
                new SqlParameter("@month", month)
            };

            return db.ExecuteQuery(query, parameters);
        }

        public DataTable GetIncomesByQuarter(int userId, int year, int quarter)
        {
            string query = @"
                SELECT * FROM Incomes 
                WHERE user_id = @user_id AND YEAR(income_date) = @year 
                AND DATEPART(QUARTER, income_date) = @quarter
                ORDER BY income_date DESC";

            SqlParameter[] parameters =
            {
                new SqlParameter("@user_id", userId),
                new SqlParameter("@year", year),
                new SqlParameter("@quarter", quarter)
            };

            return db.ExecuteQuery(query, parameters);
        }

        public DataTable GetIncomesByYear(int userId, int year)
        {
            string query = @"
                SELECT * FROM Incomes 
                WHERE user_id = @user_id AND YEAR(income_date) = @year
                ORDER BY income_date DESC";

            SqlParameter[] parameters =
            {
                new SqlParameter("@user_id", userId),
                new SqlParameter("@year", year)
            };

            return db.ExecuteQuery(query, parameters);
        }
        public DataTable GetIncomeByIdCreate(int userId, int incomeId)
        {
            string query = @"
                SELECT 
                    i.income_id, i.user_id, i.category_id, i.amount, i.income_date, 
                    i.description, i.payment_method, i.created_at, i.updated_at, 
                    i.recurring, i.location, i.tags, i.image_path, i.end_date,
                    c.category_name, c.category_type, c.category_group_id, 
                    c.category_icon_char, c.category_icon_url
                FROM Incomes i
                LEFT JOIN Categories c ON i.category_id = c.category_id
                WHERE i.user_id = @user_id AND i.income_id = @income_id
                ORDER BY i.income_date DESC";

            SqlParameter[] parameters =
            {
                new SqlParameter("@user_id", userId),
                new SqlParameter("@income_id", incomeId)
            };

            return db.ExecuteQuery(query, parameters);
        }
        public void CheckRecurringIncome(int userId)
        {
            string query = @"
            DECLARE @CurrentDate DATE = CAST(GETDATE() AS DATE);

            -- Lặp từng bản ghi recurring của user trong bảng Incomes
            DECLARE cur CURSOR FOR
            SELECT income_id, user_id, category_id, amount, income_date, description,
                   payment_method, created_at, recurring, location, tags, image_path, end_date
            FROM [expense_management].[dbo].[Incomes]
            WHERE recurring IS NOT NULL AND recurring <> 0
              AND CAST(GETDATE() AS DATE) <= CAST(end_date AS DATE)
              AND user_id = @user_id;

            DECLARE @income_id INT, @user_id INT, @category_id INT, @recurring INT;
            DECLARE @amount DECIMAL(18, 2), @income_date DATE, @description NVARCHAR(MAX);
            DECLARE @payment_method NVARCHAR(50), @created_at DATETIME, @location NVARCHAR(255);
            DECLARE @tags NVARCHAR(255), @image_path NVARCHAR(255), @end_date DATETIME;

            OPEN cur;
            FETCH NEXT FROM cur INTO @income_id, @user_id, @category_id, @amount, @income_date, @description,
                                    @payment_method, @created_at, @recurring, @location, @tags, @image_path, @end_date;

            WHILE @@FETCH_STATUS = 0
            BEGIN
                DECLARE @next_income_date DATE = NULL;

                WHILE 1 = 1
                BEGIN
                    IF @recurring = 1 SET @next_income_date = DATEADD(DAY, 1, @income_date);
                    ELSE IF @recurring = 2 SET @next_income_date = DATEADD(DAY, 7, @income_date);
                    ELSE IF @recurring = 3 SET @next_income_date = DATEADD(MONTH, 1, @income_date);
                    ELSE IF @recurring = 4 SET @next_income_date = DATEADD(MONTH, 3, @income_date);
                    ELSE IF @recurring = 5 SET @next_income_date = DATEADD(YEAR, 1, @income_date);
                    ELSE BREAK;

                    IF @next_income_date IS NULL 
                       OR @next_income_date > CAST(@end_date AS DATE) 
                       OR @next_income_date > @CurrentDate
                    BEGIN
                        BREAK;
                    END

                    IF NOT EXISTS (
                        SELECT 1 FROM [expense_management].[dbo].[Incomes]
                        WHERE user_id = @user_id
                          AND category_id = @category_id
                          AND amount = @amount
                          AND income_date = @next_income_date
                          AND description = @description
                          AND payment_method = @payment_method
                          AND recurring = @recurring
                          AND location = @location
                          AND tags = @tags
                          AND ISNULL(image_path, '') = ISNULL(@image_path, '')
                          AND CAST(end_date AS DATE) = CAST(@end_date AS DATE)
                    )
                    BEGIN
                        INSERT INTO [expense_management].[dbo].[Incomes]
                        ([user_id], [category_id], [amount], [income_date], [description],
                         [payment_method], [created_at], [recurring], [location], [tags],
                         [image_path], [end_date])
                        VALUES
                        (@user_id, @category_id, @amount, @next_income_date, @description,
                         @payment_method, GETDATE(), @recurring, @location, @tags,
                         @image_path, @end_date)
                    END

                    SET @income_date = @next_income_date;
                END

                IF @next_income_date IS NOT NULL AND @next_income_date > CAST(@end_date AS DATE)
                BEGIN
                    UPDATE [expense_management].[dbo].[Incomes]
                    SET recurring = 0, updated_at = GETDATE()
                    WHERE income_id = @income_id;
                END

                FETCH NEXT FROM cur INTO @income_id, @user_id, @category_id, @amount, @income_date, @description,
                                        @payment_method, @created_at, @recurring, @location, @tags, @image_path, @end_date;
            END

            CLOSE cur;
            DEALLOCATE cur;
        ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@user_id", userId)
            };

            db.ExecuteNonQuery(query, parameters);
        }
        public void CheckRecurringIncome()
        {
            string query = @"
            DECLARE @CurrentDate DATE = CAST(GETDATE() AS DATE);

            -- Duyệt qua toàn bộ bản ghi recurring còn hiệu lực trong bảng Incomes
            DECLARE cur CURSOR FOR
            SELECT income_id, user_id, category_id, amount, income_date, description,
                   payment_method, created_at, recurring, location, tags, image_path, end_date
            FROM [expense_management].[dbo].[Incomes]
            WHERE recurring IS NOT NULL AND recurring <> 0
              AND CAST(GETDATE() AS DATE) <= CAST(end_date AS DATE);

            DECLARE @income_id INT, @user_id INT, @category_id INT, @recurring INT;
            DECLARE @amount DECIMAL(18, 2), @income_date DATE, @description NVARCHAR(MAX);
            DECLARE @payment_method NVARCHAR(50), @created_at DATETIME, @location NVARCHAR(255);
            DECLARE @tags NVARCHAR(255), @image_path NVARCHAR(255), @end_date DATETIME;

            OPEN cur;
            FETCH NEXT FROM cur INTO @income_id, @user_id, @category_id, @amount, @income_date, @description,
                                    @payment_method, @created_at, @recurring, @location, @tags, @image_path, @end_date;

            WHILE @@FETCH_STATUS = 0
            BEGIN
                DECLARE @next_income_date DATE = NULL;

                WHILE 1 = 1
                BEGIN
                    IF @recurring = 1 SET @next_income_date = DATEADD(DAY, 1, @income_date);
                    ELSE IF @recurring = 2 SET @next_income_date = DATEADD(DAY, 7, @income_date);
                    ELSE IF @recurring = 3 SET @next_income_date = DATEADD(MONTH, 1, @income_date);
                    ELSE IF @recurring = 4 SET @next_income_date = DATEADD(MONTH, 3, @income_date);
                    ELSE IF @recurring = 5 SET @next_income_date = DATEADD(YEAR, 1, @income_date);
                    ELSE BREAK;

                    IF @next_income_date IS NULL 
                       OR @next_income_date > CAST(@end_date AS DATE) 
                       OR @next_income_date > @CurrentDate
                    BEGIN
                        BREAK;
                    END

                    IF NOT EXISTS (
                        SELECT 1 FROM [expense_management].[dbo].[Incomes]
                        WHERE user_id = @user_id
                          AND category_id = @category_id
                          AND amount = @amount
                          AND income_date = @next_income_date
                          AND description = @description
                          AND payment_method = @payment_method
                          AND recurring = @recurring
                          AND location = @location
                          AND tags = @tags
                          AND ISNULL(image_path, '') = ISNULL(@image_path, '')
                          AND CAST(end_date AS DATE) = CAST(@end_date AS DATE)
                    )
                    BEGIN
                        INSERT INTO [expense_management].[dbo].[Incomes]
                        ([user_id], [category_id], [amount], [income_date], [description],
                         [payment_method], [created_at], [recurring], [location], [tags],
                         [image_path], [end_date])
                        VALUES
                        (@user_id, @category_id, @amount, @next_income_date, @description,
                         @payment_method, GETDATE(), @recurring, @location, @tags,
                         @image_path, @end_date)
                    END

                    SET @income_date = @next_income_date;
                END

                IF @next_income_date IS NOT NULL AND @next_income_date > CAST(@end_date AS DATE)
                BEGIN
                    UPDATE [expense_management].[dbo].[Incomes]
                    SET recurring = 0, updated_at = GETDATE()
                    WHERE income_id = @income_id;
                END

                FETCH NEXT FROM cur INTO @income_id, @user_id, @category_id, @amount, @income_date, @description,
                                        @payment_method, @created_at, @recurring, @location, @tags, @image_path, @end_date;
            END

            CLOSE cur;
            DEALLOCATE cur;
        ";

            db.ExecuteNonQuery(query); // Không cần truyền tham số
        }

    }
}
