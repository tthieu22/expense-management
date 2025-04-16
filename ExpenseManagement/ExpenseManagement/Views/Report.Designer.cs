namespace ExpenseManagement.Views
{
    partial class Report
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Report));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel2 = new System.Windows.Forms.Panel();
            this.monthNext = new System.Windows.Forms.Label();
            this.monthPrev = new System.Windows.Forms.Label();
            this.monthNow = new System.Windows.Forms.Label();
            this.btnPrevMonth = new System.Windows.Forms.PictureBox();
            this.btnNextMonth = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbToAmount = new System.Windows.Forms.TextBox();
            this.tbFromAmount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ckIncome = new System.Windows.Forms.CheckBox();
            this.ckExpense = new System.Windows.Forms.CheckBox();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Panel();
            this.picExportReport = new System.Windows.Forms.PictureBox();
            this.textExportReport = new System.Windows.Forms.Label();
            this.raColumn = new System.Windows.Forms.RadioButton();
            this.raLine = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.radioYear = new System.Windows.Forms.RadioButton();
            this.radioQuatar = new System.Windows.Forms.RadioButton();
            this.radioMonth = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dataReport = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.chartDisplayChildren = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label13 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.chartDisplayParent = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrevMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNextMonth)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.btnExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExportReport)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataReport)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDisplayChildren)).BeginInit();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDisplayParent)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Snow;
            this.panel2.Controls.Add(this.monthNext);
            this.panel2.Controls.Add(this.monthPrev);
            this.panel2.Controls.Add(this.monthNow);
            this.panel2.Controls.Add(this.btnPrevMonth);
            this.panel2.Controls.Add(this.btnNextMonth);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1180, 47);
            this.panel2.TabIndex = 1;
            // 
            // monthNext
            // 
            this.monthNext.AutoSize = true;
            this.monthNext.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monthNext.Location = new System.Drawing.Point(664, 14);
            this.monthNext.Name = "monthNext";
            this.monthNext.Size = new System.Drawing.Size(58, 19);
            this.monthNext.TabIndex = 4;
            this.monthNext.Text = "2/2024";
            // 
            // monthPrev
            // 
            this.monthPrev.AutoSize = true;
            this.monthPrev.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monthPrev.Location = new System.Drawing.Point(432, 14);
            this.monthPrev.Name = "monthPrev";
            this.monthPrev.Size = new System.Drawing.Size(58, 19);
            this.monthPrev.TabIndex = 3;
            this.monthPrev.Text = "2/2024";
            // 
            // monthNow
            // 
            this.monthNow.AutoSize = true;
            this.monthNow.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monthNow.Location = new System.Drawing.Point(548, 14);
            this.monthNow.Name = "monthNow";
            this.monthNow.Size = new System.Drawing.Size(58, 19);
            this.monthNow.TabIndex = 2;
            this.monthNow.Text = "2/2024";
            // 
            // btnPrevMonth
            // 
            this.btnPrevMonth.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPrevMonth.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevMonth.Image")));
            this.btnPrevMonth.Location = new System.Drawing.Point(0, 0);
            this.btnPrevMonth.Name = "btnPrevMonth";
            this.btnPrevMonth.Size = new System.Drawing.Size(53, 47);
            this.btnPrevMonth.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnPrevMonth.TabIndex = 1;
            this.btnPrevMonth.TabStop = false;
            this.btnPrevMonth.Click += new System.EventHandler(this.btnPrevMonth_Click);
            // 
            // btnNextMonth
            // 
            this.btnNextMonth.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNextMonth.Image = ((System.Drawing.Image)(resources.GetObject("btnNextMonth.Image")));
            this.btnNextMonth.Location = new System.Drawing.Point(1130, 0);
            this.btnNextMonth.Name = "btnNextMonth";
            this.btnNextMonth.Size = new System.Drawing.Size(50, 47);
            this.btnNextMonth.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnNextMonth.TabIndex = 0;
            this.btnNextMonth.TabStop = false;
            this.btnNextMonth.Click += new System.EventHandler(this.btnNextMonth_Click);
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.tbToAmount);
            this.panel3.Controls.Add(this.tbFromAmount);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.ckIncome);
            this.panel3.Controls.Add(this.ckExpense);
            this.panel3.Controls.Add(this.dateTo);
            this.panel3.Controls.Add(this.dateFrom);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.raColumn);
            this.panel3.Controls.Add(this.raLine);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.radioYear);
            this.panel3.Controls.Add(this.radioQuatar);
            this.panel3.Controls.Add(this.radioMonth);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 47);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(20);
            this.panel3.Size = new System.Drawing.Size(1180, 216);
            this.panel3.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(549, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 16);
            this.label6.TabIndex = 24;
            this.label6.Text = "Đến";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(369, 126);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 16);
            this.label9.TabIndex = 23;
            this.label9.Text = "Từ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(193, 126);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 16);
            this.label12.TabIndex = 22;
            this.label12.Text = "Đến";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(11, 99);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 19);
            this.label11.TabIndex = 21;
            this.label11.Text = "Ngày";
            // 
            // tbToAmount
            // 
            this.tbToAmount.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbToAmount.Location = new System.Drawing.Point(552, 144);
            this.tbToAmount.Name = "tbToAmount";
            this.tbToAmount.Size = new System.Drawing.Size(145, 29);
            this.tbToAmount.TabIndex = 19;
            this.tbToAmount.TextChanged += new System.EventHandler(this.tbToAmount_TextChanged);
            // 
            // tbFromAmount
            // 
            this.tbFromAmount.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFromAmount.Location = new System.Drawing.Point(372, 144);
            this.tbFromAmount.Name = "tbFromAmount";
            this.tbFromAmount.Size = new System.Drawing.Size(157, 29);
            this.tbFromAmount.TabIndex = 17;
            this.tbFromAmount.TextChanged += new System.EventHandler(this.tbFromAmount_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(368, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 19);
            this.label8.TabIndex = 16;
            this.label8.Text = "Số tiền";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 19);
            this.label7.TabIndex = 15;
            this.label7.Text = "Loại";
            // 
            // ckIncome
            // 
            this.ckIncome.AutoSize = true;
            this.ckIncome.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckIncome.Location = new System.Drawing.Point(113, 59);
            this.ckIncome.Name = "ckIncome";
            this.ckIncome.Size = new System.Drawing.Size(96, 25);
            this.ckIncome.TabIndex = 14;
            this.ckIncome.Text = "Thu nhập";
            this.ckIncome.UseVisualStyleBackColor = true;
            this.ckIncome.CheckedChanged += new System.EventHandler(this.ckIncome_CheckedChanged);
            // 
            // ckExpense
            // 
            this.ckExpense.AutoSize = true;
            this.ckExpense.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckExpense.Location = new System.Drawing.Point(16, 59);
            this.ckExpense.Name = "ckExpense";
            this.ckExpense.Size = new System.Drawing.Size(84, 25);
            this.ckExpense.TabIndex = 13;
            this.ckExpense.Text = "Chi tiêu";
            this.ckExpense.UseVisualStyleBackColor = true;
            this.ckExpense.CheckedChanged += new System.EventHandler(this.ckExpense_CheckedChanged);
            // 
            // dateTo
            // 
            this.dateTo.CustomFormat = "dd/ MM/ yyyy";
            this.dateTo.Font = new System.Drawing.Font("Roboto Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTo.Location = new System.Drawing.Point(196, 146);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(144, 27);
            this.dateTo.TabIndex = 12;
            this.dateTo.ValueChanged += new System.EventHandler(this.dateTo_ValueChanged);
            // 
            // dateFrom
            // 
            this.dateFrom.CustomFormat = "dd/ MM/ yyyy";
            this.dateFrom.Font = new System.Drawing.Font("Roboto Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateFrom.Location = new System.Drawing.Point(16, 146);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(157, 27);
            this.dateFrom.TabIndex = 11;
            this.dateFrom.ValueChanged += new System.EventHandler(this.dateFrom_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Từ";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightSalmon;
            this.panel4.Controls.Add(this.btnExport);
            this.panel4.Location = new System.Drawing.Point(928, 126);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panel4.Size = new System.Drawing.Size(240, 67);
            this.panel4.TabIndex = 0;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.MistyRose;
            this.btnExport.Controls.Add(this.picExportReport);
            this.btnExport.Controls.Add(this.textExportReport);
            this.btnExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExport.Location = new System.Drawing.Point(20, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(200, 47);
            this.btnExport.TabIndex = 0;
            this.btnExport.Paint += new System.Windows.Forms.PaintEventHandler(this.btnExport_Paint);
            // 
            // picExportReport
            // 
            this.picExportReport.Image = ((System.Drawing.Image)(resources.GetObject("picExportReport.Image")));
            this.picExportReport.Location = new System.Drawing.Point(144, 3);
            this.picExportReport.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.picExportReport.Name = "picExportReport";
            this.picExportReport.Padding = new System.Windows.Forms.Padding(10);
            this.picExportReport.Size = new System.Drawing.Size(49, 42);
            this.picExportReport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picExportReport.TabIndex = 4;
            this.picExportReport.TabStop = false;
            this.picExportReport.Click += new System.EventHandler(this.picExportReport_Click);
            // 
            // textExportReport
            // 
            this.textExportReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textExportReport.Font = new System.Drawing.Font("Roboto Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textExportReport.Location = new System.Drawing.Point(0, 0);
            this.textExportReport.Name = "textExportReport";
            this.textExportReport.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.textExportReport.Size = new System.Drawing.Size(200, 47);
            this.textExportReport.TabIndex = 5;
            this.textExportReport.Text = "Xuất báo cáo";
            this.textExportReport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // raColumn
            // 
            this.raColumn.AutoSize = true;
            this.raColumn.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.raColumn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.raColumn.Location = new System.Drawing.Point(436, 59);
            this.raColumn.Name = "raColumn";
            this.raColumn.Size = new System.Drawing.Size(84, 25);
            this.raColumn.TabIndex = 27;
            this.raColumn.TabStop = true;
            this.raColumn.Text = "Column";
            this.raColumn.UseVisualStyleBackColor = true;
            this.raColumn.Visible = false;
            this.raColumn.CheckedChanged += new System.EventHandler(this.raColumn_CheckedChanged);
            // 
            // raLine
            // 
            this.raLine.AutoSize = true;
            this.raLine.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.raLine.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.raLine.Location = new System.Drawing.Point(372, 59);
            this.raLine.Name = "raLine";
            this.raLine.Size = new System.Drawing.Size(58, 25);
            this.raLine.TabIndex = 26;
            this.raLine.TabStop = true;
            this.raLine.Text = "Line";
            this.raLine.UseVisualStyleBackColor = true;
            this.raLine.Visible = false;
            this.raLine.CheckedChanged += new System.EventHandler(this.raLine_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(368, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 19);
            this.label10.TabIndex = 25;
            this.label10.Text = "Chart";
            this.label10.Visible = false;
            // 
            // radioYear
            // 
            this.radioYear.AutoSize = true;
            this.radioYear.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioYear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.radioYear.Location = new System.Drawing.Point(742, 150);
            this.radioYear.Name = "radioYear";
            this.radioYear.Size = new System.Drawing.Size(59, 25);
            this.radioYear.TabIndex = 8;
            this.radioYear.TabStop = true;
            this.radioYear.Text = "Year";
            this.radioYear.UseVisualStyleBackColor = true;
            this.radioYear.Visible = false;
            this.radioYear.CheckedChanged += new System.EventHandler(this.radioYear_CheckedChanged);
            // 
            // radioQuatar
            // 
            this.radioQuatar.AutoSize = true;
            this.radioQuatar.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioQuatar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.radioQuatar.Location = new System.Drawing.Point(742, 115);
            this.radioQuatar.Name = "radioQuatar";
            this.radioQuatar.Size = new System.Drawing.Size(77, 25);
            this.radioQuatar.TabIndex = 7;
            this.radioQuatar.TabStop = true;
            this.radioQuatar.Text = "Quatar";
            this.radioQuatar.UseVisualStyleBackColor = true;
            this.radioQuatar.Visible = false;
            this.radioQuatar.CheckedChanged += new System.EventHandler(this.radioQuatar_CheckedChanged);
            // 
            // radioMonth
            // 
            this.radioMonth.AutoSize = true;
            this.radioMonth.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioMonth.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.radioMonth.Location = new System.Drawing.Point(742, 78);
            this.radioMonth.Name = "radioMonth";
            this.radioMonth.Size = new System.Drawing.Size(77, 25);
            this.radioMonth.TabIndex = 6;
            this.radioMonth.TabStop = true;
            this.radioMonth.Text = "Month";
            this.radioMonth.UseVisualStyleBackColor = true;
            this.radioMonth.Visible = false;
            this.radioMonth.CheckedChanged += new System.EventHandler(this.radioMonth_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(738, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Filter";
            this.label2.Visible = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dataReport);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 653);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1180, 267);
            this.panel5.TabIndex = 49;
            // 
            // dataReport
            // 
            this.dataReport.BackgroundColor = System.Drawing.Color.White;
            this.dataReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataReport.Location = new System.Drawing.Point(0, 32);
            this.dataReport.Name = "dataReport";
            this.dataReport.Size = new System.Drawing.Size(1180, 235);
            this.dataReport.TabIndex = 52;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1180, 32);
            this.label4.TabIndex = 51;
            this.label4.Text = "Danh sách các ghi chép";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel8);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 263);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1180, 390);
            this.panel6.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.chartDisplayChildren);
            this.panel8.Controls.Add(this.label13);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(606, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(574, 390);
            this.panel8.TabIndex = 0;
            // 
            // chartDisplayChildren
            // 
            chartArea1.Name = "ChartArea1";
            this.chartDisplayChildren.ChartAreas.Add(chartArea1);
            this.chartDisplayChildren.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartDisplayChildren.Legends.Add(legend1);
            this.chartDisplayChildren.Location = new System.Drawing.Point(0, 35);
            this.chartDisplayChildren.Name = "chartDisplayChildren";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartDisplayChildren.Series.Add(series1);
            this.chartDisplayChildren.Size = new System.Drawing.Size(574, 355);
            this.chartDisplayChildren.TabIndex = 52;
            this.chartDisplayChildren.Text = "chart1";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(574, 35);
            this.label13.TabIndex = 51;
            this.label13.Text = "Biểu đồ theo danh mục (danh mục con)";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.chartDisplayParent);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(606, 390);
            this.panel7.TabIndex = 0;
            // 
            // chartDisplayParent
            // 
            chartArea2.Name = "ChartArea1";
            this.chartDisplayParent.ChartAreas.Add(chartArea2);
            this.chartDisplayParent.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartDisplayParent.Legends.Add(legend2);
            this.chartDisplayParent.Location = new System.Drawing.Point(0, 35);
            this.chartDisplayParent.Name = "chartDisplayParent";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartDisplayParent.Series.Add(series2);
            this.chartDisplayParent.Size = new System.Drawing.Size(606, 355);
            this.chartDisplayParent.TabIndex = 48;
            this.chartDisplayParent.Text = "chart1";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(606, 35);
            this.label3.TabIndex = 47;
            this.label3.Text = "Biểu đồ theo nhóm danh mục (danh mục cha)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Report
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1180, 920);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Report";
            this.Text = "Report";
            this.Load += new System.EventHandler(this.Report_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrevMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNextMonth)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.btnExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picExportReport)).EndInit();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataReport)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDisplayChildren)).EndInit();
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDisplayParent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox btnNextMonth;
        private System.Windows.Forms.PictureBox btnPrevMonth;
        private System.Windows.Forms.Label monthNext;
        private System.Windows.Forms.Label monthPrev;
        private System.Windows.Forms.Label monthNow;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel btnExport;
        private System.Windows.Forms.Label textExportReport;
        private System.Windows.Forms.PictureBox picExportReport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioYear;
        private System.Windows.Forms.RadioButton radioQuatar;
        private System.Windows.Forms.RadioButton radioMonth;
        private System.Windows.Forms.CheckBox ckIncome;
        private System.Windows.Forms.CheckBox ckExpense;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbToAmount;
        private System.Windows.Forms.TextBox tbFromAmount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton raColumn;
        private System.Windows.Forms.RadioButton raLine;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridView dataReport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDisplayChildren;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDisplayParent;
        private System.Windows.Forms.Label label3;
    }
}