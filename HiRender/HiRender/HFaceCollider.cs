using System.Drawing;

namespace HiRender
{
    internal class HFaceCollider : ICollider
    {
        public HFaceCollider(HPoint a, HPoint b, HPoint c, HShaders.Shader shader)
        {
            A = a;
            B = b;
            C = c;
            Shader = shader;
        }

        public HPoint A { get; set; } //triangle coordinates
        public HPoint B { get; set; }
        public HPoint C { get; set; }
        public HShaders.Shader Shader { get; set; }

        public HPoint CollisionPoint(HRay ray)
        {
            HPoint intersectionPoint = HGeometry.OldIntersectPlaneLine(A, B, C, ray.Source, ray.Source + ray.Direction);

            if (intersectionPoint == null) //no intersection point with plane
                return null;
            if (!HGeometry.IsPointOnRay(intersectionPoint, ray)) //point is invisible
                return null;

            double firstSquare = HGeometry.TriangleSquare(A, B, C);
            double secondSquare = HGeometry.TriangleSquare(A, B, intersectionPoint) +
                                  HGeometry.TriangleSquare(A, C, intersectionPoint) +
                                  HGeometry.TriangleSquare(C, B, intersectionPoint);

            return HAccuracy.DoubleEqual(firstSquare, secondSquare) ? intersectionPoint : null;
        }

        public Color ProcessCollision(HRender render, ICollider collider, HRay ray)
        {
            return Shader(render, collider, ray);
        }
    }
}