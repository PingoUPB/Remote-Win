using System.Globalization;
using System.Windows.Forms;

namespace WinRemote.App.Helpers
{
    /// <summary>
    /// Provides error messages.
    /// </summary>
    internal class NotificationHelper
    {
        /// <summary>
        /// Shows the error message with a c# predefined MessageBox. 
        /// </summary>
        /// <param name="errmsg">The message to be displayed.</param>
        public static void ShowError(string errmsg)
        {
            MessageBox.Show(errmsg,
                CultureInfo.CurrentCulture.Name.Contains("de")
                    ? Properties.translate_de.Error
                    : Properties.translate.Error,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows the error message with a c# predefined MessageBox and closes the application. Use this for crucial errors.
        /// </summary>
        /// <param name="errmsg">The message to be shown.</param>
        public static void ShowErrorAndClose(string errmsg)
        {
            ShowError(errmsg);
            Application.Exit();
        }
    }
}