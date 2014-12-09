using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Media.Media3D;
using HiMath;

namespace HiTracer
{
    public class HPointLightSource : ILightSource
    {
        public HPointLightSource(Point3D position)
        {
            Position = position;
            Strength = 1;
            LightColor = Color.White;
            LightScheme = HLightSchemes.Default;
        }

        public HPointLightSource(Point3D position, double strength, Color lightColor, HLightSchemes.HLightScheme lightScheme)
        {
            Position = position;
            Strength = strength;
            LightColor = lightColor;
            LightScheme = lightScheme;
        }

        public Point3D Position { get; set; }
        public double Strength { get; set; }
        public Color LightColor { get; set; }

        public HLightSchemes.HLightScheme LightScheme { get; set; }

        public bool IsPointInShadow(Point3D point, List<ICollider> colliders)
        {
            var r1 = new HRay(Position, point - Position);
            var r2 = new HRay(point, Position - point);

            return colliders.Any(
                    collider =>
                        collider.DetectCollision(r1) &&
                        HGeometry.IsPointOnRayExcludeSource(r2, collider.CollisionPoint(r1)));
        }

        public double LightnessInPoint(Point3D point, Vector3D rayOnCamera, Vector3D normal, List<ICollider> colliders)
        {
            return IsPointInShadow(point, colliders) ? 0 : LightScheme(Position - point, rayOnCamera, normal);
        }
    }
}