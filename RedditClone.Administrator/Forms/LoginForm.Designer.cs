namespace RedditClone.Administrator.Forms
{
    partial class LoginForm
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
            this.LblUrl = new System.Windows.Forms.Label();
            this.TbUrl = new System.Windows.Forms.TextBox();
            this.BtnLogin = new System.Windows.Forms.Button();
            this.LblEmail = new System.Windows.Forms.Label();
            this.LblPassword = new System.Windows.Forms.Label();
            this.TbEmail = new System.Windows.Forms.TextBox();
            this.TbPassword = new System.Windows.Forms.TextBox();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LblUrl
            // 
            this.LblUrl.AutoSize = true;
            this.LblUrl.Location = new System.Drawing.Point(12, 15);
            this.LblUrl.Name = "LblUrl";
            this.LblUrl.Size = new System.Drawing.Size(80, 20);
            this.LblUrl.TabIndex = 0;
            this.LblUrl.Text = "Server URL";
            // 
            // TbUrl
            // 
            this.TbUrl.Location = new System.Drawing.Point(98, 12);
            this.TbUrl.Name = "TbUrl";
            this.TbUrl.Size = new System.Drawing.Size(429, 27);
            this.TbUrl.TabIndex = 1;
            // 
            // BtnLogin
            // 
            this.BtnLogin.Location = new System.Drawing.Point(433, 111);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(94, 29);
            this.BtnLogin.TabIndex = 2;
            this.BtnLogin.Text = "Login";
            this.BtnLogin.UseVisualStyleBackColor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // LblEmail
            // 
            this.LblEmail.AutoSize = true;
            this.LblEmail.Location = new System.Drawing.Point(46, 48);
            this.LblEmail.Name = "LblEmail";
            this.LblEmail.Size = new System.Drawing.Size(46, 20);
            this.LblEmail.TabIndex = 3;
            this.LblEmail.Text = "Email";
            // 
            // LblPassword
            // 
            this.LblPassword.AutoSize = true;
            this.LblPassword.Location = new System.Drawing.Point(22, 81);
            this.LblPassword.Name = "LblPassword";
            this.LblPassword.Size = new System.Drawing.Size(70, 20);
            this.LblPassword.TabIndex = 4;
            this.LblPassword.Text = "Password";
            // 
            // TbEmail
            // 
            this.TbEmail.Location = new System.Drawing.Point(98, 45);
            this.TbEmail.Name = "TbEmail";
            this.TbEmail.Size = new System.Drawing.Size(429, 27);
            this.TbEmail.TabIndex = 5;
            // 
            // TbPassword
            // 
            this.TbPassword.Location = new System.Drawing.Point(98, 78);
            this.TbPassword.Name = "TbPassword";
            this.TbPassword.Size = new System.Drawing.Size(429, 27);
            this.TbPassword.TabIndex = 6;
            this.TbPassword.UseSystemPasswordChar = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(333, 111);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(94, 29);
            this.BtnCancel.TabIndex = 7;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 148);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.TbPassword);
            this.Controls.Add(this.TbEmail);
            this.Controls.Add(this.LblPassword);
            this.Controls.Add(this.LblEmail);
            this.Controls.Add(this.BtnLogin);
            this.Controls.Add(this.TbUrl);
            this.Controls.Add(this.LblUrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label LblUrl;
        private TextBox TbUrl;
        private Button BtnLogin;
        private Label LblEmail;
        private Label LblPassword;
        private TextBox TbEmail;
        private TextBox TbPassword;
        private Button BtnCancel;
    }
}