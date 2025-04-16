using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ExpenseManagement.Views
{
    public partial class BudgetCategoryItem : UserControl
    {
        public event EventHandler<string> OnCategoryClick;

        private Color originalColor;
        private string budgetId;

        public BudgetCategoryItem(string name, string total, string expense, string remaining, string status, string budgetId, string threshold, string spentPercentage)
        {
            InitializeComponent();
            this.budgetId = budgetId;

            // Gán dữ liệu
            lbName.Text = name;
            lbTotal.Text = $"{Convert.ToDecimal(total):#,0} đ";
            lbExpense.Text = $"-{Convert.ToDecimal(expense):#,0} đ";
            lbRemaining.Text = $"{Convert.ToDecimal(remaining):#,0} đ";

            bool isOverBudget = status == "1";
            lbStatus.Text = isOverBudget ? $"Vượt ngân sách {spentPercentage} %" : $"Đúng ngân sách {spentPercentage} %";
            picWarning.Visible = isOverBudget;

            // Vẽ biểu đồ
            DrawPieChart(Convert.ToDecimal(expense), Convert.ToDecimal(total));

            // Màu nền theo trạng thái
            originalColor = isOverBudget ? Color.FloralWhite : Color.Honeydew;
            this.BackColor = chart.BackColor = originalColor;

            // Gán sự kiện click
            AttachClickEvents();
        }

        private void AttachClickEvents()
        {
            // Danh sách control cần gán sự kiện
            Control[] clickables = {
                this, lbName, lbTotal, lbExpense, lbRemaining,
                lbStatus, picWarning, panel2, chart
            };

            foreach (var ctl in clickables)
                ctl.Click += BudgetCategoryItem_Click;
        }

        private void BudgetCategoryItem_Click(object sender, EventArgs e)
        {
            OnCategoryClick?.Invoke(this, budgetId);

            // Đổi màu nền khi được chọn
            this.BackColor = this.BackColor == originalColor ? Color.AliceBlue : originalColor;
            chart.BackColor = this.BackColor;
        }
        private void DrawPieChart(decimal expense, decimal total)
        {
            chart.Series.Clear();
            chart.Legends.Clear();
            chart.Titles.Clear();

            decimal percent = total > 0 ? expense / total * 100 : 0;
            percent = Math.Min(percent, 100);
            decimal spent = percent;
            decimal remaining = 100 - percent;

            var robotoFont = new Font("Roboto", 8, FontStyle.Bold);

            var series = new Series
            {
                ChartType = SeriesChartType.Doughnut,
                Font = robotoFont,
                CustomProperties = "PieLabelStyle=Outside, PieLineColor=Black",
                IsValueShownAsLabel = true,
                LabelForeColor = Color.Black,
                LabelFormat = "#'%'"
            };

            series.Points.AddXY("", spent);
            series.Points[0].Color = Color.OrangeRed;

            series.Points.AddXY("", remaining);
            series.Points[1].Color = Color.LightGreen;

            chart.Series.Add(series);

            chart.Legends.Clear();
            chart.Titles.Clear();
            chart.BackColor = this.BackColor;
            chart.ChartAreas[0].BackColor = this.BackColor;
        }

    }
}
