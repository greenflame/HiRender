using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Collections.Generic;

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
            var colliders = new List<ICollider>();
            var lights = new List<HLight>();
            var camera = new HCamera(20, 10, 2);

            ICollider face = new HFaceCollider(new HPoint(-100, 0, -100), new HPoint(100, 0, -100), new HPoint(0, 100, -50),
                HShaders.SimpleRedShader);

            colliders.Add(face);

            var scene = new HScene(colliders, lights, camera);
            var render = new HRender(scene, 600, 300);

            Bitmap bmp = render.Visualize();
            pictureBox1.Image = bmp;
            bmp.Save("test.png");
        }
    }
}