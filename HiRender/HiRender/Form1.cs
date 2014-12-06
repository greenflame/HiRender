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
            var scene = new HScene();

            HModel m1 = HPrimitives.PlaneXy(HShaders.ChessShader, 10); //main chess plane
            m1.TransformationMatrix.Rotate(new Quaternion(new Vector3D(1, 0, 0), 90));
            scene.Models.Add(m1);

            var m2 = HPrimitives.PlaneXy(HShaders.MirrorGrayShader, 10);
            m2.TransformationMatrix.Rotate(new Quaternion(new Vector3D(0, 1, 0), 0));
            m2.TransformationMatrix.Translate(new Vector3D(0, 0, -3));
            scene.Models.Add(m2);

            var m3 = HPrimitives.PlaneXy(HShaders.MirrorGrayShader, 5);
            m3.TransformationMatrix.Rotate(new Quaternion(new Vector3D(0, 1, 0), 0));
            m3.TransformationMatrix.Translate(new Vector3D(0, 0, 6));
            scene.Models.Add(m3);

            //HModel s = new HModel();
            //ICollider sc = new HSphere(new Point3D(0,0,0), 0.5, HShaders.SimpleRedShader);
            //s.Colliders.Add(sc);
            //s.TransformationMatrix.Translate(new Vector3D(0,2,0));
            //scene.Models.Add(s);

            //var m3 = HPrimitives.PlaneXy(HShaders.MirrorBlackShader);
            //m3.TransformationMatrix.Rotate(new Quaternion(new Vector3D(1, 0, 0), 90));


            HModel cube = HPrimitives.Cube(HShaders.ChessShader);   //cube
            cube.TransformationMatrix.Translate(new Vector3D(0, 1, 0));
            scene.Models.Add(cube);

            scene.TransformationMatrix.Rotate(new Quaternion(new Vector3D(0, 1, 0), 45));
            scene.TransformationMatrix.Rotate(new Quaternion(new Vector3D(1, 0, 0), 30)); //lean
            scene.TransformationMatrix.Translate(new Vector3D(0, 0, -10));


            //rendering
            var render = new HRender();
            render.ViewportDistance = 0.8;
            render.RayLifeTime = 10;
            render.RayPerPixel = 1;
            //render.WidthIterations = 2000;
            //render.HeightIterations = 1000;

            render.Colliders.Clear();
            render.Colliders.AddRange(scene.ApplyTransform());

            Bitmap bmp = render.Visualize();
            pictureBox1.Image = bmp;
            bmp.Save("test.png");
        }
    }
}