namespace ExpenseManagement.Views
{
    partial class CategoryTemplate
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
            this.icIcon = new FontAwesome.Sharp.IconButton();
            this.lbName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // icIcon
            // 
            this.icIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.icIcon.IconChar = FontAwesome.Sharp.IconChar.None;
            this.icIcon.IconColor = System.Drawing.Color.Black;
            this.icIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.icIcon.Location = new System.Drawing.Point(5, 5);
            this.icIcon.Name = "icIcon";
            this.icIcon.Size = new System.Drawing.Size(40, 40);
            this.icIcon.TabIndex = 0;
            this.icIcon.UseVisualStyleBackColor = true;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbName.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.Location = new System.Drawing.Point(45, 5);
            this.lbName.Name = "lbName";
            this.lbName.Padding = new System.Windows.Forms.Padding(10);
            this.lbName.Size = new System.Drawing.Size(58, 36);
            this.lbName.TabIndex = 1;
            this.lbName.Text = "label1";
            this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CategoryTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.icIcon);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "CategoryTemplate";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(550, 50);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton icIcon;
        private System.Windows.Forms.Label lbName;
    }
}
