using System.Windows.Media.Media3D;

namespace HiRender
{
    internal class HGeometry
    {
        public static HPoint OldIntersectPlaneLine(HPoint p0, HPoint p1, HPoint p2, HPoint l0, HPoint l1)
        {
            Point3D tmp = OldIntersectPlaneLine(
                new[]
                {
                    new Point3D(p0.X, p0.Y, p0.Z),
                    new Point3D(p1.X, p1.Y, p1.Z),
                    new Point3D(p2.X, p2.Y, p2.Z)
                },
                new[]
                {
                    new Point3D(l0.X, l0.Y, l0.Z),
                    new Point3D(l1.X, l1.Y, l1.Z)
                });

            return new HPoint(tmp.X, tmp.Y, tmp.Z);
        }

        public static Point3D OldIntersectPlaneLine(Point3D[] plane, Point3D[] line) // 9/10
        {
            //coefficients A,B,C,D of plane Ax + By + Cz + D = 0
            double A = new Matrix3D(
                1, plane[0].Y, plane[0].Z, 0,
                1, plane[1].Y, plane[1].Z, 0,
                1, plane[2].Y, plane[2].Z, 0,
                0, 0, 0, 1
                ).Determinant;

            double B = new Matrix3D(
                plane[0].X, 1, plane[0].Z, 0,
                plane[1].X, 1, plane[1].Z, 0,
                plane[2].X, 1, plane[2].Z, 0,
                0, 0, 0, 1
                ).Determinant;

            double C = new Matrix3D(
                plane[0].X, plane[0].Y, 1, 0,
                plane[1].X, plane[1].Y, 1, 0,
                plane[2].X, plane[2].Y, 1, 0,
                0, 0, 0, 1
                ).Determinant;

            double D = -new Matrix3D(
                plane[0].X, plane[0].Y, plane[0].Z, 0,
                plane[1].X, plane[1].Y, plane[1].Z, 0,
                plane[2].X, plane[2].Y, plane[2].Z, 0,
                0, 0, 0, 1
                ).Determinant;

            //finding: k, l, m, x0, y0, z0 of line x = tk + x0; y = tl + y0; z = tm + z0;

            double k = line[1].X - line[0].X;
            double l = line[1].Y - line[0].Y;
            double m = line[1].Z - line[0].Z;

            double x0 = line[0].X;
            double y0 = line[0].Y;
            double z0 = line[0].Z;

            //finding t and poin of intersection

            double t = -(D + A*x0 + B*y0 + C*z0)/(A*k + B*l + C*m);

            var result = new Point3D(t*k + x0, t*l + y0, t*m + z0);

            return result;
        }
    }
}