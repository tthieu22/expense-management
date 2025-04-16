using ExpenseManagement.Models;
using ExpenseManagement.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseManagement.Controllers
{
    internal class SavingMainController
    {
        private readonly SavingMainModel savingModel = new SavingMainModel();

        public decimal GetAvailableBalance(int userId, int month, int year)
        {
            decimal income = savingModel.GetMonthlyIncome(userId, month, year);
            decimal expenses = savingModel.GetMonthlyExpenses(userId, month, year);
            decimal savings = savingModel.GetSavedAmount(userId, month, year);
            return income - expenses - savings;
        }

        public Dictionary<string, decimal> SuggestExpenseReduction(int userId, int month, int year)
        {
            var topExpenses = savingModel.GetTopExpenseCategories(userId, month, year);
            var reductionSuggestions = new Dictionary<string, decimal>();

            foreach (var expense in topExpenses)
            {
                reductionSuggestions[expense.Key] = expense.Value * 0.15m;
            }
            return reductionSuggestions;
        }
        

        public decimal GetSuggestedMonthlySaving(int userId, int month, int year)
        {
            decimal availableBalance = GetAvailableBalance(userId, month, year);
            return availableBalance > 0 ? availableBalance * 0.50m : 0;
        }

        // Lấy danh sách nhắc nhở tiết kiệm cho tháng và năm cụ thể
        public List<Dictionary<string, object>> GetSavingReminders(int userId, int month, int year)
        {
            return savingModel.GetSavingReminders(userId, month, year);
        }
            
        public string GetSavingReminder(int userId, int month, int year)
        {
            decimal targetSaving = savingModel.GetSuggestedFutureSaving(userId, month, year);
            decimal savedAmount = savingModel.GetSavedAmount(userId, month, year);

            string checkIcon = "\u2705";
            string warningIcon = "\u26A0";
            string moneyIcon = "\uD83D\uDCB8";
            string chartIcon = "\uD83D\uDCCA";
            string clockIcon = "\u23F0";

            return savedAmount >= targetSaving
                ? $"{checkIcon} Gợi ý tiết kiệm tháng này\n{chartIcon} Mục tiêu tiết kiệm: {targetSaving:N0} VND\n{moneyIcon} Bạn đã đạt mục tiêu tiết kiệm! Tiếp tục duy trì thói quen này."
                : $"{warningIcon} Gợi ý tiết kiệm tháng này\n{chartIcon} Mục tiêu tiết kiệm: {targetSaving:N0} VND\n{moneyIcon} Bạn chưa đạt mục tiêu tiết kiệm!\n{clockIcon} Hãy chuyển thêm {(targetSaving - savedAmount):N0} VND vào tài khoản tiết kiệm.";
        }

        public string GetSavingMethodSuggestion() => "Bạn có thể áp dụng phương pháp 50/30/20 để tiết kiệm.";

        public Dictionary<string, Tuple<decimal, decimal>> GetSavingPlan(int userId, int year)
        {
            return savingModel.GetSavingPlan(userId, year);
        }

        public bool AddSavingGoal(int userId, string goalName, decimal targetAmount, int months, string description)
        {
            return savingModel.AddSavingGoal(userId, goalName, targetAmount, months, description);
        }

        public bool UpdateSavingGoal(int goalId, string goalName, decimal targetAmount, decimal savedAmount, DateTime targetDate, string status, int categoryId, int priorityLevel, string description)
        {
            return savingModel.UpdateSavingGoal(goalId, goalName, targetAmount, savedAmount, targetDate, status, categoryId, priorityLevel, description);
        }

        public bool DeleteSavingGoal(int goalId, int userId)
        {
            return savingModel.DeleteSavingGoal(goalId, userId);
        }

        public DataTable GetUserSavings(int userId)
        {
            return savingModel.GetSavingGoalsByUser(userId);
        }
        public Dictionary<string, decimal> GetTopExpenseCategories(int userId, int month, int year)
        {
            return savingModel.GetTopExpenseCategories(userId, month, year, 5);
        }

        public decimal GetYearlyIncome(int userId, int year)
        {
            return savingModel.GetYearlyIncome(userId, year);
        }

        public decimal GetYearlyExpenses(int userId, int year)
        {
            return savingModel.GetYearlyExpenses(userId, year);
        }

        public decimal GetYearlySavedAmount(int userId, int year)
        {
            return savingModel.GetYearlySavedAmount(userId, year);
        }

        public DataTable GetExpenseDetails(int userId, int month, int year)
        {
            return savingModel.GetExpenseDetails(userId, month, year);
        }

        public decimal GetMonthlyIncome(int userId, int month, int year)
        {
            return savingModel.GetMonthlyIncome(userId, month, year);
        }

        public decimal GetMonthlyExpenses(int userId, int month, int year)
        {
            return savingModel.GetMonthlyExpenses(userId, month, year);

        }

        public decimal GetMonthlyBudget(int userId, int month, int year)
        {
            return savingModel.GetMonthlyBudget(userId, month, year);

        }

        
        public DataTable GetTop5ExpensesInMonth(int userId, int month, int year)
        {
            return savingModel.GetTop5ExpensesInMonth(userId, month, year);
        }

        //Lấy tất cả các muc tiêu chưa hoàn thành
        public DataTable GetUnfinishedGoals(int userId)
        {
            return savingModel.GetUnfinishedGoals(userId);
        }

        // 13. Dự đoán số dư tài chính cuối tháng
        public decimal PredictMonthEndBalance(int userId, int month, int year)
        {
            return savingModel.PredictMonthEndBalance(userId, month,  year);
        }

        // 6. Dự đoán chi tiêu tháng tới
        public decimal PredictNextMonthExpenses(int userId)
        {
            return savingModel.PredictNextMonthExpenses(userId);
        }

        // 3. Gợi ý số tiền tiết kiệm
        public decimal SuggestSavingAmount(int userId , int month)
        {
            return savingModel.SuggestSavingAmount(userId, month);
        }

        // Add saving
        public bool AddSavingAmount(int userId, int goalId, decimal amount)
        {
            return savingModel.UpdateSavedAmount(userId, goalId, amount);
        }

        //Check goal
        public int CheckSavingGoalStatus(int userId, int goalId)
        {
            return savingModel.CheckSavingGoalStatus(userId, goalId);
        }

        // Tỉ lệ hoàn thành mục tiêu toàn bộ
        public decimal CalculateTotalCompletionRate(int userId)
        {
            return savingModel.CalculateTotalCompletionRate(userId);

        }


        // Tỉ lệ hoàn thanh mục tiêu trong năm
        public decimal CalculateCompletionRateByYear(int userId, int year)
        {
            return savingModel.CalculateCompletionRateByYear(userId, year);

        }

    }
}
