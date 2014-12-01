using System;
using System.Drawing;

namespace HiRender
{
    internal class HSphereCollider : ICollider
    {
        public HSphereCollider(HPoint center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public HPoint Center { get; set; }
        public double Radius { get; set; }
        public HShaders.Shader Shader { get; set; }

        public HPoint CollisionPoint(HRay ray)
        {
            throw new NotImplementedException();
        }

        public Color ProcessCollision(HRender render, ICollider collider, HRay ray)
        {
            throw new NotImplementedException();
        }
    }
}