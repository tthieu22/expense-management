using System;
using System.Windows.Forms;
using ExpenseManagement.Controller;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ExpenseManagement.Views
{
    public partial class SignIn : Form
    {
        private readonly UserController userController;

        public SignIn()
        {
            InitializeComponent();
            userController = new UserController();
            AutoLogin();
        }
        private void AutoLogin()
        {
            if (Properties.Settings.Default.IsRememberMe)
            {
                string savedUsername = Properties.Settings.Default.Username;
                string savedPassword = Properties.Settings.Default.Password;

                if (userController.Authenticate(savedUsername, savedPassword, out int userId, out string fullName, out string role))
                {
                    this.Hide();
                    this.WindowState = FormWindowState.Minimized;

                    if (role == "admin")
                    {
                        var adminForm = new Home(userId);
                        adminForm.Show();
                    }
                    else
                    {
                        var homeForm = new Home(userId);
                        homeForm.Show();
                    }
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            string username = txtUsernameRegister.Text.Trim();
            string password = txtPasswordRegister.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Sign Up Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Sign Up Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (password == confirmPassword && !string.IsNullOrEmpty(username))
            {
                txtConfirmPassword.Visible = false;
                txtUsernameRegister.Visible = false;
                txtPasswordRegister.Visible = false;
                btnNext.Visible = false;
                showFormInfor();
            }
        }
        public void ShowFormSignUp()
        {
            plSignUp.Visible = true;
        }
        public void ShowFormLogin()
        {
            plLogin.Visible = true;
        }
        public void showFormInfor()
        {
            showInfor.Visible = true;
            txtEmail.Visible = true;
            txtFullName.Visible = true;
            txtPhoneNumber.Visible = true;
        }

        public void HideFormSignUp()
        {
            plSignUp.Visible = false;
        }
        public void HideFormLogin()
        {
            plLogin.Visible = false;
        }
        public void HideFormInfor()
        {
            showInfor.Visible = false;
            txtEmail.Visible = false;
            txtFullName.Visible = false;
            txtPhoneNumber.Visible = false;
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string username = txtUsernameRegister.Text.Trim();
            string password = txtPasswordRegister.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phoneNumber = txtPhoneNumber.Text.Trim();
            string fullName = txtFullName.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Please fill in all fields.", "Sign Up Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Sign Up Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (userController.Register(username, password, email, phoneNumber, fullName, "user", null))
            {
                MessageBox.Show("Account created successfully.", "Sign Up Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                var signInForm = new SignIn();
                signInForm.Show();
            }
            else
            {
                MessageBox.Show("Error creating account. Username or Email may already be taken.", "Sign Up Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnShowLogin_Click(object sender, EventArgs e)
        {
            ShowFormLogin();
            HideFormInfor();
            HideFormSignUp();
        }

        private void plLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkRememberMe_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPasswordRegister_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsernameRegister_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (userController.Authenticate(username, password, out int userId, out string fullName, out string role))
            {
                if (chkRememberMe.Checked)
                {
                    Properties.Settings.Default.Username = username;
                    Properties.Settings.Default.Password = password;
                    Properties.Settings.Default.IsRememberMe = true;
                }
                else
                {
                    Properties.Settings.Default.Username = string.Empty;
                    Properties.Settings.Default.Password = string.Empty;
                    Properties.Settings.Default.IsRememberMe = false;
                }
                Properties.Settings.Default.Save();

                this.Hide();

                if (role == "admin")
                {
                    var adminForm = new Home(userId);
                    adminForm.Show();
                }
                else
                {
                    var homeForm = new Home(userId);
                    homeForm.Show();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnShowSignUp_Click(object sender, EventArgs e)
        {
            ShowFormSignUp();
            HideFormLogin();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();

        }
    }
}
