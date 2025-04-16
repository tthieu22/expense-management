namespace ExpenseManagement.Views
{
    partial class ExpenseFinish
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpenseFinish));
            this.panel3 = new System.Windows.Forms.Panel();
            this.textEdit = new System.Windows.Forms.Label();
            this.picEdit = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textDelete = new System.Windows.Forms.Label();
            this.picDelete = new System.Windows.Forms.PictureBox();
            this.plEdit = new System.Windows.Forms.Panel();
            this.pnDesc = new System.Windows.Forms.Panel();
            this.lbDesc = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbCategory = new System.Windows.Forms.Label();
            this.iconChar = new FontAwesome.Sharp.IconButton();
            this.lbTotal = new System.Windows.Forms.Label();
            this.lbDate = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDelete)).BeginInit();
            this.plEdit.SuspendLayout();
            this.pnDesc.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textEdit);
            this.panel3.Controls.Add(this.picEdit);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.panel3.Size = new System.Drawing.Size(95, 0);
            this.panel3.TabIndex = 0;
            // 
            // textEdit
            // 
            this.textEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEdit.Font = new System.Drawing.Font("Poppins SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit.Location = new System.Drawing.Point(40, 5);
            this.textEdit.Name = "textEdit";
            this.textEdit.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.textEdit.Size = new System.Drawing.Size(55, 0);
            this.textEdit.TabIndex = 1;
            this.textEdit.Text = "Edit";
            this.textEdit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.textEdit.Click += new System.EventHandler(this.textEdit_Click);
            // 
            // picEdit
            // 
            this.picEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.picEdit.Image = ((System.Drawing.Image)(resources.GetObject("picEdit.Image")));
            this.picEdit.Location = new System.Drawing.Point(0, 5);
            this.picEdit.Name = "picEdit";
            this.picEdit.Size = new System.Drawing.Size(40, 0);
            this.picEdit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picEdit.TabIndex = 0;
            this.picEdit.TabStop = false;
            this.picEdit.Click += new System.EventHandler(this.picEdit_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textDelete);
            this.panel2.Controls.Add(this.picDelete);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(95, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.panel2.Size = new System.Drawing.Size(114, 0);
            this.panel2.TabIndex = 1;
            // 
            // textDelete
            // 
            this.textDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textDelete.Font = new System.Drawing.Font("Poppins SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDelete.Location = new System.Drawing.Point(40, 5);
            this.textDelete.Name = "textDelete";
            this.textDelete.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.textDelete.Size = new System.Drawing.Size(74, 0);
            this.textDelete.TabIndex = 3;
            this.textDelete.Text = "Delete";
            this.textDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.textDelete.Click += new System.EventHandler(this.textDelete_Click);
            // 
            // picDelete
            // 
            this.picDelete.Dock = System.Windows.Forms.DockStyle.Left;
            this.picDelete.Image = ((System.Drawing.Image)(resources.GetObject("picDelete.Image")));
            this.picDelete.Location = new System.Drawing.Point(0, 5);
            this.picDelete.Name = "picDelete";
            this.picDelete.Size = new System.Drawing.Size(40, 0);
            this.picDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDelete.TabIndex = 2;
            this.picDelete.TabStop = false;
            this.picDelete.Click += new System.EventHandler(this.picDelete_Click);
            // 
            // plEdit
            // 
            this.plEdit.BackColor = System.Drawing.Color.LightSteelBlue;
            this.plEdit.Controls.Add(this.panel2);
            this.plEdit.Controls.Add(this.panel3);
            this.plEdit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plEdit.Location = new System.Drawing.Point(10, 86);
            this.plEdit.Name = "plEdit";
            this.plEdit.Size = new System.Drawing.Size(480, 0);
            this.plEdit.TabIndex = 18;
            this.plEdit.Visible = false;
            // 
            // pnDesc
            // 
            this.pnDesc.Controls.Add(this.lbDesc);
            this.pnDesc.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pnDesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnDesc.Location = new System.Drawing.Point(10, 10);
            this.pnDesc.Name = "pnDesc";
            this.pnDesc.Size = new System.Drawing.Size(480, 0);
            this.pnDesc.TabIndex = 19;
            // 
            // lbDesc
            // 
            this.lbDesc.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbDesc.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDesc.Location = new System.Drawing.Point(0, 0);
            this.lbDesc.Name = "lbDesc";
            this.lbDesc.Size = new System.Drawing.Size(155, 0);
            this.lbDesc.TabIndex = 14;
            this.lbDesc.Text = "Mua đồ cho người yêu ";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbCategory);
            this.panel1.Controls.Add(this.iconChar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5, 5, 0, 5);
            this.panel1.Size = new System.Drawing.Size(480, 30);
            this.panel1.TabIndex = 20;
            // 
            // lbCategory
            // 
            this.lbCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCategory.Font = new System.Drawing.Font("Poppins SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCategory.Location = new System.Drawing.Point(25, 5);
            this.lbCategory.Name = "lbCategory";
            this.lbCategory.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbCategory.Size = new System.Drawing.Size(455, 20);
            this.lbCategory.TabIndex = 10;
            this.lbCategory.Text = "Ăn uống";
            this.lbCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // iconChar
            // 
            this.iconChar.BackColor = System.Drawing.Color.AliceBlue;
            this.iconChar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.iconChar.Dock = System.Windows.Forms.DockStyle.Left;
            this.iconChar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconChar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconChar.IconColor = System.Drawing.Color.Black;
            this.iconChar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconChar.Location = new System.Drawing.Point(5, 5);
            this.iconChar.Name = "iconChar";
            this.iconChar.Size = new System.Drawing.Size(20, 20);
            this.iconChar.TabIndex = 0;
            this.iconChar.UseVisualStyleBackColor = false;
            this.iconChar.UseWaitCursor = true;
            // 
            // lbTotal
            // 
            this.lbTotal.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTotal.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotal.Location = new System.Drawing.Point(10, 40);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(480, 23);
            this.lbTotal.TabIndex = 21;
            this.lbTotal.Text = "-1.000.000đ";
            this.lbTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbDate
            // 
            this.lbDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbDate.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDate.Location = new System.Drawing.Point(10, 63);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(480, 23);
            this.lbDate.TabIndex = 22;
            this.lbDate.Text = "Đã chi vào ngày 24/3/2025 lúc 15:34";
            this.lbDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ExpenseFinish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Controls.Add(this.lbDate);
            this.Controls.Add(this.lbTotal);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.plEdit);
            this.Controls.Add(this.pnDesc);
            this.MinimumSize = new System.Drawing.Size(500, 0);
            this.Name = "ExpenseFinish";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(500, 96);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picEdit)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDelete)).EndInit();
            this.plEdit.ResumeLayout(false);
            this.pnDesc.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label textEdit;
        private System.Windows.Forms.PictureBox picEdit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label textDelete;
        private System.Windows.Forms.PictureBox picDelete;
        private System.Windows.Forms.Panel plEdit;
        private System.Windows.Forms.Panel pnDesc;
        private System.Windows.Forms.Label lbDesc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbCategory;
        private FontAwesome.Sharp.IconButton iconChar;
        private System.Windows.Forms.Label lbTotal;
        private System.Windows.Forms.Label lbDate;
    }
}
