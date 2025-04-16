using System;
using System.Data;
using System.Windows.Forms;
using ExpenseManagement.Controller;
using System.IO;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using Windows.System;
using ExpenseManagement.Controllers;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Messaging;

namespace ExpenseManagement.Views
{
    public partial class Savings : Form
    {
        private readonly int _userId;
        private readonly SavingSuggestionController _controller;
        private readonly SavingMainController _controllerSaving;
        private NotifyIcon notifySavingReminder;
        private MessageManager messageManager;
        private int _goal_id;
        private int _month;
        private int _year;
        private string _message = string.Empty;
        public Savings(int userId)
        {
            InitializeComponent();
            _userId = userId;
            _controller = new SavingSuggestionController();
            _controllerSaving = new SavingMainController();
            ShowSavingNotification();
            LoadExpenseReductionSuggestions();
            LoadSavingReminder();
            LoadAvailableBalance();
            LoadSavingGoals();
            SavingTrends();
        }
        private void ShowSavingNotification()
        {
            notifySavingReminder = new NotifyIcon
            {
                Icon = SystemIcons.Information,
                BalloonTipTitle = "Nhắc nhở tiết kiệm",
                BalloonTipText = _controllerSaving.GetSavingReminder(_userId, DateTime.Now.Month, DateTime.Now.Year),
                Visible = true
            };
            notifySavingReminder.ShowBalloonTip(3000);
        }
        private void LoadExpenseReductionSuggestions()
        {
            var controller = new SavingMainController();
            var suggestions = controller.SuggestExpenseReduction(_userId, DateTime.Now.Month, DateTime.Now.Year);

            dgvCutbackCategories.Rows.Clear();

            if (dgvCutbackCategories.Columns.Count == 0)
            {
                dgvCutbackCategories.Columns.Add("Category", "Danh mục");
                dgvCutbackCategories.Columns.Add("Reduction", "Giảm chi tiêu (VND)");
            }

            foreach (var item in suggestions)
            {
                dgvCutbackCategories.Rows.Add(item.Key, item.Value.ToString("N0") + " VND");
            }

            dgvCutbackCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCutbackCategories.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        // Nhắc nhở tiết kiệm
        private void LoadSavingReminder()
        {

            var controller = new SavingMainController();
            string reminder = _controllerSaving.GetSavingReminder(_userId, DateTime.Now.Month, DateTime.Now.Year);
            richTextBoxSavingReminder.Text = reminder;
        }
        // số dư khả dụng
        private void LoadAvailableBalance()
        {
            var controller = new SavingMainController();
            decimal balance = _controllerSaving.GetAvailableBalance(_userId, DateTime.Now.Month, DateTime.Now.Year);
            lblAvailableBalance.Text = $"{balance:N0}đ";
        }
        // Các mục tiêu tiết kiệm
        private void LoadSavingGoals()
        {
            var controller = new SavingMainController();
            DataTable savingsTable = controller.GetUserSavings(_userId);

            if (!savingsTable.Columns.Contains("progress"))
            {
                savingsTable.Columns.Add("progress", typeof(decimal));

                foreach (DataRow row in savingsTable.Rows)
                {
                    decimal targetAmount = row["target_amount"] != DBNull.Value ? Convert.ToDecimal(row["target_amount"]) : 0;
                    decimal savedAmount = row["saved_amount"] != DBNull.Value ? Convert.ToDecimal(row["saved_amount"]) : 0;

                    row["progress"] = targetAmount > 0 ? (savedAmount / targetAmount) * 100 : 0;
                }
            }

            dataGridViewSavingsGoals.DataSource = savingsTable;

            if (dataGridViewSavingsGoals.Columns.Contains("progress"))
            {
                dataGridViewSavingsGoals.Columns["progress"].DefaultCellStyle.Format = "N2";
                dataGridViewSavingsGoals.Columns["progress"].HeaderText = "Tiến độ (%)";
            }

            dataGridViewSavingsGoals.Columns["goal_id"].Visible = false;
            dataGridViewSavingsGoals.Columns["user_id"].Visible = false;
            dataGridViewSavingsGoals.Columns["goal_name"].HeaderText = "Mục tiêu";
            dataGridViewSavingsGoals.Columns["target_amount"].HeaderText = "Số tiền cần";
            dataGridViewSavingsGoals.Columns["saved_amount"].HeaderText = "Đã tiết kiệm";
            dataGridViewSavingsGoals.Columns["target_date"].HeaderText = "Ngày kết thúc";

            if (dataGridViewSavingsGoals.Columns.Contains("priority_level"))
                dataGridViewSavingsGoals.Columns["priority_level"].Visible = false;

            if (dataGridViewSavingsGoals.Columns.Contains("saved_this_month"))
                dataGridViewSavingsGoals.Columns["saved_this_month"].Visible = false;

            if (dataGridViewSavingsGoals.Columns.Contains("monthly_required_amount"))
                dataGridViewSavingsGoals.Columns["monthly_required_amount"].Visible = false;

            dataGridViewSavingsGoals.Columns["created_at"].Visible = false;
            if (dataGridViewSavingsGoals.Columns.Contains("remaining_amount"))
                dataGridViewSavingsGoals.Columns["remaining_amount"].Visible = false;
            if (dataGridViewSavingsGoals.Columns.Contains("is_completed_this_month"))
                dataGridViewSavingsGoals.Columns["is_completed_this_month"].Visible = false;
            if (dataGridViewSavingsGoals.Columns.Contains("last_saved_at"))
                dataGridViewSavingsGoals.Columns["last_saved_at"].Visible = false;
            if (dataGridViewSavingsGoals.Columns.Contains("extra_amount"))
                dataGridViewSavingsGoals.Columns["extra_amount"].Visible = false;

            dataGridViewSavingsGoals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewSavingsGoals.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        private void SavingTrends()
        {
            DataTable dt = _controller.TrackSavingTrends(_userId);

            chartSavingTrends.Series.Clear();

            Series series = new Series("Saving Trends")
            {
                ChartType = SeriesChartType.Column
            };

            foreach (DataRow row in dt.Rows)
            {
                string month = row["month"].ToString();
                decimal total = Convert.ToDecimal(row["total"]);
                series.Points.AddXY(month, total);
            }

            chartSavingTrends.Series.Add(series);

            chartSavingTrends.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartSavingTrends.ChartAreas[0].AxisX.Interval = 1;
        }


        private async void Savings_Load(object sender, EventArgs e)
        {
            messageManager = new MessageManager(flMessage, OnMessageClicked);
            await messageManager.DisplayMessages();

            LoadData();
            DisplaySavingReminders();
        }

        private void OnMessageClicked(string combinedMessage)
        {
            string[] parts = combinedMessage.Split(new[] { " | " }, StringSplitOptions.None);
            string text = parts[0];  // Phần hiển thị
            string message = parts.Length > 1 ? parts[1] : "";

            _message = message;

            Home homeForm = Home.GetInstance(_userId);
            if (homeForm != null)
            {
                homeForm.ProcessToastAction($"openDashboard|{text} - {message}");
                this.Close();
            }
        }
        private void LoadData()
        {
            _month = DateTime.Now.Month;
            _year = DateTime.Now.Year;

            decimal totalIncome = _controllerSaving.GetMonthlyIncome(_userId, _month, _year);
            decimal totalExpenses = _controllerSaving.GetMonthlyExpenses(_userId, _month, _year);
            decimal yearlyIncome = _controllerSaving.GetYearlyIncome(_userId, _year);
            decimal yearlyExpenses = _controllerSaving.GetYearlyExpenses(_userId, _year);
            decimal predictedExpenses = _controllerSaving.PredictMonthEndBalance(_userId, _month, _year);
            decimal monthEndBalance = _controllerSaving.PredictNextMonthExpenses(_userId);
            decimal suggestedSaving = _controllerSaving.SuggestSavingAmount(_userId, _year);

            decimal yearlyBalance = yearlyIncome - yearlyExpenses;
            
            lblTotalIncome.Text = totalIncome.ToString("N0");
            lblTotalExpenses.Text = totalExpenses.ToString("N0");
            lbIncomeYear.Text = yearlyIncome.ToString("N0");
            lbExpenseYear.Text = yearlyExpenses.ToString("N0");
            lbBlanceYear.Text = yearlyBalance.ToString("N0");
            var topExpenses = _controllerSaving.GetTopExpenseCategories(_userId, _month, _year);
            DataTable dt = ConvertDictionaryToDataTable(topExpenses);
            DataTable top = _controllerSaving.GetTop5ExpensesInMonth(_userId, _month, _year);

            if (top.Rows.Count > 0)
            {
                dgvTopExpenses.DataSource = top;

                if (dgvTopExpenses.Columns.Contains("user_id"))
                    dgvTopExpenses.Columns["user_id"].Visible = false;

                if (dgvTopExpenses.Columns.Contains("expense_id"))
                    dgvTopExpenses.Columns["expense_id"].Visible = false;

                dgvTopExpenses.Columns["category_name"].HeaderText = "Danh mục";
                dgvTopExpenses.Columns["category_type"].HeaderText = "Loại chi tiêu";
                dgvTopExpenses.Columns["amount"].HeaderText = "Số tiền";
                dgvTopExpenses.Columns["expense_date"].HeaderText = "Ngày chi tiêu";
                dgvTopExpenses.Columns["description"].HeaderText = "Mô tả";
                dgvTopExpenses.Columns["payment_method"].HeaderText = "Phương thức thanh toán";

            }
            else
            {
                MessageBox.Show("Không có dữ liệu chi tiêu trong tháng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dgvTopExpenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTopExpenses.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lblPredictedExpenses.Text = predictedExpenses.ToString("N0");
            lblMonthEndBalance.Text = monthEndBalance.ToString("N0");
            lblSuggestedSaving.Text = suggestedSaving.ToString("N0");

            decimal yearlyCompletion = 0;
            try
            {
                yearlyCompletion = _controllerSaving.CalculateCompletionRateByYear(_userId, _year);
                if (yearlyCompletion < 0 || yearlyCompletion > 100) yearlyCompletion = 0;
            }
            catch
            {
                yearlyCompletion = 0;
            }

            int yearlyCompletionInt = (int)Math.Min(Math.Max(yearlyCompletion, 0), 100);
            lblYearlyRate.Text = $"Năm {_year} bạn đã hoàn thành được {yearlyCompletionInt}% các mục tiêu tiết kiệm";
            progressBarYear.Value = yearlyCompletionInt;
            lblSavingProgressYear.Text = $"{yearlyCompletionInt}%";


            // Tổng cộng
            decimal totalCompletion = 0;
            try
            {
                totalCompletion = _controllerSaving.CalculateTotalCompletionRate(_userId);
                if (totalCompletion < 0 || totalCompletion > 100) totalCompletion = 0;
            }
            catch
            {
                totalCompletion = 0;
            }

            int totalCompletionInt = (int)Math.Min(Math.Max(totalCompletion, 0), 100);
            lbStatusComplete.Text = $"Bạn đã hoàn thành {totalCompletionInt}% các mục tiêu tiết kiệm từ trước đến nay";
            progressSaving.Value = totalCompletionInt;
            lblSavingProgress.Text = $"{totalCompletionInt}%";

            LoadCombobox();
        }

        public void LoadCombobox()
        {

            // Load Mục tiêu chưa hoàn thành vào combo box lấy id 

            DataTable savingLost = _controllerSaving.GetUnfinishedGoals(_userId);
            cbNameSaving.DataSource = savingLost;
            cbNameSaving.DisplayMember = "goal_name";
            cbNameSaving.ValueMember = "goal_id";
        }
        private DataTable ConvertDictionaryToDataTable(Dictionary<string, decimal> dictionary)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Category Name", typeof(string)); 
            dt.Columns.Add("Total Spent", typeof(decimal)); 

            foreach (var item in dictionary)
            {
                DataRow row = dt.NewRow();
                row["Category Name"] = item.Key;
                row["Total Spent"] = item.Value;
                dt.Rows.Add(row);
            }

            return dt;
        }

        private void btnCreateSavingPlan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNameSaving.Text))
            {
                MessageBox.Show("Tên kế hoạch không được để trống.");
                return;
            }

            if (txtNameSaving.Text.Length > 100)
            {
                MessageBox.Show("Tên kế hoạch không được quá 100 ký tự.");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbDesc.Text))
            {
                MessageBox.Show("Mô tả không được để trống.");
                return;
            }

