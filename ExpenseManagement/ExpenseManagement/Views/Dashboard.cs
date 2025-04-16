using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using ExpenseManagement.Core;
using ExpenseManagement.Controllers;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json.Linq;
using Windows.Networking.NetworkOperators;
using ExpenseManagement.Controller;
using System.Runtime.Remoting.Messaging;
using System.Drawing;
using Windows.ApplicationModel.Chat;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Net.Mime.MediaTypeNames;

namespace ExpenseManagement.Views
{
    public partial class Dashboard : Form
    {
        private int _userID;
        private readonly GeminiApiService _geminiApiService;
        private readonly TransactionController _transactionController;
        private string _isQuickExpenseMode = "normal";
        private CategoryController _category;
        private UserController _userController;
        private BudgetMainController _budgetController;
        private MessageManager messageManager;
        private ExpensesController _expenseController;
        private IncomesController _incomesController;
        private string _userName;
        private string _botName = "Moni - Trợ lý tài chính";
        private string _message;
        private CategoryGroupController _categoryGroup;

        private ToolTip toolTip = new ToolTip();
        public Dashboard(int userID, string fullMessage)
        {
            InitializeComponent();
            _userID = userID;
            _userController = new UserController();
            _geminiApiService = new GeminiApiService();
            _transactionController = new TransactionController();
            _category = new CategoryController();
            _budgetController = new BudgetMainController();
            _expenseController = new ExpensesController();
            _incomesController = new IncomesController();
            _userName = _userController.GetFullName(_userID).ToString();
        }

        public async void ProcessMessage(string fullMessage)
        {
            if (!string.IsNullOrWhiteSpace(fullMessage))
            {
                string[] parts = fullMessage.Split(new[] { " - " }, StringSplitOptions.None);

                if (parts.Length == 2)
                {
                    string text = parts[0].Trim();
                    string message = parts[1].Trim();

                    await HandleMessage(text, message);
                }
            }
        }

        private async void OnMessageClicked(string combinedMessage)
        {
            string[] parts = combinedMessage.Split(new[] { " | " }, StringSplitOptions.None);
            string text = parts[0];
            string message = parts.Length > 1 ? parts[1] : "";

            await HandleMessage(text, message);
        }

        private async Task HandleMessage(string text, string message)
        {
            switch (message)
            {
                case "createBudget":
                    HandleCreateBudget(text);
                    break;
                case "analysis":
                    await HandleAnalysisAsync(text);
                    break;
                case "createIncome":
                    HandleCreateIncome(text);
                    break;
                //case "createSaving":
                //    await HandleCreateSaving(text);
                    //break;
                case "savingsSuggestion":
                    await HandleSavingsSuggestion(text);
                    break;
                case "createExpense":
                    HandleCreateExpense(text);
                    break;
                case "createCategory":
                    HandleCreateCategory(text);
                    break;
                //case "abnormalSpending":
                //    await HandleAbnormalSpending(text);
                //    break;
                default:
                    await HandleDefaultMessage(text, message);
                    break;
            }
        }
        private async Task HandleAbnormalSpending(string text)
        {
            // Hàm tạm thời chưa có nội dung
        }
        private void HandleCreateBudget(string text)
        {

            if (_isQuickExpenseMode == "createBudget")
            {
                string reminderMessage = CuteResponseGenerator.GetQuickBudgetReminder();
                AddMessage(reminderMessage, _botName, false);
                return;
            }

            string quickExpenseMessage = CuteResponseGenerator.GetQuickBudgetMessage();
            AddMessage(quickExpenseMessage, _botName, false);
            _isQuickExpenseMode = "createBudget";
        }

