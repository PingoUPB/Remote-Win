using System;
using System.Globalization;
using System.Windows.Forms;
using WinRemote.App.Controllers;

namespace WinRemote.App.Views
{
   

    /// <summary>
    /// 
    /// </summary>
    public partial class SettingsForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public SettingsForm()
        {
            InitializeComponent();
            Translate();
        }

        private void Translate()
        {
            if (CultureInfo.CurrentCulture.Name.Contains("de"))
            {
                InfoSettingsLabel.Text = Properties.translate_de.SettingsInfo;
                SaveButton.Text = Properties.translate_de.SaveChanges;
                DiscardButton.Text = Properties.translate_de.DiscardChanges;
                ResetButton.Text = Properties.translate_de.ResetToDefault;
            }
            else
            {
                InfoSettingsLabel.Text = Properties.translate.SettingsInfo;
                SaveButton.Text = Properties.translate.SaveChanges;
                DiscardButton.Text = Properties.translate.DiscardChanges;
                ResetButton.Text = Properties.translate.ResetToDefault;
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            SiteURLText.Text = Settings.BaseUrl;
            SocketURLText.Text = Settings.BaseSocketUrl;
        }

        private void DiscardButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            SiteURLText.Text = Settings.DefaultUrl;
            SocketURLText.Text = Settings.DefaultSocketUrl;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Settings.BaseUrl = SiteURLText.Text;
            if (!Settings.BaseUrl.Contains("http") && !Settings.BaseUrl.Contains("https")) Settings.BaseUrl = "https://" + Settings.BaseUrl;
            Settings.BaseSocketUrl = SocketURLText.Text;
            if (!Settings.BaseSocketUrl.Contains("http") && !Settings.BaseSocketUrl.Contains("https")) Settings.BaseSocketUrl = "http://" + Settings.BaseSocketUrl;
            DbController.StoreUrLs(Settings.BaseUrl, Settings.BaseSocketUrl);
            Close();
        }
    }
}
