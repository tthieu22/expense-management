using ExpenseManagement.Controllers;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Windows.System;

namespace ExpenseManagement.Views
{
    public partial class BudgetMain : Form
    {
        private MessageManager messageManager;
        private BudgetMainController _budgetController;
        private int _userID;
        private int _month;
        private int _year;
        private string _currency = "đ";
        private string _message = string.Empty;
        public BudgetMain(int userId)
        {
            InitializeComponent();
            _budgetController = new BudgetMainController();
            InitializeCharts();
            _userID = userId;
            _month = DateTime.Now.Month;
            _year = DateTime.Now.Year;

            lbMonthYear.Text = $"{_month}/{_year}";
            
        }

        private async void BudgetMain_Load(object sender, EventArgs e)
        {
            await loadBudget();

            btnAddBudget.Visible = true;
            btnAddBudget.Enabled = true;
            DataTable budgetSummary = _budgetController.GetBudgetSummary(_userID, _month, _year);

            if (budgetSummary.Rows.Count > 0)
            {
                var warningBudgets = budgetSummary.AsEnumerable()
                    .Where(row => row.Field<int>("TrangThai") == 2) 
                    .Select(row => $"{row.Field<string>("DanhMuc")}: {row.Field<decimal>("TyLeDaChi")}%")
                    .ToList();

                lbWarringBudget.Text = warningBudgets.Count > 0
                    ? string.Join(", ", warningBudgets)
                    : "Không có ngân sách nào đạt mức cảnh báo.";
            }
            else
            {
                lbWarringBudget.Text = "Không có dữ liệu ngân sách.";
            }

        }
        public async Task loadBudget()
        {
            messageManager = new MessageManager(flMessage, OnMessageClicked);
            await messageManager.DisplayMessages();
            decimal totalBudget = _budgetController.GetTotalBudget(_userID, _month, _year);
            decimal totalExpenses = _budgetController.GetTotalExpensesByMonth(_userID, _month, _year);
            decimal remainingBudget = totalBudget - totalExpenses;
            DataTable budgetAll = _budgetController.GetBudgetAll(_userID);
            List<string> exceededCategories = new List<string>();

            if (totalBudget == 0)
            {
                new ToastContentBuilder()
                    .AddText("Thông báo")
                    .AddText("Bạn chưa tạo ngân sách tổng. Vui lòng thêm ngân sách để theo dõi chi tiêu.")
                    .AddArgument("action", "openBudget") 
                    .Show();
            }

            DrawBarChart();
            DrawPieChart();
            DrawLineChart();

            decimal warningThreshold = 0;
            foreach (DataRow row in budgetAll.Rows)
            {
                warningThreshold = Convert.ToDecimal(row["warning_threshold"]);
            }

            lbWarringBudget.Text = $" Cảnh báo nếu chi tiêu vượt {warningThreshold} % ngân sách";
            decimal thresholdAmount = totalBudget * (warningThreshold / 100);

            if (totalExpenses > thresholdAmount)
            {
                lbWarningTotal.Text = $"Bạn đã vượt quá {warningThreshold}% ngân sách tổng!";
            }

            lbBudgetTotal.Text = $"{totalBudget:N2} {_currency}";
            lbExpenseTotal.Text = $"{totalExpenses:N2} {_currency}";
            lbExpenseRemaining.Text = $"{remainingBudget:N2} {_currency}";

            DataTable budgetSummary = _budgetController.GetBudgetSummary(_userID, _month, _year);

            if (budgetSummary.Rows.Count != 0)
            {
                foreach (DataRow row in budgetSummary.Rows)
                {
                    string budgetId = row["budget_id"].ToString();
                    string categoryName = row["DanhMuc"].ToString();
                    string total = row["NganSach"].ToString();
                    string expense = row["DaChi"].ToString();
                    string remaining = row["ConLai"].ToString();
                    string status = row["TrangThai"].ToString();
                    string threshold = row["warning_threshold"].ToString();
                    string spentPercentage = row["TyLeDaChi"].ToString();

                    BudgetCategoryItem item = new BudgetCategoryItem(categoryName, total, expense, remaining, status, budgetId, threshold, spentPercentage);

                    flListBudgetCategory.Controls.Add(item);
                    if (status == "1")
                    {
                        exceededCategories.Add(categoryName);
                    }

                    item.OnCategoryClick += (sender, budgetIdClicked) =>
                    {
                        MessageBox.Show($"Category ID clicked: {budgetIdClicked}");
                    };
                }
            }

            if (exceededCategories.Count > 0)
            {
                lbCategorySugsest.Text = "Bạn đã chi tiêu quá nhiều vào " + string.Join(", ", exceededCategories);
            }
            else
            {
                lbCategorySugsest.Text = "Không có danh mục nào vượt ngân sách.";
            }



            DataTable budgetList = _budgetController.GetBudgetAll(_userID);
            if (budgetList != null && budgetList.Rows.Count > 0)
            {
                LoadBudgetList(budgetList);
            }

        }

