using System;
using System.Windows.Media.Media3D;

namespace HiTracer
{
    public class HLightSchemes
    {
        public delegate double HLightScheme(Vector3D rayOnLamp, Vector3D rayOnCamera, Vector3D normal);

        public static double Default(Vector3D rayOnLamp, Vector3D rayOnCamera, Vector3D normal)
        {
            return Lambert(rayOnLamp, rayOnCamera, normal);
        }

        public static double Lambert(Vector3D rayOnLamp, Vector3D rayOnCamera, Vector3D normal)
        {
            return Math.Max(0, Math.Cos(Vector3D.AngleBetween(rayOnLamp, normal)/180*Math.PI));
        }
    }
}