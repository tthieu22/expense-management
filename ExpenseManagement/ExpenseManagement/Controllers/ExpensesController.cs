using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ExpenseManagement.Model;
using ExpenseManagement.Views;

namespace ExpenseManagement.Controller
{
    public class ExpensesController
    {
        private readonly ExpensesModel expensesModel = new ExpensesModel();

        public void CheckRecurringExpense()
        {
            expensesModel.CheckRecurringExpense();
        }
        public void CheckRecuringExpense(int userid)
        {
            expensesModel.CheckRecuringExpense(userid);
        }
        public int RecordExpense(int userId, int categoryId, decimal amount, DateTime expenseDate,
                                 string description = null, string paymentMethod = null, int recurring = 0,
                                 string location = null, string tags = null, string imagePath = null, DateTime? endDate = null)
        {
            return expensesModel.RecordExpense(userId, categoryId, amount, expenseDate,
                                               description, paymentMethod, recurring, location, tags, imagePath, endDate);
        }


        public int UpdateExpense(int expenseId, int userId, int categoryId, decimal amount, DateTime expenseDate,
                             string description = null, string paymentMethod = null, int recurring = 0,
                             string location = null, string tags = null, string imagePath = null, DateTime? endDate = null)
        {
            return expensesModel.UpdateExpense(expenseId, userId, categoryId, amount, expenseDate,
                                               description, paymentMethod, recurring, location, tags, imagePath, endDate);
        }

        public int DeleteExpense(int expenseId, int userId)
        {
            return expensesModel.DeleteExpense(expenseId, userId);
        }

        public DataTable GetExpenses(int userId)
        {
            return expensesModel.GetExpenses(userId);
        }

        public DataTable SearchExpenses(int userId, string keyword, DateTime? startDate = null, DateTime? endDate = null)
        {
            return expensesModel.SearchExpenses(userId, keyword, startDate, endDate);
        }

        public DataTable GetExpensesByDay(int userId, DateTime date)
        {
            return expensesModel.GetExpensesByDay(userId, date);
        }

        public DataTable GetExpensesByMonth(int userId, int year, int month)
        {
            return expensesModel.GetExpensesByMonth(userId, year, month);
        }

        public DataTable GetExpensesByQuarter(int userId, int year, int quarter)
        {
            return expensesModel.GetExpensesByQuarter(userId, year, quarter);
        }

        public DataTable GetExpensesByYear(int userId, int year)
        {
            return expensesModel.GetExpensesByYear(userId, year);
        }

        public string GetSavingSuggestions(int userId, int year, int month)
        {
            DataTable expenses = GetExpensesByMonth(userId, year, month);
            if (expenses.Rows.Count == 0)
                return "Bạn chưa có chi tiêu trong tháng này. Hãy bắt đầu ghi chép chi tiêu để theo dõi.";

            decimal totalSpent = expenses.AsEnumerable().Sum(row => row.Field<decimal>("amount"));
            var categorySummary = expenses.AsEnumerable()
                .GroupBy(row => row.Field<int>("category_id"))
                .Select(g => new
                {
                    CategoryId = g.Key,
                    Total = g.Sum(row => row.Field<decimal>("amount"))
                })
                .OrderByDescending(g => g.Total)
                .ToList();

            string suggestion = $"Tổng chi tiêu tháng {month}/{year}: {totalSpent:C}\n";

            if (categorySummary.Count > 0)
            {
                suggestion += "Danh mục chi tiêu nhiều nhất:\n";
                foreach (var category in categorySummary.Take(3))
                {
                    suggestion += $"- Danh mục {category.CategoryId}: {category.Total:C}\n";
                }

                var topCategory = categorySummary.First();
                if (topCategory.Total > (totalSpent * 0.4m))
                {
                    suggestion += $"Bạn đã chi quá nhiều vào danh mục {topCategory.CategoryId}. Hãy xem xét cắt giảm chi tiêu ở đây!\n";
                }
            }
            return suggestion;
        }

        public DataTable GetMonthlyExpenses(int userId, int year, int month)
        {
            return expensesModel.GetExpensesByMonth(userId, year, month);
        }

        public DataTable GetExpenseByIdCreate(int userId, int id)
        {
            return expensesModel.GetExpenseByIdCreate(userId, id);
        }

    }
}
