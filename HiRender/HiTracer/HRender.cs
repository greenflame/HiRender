using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Media3D;
using HiMath;

namespace HiTracer
{
    public class HRender
    {
        private readonly List<ICollider> _colliders;

        public HRender()
        {
            _colliders = new List<ICollider>();

            ViewportWidth = 2;
            ViewportHeight = 1;
            ViewportDistance = 1.5;

            WidthIterations = 600;
            HeightIterations = 300;

            RayLifeTime = 3;
            BackgrundColor = Color.White;
            RayPerPixel = 1;
        }

        public double ViewportWidth { get; set; }
        public double ViewportHeight { get; set; }
        public double ViewportDistance { get; set; }

        public int WidthIterations { get; set; }
        public int HeightIterations { get; set; }

        public int RayLifeTime { get; set; }
        public Color BackgrundColor { get; set; }
        public int RayPerPixel { get; set; }

        public List<ICollider> Colliders
        {
            get { return _colliders; }
        }

        public Color TraseRay(HRay ray, int rayLifeTime)
        {
            if (rayLifeTime == 0)
                return BackgrundColor;  //?

            var collisions = new List<KeyValuePair<Point3D, ICollider>>();

            foreach (ICollider collider in Colliders)
            {
                if (collider.DetectCollision(ray))
                    collisions.Add(new KeyValuePair<Point3D, ICollider>(collider.CollisionPoint(ray), collider));
            }

            collisions.Sort(
                (KeyValuePair<Point3D, ICollider> a, KeyValuePair<Point3D, ICollider> b) =>
                    (ray.Source - a.Key).Length.CompareTo((ray.Source - b.Key).Length));

            return collisions.Count == 0 ? BackgrundColor : collisions[0].Value.Shader(this, collisions[0].Value, ray, rayLifeTime);
        }

        public Bitmap Visualize()
        {
            WidthIterations *= (int) Math.Sqrt(RayPerPixel);
            HeightIterations *= (int) Math.Sqrt(RayPerPixel);

            var bmp = new Bitmap(WidthIterations, HeightIterations);

            for (int y = 0; y < HeightIterations; y++)
                for (int x = 0; x < WidthIterations; x++)
                {
                    var a = new Point3D(0, 0, 0);
                    var b = new Vector3D(
                        ViewportWidth/WidthIterations*x - ViewportWidth/2,
                        ViewportHeight/HeightIterations*y - ViewportHeight/2,
                        -ViewportDistance);

                    bmp.SetPixel(x, HeightIterations - y - 1, TraseRay(new HRay(a, b), RayLifeTime));
                }

            WidthIterations /= (int)Math.Sqrt(RayPerPixel);
            HeightIterations /= (int)Math.Sqrt(RayPerPixel);

            Bitmap res = new Bitmap(bmp, WidthIterations, HeightIterations);
            return res;
        }
    }
}