        private async Task HandleAnalysisAsync(string text)
        {
            _isQuickExpenseMode = "analysis";

            string quickExpenseMessage = CuteResponseGenerator.GetQuickFinacialAnalistMessage();
            AddMessage(quickExpenseMessage, _botName, false);

            JObject response = await GetBotResponseAsync("Phân xu hướng tài chính cá nhân");

            if (response != null && response["status"]?.ToString() == "success")
            {
                JArray trendData = response["trend_data"] as JArray ?? new JArray();
                string message = response["message"]?.ToString() ?? "Không có phân tích.";

                AddMessage(message, _botName, false);

                if (trendData.Count > 0)
                {
                    Control trendChart = CreateTrendChart(trendData, flpMessages.Width - 20);
                    flpMessages.Invoke(new Action(() =>
                    {
                        flpMessages.Controls.Add(trendChart);
                        flpMessages.ScrollControlIntoView(trendChart);
                    }));
                }
            }
            else
            {
                AddMessage("Không thể phân tích tài chính ngay bây giờ. Vui lòng thử lại sau.", _botName, false);
            }
            LoadItemChat();
            _isQuickExpenseMode = "normal";

        }
        private Control CreateTrendChart(JArray trendData, int containerWidth)
        {
            int minWidth = (int)(flpMessages.ClientSize.Width * 0.9); // Ít nhất 90% chiều rộng
            int calculatedWidth = Math.Max(minWidth, trendData.Count * 30); // Tăng width theo dữ liệu

            Chart chart = new Chart
            {
                Width = calculatedWidth,
                Height = 250,
                BorderlineDashStyle = ChartDashStyle.Solid,
                BorderlineColor = Color.Gray,
                BackColor = Color.White
            };

            ChartArea chartArea = new ChartArea
            {
                AxisX = { Title = "Ngày", Interval = 1, LineWidth = 1, LineColor = Color.Black },
                AxisY = { Title = "Số tiền (VNĐ)", LabelStyle = { Format = "N0" }, LineWidth = 1, LineColor = Color.Black }
            };

            chart.ChartAreas.Add(chartArea);

            Series incomeSeries = new Series("Thu nhập")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Green,
                BorderWidth = 2
            };

            Series expenseSeries = new Series("Chi tiêu")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Red,
                BorderWidth = 2
            };

            foreach (var item in trendData)
            {
                int day = item["day"]?.Value<int>() ?? 0;
                decimal income = item["income"]?.Value<decimal>() ?? 0;
                decimal expense = item["expense"]?.Value<decimal>() ?? 0;

                incomeSeries.Points.AddXY(day, income);
                expenseSeries.Points.AddXY(day, expense);
            }

            chart.Series.Add(incomeSeries);
            chart.Series.Add(expenseSeries);

            Panel panel = new Panel
            {
                Width = calculatedWidth,
                Height = 270,
                BackColor = Color.White,
                AutoScroll = true // Bật cuộn nếu nội dung lớn hơn panel
            };

            chart.Dock = DockStyle.Fill;
            panel.Controls.Add(chart);

