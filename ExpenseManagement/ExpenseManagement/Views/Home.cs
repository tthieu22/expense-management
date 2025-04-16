using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ExpenseManagement.Controller;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;

namespace ExpenseManagement.Views
{
    public partial class Home : Form
    {
        private static Home _instance;
        private readonly int userId;
        private readonly Dictionary<string, Form> formCache = new Dictionary<string, Form>();
        private Notifications notificationsForm;
        private readonly UserController _userController = new UserController(); 
        private readonly Stack<string> navigationHistory = new Stack<string>();
        private ToolTip toolTip = new ToolTip();
        private readonly Dictionary<string, string> formTitles = new Dictionary<string, string>
        {
            {"Dashboard", "Moni - Trợ lý tài chính"},
            {"Transaction", "Sổ ghi chép"},
            {"Report", "Thống kê tài chính"},
            {"Category", "Quản lý danh mục"},
            {"Savings", "Tiết kiệm"},
            {"BudgetMain", "Ngân sách"},
            {"Budget", "Chi tiết ngân sách"},
            {"Expenses", "Ghi chép"},
            {"Notifications", "Thông báo"},
            {"Setting", "Quản lý"},
            {"Profile", "Thông tin cá nhân"},
            {"Account", "Tài khoản"},
            {"GroupCategory", "Nhóm danh mục"},
            {"Income", "Thu nhập"},
            {"AddNotifications", "Thêm thông báo"},
        };

