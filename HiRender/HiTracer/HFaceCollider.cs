using System.Windows.Media.Media3D;
using HiMath;

namespace HiTracer
{
    public class HFaceCollider : ICollider
    {
        public HFaceCollider(Point3D a, Point3D b, Point3D c, HShaders.Shader shader)
        {
            A = a;
            B = b;
            C = c;
            Shader = shader;

            ApplyedTranformation = new Matrix3D();
        }

        public HFaceCollider(Point3D a, Point3D b, Point3D c, HShaders.Shader shader, Matrix3D applyedTransforation)
        {
            A = a;
            B = b;
            C = c;
            Shader = shader;

            ApplyedTranformation = applyedTransforation;
        }

        public Point3D A { get; set; } //triangle coordinates
        public Point3D B { get; set; }
        public Point3D C { get; set; }
        public HShaders.Shader Shader { get; set; }

        public bool DetectCollision(HRay ray)
        {
            Point3D intersectionPoint;
            bool intersectionExists = HGeometry.IntersectPlaneLineByEquation(A, B, C, ray.Source,
                ray.Source + ray.Direction, out intersectionPoint);

            return intersectionExists && HGeometry.IsPointOnRayExcludeSource(ray, intersectionPoint) &&
                   HGeometry.IsPointInTriangle(A, B, C, intersectionPoint);
                //intersection exists and visible and in triangle
        }

        public Point3D CollisionPoint(HRay ray)
        {
            Point3D intersectionPoint;
            HGeometry.IntersectPlaneLineByEquation(A, B, C, ray.Source,
                ray.Source + ray.Direction, out intersectionPoint);

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
            Vector3D tmp = Vector3D.CrossProduct(A - B, C - B);

            if (Vector3D.AngleBetween(tmp, ray.Direction) < 90)
                tmp = -tmp;

            return tmp;
        }

        public ICollider Transform(Matrix3D matrix)
        {
            return new HFaceCollider(
                A*matrix,
                B*matrix,
                C*matrix,
                Shader,
                ApplyedTranformation*matrix
                );
        }

        public Matrix3D ApplyedTranformation { get; private set; }
    }
}