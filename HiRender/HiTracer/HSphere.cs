using System;
using System.Windows.Media.Media3D;
using HiMath;

namespace HiTracer
{
    public class HSphere : ICollider
    {
        public HSphere(Point3D center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public Point3D Center { get; set; }
        public double Radius { get; set; }
        public HShaders.Shader Shader { get; set; }

        public Point3D? CollisionPoint(HRay ray)
        {
            throw new NotImplementedException();
        }

        public Vector3D CollisionNormal(HRay ray)
        {
            throw new NotImplementedException();
        }

        public void Transform(Matrix3D matrix)
        {
            Center *= matrix;
        }
    }
}