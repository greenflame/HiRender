using System.Drawing;

namespace HiRender
{
    internal interface ICollider
    {
        HShaders.Shader Shader { get; set; }
        HPoint CollisionPoint(HRay ray);
        HPoint CollisionNormal(HRay ray);
        Color ProcessCollision(HRender render, ICollider collider, HRay ray);
    }
}