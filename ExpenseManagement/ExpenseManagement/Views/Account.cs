using ExpenseManagement.Controller;
using System;
using System.Data;
using System.Windows.Forms;

namespace ExpenseManagement.Views
{
    public partial class Account : Form
    {
        private readonly UserController userController;
        private int _userID;

        public Account(int userID)
        {
            this._userID = userID;
            userController = new UserController();
            InitializeComponent();
            LoadUsers();
            InitializeRoleComboBox();
        }

        private void LoadUsers()
        {
            dgvAccount.DataSource = userController.GetAllUsers();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text.Trim();
            string password = tbPassword.Text.Trim();
            string email = tbEmail.Text.Trim();
            string phoneNumber = tbPhone.Text.Trim();
            string fullName = tbFullname.Text.Trim();
            string role = cbRole.Text;
            DateTime? dateOfBirth = dtpkDateofBirth.Value;

            if (userController.Register(username, password, email, phoneNumber, fullName, role, dateOfBirth))
                LoadUsers();
        }

        private void InitializeRoleComboBox()
        {
            cbRole.Items.Add("user");
            cbRole.Items.Add("admin");
            cbRole.SelectedIndex = 0;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(tbUserId.Text.Trim()); 
            string username = tbUsername.Text.Trim();
            string email = tbEmail.Text.Trim();
            string phoneNumber = tbPhone.Text.Trim();
            string fullName = tbFullname.Text.Trim();
            string role = cbRole.Text;
            DateTime? dateOfBirth = dtpkDateofBirth.Value;

            if (userController.UpdateUser(userId, username, email, phoneNumber, fullName, dateOfBirth, role))
                LoadUsers();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
             int userId = Convert.ToInt32(tbUserId.Text.Trim());

             if (userController.DeleteUser(userId))
                 LoadUsers();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string searchValue = tbSearch.Text.Trim().ToLower();
            DataTable users = userController.GetAllUsers();
            DataView dv = new DataView(users);
            dv.RowFilter = $"username LIKE '%{searchValue}%' OR full_name LIKE '%{searchValue}%'";
            dgvAccount.DataSource = dv.ToTable();
        }

        private void dgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvAccount.Rows[e.RowIndex];
                tbUserId.Text = row.Cells["user_id"].Value.ToString();
                tbUsername.Text = row.Cells["username"].Value?.ToString();
                tbEmail.Text = row.Cells["email"].Value?.ToString();
                tbPhone.Text = row.Cells["phone_number"].Value?.ToString();
                tbFullname.Text = row.Cells["full_name"].Value?.ToString();
                cbRole.Text = row.Cells["role"].Value?.ToString();

                if (DateTime.TryParse(row.Cells["date_of_birth"].Value?.ToString(), out DateTime dob))
                {
                    dtpkDateofBirth.Value = dob;
                }
                else
                {
                    dtpkDateofBirth.Value = DateTime.Today;
                }
            }
        }
    }
}
