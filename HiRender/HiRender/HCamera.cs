using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiRender
{
    class HCamera
    {
        public HCamera()
        {

        }

        public HCamera(double viewportWidth, double viewportHeight, double viewportDistance)
        {
            ViewportWidth = viewportWidth;
            ViewportHeight = viewportHeight;
            ViewportDistance = viewportDistance;
        }


        public double ViewportWidth { get; set; }
        public double ViewportHeight { get; set; }
        public double ViewportDistance { get; set; }
    }
}
