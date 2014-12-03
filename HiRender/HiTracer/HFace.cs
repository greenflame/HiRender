using System.Windows.Media.Media3D;
using HiMath;

namespace HiTracer
{
    public class HFace : ICollider
    {
        public HFace(Point3D a, Point3D b, Point3D c, HShaders.Shader shader)
        {
            A = a;
            B = b;
            C = c;
            Shader = shader;
        }

        public Point3D A { get; set; } //triangle coordinates
        public Point3D B { get; set; }
        public Point3D C { get; set; }
        public HShaders.Shader Shader { get; set; }

        public Point3D? CollisionPoint(HRay ray)
        {
            Point3D? intersectionPoint = HGeometry.IntersectPlaneLineByEquation(A, B, C, ray.Source,
                ray.Source + ray.Direction);

            if (intersectionPoint == null) //no intersection point with plane
                return null;
            if (!HGeometry.IsPointOnRayExcludeSource(ray, intersectionPoint.Value)) //point is invisible
                return null;

            return HGeometry.IsPointInTriangle(A, B, C, intersectionPoint.Value) ? intersectionPoint : null;
        }

        public Vector3D CollisionNormal(HRay ray)
        {
            return Vector3D.CrossProduct(A - B, C - B);
        }

        public void Transform(Matrix3D matrix)
        {
            A *= matrix;
            B *= matrix;
            C *= matrix;
        }
    }
}