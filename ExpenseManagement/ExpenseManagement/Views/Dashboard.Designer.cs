namespace ExpenseManagement.Views
{
    partial class Dashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSend = new System.Windows.Forms.PictureBox();
            this.txtUserMessage = new System.Windows.Forms.TextBox();
            this.flpMessages = new System.Windows.Forms.FlowLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbStatus = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSend)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MistyRose;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 857);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1180, 63);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnSend);
            this.panel4.Controls.Add(this.txtUserMessage);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(20);
            this.panel4.Size = new System.Drawing.Size(1180, 63);
            this.panel4.TabIndex = 6;
            // 
            // btnSend
            // 
            this.btnSend.Image = ((System.Drawing.Image)(resources.GetObject("btnSend.Image")));
            this.btnSend.Location = new System.Drawing.Point(1103, 12);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(60, 37);
            this.btnSend.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnSend.TabIndex = 2;
            this.btnSend.TabStop = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            this.btnSend.MouseHover += new System.EventHandler(this.btnSend_MouseHover);
            // 
            // txtUserMessage
            // 
            this.txtUserMessage.BackColor = System.Drawing.Color.White;
            this.txtUserMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserMessage.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserMessage.Location = new System.Drawing.Point(23, 12);
            this.txtUserMessage.Multiline = true;
            this.txtUserMessage.Name = "txtUserMessage";
            this.txtUserMessage.Size = new System.Drawing.Size(1071, 37);
            this.txtUserMessage.TabIndex = 0;
            this.txtUserMessage.TextChanged += new System.EventHandler(this.txtUserMessage_TextChanged);
            this.txtUserMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserMessage_KeyDown);
            // 
            // flpMessages
            // 
            this.flpMessages.AutoScroll = true;
            this.flpMessages.BackColor = System.Drawing.Color.White;
            this.flpMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMessages.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flpMessages.Location = new System.Drawing.Point(0, 30);
            this.flpMessages.Name = "flpMessages";
            this.flpMessages.Size = new System.Drawing.Size(1180, 827);
            this.flpMessages.TabIndex = 4;
            this.flpMessages.WrapContents = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Snow;
            this.panel3.Controls.Add(this.lbStatus);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(1180, 30);
            this.panel3.TabIndex = 3;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbStatus.Font = new System.Drawing.Font("Poppins", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.Location = new System.Drawing.Point(5, 5);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lbStatus.Size = new System.Drawing.Size(57, 19);
            this.lbStatus.TabIndex = 0;
            this.lbStatus.Text = "Chờ xíu ";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1180, 920);
            this.Controls.Add(this.flpMessages);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSend)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtUserMessage;
        private System.Windows.Forms.PictureBox btnSend;
        private System.Windows.Forms.FlowLayoutPanel flpMessages;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lbStatus;
    }
}