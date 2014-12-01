using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiRender
{
    class HShaders
    {
        public delegate Color Shader(HRender render, ICollider collider, HRay ray);

        public static Color SimpleBlackShader(HRender render, ICollider collider, HRay ray)
        {
            return Color.Black;
        }
        public static Color SimpleRedShader(HRender render, ICollider collider, HRay ray)
        {
            return Color.Red;
        }
    }
}
