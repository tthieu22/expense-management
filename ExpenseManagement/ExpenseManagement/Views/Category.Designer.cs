namespace ExpenseManagement.Views
{
    partial class Category
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Category));
            this.plAddNew = new System.Windows.Forms.Panel();
            this.btnOpenCategoryGroup = new System.Windows.Forms.LinkLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.picUpload = new System.Windows.Forms.PictureBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbGroup = new System.Windows.Forms.ComboBox();
            this.sfsdf = new System.Windows.Forms.Label();
            this.lbIncome = new System.Windows.Forms.Label();
            this.blExpense = new System.Windows.Forms.Label();
            this.plListIcon = new System.Windows.Forms.FlowLayoutPanel();
            this.sjdsjdha = new System.Windows.Forms.FlowLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pfExpense = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pfIncome = new System.Windows.Forms.FlowLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.plAddNew.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUpload)).BeginInit();
            this.sjdsjdha.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // plAddNew
            // 
            this.plAddNew.Controls.Add(this.btnOpenCategoryGroup);
            this.plAddNew.Controls.Add(this.panel3);
            this.plAddNew.Controls.Add(this.lbIncome);
            this.plAddNew.Controls.Add(this.blExpense);
            this.plAddNew.Controls.Add(this.plListIcon);
            this.plAddNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plAddNew.Location = new System.Drawing.Point(0, 0);
            this.plAddNew.Name = "plAddNew";
            this.plAddNew.Size = new System.Drawing.Size(1180, 388);
            this.plAddNew.TabIndex = 2;
            // 
            // btnOpenCategoryGroup
            // 
            this.btnOpenCategoryGroup.AutoSize = true;
            this.btnOpenCategoryGroup.Font = new System.Drawing.Font("Poppins Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenCategoryGroup.LinkColor = System.Drawing.Color.Black;
            this.btnOpenCategoryGroup.Location = new System.Drawing.Point(42, 322);
            this.btnOpenCategoryGroup.Name = "btnOpenCategoryGroup";
            this.btnOpenCategoryGroup.Size = new System.Drawing.Size(165, 22);
            this.btnOpenCategoryGroup.TabIndex = 8;
            this.btnOpenCategoryGroup.TabStop = true;
            this.btnOpenCategoryGroup.Text = "Thêm nhóm danh mục";
            this.btnOpenCategoryGroup.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnOpenCategoryGroup_LinkClicked);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnDelete);
            this.panel3.Controls.Add(this.btnUpdate);
            this.panel3.Controls.Add(this.btnAdd);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.picUpload);
            this.panel3.Controls.Add(this.tbName);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.cbGroup);
            this.panel3.Controls.Add(this.sfsdf);
            this.panel3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(40, 99);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(540, 199);
            this.panel3.TabIndex = 0;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.White;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(318, 138);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 30);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.White;
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Location = new System.Drawing.Point(232, 138);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 30);
            this.btnUpdate.TabIndex = 12;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.White;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(148, 138);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 30);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "New";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(423, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 26);
            this.label4.TabIndex = 10;
            this.label4.Text = "UpLoad icon";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = false;
            // 
            // picUpload
            // 
            this.picUpload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picUpload.Image = ((System.Drawing.Image)(resources.GetObject("picUpload.Image")));
            this.picUpload.Location = new System.Drawing.Point(426, 45);
            this.picUpload.Name = "picUpload";
            this.picUpload.Size = new System.Drawing.Size(96, 90);
            this.picUpload.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picUpload.TabIndex = 9;
            this.picUpload.TabStop = false;
            this.picUpload.Visible = false;
            this.picUpload.Click += new System.EventHandler(this.picUpload_Click);
            // 
            // tbName
            // 
            this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbName.Location = new System.Drawing.Point(148, 37);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(250, 26);
            this.tbName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(60, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 26);
            this.label2.TabIndex = 8;
            this.label2.Text = "Group";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cbGroup
            // 
            this.cbGroup.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbGroup.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGroup.FormattingEnabled = true;
            this.cbGroup.Location = new System.Drawing.Point(148, 91);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Size = new System.Drawing.Size(250, 26);
            this.cbGroup.TabIndex = 4;
            this.cbGroup.SelectedIndexChanged += new System.EventHandler(this.cbGroup_SelectedIndexChanged);
            // 
            // sfsdf
            // 
            this.sfsdf.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sfsdf.Location = new System.Drawing.Point(60, 37);
            this.sfsdf.Name = "sfsdf";
            this.sfsdf.Size = new System.Drawing.Size(72, 26);
            this.sfsdf.TabIndex = 7;
            this.sfsdf.Text = "Name";
            this.sfsdf.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lbIncome
            // 
            this.lbIncome.BackColor = System.Drawing.Color.Bisque;
            this.lbIncome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbIncome.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIncome.ForeColor = System.Drawing.Color.Gray;
            this.lbIncome.Location = new System.Drawing.Point(133, 64);
            this.lbIncome.Name = "lbIncome";
            this.lbIncome.Size = new System.Drawing.Size(90, 34);
            this.lbIncome.TabIndex = 6;
            this.lbIncome.Text = "Thu nhập";
            this.lbIncome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbIncome.Click += new System.EventHandler(this.lbIncome_Click);
            // 
            // blExpense
            // 
            this.blExpense.BackColor = System.Drawing.Color.LightSalmon;
            this.blExpense.Cursor = System.Windows.Forms.Cursors.Hand;
            this.blExpense.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blExpense.ForeColor = System.Drawing.Color.Black;
            this.blExpense.Location = new System.Drawing.Point(42, 64);
            this.blExpense.Name = "blExpense";
            this.blExpense.Size = new System.Drawing.Size(90, 34);
            this.blExpense.TabIndex = 5;
            this.blExpense.Text = "Chi tiêu";
            this.blExpense.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.blExpense.Click += new System.EventHandler(this.blExpense_Click);
            // 
            // plListIcon
            // 
            this.plListIcon.AutoScroll = true;
            this.plListIcon.BackColor = System.Drawing.SystemColors.HighlightText;
            this.plListIcon.Dock = System.Windows.Forms.DockStyle.Right;
            this.plListIcon.Location = new System.Drawing.Point(650, 0);
            this.plListIcon.Name = "plListIcon";
            this.plListIcon.Size = new System.Drawing.Size(530, 388);
            this.plListIcon.TabIndex = 2;
            // 
            // sjdsjdha
            // 
            this.sjdsjdha.BackColor = System.Drawing.Color.Snow;
            this.sjdsjdha.Controls.Add(this.panel4);
            this.sjdsjdha.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sjdsjdha.Location = new System.Drawing.Point(0, 388);
            this.sjdsjdha.Margin = new System.Windows.Forms.Padding(10);
            this.sjdsjdha.Name = "sjdsjdha";
            this.sjdsjdha.Padding = new System.Windows.Forms.Padding(10);
            this.sjdsjdha.Size = new System.Drawing.Size(1180, 532);
            this.sjdsjdha.TabIndex = 1;
            this.sjdsjdha.WrapContents = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Location = new System.Drawing.Point(13, 13);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1155, 507);
            this.panel4.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.pfExpense);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(567, 507);
            this.panel5.TabIndex = 8;
            // 
            // pfExpense
            // 
            this.pfExpense.AutoScroll = true;
            this.pfExpense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pfExpense.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pfExpense.Location = new System.Drawing.Point(0, 31);
            this.pfExpense.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.pfExpense.Name = "pfExpense";
            this.pfExpense.Padding = new System.Windows.Forms.Padding(10);
            this.pfExpense.Size = new System.Drawing.Size(567, 476);
            this.pfExpense.TabIndex = 8;
            this.pfExpense.WrapContents = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LightCoral;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label3.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label3.Size = new System.Drawing.Size(567, 31);
            this.label3.TabIndex = 6;
            this.label3.Text = "Danh mục chi tiêu";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pfIncome);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(578, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(577, 507);
            this.panel2.TabIndex = 7;
            // 
            // pfIncome
            // 
            this.pfIncome.AutoScroll = true;
            this.pfIncome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pfIncome.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pfIncome.Location = new System.Drawing.Point(0, 31);
            this.pfIncome.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.pfIncome.Name = "pfIncome";
            this.pfIncome.Padding = new System.Windows.Forms.Padding(10);
            this.pfIncome.Size = new System.Drawing.Size(577, 476);
            this.pfIncome.TabIndex = 7;
            this.pfIncome.WrapContents = false;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.LightCoral;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label10.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label10.Size = new System.Drawing.Size(577, 31);
            this.label10.TabIndex = 6;
            this.label10.Text = "Danh mục thu nhập";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Category
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1180, 920);
            this.Controls.Add(this.plAddNew);
            this.Controls.Add(this.sjdsjdha);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Category";
            this.Text = "Category";
            this.Load += new System.EventHandler(this.Category_Load);
            this.plAddNew.ResumeLayout(false);
            this.plAddNew.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUpload)).EndInit();
            this.sjdsjdha.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel plAddNew;
        private System.Windows.Forms.ComboBox cbGroup;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lbIncome;
        private System.Windows.Forms.Label blExpense;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label sfsdf;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.FlowLayoutPanel sjdsjdha;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox picUpload;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel pfIncome;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.FlowLayoutPanel plListIcon;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel pfExpense;
        private System.Windows.Forms.LinkLabel btnOpenCategoryGroup;
    }
}