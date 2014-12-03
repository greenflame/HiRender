using System.Windows.Media.Media3D;
using HiMath;

namespace HiTracer
{
    public interface ICollider
    {
        HShaders.Shader Shader { get; set; }
        Point3D? CollisionPoint(HRay ray);
        Vector3D CollisionNormal(HRay ray);
        void Transform(Matrix3D matrix);
    }
}