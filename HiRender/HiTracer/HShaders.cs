using System.Drawing;
using HiMath;

namespace HiTracer
{
    public class HShaders
    {
        public delegate Color Shader(HRender render, ICollider collider, HRay ray);

        public static Color MixColors(Color c1, Color c2, double k1, double k2)
        {
            return Color.FromArgb(
                (int) ((c1.A*k1 + c2.A*k2)/(k1 + k2)),
                (int) ((c1.R*k1 + c2.R*k2)/(k1 + k2)),
                (int) ((c1.G*k1 + c2.G*k2)/(k1 + k2)),
                (int) ((c1.B*k1 + c2.B*k2)/(k1 + k2))
                );
        }

        public static Color SimpleBlackShader(HRender render, ICollider collider, HRay ray)
        {
            return Color.Black;
        }

        public static Color SimpleRedShader(HRender render, ICollider collider, HRay ray)
        {
            return Color.Red;
        }

        public static Color MirrorBlackShader(HRender render, ICollider collider, HRay ray)
        {
            var ray2 = new HRay(collider.CollisionPoint(ray).Value,
                HGeometry.ReflectVector(collider.CollisionNormal(ray), ray.Direction));
            return MixColors(Color.Black, render.TraseRay(ray2), 0.5, 0.5);
        }
    }
}