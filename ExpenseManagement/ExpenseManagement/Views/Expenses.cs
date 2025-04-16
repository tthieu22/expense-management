using System;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Collections.Generic;
using ExpenseManagement.Core;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;
using ExpenseManagement.Controller;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using ExpenseManagement.Controllers;
using FontAwesome.Sharp;
using System.Runtime.Remoting.Messaging;
using System.Linq;
using System.Transactions;
using System.Diagnostics;

namespace ExpenseManagement.Views

{
    public partial class Expenses : Form
    {
        // Init
        private int txtAmount;
        private DateTime txtExpenseDate;
        private string txtDescription = null;
        private string txtPaymentMethod = null;
        private int txtRecurring = 0;
        private string txtLocation = null;
        private string txtImagePath = null;
        private string txtTags = null;
        private int txtCategory = 0; 
        private DateTime ? txtEndDate = null ;
        private string typeExpense = "Expense";
        private int _transactionID;
        private string _publicClouldId;


        // Map
        private string googleApiKey = "AIzaSyARKAOJSHIaLQHiX6qxg_LZaeisMlNLOck";
        private string filePath;
        private MapHandler mapHandler;

        // Time
        private bool selectToday = false;

        // Camera
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private Bitmap capturedImage;

        // Category
        //private readonly CategoriesController _categoriesController;
        TransactionController transactionController = new TransactionController();
        private readonly ExpensesController _expensesController;
        private readonly IncomesController _incomeController;
        private int _userId;
        private int _selectedCategoryItem;
        private Panel _currentSelectedPanel = null;
        private CategoryController _categoryController;
        private MessageManager messageManager;


        CloudinaryHelper _cloudinaryHelper;
        // Amount
        private bool isTyping = false;
        private readonly string placeholderText = "-0.000.000.000";
        private string _message = string.Empty;

