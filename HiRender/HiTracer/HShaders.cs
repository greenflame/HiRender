using System;
using System.Drawing;
using System.Windows.Media.Media3D;
using HiMath;

namespace HiTracer
{
    public class HShaders
    {
        public delegate Color Shader(HTracer tracer, ICollider collider, HRay ray, int rayLifeTime);

        public static Color MixColors(Color c1, Color c2, double k1, double k2)
        {
            return Color.FromArgb(
                (int) ((c1.A*k1 + c2.A*k2)/(k1 + k2)),
                (int) ((c1.R*k1 + c2.R*k2)/(k1 + k2)),
                (int) ((c1.G*k1 + c2.G*k2)/(k1 + k2)),
                (int) ((c1.B*k1 + c2.B*k2)/(k1 + k2))
                );
        }

        public static Color DiffuseGrayShader(HTracer tracer, ICollider collider, HRay ray, int rayLifeTime)
        {
            var lightness = tracer.LightnessInPoint(collider.CollisionPoint(ray), -ray.Direction,
                collider.CollisionNormal(ray));
            return MixColors(Color.LightGray, tracer.Camera.BackgrundColor, lightness, 1 - lightness);
        }

        public static Color ChessShader(HTracer tracer, ICollider collider, HRay ray, int rayLifeTime)
        {
            Point3D cp = collider.CollisionPoint(ray);
            Matrix3D tm = collider.ApplyedTranformation;
            tm.Invert();
            cp = cp*tm;
            var result = ((int) Math.Round(cp.X) + (int) Math.Round(cp.Y) + (int) Math.Round(cp.Z))%2 == 0
                ? Color.White
                : Color.Black;

            double lightness = tracer.LightnessInPoint(collider.CollisionPoint(ray), -ray.Direction,
                collider.CollisionNormal(ray));

            return MixColors(result, tracer.Camera.BackgrundColor, lightness, 1 - lightness);

        }
    }
}