using ExpenseManagement.Controller;
using ExpenseManagement.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseManagement.Views
{
    public partial class Transaction : Form
    {

        TransactionController transactionController = new TransactionController();
        ExpensesController _expenseController = new ExpensesController();
        IncomesController _incomeController = new IncomesController();
        private Cons cons;
        private DateTime? selectedDate = null;
        private readonly int _userId;
        private int _idExpense;
        private int id_trans = 0; 
        #region Properties

        private List<List<Panel>> matrix;
        public List<List<Panel>> Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }

        public List<string> DateOfWeek { get => dateOfWeek; set => dateOfWeek = value; }

        private List<string> dateOfWeek = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        #endregion

        public Transaction(int userId)
        {
            _userId = userId;
            InitializeComponent();
            LoadMatrix();
        }

        void LoadMatrix()
        {
            Matrix = new List<List<Panel>>();
            Panel pnlOld = new Panel() { Width = 0, Height = 0, Location = new Point(-Cons.margin, 0) };

            for (int i = 0; i < Cons.DayOfColumn; i++)
            {
                Matrix.Add(new List<Panel>());
                for (int j = 0; j < Cons.DayOfWeek; j++)
                {
                    Panel pnl = CreateDayPanel(pnlOld);

                    pnl.MouseEnter += (sender, e) => pnl.Cursor = Cursors.Hand;
                    pnl.MouseLeave += (sender, e) => pnl.Cursor = Cursors.Default;
                    pnl.Click += (sender, e) => OnPanelClick(sender as Panel, pnl);

                    pnlMatrix.Controls.Add(pnl);
                    Matrix[i].Add(pnl);

                    pnlOld = pnl;
                }

                pnlOld = new Panel() { Width = 0, Height = 0, Location = new Point(-Cons.margin, pnlOld.Location.Y + 120 + Cons.margin) };
            }

            SetDefaultDate();
        }

        private Panel CreateDayPanel(Panel pnlOld)
        {
            var pnl = new Panel()
            {
                Width = 120,
                Height = 120,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(pnlOld.Location.X + pnlOld.Width + Cons.margin, pnlOld.Location.Y),
                BackColor = Color.White
            };

            Label lblDay = new Label()
            {
                AutoSize = false,
                Size = new Size(30, 20),
                Location = new Point(5, 5),
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Arial", 8, FontStyle.Bold),
                ForeColor = Color.Black,
            };

            Label lblIncome = new Label()
            {
                AutoSize = false,
                Size = new Size(pnl.Width - 10, 15),
                Location = new Point(5, pnl.Height / 2 - 8),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 7, FontStyle.Regular),
                ForeColor = Color.Green,
                Text = "1.00M",
            };

            Label lblExpense = new Label()
            {
                AutoSize = false,
                Size = new Size(pnl.Width - 10, 15),
                Location = new Point(5, pnl.Height - 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 7, FontStyle.Regular),
                ForeColor = Color.Red,
                Text = "100.00K",
            };

            pnl.Controls.Add(lblDay);
            pnl.Controls.Add(lblIncome);
            pnl.Controls.Add(lblExpense);

            return pnl;
        }

        private void OnPanelClick(Panel clickedPanel, Panel pnl)
        {
            Label lblDay = pnl.Controls.OfType<Label>().FirstOrDefault(l => l.Location == new Point(5, 5));
            if (lblDay != null && int.TryParse(lblDay.Text, out int day))
            {
                DateTime selectedDateTime = new DateTime(dtpkDate.Value.Year, dtpkDate.Value.Month, day);
                dtpkDate.Value = selectedDateTime;

                HighlightSelectedPanel(pnl, selectedDateTime);
            }
        }
        void HighlightSelectedPanel(Panel selectedPanel, DateTime selectedDateTime)
        {
            DateTime today = DateTime.Now.Date;
            DateTime currentMonth = dtpkDate.Value;
             
            foreach (var row in Matrix)
            {
                foreach (var pnl in row)
                {
                    Label lblDay = pnl.Controls.OfType<Label>().FirstOrDefault(l => l.Location == new Point(5, 5));

                    if (lblDay != null && int.TryParse(lblDay.Text, out int day))
                    {
                        DateTime panelDate = new DateTime(currentMonth.Year, currentMonth.Month, day);

                        if (panelDate == today && panelDate.Month == currentMonth.Month && panelDate.Year == currentMonth.Year)
                        {
                            pnl.BackColor = (pnl != selectedPanel) ? Color.LightGreen : Color.LightBlue;
                        }
                        else if (panelDate == selectedDateTime.Date)
                        {
                            pnl.BackColor = Color.LightBlue;
                        }
                        else
                        {
                            pnl.BackColor = Color.White;
                        }
                    }
                }
            }
        }

        private void LoadTransactionsForDate(DateTime date, int userId)
        {
            var transactionByDate = transactionController.GetTransactionsByDate(date, userId);
            decimal totalIncome = 0;
            decimal totalExpense = 0;

            plTransactionList.Controls.Clear();

            if (transactionByDate.Count == 0)
            {
                Label lblNoData = new Label()
                {
                    Text = "No data available",
                    Font = new Font("Poppin", 10, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Location = new Point(5, 5)
                };

                plTransactionList.Controls.Add(lblNoData);
            }
            else
            {
                foreach (var transaction in transactionByDate)
                {
                    string transactionType = transaction.ContainsKey("transaction_type") ? transaction["transaction_type"].ToString() : "Unknown";
                    string category = transaction.ContainsKey("category_name") ? transaction["category_name"].ToString() : "No Category";
                    decimal amount = transaction.ContainsKey("amount") ? Convert.ToDecimal(transaction["amount"]) : 0;
                    string icon = transaction.ContainsKey("category_icon_char") ? transaction["category_icon_char"].ToString() : "";
                    string imageUrl = transaction.ContainsKey("category_icon_url") ? transaction["category_icon_url"].ToString() : "";
                    string description = transaction.ContainsKey("description") ? transaction["description"].ToString() : "";
                    string dateString = transaction.ContainsKey("date") ? transaction["date"].ToString() : "";

                    if (transaction.ContainsKey("expense_id") && transaction["expense_id"] != DBNull.Value)
                    {
                        id_trans = Convert.ToInt32(transaction["expense_id"]);
                    }
                    else if (transaction.ContainsKey("income_id") && transaction["income_id"] != DBNull.Value)
                    {
                        id_trans = Convert.ToInt32(transaction["income_id"]);
                    }

                    if (transactionType == "Income")
                        totalIncome += amount;
                    else
                        totalExpense += amount;

                    TransactionItem transactionItem = new TransactionItem(id_trans, category, amount.ToString() + "đ", transactionType, description, dateString, icon, imageUrl);

                    transactionItem.OnActionClick += HandleTransactionAction;


                    plTransactionList.Controls.Add(transactionItem);
                }
            }

            decimal balance = totalIncome - totalExpense;

            lblTotalIncome.Text = $"{totalIncome:N2} VND";
            lblTotalExpense.Text = $"{totalExpense:N2} VND";
            lblBalance.Text = $"{balance:N2} VND";
        }
        private void HandleTransactionAction(int transactionId, string action, string type)
        {
            if (action == "edit")
            {
                if (type == "expense")
                {
                    Home.GetInstance(_userId)?.ProcessToastAction($"openExpense|{transactionId}");
                }
                else if (type == "income")
                {
                    Home.GetInstance(_userId)?.ProcessToastAction($"openIncome|{transactionId}");
                }
            }
            else if (action == "delete")
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa giao dịch này?", "Xác nhận xóa",
                                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Xóa dữ liệu khỏi database
                    if (type == "expense")
                    {
                        _expenseController.DeleteExpense(transactionId, _userId);
                    }
                    else if (type == "income")
                    {
                        _incomeController.DeleteIncome(transactionId, _userId);
                    }

                    // Xóa item khỏi giao diện
                    Control itemToRemove = null;
                    foreach (Control control in plTransactionList.Controls)
                    {
                        if (control is TransactionItem transactionItem && transactionItem.TransactionId == transactionId)
                        {
                            itemToRemove = control;
                            break;
                        }
                    }

                    if (itemToRemove != null)
                    {
                        plTransactionList.Controls.Remove(itemToRemove);
                        itemToRemove.Dispose();
                    }
                }
            }
        }



        int DayOfMonth(DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month);
        }

        void AddNumberIntoMatrix(DateTime date, int userId)
        {
            ClearMatrix();
            DateTime useDate = new DateTime(date.Year, date.Month, 1);
            DateTime today = DateTime.Now.Date;

            var transactionsByDay = transactionController.GetTotalByDayGroupedByType(date.Year, date.Month, _userId)
                .ToDictionary(t => Convert.ToDateTime(t["Date"]), t => t);

            int line = 0;

            for (int i = 1; i <= DayOfMonth(date); i++)
            {
                int column = DateOfWeek.IndexOf(useDate.DayOfWeek.ToString());
                Panel pnl = Matrix[line][column];

                Label lblDay = pnl.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Location == new Point(5, 5));
                Label lblIncome = pnl.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.ForeColor == Color.Green);
                Label lblExpense = pnl.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.ForeColor == Color.Red);

                if (lblDay != null)
                {
                    lblDay.Text = i.ToString();
                    lblDay.Visible = true;

                    DateTime currentDate = new DateTime(date.Year, date.Month, i);
                    pnl.BackColor = Color.White;

                    if (currentDate == today)
                    {
                        pnl.BackColor = Color.LightGreen;
                    }

                    if (lblIncome != null && lblExpense != null)
                    {
                        lblIncome.Visible = false;
                        lblExpense.Visible = false;

                        if (transactionsByDay.ContainsKey(currentDate))
                        {
                            var transactionData = transactionsByDay[currentDate];

                            decimal totalIncome = Convert.ToDecimal(transactionData["Income"]);
                            decimal totalExpense = Convert.ToDecimal(transactionData["Expense"]);

                            if (totalIncome > 0)
                            {
                                lblIncome.Text = totalIncome.ToString("N2");
                                lblIncome.Visible = true;
                            }

                            if (totalExpense > 0)
                            {
                                lblExpense.Text = totalExpense.ToString("N2");
                                lblExpense.Visible = true;
                            }
                        }
                    }
                }

                if (column == DateOfWeek.Count - 1)
                    line++;

                useDate = useDate.AddDays(1);
            }
        }


        private void ClearMatrix()
        {
            foreach (var row in Matrix)
            {
                foreach (var pnl in row)
                {
                    Label lblDay = pnl.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Location == new Point(5, 5));
                    Label lblIncome = pnl.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.ForeColor == Color.Green);
                    Label lblExpense = pnl.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.ForeColor == Color.Red);

                    if (lblDay != null)
                    {
                        lblDay.Text = "";
                        lblDay.Visible = false;
                    }

                    if (lblIncome != null)
                    {
                        lblIncome.Text = "1.00M";
                        lblIncome.Visible = false;
                    }

                    if (lblExpense != null)
                    {
                        lblExpense.Text = "500.00K";
                        lblExpense.Visible = false;
                    }

                    pnl.BackColor = Color.White;
                }
            }
        }

        void SetDefaultDate()
        {
            dtpkDate.Value = DateTime.Now;
        }



        private void LoadTransactionsForMonth(DateTime date, int userId)
        {
            int year = date.Year;
            int month = date.Month;

            var totalByMonth = transactionController.GetTotalByMonthGroupedByType(year, month, userId);

            decimal totalIncome = totalByMonth["Income"];
            decimal totalExpense = totalByMonth["Expense"];
            decimal balance = totalIncome - totalExpense;

            lblTotalIncome.Text = $"{totalIncome:N2} VND";
            lblTotalExpense.Text = $"{totalExpense:N2} VND";
            lblBalance.Text = $"{balance:N2} VND";

            plTransactionList.Controls.Clear();
            plTransactionList.AutoScroll = true;

            var listTransaction = transactionController.GetTransactionsByMonth(year, month, userId);

            if (listTransaction.Count == 0)
            {
                Label lblNoData = new Label()
                {
                    Text = "No data available",
                    Font = new Font("Arial", 10, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Location = new Point(10, 10)
                };

                plTransactionList.Controls.Add(lblNoData);
            }
            else
            {
                int topPosition = 10;

                foreach (var transaction in listTransaction)
                {
                    string transactionType = transaction.ContainsKey("transaction_type") ? transaction["transaction_type"].ToString() : "Err";
                    string category = transaction.ContainsKey("category_name") ? transaction["category_name"].ToString() : "No";
                    decimal amount = transaction.ContainsKey("amount") ? Convert.ToDecimal(transaction["amount"]) : 0;
                    string icon = transaction.ContainsKey("category_icon_char") ? transaction["category_icon_char"].ToString() : "";
                    string imageUrl = transaction.ContainsKey("category_icon_url") ? transaction["category_icon_url"].ToString() : "";
                    string description = transaction.ContainsKey("description") ? transaction["description"].ToString() : "";
                    string dateString = transaction.ContainsKey("date") ? transaction["date"].ToString() : "";

                    if (transaction.ContainsKey("expense_id") && transaction["expense_id"] != DBNull.Value)
                    {
                        id_trans = Convert.ToInt32(transaction["expense_id"]);
                    }
                    else if (transaction.ContainsKey("income_id") && transaction["income_id"] != DBNull.Value)
                    {
                        id_trans = Convert.ToInt32(transaction["income_id"]);
                    }

                    TransactionItem transactionItem = new TransactionItem(id_trans, category, amount.ToString("N2") + " VND", transactionType, description, dateString, icon, imageUrl);

                    transactionItem.Location = new Point(5, topPosition);
                    transactionItem.Width = plTransactionList.Width - 20;
                    topPosition += transactionItem.Height + 5;

                    transactionItem.OnActionClick += HandleTransactionAction;


                    plTransactionList.Controls.Add(transactionItem);
                }
            }
        }


        private void dtpkDate_ValueChanged(object sender, EventArgs e)
        {
            LoadTransactionsForDate(dtpkDate.Value, _userId);
            AddNumberIntoMatrix(dtpkDate.Value, _userId);
        }


        private void Transaction_Load(object sender, EventArgs e)
        {
            AddNumberIntoMatrix(dtpkDate.Value, _userId);
            LoadTransactionsForDate(dtpkDate.Value, _userId);
            LoadTransactionsForMonth(dtpkDate.Value, _userId);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            dtpkDate.Value = dtpkDate.Value.AddMonths(1);
            LoadTransactionsForMonth(dtpkDate.Value, _userId);
            AddNumberIntoMatrix(dtpkDate.Value, _userId);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            dtpkDate.Value = dtpkDate.Value.AddMonths(-1);

            AddNumberIntoMatrix(dtpkDate.Value, _userId);
            LoadTransactionsForMonth(dtpkDate.Value, _userId);
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpkDate.Value = DateTime.Now;
        }
    }
}