        // ID load
        private int _expenseId;
        private int _incomeId;
        public Expenses(int userId,int expenseId, int incomeId)
        {
            _userId = userId;
            _expenseId = expenseId;
            _incomeId = incomeId;
            _expensesController = new ExpensesController();
            _incomeController = new IncomesController();
            _categoryController = new CategoryController();
            _cloudinaryHelper = new CloudinaryHelper();
            InitializeComponent();
            this.Shown += Form_Shown;
        }
        private void Form_Shown(object sender, EventArgs e)
        {
            if (_expenseId > 0)
            {
                _transactionID = _expenseId;
                DataTable dataExpense = _expensesController.GetExpenseByIdCreate(_userId, _expenseId);
                if (dataExpense.Rows.Count > 0)
                {
                    DataRow row = dataExpense.Rows[0];

                    string amount = row["amount"].ToString();
                    string description = row["description"].ToString();
                    string tags = row["tags"].ToString();
                    string paymentMethod = row["payment_method"].ToString();
                    int recurringValue = Convert.ToInt32(row["recurring"]);
                    string location = row["location"].ToString();
                    DateTime incomeDate = Convert.ToDateTime(row["expense_date"]);
                    string imagePath = row["image_path"].ToString();
                    typeExpense = "Expense";
                    PopulateTransactionData(amount, description, tags, paymentMethod, recurringValue, location, incomeDate, imagePath);
                    UpdateLabelColors();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (_incomeId > 0)
            {
                _transactionID = _incomeId;

                DataTable dataIncome = _incomeController.GetIncomeByIdCreate(_userId, _incomeId);

                if (dataIncome.Rows.Count > 0)
                {
                    DataRow row = dataIncome.Rows[0];

                    string amount = row["amount"].ToString();
                    string description = row["description"].ToString();
                    string tags = row["tags"].ToString();
                    string paymentMethod = row["payment_method"].ToString();
                    int recurringValue = Convert.ToInt32(row["recurring"]);
                    string location = row["location"].ToString();
                    DateTime incomeDate = Convert.ToDateTime(row["income_date"]);
                    string imagePath = row["image_path"].ToString();
                    typeExpense = "Income";
                    PopulateTransactionData(amount, description, tags, paymentMethod, recurringValue, location, incomeDate, imagePath);
                    UpdateLabelColors();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
      
        public void ShowLocationOnMap(string location)
        {
            string[] parts = location.Split(',');
            if (parts.Length == 2 &&
                double.TryParse(parts[0].Trim(), out double lat) &&
                double.TryParse(parts[1].Trim(), out double lng))
            {
                mapHandler.SetPosition(lat, lng);
                mapHandler.AddMarker(lat, lng, "Vị trí của tôi");
            }
            else
            {
                MessageBox.Show("Dữ liệu tọa độ không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Expenses_Load(object sender, EventArgs e)
        {
            LoadExpense();
            InitializeControls();
            mapHandler = new MapHandler(gMapLoadMap, googleApiKey);
            LoadMap();
            UpdateLabelColors();
            LoadTransaction();
        }
        public void LoadTransaction()
        {
            var transactions = transactionController.GetRecentTransactions(_userId);

            var formattedData = transactions.Select(t => new
            {
                LoaiGiaoDich = t.ContainsKey("transaction_type") && t["transaction_type"] != null
                    ? (t["transaction_type"].ToString() == "Income" ? "Thu nhập" : "Chi tiêu")
                    : "Không xác định",
                SoTien = t.ContainsKey("amount") ? t["amount"] : 0,
                NgayGiaoDich = t.ContainsKey("date") ? t["date"] : DBNull.Value,
                TheLoai = t.ContainsKey("category_name") ? t["category_name"] : "Không có",
                MoTa = t.ContainsKey("expense_description") ? t["expense_description"] :
                       t.ContainsKey("income_description") ? t["income_description"] : "Không có",
                PhuongThucTT = t.ContainsKey("expense_payment_method") ? t["expense_payment_method"] :
                               t.ContainsKey("income_payment_method") ? t["income_payment_method"] : "Không có",
                NhanTag = t.ContainsKey("expense_tags") ? t["expense_tags"] :
                          t.ContainsKey("income_tags") ? t["income_tags"] : "Không có",

                ExpenseID = t.ContainsKey("expense_id") ? t["expense_id"] : DBNull.Value,
                IncomeID = t.ContainsKey("income_id") ? t["income_id"] : DBNull.Value
            }).ToList();

            dvgDataExpense.DataSource = formattedData;
            dvgDataExpense.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dvgDataExpense.Columns["LoaiGiaoDich"].HeaderText = "Loại Giao Dịch";
            dvgDataExpense.Columns["SoTien"].HeaderText = "Số Tiền";
            dvgDataExpense.Columns["NgayGiaoDich"].HeaderText = "Ngày Giao Dịch";
            dvgDataExpense.Columns["TheLoai"].HeaderText = "Thể Loại";
            dvgDataExpense.Columns["MoTa"].HeaderText = "Mô Tả";
            dvgDataExpense.Columns["PhuongThucTT"].HeaderText = "Phương Thức Thanh Toán";
            dvgDataExpense.Columns["NhanTag"].HeaderText = "Nhãn Tag";

            dvgDataExpense.Columns["ExpenseID"].Visible = false;
            dvgDataExpense.Columns["IncomeID"].Visible = false;
        }

        public async void LoadExpense()
        {

            messageManager = new MessageManager(flMessage, OnMessageClicked);
            await messageManager.DisplayMessages();

            LoadCameraDevices();
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
        private void InitializeControls()
        {
            // Init
            selectToday = false;
            tbAmount.Text = placeholderText;
            tbAmount.ForeColor = System.Drawing.Color.Gray;
            tbAmount.KeyPress += tbAmount_KeyPress;

            //Datetime picker

            //Recuring
            cbRecuring.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRecuring.Items.Add(new KeyValuePair<string, int>("No Repeat", 0));
            cbRecuring.Items.Add(new KeyValuePair<string, int>("Daily", 1));
            cbRecuring.Items.Add(new KeyValuePair<string, int>("Weekly", 2));
            cbRecuring.Items.Add(new KeyValuePair<string, int>("Monthly", 3));
            cbRecuring.Items.Add(new KeyValuePair<string, int>("Quarterly", 4));
            cbRecuring.Items.Add(new KeyValuePair<string, int>("Yearly", 5));

            cbRecuring.DisplayMember = "Key";  
            cbRecuring.ValueMember = "Value"; 
            cbRecuring.SelectedIndex = 0;

            // Pay Method
            cbPayMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            cbPayMethod.Items.Add("Cash");
            cbPayMethod.Items.Add("Credit Card");
            cbPayMethod.Items.Add("Debit Card");
            cbPayMethod.Items.Add("Bank Transfer");
            cbPayMethod.Items.Add("Mobile Payment");
            cbPayMethod.Items.Add("PayPal");
            cbPayMethod.Items.Add("Cryptocurrency");

            cbPayMethod.SelectedIndex = 0;

            //End date 
            dtpkEnddate.Visible = true;
            lbEnddate.Visible = true;
            dtpkEnddate.Visible = true;

            //btn Add
            btnAddExpenses.Enabled = true;
        }
        private void LoadCameraDevices()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count > 0)
            {
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
            }
            else
            {
                MessageBox.Show("Không tìm thấy camera!");
            }
        }
        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            capturedImage = (Bitmap)eventArgs.Frame.Clone();
            picUploadImage.Image = capturedImage;
        }

        private void LoadMap()
        {
            var location = mapHandler.GetCurrentLocation();
            if (location != null)
            {
                double lat = location.Item1;
                double lng = location.Item2;
                mapHandler.SetPosition(lat, lng);
                mapHandler.AddMarker(lat, lng, "Vị trí của bạn");

                string address = mapHandler.GetAddressFromCoordinates(lat, lng);
                lbSelectMap.Text = address;
                txtLocation = $"{lat}, {lng}";
            }
            else
            {
                lbSelectMap.Text = "Không thể lấy vị trí hiện tại!";
            }
        }

        private void Expenses_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }

        private void tbAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private string FormatAmount(decimal amount)
        {
            return amount.ToString("#,##0", new CultureInfo("vi-VN"));
        }

        private int GetAmountValue()
        {
            if (decimal.TryParse(tbAmount.Text.Replace(".", "").Replace(",", ""), out decimal value))
            {
                if (value > int.MaxValue)
                {
                    throw new OverflowException("The value is too large to fit in an Int32.");
                }

                return (int)Math.Abs(value);
            }
            return 0;
        }


        private void UpdateEndDate()
        {
            var selectedValue = ((KeyValuePair<string, int>)cbRecuring.SelectedItem).Value;

            if (selectedValue == 0 )
            {
                txtEndDate = null;
            }
            else { 
            

                DateTime now = DateTime.Now;
                DateTime endDate = now;

                switch (selectedValue)
                {
                    case 1:
                        endDate = now.AddDays(1);
                        break;
                    case 2:
                        endDate = now.AddDays(7);
                        break;
                    case 3:
                        endDate = now.AddMonths(1);
                        break;
                    case 4:
                        endDate = now.AddMonths(3);
                        break;
                    case 5:
                        endDate = now.AddYears(1);
                        break;
                }

                dtpkEnddate.Value = endDate;
                txtEndDate = endDate;
            }
        }

        private void UpdateLabelColors()
        {
            LoadCategory();
            if (typeExpense == "Expense")
            {
                lbExpense.ForeColor = Color.Black; 
                lbIncome.ForeColor = Color.Gray; 
            }
            else if (typeExpense == "Income")
            {
                lbIncome.ForeColor = Color.Black;
                lbExpense.ForeColor = Color.Gray;
            }
        }

        public void LoadCategory()
        {
            plLoadCategory.Controls.Clear();

            string categoryType = typeExpense == "Expense" ? "Expense" : "Income";
            var categorizedData = _categoryController.CategorizeByGroup(_userId, categoryType);

            foreach (var group in categorizedData)
            {
                string groupName = group["group_name"].ToString();
                string groupIconName = group["group_icon"].ToString();

                if (!Enum.TryParse(groupIconName, out IconChar groupIcon))
                {
                    groupIcon = IconChar.LayerGroup;
                }

                List<Dictionary<string, object>> categories = (List<Dictionary<string, object>>)group["categories"];

                CategoryGroup categoryGroup = new CategoryGroup(groupName, groupIcon, categories, 395);
                categoryGroup.CategoryClicked += OnCategoryClicked;
                plLoadCategory.Controls.Add(categoryGroup);
            }
        }

        private void OnCategoryClicked(int categoryId)
        {
            _selectedCategoryItem = categoryId;
        }

        private void lbIncome_Click(object sender, EventArgs e)
        {
            typeExpense = "Income";
            UpdateLabelColors();
        }

        private void lbExpense_Click(object sender, EventArgs e)
        {

            typeExpense = "Expense";
            UpdateLabelColors();
        }

        private void btnCurrentNow_Click(object sender, EventArgs e)
        {
            LoadMap();
            tbEnterAddress.Text = "";

        }

        private void chkInfinity_CheckedChanged(object sender, EventArgs e)
        {
            UpdateEndDate();
        }

        private void cbRecuring_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEndDate();
        }

        private void tbAmount_TextChanged(object sender, EventArgs e)
        {

            if (isTyping) return;

            isTyping = true;
            string rawText = tbAmount.Text.Replace(".", "").Replace("-", "").Replace("+", "");

            if (decimal.TryParse(rawText, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out decimal amount))
            {
                tbAmount.Text = (typeExpense == "Expense" ? "-" : "+") + FormatAmount(amount);
                tbAmount.SelectionStart = tbAmount.Text.Length;
            }
            else
            {
                tbAmount.Text = (typeExpense == "Expense" ? "-" : "+");
            }

            isTyping = false;
        }

        private void tbAmount_Leave(object sender, EventArgs e)
        {

            if (tbAmount.Text == "-" || string.IsNullOrWhiteSpace(tbAmount.Text.Substring(1)))
            {
                tbAmount.Text = placeholderText;
                tbAmount.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                if (decimal.TryParse(tbAmount.Text.Replace(".", ""), out decimal amount))
                {
                    tbAmount.Text = FormatAmount(amount);
                }
                else
                {
                    tbAmount.Text = placeholderText;
                    tbAmount.ForeColor = System.Drawing.Color.Black;
                }
            }
        }

        private void tbAmount_Enter(object sender, EventArgs e)
        {

            if (tbAmount.Text == placeholderText)
            {
                tbAmount.Text = typeExpense == "Expense" ? "-" : "+";
                tbAmount.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void dtpkDatetime_ValueChanged(object sender, EventArgs e)
        {

            selectToday = false;
            btnToday.BackColor = Color.White;
        }

        private void btnToday_Click(object sender, EventArgs e)
        {

            selectToday = true;
            dtpkDatetime.Value = DateTime.Now;

            btnToday.BackColor = Color.LightCoral;
        }

        private async void btnAddExpenses_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbAmount.Text) ||
                cbPayMethod.SelectedItem == null ||
                _selectedCategoryItem == 0 ||
                GetAmountValue() <= 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin và số tiền phải lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else
            {
                btnAddExpenses.Enabled = false;
                btnAddExpenses.Text = "Upload...";
                string imageUrl = null;

                if (capturedImage != null)
                {
                    imageUrl = await _cloudinaryHelper.UploadImageAsync(capturedImage);
                }
                else if (filePath != null)
                {
                    imageUrl = await _cloudinaryHelper.UploadImageAsync(filePath);
                }

                if (!string.IsNullOrEmpty(imageUrl))
                {
                    picUploadImage.ImageLocation = imageUrl;
                }

                btnAddExpenses.Enabled = true;
                btnAddExpenses.Text = "Thêm";

                txtExpenseDate = selectToday ? DateTime.Now : dtpkDatetime.Value;
                txtAmount = GetAmountValue();
                txtCategory = _selectedCategoryItem;
                txtDescription = tbDescription.Text;
                txtPaymentMethod = cbPayMethod.SelectedItem.ToString();
                txtTags = tbTags.Text;

                if (cbRecuring.SelectedItem != null)
                {
                    KeyValuePair<string, int> selectedRecuring = (KeyValuePair<string, int>)cbRecuring.SelectedItem;
                    txtRecurring = selectedRecuring.Value;
                }

                int newRecordId = 0;
                if (typeExpense == "Expense")
                {
                    newRecordId = _expensesController.RecordExpense(
                        _userId, txtCategory, txtAmount, txtExpenseDate, txtDescription,
                        txtPaymentMethod, txtRecurring, txtLocation, txtTags, imageUrl, txtEndDate);
                }
                else if (typeExpense == "Income")
                {
                    newRecordId = _incomeController.RecordIncome(
                        _userId, txtCategory, txtAmount, txtExpenseDate, txtDescription,
                        txtPaymentMethod, txtRecurring, txtLocation, txtTags, imageUrl, txtEndDate);
                }

                if (newRecordId > 0)
                {
                    ResetForm();
                    MessageBox.Show($"Ghi chép thành công! ");
                }
                else
                {
                    MessageBox.Show("Lỗi trong khi lưu");
                }
            }
            
        }

        private void ResetForm()
        {
            tbAmount.Clear();
            tbDescription.Clear();
            tbTags.Clear();
            cbRecuring.SelectedIndex = 0;
            cbPayMethod.SelectedIndex = 0;
            picUploadImage.Image = Properties.Resources.upload;
            dtpkDatetime.Value = DateTime.Now;
            filePath = null;
            capturedImage = null;
        }


        private void gMapLoadMap_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                var point = gMapLoadMap.FromLocalToLatLng(e.X, e.Y);
                string address = mapHandler.GetAddressFromCoordinates(point.Lat, point.Lng);
                lbSelectMap.Text = address;
                mapHandler.AddMarker(point.Lat, point.Lng, address);
                txtLocation = $"{point.Lat}, {point.Lng}";
            }
        }


        private void lbSuggestions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSuggestions.SelectedItem != null)
            {
                string selectedAddress = lbSuggestions.SelectedItem.ToString();
                tbEnterAddress.Text = selectedAddress;
                lbSuggestions.Visible = false;

                var coordinates = mapHandler.GetCoordinatesFromAddress(selectedAddress);
                if (coordinates != null)
                {
                    double lat = coordinates.Item1;
                    double lng = coordinates.Item2;
                    mapHandler.SetPosition(lat, lng);
                    mapHandler.AddMarker(lat, lng, selectedAddress);
                    lbSelectMap.Text = selectedAddress;
                    tbEnterAddress.Text = "";
                }
            }
        }

