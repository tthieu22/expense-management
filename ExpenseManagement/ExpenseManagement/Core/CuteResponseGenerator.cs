using System.Collections.Generic;
using System;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Office.Interop.Excel;

public class CuteResponseGenerator
{
    private static readonly Random _random = new Random();

    private static readonly Dictionary<string, List<string>> Responses = new Dictionary<string, List<string>>()
    {
        { "welcome", new List<string> {
             "Xin chào! Mình là Moni – trợ lý tài chính cá nhân được thiết kế để giúp bạn quản lý thu nhập, chi tiêu và tiết kiệm một cách hiệu quả.",
            "Chào mừng bạn đến với Moni! Mình là trợ lý tài chính cá nhân, hỗ trợ bạn theo dõi tài chính, kiểm soát ngân sách và xây dựng thói quen chi tiêu hợp lý.",
            "Xin chào! Mình là Moni – một trợ lý tài chính thông minh, sẵn sàng đồng hành cùng bạn trong việc phân tích chi tiêu, đưa ra cảnh báo bất thường và đề xuất chiến lược tiết kiệm.",
            "Chào bạn! Moni là trợ lý tài chính cá nhân, giúp bạn hiểu rõ hơn về tình hình tài chính, lập ngân sách và đạt được các mục tiêu tài chính dài hạn.",
            "Moni xin chào bạn! Với vai trò là trợ lý tài chính, mình sẽ hỗ trợ bạn ghi chép thu chi nhanh chóng, theo dõi danh mục và đưa ra các phân tích hữu ích để tối ưu hóa tài chính cá nhân.",
        }},

        { "quick_expense", new List<string> {
            "Tớ sẽ giúp bạn ghi lại chi tiêu nè! Ví dụ: 'Ăn trưa 50K' 🍔",
            "Cùng theo dõi chi tiêu nào! Bạn có thể nhập kiểu: 'Mua sách 120K' 📚",
            "Nào nào, mình giúp bạn ghi lại nhé! Thử nhập: 'Cà phê 40K' ☕",
            "Ghi chép chi tiêu siêu dễ! Bạn thử nhập: 'Đổ xăng 80K' ⛽",
            "Mình ở đây để giúp bạn! Nhập nhanh: 'Mua vé xem phim 100K' 🎬",
            "Bắt đầu ghi lại chi tiêu nhé! Ví dụ: 'Mua bánh ngọt 30K' 🍩",
            "Mình sẽ ghi nhớ giúp bạn! Nhập thử: 'Trả tiền điện 500K' 💡"
        }},
        { "error_parsing", new List<string> {
            "Mình chưa hiểu lắm, bạn thử nói lại giúp mình nha! 😊",
            "Hơi khó hiểu nè, bạn nhập lại thử xem sao nhé! ✨",
            "Ơ kìa, mình chưa bắt được ý, bạn giúp mình với nha! 🧐",
            "Hình như có chút nhầm lẫn, bạn thử lại giúp mình nhé! 💡",
            "Mình chưa hiểu rõ lắm, có thể bạn nhập lại theo cách khác không? 📝"
        }},
        { "other", new List<string> {
            "Oh! {info}",
            "Hí! {info}",
            "Hehe! {info}",
            "Tada~ {info}"
        }},

        { "create_category_success", new List<string> {
            "Yay! Danh mục '{category}' đã được tạo vào ngày {date}, và {amount} VND đã được ghi nhận! 🎉",
            "'{category}' đã xuất hiện trên bản đồ chi tiêu của bạn vào {date}, số tiền {amount} VND đã lưu nha! 📝",
            "Xong! '{category}' đã sẵn sàng từ ngày {date} với {amount} VND! Bạn cứ tiếp tục quản lý tài chính thật tốt nha! 💰"
        }},
        { "expense_entry_success", new List<string> {
            "Bạn đã ghi lại chi tiêu: '{description}' - {amount} VND vào ngày {date} rồi nha! 📒",
            "Mình đã lưu chi tiêu '{description}' với số tiền {amount} VND vào {date}! ✅",
            "Ghi chú xong! '{description}' - {amount} VND đã được lưu lại vào {date}! 📝"
        }},
        { "fallback", new List<string> {
            "Mình chưa hiểu lắm, bạn thử ghi chú lại kiểu khác nhé! 😊",
            "Bạn có thể nhập lại theo cách khác giúp mình không? 📝",
            "Hình như chưa rõ ràng lắm, bạn thử nhập lại xem sao nhé! 🔄"
        }},
        { "quick_income", new List<string> {
             "Tớ sẽ giúp bạn ghi lại thu nhập nè! Ví dụ: 'Lương tháng 10 triệu' 💰",
             "Cùng theo dõi thu nhập nào! Bạn có thể nhập kiểu: 'Tiền thưởng 2 triệu' 🎉",
             "Nào nào, mình giúp bạn ghi lại nhé! Thử nhập: 'Bán hàng online 5 triệu' 🛒",
             "Ghi chép thu nhập siêu dễ! Bạn thử nhập: 'Lãi đầu tư 3 triệu' 📈",
             "Mình ở đây để giúp bạn! Nhập nhanh: 'Nhận tiền trợ cấp 1 triệu' 💵",
             "Bắt đầu ghi lại thu nhập nhé! Ví dụ: 'Được tặng 500K' 🎁",
             "Mình sẽ ghi nhớ giúp bạn! Nhập thử: 'Tiền hoa hồng 2.5 triệu' 🏆"
         }},
        { "income_entry_success", new List<string> {
            "Bạn đã ghi lại thu nhập: '{description}' - {amount} VND vào ngày {date} rồi nha! 🎉",
            "Mình đã lưu thu nhập '{description}' với số tiền {amount} VND vào {date}! ✅",
            "Xong rồi! '{description}' - {amount} VND đã được ghi nhận vào {date}! 💰"
        }},
        { "create_income_category_success", new List<string> {
            "Tuyệt vời! Danh mục thu nhập '{category}' đã được tạo vào ngày {date}, và {amount} VND đã được ghi nhận! 🎉",
            "'{category}' đã có mặt trong danh sách thu nhập của bạn vào {date}, số tiền {amount} VND đã lưu nha! 📝",
            "Xong rồi! '{category}' đã sẵn sàng từ ngày {date} với {amount} VND! Hãy tiếp tục theo dõi tài chính của bạn nhé! 💰"
        }},
        { "quick_budget", new List<string> {
            "Bạn muốn đặt ngân sách bao nhiêu? Ví dụ: 'Ngân sách 20 triệu'",
            "Hãy nhập số tiền bạn muốn đặt làm ngân sách. Ví dụ: '20 triệu tháng này'",
            "Nhập ngân sách bạn muốn đặt. Ví dụ: '20 triệu tháng sau'",
            "Bạn có thể đặt ngân sách như thế này: '20 triệu từ 2/3/2025 đến 26/3/2025'",
            "Hãy đặt ngân sách cho tháng này. Ví dụ: '20 triệu'",
            "Nhập ngân sách bạn muốn sử dụng. Ví dụ: '20 triệu trong tháng 3'",
            "Hãy nhập số tiền bạn muốn chi tiêu. Ví dụ: 'Ngân sách tháng này 20 triệu'",
            "Bạn có thể đặt ngân sách như sau: 'Dành 20 triệu cho tháng sau'",
            "Nhập ngân sách bạn cần. Ví dụ: 'Ngân sách 20 triệu từ 2/3/2025 đến 26/3/2025'",
            "Hãy đặt ngân sách phù hợp với bạn. Ví dụ: 'Tháng này 20 triệu'"
        }},
        {
        "create_budget_success", new List<string> {
            "Ngân sách mới đã được tạo từ {startDate} đến {endDate} với số tiền {amount} VND.",
            "Hoàn tất! Bạn đã thiết lập ngân sách từ {startDate} đến {endDate} với số tiền {amount} VND.",
            "Thành công! Ngân sách {amount} VND sẽ áp dụng từ {startDate} đến {endDate}. Hãy tiếp tục theo dõi chi tiêu nhé."
        }},
        { "create_category_in_group_success", new List<string> {
            "Tuyệt vời! Danh mục '{categoryName}' thuộc loại '{description}' đã được tạo vào ngày {date}.",
            "Danh mục '{categoryName}' của loại '{description}' đã được thêm vào ngày {date}. Bạn đã sẵn sàng để quản lý tài chính rồi!",
            "Xong! Danh mục '{categoryName}' đã sẵn sàng từ ngày {date}, thuộc loại '{description}'. Tiếp tục quản lý tài chính tốt nhé!"
        }},
        {"update_category_in_group_success", new List<string> {
            "Danh mục '{categoryName}' đã được cập nhật thành công với mô tả '{description}' vào ngày {date}.",
            "Cập nhật thành công! Danh mục '{categoryName}' với mô tả '{description}' đã được thay đổi vào ngày {date}.",
            "Xong! Danh mục '{categoryName}' đã được chỉnh sửa, với mô tả mới là '{description}' và cập nhật vào ngày {date}.",
            "Danh mục '{categoryName}' đã được cập nhật thành công vào ngày {date}. Mô tả hiện tại là '{description}'.",
            "Hoàn thành! Cập nhật thông tin danh mục '{categoryName}' với mô tả '{description}' đã được thực hiện vào {date}."
        }},
        { "quick_create_category", new List<string> {
            "Hãy bắt đầu tạo danh mục mới! Ví dụ: 'Tạo danh mục Ăn uống'",
            "Để tạo danh mục mới, bạn có thể nhập: 'Tạo danh mục Mua sắm'",
            "Hãy thử tạo danh mục cho chi tiêu của bạn! Ví dụ: 'Thêm danh mục Tiết kiệm'",
            "Cùng tạo danh mục mới thôi! Nhập thử: 'Tạo danh mục Vui chơi'",
            "Thêm danh mục mới giúp bạn quản lý chi tiêu tốt hơn! Ví dụ: 'Tạo danh mục Học tập'",
            "Tạo danh mục mới ngay bây giờ! Ví dụ: 'Tạo danh mục Du lịch'",
            "Chúng ta sẽ tạo danh mục mới! Nhập thử: 'Tạo danh mục Mua sắm trực tuyến'",
        }},
        {"quick_create_income_category", new List<string> {
            "Hãy bắt đầu tạo danh mục thu nhập! Ví dụ: 'Tạo danh mục Lương'",
            "Để theo dõi thu nhập, bạn có thể nhập: 'Tạo danh mục Tiền thưởng'",
            "Hãy thêm một danh mục cho nguồn thu nhập của bạn! Ví dụ: 'Tạo danh mục Đầu tư'",
            "Cùng tạo danh mục thu nhập mới! Nhập thử: 'Tạo danh mục Bán hàng'",
            "Thêm danh mục mới giúp bạn quản lý tài chính tốt hơn! Ví dụ: 'Tạo danh mục Thu nhập phụ'",
            "Tạo danh mục thu nhập ngay bây giờ! Ví dụ: 'Tạo danh mục Tiền lãi'",
            "Bạn muốn theo dõi nguồn thu nào? Nhập thử: 'Tạo danh mục Cho thuê'"
        }},
        { "financial_trend_analysis", new List<string> {
           "Dưới đây là tình hình phân tích chi tiêu của bạn.",
           "Mình đã tổng hợp xu hướng tài chính của bạn rồi đây.",
           "Tôi đã phân tích cách bạn chi tiêu trong thời gian qua.",
           "Đây là bức tranh tài chính của bạn, tôi đã phân tích chi tiết.",
           "Bạn có muốn biết xu hướng chi tiêu của mình không? Đây là kết quả.",
           "Tôi đã tổng hợp các khoản thu và chi của bạn. Hãy kiểm tra nhé.",
           "Cùng xem bạn đã tiết kiệm hay chi tiêu ra sao trong tháng này nhé."
        }},
        { "smart_saving_suggestions", new List<string> {
            "Dưới đây là một số gợi ý giúp bạn tiết kiệm hiệu quả hơn.",
            "Mình có một số mẹo tiết kiệm thông minh cho bạn đây.",
            "Bạn có thể cải thiện tài chính của mình với những gợi ý tiết kiệm này.",
            "Tôi đã phân tích thu nhập và chi tiêu của bạn để đưa ra kế hoạch tiết kiệm hợp lý.",
            "Bạn muốn tiết kiệm nhiều hơn mà không ảnh hưởng đến chi tiêu hàng ngày? Hãy xem ngay.",
            "Đây là những cách giúp bạn tối ưu hóa ngân sách và tiết kiệm tốt hơn.",
            "Cùng xem bạn có thể tiết kiệm thêm bao nhiêu mỗi tháng nhé!"
        }}

    };
    public static string GetExpenseModeReminder()
    {
        List<string> reminders = new List<string>
        {
            "Bạn đang trong chế độ ghi chép rồi nè! Nhập chi tiêu thôi nhé! 😊",
            "Mình vẫn nhớ mà! Cứ nhập số tiền và nội dung chi tiêu vào là được nha! 💰",
            "Không cần nhấn lại đâu, cứ nhập chi tiêu là mình sẽ lưu ngay cho bạn! ✍️",
            "Mọi thứ sẵn sàng rồi đó! Nhập chi tiêu vào thôi nào! 📌",
            "Bạn cứ nhập chi tiêu vào nhé! Không cần nhấn lại đâu nè! 😃"
        };

        return reminders[_random.Next(reminders.Count)];
    }
    public static string GetQuickExpenseReminder()
    {
        List<string> reminders = new List<string>
        {
            "Xong rồi nè! Nhưng nếu bạn còn khoản chi nào nữa thì cứ nhấn 'Quickly Add Expense' nha! 💸✨",
            "Hết việc rồi nè! Nhưng nếu ví lại vơi đi chút xíu, nhớ nhấn 'Quickly Add Expense' nha! 😆",
            "Ghi chép xong xuôi rồi! Có chi tiêu mới thì đừng ngại nhấn 'Quickly Add Expense' nha! 📜💰",
            "Tất cả đã được lưu lại rồi! Nếu còn chi tiêu mới, mình vẫn ở đây, nhấn 'Quickly Add Expense' nào! 😉",
            "Hôm nay chi tiêu ổn chứ? Nếu có gì mới, bạn biết phải làm gì rồi nhỉ? Nhấn 'Quickly Add Expense' thôi! 😍"
        };

        return reminders[_random.Next(reminders.Count)];
    }
    public static string GetQuickBudgetReminder()
    {
        List<string> reminders = new List<string>
        {
            "Ngân sách của bạn đã được thiết lập! Nếu muốn cập nhật thêm, hãy nhấn 'Quickly Add Budget'.",
            "Hoàn tất rồi! Nếu cần thay đổi ngân sách, đừng quên nhấn 'Quickly Add Budget' nhé!",
            "Ngân sách đã lưu thành công! Nếu có điều chỉnh nào, hãy nhập lại ngay.",
            "Mọi thứ đã sẵn sàng! Nếu muốn thêm hoặc sửa ngân sách, bạn có thể làm ngay bây giờ.",
            "Thiết lập ngân sách xong rồi! Nếu có thay đổi, cứ nhấn 'Quickly Add Budget' để cập nhật."
        };

        return reminders[_random.Next(reminders.Count)];
    }
    public static string GetCategoryModeReminder()
    {
        List<string> reminders = new List<string>
    {
        "Danh mục của bạn đã được thiết lập! Hãy nhập danh mục mới hoặc chỉnh sửa ngay nhé.",
        "Danh mục đã sẵn sàng! Hãy nhập danh mục mới hoặc thay đổi nếu cần.",
        "Danh mục đã lưu thành công! Hãy nhập danh mục mới hoặc cập nhật nếu có thay đổi.",
        "Mọi thứ đã sẵn sàng! Hãy nhập danh mục mới hoặc chỉnh sửa ngay nhé.",
        "Danh mục đã được thiết lập xong! Hãy nhập danh mục mới hoặc nhấn 'Quickly Add Category' để cập nhật."
    };

        return reminders[_random.Next(reminders.Count)];
    }

