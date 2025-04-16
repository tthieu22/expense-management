namespace ExpenseManagement.Views
{
    partial class BudgetCategoryItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BudgetCategoryItem));
            this.panel1 = new System.Windows.Forms.Panel();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbRemaining = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.picWarning = new System.Windows.Forms.PictureBox();
            this.lbStatus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbTotal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbExpense = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWarning)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(14, 13);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 160);
            this.panel1.TabIndex = 0;
            // 
            // chart
            // 
            this.chart.BackColor = System.Drawing.Color.LightSteelBlue;
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(6, 11);
            this.chart.Name = "chart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart.Series.Add(series1);
            this.chart.Size = new System.Drawing.Size(140, 140);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbRemaining);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.lbStatus);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.lbTotal);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lbExpense);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lbName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(172, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(364, 160);
            this.panel2.TabIndex = 1;
            // 
            // lbRemaining
            // 
            this.lbRemaining.AutoSize = true;
            this.lbRemaining.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRemaining.ForeColor = System.Drawing.Color.Green;
            this.lbRemaining.Location = new System.Drawing.Point(72, 73);
            this.lbRemaining.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbRemaining.Name = "lbRemaining";
            this.lbRemaining.Size = new System.Drawing.Size(82, 19);
            this.lbRemaining.TabIndex = 11;
            this.lbRemaining.Text = "2.000.000";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.picWarning);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(334, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(30, 160);
            this.panel3.TabIndex = 19;
            // 
            // picWarning
            // 
            this.picWarning.Dock = System.Windows.Forms.DockStyle.Top;
            this.picWarning.Image = ((System.Drawing.Image)(resources.GetObject("picWarning.Image")));
            this.picWarning.Location = new System.Drawing.Point(0, 0);
            this.picWarning.Name = "picWarning";
            this.picWarning.Size = new System.Drawing.Size(30, 51);
            this.picWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picWarning.TabIndex = 20;
            this.picWarning.TabStop = false;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.Location = new System.Drawing.Point(99, 130);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(99, 15);
            this.lbStatus.TabIndex = 18;
            this.lbStatus.Text = "Đúng ngân sách";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(17, 130);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 15);
            this.label6.TabIndex = 17;
            this.label6.Text = "Trạng thái:";
            // 
            // lbTotal
            // 
            this.lbTotal.AutoSize = true;
            this.lbTotal.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotal.Location = new System.Drawing.Point(98, 38);
            this.lbTotal.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(82, 19);
            this.lbTotal.TabIndex = 16;
            this.lbTotal.Text = "3.000.000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 41);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "Ngân sách: ";
            // 
            // lbExpense
            // 
            this.lbExpense.AutoSize = true;
            this.lbExpense.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExpense.Location = new System.Drawing.Point(67, 96);
            this.lbExpense.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbExpense.Name = "lbExpense";
            this.lbExpense.Size = new System.Drawing.Size(82, 19);
            this.lbExpense.TabIndex = 14;
            this.lbExpense.Text = "1.000.000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 99);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "Đã chi:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 76);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "Còn lại: ";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.Location = new System.Drawing.Point(14, 7);
            this.lbName.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(69, 19);
            this.lbName.TabIndex = 10;
            this.lbName.Text = "Ăn uống";
            // 
            // BudgetCategoryItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(10);
            this.Name = "BudgetCategoryItem";
            this.Padding = new System.Windows.Forms.Padding(14, 13, 14, 13);
            this.Size = new System.Drawing.Size(550, 186);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picWarning)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbTotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbExpense;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbRemaining;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox picWarning;
    }
}
