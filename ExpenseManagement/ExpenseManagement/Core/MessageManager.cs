using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExpenseManagement.Core;
using ExpenseManagement.Views;
using Newtonsoft.Json;

public class MessageManager
{
    private readonly FlowLayoutPanel _flowPanel;
    private readonly Action<string> _onMessageClicked;
    private readonly GeminiApiService _geminiApiService;

    public MessageManager(FlowLayoutPanel flowPanel, Action<string> onMessageClicked)
    {
        _geminiApiService = new GeminiApiService();
        _flowPanel = flowPanel;
        _onMessageClicked = onMessageClicked;
    }

    public async Task DisplayMessages()
    {
        _flowPanel.Controls.Clear();
        List<string> financialQuestions = await _geminiApiService.GetFinancialQuestionsAsync();

        List<(string text, string message)> messages = new List<(string, string)>
        {
            ("Tạo ngân sách tổng nhanh", "createBudget"),
            ("Phân tích xu hướng tài chính cá nhân", "analysis"),
            //("Cảnh báo chi tiêu bất thường", "abnormalSpending"),
            //("Tạo mục tiêu tiết kiệm", "createSaving"),
            ("Gợi ý tiết kiệm thông minh", "savingsSuggestion"),
            ("Ghi chép chi tiêu nhanh", "createExpense"),
            ("Ghi chép thu nhập nhanh", "createIncome"),
            ("Tạo danh mục nhanh", "createCategory"),
        };

        var random = new Random();
        int messageCount = Math.Min(random.Next(1, 3), financialQuestions.Count);

        var selectedMessages = financialQuestions.OrderBy(x => random.Next()).Take(messageCount)
            .Select(q => (q, q))
            .ToList();

        var selectedActions = messages.OrderBy(x => random.Next()).Take(5 - messageCount).ToList();

        var finalSelection = selectedMessages.Concat(selectedActions).OrderBy(x => random.Next()).ToList();

        foreach (var (text, message) in finalSelection)
        {
            var messageItem = new MessageItem(text, message);
            messageItem.MessageClicked += (msg) => _onMessageClicked($"{text} | {msg}");
            _flowPanel.Controls.Add(messageItem);
        }
    }
    public async Task ShowMessagesAsync()
    {
        _flowPanel.Controls.Clear();
        List<string> financialQuestions = await _geminiApiService.GetFinancialQuestionsAsync();

        List<(string text, string message)> defaultMessages = new List<(string, string)>
        {
            ("Tạo ngân sách tổng nhanh", "createBudget"),
            ("Phân tích xu hướng tài chính cá nhân", "analysis"),
            //("Cảnh báo chi tiêu bất thường", "abnormalSpending"),
            //("Tạo mục tiêu tiết kiệm", "createSaving"),
            ("Gợi ý tiết kiệm thông minh", "savingsSuggestion"),
            ("Ghi chép chi tiêu nhanh", "createExpense"),
            ("Ghi chép thu nhập nhanh", "createIncome"),
            ("Tạo danh mục nhanh", "createCategory"),
        };

        var random = new Random();
        int selectedQuestionCount = Math.Min(random.Next(1, 6), financialQuestions.Count);

        var selectedFinancialQuestions = financialQuestions.OrderBy(x => random.Next())
                                                            .Take(selectedQuestionCount)
                                                            .Select(q => (q, q))
                                                            .ToList();

        var selectedDefaultMessages = defaultMessages.ToList();

        var finalMessages = selectedFinancialQuestions.Concat(selectedDefaultMessages)
                                                     .OrderBy(x => random.Next())
                                                     .ToList();

        foreach (var (text, message) in finalMessages)
        {
            var messageItem = new MessageItem(text, message);
            messageItem.MessageClicked += (msg) => _onMessageClicked($"{text} | {msg}");
            _flowPanel.Controls.Add(messageItem);
        }
    }

}
