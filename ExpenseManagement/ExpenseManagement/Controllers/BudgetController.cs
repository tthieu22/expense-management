using System;
using System.Data;
using ExpenseManagement.Model;

namespace ExpenseManagement.Controller
{
    internal class BudgetController
    {
        private BudgetModel budgetModel;

        public BudgetController()
        {
            budgetModel = new BudgetModel();
        }

        public bool AddBudget(int userId, int categoryId, decimal amount, DateTime startDate, DateTime endDate)
        {
            return budgetModel.AddBudget(userId, categoryId, amount, startDate, endDate);
        }

        public bool UpdateBudget(int budgetId, int userId, int categoryId, decimal amount, DateTime startDate, DateTime endDate)
        {
            return budgetModel.UpdateBudget(budgetId, userId, categoryId, amount, startDate, endDate);
        }

        public bool DeleteBudget(int budgetId, int userId)
        {
            return budgetModel.DeleteBudget(budgetId, userId);
        }

        public DataTable GetBudgetById(int budgetId, int userId)
        {
            return budgetModel.GetBudgetById(budgetId, userId);
        }

        public DataTable GetBudgetsByCategory(int userId, int categoryId)
        {
            return budgetModel.GetBudgetsByCategory(userId, categoryId);
        }

        public DataTable GetBudgetsInMonth(int userId, int month, int year)
        {
            return budgetModel.GetBudgetsInMonth(userId, month, year);
        }

        public DataTable GetCategoriesWithBudgetsInMonth(int userId, int month, int year)
        {
            return budgetModel.GetCategoriesWithBudgetsInMonth(userId, month, year);
        }
    }
}