        private void tbEnterAddress_TextChanged(object sender, EventArgs e)
        {
            string input = tbEnterAddress.Text;
            lbSuggestions.Visible = false;
            if (!string.IsNullOrWhiteSpace(input))
            {
                List<string> suggestions = mapHandler.GetAddressSuggestions(input);
                if (suggestions.Count > 0)
                {
                    lbSuggestions.Items.Clear();
                    lbSuggestions.Items.AddRange(suggestions.ToArray());
                    lbSuggestions.Visible = true;
                }
                else
                {
                    lbSuggestions.Visible = false;
                }
            }
            else
            {
                lbSuggestions.Visible = false;
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {

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

        private void openDashBoard_Click(object sender, EventArgs e)
        {

        }

        

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(typeExpense) || (_expenseId == 0 && _incomeId == 0))
            {
                MessageBox.Show("Không có giao dịch hợp lệ để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa giao dịch này?", "Xác nhận xóa",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int deletedId = typeExpense == "Expense"
                    ? _expensesController.DeleteExpense(_expenseId, _userId)
                    : _incomeController.DeleteIncome(_incomeId, _userId);

                if (deletedId > 0)
                {
                    MessageBox.Show("Xóa giao dịch thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTransaction();
                }
                else
                {
                    MessageBox.Show("Xóa giao dịch thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnTakePictrue_Click(object sender, EventArgs e)
        {
            filePath = null;

            if (videoSource == null)
            {
                MessageBox.Show("Camera chưa được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!videoSource.IsRunning)
            {
                videoSource.Start();
            }
            else
            {
                if (picUploadImage.Image != null)
                {
                    capturedImage = (Bitmap)picUploadImage.Image.Clone();
                }

                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }

        private void picUploadImage_Click(object sender, EventArgs e)
        {
            if (videoSource?.IsRunning == true)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    picUploadImage.SizeMode = PictureBoxSizeMode.Zoom;
                    picUploadImage.Image = new Bitmap(Image.FromFile(filePath));
                }
            }
        }
        private void dvgDataExpense_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dvgDataExpense.Rows[e.RowIndex];
            string transactionType = row.Cells["LoaiGiaoDich"].Value.ToString();

            if (transactionType == "Thu nhập")
            {
                _incomeId = row.Cells["IncomeID"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["IncomeID"].Value) : 0;
                _expenseId = 0;

                DataTable dataIncome = _incomeController.GetIncomeByIdCreate(_userId, _incomeId);
                if (dataIncome.Rows.Count > 0)
                {
                    DataRow dataRow = dataIncome.Rows[0];

                    PopulateTransactionData(
                        dataRow["amount"].ToString(),
                        dataRow["description"].ToString(),
                        dataRow["tags"].ToString(),
                        dataRow["payment_method"].ToString(),
                        Convert.ToInt32(dataRow["recurring"]),
                        dataRow["location"].ToString(),
                        Convert.ToDateTime(dataRow["income_date"]),
                        dataRow["image_path"].ToString()
                    );

                    if (!int.TryParse(dataRow["category_id"]?.ToString(), out _selectedCategoryItem))
                    {
                        _selectedCategoryItem = -1;
                    }

                    typeExpense = "Income";
                    UpdateLabelColors();
                }
            }
            else if (transactionType == "Chi tiêu")
            {
                _expenseId = row.Cells["ExpenseID"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["ExpenseID"].Value) : 0;
                _incomeId = 0;

                DataTable dataExpense = _expensesController.GetExpenseByIdCreate(_userId, _expenseId);
                if (dataExpense.Rows.Count > 0)
                {
                    DataRow dataRow = dataExpense.Rows[0];

                    PopulateTransactionData(
                        dataRow["amount"].ToString(),
                        dataRow["description"].ToString(),
                        dataRow["tags"].ToString(),
                        dataRow["payment_method"].ToString(),
                        Convert.ToInt32(dataRow["recurring"]),
                        dataRow["location"].ToString(),
                        Convert.ToDateTime(dataRow["expense_date"]),
                        dataRow["image_path"].ToString()
                    );
                    if (!int.TryParse(dataRow["category_id"]?.ToString(), out _selectedCategoryItem))
                    {
                        _selectedCategoryItem = -1;
                    }

                    typeExpense = "Expense";
                    UpdateLabelColors();
                }
            }
        }

        public void PopulateTransactionData(string amount, string description, string tags, string paymentMethod, int recurringValue, string location, DateTime transactionDate, string imagePath)
        {
            tbDescription.Text = description;
            tbTags.Text = tags;
            decimal amounts = Convert.ToDecimal(amount);
            int result = (int)Math.Abs(amounts);

            tbAmount.Text = amounts.ToString("N0");
            if (cbPayMethod.Items.Contains(paymentMethod))
            {
                cbPayMethod.SelectedItem = paymentMethod;
            }

            foreach (KeyValuePair<string, int> item in cbRecuring.Items)
            {
                if (item.Value == recurringValue)
                {
                    cbRecuring.SelectedItem = item;
                    break;
                }
            }

            dtpkDatetime.Value = transactionDate;

            if (!string.IsNullOrEmpty(location) && location.Contains(","))
            {
                string[] parts = location.Split(',');
                if (parts.Length == 2 &&
                    double.TryParse(parts[0].Trim(), out double lat) &&
                    double.TryParse(parts[1].Trim(), out double lng))
                {
                    ShowLocationOnMap(location);
                }
                else
                {
                    MessageBox.Show("Vị trí không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                picUploadImage.Image = Image.FromFile(imagePath);
            }
            else if (!string.IsNullOrEmpty(imagePath))
            {
                _publicClouldId = _cloudinaryHelper.GetPublicIdFromUrl(imagePath);
                picUploadImage.ImageLocation = imagePath;
            }
            else
            {
                picUploadImage.Image = Properties.Resources.upload;
            }

        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbAmount.Text) ||
                cbPayMethod.SelectedItem == null ||
                _selectedCategoryItem == 0 ||
                GetAmountValue() <= 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin và số tiền phải lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else
            {
                btnUpdate.Enabled = false;
                btnUpdate.Text = "Upload...";
                string imageUrl = null;

                if (capturedImage != null)
                {
                    imageUrl = await _cloudinaryHelper.UpdateImageAsync(_publicClouldId, capturedImage);
                }
                else if (filePath != null)
                {
                    imageUrl = await _cloudinaryHelper.UpdateImageAsync(_publicClouldId, filePath);
                }

                if (!string.IsNullOrEmpty(imageUrl))
                {
                    picUploadImage.ImageLocation = imageUrl;
                }

                btnUpdate.Enabled = true;
                btnUpdate.Text = "Sửa";

                txtExpenseDate = selectToday ? DateTime.Now : dtpkDatetime.Value;
                txtAmount = GetAmountValue();
                txtCategory = _selectedCategoryItem;
                txtDescription = tbDescription.Text;
                txtPaymentMethod = cbPayMethod.SelectedItem.ToString();
                txtTags = tbTags.Text;

                if (cbRecuring.SelectedItem != null)
                {
                    KeyValuePair<string, int> selectedRecuring = (KeyValuePair<string, int>)cbRecuring.SelectedItem;
                    txtRecurring = selectedRecuring.Value;
                }

                int updateSuccess = 0;
                if (typeExpense == "Expense")
                {
                    updateSuccess = _expensesController.UpdateExpense(_expenseId,
                        _userId, txtCategory, txtAmount, txtExpenseDate, txtDescription,
                        txtPaymentMethod, txtRecurring, txtLocation, txtTags, imageUrl, txtEndDate); 
                }

                else if (typeExpense == "Income")
                {
                    updateSuccess = _incomeController.UpdateIncome(_incomeId,
                        _userId, txtCategory, txtAmount, txtExpenseDate, txtDescription,
                        txtPaymentMethod, txtRecurring, txtLocation, txtTags, imageUrl, txtEndDate);
                }

                if (updateSuccess > 0)
                {
                    ResetForm();
                    MessageBox.Show(" Cập nhật ghi chép thành công! ");
                }
                else
                {
                    MessageBox.Show("Lỗi trong khi lưu");
                }
            }

        }
        private void tbMesage_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel25_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
