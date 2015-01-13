using System;
using System.Globalization;
using System.Windows.Forms;
using WinRemote.App.Controllers;
using WinRemote.App.Helpers;
using WinRemote.App.Models;

namespace WinRemote.App.Views
{
    /// <summary>
    /// 
    /// </summary>
    public partial class LoginForm : Form
    {
        private void Translate()
        {
            if (CultureInfo.CurrentCulture.Name.Contains("de"))
            {
                InfoLoginLabel.Text = Properties.translate_de.LoginInfo;
                InfoLoginLabel.Text += '\n' + Properties.translate_de.LoginInfo2;
                MailLabel.Text = Properties.translate_de.Mail;
                PasswordLabel.Text = Properties.translate_de.Password;
                SignUpLabel.Text = Properties.translate_de.SignUp;
                SettingsButton.Text = Properties.translate_de.Settings;
            }
            else
            {
                InfoLoginLabel.Text = Properties.translate.LoginInfo;
                InfoLoginLabel.Text += '\n' + Properties.translate.LoginInfo2;
                MailLabel.Text = Properties.translate.Mail;
                PasswordLabel.Text = Properties.translate.Password;
                SignUpLabel.Text = Properties.translate.SignUp;
                SettingsButton.Text = Properties.translate.Settings;
            }
        }

        #region fields
        /// <summary>
        /// Event is called when a login attempt was successful
        /// </summary>
        public event EventHandler AuthSuccess;
        /// <summary>
        /// Necessary to manage the AuthSuccess Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void EventHandler(object sender, EventArgs e);
        /// <summary>
        /// true if login was successful
        /// </summary>
        private bool _success;
        #endregion

        #region methods
        /// <summary>
        /// Initialize and translate
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();

            Translate();
        }

       
        /// <summary>
        /// Is called when the user presses the submit button and tries to log in.
        /// If the token is not valid, the user is asked to type in his data again or
        /// create an account. If it is valid, the auth token is stored and the user can
        /// operate on the MainForm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitLogin_Click(object sender, EventArgs e)
        {
            var authToken = User.Authenticate(MailText.Text, PasswordText.Text);

            if (authToken.Equals("invalid"))
            {
                NotificationHelper.ShowError(Properties.translate.InvalidData);
                MailText.Text = "";
                PasswordText.Text = "";
            }
            else
            {
                DbController.StoreAuthToken(authToken);
                Settings.AuthToken = authToken;
                AuthSuccess(this, new EventArgs());
                _success = true;
                Close();
            }
        }
        /// <summary>
        /// Is called when form is closed. If user is not successfully logged in, it will shut down the app.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Close(object sender, EventArgs e)
        {
            if (!_success)
            {
               
                    Awesomium.Core.WebCore.Shutdown();
                    Application.Exit();
               
            }
        }
        /// <summary>
        /// Called when the form is loaded. Sets the AcceptButton to the SubmitButton so it is pressed on pressing Enter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Load(object sender, EventArgs e)
        {
            AcceptButton = SubmitLogin;
        }

        /// <summary>
        /// Called when the SignUp link is clicked. Opens the SignUp Base URL from Settings in the Default Browser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignUpLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string goTo = Settings.BaseSignUpUrl;
            //Open URL with Default Browser
            System.Diagnostics.Process.Start(goTo);
        }
        #endregion

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            new SettingsForm().ShowDialog(this);
        }
    }
}