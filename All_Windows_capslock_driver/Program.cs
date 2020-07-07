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
            layar.ambilUkuranDevice();
            // Start objek driver set global hook secara lowlevel true
            driver_caps driver_chronome = new driver_caps(true);
            Application.Run();
        }
    }

    static class layar // Optimized for aspect Ratio 16:9 (Optimasi untuk rasio 16:9)
        //Future works -> will be another aspect ratio
        // Kedepannya mungkin akan ada aspek rasio lebih banyak
    {
        public static int pixelX;
        public static int pixelY;


        public static void ambilUkuranDevice()
        {
            pixelX = Screen.PrimaryScreen.Bounds.Width;
            pixelY = Screen.PrimaryScreen.Bounds.Height;
        }

        public static int[] akhir()
        {
            int[] pixelArr = new int[2];
            pixelArr[0] = (pixelX / 2);
            pixelArr[1] = pixelY - (pixelY / 9); // ->Aspect ratio (aspek rasio)
            return pixelArr;
        }
    }
}
