using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Media.Media3D;
using HiMath;

namespace HiTracer
{
    public class HShaders
    {
        public delegate Color Shader(HRender render, ICollider collider, HRay ray, int rayLifeTime);

        public static Color MixColors(Color c1, Color c2, double k1, double k2)
        {
            return Color.FromArgb(
                (int) ((c1.A*k1 + c2.A*k2)/(k1 + k2)),
                (int) ((c1.R*k1 + c2.R*k2)/(k1 + k2)),
                (int) ((c1.G*k1 + c2.G*k2)/(k1 + k2)),
                (int) ((c1.B*k1 + c2.B*k2)/(k1 + k2))
                );
        }

        public static Color MixShader(Shader s1, Shader s2, double k1, double k2)
        {
            return Color.White;
        }

        public static Color MirrorShader(HRender render, ICollider collider, HRay ray, int rayLifeTime)
        {
            return render.TraseRay(collider.ReflectRay(ray), rayLifeTime - 1);
        }

        public static Color TransparentShader(HRender render, ICollider collider, HRay ray, int rayLifeTime)
        {
            return render.TraseRay(collider.PassRay(ray), rayLifeTime);
        }

        public static Color SimpleBlackShader(HRender render, ICollider collider, HRay ray, int rayLifeTime)
        {
            return Color.Black;
        }

        public static Color SimpleRedShader(HRender render, ICollider collider, HRay ray, int rayLifeTime)
        {
            return Color.Red;
        }

        public static Color TransparentBlueShader(HRender render, ICollider collider, HRay ray, int rayLifeTime)
        {
            return MixColors(
                Color.Blue, 
                TransparentShader(render, collider, ray, rayLifeTime),
                0.5, 0.5);
        }

        public static Color MirrorBlackShader(HRender render, ICollider collider, HRay ray, int rayLifeTime)
        {
            return MixColors(
                SimpleBlackShader(render, collider, ray, rayLifeTime),
                MirrorShader(render, collider, ray, rayLifeTime),
                0.5, 0.5);
        }
        
        public static Color MirrorRedShader(HRender render, ICollider collider, HRay ray, int rayLifeTime)
        {
            return MixColors(
                SimpleRedShader(render, collider, ray, rayLifeTime),
                MirrorShader(render, collider, ray, rayLifeTime),
                0.5, 0.5);
        }

        public static Color MirrorTransparentBlackShader(HRender render, ICollider collider, HRay ray, int rayLifeTime)
        {
            Color c = MixColors(
                TransparentShader(render, collider, ray, rayLifeTime),
                MirrorShader(render, collider, ray, rayLifeTime),
                0.5, 0.5);

            return MixColors(
                SimpleBlackShader(render, collider, ray, rayLifeTime),
                c,
                0.5, 0.5);
        }

        public static Color MirrorGrayShader(HRender render, ICollider collider, HRay ray, int rayLifeTime)
        {
            return MixColors(
                Color.Gray,
                MirrorShader(render, collider, ray, rayLifeTime),
                0.1, 0.5);
        }

        public static Color MirrorTransparentRedShader(HRender render, ICollider collider, HRay ray, int rayLifeTime)
        {
            Color c = MixColors(
                TransparentShader(render, collider, ray, rayLifeTime),
                MirrorShader(render, collider, ray, rayLifeTime),
                0.5, 0.5);

            return MixColors(
                SimpleRedShader(render, collider, ray, rayLifeTime),
                c,
                0.5, 0.5);
        }

        public static Color ChessShader(HRender render, ICollider collider, HRay ray, int rayLifeTime)
        {
            Point3D cp = collider.CollisionPoint(ray);
            Matrix3D tm = collider.ApplyedTranformation;
            tm.Invert();
            cp = cp*tm;
            return ((int)Math.Round(cp.X) + (int)Math.Round(cp.Y) + (int)Math.Round(cp.Z)) % 2 == 0 ? Color.White : Color.Black;
        }
    }
}