            return panel;
        }
        private Control CreateSavingSuggestionChart(JArray trendData, int width)
        {
            Chart chart = new Chart
            {
                Width = width,
                Height = 300,
                BackColor = Color.White
            };

            ChartArea chartArea = new ChartArea
            {
                AxisX = { Title = "Ngày", Interval = 1 },
                AxisY = { Title = "Số tiền (VNĐ)" }
            };

            chart.ChartAreas.Add(chartArea);

            Series incomeSeries = new Series("Thu nhập")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Green,
                BorderWidth = 2
            };

            Series expenseSeries = new Series("Chi tiêu")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Red,
                BorderWidth = 2
            };

            Series savingSeries = new Series("Tiết kiệm")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Blue,
                BorderWidth = 2
            };

            foreach (JObject dataPoint in trendData)
            {
                int day = dataPoint["day"].ToObject<int>();
                decimal income = dataPoint["income"].ToObject<decimal>();
                decimal expense = dataPoint["expense"].ToObject<decimal>();
                decimal saving = dataPoint["saving"].ToObject<decimal>();

                incomeSeries.Points.AddXY(day, income);
                expenseSeries.Points.AddXY(day, expense);
                savingSeries.Points.AddXY(day, saving);
            }

            chart.Series.Add(incomeSeries);
            chart.Series.Add(expenseSeries);
            chart.Series.Add(savingSeries);

            return chart;
        }

        private void HandleCreateIncome(string text)
        {
            if (_isQuickExpenseMode == "income")
            {
                string reminderMessage = CuteResponseGenerator.GetQuickIncomeMessage();
                AddMessage(reminderMessage, _botName, false);
                return;
            }

            string quickExpenseMessage = CuteResponseGenerator.GetQuickIncomeMessage();
            AddMessage(quickExpenseMessage, _botName, false);
            _isQuickExpenseMode = "income";

        }

        //private async Task HandleCreateSaving(string text)
        //{

        //}

        private async Task HandleSavingsSuggestion(string text)
        {
            _isQuickExpenseMode = "savingsSuggestion";

            string quickExpenseMessage = CuteResponseGenerator.GetQuickSavingSuggestionsMessage();
            AddMessage(quickExpenseMessage, _botName, false);

            JObject response = await GetBotResponseAsync("Gợi ý tiết kiệm dựa vào dữ liệu của tôi");

            if (response != null && response["status"]?.ToString() == "success")
            {
                JArray trendData = response["trend_data"] as JArray ?? new JArray();
                string message = response["message"]?.ToString() ?? "Không có phân tích.";

                AddMessage(message, _botName, false);

                if (trendData.Count > 0)
                {
                    Control trendChart = CreateSavingSuggestionChart(trendData, flpMessages.Width - 20);
                    flpMessages.Invoke(new Action(() =>
                    {
                        flpMessages.Controls.Add(trendChart);
                        flpMessages.ScrollControlIntoView(trendChart);
                    }));
                }
            }
            else
            {
                AddMessage("Không thể phân tích tài chính ngay bây giờ. Vui lòng thử lại sau.", _botName, false);
            }
            _isQuickExpenseMode = "normal";

            LoadItemChat();
        }

        private void HandleCreateExpense(string text)
        {
            if (_isQuickExpenseMode == "expense")
            {
                string reminderMessage = CuteResponseGenerator.GetExpenseModeReminder();
                AddMessage(reminderMessage, _botName, false);
                return;
            }

            string quickExpenseMessage = CuteResponseGenerator.GetQuickExpenseMessage();
            AddMessage(quickExpenseMessage, _botName, false);
            _isQuickExpenseMode = "expense";
        }


        private void HandleCreateCategory(string text)
        {
            if (_isQuickExpenseMode == "createCategoryExpense")
            {
                string reminderMessage = CuteResponseGenerator.GetCategoryModeReminder();
                AddMessage(reminderMessage, _botName, false);
                return;
            }

            string quickExpenseMessage = CuteResponseGenerator.GetQuickCreateCategoryIncometMessage();
            AddMessage(quickExpenseMessage, _botName, false);
            _isQuickExpenseMode = "createCategoryExpense";
        }


        private async Task HandleDefaultMessage(string text, string message)
        {
            _isQuickExpenseMode = "normal";
            AddMessage(message, _userName, true);
            string contentSuccess = await _geminiApiService.ChatWithAIAsync(message, "vi");
            AddMessage(contentSuccess, _botName, false);
        }


        private async void Dashboard_Load(object sender, EventArgs e)
        {
            LoadData();
            await LoadInitialDataAsync();
            _expenseController.CheckRecuringExpense(_userID);
            _incomesController.CheckRecurringIncome(_userID);
        }
        private void LoadData()
        {
            //lbNameChatBot.Text  = _botName;
        }
        private async Task LoadInitialDataAsync()
        {
            try
            {
                var expensesData = _transactionController.GetTransactionsByMonth(DateTime.Now.Year, DateTime.Now.Month, _userID);
                var filteredData = _transactionController.FilterExpensesData(expensesData);

                await _geminiApiService.InitializeAnalysisAsync(filteredData, "vi");

                var welcomeMessage = CuteResponseGenerator.GetWelcomeMessage();
                AddMessage(welcomeMessage, _botName, false);
            }
            catch (Exception)
            {
                var errorMessage = CuteResponseGenerator.GetErrorParsingResponse();
                AddMessage(errorMessage, _botName, false);
            }

            
            lbStatus.Text = "Đã sẵn sàng bạn có thể hỏi bất kỳ câu hỏi nào liên quan đến tài chính tôi sẽ trâ lời ngay!";
            LoadItemChat();


        }
        public async void LoadItemChat()
        {

            FlowLayoutPanel tempPanel = new FlowLayoutPanel
            {
                Padding = new Padding(0, 10, 0, 10),
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
            };

            messageManager = new MessageManager(tempPanel, OnMessageClicked);
            await messageManager.ShowMessagesAsync();

            tempPanel.MaximumSize = new Size(1150, 0);
            flpMessages.Controls.Add(tempPanel);
        }

        // Thao tác nhanh

        private void AddMessage(string message, string senderName, bool isUser)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            var chatMessage = new ChatMessageControl(message, senderName, isUser)
            {
                Margin = new Padding(10)
            };

            flpMessages.Controls.Add(chatMessage);
            flpMessages.ScrollControlIntoView(chatMessage);
            flpMessages.PerformLayout();
        }

        private async Task<bool> DetectModeAndSetState(string userMessage)
        {
            var messages = new List<(string text, string message)>
            {
                ("Tạo ngân sách tổng nhanh", "createBudget"),
                ("Phân xu hướng tài chính cá nhân", "analysis"),
                ("Cảnh báo chi tiêu bất thường", "abnormalSpending"),
                ("Tạo mục tiêu tiết kiệm", "createSaving"),
                ("Gợi ý tiết kiệm thông minh", "savingsSuggestion"),
                ("Ghi chép chi tiêu nhanh", "createExpense"),
                ("Ghi chép thu nhập nhanh", "createIncome"),
                ("Tạo danh mục nhanh", "createCategory"),
            };

            string lowerUserMessage = userMessage.ToLower();
            const double threshold = 0.7;

            var bestMatch = messages
                .Select(m => new
                {
                    m.text,
                    m.message,
                    similarity =  CalculateSimilarity(lowerUserMessage, m.text.ToLower())
                })
                .OrderByDescending(m => m.similarity)
                .FirstOrDefault();

            if (bestMatch != null && bestMatch.similarity >= threshold)
            {
                _isQuickExpenseMode = bestMatch.message;
                AddMessage($"Đã chuyển sang chế độ **{bestMatch.text}**.", _botName, false);
                await HandleMessage(bestMatch.text, bestMatch.message);
                return true;
            }

            return false;
        }
        private double CalculateSimilarity(string source, string target)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(target))
                return 0.0;

            int stepsToSame = ComputeLevenshteinDistance(source, target);
            return 1.0 - (double)stepsToSame / Math.Max(source.Length, target.Length);
        }

        private int ComputeLevenshteinDistance(string source, string target)
        {
            int[,] matrix = new int[source.Length + 1, target.Length + 1];

            for (int i = 0; i <= source.Length; i++)
                matrix[i, 0] = i;
            for (int j = 0; j <= target.Length; j++)
                matrix[0, j] = j;

            for (int i = 1; i <= source.Length; i++)
            {
                for (int j = 1; j <= target.Length; j++)
                {
                    int cost = (source[i - 1] == target[j - 1]) ? 0 : 1;
                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }

            return matrix[source.Length, target.Length];
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            string userMessage = txtUserMessage.Text.Trim();
            if (string.IsNullOrEmpty(userMessage)) return;

            txtUserMessage.Clear();
            AddMessage(userMessage, _userName, true);

            bool isModeChanged = await DetectModeAndSetState(userMessage);
            if (isModeChanged) return;
            if (_isQuickExpenseMode == "normal")
            {
                string contenSuccess = await _geminiApiService.ChatWithAIAsync(userMessage, "vi");

                AddMessage(contenSuccess, _botName, false);
            }

            JObject botResponse = await GetBotResponseAsync(userMessage);

            if (botResponse == null)
            {
                return;
            }

            if (botResponse != null)
            {
                string status = botResponse["status"]?.ToString();
                string message = botResponse["message"]?.ToString();
                string type = botResponse["type"]?.ToString();
                if ( type == "other" || type == "error" )
                {
                    _isQuickExpenseMode = "normal";
                    LoadItemChat();
                } 
                else if ( type == "create_category_expense")
                {
                    // Tạo chi tiêu và danh mục
                    _isQuickExpenseMode = "expense";
                    int expenseId = botResponse["expense_id"]?.ToObject<int>() ?? 0;
                    int categoryId = botResponse["category_id"]?.ToObject<int>() ?? 0;
                    if (categoryId > 0)
                    {
                        DataTable budgetFinish = _expenseController.GetExpenseByIdCreate(_userID, categoryId);
                        if (budgetFinish.Rows.Count > 0)
                        {
                            CategoryFinsh itemBudget = new CategoryFinsh(budgetFinish);

                            itemBudget.OnActionClick += BudgetItem_ActionClick;
                            flpMessages.Controls.Add(itemBudget);
                        }
                    }
                    if (expenseId > 0)
                    {
                        DataTable expenseFinish = _expenseController.GetExpenseByIdCreate(_userID, expenseId);
                        if (expenseFinish.Rows.Count > 0)
                        {
                            ExpenseFinish itemExpense = new ExpenseFinish(expenseFinish);

                            itemExpense.OnActionClick += HandleTransactionAction;
                            flpMessages.Controls.Add(itemExpense);
                        }
                    }
                }
                else if (type == "expense_entry")
                {
                    // Tạo chi tiêu
                    _isQuickExpenseMode = "expense";

                    if (botResponse != null && botResponse["expense_id"] != null && botResponse["expense_id"].Type == JTokenType.Integer)
                    {
                        int expenseId = botResponse["expense_id"].ToObject<int>();

                        if (expenseId > 0)
                        {
                            DataTable expenseFinish = _expenseController.GetExpenseByIdCreate(_userID, expenseId);
                            if (expenseFinish.Rows.Count > 0)
                            {
                                ExpenseFinish itemExpense = new ExpenseFinish(expenseFinish);
                                itemExpense.OnActionClick += HandleTransactionAction;
                                flpMessages.Controls.Add(itemExpense);
                            }
                        }
                    }
                }

                else if (type == "income_entry")
                {
                    // Tạo thu nhập
                    _isQuickExpenseMode = "income";
                    int incomeId = botResponse["income_id"]?.ToObject<int>() ?? 0;
                    if (incomeId > 0)
                    {
                        DataTable icomeFinish = _incomesController.GetIncomeByIdCreate(_userID, incomeId);
                        if (icomeFinish.Rows.Count > 0)
                        {
                            IncomeFinish itemIcome = new IncomeFinish(icomeFinish);

                            itemIcome.OnActionClick += HandleTransactionAction;
                            flpMessages.Controls.Add(itemIcome);
                        }
                    }
                }
                else if (type == "create_category_income")
                {
                    // Tạo thu nhập và danh mục
                    _isQuickExpenseMode = "income";
                    int incomeId = botResponse["income_id"]?.ToObject<int>() ?? 0;
                    int categoryId = botResponse["category_id"]?.ToObject<int>() ?? 0;
                    if (incomeId > 0)
                    {
                        DataTable icomeFinish = _incomesController.GetIncomeByIdCreate(_userID, incomeId);
                        if (icomeFinish.Rows.Count > 0)
                        {
                            IncomeFinish itemIcome = new IncomeFinish(icomeFinish);

                            itemIcome.OnActionClick += HandleTransactionAction;
                            flpMessages.Controls.Add(itemIcome);
                        }
                    }
                    if (categoryId > 0)
                    {
                        DataTable budgetFinish = _budgetController.GetBudgetById(_userID, categoryId);
                        if (budgetFinish.Rows.Count > 0)
                        {
                            BudgetChatItem itemBudget = new BudgetChatItem(budgetFinish);

                            itemBudget.OnActionClick += BudgetItem_ActionClick;
                            flpMessages.Controls.Add(itemBudget);
                        }
                    }
                }
                else if (type == "budget_entry")
                {
                    _isQuickExpenseMode = "createBudget";
                    int budgetId = botResponse["budget_id"]?.ToObject<int>() ?? 0;

                    if (budgetId > 0)
                    {
                        DataTable budgetFinish = _budgetController.GetBudgetById(_userID, budgetId);
                        if (budgetFinish.Rows.Count > 0)
                        {
                            BudgetChatItem itemBudget = new BudgetChatItem(budgetFinish);

                            itemBudget.OnActionClick += BudgetItem_ActionClick;
                            flpMessages.Controls.Add(itemBudget);
                        }
                    }
                }
                else if (type == "category_create")
                {
                    _isQuickExpenseMode = "createCategoryExpense";
                    int categoryId = botResponse["category_id"]?.ToObject<int>() ?? 0;
                    if (categoryId > 0) {

                        DataTable CategoryFinish = _category.GetCategoryById(_userID, categoryId);
                        if (CategoryFinish.Rows.Count > 0) {
                            itemCategoryChat itemCategoryChat = new itemCategoryChat(CategoryFinish);
                            itemCategoryChat.OnActionClick += CategoryItem_ActionClick;
                            flpMessages.Controls.Add(itemCategoryChat);
                        }
                    }
                }
                else if (type == "category_group")
                {
                    _isQuickExpenseMode = "createCategoryExpense";
                    int categoryId = botResponse["category_id"]?.ToObject<int>() ?? 0;
                    int categoryGroupId = botResponse["category_group_id"]?.ToObject<int>() ?? 0;
                    if (categoryGroupId > 0) {
                        DataTable data = _categoryGroup.GetCategoryGroupById(_userID, categoryId);
                        if (data.Rows.Count > 0)
                        {
                            CategoryFinsh itemCategoryChat = new CategoryFinsh(data);
                            itemCategoryChat.OnActionClick += BudgetItem_ActionClick;
                            flpMessages.Controls.Add(itemCategoryChat);
                            LoadItemChat();
                        }
                    }
                    if (categoryId > 0)
                    {

                        DataTable CategoryFinish = _category.GetCategoryById(_userID, categoryId);
                        if (CategoryFinish.Rows.Count > 0)
                        {
                            itemCategoryChat itemCategoryChat = new itemCategoryChat(CategoryFinish);
                            itemCategoryChat.OnActionClick += CategoryItem_ActionClick;
                            flpMessages.Controls.Add(itemCategoryChat);
                        }

                    }

                }
                else if (type == "update_category_group")
                {
                    _isQuickExpenseMode = "createCategoryExpense";
                    int categoryId = botResponse["category_id"]?.ToObject<int>() ?? 0;
                    int categoryGroupId = botResponse["category_group_id"]?.ToObject<int>() ?? 0;
                    if (categoryGroupId > 0)
                    {
                        DataTable data = _categoryGroup.GetCategoryGroupById(_userID, categoryId);
                        if (data.Rows.Count > 0)
                        {
                            CategoryFinsh itemCategoryChat = new CategoryFinsh(data);
                            itemCategoryChat.OnActionClick += BudgetItem_ActionClick;
                            flpMessages.Controls.Add(itemCategoryChat);
                            LoadItemChat();
                        }
                    }
                    if (categoryId > 0)
                    {

                        DataTable CategoryFinish = _category.GetCategoryById(_userID, categoryId);
                        if (CategoryFinish.Rows.Count > 0)
                        {
                            itemCategoryChat itemCategoryChat = new itemCategoryChat(CategoryFinish);
                            itemCategoryChat.OnActionClick += CategoryItem_ActionClick;
                            flpMessages.Controls.Add(itemCategoryChat);
                        }

                    }

                }
                else if (type == "financial_analysis")
                {
                    //Tạo chi tiêu
                    _isQuickExpenseMode = "normal";
                }
                else if (type == "savings_analysis")
                {
                    //Tạo chi tiêu
                    _isQuickExpenseMode = "normal";
                }
                else
                {
                    _isQuickExpenseMode = "normal";
                }

                if (!string.IsNullOrEmpty(message))
                {
                    AddMessage(message, _botName, false);
                }

            }

        }
        private void BudgetItem_ActionClick(int budgetId, string action)
        {
            if (action == "delete" &&
                MessageBox.Show($"Bạn có chắc muốn xóa ID: {budgetId}?", "Xác nhận",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int deletedBudgetId = _budgetController.DeleteTotalBudget(budgetId, _userID);

                string message = deletedBudgetId > 0
                                 ? $"Xóa thành công!\nID: {budgetId}"
                                 : $"Xóa thất bại!\nID: {budgetId}";

                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK,
                                deletedBudgetId > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
            else if (action == "edit")
            {
                Home.GetInstance(_userID)?.ProcessToastAction($"openBudget|{budgetId}");
            }
        }
          
        private void CategoryItem_ActionClick(int categoryId, string action)
        {
            if (action == "delete" &&
                MessageBox.Show($"Bạn có chắc muốn xóa ID: {categoryId}?", "Xác nhận",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int deletedBudgetId = _category.DeleteCategory( _userID, categoryId);

                string message = deletedBudgetId > 0
                                 ? $"Xóa thành công!\nID: {categoryId}"
                                 : $"Xóa thất bại!\nID: {categoryId}";

                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK,
                                deletedBudgetId > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
            else if (action == "edit")
            {
                Home.GetInstance(_userID)?.ProcessToastAction($"openCategory|{categoryId}");
            }
        }

        private void HandleTransactionAction(int transactionId, string action, string type)
        {
            if (action == "edit")
            {
                if (type == "expense")
                {
                    Home.GetInstance(_userID)?.ProcessToastAction($"openExpense|{transactionId}");
                }
                else if (type == "income")
                {
                    Home.GetInstance(_userID)?.ProcessToastAction($"openIncome|{transactionId}");
                }
            }
            else if (action == "delete")
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa giao dịch này?", "Xác nhận xóa",
                                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Xóa dữ liệu khỏi database
                    if (type == "expense")
                    {
                        _expenseController.DeleteExpense(transactionId, _userID);
                    }
                    else if (type == "income")
                    {
                        _incomesController.DeleteIncome(transactionId, _userID);
                    }

                    // Xóa item khỏi giao diện
                    Control itemToRemove = null;
                    foreach (Control control in flpMessages.Controls)
                    {
                        if (control is TransactionItem transactionItem && transactionItem.TransactionId == transactionId)
                        {
                            itemToRemove = control;
                            break;
                        }
                    }

                    if (itemToRemove != null)
                    {
                        flpMessages.Controls.Remove(itemToRemove);
                        itemToRemove.Dispose();
                    }
                }
            }
        }


        private async Task<JObject> GetBotResponseAsync(string message)
        {
            DataTable categories = _category.GetAllCategories(_userID);
            DataTable categoryGroups = _category.GetAllCategoriesGroup();

            JObject response = new JObject();

            if (_isQuickExpenseMode == "expense")
            {
                response = await _geminiApiService.QuickExpenseEntryAsync(message, categories, categoryGroups, _userID);
                return new JObject
                {
                    { "status", response["status"] },
                    { "message", response["message"] },
                    { "type", response["type"] },
                    { "expense_id", response["expense_id"] },
                };
            }
            else if (_isQuickExpenseMode == "income")
            {
                response = await _geminiApiService.QuickIncomeEntryAsync(message, categories, categoryGroups, _userID);
                return new JObject
                {
                    { "status", response["status"] },
                    { "message", response["message"] },
                    { "type", response["type"] },
                    { "income_id", response["income_id"] }
                };
            }
            else if (_isQuickExpenseMode == "createBudget")
            {
                DataTable budgetTotal = _budgetController.GetMonthlyBudgets(_userID);
                response = await _geminiApiService.QuickBudgetEntryAsync(message, budgetTotal, _userID);

                return new JObject
                {
                    { "status", response["status"] },
                    { "message", response["message"] },
                    { "type", response["type"] },
                    { "budget_id", response["budget_id"] }
                };
            }
            else if (_isQuickExpenseMode == "createCategoryExpense")
            {
                response = await _geminiApiService.QuickCategoryEntryAsync(message, categories, categoryGroups, _userID);
                return new JObject
                {
                    { "status", response["status"] },
                    { "message", response["message"] },
                    { "type", response["type"] },
                    { "category_group_id", response["category_group_id"] },
                    { "category_id", response["category_id"] }
                };
            }
            else if (_isQuickExpenseMode == "analysis")
            {
                response = await _geminiApiService.QuickAnalysisEntryAsync(message,_userID);
                return new JObject
                {
                    { "status", response["status"] },
                    { "message", response["message"] },
                    { "type", response["type"] },
                    { "trend_data", response["trend_data"] },
                    { "ai_analysis", response["ai_analysis"] }
                };
            }
            if (_isQuickExpenseMode == "savingsSuggestion")
            {
                response = await _geminiApiService.QuickSavingsAnalysisAsync(message, _userID);
                return new JObject
                {
                    { "status", response["status"] },
                    { "message", response["message"] },
                    { "type", response["type"] },
                    { "trend_data", response["trend_data"] },
                    { "ai_analysis", response["ai_analysis"] }
                };
            }
            else if (_isQuickExpenseMode == "normal")
            {
                await _geminiApiService.ChatWithAIAsync(message, "vi");
            }
            else
            {
                await _geminiApiService.ChatWithAIAsync(message, "vi");
            }

            return new JObject
            {
                { "status", response["status"]?.ToString() },
                { "message", response["message"]?.ToString() },
                { "type", response["type"]?.ToString() }
            };
        }

        private void txtUserMessage_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSend_Click(sender, e);
            }
        }

        private void txtUserMessage_TextChanged(object sender, EventArgs e)
        {

        }

        private void picClose_Click(object sender, EventArgs e)
        {

            System.Windows.Forms.Application.Exit();

        }

        private void btnSend_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(btnSend, "Gửi tin nhắn");
        }
    }
}
