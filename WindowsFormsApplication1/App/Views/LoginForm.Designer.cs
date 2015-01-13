namespace WinRemote.App.Views
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.MailText = new System.Windows.Forms.TextBox();
            this.PasswordText = new System.Windows.Forms.TextBox();
            this.MailLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.SubmitLogin = new System.Windows.Forms.Button();
            this.ErrorMsg = new System.Windows.Forms.Label();
            this.SignUpLabel = new System.Windows.Forms.LinkLabel();
            this.InfoLoginLabel = new System.Windows.Forms.Label();
            this.SettingsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MailText
            // 
            resources.ApplyResources(this.MailText, "MailText");
            this.MailText.Name = "MailText";
            // 
            // PasswordText
            // 
            resources.ApplyResources(this.PasswordText, "PasswordText");
            this.PasswordText.Name = "PasswordText";
            this.PasswordText.UseSystemPasswordChar = true;
            // 
            // MailLabel
            // 
            resources.ApplyResources(this.MailLabel, "MailLabel");
            this.MailLabel.Name = "MailLabel";
            // 
            // PasswordLabel
            // 
            resources.ApplyResources(this.PasswordLabel, "PasswordLabel");
            this.PasswordLabel.Name = "PasswordLabel";
            // 
            // SubmitLogin
            // 
            resources.ApplyResources(this.SubmitLogin, "SubmitLogin");
            this.SubmitLogin.Name = "SubmitLogin";
            this.SubmitLogin.UseVisualStyleBackColor = true;
            this.SubmitLogin.Click += new System.EventHandler(this.SubmitLogin_Click);
            // 
            // ErrorMsg
            // 
            resources.ApplyResources(this.ErrorMsg, "ErrorMsg");
            this.ErrorMsg.ForeColor = System.Drawing.Color.Black;
            this.ErrorMsg.Name = "ErrorMsg";
            // 
            // SignUpLabel
            // 
            resources.ApplyResources(this.SignUpLabel, "SignUpLabel");
            this.SignUpLabel.Name = "SignUpLabel";
            this.SignUpLabel.TabStop = true;
            this.SignUpLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SignUpLabel_LinkClicked);
            // 
            // InfoLoginLabel
            // 
            resources.ApplyResources(this.InfoLoginLabel, "InfoLoginLabel");
            this.InfoLoginLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.InfoLoginLabel.Name = "InfoLoginLabel";
            // 
            // SettingsButton
            // 
            resources.ApplyResources(this.SettingsButton, "SettingsButton");
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.UseVisualStyleBackColor = true;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // LoginForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.InfoLoginLabel);
            this.Controls.Add(this.MailLabel);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.MailText);
            this.Controls.Add(this.PasswordText);
            this.Controls.Add(this.SignUpLabel);
            this.Controls.Add(this.ErrorMsg);
            this.Controls.Add(this.SubmitLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "LoginForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoginForm_Close);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MailText;
        private System.Windows.Forms.TextBox PasswordText;
        private System.Windows.Forms.Label MailLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Button SubmitLogin;
        private System.Windows.Forms.Label ErrorMsg;
        private System.Windows.Forms.LinkLabel SignUpLabel;
        private System.Windows.Forms.Label InfoLoginLabel;
        private System.Windows.Forms.Button SettingsButton;
    }
}