namespace ExpenseManagement.Views
{
    partial class Setting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setting));
            this.plAccount = new System.Windows.Forms.Panel();
            this.tAccount = new System.Windows.Forms.Label();
            this.btnAccount = new System.Windows.Forms.PictureBox();
            this.plReport = new System.Windows.Forms.Panel();
            this.tReport = new System.Windows.Forms.Label();
            this.btnReport = new System.Windows.Forms.PictureBox();
            this.pnNoti = new System.Windows.Forms.Panel();
            this.tNoti = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.plAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAccount)).BeginInit();
            this.plReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnReport)).BeginInit();
            this.pnNoti.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // plAccount
            // 
            this.plAccount.BackColor = System.Drawing.Color.LightCyan;
            this.plAccount.Controls.Add(this.tAccount);
            this.plAccount.Controls.Add(this.btnAccount);
            this.plAccount.Location = new System.Drawing.Point(34, 76);
            this.plAccount.Name = "plAccount";
            this.plAccount.Size = new System.Drawing.Size(468, 170);
            this.plAccount.TabIndex = 11;
            // 
            // tAccount
            // 
            this.tAccount.Dock = System.Windows.Forms.DockStyle.Right;
            this.tAccount.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tAccount.Location = new System.Drawing.Point(67, 0);
            this.tAccount.Name = "tAccount";
            this.tAccount.Size = new System.Drawing.Size(401, 170);
            this.tAccount.TabIndex = 10;
            this.tAccount.Text = "Quản lý người dùng";
            this.tAccount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tAccount.Click += new System.EventHandler(this.tAccount_Click);
            // 
            // btnAccount
            // 
            this.btnAccount.Image = ((System.Drawing.Image)(resources.GetObject("btnAccount.Image")));
            this.btnAccount.Location = new System.Drawing.Point(27, 59);
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.Size = new System.Drawing.Size(40, 40);
            this.btnAccount.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnAccount.TabIndex = 9;
            this.btnAccount.TabStop = false;
            // 
            // plReport
            // 
            this.plReport.BackColor = System.Drawing.Color.MistyRose;
            this.plReport.Controls.Add(this.tReport);
            this.plReport.Controls.Add(this.btnReport);
            this.plReport.Location = new System.Drawing.Point(34, 305);
            this.plReport.Name = "plReport";
            this.plReport.Size = new System.Drawing.Size(468, 170);
            this.plReport.TabIndex = 12;
            this.plReport.Visible = false;
            // 
            // tReport
            // 
            this.tReport.Dock = System.Windows.Forms.DockStyle.Right;
            this.tReport.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tReport.Location = new System.Drawing.Point(67, 0);
            this.tReport.Name = "tReport";
            this.tReport.Size = new System.Drawing.Size(401, 170);
            this.tReport.TabIndex = 7;
            this.tReport.Text = "Báo cáo tổng quan";
            this.tReport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tReport.Click += new System.EventHandler(this.tReport_Click);
            // 
            // btnReport
            // 
            this.btnReport.Image = ((System.Drawing.Image)(resources.GetObject("btnReport.Image")));
            this.btnReport.Location = new System.Drawing.Point(27, 68);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(40, 40);
            this.btnReport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnReport.TabIndex = 6;
            this.btnReport.TabStop = false;
            // 
            // pnNoti
            // 
            this.pnNoti.BackColor = System.Drawing.Color.PaleGreen;
            this.pnNoti.Controls.Add(this.tNoti);
            this.pnNoti.Controls.Add(this.pictureBox1);
            this.pnNoti.Location = new System.Drawing.Point(655, 76);
            this.pnNoti.Name = "pnNoti";
            this.pnNoti.Size = new System.Drawing.Size(468, 170);
            this.pnNoti.TabIndex = 13;
            // 
            // tNoti
            // 
            this.tNoti.Dock = System.Windows.Forms.DockStyle.Right;
            this.tNoti.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tNoti.Location = new System.Drawing.Point(67, 0);
            this.tNoti.Name = "tNoti";
            this.tNoti.Size = new System.Drawing.Size(401, 170);
            this.tNoti.TabIndex = 7;
            this.tNoti.Text = "Quản lý thông báo";
            this.tNoti.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tNoti.Click += new System.EventHandler(this.tNoti_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(27, 68);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trang quản lý ";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightCoral;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1180, 50);
            this.panel1.TabIndex = 10;
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1180, 920);
            this.Controls.Add(this.pnNoti);
            this.Controls.Add(this.plReport);
            this.Controls.Add(this.plAccount);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Setting";
            this.Text = "Setting";
            this.plAccount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnAccount)).EndInit();
            this.plReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnReport)).EndInit();
            this.pnNoti.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox btnAccount;
        private System.Windows.Forms.PictureBox btnReport;
        private System.Windows.Forms.Panel plAccount;
        private System.Windows.Forms.Panel plReport;
        private System.Windows.Forms.Panel pnNoti;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label tAccount;
        private System.Windows.Forms.Label tReport;
        private System.Windows.Forms.Label tNoti;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
    }
}