        private void LoadBudgetList(DataTable budgetList)
        {
            if (budgetList == null || budgetList.Rows.Count == 0)
                return;

            foreach (DataRow row in budgetList.Rows)
                if (row["budget_type"] != DBNull.Value)
                {
                    string budgetType = row["budget_type"].ToString();
                    if (budgetType == "total")
                    {
                        row["budget_type"] = "Ngân sách tổng";
                    }
                    else if (budgetType == "category" && row["category_name"] != DBNull.Value)
                    {
                        row["budget_type"] = "Ngân sách danh mục";
                    }
                }
            dvgListBudget.Columns.Clear();
            dvgListBudget.DataSource = budgetList;

            foreach (string col in new[] { "category_id", "user_id", "budget_id", "category_group_id", "created_by" })
                if (dvgListBudget.Columns.Contains(col)) dvgListBudget.Columns[col].Visible = false;

            var headers = new Dictionary<string, string>
            {
                { "budget_type", "Loại ngân sách" }, { "amount", "Số tiền" },
                { "start_date", "Ngày bắt đầu" }, { "end_date", "Ngày kết thúc" },
                { "warning_threshold", "Ngưỡng cảnh báo" }, { "category_name", "Tên danh mục" },
                { "category_type", "Loại danh mục" }, { "group_name", "Nhóm danh mục" }
            };

            foreach (var col in headers)
                if (dvgListBudget.Columns.Contains(col.Key)) dvgListBudget.Columns[col.Key].HeaderText = col.Value;

            dvgListBudget.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dvgListBudget.AutoResizeColumns();
        }


        private void OnMessageClicked(string combinedMessage)
        {
            string[] parts = combinedMessage.Split(new[] { " | " }, StringSplitOptions.None);
            string text = parts[0];  // Phần hiển thị
            string message = parts.Length > 1 ? parts[1] : ""; 

            _message = message;

            Home homeForm = Home.GetInstance(_userID);
            if (homeForm != null)
            {
                homeForm.ProcessToastAction($"openDashboard|{text} - {message}");
                this.Close();
            }
        }


        public void InitializeCharts()
        {
            chartBar.ChartAreas.Clear();
            chartBar.ChartAreas.Add("BarChartArea");
            pieChart.ChartAreas.Clear();
            pieChart.ChartAreas.Add("PieChartArea");
            lineChart.ChartAreas.Clear();
            lineChart.ChartAreas.Add("LineChartArea");

            AddChartSeries(chartBar, "CategoryExpenses", SeriesChartType.Column, "BarChartArea");
            AddChartSeries(pieChart, "CategoryExpenses", SeriesChartType.Pie, "PieChartArea");
            AddChartSeries(lineChart, "DailyExpenses", SeriesChartType.Line, "LineChartArea");
        }

        public void AddChartSeries(Chart chart, string seriesName, SeriesChartType chartType, string chartArea)
        {
            if (chart.Series.IndexOf(seriesName) == -1)
            {
                chart.Series.Add(seriesName);
                chart.Series[seriesName].ChartType = chartType;
                chart.Series[seriesName].ChartArea = chartArea;
            }
        }
        public void DrawBarChart()
        {
            DataTable expensesByCategory = _budgetController.GetExpensesByCategory(_userID, _month, _year);

            if (expensesByCategory.Rows.Count == 0)
            {
                return;
            }

            var topExpenses = expensesByCategory.AsEnumerable()
                .OrderByDescending(row => row.Field<decimal>("total_expense"))
                .Take(10)
                .ToList();

            chartBar.Series["CategoryExpenses"].Points.Clear();

            foreach (var row in topExpenses)
            {
                string categoryName = row["category_name"].ToString();
                decimal totalExpense = row.Field<decimal>("total_expense");

                chartBar.Series["CategoryExpenses"].Points.AddXY(categoryName, totalExpense);
            }

            chartBar.Legends.Clear();
            chartBar.Invalidate();
        }

