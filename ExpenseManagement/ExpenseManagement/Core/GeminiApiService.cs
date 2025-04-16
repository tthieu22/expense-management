using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using ExpenseManagement.Controller;
using System.Windows.Forms;
using ExpenseManagement.Views;
using ExpenseManagement.Controllers;
using System.Globalization;
using System.Data;
using ExpenseManagement.Model;
using CloudinaryDotNet;
using System.Runtime.InteropServices.ComTypes;
using Windows.System;

namespace ExpenseManagement.Core
{
    public class GeminiApiService : IDisposable
    {
        private readonly HttpClient _httpClient;
        private static readonly string API_KEY = Cons.apikey;
        private readonly string API_URL;
        private readonly string LM_STUDIO_URL = "http://localhost:1234/v1/chat/completions";

        private static string _detailedAnalysis = "";
        private static DateTime _lastAnalysisTime = DateTime.MinValue;
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(30);


        private CategoryController _category = new CategoryController();
        private ExpensesController _expenseController = new ExpensesController();
        private IncomesController _incomeController = new IncomesController();
        private BudgetMainController _budgetMainController = new BudgetMainController();
        private CategoryGroupController _categoryGroupController = new CategoryGroupController();
        private SavingMainController _savingController = new SavingMainController();    
        private int _month;
        private int _year;
        //Service
        public GeminiApiService()
        {
            if (string.IsNullOrWhiteSpace(API_KEY))
                throw new InvalidOperationException("Lỗi: Chưa có API Key.");

            API_URL = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={API_KEY}";
            _httpClient = new HttpClient();

            _month = DateTime.Now.Month;
            _year = DateTime.Now.Year;
        }

        // Khởi động phân tích
        public async Task InitializeAnalysisAsync(List<Dictionary<string, object>> expensesData, string language = "vi")
        {
            _detailedAnalysis = await AnalyzeExpensesAsync(expensesData, "detailed", language);
            _lastAnalysisTime = DateTime.Now;
        }
        // Phân tích chi tiêu
        public async Task<string> AnalyzeExpensesAsync(List<Dictionary<string, object>> expensesData, string mode = "summary", string language = "vi")
        {
            if (mode == "summary" && !string.IsNullOrEmpty(_detailedAnalysis) && DateTime.Now - _lastAnalysisTime < CacheDuration)
                return GenerateShortSummary(_detailedAnalysis);

            string prompt = mode == "summary"
            ? $"Bạn là Moni - một trợ lý tài chính cá nhân. Dựa trên phân tích trước đó, hãy tóm tắt ngắn gọn, chỉ cần các ý chính. Trả lời ngắn dưới 10 câu bằng ngôn ngữ: {language}."
            : $"Bạn là Moni - một trợ lý tài chính cá nhân. Dưới đây là dữ liệu chi tiêu của người dùng: {JsonConvert.SerializeObject(expensesData, Formatting.None)}. "
              + $"Hãy phân tích và đưa ra lời khuyên cô đọng, dễ hiểu, chỉ nêu ý chính. "
              + $"Giới hạn câu trả lời dưới 10. Trả lời bằng ngôn ngữ: {language}.";


            return await SendRequestAsync(prompt, mode);
        }
        // Chat với AI
        public async Task<string> ChatWithAIAsync(string userMessage, string language = "vi")
        {
            if (string.IsNullOrEmpty(_detailedAnalysis) || DateTime.Now - _lastAnalysisTime > CacheDuration)
                return "Dữ liệu chi tiêu chưa sẵn sàng. Vui lòng phân tích lại trước khi đặt câu hỏi.";

            string prompt = $"Bạn là Moni - một trợ lý tài chính cá nhân. Dữ liệu phân tích chi tiêu: {_detailedAnalysis}\n"
                            + $"Người dùng hỏi: \"{userMessage}\". "
                            + $"Trả lời ngắn gọn, tập trung vào các ý chính, dưới 10 câu. Nếu liên quan đến tiền, hãy ghi cụ thể số tiền. "
                            + $"Trả lời bằng ngôn ngữ: {language}.";

            return await SendRequestAsync(prompt, "chat");
        }

