namespace ExpenseManagement.Views
{
    partial class AddNotifications
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
            this.label4 = new System.Windows.Forms.Label();
            this.dataNoti = new System.Windows.Forms.DataGridView();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDeleteCat = new System.Windows.Forms.Label();
            this.btnUpdateCat = new System.Windows.Forms.Label();
            this.btnAddCat = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataNoti)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.LightCoral;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label4.Font = new System.Drawing.Font("Poppins Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1180, 41);
            this.label4.TabIndex = 5;
            this.label4.Text = "Quản lý thông báo";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dataNoti
            // 
            this.dataNoti.BackgroundColor = System.Drawing.Color.White;
            this.dataNoti.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataNoti.Location = new System.Drawing.Point(98, 373);
            this.dataNoti.Name = "dataNoti";
            this.dataNoti.Size = new System.Drawing.Size(930, 266);
            this.dataNoti.TabIndex = 6;
            this.dataNoti.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataNoti_CellClick);
            this.dataNoti.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataNoti_CellContentClick);
            // 
            // tbMessage
            // 
            this.tbMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMessage.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMessage.Location = new System.Drawing.Point(98, 132);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(897, 86);
            this.tbMessage.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Poppins Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(94, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 23);
            this.label3.TabIndex = 9;
            this.label3.Text = "Message";
            // 
            // btnDeleteCat
            // 
            this.btnDeleteCat.BackColor = System.Drawing.Color.LightCoral;
            this.btnDeleteCat.Font = new System.Drawing.Font("Poppins Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteCat.Location = new System.Drawing.Point(255, 258);
            this.btnDeleteCat.Name = "btnDeleteCat";
            this.btnDeleteCat.Size = new System.Drawing.Size(73, 38);
            this.btnDeleteCat.TabIndex = 13;
            this.btnDeleteCat.Text = "Xóa";
            this.btnDeleteCat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDeleteCat.Click += new System.EventHandler(this.btnDeleteCat_Click);
            // 
            // btnUpdateCat
            // 
            this.btnUpdateCat.BackColor = System.Drawing.Color.LightCoral;
            this.btnUpdateCat.Font = new System.Drawing.Font("Poppins Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateCat.Location = new System.Drawing.Point(176, 258);
            this.btnUpdateCat.Name = "btnUpdateCat";
            this.btnUpdateCat.Size = new System.Drawing.Size(73, 38);
            this.btnUpdateCat.TabIndex = 12;
            this.btnUpdateCat.Text = "Sửa";
            this.btnUpdateCat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUpdateCat.Click += new System.EventHandler(this.btnUpdateCat_Click);
            // 
            // btnAddCat
            // 
            this.btnAddCat.BackColor = System.Drawing.Color.LightCoral;
            this.btnAddCat.Font = new System.Drawing.Font("Poppins Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCat.Location = new System.Drawing.Point(97, 258);
            this.btnAddCat.Name = "btnAddCat";
            this.btnAddCat.Size = new System.Drawing.Size(73, 38);
            this.btnAddCat.TabIndex = 11;
            this.btnAddCat.Text = "Thêm";
            this.btnAddCat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddCat.Click += new System.EventHandler(this.btnAddCat_Click);
            // 
            // AddNotifications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1180, 920);
            this.Controls.Add(this.btnDeleteCat);
            this.Controls.Add(this.btnUpdateCat);
            this.Controls.Add(this.btnAddCat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.dataNoti);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddNotifications";
            this.Text = "AddNotifications";
            ((System.ComponentModel.ISupportInitialize)(this.dataNoti)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataNoti;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label btnDeleteCat;
        private System.Windows.Forms.Label btnUpdateCat;
        private System.Windows.Forms.Label btnAddCat;
    }
}