            if (tbDesc.Text.Length > 255)
            {
                MessageBox.Show("Mô tả không được quá 255 ký tự.");
                return;
            }

            if (!decimal.TryParse(txtTargetAmount.Text, out decimal targetAmount) || targetAmount <= 0)
            {
                MessageBox.Show("Số tiền mục tiêu phải là số dương hợp lệ.");
                return;
            }

            if (!int.TryParse(numMonths.Value.ToString(), out int months) || months <= 0)
            {
                MessageBox.Show("Số tháng phải là số nguyên dương.");
                return;
            }

            bool success = _controllerSaving.AddSavingGoal(_userId, txtNameSaving.Text, targetAmount, months, tbDesc.Text);
            MessageBox.Show(success ? "Saving Plan Created!" : "Failed to Create Saving Plan");
            LoadSavingGoals();
            LoadCombobox();
        }

        private void btnAddSaving_Click(object sender, EventArgs e)
        {
            if (cbNameSaving.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một mục tiêu!");
                return;
            }

            int goalId = Convert.ToInt32(cbNameSaving.SelectedValue); 
            decimal amount;

            if (!decimal.TryParse(txtAmount.Text, out amount))
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ!");
                return;
            }

            bool success = _controllerSaving.AddSavingAmount(_userId, goalId, amount);

