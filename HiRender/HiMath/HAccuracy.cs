using System;

namespace HiMath
{
    public class HAccuracy
    {
        public static double Epsilon = 1e-9;

        public static double Sqr(double a)
        {
            return a*a;
        }

        public static bool DoubleEqual(double a, double b)
        {
            return Math.Abs(a - b) < Epsilon;
        }

        public static bool DoubleLessOrEqual(double a, double b)
        {
            return a < b || DoubleEqual(a, b);
        }

        public static bool DoubleLess(double a, double b)
        {
            return a < b && !DoubleEqual(a, b);
        }

        public static bool DoubleGreaterOrEqual(double a, double b)
        {
            return a > b || DoubleEqual(a, b);
        }

        public static bool DoubleGreater(double a, double b)
        {
            return a > b && !DoubleEqual(a, b);
        }

        public static double MySqrt(double a)
        {
            if (DoubleLess(a, 0))
            {
                throw new Exception("sqrt(-1)");
            }
            if (a < 0) return 0;
            return Math.Sqrt(a);
        }
    }
}