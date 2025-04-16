namespace ExpenseManagement.Views
{
    partial class CategoryItem
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.lbName = new System.Windows.Forms.Label();
            this.ckSelect = new System.Windows.Forms.CheckBox();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.iconpicbox = new FontAwesome.Sharp.IconPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconpicbox)).BeginInit();
            this.SuspendLayout();
            // 
            // pbIcon
            // 
            this.pbIcon.Location = new System.Drawing.Point(10, 10);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(40, 40);
            this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbIcon.TabIndex = 0;
            this.pbIcon.TabStop = false;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lbName.Location = new System.Drawing.Point(60, 20);
            this.lbName.MaximumSize = new System.Drawing.Size(300, 0);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(127, 19);
            this.lbName.TabIndex = 2;
            this.lbName.Text = "Category Name";
            // 
            // ckSelect
            // 
            this.ckSelect.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckSelect.Dock = System.Windows.Forms.DockStyle.Right;
            this.ckSelect.Location = new System.Drawing.Point(300, 0);
            this.ckSelect.Name = "ckSelect";
            this.ckSelect.Size = new System.Drawing.Size(50, 60);
            this.ckSelect.TabIndex = 3;
            this.ckSelect.UseVisualStyleBackColor = true;
            this.ckSelect.CheckedChanged += new System.EventHandler(this.ckSelect_CheckedChanged);
            // 
            // pnlContainer
            // 
            this.pnlContainer.AutoSize = true;
            this.pnlContainer.BackColor = System.Drawing.Color.White;
            this.pnlContainer.Controls.Add(this.iconpicbox);
            this.pnlContainer.Controls.Add(this.pbIcon);
            this.pnlContainer.Controls.Add(this.lbName);
            this.pnlContainer.Controls.Add(this.ckSelect);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(350, 60);
            this.pnlContainer.TabIndex = 4;
            // 
            // iconpicbox
            // 
            this.iconpicbox.BackColor = System.Drawing.Color.White;
            this.iconpicbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.iconpicbox.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconpicbox.IconColor = System.Drawing.SystemColors.ControlText;
            this.iconpicbox.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconpicbox.IconSize = 40;
            this.iconpicbox.Location = new System.Drawing.Point(10, 10);
            this.iconpicbox.Name = "iconpicbox";
            this.iconpicbox.Size = new System.Drawing.Size(40, 40);
            this.iconpicbox.TabIndex = 4;
            this.iconpicbox.TabStop = false;
            // 
            // CategoryItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnlContainer);
            this.Name = "CategoryItem";
            this.Size = new System.Drawing.Size(350, 60);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconpicbox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbIcon;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.CheckBox ckSelect;
        private System.Windows.Forms.Panel pnlContainer;
        private FontAwesome.Sharp.IconPictureBox iconpicbox;
    }
}