        // Gửi yêu cầu, nếu Gemini không phản hồi thì fallback sang LM Studio
        private async Task<string> SendRequestAsync(string prompt, string mode)
        {
            string response = await SendRequestToGoogleGemini(prompt, mode);
            if (string.IsNullOrEmpty(response) || response.StartsWith("Lỗi"))
            {
                response = await SendRequestToLMStudio(prompt, mode);
            }
            return response;
        }
        // Gửi yêu cầu đến Google Gemini
        private async Task<string> SendRequestToGoogleGemini(string prompt, string mode)
        {
            var requestBody = new StringContent(JsonConvert.SerializeObject(new
            {
                contents = new[] { new { parts = new[] { new { text = prompt } } } }
            }), Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync(API_URL, requestBody);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return $"Lỗi API Gemini ({response.StatusCode}): {responseBody}";

                var jsonResponse = JObject.Parse(responseBody);
                string messageContent = jsonResponse["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString();
                if (string.IsNullOrEmpty(messageContent))
                    return "Không có phản hồi từ AI Gemini.";

                if (mode == "detailed")
                {
                    _detailedAnalysis = messageContent;
                    _lastAnalysisTime = DateTime.Now;
                }

                return messageContent;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return $"Lỗi khi gọi API Gemini: {ex.Message}";
            }
        }

        // Gửi yêu cầu đến LM Studio nếu Gemini không phản hồi
        private async Task<string> SendRequestToLMStudio(string prompt, string mode)
        {
            var requestBody = new StringContent(JsonConvert.SerializeObject(new
            {
                model = "gemma-3-1b-it",
                messages = new[] { new { role = "user", content = prompt } },
                temperature = 0.1
            }), Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync(LM_STUDIO_URL, requestBody);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return $"Lỗi LM Studio ({response.StatusCode}): {responseBody}";

                var jsonResponse = JObject.Parse(responseBody);
                string messageContent = jsonResponse["choices"]?[0]?["message"]?["content"]?.ToString();
                return string.IsNullOrEmpty(messageContent) ? "Không có phản hồi từ LM Studio." : messageContent;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return $"Lỗi khi gọi LM Studio: {ex.Message}";
            }
        }

        // Format
        private string GenerateShortSummary(string detailedText)
        {
            var sentences = detailedText.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(". ", sentences.Take(3)) + ".";
        }
        // Log
        private void LogError(Exception ex)
        {
            string logPath = "error_log.txt";
            string logMessage = $"{DateTime.Now}: {ex.Message}\n{ex.StackTrace}\n";
            File.AppendAllText(logPath, logMessage);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
        //Tạo chi tiêu nhanh
        public async Task<JObject> QuickExpenseEntryAsync(string userInput, DataTable categories, DataTable categoryGroups, int userId)
        {
            JObject jsonResponse = await AnalyzeUserInputAsync(userInput, categories, categoryGroups, userId);

            if (jsonResponse == null || jsonResponse["type"] == null)
            {
                return new JObject
                {
                    { "status", "error" },
                    { "message", CuteResponseGenerator.GetErrorParsingResponse() },
                    { "type", "error" }
                };
            }

            string responseType = jsonResponse["type"]?.ToString();
            string message = jsonResponse["message"]?.ToString();

            if (responseType == "other" || responseType == "error")
            {
                return new JObject
                {
                    { "status", responseType == "other" ? "success" : "error" },
                    { "message", CuteResponseGenerator.GetGeneralResponse(message) },
                    { "type", responseType }
                };
            }

            if (responseType == "create_category")
            {
                string categoryName = jsonResponse["category_name"]?.ToString()?.Trim();
                string categoryType = jsonResponse["category_type"]?.ToString()?.Trim();
                int groupCategoryId = jsonResponse["category_group_id"]?.ToObject<int>() ?? 0;
                string iconChar = jsonResponse["category_icon_char"]?.ToString();
                string typeExpense = "Expense";

                if (string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(categoryType) || groupCategoryId <= 0)
                {
                    return new JObject
                    {
                        { "status", "success" },
                        { "message", CuteResponseGenerator.GetFallbackResponse() },
                        { "type", "create_category_expense" }
                    };
                }

                int categoryId = await CreateCategoryQuicklyAsync(userId, categoryName, typeExpense, groupCategoryId, iconChar, "");

                if (categoryId <= 0)
                {
                    return new JObject
                    {
                        { "status", "success" },
                        { "message", CuteResponseGenerator.GetFallbackResponse() },
                        { "type", "create_category_expense" }
                    };
                }

                string description = jsonResponse["description"]?.ToString()?.Trim();
                string date = jsonResponse["date"]?.ToString()?.Trim();
                string tags = jsonResponse["tags"]?.ToString()?.Trim();
                string amountStr = jsonResponse["amount"]?.ToString()?.Trim();
                long amount = ConvertAmount(amountStr);

                if (amount <= 0)
                {
                    return new JObject
                    {
                        { "status", "success" },
                        { "message", CuteResponseGenerator.GetFallbackResponse() },
                        { "type", "expense_entry" }
                    };
                }

                int expenseID = await CreateExpense(userId, categoryId, amount, description, date, tags);

                return new JObject
                {
                    { "status", expenseID > 0  ? "success" : "error" },
                    { "message", CuteResponseGenerator.GetCreateCategorySuccess(categoryName, amount, date) },
                    { "type", "create_category_expense" },
                    { "expense_id", expenseID },
                    { "category_id", categoryId },

                };
            }

            if (responseType == "expense_entry")
            {
                string categoryIdStr = jsonResponse["category_id"]?.ToString()?.Trim();
                if (!int.TryParse(categoryIdStr, out int categoryId) || categoryId <= 0)
                {
                    return new JObject
                    {
                        { "status", "error" },
                        { "message", CuteResponseGenerator.GetFallbackResponse() },
                        { "type", "expense_entry" }
                    };
                }

                string description = jsonResponse["description"]?.ToString()?.Trim();
                string date = jsonResponse["date"]?.ToString()?.Trim();
                string tags = jsonResponse["tags"]?.ToString()?.Trim();
                string amountStr = jsonResponse["amount"]?.ToString()?.Trim();
                long amount = ConvertAmount(amountStr);

                if (amount <= 0)
                {
                    return new JObject
                    {
                        { "status", "error" },
                        { "message", CuteResponseGenerator.GetFallbackResponse() },
                        { "type", "expense_entry" }
                    };
                }

                int expenseID = await CreateExpense(userId, categoryId, amount, description, date, tags);
                return new JObject
                {                    
                    { "status", expenseID > 0  ? "success" : "error" },
                    { "message", CuteResponseGenerator.GetExpenseEntrySuccess(description, amount, date) },
                    { "type", "expense_entry" },
                    { "expense_id", expenseID }

                };
            }

            return new JObject
            {
                { "status", "error" },
                { "message", CuteResponseGenerator.GetFallbackResponse() },
                { "type", "error" }
            };
        }

        // Gửi yêu cầu tạo chi tiêu nhanh
        private async Task<JObject> AnalyzeUserInputAsync(string userInput, DataTable categories, DataTable categoryGroups, int userId)
        {
            try
            {
                var categoriesJson = JArray.FromObject(categories);
                var categoryGroupsJson = JArray.FromObject(categoryGroups);
                string today = DateTime.Now.ToString("yyyy-M-d");
                string prompt = $"Người dùng nhập: \"{userInput}\". "
                              + "Dựa trên các danh mục và nhóm danh mục đã có, hãy phân tích và trích xuất thông tin chi tiêu nếu có. "
                              + "Nếu là chi tiêu, trả về JSON: "
                              + "{ 'type': 'expense_entry', 'category_id': <id danh mục>,'category_name': <tên danh mục>, 'amount': <số tiền>, 'description': <mô tả>, "
                              + "'tags': <thẻ từ AI trả về dạng item1, item 2...>, 'currency': <đơn vị tiền tệ nếu có>, 'category_group_id': <id nhóm>, 'date': lấy ngày hôm nay tính nếu có ngày mà người dùng nhập lấy ngày đó, 'note': <ghi chú>, 'category_name':<danh mục> }. "
                              + "Nếu không có danh mục, trả về JSON yêu cầu tạo mới: "
                              + "{ 'type': 'create_category', 'category_name': <tên danh mục>, 'category_type': 'Expense', "
                              + "'amount': <số tiền>, 'description': <mô tả>, 'tags': <thẻ từ AI>, 'currency': <đơn vị tiền tệ nếu có>, "
                              + "'category_group_id': <id nhóm>, 'category_icon_char': <icon từ AI lấy trong thư viện font name char anwersome> }. "
                              + "Nếu không liên quan đến chi tiêu hãy trả lời câu hỏi, trả về JSON: "
                              + "{ 'type': 'other', 'message': <nội dung do AI tự trả lời> }. "
                              + "Nếu thiếu thông tin (category hoặc số tiền), trả về JSON: "
                              + "{ 'type': 'error', 'message': <lý do thiếu thông tin> }.Yêu cầu chỉ trả về nội dung trong json không được trả về nội dung khác json";

                prompt += $" Các danh mục hiện có: {categoriesJson.ToString()}.";
                prompt += $" Các nhóm danh mục hiện có: {categoryGroupsJson.ToString()}.";
                prompt += $" Ngày hôm nay : {today} ";

                string cleanedResponse = await SendRequestAsync(prompt, "analyze_expense");
                string response = cleanedResponse.Replace("```json", "").Replace("```", "").Trim();

                if (string.IsNullOrWhiteSpace(response))
                {
                    return new JObject { { "type", "error" }, { "message", "Phản hồi từ API trống." } };
                }

                JObject json = JObject.Parse(response);
                if (!json.ContainsKey("type"))
                {
                    return new JObject { { "type", "error" }, { "message", "Thiếu trường 'type' trong phản hồi." } };
                }

                return json;
            }
            catch (Exception )
            {
                return new JObject { { "type", "error" }, { "message", "Đã xảy ra lỗi khi phân tích đầu vào." } };
            }
        }
        // Hàm tạo nhanh Danh mục
        public async Task<int> CreateCategoryQuicklyAsync(int userId, string name, string type, int groupId, string iconChar, string iconUrl)
        {
            return await Task.Run(() => _category.CreateCategoryQuickly(userId, name, type, groupId, iconChar, iconUrl));
        }

        // Cover số tiền
        private long ConvertAmount(string amountStr)
        {
            if (amountStr.EndsWith("K", StringComparison.OrdinalIgnoreCase))
            {
                return long.Parse(amountStr.Substring(0, amountStr.Length - 1)) * 1000;
            }
            else if (long.TryParse(amountStr, out long amount))
            {
                return amount;
            }
            return 0;
        }
        // Tạo nhanh chi tiêu
        private async Task<int> CreateExpense(int userId, int category, long amount, string description, string expenseDate, string tags)
        {
            DateTime expenseDateFormat = string.IsNullOrEmpty(expenseDate)
                ? DateTime.Now
                : DateTime.ParseExact(expenseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            return await Task.Run(() => _expenseController.RecordExpense(userId, category, amount, expenseDateFormat, description, "Cash", 0, null, tags));
        }

        // Tạo câu hỏi nhanh
        public async Task<List<string>> GetFinancialQuestionsAsync()
        {
            string prompt = "Tạo danh sách 10 câu hỏi ngắn (không quá 10 từ) về quản lý tài chính cá nhân. " +
                "Các câu hỏi phải tập trung vào ngân sách, tiết kiệm, chi tiêu, thu nhập và các mẹo quản lý tài chính hữu ích. Đặt vào vị trí của tôi để hỏi. " +
                "Câu hỏi nên giúp người dùng hiểu rõ hơn về cách quản lý tiền bạc hiệu quả. " +
                "Trả về dưới dạng JSON array, không kèm theo bất kỳ văn bản nào khác. " +
                "Định dạng chính xác: [\"Câu hỏi 1\", \"Câu hỏi 2\", \"Câu hỏi 3\", \"Câu hỏi 4\", \"Câu hỏi 5\"].";

            string response = (await SendRequestAsync(prompt, "questions"))
                              .Replace("```json", "").Replace("```", "").Trim();

            try
            {
                return JsonConvert.DeserializeObject<List<string>>(response) ?? new List<string> { "Dữ liệu không hợp lệ." };
            }
            catch
            {
                return new List<string> { "Lỗi xử lý JSON." };
            }
        }


        // Tạo thu nhập nhanh
        public async Task<JObject> QuickIncomeEntryAsync(string userInput, DataTable categories, DataTable categoryGroups, int userId)
        {
            JObject jsonResponse = await AnalyzeUserInputAsyncCreateIncome(userInput, categories, categoryGroups, userId);
            // Không đủ dữ liệu
            if (jsonResponse == null || jsonResponse["type"] == null)
            {
                return new JObject
                {
                    { "status", "error" },
                    { "message", CuteResponseGenerator.GetErrorParsingResponse() },
                    { "type", "error" }
                };
            }

            string responseType = jsonResponse["type"]?.ToString();
            string message = jsonResponse["message"]?.ToString();
            // Nội dung không liên quan
            if (responseType == "other" || responseType == "error")
            {
                return new JObject
                {
                    { "status", responseType == "other" ? "success" : "error" },
                    { "message", CuteResponseGenerator.GetGeneralResponse(message) },
                    { "type", responseType }
                };
            }
            // Tạo danh mục
            if (responseType == "create_category")
            {
                string categoryName = jsonResponse["category_name"]?.ToString()?.Trim();
                string categoryType = jsonResponse["category_type"]?.ToString()?.Trim();
                int groupCategoryId = jsonResponse["category_group_id"]?.ToObject<int>() ?? 0;
                string iconChar = jsonResponse["category_icon_char"]?.ToString();
                string typeIncome = "Income";
                // Dữ liệu trống
                if (string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(categoryType) || groupCategoryId <= 0)
                {
                    return new JObject
                    {
                        { "status", "success" },
                        { "message", CuteResponseGenerator.GetFallbackResponse() },
                        { "type", "create_category_income" }
                    };
                }

                int categoryId = await CreateCategoryQuicklyAsync(userId, categoryName, typeIncome, groupCategoryId, iconChar, "");
                // Không tạo được danh mục
                if (categoryId <= 0)
                {
                    return new JObject
                    {
                        { "status", "success" },
                        { "message", CuteResponseGenerator.GetFallbackResponse() },
                        { "type", "create_category_income" }
                    };
                }

                string description = jsonResponse["description"]?.ToString()?.Trim();
                string date = jsonResponse["date"]?.ToString()?.Trim();
                string tags = jsonResponse["tags"]?.ToString()?.Trim();
                string amountStr = jsonResponse["amount"]?.ToString()?.Trim();
                long amount = ConvertAmount(amountStr);
                // Số tiền lỗi 
                if (amount <= 0)
                {
                    return new JObject
                    {
                        { "status", "success" },
                        { "message", CuteResponseGenerator.GetFallbackResponse() },
                        { "type", "create_category_income" }
                    };
                }

                int icomceID = await CreateIncome(userId, categoryId, amount, description, date, tags);
                return new JObject
                {                    
                    { "status", icomceID > 0 ? "success" : "error" },
                    { "message", CuteResponseGenerator.GetCreatIncomeCategorySuccess(categoryName, amount, date) },
                    { "type", "create_category_income" },
                    { "income_id", icomceID },
                    { "category_id", categoryId },

                };
            }

            if (responseType == "income_entry")
            {
                string categoryIdStr = jsonResponse["category_id"]?.ToString()?.Trim();
                if (!int.TryParse(categoryIdStr, out int categoryId) || categoryId <= 0)
                {
                    return new JObject
                    {
                        { "status", "error" },
                        { "message", CuteResponseGenerator.GetFallbackResponse() },
                        { "type", "income_entry" }
                    };
                }

                string description = jsonResponse["description"]?.ToString()?.Trim();
                string date = jsonResponse["date"]?.ToString()?.Trim();
                string tags = jsonResponse["tags"]?.ToString()?.Trim();
                string amountStr = jsonResponse["amount"]?.ToString()?.Trim();
                long amount = ConvertAmount(amountStr);

                if (amount <= 0)
                {
                    return new JObject
                {
                    { "status", "error" },
                    { "message", CuteResponseGenerator.GetFallbackResponse() },
                    { "type", "income_entry" }
                };
                }

                int icomceID = await CreateIncome(userId, categoryId, amount, description, date, tags);
                return new JObject
                {
                    { "status", icomceID > 0 ? "success" : "error" },
                    { "message", CuteResponseGenerator.GetIncomeEntrySuccess(description, amount, date) },
                    { "type", "income_entry" },
                    { "income_id", icomceID }
                };
            }

            return new JObject
            {
                { "status", "error" },
                { "message", CuteResponseGenerator.GetFallbackResponse() },
                { "type", "error" }
            };
        }

        // Hàm tạo thu nhập nhanh
        private async Task<int> CreateIncome(int userId, int category, long amount, string description, string incomeDate, string tags)
        {
            DateTime incomeDateFormat = string.IsNullOrEmpty(incomeDate)
                ? DateTime.Now
                : DateTime.ParseExact(incomeDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

          
            return await Task.Run(() => _incomeController.RecordIncome(userId, category, amount, incomeDateFormat, description, "Cash", 0, null, tags));
        }

        // Chi tiêu
        private async Task<JObject> AnalyzeUserInputAsyncCreateIncome(string userInput, DataTable categories, DataTable categoryGroups, int userId)
        {
            try
            {
                var categoriesJson = JArray.FromObject(categories);
                var categoryGroupsJson = JArray.FromObject(categoryGroups);
                string today = DateTime.Now.ToString("yyyy-M-d");
                string prompt = $"Người dùng nhập: \"{userInput}\". "
                              + "Dựa trên các danh mục và nhóm danh mục đã có, hãy phân tích và trích xuất thông tin chi tiêu nếu có. "
                              + "Nếu là thu nhập, trả về JSON: "
                              + "{ 'type': 'income_entry', 'category_id': <id danh mục>,'category_name': <tên danh mục>, 'amount': <số tiền>, 'description': <mô tả>, "
                              + "'tags': <thẻ từ AI trả về dạng item1, item 2...>, 'currency': <đơn vị tiền tệ nếu có>, 'category_group_id': <id nhóm>, 'date': lấy ngày hôm nay tính nếu truyền vào ngày cụ thể lấy ngày đó, 'note': <ghi chú>, 'category_name':<danh mục> }. "
                              + "Nếu không có danh mục, trả về JSON yêu cầu tạo mới: "
                              + "{ 'type': 'create_category', 'category_name': <tên danh mục>, 'category_type': 'Income', "
                              + "'amount': <số tiền>, 'description': <mô tả>, 'tags': <thẻ từ AI>, 'currency': <đơn vị tiền tệ nếu có>, "
                              + "'category_group_id': <id nhóm>, 'category_icon_char': <icon là tên từ thư viện font anwersome> }. "
                              + "Nếu không liên quan đến chi tiêu hãy trả lời câu hỏi, trả về JSON: "
                              + "{ 'type': 'other', 'message': <nội dung do AI tự trả lời> }. "
                              + "Nếu thiếu thông tin (category hoặc số tiền), trả về JSON: "
                              + "{ 'type': 'error', 'message': <lý do thiếu thông tin> }.Hãy trả về Json tương ứng chính xác";

                prompt += $" Các danh mục hiện có: {categoriesJson.ToString()}.";
                prompt += $" Các nhóm danh mục hiện có: {categoryGroupsJson.ToString()}.";
                prompt += $" Ngày hôm nay : {today} ";

                string cleanedResponse = await SendRequestAsync(prompt, "analyze_income");
                string response = cleanedResponse.Replace("```json", "").Replace("```", "").Trim();

                if (string.IsNullOrWhiteSpace(response))
                {
                    return new JObject { { "type", "error" }, { "message", "Phản hồi từ API trống." } };
                }

                JObject json = JObject.Parse(response);
                if (!json.ContainsKey("type"))
                {
                    return new JObject { { "type", "error" }, { "message", "Thiếu trường 'type' trong phản hồi." } };
                }

                return json;
            }
            catch (Exception)
            {
                return new JObject { { "type", "error" }, { "message", "Đã xảy ra lỗi khi phân tích đầu vào." } };
            }
        }

        private async Task<JObject> AnalyzeUserInputAsyncCreateBudgetTotal(string userInput, DataTable budget, int userId)
        {
            DateTime today = DateTime.Today;

            string budgetJson = JArray.FromObject(budget).ToString();

            string prompt = $"Người dùng nhập: \"{userInput}\". "
                           + "Dựa trên ngân sách hiện có, trích xuất số tiền và ngày bắt đầu nếu có. "
                           + "Trả về JSON với định dạng sau: "
                           + "{ 'type': 'create_budget', 'amount': <số tiền>, 'start_date': <ngày bắt đầu>, 'end_date': <ngày kết thúc> }. "
                           + "Nếu người dùng không nhập ngày, mặc định 'start_date' là '" + today.ToString("yyyy-MM-dd") + "' và 'end_date' là ngày cuối cùng của tháng đó. "
                           + "Ngày kết thúc luôn là ngày cuối cùng của tháng của 'start_date'. "
                           + "Nếu không liên quan, trả về { 'type': 'other', 'message': <nội dung AI> }. "
                           + "Nếu thiếu thông tin, trả về { 'type': 'error', 'message': <lý do> }. "
                           + $"Ngân sách hiện có: {budgetJson}.";

            string response = (await SendRequestAsync(prompt, "analyze_budget")).Trim()
                              .Replace("```json", "").Replace("```", "");

            JObject jsonResponse = JObject.Parse(string.IsNullOrWhiteSpace(response) ? "{ 'type': 'error', 'message': 'Phản hồi trống.' }" : response);

            if (jsonResponse["type"]?.ToString() == "create_budget" && DateTime.TryParse(jsonResponse["start_date"]?.ToString(), out DateTime startDate))
            {
                DateTime endDate = new DateTime(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
                jsonResponse["end_date"] = endDate.ToString("yyyy-MM-dd");
            }

            return jsonResponse;
        }
        public async Task<JObject> QuickBudgetEntryAsync(string userInput, DataTable budget, int userId)
        {
            JObject jsonResponse = await AnalyzeUserInputAsyncCreateBudgetTotal(userInput, budget, userId);

            if (jsonResponse == null || jsonResponse["type"] == null)
            {
                return new JObject
                {
                    { "status", "error" },
                    { "message", CuteResponseGenerator.GetErrorParsingResponse() },
                    { "type", "error" }
                };
            }

            string responseType = jsonResponse["type"]?.ToString();
            string message = jsonResponse["message"]?.ToString();

            if (responseType == "other" || responseType == "error")
            {
                return new JObject
                {
                    { "status", responseType == "other" ? "success" : "error" },
                    { "message", CuteResponseGenerator.GetGeneralResponse(message) },
                    { "type", responseType }
                };
            }

            if (responseType == "create_budget")
            {
                string startDate = jsonResponse["start_date"]?.ToString()?.Trim();
                string endDate = jsonResponse["end_date"]?.ToString()?.Trim();
                string amountStr = jsonResponse["amount"]?.ToString()?.Trim();
                long amount = ConvertAmount(amountStr);

                if (amount <= 0)
                {
                    return new JObject
                    {
                        { "status", "error" },
                        { "message", CuteResponseGenerator.GetFallbackResponse() },
                        { "type", "budget_entry" }
                    };
                }

                int budgetId = await CreateBudget(userId, amount, startDate, endDate);
                return new JObject
                {
                    { "status", budgetId > 0 ? "success" : "error" },
                    { "message", CuteResponseGenerator.GetBudgetEntrySuccess(amount, startDate, endDate) },
                    { "type", "budget_entry" },
                    { "budget_id", budgetId }
                };
            }

            return new JObject
            {
                { "status", "error" },
                { "message", CuteResponseGenerator.GetFallbackResponse() },
                { "type", "error" }
            };
        }



        //tạo ngân sách
        private async Task<int> CreateBudget(int userId, long amount, string startDate, string endDate)
        {
            DateTime start, end;

            if (string.IsNullOrEmpty(startDate))
                start = DateTime.Today;
            else
                start = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            if (string.IsNullOrEmpty(endDate))
                end = new DateTime(start.Year, start.Month, DateTime.DaysInMonth(start.Year, start.Month));
            else
                end = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            return await Task.Run(() => _budgetMainController.AddTotalBudgetWithId(userId, amount, start, end));
        }

        private async Task<JObject> AnalyzeUserInputAsyncCreateCategory(string userInput, DataTable category, DataTable categoryGroup, int userId)
        {
            DateTime today = DateTime.Today;

            // Chuyển đổi dữ liệu DataTable thành JSON
            string categoryJson = JArray.FromObject(category).ToString();
            string categoryGroupJson = JArray.FromObject(categoryGroup).ToString();

            string prompt = $"Người dùng nhập: '{userInput}'. "
               + "Hãy phân tích và xác định danh mục phù hợp dựa trên danh sách hiện có. Nếu không có, đề xuất tạo mới theo quy tắc sau: "
               + "1. Phân biệt giữa 'Nhóm danh mục' (category group) và 'Danh mục' (category): "
               + "   - 'Nhóm danh mục' là tập hợp chứa nhiều danh mục con liên quan đến một chủ đề. "
               + "   - 'Danh mục' là một mục cụ thể thuộc về một nhóm danh mục. "
               + "2. Nếu cả nhóm danh mục và danh mục đã tồn tại hoặc có ý nghĩa tương đương, trả về: "
               + "   {{ 'type': 'other', 'message': 'Đã tồn tại danh mục \"<tên danh mục>\" trong nhóm \"<tên nhóm danh mục>\".', "
               + "      'existing_id_category': <ID danh mục nếu có>, 'existing_name_category': '<Tên danh mục>', "
               + "      'existing_id_category_group': <ID nhóm danh mục nếu có>, 'existing_name_category_group': '<Tên nhóm danh mục>' }}. "
               + "3. Nếu danh mục đã tồn tại nhưng thuộc nhóm khác, trả về: "
               + "   {{ 'type': 'update', 'existing_id': <ID danh mục>, 'existing_name': '<Tên danh mục>', "
               + "      'suggested_id': <ID nhóm danh mục đề xuất>, 'suggested_name': '<Tên nhóm danh mục đề xuất>', 'description': '<Mô tả>' }}. "
               + "4. Nếu cần tạo nhóm danh mục mới (chưa có trong danh sách), chỉ trả về JSON sau khi xác nhận cần thiết: "
               + "   {{ 'type': 'category_group', 'group_name': '<Tên nhóm>', 'description': '<Mô tả tổng quan>', "
               + "      'group_icon': '<Icon nhóm trong Font Awesome>', "
               + "      'category_name': '<Tên danh mục>', 'category_type': '<Loại: Income | Expense>', "
               + "      'category_icon_char': '<Icon danh mục trong Font Awesome>' }}. "
               + "5. Nếu nhóm danh mục đã tồn tại nhưng danh mục chưa có, chỉ trả về JSON này (ưu tiên hơn việc tạo nhóm mới): "
               + "   {{ 'type': 'category_create', 'category_name': '<Tên danh mục>', 'category_type': '<Loại: Income | Expense>', "
               + "      'category_group_id': '<ID nhóm danh mục>', 'category_group_name': '<Tên nhóm danh mục>', "
               + "      'category_icon_char': '<Icon danh mục trong Font Awesome>' }}. "
               + "6. Nếu thông tin không liên quan đến danh mục hay nhóm danh mục, chẳng hạn như lời chào, phép toán đơn giản, hoặc câu hỏi chung, hãy trả về: "
               + "   {{ 'type': 'other', 'message': '<Trả lời câu hỏi của người dùng theo ý của bạn>' }}. "
               + "7. Nếu thiếu thông tin cần thiết, trả về: "
               + "   {{ 'type': 'error', 'message': '<Lý do>' }}. "
               + "Chỉ trả về JSON phù hợp, không thêm thông tin khác. "
               + $"Danh mục hiện có: {categoryJson}. Nhóm danh mục hiện có: {categoryGroupJson}.";

            string response = (await SendRequestAsync(prompt, "analyze_category_group")).Trim()
                               .Replace("```json", "").Replace("```", "");


            JObject jsonResponse = JObject.Parse(string.IsNullOrWhiteSpace(response) ? "{ 'type': 'error', 'message': 'Phản hồi trống.' }" : response);

            return jsonResponse;
        }


        // Tao danh muc nhanh
        public async Task<JObject> QuickCategoryEntryAsync(string userInput, DataTable category, DataTable categoryGroup, int userId)
        {
            JObject jsonResponse = await AnalyzeUserInputAsyncCreateCategory(userInput, category, categoryGroup, userId);

            if (jsonResponse == null || jsonResponse["type"] == null)
            {
                return new JObject
                {
                    { "status", "error" },
                    { "message", CuteResponseGenerator.GetErrorParsingResponse() },
                    { "type", "error" }
                };
            }

            string responseType = jsonResponse["type"]?.ToString();
            string message = jsonResponse["message"]?.ToString();

            if (responseType == "other" || responseType == "error")
            {
                return new JObject
                {
                    { "status", responseType == "other" ? "success" : "error" },
                    { "message", CuteResponseGenerator.GetGeneralResponse(message) },
                    { "type", responseType }
                };
            }

            if (responseType == "update")
            {
                int existingCategoryId = jsonResponse["existing_id"]?.ToObject<int>() ?? 0;
                int suggestedGroupId = jsonResponse["suggested_id"]?.ToObject<int>() ?? 0;
                string description = jsonResponse["description"]?.ToString();

                if (existingCategoryId <= 0 || suggestedGroupId <= 0 || string.IsNullOrEmpty(description))
                {
                    return new JObject
                    {
                        { "status", "error" },
                        { "message", CuteResponseGenerator.GetErrorParsingResponse() },
                        { "type", "update_category_group" }
                    };
                }

                int updateSuccess = await UpdateCategoryGroupDescriptionIdAsync(existingCategoryId, suggestedGroupId, description);
                return new JObject
                {
                    { "status", updateSuccess > 0 ? "success" : "error" },
                    { "message", CuteResponseGenerator.ReplaceCategoryUpdateMessage(existingCategoryId, suggestedGroupId, description) },
                    { "type", "update_category_group" },
                    { "existing_id", existingCategoryId },
                    { "category_group_id", updateSuccess }
                };
            }

            if (responseType == "category_group")
            {
                string groupName = jsonResponse["group_name"]?.ToString();
                string description = jsonResponse["description"]?.ToString();
                string groupIcon = jsonResponse["group_icon"]?.ToString();
                string categoryName = jsonResponse["category_name"]?.ToString();
                string categoryType = jsonResponse["category_type"]?.ToString();
                string categoryIcon = jsonResponse["category_icon_char"]?.ToString();

                if (string.IsNullOrEmpty(groupName) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(categoryName))
                {
                    return new JObject
                    {
                        { "status", "error" },
                        { "message", CuteResponseGenerator.GetErrorParsingResponse() },
                        { "type", "category_group" }
                    };
                }

                int newCategoryGroupId = await AddNewCategoryGroupDescriptionIdAsync(userId, groupName, description, groupIcon);

                int newCategoryId = 0;
                if (newCategoryGroupId > 0)
                {
                    newCategoryId = await AddNewCategoryInGroupIdAsync(userId, newCategoryGroupId, categoryName, categoryType, categoryIcon);
                }

                return new JObject
                {
                    { "status", newCategoryGroupId > 0 ? "success" : "error" },
                    { "message", CuteResponseGenerator.GetCategoryCreationMessage(categoryName, groupName) },
                    { "type", "category_group" },
                    { "category_group_id", newCategoryGroupId },
                    { "category_id", newCategoryId },
                };
            }

            if (responseType == "category_create")
            {
                string categoryName = jsonResponse["category_name"]?.ToString();
                string categoryType = jsonResponse["category_type"]?.ToString();
                int categoryGroupId = jsonResponse["category_group_id"]?.ToObject<int>() ?? 0;
                string categoryIcon = jsonResponse["category_icon_char"]?.ToString();

                if (string.IsNullOrEmpty(categoryName) || categoryGroupId <= 0)
                {
                    return new JObject
                    {
                        { "status", "error" },
                        { "message", CuteResponseGenerator.GetErrorParsingResponse() },
                        { "type", "category_create" }
                    };
                }

                int newCategoryId = await AddNewCategoryInGroupIdAsync(userId, categoryGroupId, categoryName, categoryType, categoryIcon);
                return new JObject
                {
                    { "status", newCategoryId > 0 ? "success" : "error" },
                    { "message", CuteResponseGenerator.GetCategoryCreationMessage(categoryName, categoryType) },
                    { "type", "category_create" },
                    { "category_id", newCategoryId }
                };
            }

            return new JObject
            {
                { "status", "error" },
                { "message", CuteResponseGenerator.GetFallbackResponse() },
                { "type", "error" }
            };
        }

        private async Task<int> UpdateCategoryGroupDescriptionIdAsync(int existingCategoryId, int suggestedGroupId, string description)
        {
            return await Task.Run(() => _categoryGroupController.UpdateCategoryGroupDescriptionId(existingCategoryId, suggestedGroupId, description));
        }

        private async Task<int> AddNewCategoryGroupDescriptionIdAsync(int userId, string groupName, string description, string groupIcon)
        {
            return await Task.Run(() => _categoryGroupController.AddNewCategoryGroupId(userId, groupName, description, groupIcon));
        }

        private async Task<int> AddNewCategoryInGroupIdAsync(int userId, int newCategoryGroupId, string categoryName, string categoryType, string categoryIcon)
        {
            return await Task.Run(() => _categoryGroupController.AddNewCategoryInGroupId(userId, newCategoryGroupId, categoryName, categoryType, categoryIcon));
        }


        private async Task<Dictionary<string, object>> AnalyzeUserInputAsyncAnalysis(string userInput, int userId)
        {
            DataTable expenseData = _budgetMainController.GetDailyExpensesByMonth(userId, _month, _year);
            DataTable incomeData = _budgetMainController.GetDailyIncomeByMonth(userId, _month, _year);
            DataTable budget = _budgetMainController.GetBudgetAll(userId);
            Dictionary<int, decimal> expenseDict = new Dictionary<int, decimal>();
            Dictionary<int, decimal> incomeDict = new Dictionary<int, decimal>();
            string icomeJson = JArray.FromObject(incomeData).ToString();
            string expenseJson = JArray.FromObject(expenseData).ToString();
            string budgetJson = JArray.FromObject(budget).ToString();
            // Đọc dữ liệu chi tiêu
            foreach (DataRow row in expenseData.Rows)
            {
                int day = Convert.ToInt32(row["day"]);
                decimal expense = Convert.ToDecimal(row["total_expense"]); // Đúng tên cột
                expenseDict[day] = expense;
            }

            // Đọc dữ liệu thu nhập
            foreach (DataRow row in incomeData.Rows)
            {
                int day = Convert.ToInt32(row["day"]);
                decimal income = Convert.ToDecimal(row["total_income"]); // Đúng tên cột
                incomeDict[day] = income;
            }

            // Hợp nhất danh sách ngày từ cả hai bảng
            List<int> days = expenseDict.Keys.Union(incomeDict.Keys).OrderBy(d => d).ToList();
            List<decimal> expenses = days.Select(day => expenseDict.ContainsKey(day) ? expenseDict[day] : 0).ToList();
            List<decimal> incomes = days.Select(day => incomeDict.ContainsKey(day) ? incomeDict[day] : 0).ToList();

            // Gửi dữ liệu đến AI để phân tích
            string prompt = $"Người dùng nhập: '{userInput}'. "
                + "Hãy phân tích xu hướng tài chính cá nhân dựa trên dữ liệu lịch sử. "
                + "Dữ liệu bao gồm chi tiêu và thu nhập theo ngày trong tháng. "
                + "Nhận xét liệu người dùng có đang chi tiêu hợp lý không, xu hướng tài chính có ổn định không." +
                $"Đây là danh sách thu nhập tháng này {icomeJson}" +
                $"Đây là danh sách chi tiêu tháng này {expenseJson}" +
                $"Đây là các ngân sách tháng ngày của người dùng {budgetJson} .Tóm tắt trong khoảng 20 câu. Không được dài quá";

            string response = await SendRequestAsync(prompt, "analyze_financial_trend");
            response = response.Trim().Replace("```json", "").Replace("```", "");

            JObject responseJson;
            try
            {
                responseJson = JObject.Parse(response);
            }
            catch (JsonReaderException)
            {
                return new Dictionary<string, object>
                {
                    { "message", response },
                    { "days", days },
                    { "expenses", expenses },
                    { "incomes", incomes },
                    { "aiAnalysis", new Dictionary<string, object>() }
                };
            }

            string analysisMessage = responseJson["message"]?.ToString() ?? "Không có phân tích.";
            var aiAnalysisData = responseJson["analysisData"]?.ToObject<Dictionary<string, object>>() ?? new Dictionary<string, object>();

            return new Dictionary<string, object>
            {
                { "message", analysisMessage },
                { "days", days },
                { "expenses", expenses },
                { "incomes", incomes },
                { "aiAnalysis", aiAnalysisData }
            };
        }

        public async Task<JObject> QuickAnalysisEntryAsync(string userInput, int userId)
        {
            var analysisResult = await AnalyzeUserInputAsyncAnalysis(userInput, userId);

            if (analysisResult == null || !analysisResult.ContainsKey("message"))
            {
                return new JObject
                {
                    { "status", "error" },
                    { "message", CuteResponseGenerator.GetErrorParsingResponse() },
                    { "type", "error" }
                };
            }

            string message = analysisResult["message"]?.ToString() ?? "Không có phân tích.";
            var days = analysisResult["days"] as List<int> ?? new List<int>();
            var expenses = analysisResult["expenses"] as List<decimal> ?? new List<decimal>();
            var incomes = analysisResult["incomes"] as List<decimal> ?? new List<decimal>();
            var aiAnalysisData = analysisResult["aiAnalysis"] as Dictionary<string, object> ?? new Dictionary<string, object>();

            int count = days.Count;
            if (expenses.Count != count || incomes.Count != count)
            {
                return new JObject
                {
                    { "status", "error" },
                    { "message", "Dữ liệu ngày, thu nhập và chi tiêu không khớp." },
                    { "type", "error" }
                };
            }

            JArray trendData = new JArray();
            for (int i = 0; i < count; i++)
            {
                trendData.Add(new JObject
                {
                    { "day", days[i] },
                    { "income", incomes[i] },
                    { "expense", expenses[i] }
                });
            }

            return new JObject
            {
                { "status", "success" },
                { "message", message },
                { "type", "financial_analysis" },
                { "trend_data", trendData },
                { "ai_analysis", JObject.FromObject(aiAnalysisData) }
            };
        }
        private async Task<Dictionary<string, object>> AnalyzeUserInputWithSavingSuggestionsAsync(string userInput, int userId)
        {
            DataTable expenseData = _expenseController.GetExpensesByMonth(userId, _month, _year);
            DataTable incomeData = _incomeController.GetIncomesByMonth(userId, _month, _year);
            DataTable budget = _budgetMainController.GetBudgetAll(userId);
            DataTable category = _category.GetAllCategories(userId);
            string icomeJson = JArray.FromObject(incomeData).ToString();
            string expenseJson = JArray.FromObject(expenseData).ToString();
            string budgetJson = JArray.FromObject(budget).ToString();
            string categoryJson = JArray.FromObject(category).ToString();
            Dictionary<int, decimal> expenseDict = new Dictionary<int, decimal>();
            Dictionary<int, decimal> incomeDict = new Dictionary<int, decimal>();
            Dictionary<int, decimal> savingDict = new Dictionary<int, decimal>();

            foreach (DataRow row in expenseData.Rows)
            {
                DateTime date = Convert.ToDateTime(row["expense_date"]);
                int day = date.Day;
                decimal amount = Convert.ToDecimal(row["amount"]); 
                expenseDict[day] = expenseDict.ContainsKey(day) ? expenseDict[day] + amount : amount;
            }

            foreach (DataRow row in incomeData.Rows)
            {
                DateTime date = Convert.ToDateTime(row["income_date"]);
                int day = date.Day;
                decimal amount = Convert.ToDecimal(row["amount"]); 
                incomeDict[day] = incomeDict.ContainsKey(day) ? incomeDict[day] + amount : amount;
            }

            foreach (var day in expenseDict.Keys.Union(incomeDict.Keys))
            {
                decimal income = incomeDict.ContainsKey(day) ? incomeDict[day] : 0;
                decimal expense = expenseDict.ContainsKey(day) ? expenseDict[day] : 0;
                decimal savingPotential = (income - expense) * 0.2m; 

                savingDict[day] = savingPotential > 0 ? savingPotential : 0;
            }

            List<int> days = savingDict.Keys.OrderBy(d => d).ToList();
            List<decimal> expenses = days.Select(day => expenseDict.ContainsKey(day) ? expenseDict[day] : 0).ToList();
            List<decimal> incomes = days.Select(day => incomeDict.ContainsKey(day) ? incomeDict[day] : 0).ToList();
            List<decimal> savings = days.Select(day => savingDict.ContainsKey(day) ? savingDict[day] : 0).ToList();

            string prompt = $"Người dùng nhập: '{userInput}'. "
                + "Hãy phân tích xu hướng tài chính cá nhân và đưa ra gợi ý tiết kiệm dựa trên dữ liệu lịch sử. "
                + "Dữ liệu bao gồm chi tiêu và thu nhập theo ngày trong tháng. "
                + "Hãy đưa ra lời khuyên về cách tiết kiệm tốt hơn dựa trên tình hình tài chính."+
                $"Đây là danh sách thu nhập tháng này {icomeJson}" +
                $"Đây là danh sách chi tiêu tháng này {expenseJson}" +
                $"Đây là các ngân sách tháng ngày của người dùng {budgetJson}. " +
                $"Đây là các danh mục của người dùng{categoryJson} .Tóm tắt trong khoảng 20 câu. Không được dài quá";

            string response = await SendRequestAsync(prompt, "analyze_financial_trend");
            response = response.Trim().Replace("```json", "").Replace("```", "");

            JObject responseJson;
            try
            {
                responseJson = JObject.Parse(response);
            }
            catch (JsonReaderException)
            {
                return new Dictionary<string, object>
                {
                    { "message", response },
                    { "days", days },
                    { "expenses", expenses },
                    { "incomes", incomes },
                    { "savings", savings },
                    { "aiAnalysis", new Dictionary<string, object>() }
                };
            }

            string analysisMessage = responseJson["message"]?.ToString() ?? "Không có phân tích.";
            var aiAnalysisData = responseJson["analysisData"]?.ToObject<Dictionary<string, object>>() ?? new Dictionary<string, object>();

            return new Dictionary<string, object>
            {
                { "message", analysisMessage },
                { "days", days },
                { "expenses", expenses },
                { "incomes", incomes },
                { "savings", savings },
                { "aiAnalysis", aiAnalysisData }
            };
        }

        public async Task<JObject> QuickSavingsAnalysisAsync(string userInput, int userId)
        {
            var analysisResult = await AnalyzeUserInputWithSavingSuggestionsAsync(userInput, userId);

            if (analysisResult == null || !analysisResult.ContainsKey("message"))
            {
                return new JObject
                {
                    { "status", "error" },
                    { "message", "Không thể phân tích dữ liệu." },
                    { "type", "error" }
                };
            }

            string message = analysisResult["message"]?.ToString() ?? "Không có phân tích.";
            var days = analysisResult["days"] as List<int> ?? new List<int>();
            var expenses = analysisResult["expenses"] as List<decimal> ?? new List<decimal>();
            var incomes = analysisResult["incomes"] as List<decimal> ?? new List<decimal>();
            var savings = analysisResult["savings"] as List<decimal> ?? new List<decimal>();
            var aiAnalysisData = analysisResult["aiAnalysis"] as Dictionary<string, object> ?? new Dictionary<string, object>();

            int count = days.Count;
            if (expenses.Count != count || incomes.Count != count || savings.Count != count)
            {
                return new JObject
                {
                    { "status", "error" },
                    { "message", "Dữ liệu ngày, thu nhập, chi tiêu và tiết kiệm không khớp." },
                    { "type", "error" }
                };
            }

            JArray trendData = new JArray();
            for (int i = 0; i < count; i++)
            {
                trendData.Add(new JObject
                {
                    { "day", days[i] },
                    { "income", incomes[i] },
                    { "expense", expenses[i] },
                    { "saving", savings[i] }
                });
            }

            return new JObject
            {
                { "status", "success" },
                { "message", message },
                { "type", "savings_analysis" },
                { "trend_data", trendData },
                { "ai_analysis", JObject.FromObject(aiAnalysisData) }
            };
        }

    }
}
