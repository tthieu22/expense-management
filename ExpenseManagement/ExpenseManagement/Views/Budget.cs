using ExpenseManagement.Controller;
using ExpenseManagement.Controllers;
using ExpenseManagement.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Windows.System;

namespace ExpenseManagement.Views
{
    public partial class Budget : Form
    {
        private readonly BudgetMainController _budgetController;
        private readonly CategoryController _categoriesController;
        private readonly ExpensesController _expensesController;
        private readonly IncomesController _incomeController;
        private int _userId;
        private int _selectedMonth;
        private int _categoryId;
        private int _budget_id;
        private int _month;
        private int _year;

        public Budget(int userId, int budgetId)
        {
            InitializeComponent();
            _budgetController = new BudgetMainController();
            _categoriesController = new CategoryController();
            _expensesController = new ExpensesController();
            _incomeController = new IncomesController();
            _userId = userId;

            _month = DateTime.Now.Month;
            _year = DateTime.Now.Year;
            if (budgetId > 0)
            {
                findBudgetId(budgetId);
            }

            LoadBudgetList();

        }
        public void findBudgetId(int budgetId)
        {
            _budget_id = budgetId;
            DataTable data = _budgetController.GetBudgetById(_userId, _budget_id);

            if (data != null && data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                string budgetType = row["budget_type"].ToString();

                if (budgetType.ToLower() == "total")
                {
                    tbAmount.Text = row["amount"].ToString();
                    tbWarning.Text = row["warning_threshold"].ToString();

                    if (DateTime.TryParse(row["start_date"].ToString(), out DateTime startDate))
                    {
                        dateFromTotal.Value = startDate;
                    }

                    if (DateTime.TryParse(row["end_date"].ToString(), out DateTime endDate))
                    {
                        dateTotalTo.Value = endDate;
                    }
                }
                else
                {
                    tbAmount.Text = "";
                    tbWarning.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy ngân sách!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataTotal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var budgetIdCell = dataTotal.Rows[e.RowIndex].Cells["budget_id"].Value;
                if (budgetIdCell == null || budgetIdCell == DBNull.Value)
                {
                    MessageBox.Show("Dòng này không có dữ liệu hợp lệ.");
                    return;
                }

                _budget_id = Convert.ToInt32(budgetIdCell);

                var amountCell = dataTotal.Rows[e.RowIndex].Cells["amount"].Value;
                var warningThresholdCell = dataTotal.Rows[e.RowIndex].Cells["warning_threshold"].Value;

                if (amountCell == null || warningThresholdCell == null ||
                    amountCell == DBNull.Value || warningThresholdCell == DBNull.Value)
                {
                    MessageBox.Show("Dữ liệu trong các ô 'Amount' hoặc 'Warning Threshold' không hợp lệ.");
                    return;
                }

                string amountText = amountCell.ToString();
                string warningText = warningThresholdCell.ToString();

                tbAmount.Text = amountText;
                tbWarning.Text = warningText;

                DateTime startDate = Convert.ToDateTime(dataTotal.Rows[e.RowIndex].Cells["start_date"].Value);
                DateTime endDate = Convert.ToDateTime(dataTotal.Rows[e.RowIndex].Cells["end_date"].Value);
                dateFromTotal.Value = startDate;
                dateTotalTo.Value = endDate;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbAmount.Text) || string.IsNullOrEmpty(tbWarning.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            if (!decimal.TryParse(tbAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Số tiền không hợp lệ.");
                return;
            }

            if (!int.TryParse(tbWarning.Text, out int warningThreshold) || warningThreshold < 0)
            {
                MessageBox.Show("Ngưỡng cảnh báo không hợp lệ.");
                return;
            }

            DateTime startDate = dateFromTotal.Value;
            DateTime endDate = dateTotalTo.Value;
            if (startDate > endDate)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.");
                return;
            }

            bool success = _budgetController.AddTotalBudget(_userId, amount, warningThreshold, startDate, endDate);
            if (success)
            {
                MessageBox.Show("Ngân sách đã được thêm thành công.");
                LoadBudgetList();
                tbAmount.Clear();
                tbWarning.Clear();
                dateFromTotal.Value = DateTime.Now;
                dateTotalTo.Value = DateTime.Now;
            }
            else
            {
                MessageBox.Show("Thêm ngân sách thất bại.");
            }
        }


        public void DisplayTotalChart()
        {
            decimal totalBudget = _budgetController.GetTotalBudget(_userId, _month, _year);
            decimal totalExpenses = _budgetController.GetTotalExpensesByMonth(_userId, _month, _year);
            decimal remaining = totalBudget - totalExpenses;

            chartTotalBudget.Series.Clear();
            chartTotalBudget.ChartAreas.Clear();
            chartTotalBudget.ChartAreas.Add(new ChartArea("MainChartArea"));

            Series series = new Series("Ngân sách tổng");
            series.ChartType = SeriesChartType.Column;

            series.Points.AddXY("Tổng ngân sách", totalBudget);
            series.Points.AddXY("Đã chi tiêu", totalExpenses);
            int remainingIndex = series.Points.AddXY("Còn lại", remaining);

            series.Points[remainingIndex].Color = remaining >= 0 ? System.Drawing.Color.Green : System.Drawing.Color.Red;

            chartTotalBudget.Series.Add(series);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(tbAmount.Text, out decimal amount) ||
                !int.TryParse(tbWarning.Text, out int warningThreshold) ||
                dateFromTotal.Value > dateTotalTo.Value)
            {
                MessageBox.Show("Dữ liệu không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int rowsAffected = _budgetController.UpdateTotalBudget(_userId,_budget_id, amount, warningThreshold, dateFromTotal.Value, dateTotalTo.Value);

            MessageBox.Show(rowsAffected > 0 ? "Cập nhật thành công!" : "Không có dữ liệu nào được cập nhật.",
                            rowsAffected > 0 ? "Thông báo" : "Lỗi",
                            MessageBoxButtons.OK,
                            rowsAffected > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (rowsAffected > 0) LoadBudgetList();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_budget_id == 0 || _userId == 0) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (_budgetController.DeleteTotalBudget(_budget_id, _userId) > 0)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBudgetList();
                }
                else MessageBox.Show("Xóa thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadBudgetList()
        {
            DataTable budgetList = _budgetController.GetTotalBudgets(_userId,_month,_year);
            if (budgetList == null || budgetList.Rows.Count == 0) return;

            dataTotal.Columns.Clear();
            dataTotal.DataSource = budgetList;

            string[] visibleColumns = { "amount", "start_date", "end_date", "warning_threshold" };
            foreach (DataGridViewColumn col in dataTotal.Columns)
                col.Visible = visibleColumns.Contains(col.Name);

            var columnHeaders = new Dictionary<string, string>
            {
                { "amount", "Số tiền" }, { "start_date", "Ngày bắt đầu" },
                { "end_date", "Ngày kết thúc" }, { "warning_threshold", "Ngưỡng cảnh báo" }
            };

            foreach (var col in columnHeaders)
                if (dataTotal.Columns.Contains(col.Key))
                {
                    dataTotal.Columns[col.Key].HeaderText = col.Value;
                    dataTotal.Columns[col.Key].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataTotal.Columns[col.Key].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            dataTotal.AutoResizeColumns();
            dataTotal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataTotal.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataTable budgetListCat = _budgetController.GetCategoryBudgets(_userId, _month, _year);
            if (budgetListCat == null || budgetListCat.Rows.Count == 0) return;

            DrawCategoryBudgetChart(budgetListCat);

            dataCat.Columns.Clear();
            dataCat.DataSource = budgetListCat;

            string[] visibleColumnscat = { "category_name", "amount", "start_date", "end_date", "warning_threshold", "category_id" };
            foreach (DataGridViewColumn col in dataCat.Columns)
                col.Visible = visibleColumnscat.Contains(col.Name);

            var columnHeaderscate = new Dictionary<string, string>
            {
                { "category_name", "Danh mục" }, { "amount", "Số tiền" },
                { "start_date", "Ngày bắt đầu" }, { "end_date", "Ngày kết thúc" },
                { "warning_threshold", "Ngưỡng cảnh báo" }
            };

            foreach (var col in columnHeaderscate)
                if (dataCat.Columns.Contains(col.Key))
                {
                    dataCat.Columns[col.Key].HeaderText = col.Value;
                    dataCat.Columns[col.Key].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataCat.Columns[col.Key].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            // Đưa cột "category_name" lên đầu
            dataCat.Columns["category_name"].DisplayIndex = 0;

            // Ẩn cột "category_id"
            if (dataCat.Columns.Contains("category_id"))
                dataCat.Columns["category_id"].Visible = false;

            dataCat.AutoResizeColumns();
            dataCat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataCat.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        private void DrawCategoryBudgetChart(DataTable budgetListCat)
        {
            if (budgetListCat == null || budgetListCat.Rows.Count == 0) return;

            chartCategoryBudget.Series.Clear();
            chartCategoryBudget.ChartAreas.Clear();

            chartCategoryBudget.ChartAreas.Add(new ChartArea("MainChart"));

            Series series = new Series("Ngân sách theo danh mục")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };

            chartCategoryBudget.Series.Add(series);

            foreach (DataRow row in budgetListCat.Rows)
            {
                string categoryName = row["category_name"].ToString();
                decimal amount = Convert.ToDecimal(row["amount"]);
                series.Points.AddXY(categoryName, amount);
            }

            var chartArea = chartCategoryBudget.ChartAreas["MainChart"];
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.LabelStyle.Angle = -45;
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = true;

            series.Color = Color.CornflowerBlue;
            series.BorderWidth = 2;
            series.BorderColor = Color.Black;
        }

        private void Budget_Load(object sender, EventArgs e)
        {
            LoadBudgetList();
            DisplayTotalChart();
            LoadCategoryIntoComboBox();
        }

        private void btnAddCat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbAmountCat.Text) || string.IsNullOrEmpty(tbWarningCat.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            if (!decimal.TryParse(tbAmountCat.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Số tiền không hợp lệ.");
                return;
            }

            if (!int.TryParse(tbWarningCat.Text, out int warningThreshold) || warningThreshold < 0)
            {
                MessageBox.Show("Ngưỡng cảnh báo không hợp lệ.");
                return;
            }

            DateTime startDate = dateFromCat.Value;
            DateTime endDate = dateToCat.Value;
            if (_categoryId == 0)
            {
                MessageBox.Show("Vui lòng chọn danh mục.");
                return;
            }
            if (startDate > endDate)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.");
                return;
            }

            bool success = _budgetController.AddCategoryBudget(_userId,_categoryId, amount, warningThreshold, startDate, endDate);
            if (success)
            {
                MessageBox.Show("Ngân sách đã được thêm thành công.");
                LoadBudgetList();
                tbAmountCat.Clear();
                tbWarningCat.Clear();
                cbCat.SelectedIndex = 0;
                dateFromCat.Value = DateTime.Now;
                dateToCat.Value = DateTime.Now;
            }
            else
            {
                MessageBox.Show("Thêm ngân sách thất bại.");
            }
        }
        private void LoadCategoryIntoComboBox()
        {
            DataTable categoryGroupsData = _categoriesController.GetCategoriesByType(_userId,"Expense");
            cbCat.Items.Clear();

            var defaultItem = new ComboBoxItem { Text = "Select Category ", Value = 0 };
            cbCat.Items.Add(defaultItem);

            foreach (DataRow row in categoryGroupsData.Rows)
            {
                cbCat.Items.Add(new
                {
                    Text = row["category_name"].ToString(),
                    Value = Convert.ToInt32(row["category_id"])
                });
            }
            cbCat.DisplayMember = "Text";
            cbCat.ValueMember = "Value";

            cbCat.SelectedItem = defaultItem;

        }
        private void btnUpdateCat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbAmountCat.Text) || string.IsNullOrEmpty(tbWarningCat.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            if (!decimal.TryParse(tbAmountCat.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Số tiền không hợp lệ.");
                return;
            }

            if (!int.TryParse(tbWarningCat.Text, out int warningThreshold) || warningThreshold < 0)
            {
                MessageBox.Show("Ngưỡng cảnh báo không hợp lệ.");
                return;
            }

            DateTime startDate = dateFromCat.Value;
            DateTime endDate = dateToCat.Value;
            if (_categoryId == 0)
            {
                MessageBox.Show("Vui lòng chọn danh mục.");
                return;
            }
            if (startDate > endDate)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.");
                return;
            }

            bool success = _budgetController.UpdateCategoryBudget(_budget_id,_userId, _categoryId, amount, warningThreshold, startDate, endDate);
            if (success)
            {
                MessageBox.Show("Ngân sách đã được sửa thành công.");
                LoadBudgetList();
                tbAmountCat.Clear();
                tbWarningCat.Clear();
                cbCat.SelectedIndex = 0;
                dateFromCat.Value = DateTime.Now;
                dateToCat.Value = DateTime.Now;
            }
            else
            {
                MessageBox.Show("Sửa ngân sách thất bại.");
            }
        }

        private void btnDeleteCat_Click(object sender, EventArgs e)
        {
            if (_budget_id == 0 || _userId == 0) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (_budgetController.DeleteCategoryBudget(_budget_id, _userId))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBudgetList();
                }
                else MessageBox.Show("Xóa thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbCat_SelectedIndexChanged(object sender, EventArgs e)
        {

            var selectedItem = cbCat.SelectedItem as dynamic;
            if (selectedItem != null && selectedItem.Value != 0)
            {
                _categoryId = selectedItem.Value;
            }
        }
        private void dataCat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dataCat.Rows[e.RowIndex];

                var budgetIdCell = selectedRow.Cells["budget_id"].Value;
                if (budgetIdCell == null || budgetIdCell == DBNull.Value)
                {
                    MessageBox.Show("Dòng này không có dữ liệu hợp lệ.");
                    return;
                }

                _budget_id = Convert.ToInt32(budgetIdCell);
                tbAmountCat.Text = selectedRow.Cells["amount"].Value?.ToString() ?? "";
                tbWarningCat.Text = selectedRow.Cells["warning_threshold"].Value?.ToString() ?? "";

                dateFromCat.Value = Convert.ToDateTime(selectedRow.Cells["start_date"].Value);
                dateToCat.Value = Convert.ToDateTime(selectedRow.Cells["end_date"].Value);

                var categoryIdCell = selectedRow.Cells["category_id"].Value;
                if (categoryIdCell != null && categoryIdCell != DBNull.Value)
                {
                    int categoryId = Convert.ToInt32(categoryIdCell);
                    foreach (var item in cbCat.Items)
                    {
                        if (item is ComboBoxItem comboBoxItem && Convert.ToInt32(comboBoxItem.Value) == categoryId)
                        {
                            cbCat.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();

        }
    }
}