        public static Home GetInstance(int userId)
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new Home(userId);
            }
            return _instance;
        }

        public Home(int userId)
        {
            _instance = this;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.userId = userId;
        }


        private void LoadChildForm(string formKey, Form childForm,string message = "")
        {
            try
            {
                if (formCache.ContainsKey(formKey))
                {
                    foreach (Control control in plContentMain.Controls)
                    {
                        control.Hide();
                    }
                    
                    if ( formKey == "Dashboard")
                    {
                        formCache[formKey].Show();
                        formCache[formKey].BringToFront();
                    }
                    else
                    {
                        formCache[formKey].Close();
                        formCache.Remove(formKey);
                        AddNewForm(formKey, childForm);
                    }
                }
                else
                {
                    AddNewForm(formKey, childForm);
                }
                if (navigationHistory.Count == 0 || navigationHistory.Peek() != formKey)
                {
                    navigationHistory.Push(formKey);

                }

                btnBack.Enabled = navigationHistory.Count > 1;
                lbTitle.Text = formTitles.ContainsKey(formKey) ? formTitles[formKey] : formKey;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying child form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddNewForm(string formKey, Form childForm)
        {
            foreach (Control control in plContentMain.Controls)
            {
                control.Hide();
            }

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            plContentMain.Controls.Add(childForm);
            formCache[formKey] = childForm;

            childForm.BringToFront();
            childForm.Show();
        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            LoadChildForm("Transaction", new Transaction(userId));
        }

        private void btnBudget_Click(object sender, EventArgs e)
        {
            LoadChildForm("BudgetMain", new BudgetMain(userId));
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            LoadChildForm("Category", new Category(userId,0));
        }

        private void btnNotifications_Click(object sender, EventArgs e)
        {
            LoadChildForm("Notifications", new Notifications(userId));
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            LoadChildForm("Setting", new Setting(userId));
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            LoadChildForm("Profile", new Profile(userId));
        }

        private void btnDashbard_Click(object sender, EventArgs e)
        {
            LoadChildForm("Dashboard", new Dashboard(userId,""));
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            LoadChildForm("Report", new Report(userId));
        }

        private void btnSavings_Click(object sender, EventArgs e)
        {
            LoadChildForm("Savings", new Savings(userId));
        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {
            LoadChildForm("Expenses", new Expenses(userId,0,0));
        }

        private void Home_Load(object sender, EventArgs e)
        {
            LoadChildForm("Dashboard", new Dashboard(userId,""));

            notificationsForm = new Notifications(userId);
            notificationsForm.Hide();

            ToastNotificationManagerCompat.OnActivated += (toastArgs) =>
            {
                var args = toastArgs.Argument;

                this.Invoke(new Action(() =>
                {
                    ProcessToastAction(args);
                }));
            };
            btnSetting.Visible = _userController.IsAdmin(userId);

        }
        public void ProcessToastAction(string arguments)
        {
            string[] parts = arguments.Split('|');
            string action = parts[0];
            string message = parts.Length > 1 ? parts[1] : "";
            switch (action)
            {
                case "openBudget":
                    if (int.TryParse(message, out int budgetId))
                    {
                        LoadChildForm("Budget", new Budget(userId, budgetId));
                    }
                    break;
                case "openDashboard":
                    if (formCache.ContainsKey("Dashboard") && formCache["Dashboard"] is Dashboard dashboard)
                    {
                        dashboard.ProcessMessage(message);
                        LoadChildForm("Dashboard", new Dashboard(userId, message), message);
                    }
                    else
                    {
                        LoadChildForm("Dashboard", new Dashboard(userId, message), message);
                    }
                    break;
                case "openCategoryGroup":
                    LoadChildForm("CategoryGroup", new GroupCategory(userId));
                    break;

                case "openExpense":
                    if (int.TryParse(message, out int expenseID))
                    {
                        LoadChildForm("Expense", new Expenses(userId, expenseID, 0));
                    }
                    break;

                case "openIncome":
                    if (int.TryParse(message, out int incomeID))
                    {
                        LoadChildForm("Income", new Expenses(userId, 0, incomeID));
                    }
                    break;
                case "openCategory":
                    if (int.TryParse(message, out int categoryId))
                    {
                        LoadChildForm("Category", new Category(userId, categoryId));
                    }
                    break;


                case "openAccount":
                    LoadChildForm("Account", new Account(userId));
                    break;

                case "openAddNotifications":
                    LoadChildForm("AddNotifications", new AddNotifications(userId));
                    break;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát ứng dụng?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (navigationHistory.Count > 1)
            {
                navigationHistory.Pop();

                string previousFormKey = navigationHistory.Peek();

                if (formCache.ContainsKey(previousFormKey))
                {
                    foreach (Control control in plContentMain.Controls)
                    {
                        control.Hide();
                    }

                    formCache[previousFormKey].Show();
                    formCache[previousFormKey].BringToFront();
                }
            }
            else
            {
                btnBack.Enabled = false;
            }
        }

        private void btnBack_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(btnBack, "Quay lại trang trước");
        }

        private void btnClose_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(btnClose, "Đóng ứng dụng");
        }

        private void btnDashbard_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(btnDashbard, "Moni - Trợ lý tài chính");

        }

        private void btnTransaction_MouseHover(object sender, EventArgs e)
        {

            toolTip.SetToolTip(btnTransaction, "Sổ ghi chép");
        }

        private void btnReport_MouseHover(object sender, EventArgs e)
        {

            toolTip.SetToolTip(btnReport, "Thống kê chi tiêu");
        }

        private void btnCategories_MouseHover(object sender, EventArgs e)
        {

            toolTip.SetToolTip(btnCategories, "Quản lý danh mục");
        }

        private void btnSavings_MouseHover(object sender, EventArgs e)
        {

            toolTip.SetToolTip(btnSavings, "Quản lý và gợi ý tiết kiệm");
        }

        private void btnBudget_MouseHover(object sender, EventArgs e)
        {

            toolTip.SetToolTip(btnBudget, "Quản lý ngân sách");
        }

        private void btnExpenses_MouseHover(object sender, EventArgs e)
        {

            toolTip.SetToolTip(btnExpenses, "Ghi chép mới");
        }

        private void btnNotifications_MouseHover(object sender, EventArgs e)
        {

            toolTip.SetToolTip(btnNotifications, "Thông báo");
        }

        private void btnSetting_MouseHover(object sender, EventArgs e)
        {

            toolTip.SetToolTip(btnSetting, "Quản lý");
        }

        private void btnProfile_MouseHover(object sender, EventArgs e)
        {

            toolTip.SetToolTip(btnProfile, "Thông tin cá nhân");
        }
    }
}
