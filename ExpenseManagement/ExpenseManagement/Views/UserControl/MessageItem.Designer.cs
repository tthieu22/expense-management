namespace ExpenseManagement.Views
{
    partial class MessageItem
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
            this.lblText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.BackColor = System.Drawing.Color.PeachPuff;
            this.lblText.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblText.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblText.Location = new System.Drawing.Point(10, 5);
            this.lblText.Margin = new System.Windows.Forms.Padding(0);
            this.lblText.MaximumSize = new System.Drawing.Size(300, 0);
            this.lblText.Name = "lblText";
            this.lblText.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.lblText.Size = new System.Drawing.Size(203, 33);
            this.lblText.TabIndex = 1;
            this.lblText.Text = "Tạo ngân sách tổng nhanh";
            // 
            // MessageItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.lblText);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MessageItem";
            this.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.Size = new System.Drawing.Size(223, 43);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblText;
    }
}
