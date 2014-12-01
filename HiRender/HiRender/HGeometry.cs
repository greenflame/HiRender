using System.Windows.Media.Media3D;

namespace HiRender
{
    internal class HGeometry
    {
        public void ProjectPointOnLine(HPoint l0, HPoint l1, HPoint toProject)  //todo in process
        {
            HPoint planeNormal = (l1 - l0)*(toProject - l0);
            HPoint lineNormal = planeNormal*(l1 - l0);
            double l = TriangleSquare(l0, l1, toProject)/(l0 - l1).Length();
        }

        public static HPoint ReflectVector(HPoint vector, HPoint reflectionNormal)  //todo in process
        {
            return null;
        }

        public static bool IsPointOnRay(HPoint p, HRay ray)
        {
            if (!HAccuracy.DoubleEqual(0, ((p - ray.Source)*(ray.Direction)).Length())) //is not on line(colinear)
                return false;

            return HAccuracy.DoubleGreater((p - ray.Source)%(ray.Direction), 0); //is codirectional
        }

        public static double TriangleSquare(HPoint a, HPoint b, HPoint c)
        {
            return ((b - a)*(c - a)).Length()/2;
        }

        public static HPoint OldIntersectPlaneLine(HPoint p0, HPoint p1, HPoint p2, HPoint l0, HPoint l1)
        {
            Point3D tmp = EquationIntersectPlaneLine(
                new Point3D(p0.X, p0.Y, p0.Z),
                new Point3D(p1.X, p1.Y, p1.Z),
                new Point3D(p2.X, p2.Y, p2.Z),
                new Point3D(l0.X, l0.Y, l0.Z),
                new Point3D(l1.X, l1.Y, l1.Z)
                );

            return new HPoint(tmp.X, tmp.Y, tmp.Z);
        }

        public static Point3D EquationIntersectPlaneLine(Point3D p0, Point3D p1, Point3D p2, Point3D l0, Point3D l1)
            //previous render
            //todo no intersection case!
        {
            //coefficients Source,Direction,C,D of plane Ax + By + Cz + D = 0
            double a = new Matrix3D(
                1, p0.Y, p0.Z, 0,
                1, p1.Y, p1.Z, 0,
                1, p2.Y, p2.Z, 0,
                0, 0, 0, 1
                ).Determinant;

            double b = new Matrix3D(
                p0.X, 1, p0.Z, 0,
                p1.X, 1, p1.Z, 0,
                p2.X, 1, p2.Z, 0,
                0, 0, 0, 1
                ).Determinant;

            double c = new Matrix3D(
                p0.X, p0.Y, 1, 0,
                p1.X, p1.Y, 1, 0,
                p2.X, p2.Y, 1, 0,
                0, 0, 0, 1
                ).Determinant;

            double d = -new Matrix3D(
                p0.X, p0.Y, p0.Z, 0,
                p1.X, p1.Y, p1.Z, 0,
                p2.X, p2.Y, p2.Z, 0,
                0, 0, 0, 1
                ).Determinant;

            //finding: k, l, m, x0, y0, z0 of line x = tk + x0; y = tl + y0; z = tm + z0;

            double k = l1.X - l0.X;
            double l = l1.Y - l0.Y;
            double m = l1.Z - l0.Z;

            double x0 = l0.X;
            double y0 = l0.Y;
            double z0 = l0.Z;

            //finding t and poin of intersection

            double t = -(d + a*x0 + b*y0 + c*z0)/(a*k + b*l + c*m);

            var result = new Point3D(t*k + x0, t*l + y0, t*m + z0);

            return result;
        }
    }
}