        public void DrawPieChart()
        {
            DataTable expensesByCategory = _budgetController.GetExpensesByCategory(_userID, _month, _year);
            decimal totalBudget = _budgetController.GetTotalBudget(_userID, _month, _year);

            if (expensesByCategory.Rows.Count == 0 || totalBudget == 0)
            {
                return;
            }

            pieChart.Series["CategoryExpenses"].Points.Clear();

            decimal otherTotal = 0;
            List<DataPoint> mainPoints = new List<DataPoint>();

            foreach (DataRow row in expensesByCategory.Rows)
            {
                string categoryName = row["category_name"].ToString();
                decimal totalExpense = Convert.ToDecimal(row["total_expense"]);
                decimal percentage = (totalExpense / totalBudget) * 100;

                if (percentage >= 5)
                {
                    DataPoint point = new DataPoint();
                    point.SetValueXY(categoryName, percentage);
                    point.Label = $"{categoryName}: {percentage:F2}%";
                    mainPoints.Add(point);
                }
                else
                {
                    otherTotal += percentage;
                }
            }

            if (otherTotal > 0)
            {
                DataPoint otherPoint = new DataPoint();
                otherPoint.SetValueXY("Other", otherTotal);
                otherPoint.Label = $"Other: {otherTotal:F2}%";
                mainPoints.Add(otherPoint);
            }

            foreach (var point in mainPoints)
            {
                pieChart.Series["CategoryExpenses"].Points.Add(point);
            }

            pieChart.Legends.Clear();
            pieChart.Series["CategoryExpenses"].ChartType = SeriesChartType.Pie;
            pieChart.Series["CategoryExpenses"]["PieLabelStyle"] = "Outside";
            pieChart.Series["CategoryExpenses"]["PieLineColor"] = "Black";

            pieChart.Invalidate();
        }
        public void DrawLineChart()
        {
            DataTable currentMonthExpenses = _budgetController.GetDailyExpensesByMonth(_userID, _month, _year);
            int prevMonth = (_month == 1) ? 12 : _month - 1;
            int prevYear = (_month == 1) ? _year - 1 : _year;
            DataTable previousMonthExpenses = _budgetController.GetDailyExpensesByMonth(_userID, prevMonth, prevYear);

            if (currentMonthExpenses.Rows.Count == 0 && previousMonthExpenses.Rows.Count == 0)
            {
                return;
            }

            int daysInCurrentMonth = DateTime.DaysInMonth(_year, _month);
            int daysInPreviousMonth = DateTime.DaysInMonth(prevYear, prevMonth);

            lineChart.Series.Clear(); // Xóa các series cũ

            var currentSeries = new Series("CurrentMonth")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2
            };

            var previousSeries = new Series("PreviousMonth")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                Color = Color.Gray
            };

            for (int day = 1; day <= daysInCurrentMonth; day++)
            {
                decimal totalExpense = 0;
                DataRow[] rows = currentMonthExpenses.Select($"day = {day}");
                if (rows.Length > 0)
                {
                    totalExpense = Convert.ToDecimal(rows[0]["total_expense"]);
                }
                currentSeries.Points.AddXY(day, totalExpense);
            }

            for (int day = 1; day <= daysInPreviousMonth; day++)
            {
                decimal totalExpense = 0;
                DataRow[] rows = previousMonthExpenses.Select($"day = {day}");
                if (rows.Length > 0)
                {
                    totalExpense = Convert.ToDecimal(rows[0]["total_expense"]);
                }
                previousSeries.Points.AddXY(day, totalExpense);
            }

            currentSeries.LegendText = $"Tháng {_month}/{_year}";
            previousSeries.LegendText = $"Tháng {prevMonth}/{prevYear}";

            lineChart.Series.Add(currentSeries);
            lineChart.Series.Add(previousSeries);

            lineChart.Invalidate();
        }


        private void btnOpenChat_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnAddBudget_Click(object sender, EventArgs e)
        {
            int budgetId = 0;
            Home homeForm = Home.GetInstance(_userID);
            if (homeForm != null)
            {
                homeForm.ProcessToastAction($"openBudget|{budgetId}");
                this.Close();
            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {

            _message = tbMesage.Text;
            Home homeForm = Home.GetInstance(_userID);
            if (homeForm != null)
            {
                homeForm.ProcessToastAction($"openDashboard|{_message} - {_message}");
                this.Close();
            }
        }

        private void tbMesage_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flMessage_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
