using System;
using System.Drawing;
using System.Windows.Forms;

namespace All_Windows_capslock_driver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.Location = new Point(0, 100);
            this.StartPosition = FormStartPosition.Manual;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }
}
