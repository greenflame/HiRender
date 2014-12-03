using System.Windows.Media.Media3D;
using HiMath;

namespace HiTracer
{
    public interface ICollider
    {
        HShaders.Shader Shader { get; set; }
        bool DetectCollision(HRay ray);
        Point3D CollisionPoint(HRay ray);
        Vector3D CollisionNormal(HRay ray);
        HRay PassRay(HRay ray);
        HRay ReflectRay(HRay ray);
        ICollider Transform(Matrix3D matrix);
    }
}