using System.Collections.Generic;
using System.Drawing;

namespace HiRender
{
    internal class HRender
    {
        public HRender(HScene scene, int widthIterations, int heightIterations)
        {
            _scene = scene;
            WidthIterations = widthIterations;
            HeightIterations = heightIterations;
        }


        private HScene _scene;
        

        public int WidthIterations { get; set; }
        public int HeightIterations { get; set; }

        public Color TraceRay(HRay ray)
        {
            var collisions = new List<KeyValuePair<HPoint, ICollider>>();

            foreach (ICollider collider in _scene.Colliders)
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
                        _scene.Camera.ViewportWidth / WidthIterations * x - _scene.Camera.ViewportWidth / 2,
                        _scene.Camera.ViewportHeight / HeightIterations * y - _scene.Camera.ViewportHeight / 2,
                        -_scene.Camera.ViewportDistance);

                    bmp.SetPixel(x, HeightIterations - y - 1, TraceRay(new HRay(a, b)));
                }

            return bmp;
        }
    }
}