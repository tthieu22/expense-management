# 💼 Expense Management

## 📌 Mô tả
Expense Management là một ứng dụng quản lý chi tiêu cá nhân, giúp người dùng theo dõi và phân loại các khoản thu chi một cách dễ dàng. Dự án được xây dựng với .NET và SQL Server.

---

## 💻 Công nghệ sử dụng

- .NET Framework (Windows Forms)
- SQL Server
- C#
- Visual Studio

---

## ⚙️ HƯỚNG DẪN CÀI ĐẶT

### 🧱 Yêu cầu hệ thống

- Visual Studio 2019 trở lên
- SQL Server (2017 hoặc mới hơn)
- SQL Server Management Studio (SSMS)
- .NET Framework phù hợp (4.x)
- Internet để cài thư viện từ NuGet

---

### 📁 1. Mở Project

1. Tải project về máy.
2. Mở Visual Studio.
3. Vào menu **File > Open > Project/Solution...**
4. Chọn file `ExpenseManagement.sln`

---

### 📦 2. Cài đặt các thư viện NuGet

#### Tự động (nếu bật Package Restore)

- Khi mở project, Visual Studio sẽ tự động tải các thư viện nếu thiếu.

#### Thủ công (nếu cần)

1. Chuột phải vào project trong Solution Explorer.
2. Chọn **Manage NuGet Packages**
3. Cài đặt các gói cần thiết (ví dụ):
   - `System.Data.SqlClient`
   - `Microsoft.EntityFrameworkCore`
   - `Newtonsoft.Json`

---

### 🗄️ 3. Cài đặt cơ sở dữ liệu

🗄️ HƯỚNG DẪN CÀI ĐẶT CƠ SỞ DỮ LIỆU VỚI SQL SERVER
✅ Yêu cầu trước khi thực hiện
Đã cài đặt:

SQL Server (2017 trở lên)

SQL Server Management Studio (SSMS) để quản lý cơ sở dữ liệu

Có file database.sql (được cung cấp trong thư mục của project)

Import file database.sql vào cơ sở dữ liệu

Import trực tiếp trong SSMS
Chọn menu File > Open > File...

Tìm đến file database.sql trong thư mục dự án và mở file này.

Ctr + A => F5 chạy file vừa mở để thực hiện tạo cơ sở dữ liệu