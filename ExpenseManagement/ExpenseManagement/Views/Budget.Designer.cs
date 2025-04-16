namespace ExpenseManagement.Views
{
    partial class Budget
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.plLeft = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.dataCat = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.tbWarningCat = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbCat = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnDeleteCat = new System.Windows.Forms.Label();
            this.btnUpdateCat = new System.Windows.Forms.Label();
            this.btnAddCat = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dateToCat = new System.Windows.Forms.DateTimePicker();
            this.dateFromCat = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.tbAmountCat = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.chartCategoryBudget = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataTotal = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.pl = new System.Windows.Forms.Panel();
            this.tbWarning = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dateTotalTo = new System.Windows.Forms.DateTimePicker();
            this.dateFromTotal = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.tbAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.chartTotalBudget = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label2 = new System.Windows.Forms.Label();
            this.plLeft.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataCat)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartCategoryBudget)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTotal)).BeginInit();
            this.pl.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTotalBudget)).BeginInit();
            this.SuspendLayout();
            // 
            // plLeft
            // 
            this.plLeft.Controls.Add(this.panel5);
            this.plLeft.Controls.Add(this.panel4);
            this.plLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plLeft.Location = new System.Drawing.Point(0, 0);
            this.plLeft.Name = "plLeft";
            this.plLeft.Size = new System.Drawing.Size(1180, 1100);
            this.plLeft.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.GhostWhite;
            this.panel5.Controls.Add(this.panel9);
            this.panel5.Controls.Add(this.panel8);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(587, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(593, 1100);
            this.panel5.TabIndex = 3;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.dataCat);
            this.panel9.Controls.Add(this.label11);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(0, 613);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(20);
            this.panel9.Size = new System.Drawing.Size(593, 487);
            this.panel9.TabIndex = 7;
            // 
            // dataCat
            // 
            this.dataCat.BackgroundColor = System.Drawing.Color.White;
            this.dataCat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataCat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataCat.Location = new System.Drawing.Point(20, 61);
            this.dataCat.Name = "dataCat";
            this.dataCat.Size = new System.Drawing.Size(553, 406);
            this.dataCat.TabIndex = 8;
            this.dataCat.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataCat_CellClick);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.LightBlue;
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label11.Font = new System.Drawing.Font("Poppins Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(20, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(553, 41);
            this.label11.TabIndex = 7;
            this.label11.Text = "Danh sách ngân sách danh mục";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.tbWarningCat);
            this.panel8.Controls.Add(this.label13);
            this.panel8.Controls.Add(this.cbCat);
            this.panel8.Controls.Add(this.label15);
            this.panel8.Controls.Add(this.btnDeleteCat);
            this.panel8.Controls.Add(this.btnUpdateCat);
            this.panel8.Controls.Add(this.btnAddCat);
            this.panel8.Controls.Add(this.label9);
            this.panel8.Controls.Add(this.dateToCat);
            this.panel8.Controls.Add(this.dateFromCat);
            this.panel8.Controls.Add(this.label7);
            this.panel8.Controls.Add(this.tbAmountCat);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 343);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(593, 270);
            this.panel8.TabIndex = 6;
            // 
            // tbWarningCat
            // 
            this.tbWarningCat.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbWarningCat.Location = new System.Drawing.Point(134, 170);
            this.tbWarningCat.Name = "tbWarningCat";
            this.tbWarningCat.Size = new System.Drawing.Size(286, 31);
            this.tbWarningCat.TabIndex = 14;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Poppins SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(22, 176);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(106, 23);
            this.label13.TabIndex = 13;
            this.label13.Text = "Mức cảnh báo";
            // 
            // cbCat
            // 
            this.cbCat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCat.FormattingEnabled = true;
            this.cbCat.Location = new System.Drawing.Point(134, 128);
            this.cbCat.Name = "cbCat";
            this.cbCat.Size = new System.Drawing.Size(286, 26);
            this.cbCat.TabIndex = 12;
            this.cbCat.SelectedIndexChanged += new System.EventHandler(this.cbCat_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Poppins SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(21, 136);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 23);
            this.label15.TabIndex = 11;
            this.label15.Text = "Danh mục";
            // 
            // btnDeleteCat
            // 
            this.btnDeleteCat.BackColor = System.Drawing.Color.LightCoral;
            this.btnDeleteCat.Font = new System.Drawing.Font("Poppins Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteCat.Location = new System.Drawing.Point(296, 212);
            this.btnDeleteCat.Name = "btnDeleteCat";
            this.btnDeleteCat.Size = new System.Drawing.Size(73, 38);
            this.btnDeleteCat.TabIndex = 10;
            this.btnDeleteCat.Text = "Xóa";
            this.btnDeleteCat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDeleteCat.Click += new System.EventHandler(this.btnDeleteCat_Click);
            // 
            // btnUpdateCat
            // 
            this.btnUpdateCat.BackColor = System.Drawing.Color.LightCoral;
            this.btnUpdateCat.Font = new System.Drawing.Font("Poppins Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateCat.Location = new System.Drawing.Point(217, 212);
            this.btnUpdateCat.Name = "btnUpdateCat";
            this.btnUpdateCat.Size = new System.Drawing.Size(73, 38);
            this.btnUpdateCat.TabIndex = 9;
            this.btnUpdateCat.Text = "Sửa";
            this.btnUpdateCat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUpdateCat.Click += new System.EventHandler(this.btnUpdateCat_Click);
            // 
            // btnAddCat
            // 
            this.btnAddCat.BackColor = System.Drawing.Color.LightCoral;
            this.btnAddCat.Font = new System.Drawing.Font("Poppins Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCat.Location = new System.Drawing.Point(138, 212);
            this.btnAddCat.Name = "btnAddCat";
            this.btnAddCat.Size = new System.Drawing.Size(73, 38);
            this.btnAddCat.TabIndex = 8;
            this.btnAddCat.Text = "Thêm";
            this.btnAddCat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddCat.Click += new System.EventHandler(this.btnAddCat_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Poppins SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(22, 84);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 23);
            this.label9.TabIndex = 6;
            this.label9.Text = "Ngày bắt đầu";
            // 
            // dateToCat
            // 
            this.dateToCat.CustomFormat = "dd/ MM/ yyyy";
            this.dateToCat.Font = new System.Drawing.Font("Poppins Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateToCat.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateToCat.Location = new System.Drawing.Point(280, 80);
            this.dateToCat.Name = "dateToCat";
            this.dateToCat.Size = new System.Drawing.Size(140, 27);
            this.dateToCat.TabIndex = 5;
            // 
            // dateFromCat
            // 
            this.dateFromCat.CustomFormat = "dd/ MM/ yyyy";
            this.dateFromCat.Font = new System.Drawing.Font("Poppins Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateFromCat.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateFromCat.Location = new System.Drawing.Point(134, 80);
            this.dateFromCat.Name = "dateFromCat";
            this.dateFromCat.Size = new System.Drawing.Size(140, 27);
            this.dateFromCat.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Poppins SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(22, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 23);
            this.label7.TabIndex = 2;
            this.label7.Text = "Số tiền";
            // 
            // tbAmountCat
            // 
            this.tbAmountCat.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAmountCat.Location = new System.Drawing.Point(134, 25);
            this.tbAmountCat.Name = "tbAmountCat";
            this.tbAmountCat.Size = new System.Drawing.Size(286, 31);
            this.tbAmountCat.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.LightBlue;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label5.Font = new System.Drawing.Font("Poppins Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 302);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(593, 41);
            this.label5.TabIndex = 5;
            this.label5.Text = "Tạo mới ngân sách cho danh mục";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.chartCategoryBudget);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 41);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(593, 261);
            this.panel6.TabIndex = 2;
            // 
            // chartCategoryBudget
            // 
            this.chartCategoryBudget.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.chartCategoryBudget.Legends.Add(legend3);
            this.chartCategoryBudget.Location = new System.Drawing.Point(0, 0);
            this.chartCategoryBudget.Name = "chartCategoryBudget";
            this.chartCategoryBudget.Size = new System.Drawing.Size(593, 261);
            this.chartCategoryBudget.TabIndex = 1;
            this.chartCategoryBudget.Text = "chart1";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LightBlue;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label3.Font = new System.Drawing.Font("Poppins Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(593, 41);
            this.label3.TabIndex = 1;
            this.label3.Text = "Ngân sách cho danh mục";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Snow;
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.pl);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(587, 1100);
            this.panel4.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataTotal);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 613);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(20);
            this.panel2.Size = new System.Drawing.Size(587, 487);
            this.panel2.TabIndex = 6;
            // 
            // dataTotal
            // 
            this.dataTotal.BackgroundColor = System.Drawing.Color.White;
            this.dataTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataTotal.Location = new System.Drawing.Point(20, 61);
            this.dataTotal.Name = "dataTotal";
            this.dataTotal.Size = new System.Drawing.Size(547, 406);
            this.dataTotal.TabIndex = 6;
            this.dataTotal.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataTotal_CellClick);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.MistyRose;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label10.Font = new System.Drawing.Font("Poppins Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(20, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(547, 41);
            this.label10.TabIndex = 5;
            this.label10.Text = "Danh sách ngân sách tổng";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pl
            // 
            this.pl.Controls.Add(this.tbWarning);
            this.pl.Controls.Add(this.label12);
            this.pl.Controls.Add(this.btnDelete);
            this.pl.Controls.Add(this.btnUpdate);
            this.pl.Controls.Add(this.btnAdd);
            this.pl.Controls.Add(this.label8);
            this.pl.Controls.Add(this.dateTotalTo);
            this.pl.Controls.Add(this.dateFromTotal);
            this.pl.Controls.Add(this.label6);
            this.pl.Controls.Add(this.tbAmount);
            this.pl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pl.Location = new System.Drawing.Point(0, 343);
            this.pl.Name = "pl";
            this.pl.Size = new System.Drawing.Size(587, 270);
            this.pl.TabIndex = 5;
            // 
            // tbWarning
            // 
            this.tbWarning.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbWarning.Location = new System.Drawing.Point(131, 131);
            this.tbWarning.Name = "tbWarning";
            this.tbWarning.Size = new System.Drawing.Size(286, 31);
            this.tbWarning.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Poppins SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(19, 137);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 23);
            this.label12.TabIndex = 8;
            this.label12.Text = "Mức cảnh báo";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.LightCoral;
            this.btnDelete.Font = new System.Drawing.Font("Poppins Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(294, 212);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(73, 38);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.LightCoral;
            this.btnUpdate.Font = new System.Drawing.Font("Poppins Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(215, 212);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(73, 38);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Sửa";
            this.btnUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.LightCoral;
            this.btnAdd.Font = new System.Drawing.Font("Poppins Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(136, 212);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(73, 38);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Poppins SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(19, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 23);
            this.label8.TabIndex = 4;
            this.label8.Text = "Ngày bắt đầu";
            // 
            // dateTotalTo
            // 
            this.dateTotalTo.CustomFormat = "dd/ MM/ yyyy";
            this.dateTotalTo.Font = new System.Drawing.Font("Poppins Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTotalTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTotalTo.Location = new System.Drawing.Point(277, 86);
            this.dateTotalTo.Name = "dateTotalTo";
            this.dateTotalTo.Size = new System.Drawing.Size(140, 27);
            this.dateTotalTo.TabIndex = 3;
            // 
            // dateFromTotal
            // 
            this.dateFromTotal.CustomFormat = "dd/ MM/ yyyy";
            this.dateFromTotal.Font = new System.Drawing.Font("Poppins Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateFromTotal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateFromTotal.Location = new System.Drawing.Point(131, 86);
            this.dateFromTotal.Name = "dateFromTotal";
            this.dateFromTotal.Size = new System.Drawing.Size(140, 27);
            this.dateFromTotal.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Poppins SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(19, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 23);
            this.label6.TabIndex = 1;
            this.label6.Text = "Số tiền";
            // 
            // tbAmount
            // 
            this.tbAmount.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAmount.Location = new System.Drawing.Point(131, 31);
            this.tbAmount.Name = "tbAmount";
            this.tbAmount.Size = new System.Drawing.Size(286, 31);
            this.tbAmount.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.MistyRose;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label4.Font = new System.Drawing.Font("Poppins Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 302);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(587, 41);
            this.label4.TabIndex = 4;
            this.label4.Text = "Tạo mới ngân sách tổng";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.chartTotalBudget);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 41);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(587, 261);
            this.panel7.TabIndex = 3;
            // 
            // chartTotalBudget
            // 
            this.chartTotalBudget.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Name = "Legend1";
            this.chartTotalBudget.Legends.Add(legend4);
            this.chartTotalBudget.Location = new System.Drawing.Point(0, 0);
            this.chartTotalBudget.Name = "chartTotalBudget";
            this.chartTotalBudget.Size = new System.Drawing.Size(587, 261);
            this.chartTotalBudget.TabIndex = 0;
            this.chartTotalBudget.Text = "chart1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.MistyRose;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Font = new System.Drawing.Font("Poppins Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(587, 41);
            this.label2.TabIndex = 0;
            this.label2.Text = "Ngân sách tổng";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Budget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1180, 1100);
            this.Controls.Add(this.plLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Budget";
            this.Text = "Budget";
            this.Load += new System.EventHandler(this.Budget_Load);
            this.plLeft.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataCat)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartCategoryBudget)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataTotal)).EndInit();
            this.pl.ResumeLayout(false);
            this.pl.PerformLayout();
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartTotalBudget)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel plLeft;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TextBox tbAmountCat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCategoryBudget;
        private System.Windows.Forms.Panel pl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTotalBudget;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTotalTo;
        private System.Windows.Forms.DateTimePicker dateFromTotal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dateToCat;
        private System.Windows.Forms.DateTimePicker dateFromCat;
        private System.Windows.Forms.Label btnAdd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbCat;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label btnDeleteCat;
        private System.Windows.Forms.Label btnUpdateCat;
        private System.Windows.Forms.Label btnAddCat;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label btnDelete;
        private System.Windows.Forms.Label btnUpdate;
        private System.Windows.Forms.DataGridView dataTotal;
        private System.Windows.Forms.DataGridView dataCat;
        private System.Windows.Forms.TextBox tbWarningCat;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbWarning;
        private System.Windows.Forms.Label label12;
    }
}