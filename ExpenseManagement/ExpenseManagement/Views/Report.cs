using ExpenseManagement.Controllers;
using ExpenseManagement.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Windows.System;

namespace ExpenseManagement.Views
{
    public partial class Report : Form
    {
        private int currentMonth, currentYear, _userId;
        private string _typeChart;

        TransactionController transactionController = new TransactionController();
        public Report(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void Report_Load(object sender, EventArgs e)
        {
            currentMonth = DateTime.Now.Month;
            currentYear = DateTime.Now.Year;
            picExportReport.Click += btnExport_Click;
            textExportReport.Click += btnExport_Click;
            UpdateMonthDisplay();
            LoadTransportReport();
            dateFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dateTo.Value = DateTime.Now;
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            if (currentYear < DateTime.Now.Year || currentMonth < DateTime.Now.Month)
            {
                if (++currentMonth > 12) { currentMonth = 1; currentYear++; }
                UpdateMonthDisplay();
                LoadTransportReport();

            }
        }

        private void btnPrevMonth_Click(object sender, EventArgs e)
        {
            if (--currentMonth < 1) { currentMonth = 12; currentYear--; }
            UpdateMonthDisplay();
            LoadTransportReport();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExcelExporter.ExportToExcel(dataReport);
        }

        public void LoadTransportReport()
        {
            dataReport.Columns.Clear();

            dataReport.Columns.Add("transaction_id", "Transaction ID");
            dataReport.Columns.Add("user_id", "User ID");
            dataReport.Columns.Add("transaction_type", "Transaction Type");
            dataReport.Columns.Add("amount", "Amount");
            dataReport.Columns.Add("date", "Date");
            dataReport.Columns.Add("category_name", "Category");
            dataReport.Columns.Add("category_type", "Category Type");
            dataReport.Columns.Add("category_group", "Category Group");
            dataReport.Columns.Add("group_description", "Group Description");
            dataReport.Columns.Add("category_icon", "Category Icon");
            dataReport.Columns.Add("location", "Location");
            dataReport.Columns.Add("description", "Description");
            dataReport.Columns.Add("created_at", "Created At");
            dataReport.Columns.Add("updated_at", "Updated At");
            dataReport.Columns.Add("tags", "Tags");
            dataReport.Columns.Add("image_path", "Image Path");
            dataReport.Columns.Add("payment_method", "Payment Method");
            dataReport.Columns.Add("recurring", "Recurring");
            dataReport.Columns.Add("end_date", "End Date");

            dataReport.Rows.Clear();

            var transactions = transactionController.GetTransactionsByMonth(currentYear, currentMonth, _userId);

            foreach (var transaction in transactions)
            {
                string transactionId = GetSafeString(transaction, "transaction_id");
                string userId = GetSafeString(transaction, "user_id");
                string transactionType = GetSafeString(transaction, "transaction_type");
                string amount = GetSafeString(transaction, "amount");
                string date = GetSafeDate(transaction, "date");

                string categoryName = GetSafeString(transaction, "category_name");
                string categoryType = GetSafeString(transaction, "category_type");
                string categoryIcon = GetSafeString(transaction, "category_icon_url");

                string categoryGroup = GetSafeString(transaction, "group_name");
                string groupDescription = GetSafeString(transaction, "description");

                string expenseLocation = GetSafeString(transaction, "location");
                string description = GetSafeString(transaction, "description");
                string createdAt = GetSafeString(transaction, "created_at");
                string updatedAt = GetSafeString(transaction, "updated_at");
                string tags = GetSafeString(transaction, "tags");
                string imgPath = GetSafeString(transaction, "image_path");
                string paymentMethod = GetSafeString(transaction, "payment_method");
                string recurring = GetSafeString(transaction, "recurring");
                string endDate = GetSafeDate(transaction, "end_date");

                dataReport.Rows.Add(
                    transactionId, userId, transactionType, amount, date,
                    categoryName, categoryType, categoryGroup, groupDescription, categoryIcon,
                    expenseLocation, description, createdAt, updatedAt, tags, imgPath, paymentMethod, recurring, endDate
                );
            }
            // Ẩn các cột không cần hiển thị
            dataReport.Columns["transaction_id"].Visible = false;
            dataReport.Columns["user_id"].Visible = false;
            dataReport.Columns["category_icon"].Visible = false;
            dataReport.Columns["updated_at"].Visible = false;

            RenderExpenseCharts();
        }


        private void RenderExpenseCharts()
        {
            var parentCategoryExpenses = new Dictionary<string, decimal>();
            var childCategoryExpenses = new Dictionary<string, decimal>();
            var parentCategoryIncome = new Dictionary<string, decimal>();
            var childCategoryIncome = new Dictionary<string, decimal>();

            bool hasExpense = false;
            bool hasIncome = false;

            foreach (DataGridViewRow row in dataReport.Rows)
            {
                if (row.IsNewRow) continue;

                string transactionType = row.Cells["transaction_type"].Value?.ToString();
                string parentCategory = row.Cells["category_group"].Value?.ToString() ?? "Unknown";
                string subCategory = row.Cells["category_name"].Value?.ToString() ?? "Unknown";
                decimal amount = decimal.TryParse(row.Cells["amount"].Value?.ToString(), out var parsedAmount) ? parsedAmount : 0;

                if (transactionType == "Expense")
                {
                    hasExpense = true;
                    if (!parentCategoryExpenses.ContainsKey(parentCategory))
                        parentCategoryExpenses[parentCategory] = 0;
                    parentCategoryExpenses[parentCategory] += amount;

                    if (!childCategoryExpenses.ContainsKey(subCategory))
                        childCategoryExpenses[subCategory] = 0;
                    childCategoryExpenses[subCategory] += amount;
                }
                else if (transactionType == "Income")
                {
                    hasIncome = true;
                    if (!parentCategoryIncome.ContainsKey(parentCategory))
                        parentCategoryIncome[parentCategory] = 0;
                    parentCategoryIncome[parentCategory] += amount;

                    if (!childCategoryIncome.ContainsKey(subCategory))
                        childCategoryIncome[subCategory] = 0;
                    childCategoryIncome[subCategory] += amount;
                }
            }
            if (hasExpense && hasIncome)
            {
                PopulateLineChart(chartDisplayParent, parentCategoryExpenses, parentCategoryIncome, "Income vs Expenses by Category Group");
                PopulateLineChart(chartDisplayChildren, childCategoryExpenses, childCategoryIncome, "Income vs Expenses by Category");
            }

            else if (hasExpense)
            {
                PopulatePieChart(chartDisplayParent, parentCategoryExpenses, "Total Expenses by Category Group");
                PopulatePieChart(chartDisplayChildren, childCategoryExpenses, "Total Expenses by Category");
            }
            else if (hasIncome)
            {
                PopulatePieChart(chartDisplayParent, parentCategoryIncome, "Total Income by Category Group");
                PopulatePieChart(chartDisplayChildren, childCategoryIncome, "Total Income by Category");
            }

        }
        private void PopulateLineChart(Chart chart, Dictionary<string, decimal> expenses, Dictionary<string, decimal> incomes, string title)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.ChartAreas.Clear();
            chart.Legends.Clear();

            chart.ChartAreas.Add(new ChartArea());
            chart.Titles.Add(title);

            Legend legend = new Legend
            {
                Docking = Docking.Right,
                Alignment = StringAlignment.Center
            };
            chart.Legends.Add(legend);

            // Series cho chi tiêu (Expense)
            Series expenseSeries = new Series
            {
                ChartType = SeriesChartType.Line, // Biểu đồ đường
                IsValueShownAsLabel = false, // Ẩn nhãn trên điểm dữ liệu
                Name = "Expenses",
                Color = Color.Red,
                BorderWidth = 2,
                MarkerStyle = MarkerStyle.Circle
            };

            // Series cho thu nhập (Income)
            Series incomeSeries = new Series
            {
                ChartType = SeriesChartType.Line,
                IsValueShownAsLabel = false, // Ẩn nhãn trên điểm dữ liệu
                Name = "Income",
                Color = Color.Green,
                BorderWidth = 2,
                MarkerStyle = MarkerStyle.Circle
            };

            // Hợp nhất tất cả danh mục
            var allCategories = expenses.Keys.Union(incomes.Keys).Distinct();

            foreach (var category in allCategories)
            {
                decimal expenseAmount = expenses.ContainsKey(category) ? expenses[category] : 0;
                decimal incomeAmount = incomes.ContainsKey(category) ? incomes[category] : 0;

                expenseSeries.Points.AddXY(category, expenseAmount);
                incomeSeries.Points.AddXY(category, incomeAmount);
            }

            chart.Series.Add(expenseSeries);
            chart.Series.Add(incomeSeries);
        }

