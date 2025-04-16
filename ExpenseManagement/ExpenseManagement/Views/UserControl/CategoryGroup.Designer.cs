namespace ExpenseManagement.Views
{
    partial class CategoryGroup
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbNameGroup = new System.Windows.Forms.Label();
            this.iconGroup = new FontAwesome.Sharp.IconButton();
            this.flListCategory = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MistyRose;
            this.panel1.Controls.Add(this.lbNameGroup);
            this.panel1.Controls.Add(this.iconGroup);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(552, 50);
            this.panel1.TabIndex = 1;
            // 
            // lbNameGroup
            // 
            this.lbNameGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNameGroup.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNameGroup.Location = new System.Drawing.Point(45, 5);
            this.lbNameGroup.Name = "lbNameGroup";
            this.lbNameGroup.Padding = new System.Windows.Forms.Padding(10);
            this.lbNameGroup.Size = new System.Drawing.Size(502, 40);
            this.lbNameGroup.TabIndex = 4;
            this.lbNameGroup.Text = "Chi tiêu sinh hoạt";
            this.lbNameGroup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // iconGroup
            // 
            this.iconGroup.Dock = System.Windows.Forms.DockStyle.Left;
            this.iconGroup.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconGroup.IconColor = System.Drawing.Color.Black;
            this.iconGroup.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconGroup.Location = new System.Drawing.Point(5, 5);
            this.iconGroup.Name = "iconGroup";
            this.iconGroup.Size = new System.Drawing.Size(40, 40);
            this.iconGroup.TabIndex = 0;
            this.iconGroup.UseVisualStyleBackColor = true;
            // 
            // flListCategory
            // 
            this.flListCategory.AutoSize = true;
            this.flListCategory.BackColor = System.Drawing.Color.Snow;
            this.flListCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flListCategory.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flListCategory.Location = new System.Drawing.Point(10, 60);
            this.flListCategory.Name = "flListCategory";
            this.flListCategory.Padding = new System.Windows.Forms.Padding(5);
            this.flListCategory.Size = new System.Drawing.Size(552, 475);
            this.flListCategory.TabIndex = 2;
            this.flListCategory.WrapContents = false;
            // 
            // CategoryGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.flListCategory);
            this.Controls.Add(this.panel1);
            this.Name = "CategoryGroup";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(572, 545);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flListCategory;
        private FontAwesome.Sharp.IconButton iconGroup;
        private System.Windows.Forms.Label lbNameGroup;
    }
}
