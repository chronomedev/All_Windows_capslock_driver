using System;
using System.Drawing;
using System.Windows.Forms;
/// <summary>
/// Driver indicator UI code
/// ChronomeDev 2020
/// </summary>

namespace All_Windows_capslock_driver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.Manual;
            int[] output = layar.akhir();
            this.Location = new Point(output[0] - (this.Size.Width/2), output[1] - (this.Size.Height/2));   

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.TopMost = true;
            
        }
    }
}
