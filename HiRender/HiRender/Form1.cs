using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using HiMath;
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
            HScene scene = new HScene();

            //HModel m1 = new HModel();

            //ICollider face = new HFace(new Point3D(-20, -10, -20), new Point3D(20, -10, -20), new Point3D(0, 15, -20),
            //    HShaders.MirrorBlackShader);
            //ICollider face2 = new HFace(new Point3D(-10, -8, -10), new Point3D(0, -8, -30), new Point3D(40, -8, -30),
            //    HShaders.SimpleRedShader);

            //m1.Colliders.Add(face);
            //m1.Colliders.Add(face2);
            //scene.Models.Add(m1);

            Matrix3D m = new Matrix3D();

            HModel m1 = new HModel();
            m1.Colliders.Add(new HFace(new Point3D(1, 1, 0), new Point3D(-1, 1, 0), new Point3D(-1, -1, 0),
                HShaders.MirrorBlackShader));
            m1.Colliders.Add(new HFace(new Point3D(1, 1, 0), new Point3D(1, -1, 0), new Point3D(-1, -1, 0),
                HShaders.MirrorBlackShader));

            HModel m2 = new HModel();
            m2.Colliders.Add(new HFace(new Point3D(1, 1, 0), new Point3D(-1, 1, 0), new Point3D(-1, -1, 0),
                HShaders.MirrorBlackShader));
            m2.Colliders.Add(new HFace(new Point3D(1, 1, 0), new Point3D(1, -1, 0), new Point3D(-1, -1, 0),
                HShaders.MirrorBlackShader));

            m = new Matrix3D();
            m.Rotate(new Quaternion(new Vector3D(0,1,0), 70));
            m2.Transform(m);


            scene.Models.Add(m1);
            scene.Models.Add(m2);

            m = new Matrix3D();
            m.Scale(new Vector3D(4,4,4));
            m.Rotate(new Quaternion(new Vector3D(0, 1, 0), 45));
            m.Rotate(new Quaternion(new Vector3D(1, 0, 0), 30));
            m.Translate(new Vector3D(0, 0, -10));
            scene.Transform(m);
            
            //rendering
            var render = new HRender();
            
            render.Colliders.Clear();
            render.Colliders.AddRange(scene.ToColliderList());
            
            Bitmap bmp = render.Visualize();
            pictureBox1.Image = bmp;
            bmp.Save("test.png");
        }
    }
}