    public static string GetWelcomeMessage()
    {
        return GetRandomResponse("welcome");
    }
    public static string GetQuickExpenseMessage()
    {
        return GetRandomResponse("quick_expense");
    }
    public static string GetQuickIncomeMessage()
    {
        return GetRandomResponse("quick_income");
    }
    public static string GetQuickBudgetMessage()
    {
        return GetRandomResponse("quick_budget");
    }
    public static string GetErrorParsingResponse()
    {
        return GetRandomResponse("error_parsing");
    }
    public static string GetQuickCreateCategorytMessage()
    {
        return GetRandomResponse("quick_create_category");
    }

    public static string GetQuickCreateCategoryIncometMessage()
    {
        return GetRandomResponse("quick_create_income_category");
    }

    public static string GetQuickFinacialAnalistMessage()
    {
        return GetRandomResponse("financial_trend_analysis");
    }
    public static string GetQuickSavingSuggestionsMessage()
    {
        return GetRandomResponse("smart_saving_suggestions");
    }
    
    public static string GetGeneralResponse(string info)
    {
        return GetRandomResponse("other").Replace("{info}", info);
    }

    public static string GetCreateCategorySuccess(string category, long amount, string date)
    {
        string formattedAmount = FormatAmount(amount);
        return GetRandomResponse("create_category_success")
            .Replace("{category}", category)
            .Replace("{amount}", formattedAmount)
            .Replace("{date}", date);
    }

