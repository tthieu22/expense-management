namespace ExpenseManagement.Views
{
    partial class ChatMessageControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblSender;
        private System.Windows.Forms.Label lblTime;

        /// <summary> 
        /// Giải phóng tài nguyên đang sử dụng.
        /// </summary>
        /// <param name="disposing">true nếu cần giải phóng tài nguyên.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary> 
        /// Khởi tạo giao diện của Control.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSender = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSender
            // 
            this.lblSender.AutoSize = true;
            this.lblSender.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSender.Font = new System.Drawing.Font("Poppins Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSender.Location = new System.Drawing.Point(0, 0);
            this.lblSender.Name = "lblSender";
            this.lblSender.Size = new System.Drawing.Size(109, 23);
            this.lblSender.TabIndex = 0;
            this.lblSender.Text = "Tên người gửi";
            this.lblSender.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTime.Font = new System.Drawing.Font("Poppins Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.Black;
            this.lblTime.Location = new System.Drawing.Point(1068, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTime.Size = new System.Drawing.Size(82, 23);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "12:34 PM";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSender);
            this.panel1.Controls.Add(this.lblTime);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1150, 26);
            this.panel1.TabIndex = 3;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(0, 26);
            this.lblMessage.MaximumSize = new System.Drawing.Size(750, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(87, 23);
            this.lblMessage.TabIndex = 4;
            this.lblMessage.Text = "Nội dung tin";
            // 
            // ChatMessageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(850, 0);
            this.Name = "ChatMessageControl";
            this.Size = new System.Drawing.Size(1150, 145);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMessage;
    }
}
