using System.Windows.Media.Media3D;

namespace HiMath
{
    public class HRay
    {
        public HRay(Point3D source, Vector3D direction)
        {
            Source = source;
            Direction = direction;
        }

        public Point3D Source { get; set; }
        public Vector3D Direction { get; set; }
    }
}