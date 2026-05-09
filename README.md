# 💰 Expense Management - Smart Financial Assistant

![Expense Management Logo](expense_management_logo_1778343212469.png)

## 🌟 Giới thiệu
**Expense Management** là một ứng dụng quản lý tài chính cá nhân hiện đại được xây dựng trên nền tảng .NET WinForms. Với sự kết hợp giữa giao diện người dùng cao cấp (Bunifu UI) và trí tuệ nhân tạo (Google Gemini AI), ứng dụng không chỉ giúp bạn ghi chép chi tiêu mà còn đưa ra những lời khuyên thông minh để tối ưu hóa kế hoạch tài chính.

---

## ✨ Tính năng nổi bật

### 1. Quản lý Giao dịch Thông minh
- **Thu nhập & Chi tiêu:** Theo dõi chi tiết các khoản thu chi hàng ngày.
- **Phân loại tự động:** Sử dụng AI để gợi ý danh mục chi tiêu phù hợp.
- **Đính kèm hình ảnh:** Tích hợp Cloudinary để lưu trữ ảnh hóa đơn/biên lai trực tuyến.

### 2. Kế hoạch & Ngân sách
- **Thiết lập Ngân sách:** Đặt hạn mức chi tiêu cho từng danh mục (Ăn uống, Di chuyển, Giải trí...).
- **Cảnh báo vượt mức:** Tự động thông báo khi chi tiêu sắp chạm ngưỡng ngân sách đã đề ra.
- **Tiết kiệm (Savings):** Lập kế hoạch cho các mục tiêu tiết kiệm dài hạn.

### 3. Phân tích & Báo cáo
- **Dashboard trực quan:** Biểu đồ xu hướng tài chính, tỉ lệ chi tiêu theo danh mục.
- **Báo cáo chi tiết:** Xuất dữ liệu ra file **Excel** để quản lý chuyên sâu.
- **Thống kê Admin:** Chế độ xem tổng quan dành cho quản trị viên.

### 4. Hệ thống Trí tuệ Nhân tạo Đa tầng (AI-First)
- **Quick Entry (Nhập liệu nhanh):** Cho phép người dùng nhập chi tiêu/thu nhập bằng ngôn ngữ tự nhiên (ví dụ: *"Ăn sáng 30k"* hoặc *"Lương tháng này 15 triệu"*). AI sẽ tự động bóc tách số tiền, hạng mục và thời gian.
- **Tự động hóa Danh mục:** Nếu một danh mục mới chưa tồn tại, AI sẽ tự động đề xuất tạo mới với Icon (FontAwesome) và Nhóm danh mục phù hợp.
- **Trợ lý Tài chính "Moni":** Chat trực tiếp với trợ lý ảo để phân tích xu hướng chi tiêu, nhận lời khuyên tiết kiệm hoặc giải đáp các thắc mắc về tài chính.
- **Cơ chế Fallback thông minh:** Tích hợp cả **Google Gemini API** và **LM Studio** (Local LLM - ví dụ: Gemma) để đảm bảo hệ thống AI luôn hoạt động ngay cả khi không có internet hoặc API bị giới hạn.
- **Cute Response Generator:** Hệ thống phản hồi được cá nhân hóa, mang lại cảm giác thân thiện và tạo động lực cho người dùng trong việc quản lý tiền bạc.

---

## 🛠 Công nghệ sử dụng

| Công nghệ | Chi tiết |
| :--- | :--- |
| **Ngôn ngữ** | C# (C-Sharp) |
| **Framework** | .NET Framework 4.8 |
| **Giao diện** | Bunifu UI Framework (Premium Components) |
| **Cơ sở dữ liệu** | SQL Server |
| **ORM** | ServiceStack.OrmLite |
| **AI API** | Google Gemini AI |
| **Lưu trữ ảnh** | Cloudinary API |
| **Thư viện khác** | Newtonsoft.Json, ExcelDataReader, Microsoft.Office.Interop.Excel |

---

## 📂 Cấu trúc dự án
Dự án được tổ chức theo mô hình **MVC (Model-View-Controller)** giúp mã nguồn dễ dàng bảo trì và mở rộng:
- `Controllers/`: Xử lý logic điều hướng và kết nối giữa Model và View.
- `Models/`: Định nghĩa cấu trúc dữ liệu và logic tương tác với SQL Server.
- `Views/`: Chứa các Form giao diện người dùng (WinForms).
- `Core/`: Các dịch vụ cốt lõi (Gemini API Service, Excel Exporter, Cloudinary Helper...).
- `Public/`: Tài nguyên tĩnh như Icons, Hình ảnh.

---

## 🚀 Hướng dẫn cài đặt

### Yêu cầu hệ thống
- Visual Studio 2019 trở lên.
- SQL Server (Express hoặc bản đầy đủ).
- .NET Framework 4.8 Runtime.

### Các bước thực hiện
1. **Clone dự án:**
   ```bash
   git clone https://github.com/yourusername/expense-management.git
   ```
2. **Thiết lập Cơ sở dữ liệu:**
   - Mở SQL Server Management Studio (SSMS).
   - Tạo database mới tên là `expense_management`.
   - Chạy script trong file `database.sql` để tạo bảng và dữ liệu mẫu.
3. **Cấu hình Connection String:**
   - Mở file `ExpenseManagement/Models/Connect.cs`.
   - Cập nhật biến `connectionString` phù hợp với máy của bạn.
4. **Cấu hình API Key (Tùy chọn):**
   - Đăng ký API Key tại [Google AI Studio](https://aistudio.google.com/).
   - Cấu hình Key trong `GeminiApiService.cs` hoặc giao diện Settings của ứng dụng.
5. **Build & Run:**
   - Mở file `.sln` bằng Visual Studio.
   - Nhấn `F5` để khởi chạy ứng dụng.

---

## 📸 Ảnh chụp màn hình
*(Lưu ý: Dưới đây là minh họa thiết kế UI)*

![Dashboard Preview](https://res.cloudinary.com/demo/image/upload/v1631234567/sample.jpg)
> Giao diện Dashboard hiện đại với Bunifu Charts.

---

## 📝 Giấy phép
Dự án được phát hành dưới bản quyền cá nhân. Vui lòng liên hệ tác giả nếu muốn sử dụng cho mục đích thương mại.

---
**Phát triển bởi [Hieu T]**  
*Nếu bạn thấy dự án này hữu ích, hãy tặng cho mình 1 ⭐ nhé!*
