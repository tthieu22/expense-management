using ExpenseManagement.Controller;
using System;
using System.Data;
using System.Windows.Forms;
using Windows.System;

namespace ExpenseManagement.Views
{
    public partial class Profile : Form
    {
        private UserController _userController = new UserController();
        private int _userID;
        private string _userName;

        public Profile(int userId)
        {
            InitializeComponent();
            _userID = userId;
            LoadUserInfo();
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            LoadUserGreeting();
        }

        private void LoadUserInfo()
        {
            DataRow userInfo = _userController.GetUserInfo(_userID);
            if (userInfo != null)
            {
                _userName = userInfo["full_name"].ToString();
                tbFullname.Text = _userName;
                tbEmail.Text = userInfo["email"].ToString();
                tbPhone.Text = userInfo["phone_number"].ToString();
                tbDateOfBirth.Value = userInfo["date_of_birth"] != DBNull.Value ? Convert.ToDateTime(userInfo["date_of_birth"]) : DateTime.Now;
            }
        }

        private void LoadUserGreeting()
        {
            string[] greetings =
            {
                "Chúc bạn một ngày vui vẻ!",
                "Hy vọng hôm nay sẽ là một ngày tuyệt vời!",
                "Chào mừng bạn trở lại!",
                "Hãy tận hưởng ngày mới nhé!",
                "Rất vui khi gặp lại bạn!"
            };

            Random rnd = new Random();
            string randomGreeting = greetings[rnd.Next(greetings.Length)];

            lbFullname.Text = $"Xin chào, {_userName}!";
            lbgreetings.Text = randomGreeting;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string fullName = tbFullname.Text;
            string email = tbEmail.Text;
            string phoneNumber = tbPhone.Text;
            DateTime? dateOfBirth = tbDateOfBirth.Value;

            int updated = _userController.UpdateUserInfo(_userID, email, phoneNumber, fullName, dateOfBirth);

            if (updated>0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            string currentPass = tbPassCurrent.Text;
            string newPass = tbNewPass.Text;
            string confirmPass = tbNewPassConfirm.Text;

            if (newPass != confirmPass)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không trùng khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int result = _userController.ChangePassword(_userID, currentPass, newPass);
            if (result > 0)
            {
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Properties.Settings.Default.IsRememberMe = false;
                Properties.Settings.Default.Username = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();

                this.Close();
                Home.GetInstance(_userID).Close();

                SignIn signIn = new SignIn();
                signIn.Show();
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            char passwordChar = chkShowPassword.Checked ? '\0' : '*';
            tbPassCurrent.PasswordChar = passwordChar;
            tbNewPass.PasswordChar = passwordChar;
            tbNewPassConfirm.PasswordChar = passwordChar;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
