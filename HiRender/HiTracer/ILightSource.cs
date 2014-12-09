using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Media3D;

namespace HiTracer
{
    public interface ILightSource
    {
        double Strength { get; set; }
        Color LightColor { get; set; }
        HLightSchemes.HLightScheme LightScheme { get; set; }

        /// <summary>
        /// Detecting lightness in point.
        /// </summary>
        /// <param name="point">Point to detect.</param>
        /// <param name="rayOnCamera">Ray, directed to camera.</param>
        /// <param name="normal">Normal in point.</param>
        /// <param name="colliders">Scene colliders(for detecting shadows).</param>
        /// <returns></returns>
        double LightnessInPoint(Point3D point, Vector3D rayOnCamera, Vector3D normal, List<ICollider> colliders);
    }
}