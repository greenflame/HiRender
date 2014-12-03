using System.Windows.Media.Media3D;

namespace HiMath
{
    public class HGeometry
    {
        public static bool IsPointOnRayExcludeSource(HRay ray, Point3D p)
        {
            return IsPointOnRayExcludeSource(ray.Source, ray.Direction, p);
        }

        public static bool IsPointOnRayExcludeSource(Point3D raySource, Vector3D rayDirection, Point3D p)
        {
            if (!IsPointOnLine(raySource, raySource + rayDirection, p))
                return false;

            return HAccuracy.DoubleGreater(Vector3D.DotProduct(p - raySource, rayDirection), 0);
        }

        public static Vector3D ReflectVector(Vector3D mirrorNormal, Vector3D v)
        {
            Vector3D upVector = Vector3D.CrossProduct(mirrorNormal, v);
            if (HAccuracy.DoubleEqual(upVector.Length, 0)) //colinear vector and mirror
                return -v;
            Vector3D lineVector = Vector3D.CrossProduct(mirrorNormal, upVector);
            var l0 = new Point3D(0, 0, 0);
            Point3D l1 = l0 + lineVector;
            Point3D p = l0 + v;
            Point3D projection = ProjectPointOnLine(l0, l1, p);
            return v + (projection - p)*2;
        }

        public static Point3D ProjectPointOnLine(Point3D l0, Point3D l1, Point3D p)
        {
            if (IsPointOnLine(l0, l1, p))
                return p;

            double distanceToLine = Vector3D.CrossProduct(l1 - l0, p - l0).Length/(l1 - l0).Length;
            Vector3D upVector = Vector3D.CrossProduct(l1 - l0, p - l0);
            Vector3D mooveVector = Vector3D.CrossProduct(upVector, l1 - l0);
            mooveVector.Normalize();
            mooveVector *= distanceToLine;

            return IsPointOnLine(l0, l1, p + mooveVector) ? p + mooveVector : p - mooveVector;
        }

        public static bool IsPointOnLine(Point3D l0, Point3D l1, Point3D p)
        {
            return HAccuracy.DoubleEqual(TriangleSquare(l0, l1, p), 0);
        }

        public static double TriangleSquare(Point3D t0, Point3D t1, Point3D t2)
        {
            return Vector3D.CrossProduct(t1 - t0, t2 - t0).Length/2;
        }

        public static bool IsPointInTriangle(Point3D t0, Point3D t1, Point3D t2, Point3D p)
        {
            double s = TriangleSquare(t0, t1, t2);
            double s1 = TriangleSquare(p, t1, t2);
            double s2 = TriangleSquare(t0, p, t2);
            double s3 = TriangleSquare(t0, t1, p);
            return HAccuracy.DoubleEqual(s, s1 + s2 + s3);
        }

        public static Point3D? IntersectPlaneLineByEquation(Point3D p0, Point3D p1, Point3D p2, Point3D l0, Point3D l1)
        {
            double a = new Matrix3D(
                1, p0.Y, p0.Z, 0,
                1, p1.Y, p1.Z, 0,
                1, p2.Y, p2.Z, 0,
                0, 0, 0, 1
                ).Determinant; //coefficients A, B, C, D of plane Ax + By + Cz + D = 0

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

            double k = l1.X - l0.X; //finding: k, l, m, x0, y0, z0 of line x = tk + x0; y = tl + y0; z = tm + z0;
            double l = l1.Y - l0.Y;
            double m = l1.Z - l0.Z;

            double x0 = l0.X;
            double y0 = l0.Y;
            double z0 = l0.Z;

            if (HAccuracy.DoubleEqual(a*k + b*l + c*m, 0)) //if no intersection, return null
                return null;

            double t = -(d + a*x0 + b*y0 + c*z0)/(a*k + b*l + c*m); //finding t and poin of intersection

            var result = new Point3D(t*k + x0, t*l + y0, t*m + z0);

            return result;
        }
    }
}