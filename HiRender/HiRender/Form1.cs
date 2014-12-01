using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace HiRender
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var render = new HRender();
            Bitmap bmp = render.Visualize();
            pictureBox1.Image = bmp;
            bmp.Save("test.png");
        }
    }
}