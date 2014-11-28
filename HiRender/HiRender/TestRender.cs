using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HiRender
{
    internal class TestRender
    {
        public TestRender()
        {
            ViewportWidth = 2;
            ViewportHeight = 1;
            ViewportDistance = 0.5;

            WidthIterations = 600;
            HeightIterations = 300;
        }

        public double ViewportWidth { get; set; }
        public double ViewportHeight { get; set; }
        public double ViewportDistance { get; set; }

        public int WidthIterations { get; set; }
        public int HeightIterations { get; set; }

        public Color FindCollision(HPoint a, HPoint b)
        {
            //figures

            var sphere = new HPoint(0, 0, -3);
            double radius = 1;

            var plane = new HPoint[3]
            {
                new HPoint(1, -1, 1),
                new HPoint(1, -1, -1),
                new HPoint(-1, -1, 1)
            };

            //figures

            //List<HPoint> collisions

            //if (((b - a)*(sphere - a)).Length()/(a - b).Length() < radius)
            //    return Color.Black;

            HPoint coll = HGeometry.OldIntersectPlaneLine(plane[0], plane[1], plane[2], a, b);

            return ((int)Math.Round(coll.X) + (int)Math.Round(coll.Z)) % 2 == 0 ? Color.White : Color.Black;
        }

        public Bitmap Run(RichTextBox r)
        {
            var bmp = new Bitmap(WidthIterations, HeightIterations);

            for (int y = 0; y < HeightIterations; y++)
                for (int x = 0; x < WidthIterations; x++)
                {
                    var a = new HPoint(0, 0, 0);
                    var b = new HPoint(
                        ViewportWidth/WidthIterations*x - ViewportWidth/2,
                        ViewportHeight/HeightIterations*y - ViewportHeight/2,
                        -ViewportDistance);

                    //r.AppendText(b.ToString() + '\n');

                    bmp.SetPixel(x, y, FindCollision(a, b));
                }

            return bmp;
        }
    }
}