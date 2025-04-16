using ExpenseManagement.Views;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ExpenseManagement
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            SetCurrentProcessExplicitAppUserModelID("MyCompany.MyApp");

            Application.Run(new SignIn());
        }

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int SetCurrentProcessExplicitAppUserModelID(string AppID);
    }
}
