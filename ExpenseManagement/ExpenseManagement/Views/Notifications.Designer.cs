namespace ExpenseManagement.Views
{
    partial class Notifications
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
            this.btnDeleteAll = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanelNotifications = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.AutoSize = true;
            this.btnDeleteAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeleteAll.Font = new System.Drawing.Font("Poppins Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteAll.LinkColor = System.Drawing.Color.Black;
            this.btnDeleteAll.Location = new System.Drawing.Point(1074, 0);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Padding = new System.Windows.Forms.Padding(10);
            this.btnDeleteAll.Size = new System.Drawing.Size(106, 43);
            this.btnDeleteAll.TabIndex = 2;
            this.btnDeleteAll.TabStop = true;
            this.btnDeleteAll.Text = "Xóa tất cả";
            this.btnDeleteAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnDeleteAll_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnDeleteAll);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1180, 43);
            this.panel1.TabIndex = 1;
            // 
            // flowLayoutPanelNotifications
            // 
            this.flowLayoutPanelNotifications.AutoScroll = true;
            this.flowLayoutPanelNotifications.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanelNotifications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelNotifications.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelNotifications.Location = new System.Drawing.Point(0, 43);
            this.flowLayoutPanelNotifications.Name = "flowLayoutPanelNotifications";
            this.flowLayoutPanelNotifications.Size = new System.Drawing.Size(1180, 877);
            this.flowLayoutPanelNotifications.TabIndex = 2;
            this.flowLayoutPanelNotifications.WrapContents = false;
            // 
            // Notifications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 920);
            this.Controls.Add(this.flowLayoutPanelNotifications);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Notifications";
            this.Text = "Notifications";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Notifications_FormClosing);
            this.Load += new System.EventHandler(this.Notifications_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel btnDeleteAll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelNotifications;
    }
}