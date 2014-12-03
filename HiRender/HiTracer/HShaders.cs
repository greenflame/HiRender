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

        public static Color MirrorTransparentBlackShader(HRender render, ICollider collider, HRay ray)
        {
            Color passedColor = render.TraseRay(collider.PassRay(ray));
            Color reflectedColor = render.TraseRay(collider.ReflectRay(ray));
            Color c = MixColors(passedColor, reflectedColor, 0.5, 0.5);
            return MixColors(Color.Black, c, 0.5, 0.5);
        }

        public static Color MirrorTransparentBlueShader(HRender render, ICollider collider, HRay ray)
        {
            Color passedColor = render.TraseRay(collider.PassRay(ray));
            Color reflectedColor = render.TraseRay(collider.ReflectRay(ray));
            Color c = MixColors(passedColor, reflectedColor, 0.5, 0.5);
            return MixColors(Color.Blue, c, 0.5, 0.5);
        }
    }
}