            if (success)
            {
                MessageBox.Show("Cập nhật số tiền tiết kiệm thành công!");
                int isGoalCompleted = _controllerSaving.CheckSavingGoalStatus(_userId,  goalId);

                DisplaySavingReminders();

            }
            else
            {
                MessageBox.Show("Lỗi khi cập nhật số tiền tiết kiệm.");
            }
            LoadSavingGoals();
            LoadCombobox();
        }

        private void dataGridViewSavingsGoals_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridViewSavingsGoals.Rows[e.RowIndex]; 
            _goal_id = row.Cells["goal_id"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["goal_id"].Value) : 0;
            txtNameSaving.Text = row.Cells["goal_name"].Value?.ToString() ?? "";
            txtTargetAmount.Text = row.Cells["target_amount"].Value?.ToString() ?? "0";

            tbDesc.Text = row.Cells["description"].Value?.ToString() ?? "";
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }
        private void DisplaySavingReminders()
        {
            List<Dictionary<string, object>> reminders = _controllerSaving.GetSavingReminders(_userId, _month, _year);
            richTextBoxSavingReminder.Clear();
            richTextBoxSavingReminder.BackColor = Color.AliceBlue; // Đổi màu nền toàn bộ
            richTextBoxSavingReminder.Font = new Font("Roboto", 12, FontStyle.Regular);

            string padding = "     ";

            if (reminders.Count == 0)
            {
                richTextBoxSavingReminder.SelectionFont = new Font("Roboto", 12, FontStyle.Regular);
                richTextBoxSavingReminder.SelectionColor = Color.DarkGray;
                richTextBoxSavingReminder.AppendText("\n" + padding + "Không có mục tiêu tiết kiệm nào trong tháng này.\n");
                return;
            }

            foreach (var reminder in reminders)
            {
                string goalName = reminder["goal_name"].ToString();
                decimal targetSaving = Convert.ToDecimal(reminder["target_amount"]);
                decimal savedAmount = Convert.ToDecimal(reminder["saved_amount"]);
                decimal monthlyRequired = Convert.ToDecimal(reminder["monthly_required_amount"]);
                decimal savedThisMonth = Convert.ToDecimal(reminder["saved_this_month"]);
                decimal remainingThisMonth = Convert.ToDecimal(reminder["remaining_this_month"]);
                decimal extraAmount = Convert.ToDecimal(reminder["extra_amount"]);
                bool isCompletedThisMonth = Convert.ToBoolean(reminder["is_completed_this_month"]);

                // Cách dòng trên cùng trước mỗi mục tiêu
                richTextBoxSavingReminder.AppendText("\n");

                // Hiển thị tiêu đề mục tiêu
                richTextBoxSavingReminder.SelectionFont = new Font("Roboto", 12, FontStyle.Bold);
                richTextBoxSavingReminder.SelectionColor = Color.Black;
                richTextBoxSavingReminder.AppendText(padding + "* " + goalName + "\n\n");

                string message = "";
                Color messageColor = Color.Black;

                if (savedAmount >= targetSaving)
                {
                    message = "Mục tiêu đã hoàn thành!";
                    messageColor = Color.Green;
                }
                else if (remainingThisMonth == 0 && savedThisMonth == 0)
                {
                    message = "Bạn chưa tiết kiệm tháng này. Cần gửi: " + $"{monthlyRequired:N0} VND.";
                    messageColor = Color.Orange;
                }
                else if (isCompletedThisMonth)
                {
                    message = "Đã tiết kiệm đủ tháng này! ";
                    messageColor = Color.Blue;

                    if (extraAmount > 0)
                    {
                        message += $"\n{padding}Dư {extraAmount:N0} VND sẽ cộng vào tháng sau.";
                    }
                }
                else
                {
                    message = "Còn thiếu: " + $"{remainingThisMonth:N0} VND trong tháng này.";
                    messageColor = Color.Red;
                }

                // Hiển thị nội dung với căn chỉnh lề trái
                richTextBoxSavingReminder.SelectionFont = new Font("Roboto", 10, FontStyle.Regular);
                richTextBoxSavingReminder.SelectionColor = messageColor;
                richTextBoxSavingReminder.AppendText(padding + message + "\n");
            }

            richTextBoxSavingReminder.SelectionColor = richTextBoxSavingReminder.ForeColor;
        }

        private void panel22_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa mục tiêu này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                bool dele =  _controllerSaving.DeleteSavingGoal(_goal_id, _userId);
                if (dele)
                {
                    LoadCombobox();
                    MessageBox.Show("Mục tiêu đã được xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

            _message = tbMesage.Text;
            Home homeForm = Home.GetInstance(_userId);
            if (homeForm != null)
            {
                homeForm.ProcessToastAction($"openDashboard|{_message} - {_message}");
                this.Close();
            }
        }
    }
}
