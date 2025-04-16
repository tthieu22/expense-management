using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseManagement.Model
{
    public class TransactionModel
    {
        private readonly Connect _connect;

        public TransactionModel()
        {
            _connect = new Connect();
        }

        public bool AddTransaction(int userId, string transactionType, decimal amount, DateTime date, int categoryId, int expenseId)
        {
            string query = @"
                INSERT INTO Transactions (user_id, transaction_type, amount, date, category_id, expense_id)
                VALUES (@userId, @transactionType, @amount, @date, @categoryId, @expenseId)";

            var parameters = new[]
            {
                new SqlParameter("@userId", SqlDbType.Int) { Value = userId },
                new SqlParameter("@transactionType", SqlDbType.VarChar) { Value = transactionType },
                new SqlParameter("@amount", SqlDbType.Decimal) { Value = amount },
                new SqlParameter("@date", SqlDbType.DateTime) { Value = date },
                new SqlParameter("@categoryId", SqlDbType.Int) { Value = categoryId },
                new SqlParameter("@expenseId", SqlDbType.Int) { Value = expenseId }
            };

            return _connect.ExecuteNonQuery(query, parameters);
        }

        public bool UpdateTransaction(int transactionId, int userId, string transactionType, decimal amount, DateTime date, int categoryId, int expenseId)
        {
            string query = @"
                UPDATE Transactions
                SET transaction_type = @transactionType, 
                    amount = @amount, 
                    date = @date, 
                    category_id = @categoryId, 
                    expense_id = @expenseId
                WHERE transaction_id = @transactionId AND user_id = @userId";

            var parameters = new[]
            {
                new SqlParameter("@transactionId", SqlDbType.Int) { Value = transactionId },
                new SqlParameter("@userId", SqlDbType.Int) { Value = userId },
                new SqlParameter("@transactionType", SqlDbType.VarChar) { Value = transactionType },
                new SqlParameter("@amount", SqlDbType.Decimal) { Value = amount },
                new SqlParameter("@date", SqlDbType.DateTime) { Value = date },
                new SqlParameter("@categoryId", SqlDbType.Int) { Value = categoryId },
                new SqlParameter("@expenseId", SqlDbType.Int) { Value = expenseId }
            };

            return _connect.ExecuteNonQuery(query, parameters);
        }

        public bool DeleteTransaction(int transactionId, int userId)
        {
            string query = @"
                DELETE FROM Transactions
                WHERE transaction_id = @transactionId AND user_id = @userId";

            var parameters = new[]
            {
                new SqlParameter("@transactionId", SqlDbType.Int) { Value = transactionId },
                new SqlParameter("@userId", SqlDbType.Int) { Value = userId }
            };

            return _connect.ExecuteNonQuery(query, parameters);
        }

        public List<Dictionary<string, object>> GetTransactionsByDate(DateTime date, int userId)
        {
            string query = @"
                SELECT *
                FROM Transactions t
                LEFT JOIN Categories c ON t.category_id = c.category_id
                WHERE CONVERT(DATE, t.date) = @date AND t.user_id = @userId";

            var parameters = new[]
            {
                new SqlParameter("@date", SqlDbType.Date) { Value = date },
                new SqlParameter("@userId", SqlDbType.Int) { Value = userId }
            };

            return ExecuteQuery(query, parameters);
        }

        public List<Dictionary<string, object>> GetTransactionsByMonth(int year, int month, int userId)
        {
            string query = @"
                SELECT *
                FROM 
                    Transactions t
                LEFT JOIN Categories c ON t.category_id = c.category_id
                LEFT JOIN Expenses e ON t.expense_id = e.expense_id
                LEFT JOIN Incomes i ON t.income_id = i.income_id
                LEFT JOIN CategoryGroups g ON c.category_group_id = g.category_group_id

                WHERE 
                    YEAR(t.date) = @year 
                    AND MONTH(t.date) = @month 
                    AND t.user_id = @userId";

                var parameters = new[]
                    {
                    new SqlParameter("@year", SqlDbType.Int) { Value = year },
                    new SqlParameter("@month", SqlDbType.Int) { Value = month },
                    new SqlParameter("@userId", SqlDbType.Int) { Value = userId }
                };

            return ExecuteQuery(query, parameters);
        }

        public List<Dictionary<string, object>> GetTransactionsByYear(int year, int userId)
        {
            string query = @"
                SELECT *
                FROM Transactions t
                LEFT JOIN Categories c ON t.category_id = c.category_id
                LEFT JOIN Expenses e ON t.expense_id = e.expense_id
                LEFT JOIN Incomes i ON t.income_id = i.income_id
                LEFT JOIN CategoryGroups g ON c.category_id = g.category_id
                WHERE YEAR(date) = @year AND user_id = @userId";

            var parameters = new[]
            {
                new SqlParameter("@year", SqlDbType.Int) { Value = year },
                new SqlParameter("@userId", SqlDbType.Int) { Value = userId }
            };

            return ExecuteQuery(query, parameters);
        }

        private List<Dictionary<string, object>> ExecuteQuery(string query, SqlParameter[] parameters)
        {
            DataTable dataTable = _connect.ExecuteQuery(query, parameters);
            var result = new List<Dictionary<string, object>>();

            foreach (DataRow row in dataTable.Rows)
            {
                var rowDict = new Dictionary<string, object>();
                foreach (DataColumn column in dataTable.Columns)
                {
                    rowDict[column.ColumnName] = row[column];
                }
                result.Add(rowDict);
            }

            return result;
        }
        public List<Dictionary<string, object>> GetAllTransactions(int userId)
        {
            string query = @"
            SELECT *
            FROM Transactions
            WHERE user_id = @userId";

            var parameters = new[]
            {
                new SqlParameter("@userId", SqlDbType.Int) { Value = userId }
            };

            return ExecuteQuery(query, parameters);
        }
        public List<Dictionary<string, object>> GetRecentTransactions(int userId)
        {
            string query = @"
                SELECT TOP 10 
                    t.transaction_id,
                    t.user_id,
                    t.transaction_type,
                    t.amount,
                    t.date,
                    t.category_id,
                    t.expense_id,
                    t.income_id,    
                    c.category_name,
                    g.group_name,
                    e.expense_date,
                    e.description AS expense_description,
                    e.payment_method AS expense_payment_method,
                    e.tags AS expense_tags,
                    i.income_date,
                    i.description AS income_description,
                    i.payment_method AS income_payment_method,
                    i.tags AS income_tags
                FROM Transactions t
                LEFT JOIN Categories c ON t.category_id = c.category_id
                LEFT JOIN CategoryGroups g ON c.category_group_id = g.category_group_id
                LEFT JOIN Expenses e ON t.expense_id = e.expense_id
                LEFT JOIN Incomes i ON t.income_id = i.income_id
                WHERE t.user_id = @userId
                ORDER BY t.transaction_id DESC";

            var parameters = new[]
            {
                new SqlParameter("@userId", SqlDbType.Int) { Value = userId }
            };

            return ExecuteQuery(query, parameters);
        }

    }
}
