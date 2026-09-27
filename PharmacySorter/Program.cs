using System;
using System.Windows.Forms;

namespace PharmacySorter
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (FrmLogin login = new FrmLogin())
            {
                if (login.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                Application.Run(new FrmMain(login.CurrentUser));
            }
        }
    }
}
