using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using HiScene;
using HiTracer;

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
            var scene = new HtScene();

            //HModel m1 = HPrimitives.PlaneXy(HShaders.DiffuseGrayShader, 10); //main plane
            //m1.TransformationMatrix.Rotate(new Quaternion(new Vector3D(1, 0, 0), 90));
            //scene.Models.Add(m1);

            //HModel cube = HPrimitives.Cube(HShaders.DiffuseGrayShader); //cube
            //cube.TransformationMatrix.Translate(new Vector3D(0, 1, 0));
            //scene.Models.Add(cube);

            scene.Models.Add(HModel.Load("chips.obj"));

            scene.TransformationMatrix.Rotate(new Quaternion(new Vector3D(0, 1, 0), 45));   //scene transformation
            scene.TransformationMatrix.Rotate(new Quaternion(new Vector3D(1, 0, 0), 30)); //lean
            scene.TransformationMatrix.Translate(new Vector3D(0, 0, -10));


            //rendering
            var lines = 50;
            var render = new HTracer(new HPerspectiveCamera(2, 1 * 16 / 9, 1, lines * 16 / 9, lines));
            render.Camera.RayPerPixel = 1;
            render.Progress = label1;

            render.Colliders = scene.ApplyTransform();
            render.LightSources.Add(new HPointLightSource(new Point3D(5, 10, 5) * scene.TransformationMatrix));

            var bmp = render.Visualize();

            pictureBox1.Image = bmp;
            bmp.Save("test.png");
        }
    }
}