using System;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseManagement.Model
{
    internal class UserModel
    {
        private readonly Connect db = new Connect();

        public bool Login(string username, string password, out DataRow userInfo, out string role)
        {
            userInfo = null;
            role = null;

            string query = "SELECT * FROM Users WHERE username = @username";
            SqlParameter[] parameters = { new SqlParameter("@username", username) };
            DataTable dt = db.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                userInfo = dt.Rows[0];
                role = userInfo["role"].ToString();
                string hashedPasswordFromDb = userInfo["password"].ToString();
                return hashedPasswordFromDb == HashPassword(password);
            }
            return false;
        }
        public string GetFullName(int userId)
        {
            string query = "SELECT full_name FROM Users WHERE user_id = @userId";
            SqlParameter[] parameters = { new SqlParameter("@userId", userId) };
            DataTable dt = db.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["full_name"].ToString();
            }
            return null; 
        }
        public bool CreateAccount(string username, string password, string email, string phoneNumber, string fullName, DateTime? dateOfBirth, string role = "user")
        {
            string checkQuery = "SELECT COUNT(*) FROM Users WHERE username = @username OR email = @Email";
            SqlParameter[] checkParameters = {
                new SqlParameter("@username", username),
                new SqlParameter("@Email", email)
            };
            if (Convert.ToInt32(db.ExecuteScalar(checkQuery, checkParameters)) > 0) return false;

            string insertQuery = "INSERT INTO Users (username, password, email, phone_number, full_name, date_of_birth, date_created, role) VALUES (@Username, @Password, @Email, @PhoneNumber, @FullName, @DateOfBirth, GETDATE(), @Role)";
            SqlParameter[] insertParameters = {
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", HashPassword(password)),
                new SqlParameter("@Email", email),
                new SqlParameter("@PhoneNumber", (object)phoneNumber ?? DBNull.Value),
                new SqlParameter("@FullName", (object)fullName ?? DBNull.Value),
                new SqlParameter("@DateOfBirth", (object)dateOfBirth ?? DBNull.Value),
                new SqlParameter("@Role", role)
            };
            return db.ExecuteNonQuery(insertQuery, insertParameters);
        }

        public bool UpdateUser(int userId, string username, string email, string phoneNumber, string fullName, DateTime? dateOfBirth, string role)
        {
            string updateQuery = "UPDATE Users SET username=@Username, email=@Email, phone_number=@PhoneNumber, full_name=@FullName, date_of_birth=@DateOfBirth, role=@Role WHERE user_id=@UserId";
            SqlParameter[] updateParameters = {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@Username", username),
                new SqlParameter("@Email", email),
                new SqlParameter("@PhoneNumber", (object)phoneNumber ?? DBNull.Value),
                new SqlParameter("@FullName", (object)fullName ?? DBNull.Value),
                new SqlParameter("@DateOfBirth", (object)dateOfBirth ?? DBNull.Value),
                new SqlParameter("@Role", role)
            };
            return db.ExecuteNonQuery(updateQuery, updateParameters);
        }
        public bool DeleteUser(int userId)
        {
            string deleteQuery = @"
                DELETE FROM Budgets WHERE user_id = @UserId;
                DELETE FROM UserHiddenGroups WHERE user_id = @UserId;
                DELETE FROM Expenses WHERE user_id = @UserId;
                DELETE FROM SharedExpenses WHERE user_id = @UserId;
                DELETE FROM Transactions WHERE user_id = @UserId;
                DELETE FROM Categories WHERE user_id = @UserId;
                DELETE FROM Reports WHERE user_id = @UserId;
                DELETE FROM SavingsGoals WHERE user_id = @UserId;
                DELETE FROM Currencies WHERE user_id = @UserId;
                DELETE FROM Investments WHERE user_id = @UserId;
                DELETE FROM UserSettings WHERE user_id = @UserId;
                DELETE FROM Notifications WHERE user_id = @UserId;
                DELETE FROM TransactionLogs WHERE user_id = @UserId;
                DELETE FROM APIKeys WHERE user_id = @UserId;

                -- Xóa từ bảng Gifts (người gửi và người nhận)
                DELETE FROM Gifts WHERE sender_id = @UserId OR receiver_id = @UserId;

                -- Xóa người dùng sau cùng
                DELETE FROM Users WHERE user_id = @UserId;
            ";

            SqlParameter[] deleteParameters = { new SqlParameter("@UserId", userId) };
            return db.ExecuteNonQuery(deleteQuery, deleteParameters);
        }


        public DataTable GetUsers()
        {
            string query = "SELECT TOP 1000 * FROM Users ORDER BY date_created DESC";
            return db.ExecuteQuery(query);
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
        public bool IsAdmin(int userId)
        {
            string query = "SELECT role FROM Users WHERE user_id = @userId";
            SqlParameter[] parameters = { new SqlParameter("@userId", userId) };
            object result = db.ExecuteScalar(query, parameters);

            return result != null && result.ToString().Trim().ToLower() == "admin";
        }
        public int ChangePassword(int userId, string oldPassword, string newPassword)
        {
            string query = "SELECT password FROM Users WHERE user_id = @UserId";
            SqlParameter[] parameters = { new SqlParameter("@UserId", userId) };
            object result = db.ExecuteScalar(query, parameters);

            if (result == null || HashPassword(oldPassword) != result.ToString())
                return -1;

            string updateQuery = "UPDATE Users SET password = @NewPassword WHERE user_id = @UserId";
                    SqlParameter[] updateParameters = {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@NewPassword", HashPassword(newPassword))
            };

            object updateResult = db.ExecuteScalar(updateQuery, updateParameters);
            return Convert.ToInt32(updateResult);
        }

        public int UpdateUserInfo(int userId, string email, string phoneNumber, string fullName, DateTime? dateOfBirth)
        {
            string query = @"
                UPDATE Users
                SET email = @Email, 
                    phone_number = @PhoneNumber, 
                    full_name = @FullName, 
                    date_of_birth = @DateOfBirth
                WHERE user_id = @UserId;
                SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@Email", email),
                new SqlParameter("@PhoneNumber", (object)phoneNumber ?? DBNull.Value),
                new SqlParameter("@FullName", (object)fullName ?? DBNull.Value),
                new SqlParameter("@DateOfBirth", (object)dateOfBirth ?? DBNull.Value)
            };

            object result = db.ExecuteScalar(query, parameters);
            return Convert.ToInt32(result);
        }

        public DataRow GetUserInfo(int userId)
        {
            string query = "SELECT user_id, username, email, phone_number, full_name, date_of_birth, role FROM Users WHERE user_id = @UserId";

                SqlParameter[] parameters = {
                    new SqlParameter("@UserId", userId)
                };

            DataTable result = db.ExecuteQuery(query, parameters);
            return result.Rows.Count > 0 ? result.Rows[0] : null;
        }

    }
}
