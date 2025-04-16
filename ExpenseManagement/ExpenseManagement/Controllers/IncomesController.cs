using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ExpenseManagement.Model;
using ExpenseManagement.Views;

namespace ExpenseManagement.Controller
{
    public class IncomesController
    {
        private readonly IncomesModel incomesModel = new IncomesModel();

        public void CheckRecurringIncome()
        {
            incomesModel.CheckRecurringIncome();
        }
        public void CheckRecurringIncome(int id)
        {
            incomesModel.CheckRecurringIncome();
        }
        public int RecordIncome(int userId, int categoryId, decimal amount, DateTime incomeDate,
                           string description = null, string paymentMethod = null, int recurring = 0,
                           string location = null, string tags = null, string imagePath = null, DateTime? endDate = null)
        {
            return incomesModel.RecordIncome(userId, categoryId, amount, incomeDate,
                                              description, paymentMethod, recurring, location, tags, imagePath, endDate);
        }

        public int UpdateIncome(int incomeId, int userId, int categoryId, decimal amount, DateTime incomeDate,
                        string description = null, string paymentMethod = null, int recurring = 0,
                        string location = null, string tags = null, string imagePath = null, DateTime? endDate = null)
        {
            return incomesModel.UpdateIncome(incomeId, userId, categoryId, amount, incomeDate,
                                              description, paymentMethod, recurring, location, tags, imagePath, endDate);
        }


        public int DeleteIncome(int incomeId, int userId)
        {
            return incomesModel.DeleteIncome(incomeId, userId);
        }

        public DataTable GetIncomes(int userId)
        {
            return incomesModel.GetIncomes(userId);
        }

        public DataTable SearchIncomes(int userId, string keyword, DateTime? startDate = null, DateTime? endDate = null)
        {
            return incomesModel.SearchIncomes(userId, keyword, startDate, endDate);
        }

        public DataTable GetIncomesByDay(int userId, DateTime date)
        {
            return incomesModel.GetIncomesByDay(userId, date);
        }

        public DataTable GetIncomesByMonth(int userId, int year, int month)
        {
            return incomesModel.GetIncomesByMonth(userId, year, month);
        }

        public DataTable GetIncomesByQuarter(int userId, int year, int quarter)
        {
            return incomesModel.GetIncomesByQuarter(userId, year, quarter);
        }

        public DataTable GetIncomesByYear(int userId, int year)
        {
            return incomesModel.GetIncomesByYear(userId, year);
        }

        public string GetSavingSuggestions(int userId, int year, int month)
        {
            DataTable incomes = GetIncomesByMonth(userId, year, month);
            if (incomes.Rows.Count == 0)
                return "Bạn chưa có thu nhập trong tháng này. Hãy bắt đầu ghi chép thu nhập để theo dõi.";

            decimal totalIncome = incomes.AsEnumerable().Sum(row => row.Field<decimal>("amount"));
            var categorySummary = incomes.AsEnumerable()
                .GroupBy(row => row.Field<int>("category_id"))
                .Select(g => new
                {
                    CategoryId = g.Key,
                    Total = g.Sum(row => row.Field<decimal>("amount"))
                })
                .OrderByDescending(g => g.Total)
                .ToList();

            string suggestion = $"Tổng thu nhập tháng {month}/{year}: {totalIncome:C}\n";

            if (categorySummary.Count > 0)
            {
                suggestion += "Danh mục thu nhập nhiều nhất:\n";
                foreach (var category in categorySummary.Take(3))
                {
                    suggestion += $"- Danh mục {category.CategoryId}: {category.Total:C}\n";
                }

                var topCategory = categorySummary.First();
                if (topCategory.Total > (totalIncome * 0.4m))
                {
                    suggestion += $"Bạn đã có thu nhập quá nhiều từ danh mục {topCategory.CategoryId}. Hãy xem xét tiết kiệm thêm từ đây!\n";
                }
            }
            return suggestion;
        }

        public DataTable GetMonthlyIncomes(int userId, int year, int month)
        {
            return incomesModel.GetIncomesByMonth(userId, year, month);
        }
        public DataTable GetIncomeByIdCreate(int userId, int id) { 
            return incomesModel.GetIncomeByIdCreate(userId, id);
        }
    }
}
