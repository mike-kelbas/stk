using System;
using System.Windows.Forms;

namespace STK
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AutorizeForm());

            while (!AutorizeForm.Exit && !AutorizeForm.Vhod)
            { }


            if (AutorizeForm.Vhod)
            {
                MainForm.isAdmin = AutorizeForm.Admin;
                MainForm.userID = AutorizeForm.UserId;
                Application.Run(new MainForm());
            }
        }
    }
}