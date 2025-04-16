using ExpenseManagement.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseManagement.Controllers
{
    internal class BudgetMainController
    {
        private BudgetMainModel budgetModel;

        public BudgetMainController()
        {
            budgetModel = new BudgetMainModel();
        }

        // 🟢 Thêm ngân sách tổng
        public bool AddTotalBudget(int userId, decimal amount, int warningThreshold, DateTime from, DateTime to)
        {
            return budgetModel.AddTotalBudget(userId, amount, warningThreshold, from, to);
        }

        public int AddTotalBudgetWithId(int userId, decimal amount, DateTime from, DateTime to)
        {
            return budgetModel.AddTotalBudgetId(userId, amount, from, to);
        }

        // 🔵 Cập nhật ngân sách tổng
        public int UpdateTotalBudget(int userId, int budgetId, decimal amount, int warningThreshold, DateTime startDate, DateTime endDate)
        {
            return budgetModel.UpdateTotalBudget( userId, budgetId, amount, warningThreshold, startDate, endDate);
        }

        // 🔴 Xóa ngân sách tổng
        public int DeleteTotalBudget(int budgetId, int userId)
        {
            return budgetModel.DeleteTotalBudget(budgetId, userId);
        }

        // 🟡 Thêm ngân sách danh mục
        public bool AddCategoryBudget(int userId, int categoryId, decimal amount, int warningThreshold, DateTime from, DateTime to)
        {
            return budgetModel.AddCategoryBudget(userId, categoryId, amount, warningThreshold, from, to);
        }
        // 🟠 Cập nhật ngân sách danh mục
        public bool UpdateCategoryBudget(int budgetId, int userId, int categoryId, decimal amount, int warningThreshold, DateTime from, DateTime to)
        {
            return budgetModel.UpdateCategoryBudget(budgetId, userId, categoryId, amount, warningThreshold, from, to);
        }
        // 🔴 Xóa ngân sách danh mục
        public bool DeleteCategoryBudget(int budgetId, int userId)
        {
            return budgetModel.DeleteCategoryBudget(budgetId, userId);
        }

        // 🟣 Lấy tổng ngân sách của người dùng
        public decimal GetTotalBudget(int userId, int month, int year)
        {
            return budgetModel.GetTotalBudget(userId, month, year);
        }


        // 🔵 Lấy tổng chi tiêu trong tháng
        public decimal GetTotalExpensesByMonth(int userId, int month, int year)
        {
            return budgetModel.GetTotalExpensesByMonth(userId, month, year);
        }

        // 🟢 Lấy báo cáo ngân sách chi tiết theo danh mục
        public DataTable GetBudgetSummary(int userId, int month, int year)
        {
            return budgetModel.GetBudgetSummary(userId, month, year);
        }

        public DataTable GetBudgetAll(int userId)
        {
            return budgetModel.GetBudgetAll(userId);
        }
        public DataTable GetMonthlyBudgets(int userId)
        {
            return budgetModel.GetMonthlyBudgets(userId);
        }
        public DataTable GetTotalBudgets(int userId, int month, int year)
        {
            return budgetModel.GetTotalBudgets(userId, month, year);
        }

        public DataTable GetCategoryBudgets(int userId, int month, int year)
        {
            return budgetModel.GetCategoryBudgets(userId, month, year);
        }


        public DataTable GetDailyExpensesByMonth(int userId, int month, int year)
        {
            return budgetModel.GetDailyExpensesByMonth(userId, month, year);
        }

        public DataTable GetDailyIncomeByMonth(int userId, int month, int year)
        {
            return budgetModel.GetDailyIncomeByMonth(userId, month, year);
        }
        public DataTable GetExpensesByCategory(int userId, int month, int year)
        {
            return budgetModel.GetExpensesByCategory(userId, month, year);
        }

        public DataTable GetBudgetById(int userId, int budgetId)
        {
            return budgetModel.GetBudgetById(userId, budgetId);

        }

    }
}
