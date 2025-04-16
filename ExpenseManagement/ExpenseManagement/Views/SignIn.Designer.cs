namespace ExpenseManagement.Views
{
    partial class SignIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignIn));
            this.chkRememberMe = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.plLogin = new System.Windows.Forms.Panel();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSignIn = new System.Windows.Forms.Label();
            this.btnShowSignUp = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.showInfor = new System.Windows.Forms.Panel();
            this.dtpkDateofBirth = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.btnSignUp = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnShowLogin = new System.Windows.Forms.Label();
            this.txtPasswordRegister = new System.Windows.Forms.TextBox();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.txtUsernameRegister = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Label();
            this.plSignUp = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.plLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            this.showInfor.SuspendLayout();
            this.plSignUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // chkRememberMe
            // 
            this.chkRememberMe.AutoSize = true;
            this.chkRememberMe.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRememberMe.Location = new System.Drawing.Point(72, 454);
            this.chkRememberMe.Name = "chkRememberMe";
            this.chkRememberMe.Size = new System.Drawing.Size(158, 25);
            this.chkRememberMe.TabIndex = 5;
            this.chkRememberMe.Text = "Duy trì đăng nhập";
            this.chkRememberMe.UseVisualStyleBackColor = true;
            this.chkRememberMe.CheckedChanged += new System.EventHandler(this.chkRememberMe_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(70, 384);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mật khẩu";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(70, 310);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tên đăng nhập";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(72, 410);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(340, 29);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(74, 334);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(344, 29);
            this.txtUsername.TabIndex = 0;
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // txtFullName
            // 
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFullName.Location = new System.Drawing.Point(58, 171);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(379, 29);
            this.txtFullName.TabIndex = 6;
            this.txtFullName.TextChanged += new System.EventHandler(this.txtFullName_TextChanged);
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneNumber.Location = new System.Drawing.Point(62, 115);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(379, 29);
            this.txtPhoneNumber.TabIndex = 5;
            this.txtPhoneNumber.TextChanged += new System.EventHandler(this.txtPhoneNumber_TextChanged);
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(62, 55);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(379, 29);
            this.txtEmail.TabIndex = 4;
            this.txtEmail.TextChanged += new System.EventHandler(this.txtEmail_TextChanged);
            // 
            // plLogin
            // 
            this.plLogin.Controls.Add(this.picClose);
            this.plLogin.Controls.Add(this.label8);
            this.plLogin.Controls.Add(this.btnSignIn);
            this.plLogin.Controls.Add(this.btnShowSignUp);
            this.plLogin.Controls.Add(this.label7);
            this.plLogin.Controls.Add(this.chkRememberMe);
            this.plLogin.Controls.Add(this.label2);
            this.plLogin.Controls.Add(this.txtUsername);
            this.plLogin.Controls.Add(this.txtPassword);
            this.plLogin.Controls.Add(this.label1);
            this.plLogin.Location = new System.Drawing.Point(698, 0);
            this.plLogin.Name = "plLogin";
            this.plLogin.Size = new System.Drawing.Size(502, 700);
            this.plLogin.TabIndex = 3;
            this.plLogin.Paint += new System.Windows.Forms.PaintEventHandler(this.plLogin_Paint);
            // 
            // picClose
            // 
            this.picClose.Image = ((System.Drawing.Image)(resources.GetObject("picClose.Image")));
            this.picClose.Location = new System.Drawing.Point(447, 12);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(43, 41);
            this.picClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picClose.TabIndex = 25;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(74, 206);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(348, 58);
            this.label8.TabIndex = 24;
            this.label8.Text = "Xin chào! Đăng nhập để quản lý chi tiêu của bạn một cách dễ dàng";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSignIn
            // 
            this.btnSignIn.BackColor = System.Drawing.Color.MistyRose;
            this.btnSignIn.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignIn.Location = new System.Drawing.Point(208, 498);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Size = new System.Drawing.Size(98, 38);
            this.btnSignIn.TabIndex = 23;
            this.btnSignIn.Text = "Sign In";
            this.btnSignIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // btnShowSignUp
            // 
            this.btnShowSignUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowSignUp.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowSignUp.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnShowSignUp.Location = new System.Drawing.Point(64, 558);
            this.btnShowSignUp.Name = "btnShowSignUp";
            this.btnShowSignUp.Size = new System.Drawing.Size(379, 21);
            this.btnShowSignUp.TabIndex = 22;
            this.btnShowSignUp.Text = "Nếu bạn chưa có tài khoản! Đăng ký ngay";
            this.btnShowSignUp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnShowSignUp.Click += new System.EventHandler(this.btnShowSignUp_Click);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(170, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(155, 65);
            this.label7.TabIndex = 15;
            this.label7.Text = "Login";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // showInfor
            // 
            this.showInfor.Controls.Add(this.dtpkDateofBirth);
            this.showInfor.Controls.Add(this.label13);
            this.showInfor.Controls.Add(this.btnSignUp);
            this.showInfor.Controls.Add(this.label6);
            this.showInfor.Controls.Add(this.label5);
            this.showInfor.Controls.Add(this.label3);
            this.showInfor.Controls.Add(this.txtEmail);
            this.showInfor.Controls.Add(this.txtPhoneNumber);
            this.showInfor.Controls.Add(this.txtFullName);
            this.showInfor.Location = new System.Drawing.Point(6, 236);
            this.showInfor.Name = "showInfor";
            this.showInfor.Size = new System.Drawing.Size(484, 335);
            this.showInfor.TabIndex = 6;
            this.showInfor.Visible = false;
            // 
            // dtpkDateofBirth
            // 
            this.dtpkDateofBirth.CustomFormat = " dd/ MM/ yyyy";
            this.dtpkDateofBirth.Font = new System.Drawing.Font("Poppins Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpkDateofBirth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkDateofBirth.Location = new System.Drawing.Point(58, 232);
            this.dtpkDateofBirth.Name = "dtpkDateofBirth";
            this.dtpkDateofBirth.Size = new System.Drawing.Size(379, 27);
            this.dtpkDateofBirth.TabIndex = 44;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(54, 206);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(103, 21);
            this.label13.TabIndex = 43;
            this.label13.Text = "Date of birth";
            // 
            // btnSignUp
            // 
            this.btnSignUp.BackColor = System.Drawing.Color.MistyRose;
            this.btnSignUp.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignUp.Location = new System.Drawing.Point(202, 284);
            this.btnSignUp.Name = "btnSignUp";
            this.btnSignUp.Size = new System.Drawing.Size(98, 38);
            this.btnSignUp.TabIndex = 20;
            this.btnSignUp.Text = "SignUp";
            this.btnSignUp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSignUp.Click += new System.EventHandler(this.btnSignUp_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(58, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 21);
            this.label6.TabIndex = 19;
            this.label6.Text = "Your name";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(58, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 21);
            this.label5.TabIndex = 18;
            this.label5.Text = "Phone";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(58, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 21);
            this.label3.TabIndex = 17;
            this.label3.Text = "Email";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnShowLogin
            // 
            this.btnShowLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowLogin.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnShowLogin.Location = new System.Drawing.Point(74, 574);
            this.btnShowLogin.Name = "btnShowLogin";
            this.btnShowLogin.Size = new System.Drawing.Size(379, 21);
            this.btnShowLogin.TabIndex = 21;
            this.btnShowLogin.Text = " Already have an account? Log in here.";
            this.btnShowLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnShowLogin.Click += new System.EventHandler(this.btnShowLogin_Click);
            // 
            // txtPasswordRegister
            // 
            this.txtPasswordRegister.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPasswordRegister.Location = new System.Drawing.Point(74, 363);
            this.txtPasswordRegister.Name = "txtPasswordRegister";
            this.txtPasswordRegister.Size = new System.Drawing.Size(379, 29);
            this.txtPasswordRegister.TabIndex = 2;
            this.txtPasswordRegister.TextChanged += new System.EventHandler(this.txtPasswordRegister_TextChanged);
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmPassword.Location = new System.Drawing.Point(74, 434);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(379, 29);
            this.txtConfirmPassword.TabIndex = 3;
            this.txtConfirmPassword.TextChanged += new System.EventHandler(this.txtConfirmPassword_TextChanged);
            // 
            // txtUsernameRegister
            // 
            this.txtUsernameRegister.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsernameRegister.Location = new System.Drawing.Point(74, 290);
            this.txtUsernameRegister.Name = "txtUsernameRegister";
            this.txtUsernameRegister.Size = new System.Drawing.Size(379, 29);
            this.txtUsernameRegister.TabIndex = 1;
            this.txtUsernameRegister.TextChanged += new System.EventHandler(this.txtUsernameRegister_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(70, 264);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 21);
            this.label11.TabIndex = 16;
            this.label11.Text = "Usename";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(70, 339);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 21);
            this.label4.TabIndex = 17;
            this.label4.Text = "Password";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(70, 410);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(136, 21);
            this.label12.TabIndex = 18;
            this.label12.Text = "Confim Password";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.MistyRose;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(208, 520);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(98, 38);
            this.btnNext.TabIndex = 19;
            this.btnNext.Text = "Next";
            this.btnNext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // plSignUp
            // 
            this.plSignUp.Controls.Add(this.showInfor);
            this.plSignUp.Controls.Add(this.label12);
            this.plSignUp.Controls.Add(this.label4);
            this.plSignUp.Controls.Add(this.label11);
            this.plSignUp.Controls.Add(this.label10);
            this.plSignUp.Controls.Add(this.label9);
            this.plSignUp.Controls.Add(this.txtUsernameRegister);
            this.plSignUp.Controls.Add(this.txtConfirmPassword);
            this.plSignUp.Controls.Add(this.txtPasswordRegister);
            this.plSignUp.Controls.Add(this.btnShowLogin);
            this.plSignUp.Controls.Add(this.btnNext);
            this.plSignUp.Dock = System.Windows.Forms.DockStyle.Right;
            this.plSignUp.Location = new System.Drawing.Point(698, 0);
            this.plSignUp.Name = "plSignUp";
            this.plSignUp.Size = new System.Drawing.Size(502, 700);
            this.plSignUp.TabIndex = 4;
            this.plSignUp.Visible = false;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(70, 175);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(348, 58);
            this.label10.TabIndex = 15;
            this.label10.Text = "Welcome! Sign up to manage your expenses with ease.";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(154, 98);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(206, 65);
            this.label9.TabIndex = 14;
            this.label9.Text = "Sign Up";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(698, 700);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // SignIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.plLogin);
            this.Controls.Add(this.plSignUp);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "SignIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SignIn";
            this.plLogin.ResumeLayout(false);
            this.plLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            this.showInfor.ResumeLayout(false);
            this.showInfor.PerformLayout();
            this.plSignUp.ResumeLayout(false);
            this.plSignUp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkRememberMe;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel plLogin;
        private System.Windows.Forms.Panel showInfor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label btnSignUp;
        private System.Windows.Forms.Label btnShowLogin;
        private System.Windows.Forms.TextBox txtPasswordRegister;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.TextBox txtUsernameRegister;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label btnNext;
        private System.Windows.Forms.Panel plSignUp;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label btnSignIn;
        private System.Windows.Forms.Label btnShowSignUp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpkDateofBirth;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox picClose;
    }
}