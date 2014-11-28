namespace HiRender
{
    internal class HPoint
    {
        public HPoint(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public HPoint()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public static HPoint operator +(HPoint a, HPoint b)
        {
            return new HPoint(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static HPoint operator -(HPoint a, HPoint b)
        {
            return new HPoint(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static HPoint operator -(HPoint a)
        {
            return new HPoint(-a.X, -a.Y, -a.Z);
        }

        public static HPoint operator *(HPoint a, double k)
        {
            return new HPoint(a.X*k, a.Y*k, a.Z*k);
        }

        public static HPoint operator /(HPoint a, double k)
        {
            return new HPoint(a.X/k, a.Y/k, a.Z/k);
        }

        public static double operator %(HPoint a, HPoint b)
        {
            return a.X*b.X + a.Y*b.Y + a.Z*b.Z;
        }

        public static HPoint operator *(HPoint a, HPoint b)
        {
            return new HPoint(
                a.Y*b.Z - a.Z*b.Y,
                a.Z*b.X - a.X*b.Z,
                a.X*b.Y - a.Y*b.X
                );
        }

        public double Length()
        {
            return HAccuracy.MySqrt(this%this);
        }

        public override string ToString()
        {
            return X.ToString() + ' ' + Y + ' ' + Z;
        }
    }
}