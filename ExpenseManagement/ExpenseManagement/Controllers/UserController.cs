using ExpenseManagement.Model;
using System;
using System.Data;

namespace ExpenseManagement.Controller
{
    internal class UserController
    {
        private readonly UserModel userModel;

        public UserController()
        {
            userModel = new UserModel();
        }

        public bool Authenticate(string username, string password, out int userId, out string fullName, out string role)
        {
            userId = 0;
            fullName = string.Empty;
            role = string.Empty;

            if (userModel.Login(username, password, out DataRow userInfo, out role))
            {
                userId = Convert.ToInt32(userInfo["user_id"]);
                fullName = userInfo["full_name"].ToString();
                return true;
            }

            return false;
        }

        public bool Register(string username, string password, string email, string phoneNumber, string fullName, string role, DateTime ? dateOfBirth )
        {
            return userModel.CreateAccount(username, password, email, phoneNumber, fullName, dateOfBirth, role);
        }


        public bool UpdateUser(int userId, string username, string email, string phoneNumber, string fullName, DateTime? dateOfBirth, string role)
        {
            return userModel.UpdateUser(userId, username, email, phoneNumber, fullName, dateOfBirth, role);
        }

        public bool DeleteUser(int userId)
        {
            return userModel.DeleteUser(userId);
        }
        public string GetFullName(int userId)
        {
            return userModel.GetFullName(userId);
        }
        
        public DataTable GetAllUsers()
        {
            return userModel.GetUsers();
        }

        public bool IsAdmin(int userId)
        {
            return userModel.IsAdmin(userId);
        }

        public int ChangePassword(int userId, string currentPassword, string newPassword)
        {
            return userModel.ChangePassword(userId, currentPassword, newPassword);
        }

        public int UpdateUserInfo(int userId, string email, string phoneNumber, string fullName, DateTime? dateOfBirth)
        {
            return userModel.UpdateUserInfo(userId, email, phoneNumber, fullName, dateOfBirth);
        }
        public DataRow GetUserInfo(int userId) { 
            return userModel.GetUserInfo(userId);
        }
    }
}
