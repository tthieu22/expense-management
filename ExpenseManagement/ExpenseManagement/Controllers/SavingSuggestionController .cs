using System;
using System.Data;
using ExpenseManagement.Model;

namespace ExpenseManagement.Controller
{
    internal class SavingSuggestionController
    {
        private readonly SavingSuggestionModel model = new SavingSuggestionModel();

        // 1. Lấy tổng thu nhập
        public decimal GetTotalIncome(int userId)
        {
            return model.GetTotalIncome(userId);
        }

        // 2. Lấy tổng chi tiêu
        public decimal GetTotalExpenses(int userId)
        {
            return model.GetTotalExpenses(userId);
        }

        // 3. Gợi ý số tiền tiết kiệm
        public decimal SuggestSavingAmount(int userId)
        {
            return model.SuggestSavingAmount(userId);
        }

        // 4. Lấy 5 khoản chi tiêu lớn nhất
        public DataTable GetTop5Expenses(int userId)
        {
            return model.GetTop5Expenses(userId);
        }

        // 5. Kiểm tra đạt mục tiêu tiết kiệm
        public bool CheckSavingGoal(int userId, decimal goalAmount)
        {
            return model.CheckSavingGoal(userId, goalAmount);
        }

        // 6. Dự đoán chi tiêu tháng tới
        public decimal PredictNextMonthExpenses(int userId)
        {
            return model.PredictNextMonthExpenses(userId);
        }

        // 7. Kiểm tra chi tiêu vượt mức
        public bool CheckOverspending(int userId, decimal budget)
        {
            return model.CheckOverspending(userId, budget);
        }

        // 8. Gợi ý danh mục cần cắt giảm
        public DataTable SuggestCutbackCategories(int userId)
        {
            return model.SuggestCutbackCategories(userId);
        }

        // 9. Theo dõi xu hướng tiết kiệm
        public DataTable TrackSavingTrends(int userId)
        {
            return model.TrackSavingTrends(userId);
        }

        // 10. Tạo kế hoạch tiết kiệm
        public bool CreateSavingPlan(int userId,string name, decimal targetAmount, int months)
        {
            return model.CreateSavingPlan(userId,name, targetAmount, months);
        }

        // 11. Theo dõi tiến độ tiết kiệm
        public decimal TrackSavingProgress(int userId, int? month = null, int? year = null)
        {
            return model.TrackSavingProgress(userId,month,year);
        }

        // 12. Nhắc nhở khi chậm tiến độ
        public bool CheckSavingProgress(int userId, decimal expectedAmount)
        {
            return model.CheckSavingProgress(userId, expectedAmount);
        }

        // 13. Dự đoán số dư tài chính cuối tháng
        public decimal PredictMonthEndBalance(int userId)
        {
            return model.PredictMonthEndBalance(userId);
        }

        // Add saving
        public bool AddSavingAmount(int userId, int goalId, decimal amount)
        {
            return model.UpdateSavedAmount(userId, goalId, amount);
        }

        //Check goal
        public bool CheckSavingGoalStatus(int userId, int goalId)
        {
            return model.CheckSavingGoalStatus( userId,  goalId);
        }

        public DataTable GetAllSavingGoals(int userId)
        {
            return model.GetAllSavingGoals(userId);
        }
    }
}