    public static string GetBudgetEntrySuccess(long amount, string startDate, string endDate)
    {
        string formattedAmount = FormatAmount(amount);
        return GetRandomResponse("create_budget_success")
            .Replace("{startDate}", startDate)
            .Replace("{amount}", formattedAmount)
            .Replace("{endDate}", endDate);
    }

    public static string GetCreatIncomeCategorySuccess(string category, long amount, string date)
    {
        string formattedAmount = FormatAmount(amount);
        return GetRandomResponse("create_income_category_success")
            .Replace("{category}", category)
            .Replace("{amount}", formattedAmount)
            .Replace("{date}", date);
    }
    public static string GetIncomeEntrySuccess(string description, long amount, string date)
    {
        string formattedAmount = FormatAmount(amount);
        return GetRandomResponse("income_entry_success")
            .Replace("{description}", description)
            .Replace("{amount}", formattedAmount)
            .Replace("{date}", date);
    }

    public static string GetExpenseEntrySuccess(string description, long amount, string date)
    {
        string formattedAmount = FormatAmount(amount);
        return GetRandomResponse("expense_entry_success")
            .Replace("{description}", description)
            .Replace("{amount}", formattedAmount)
            .Replace("{date}", date);
    }
    public static string GetCategoryCreationMessage(string categoryName,string categoryType)
    {
        return GetRandomResponse("create_category_in_group_success")
            .Replace("{categoryName}", categoryName)
            .Replace("{description}", categoryType) 
            .Replace("{date}", DateTime.Now.ToString("dd/MM/yyyy"));
    }
    public static string ReplaceCategoryUpdateMessage(int existingCategoryId, int suggestedGroupId, string description)
    {
        string message = GetRandomResponse("update_category_in_group_success");

        message = message.Replace("{categoryName}", existingCategoryId.ToString())
                         .Replace("{description}", description)
                         .Replace("{groupName}", suggestedGroupId.ToString())
                         .Replace("{date}", DateTime.Now.ToString("dd/MM/yyyy"));

        return message;
    }

    public static string GetFallbackResponse()
    {
        return GetRandomResponse("fallback");
    }

    private static string GetRandomResponse(string key)
    {
        if (Responses.TryGetValue(key, out var responses))
        {
            return responses[_random.Next(responses.Count)];
        }
        return "Mình chưa hiểu lắm, bạn thử ghi chú lại kiểu khác nhé! 😊";
    }

    private static string FormatAmount(long amount)
    {
        try
        {
            return amount.ToString("N0");
        }
        catch (Exception)
        {
            return amount.ToString();
        }
    }
    

}
