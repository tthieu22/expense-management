using ExpenseManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExpenseManagement.Controllers
{
    public class TransactionController
    {
        private readonly TransactionModel _transactionModel;

        public TransactionController()
        {
            _transactionModel = new TransactionModel();
        }
        public List<Dictionary<string, object>> GetRecentTransactions(int userId)
        {
            return _transactionModel.GetRecentTransactions(userId);
        }

        public bool AddTransaction(int userId, string transactionType, decimal amount, DateTime date, int categoryId, int expenseId)
        {
            return _transactionModel.AddTransaction(userId, transactionType, amount, date, categoryId, expenseId);
        }

        public bool UpdateTransaction(int transactionId, int userId, string transactionType, decimal amount, DateTime date, int categoryId, int expenseId)
        {
            return _transactionModel.UpdateTransaction(transactionId, userId, transactionType, amount, date, categoryId, expenseId);
        }

        public bool DeleteTransaction(int transactionId, int userId)
        {
            return _transactionModel.DeleteTransaction(transactionId, userId);
        }

        // Get transactions by date
        public List<Dictionary<string, object>> GetTransactionsByDate(DateTime date, int userId)
        {
            return _transactionModel.GetTransactionsByDate(date, userId);
        }

        // Get transactions by month
        public List<Dictionary<string, object>> GetTransactionsByMonth(int year, int month, int userId)
        {
            return _transactionModel.GetTransactionsByMonth(year, month, userId);
        }

        // Get transactions by year
        public List<Dictionary<string, object>> GetTransactionsByYear(int year, int userId)
        {
            return _transactionModel.GetTransactionsByYear(year, userId);
        }

        // New method to calculate total for Expense and Income grouped by date
        public Dictionary<string, decimal> GetTotalByDateGroupedByType(DateTime date, int userId)
        {
            var transactions = GetTransactionsByDate(date, userId);
            var totalByType = new Dictionary<string, decimal>
            {
                { "Expense", 0 },
                { "Income", 0 }
            };

            foreach (var transaction in transactions)
            {
                string transactionType = transaction["transaction_type"].ToString();
                decimal amount = Convert.ToDecimal(transaction["amount"]);

                if (transactionType == "Expense")
                    totalByType["Expense"] += amount;
                else if (transactionType == "Income")
                    totalByType["Income"] += amount;
            }

            return totalByType;
        }

        // New method to calculate total for Expense and Income grouped by month
        public Dictionary<string, decimal> GetTotalByMonthGroupedByType(int year, int month, int userId)
        {
            var transactions = GetTransactionsByMonth(year, month, userId);
            var totalByType = new Dictionary<string, decimal>
            {
                { "Expense", 0 },
                { "Income", 0 }
            };

            foreach (var transaction in transactions)
            {
                string transactionType = transaction["transaction_type"].ToString();
                decimal amount = Convert.ToDecimal(transaction["amount"]);

                if (transactionType == "Expense")
                    totalByType["Expense"] += amount;
                else if (transactionType == "Income")
                    totalByType["Income"] += amount;
            }

            return totalByType;
        }

        // New method to calculate total for Expense and Income grouped by year
        public Dictionary<string, decimal> GetTotalByYearGroupedByType(int year, int userId)
        {
            var transactions = GetTransactionsByYear(year, userId);
            var totalByType = new Dictionary<string, decimal>
            {
                { "Expense", 0 },
                { "Income", 0 }
            };

            foreach (var transaction in transactions)
            {
                string transactionType = transaction["transaction_type"].ToString();
                decimal amount = Convert.ToDecimal(transaction["amount"]);

                if (transactionType == "Expense")
                    totalByType["Expense"] += amount;
                else if (transactionType == "Income")
                    totalByType["Income"] += amount;
            }

            return totalByType;
        }

        // Method to get total Expense and Income grouped by each day in a month
        public List<Dictionary<string, object>> GetTotalByDayGroupedByType(int year, int month, int userId)
        {
            var transactions = GetTransactionsByMonth(year, month, userId);
            var dailyTotals = new Dictionary<DateTime, Dictionary<string, decimal>>();

            for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
            {
                var currentDay = new DateTime(year, month, day);
                dailyTotals[currentDay] = new Dictionary<string, decimal>
                {
                    { "Expense", 0 },
                    { "Income", 0 }
                };
            }

            foreach (var transaction in transactions)
            {
                DateTime transactionDate = Convert.ToDateTime(transaction["date"]).Date;
                string transactionType = transaction["transaction_type"]?.ToString()?.Trim();
                decimal amount = Convert.ToDecimal(transaction["amount"]);

                if (string.IsNullOrEmpty(transactionType))
                {
                }

                if (!dailyTotals.ContainsKey(transactionDate))
                {
                    continue;
                }

                if (transactionType == "Expense")
                {
                    dailyTotals[transactionDate]["Expense"] += amount;
                }
                else if (transactionType == "Income")
                {
                    dailyTotals[transactionDate]["Income"] += amount;
                }
                else
                {
                }
            }

            var result = new List<Dictionary<string, object>>();
            foreach (var day in dailyTotals)
            {
                result.Add(new Dictionary<string, object>
                {
                    { "Date", day.Key },
                    { "Expense", day.Value["Expense"] },
                    { "Income", day.Value["Income"] }
                });
            }

            return result;
        }

        public Dictionary<int, Dictionary<string, decimal>> GetTotalByMonthInYearGroupedByType(int year, int userId)
        {
            Dictionary<int, Dictionary<string, decimal>> monthlyTotals = new Dictionary<int, Dictionary<string, decimal>>();

            for (int month = 1; month <= 12; month++)
            { 
                var totals = GetTotalByMonthGroupedByType(year, month, userId);
                 
                monthlyTotals.Add(month, totals);
            }

            return monthlyTotals;
        }
        public Dictionary<int, Dictionary<string, decimal>> GetTotalByQuarterInYearGroupedByType(int year, int userId)
        {
            Dictionary<int, Dictionary<string, decimal>> quarterlyTotals = new Dictionary<int, Dictionary<string, decimal>>();

            for (int quarter = 1; quarter <= 4; quarter++)
            {
                decimal expenseTotal = 0;
                decimal incomeTotal = 0;
                 
                int startMonth = (quarter - 1) * 3 + 1;
                int endMonth = quarter * 3;
                 
                for (int month = startMonth; month <= endMonth; month++)
                {
                    var totals = GetTotalByMonthGroupedByType(year, month, userId);
                    expenseTotal += totals["Expense"];
                    incomeTotal += totals["Income"];
                }
                 
                quarterlyTotals.Add(quarter, new Dictionary<string, decimal>
                {
                    { "Expense", expenseTotal },
                    { "Income", incomeTotal }
                });
            }

            return quarterlyTotals;
        }
        public List<Dictionary<string, object>> FilterExpensesData(List<Dictionary<string, object>> expensesData)
        {
            List<Dictionary<string, object>> filteredData = new List<Dictionary<string, object>>();

            for (int i = 0; i < expensesData.Count; i++)
            {
                var expense = expensesData[i];
                var filteredExpense = new Dictionary<string, object>
                {
                    { "transaction_type", expense.ContainsKey("transaction_type") ? expense["transaction_type"] : null },
                    { "amount", expense.ContainsKey("amount") ? expense["amount"] : null },
                    { "date", expense.ContainsKey("date") ? expense["date"] : null },
                    { "category_name", expense.ContainsKey("category_name") ? expense["category_name"] : null }
                };
                filteredData.Add(filteredExpense);
            }

            return filteredData;
        }


    }
}
