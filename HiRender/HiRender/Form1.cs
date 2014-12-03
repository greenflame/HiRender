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

            var m1 = new HModel();
            m1.Colliders.Add(new HFace(new Point3D(1, 1, 0), new Point3D(-1, 1, 0), new Point3D(-1, -1, 0),
                HShaders.MirrorTransparentBlueShader));
            m1.Colliders.Add(new HFace(new Point3D(1, 1, 0), new Point3D(1, -1, 0), new Point3D(-1, -1, 0),
                HShaders.MirrorTransparentBlueShader));

            var m2 = new HModel();
            m2.Colliders.Add(new HFace(new Point3D(1, 1, 0), new Point3D(-1, 1, 0), new Point3D(-1, -1, 0),
                HShaders.MirrorTransparentBlackShader));
            m2.Colliders.Add(new HFace(new Point3D(1, 1, 0), new Point3D(1, -1, 0), new Point3D(-1, -1, 0),
                HShaders.MirrorTransparentBlackShader));

            m2.TransformationMatrix.Rotate(new Quaternion(new Vector3D(0, 1, 0), 70));

            scene.Models.Add(m1);
            scene.Models.Add(m2);

            scene.TransformationMatrix.Scale(new Vector3D(4, 4, 4));
            scene.TransformationMatrix.Rotate(new Quaternion(new Vector3D(0, 1, 0), 45));
            scene.TransformationMatrix.Rotate(new Quaternion(new Vector3D(1, 0, 0), 30));
            scene.TransformationMatrix.Translate(new Vector3D(0, 0, -10));


            //rendering
            var render = new HRender();

            render.Colliders.Clear();
            render.Colliders.AddRange(scene.ApplyTransform());

            Bitmap bmp = render.Visualize();
            pictureBox1.Image = bmp;
            bmp.Save("test.png");
        }
    }
}