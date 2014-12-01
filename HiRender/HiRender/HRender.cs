using System.Collections.Generic;
using System.Drawing;

namespace HiRender
{
    internal class HRender
    {
        private readonly List<ICollider> _colliders;

        public HRender()
        {
            _colliders = new List<ICollider>();

            ViewportWidth = 2;
            ViewportHeight = 1;
            ViewportDistance = 0.5;

            WidthIterations = 600;
            HeightIterations = 300;

            ICollider face = new HFaceCollider(new HPoint(0, 0, -20), new HPoint(20, 0, -20), new HPoint(0, 20, -20),
                HShaders.SimpleRedShader);
            ICollider face2 = new HFaceCollider(new HPoint(0, 0, -19), new HPoint(15, 0, -19), new HPoint(0, 15, -21),
                HShaders.SimpleBlackShader);

            Colliders.Add(face);
            Colliders.Add(face2);
        }

        public double ViewportWidth { get; set; }
        public double ViewportHeight { get; set; }
        public double ViewportDistance { get; set; }

        public int WidthIterations { get; set; }
        public int HeightIterations { get; set; }

        public List<ICollider> Colliders
        {
            get { return _colliders; }
        }

        public Color TraceRay(HRay ray)
        {
            var collisions = new List<KeyValuePair<HPoint, ICollider>>();

            foreach (ICollider collider in Colliders)
            {
                HPoint collisionPoint = collider.CollisionPoint(ray);
                if (collisionPoint != null)
                    collisions.Add(new KeyValuePair<HPoint, ICollider>(collisionPoint, collider));
            }

            collisions.Sort(
                (KeyValuePair<HPoint, ICollider> a, KeyValuePair<HPoint, ICollider> b) =>
                    (ray.Source - a.Key).Length().CompareTo((ray.Source - b.Key).Length()));

            return collisions.Count == 0 ? Color.White : collisions[0].Value.ProcessCollision(this, collisions[0].Value, ray);
        }

        public Bitmap Visualize()
        {
            var bmp = new Bitmap(WidthIterations, HeightIterations);

            for (int y = 0; y < HeightIterations; y++)
                for (int x = 0; x < WidthIterations; x++)
                {
                    var a = new HPoint(0, 0, 0);
                    var b = new HPoint(
                        ViewportWidth/WidthIterations*x - ViewportWidth/2,
                        ViewportHeight/HeightIterations*y - ViewportHeight/2,
                        -ViewportDistance);

                    bmp.SetPixel(x, HeightIterations - y - 1, TraceRay(new HRay(a, b)));
                }

            return bmp;
        }
    }
}