        private void PopulatePieChart(Chart chart, Dictionary<string, decimal> data, string title)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.ChartAreas.Clear();
            chart.Legends.Clear();

            chart.ChartAreas.Add(new ChartArea());

            // Thêm tiêu đề
            chart.Titles.Add(title);

            Legend legend = new Legend
            {
                Docking = Docking.Right,
                Alignment = StringAlignment.Center
            };
            chart.Legends.Add(legend);

            Series series = new Series
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true,
                LegendText = "#VALX (#PERCENT{P0})",
                Label = "#PERCENT{P0}"
            };

            foreach (var item in data.OrderByDescending(x => x.Value))
            {
                series.Points.AddXY(item.Key, item.Value);
            }

            chart.Series.Add(series);
        }

        private string GetSafeString(Dictionary<string, object> dict, string key)
        {
            return dict.ContainsKey(key) && dict[key] != DBNull.Value ? dict[key].ToString() : "";
        }

        private string GetSafeDate(Dictionary<string, object> dict, string key)
        {
            if (dict.ContainsKey(key) && dict[key] != DBNull.Value)
            {
                DateTime date;
                if (DateTime.TryParse(dict[key].ToString(), out date))
                {
                    return date.ToString("yyyy-MM-dd");
                }
            }
            return "";
        }


        private void chartDisplay_Click(object sender, EventArgs e)
        {

        }

        private void dateFrom_ValueChanged(object sender, EventArgs e)
        {
            FilterTransactions();
        }

        private void dateTo_ValueChanged(object sender, EventArgs e)
        {
            FilterTransactions();

        }

        private void ckExpense_CheckedChanged(object sender, EventArgs e)
        {
            FilterTransactions();

        }

        private void ckIncome_CheckedChanged(object sender, EventArgs e)
        {
            FilterTransactions();

        }

        private void radioMonth_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioQuatar_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioYear_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tbFromAmount_TextChanged(object sender, EventArgs e)
        {
            FilterTransactions();
        }

        private void chartDisplayParent_Click(object sender, EventArgs e)
        {

        }

        private void chartDisplayChildren_Click(object sender, EventArgs e)
        {

        }

        private void tbToAmount_TextChanged(object sender, EventArgs e)
        {
            FilterTransactions();
        }

        private void raLine_CheckedChanged(object sender, EventArgs e)
        {
            _typeChart = "Line";
            FilterTransactionsLine();
        }

        private void raColumn_CheckedChanged(object sender, EventArgs e)
        {
            _typeChart = "Column";
            FilterTransactions();

        }
        private void FilterTransactionsLine()
        {
            var transactions = transactionController.GetTransactionsByMonth(currentYear, currentMonth, _userId);

            // Lọc theo khoảng thời gian từ `dateFrom` đến `dateTo`
            DateTime fromDate = dateFrom.Value;
            DateTime toDate = dateTo.Value;

            transactions = transactions
                .Where(t =>
                {
                    DateTime transactionDate = DateTime.Parse(t["date"].ToString());
                    return transactionDate >= fromDate && transactionDate <= toDate;
                }).ToList();

            // Lọc theo loại giao dịch
            if (ckIncome.Checked && !ckExpense.Checked)
            {
                transactions = transactions.Where(t => t["transaction_type"].ToString() == "Income").ToList();
            }
            else if (!ckIncome.Checked && ckExpense.Checked)
            {
                transactions = transactions.Where(t => t["transaction_type"].ToString() == "Expense").ToList();
            }

            // Lọc theo khoảng số tiền
            decimal fromAmount, toAmount;
            bool isFromAmountValid = decimal.TryParse(tbFromAmount.Text, out fromAmount);
            bool isToAmountValid = decimal.TryParse(tbToAmount.Text, out toAmount);

            if (isFromAmountValid || isToAmountValid)
            {
                transactions = transactions
                    .Where(t =>
                    {
                        decimal amount = decimal.Parse(t["amount"].ToString());
                        bool isValid = true;
                        if (isFromAmountValid) isValid &= amount >= fromAmount;
                        if (isToAmountValid) isValid &= amount <= toAmount;
                        return isValid;
                    })
                    .ToList();
            }

            // Tạo danh sách giao dịch theo ngày
            Dictionary<int, decimal> dailyExpenses = new Dictionary<int, decimal>();
            Dictionary<int, decimal> dailyIncome = new Dictionary<int, decimal>();

            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            for (int day = 1; day <= daysInMonth; day++)
            {
                dailyExpenses[day] = 0;
                dailyIncome[day] = 0;
            }

            foreach (var transaction in transactions)
            {
                DateTime transactionDate = DateTime.Parse(transaction["date"].ToString());
                int day = transactionDate.Day;
                decimal amount = decimal.Parse(transaction["amount"].ToString());

                if (transaction["transaction_type"].ToString() == "Expense")
                {
                    dailyExpenses[day] += amount;
                }
                else if (transaction["transaction_type"].ToString() == "Income")
                {
                    dailyIncome[day] += amount;
                }
            }

            // Vẽ biểu đồ đường xu hướng theo ngày
            PopulateTrendLineChart(chartDisplayChildren, dailyExpenses, dailyIncome, "Income vs Expenses Trend (Daily)");
        }
        private void PopulateTrendLineChart(Chart chart, Dictionary<int, decimal> expenses, Dictionary<int, decimal> incomes, string title)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.ChartAreas.Clear();
            chart.Legends.Clear();

            // Tạo khu vực hiển thị biểu đồ
            ChartArea chartArea = new ChartArea
            {
                AxisX = { Title = "Day of Month", Interval = 1, MajorGrid = { LineColor = Color.LightGray } },
                AxisY = { Title = "Amount ($)", MajorGrid = { LineColor = Color.LightGray } }
            };
            chart.ChartAreas.Add(chartArea);

            chart.Titles.Add(title);

            // Thêm chú thích (Legend)
            Legend legend = new Legend
            {
                Docking = Docking.Top,
                Alignment = StringAlignment.Center
            };
            chart.Legends.Add(legend);

            // Series cho chi tiêu (Expenses)
            Series expenseSeries = new Series
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                IsValueShownAsLabel = true,
                Name = "Expenses",
                Color = Color.Red,
                MarkerStyle = MarkerStyle.Circle
            };

            // Series cho thu nhập (Income)
            Series incomeSeries = new Series
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                IsValueShownAsLabel = true,
                Name = "Income",
                Color = Color.Green,
                MarkerStyle = MarkerStyle.Square
            };

            // Thêm dữ liệu từ ngày 1 -> cuối tháng
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            for (int day = 1; day <= daysInMonth; day++)
            {
                expenseSeries.Points.AddXY(day, expenses.ContainsKey(day) ? expenses[day] : 0);
                incomeSeries.Points.AddXY(day, incomes.ContainsKey(day) ? incomes[day] : 0);
            }

            chart.Series.Add(expenseSeries);
            chart.Series.Add(incomeSeries);
        }

        private void btnExport_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picClose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();

        }

        private void picExportReport_Click(object sender, EventArgs e)
        {

        }

        private void FilterTransactions()
        {
            // Lấy dữ liệu gốc từ controller
            var transactions = transactionController.GetTransactionsByMonth(currentYear, currentMonth, _userId);

            // Lọc theo khoảng thời gian
            DateTime fromDate = dateFrom.Value;
            DateTime toDate = dateTo.Value;

            transactions = transactions
                .Where(t =>
                {
                    DateTime transactionDate = DateTime.Parse(t["date"].ToString());
                    return transactionDate >= fromDate && transactionDate <= toDate;
                }).ToList();

            // Lọc theo loại giao dịch (Thu nhập, Chi tiêu)
            if (ckIncome.Checked && !ckExpense.Checked)
            {
                transactions = transactions.Where(t => t["transaction_type"].ToString() == "Income").ToList();
            }
            else if (!ckIncome.Checked && ckExpense.Checked)
            {
                transactions = transactions.Where(t => t["transaction_type"].ToString() == "Expense").ToList();
            }

            // Lọc theo khoảng số tiền
            decimal fromAmount, toAmount;
            bool isFromAmountValid = decimal.TryParse(tbFromAmount.Text, out fromAmount);
            bool isToAmountValid = decimal.TryParse(tbToAmount.Text, out toAmount);

            if (isFromAmountValid || isToAmountValid)
            {
                transactions = transactions
                    .Where(t =>
                    {
                        decimal amount = decimal.Parse(t["amount"].ToString());
                        bool isValid = true;

                        if (isFromAmountValid) isValid &= amount >= fromAmount;
                        if (isToAmountValid) isValid &= amount <= toAmount;

                        return isValid;
                    })
                    .ToList();
            }

            // Xóa dữ liệu cũ và cập nhật lại bảng `dataReport`
            dataReport.Rows.Clear();

            foreach (var transaction in transactions)
            {
                string transactionId = GetSafeString(transaction, "transaction_id");
                string userId = GetSafeString(transaction, "user_id");
                string transactionType = GetSafeString(transaction, "transaction_type");
                string amount = GetSafeString(transaction, "amount");
                string date = GetSafeDate(transaction, "date");

                string categoryName = GetSafeString(transaction, "category_name");
                string categoryType = GetSafeString(transaction, "category_type");
                string categoryIcon = GetSafeString(transaction, "category_icon_url");

                string categoryGroup = GetSafeString(transaction, "group_name");
                string groupDescription = GetSafeString(transaction, "description");

                string expenseLocation = GetSafeString(transaction, "location");
                string description = GetSafeString(transaction, "description");
                string createdAt = GetSafeString(transaction, "created_at");
                string updatedAt = GetSafeString(transaction, "updated_at");
                string tags = GetSafeString(transaction, "tags");
                string imgPath = GetSafeString(transaction, "image_path");
                string paymentMethod = GetSafeString(transaction, "payment_method");
                string recurring = GetSafeString(transaction, "recurring");
                string endDate = GetSafeDate(transaction, "end_date");

                dataReport.Rows.Add(
                    transactionId, userId, transactionType, amount, date,
                    categoryName, categoryType, categoryGroup, groupDescription, categoryIcon,
                    expenseLocation, description, createdAt, updatedAt, tags, imgPath, paymentMethod, recurring, endDate
                );
            }
            // Ẩn các cột không cần hiển thị
            dataReport.Columns["transaction_id"].Visible = false;
            dataReport.Columns["user_id"].Visible = false;
            dataReport.Columns["category_icon"].Visible = false;
            dataReport.Columns["updated_at"].Visible = false;

            RenderExpenseCharts();
        }

        private void UpdateMonthDisplay()
        {
            DateTime selectedDate = new DateTime(currentYear, currentMonth, 1);
            monthNow.Text = $"{currentMonth}/{currentYear}";
            monthNext.Text = $"{selectedDate.AddMonths(1):M/yyyy}";
            monthPrev.Text = $"{selectedDate.AddMonths(-1):M/yyyy}";
            monthNow.ForeColor = Color.Red;
            monthNext.ForeColor = Color.Black;
            monthPrev.ForeColor = Color.Black;
        }
    }
}
