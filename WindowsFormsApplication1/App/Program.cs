using System;
using System.Windows.Forms;
using WinRemote.App.Controllers;
using WinRemote.App.Views;

namespace WinRemote.App
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Settings.F1 = new MainForm();
                Application.Run(Settings.F1);
            }
            catch (Exception e) { Console.WriteLine(e.StackTrace); }
            
        }
    }
}
