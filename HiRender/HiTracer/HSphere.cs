using System;
using System.Windows.Media.Media3D;
using HiMath;

namespace HiTracer
{
    public class HSphere : ICollider
    {
        public HSphere(Point3D center, double radius, HShaders.Shader shader)
        {
            Center = center;
            Radius = radius;

            Shader = shader;
        }

        public Point3D Center { get; set; }
        public double Radius { get; set; }
        public HShaders.Shader Shader { get; set; }

        public bool DetectCollision(HRay ray)
        {
            throw new NotImplementedException();
        }

        public Point3D CollisionPoint(HRay ray)
        {
            throw new NotImplementedException();
        }

        public Vector3D CollisionNormal(HRay ray)
        {
            throw new NotImplementedException();
        }

        public HRay PassRay(HRay ray)
        {
            throw new NotImplementedException();
        }

        public HRay ReflectRay(HRay ray)
        {
            throw new NotImplementedException();
        }

        public ICollider Transform(Matrix3D matrix)
        {
            return new HSphere(Center*matrix, Radius, Shader);
        }
    }
}