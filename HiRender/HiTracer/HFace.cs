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

        public bool DetectCollision(HRay ray)
        {
            bool intersectionExists;
            Point3D intersectionPoint = HGeometry.IntersectPlaneLineByEquation(A, B, C, ray.Source,
                ray.Source + ray.Direction, out intersectionExists);

            return intersectionExists && HGeometry.IsPointOnRayExcludeSource(ray, intersectionPoint) &&
                   HGeometry.IsPointInTriangle(A, B, C, intersectionPoint);
                //intersection exists and visible and in triangle
        }

        public Point3D CollisionPoint(HRay ray)
        {
            bool intersectionExists;
            Point3D intersectionPoint = HGeometry.IntersectPlaneLineByEquation(A, B, C, ray.Source,
                ray.Source + ray.Direction, out intersectionExists);

            return intersectionPoint;
        }

        public HRay PassRay(HRay ray)
        {
            return new HRay(CollisionPoint(ray), ray.Direction);
        }

        public HRay ReflectRay(HRay ray)
        {
            return new HRay(CollisionPoint(ray), HGeometry.ReflectVector(CollisionNormal(ray), ray.Direction));
        }

        public Vector3D CollisionNormal(HRay ray)
        {
            return Vector3D.CrossProduct(A - B, C - B);
        }

        public ICollider Transform(Matrix3D matrix)
        {
            return new HFace(
                A*matrix,
                B*matrix,
                C*matrix,
                Shader
                );
        }
    }
}