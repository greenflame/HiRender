using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using HiMath;

namespace HiTracer
{
    public class HTracer
    {
        public delegate Color TraseRayFunction(HRay ray, int rayLifeTime);

        public HTracer(ICamera camera)
        {
            Colliders = new List<ICollider>();
            LightSources = new List<ILightSource>();
            Camera = camera;
        }

        public ICamera Camera { get; set; }
        public List<ICollider> Colliders { get; set; }
        public List<ILightSource> LightSources { get; set; }
        public Label Progress { get; set; }

        public double LightnessInPoint(Point3D point, Vector3D rayOnCamera, Vector3D normal)
        {
            return (from lightSource in LightSources
                select lightSource.LightnessInPoint(point, rayOnCamera, normal, Colliders)).Concat(new double[] {0})
                .Max();
        }

        public Color TraseRay(HRay ray, int rayLifeTime)
        {
            if (rayLifeTime == 0)
                return Camera.BackgrundColor;

            var collisions = (from collider in Colliders
                              where collider.DetectCollision(ray)
                              select new KeyValuePair<Point3D, ICollider>(collider.CollisionPoint(ray), collider)).ToList();

            collisions.Sort(
                (KeyValuePair<Point3D, ICollider> a, KeyValuePair<Point3D, ICollider> b) =>
                    (ray.Source - a.Key).Length.CompareTo((ray.Source - b.Key).Length));

            return collisions.Count == 0
                ? Camera.BackgrundColor
                : collisions[0].Value.Shader(this, collisions[0].Value, ray, rayLifeTime);
        }

        public Bitmap Visualize()
        {
            return Camera.EmitRays(TraseRay, Progress);
        }
    }
}