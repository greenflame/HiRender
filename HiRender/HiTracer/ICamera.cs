using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HiTracer
{
    public interface ICamera
    {
        Bitmap EmitRays(HTracer.TraseRayFunction traseRayFunction, Label progress);

        int ImageWidth { get; set; }
        int ImageHeight { get; set; }

        int RayLifeTime { get; set; }
        Color BackgrundColor { get; set; }
        double RayPerPixel { get; set; }
    }
}
