using System;
using System.Windows.Forms;

namespace All_Windows_capslock_driver
{
    static class Program
    {
        /// <summary>
        /// ChronomeDev 2020
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            driver_caps driver_chronome = new driver_caps(true);
            
            Application.Run();
        }
    }
}
