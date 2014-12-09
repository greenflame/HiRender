using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media.Media3D;
using HiMath;

namespace HiTracer
{
    public class HPerspectiveCamera : ICamera
    {
        public HPerspectiveCamera(double viewportWidth, double viewportHeight, double viewportDistance, int  imageWidth, int imageHeight)
        {
            ViewportDistance = viewportDistance;
            ViewportHeight = viewportHeight;
            ViewportWidth = viewportWidth;

            ImageHeight = imageHeight;
            ImageWidth = imageWidth;

            BackgrundColor = Color.Black;
            RayLifeTime = 10;
            RayPerPixel = 1;
        }

        public double ViewportWidth { get; set; }
        public double ViewportHeight { get; set; }
        public double ViewportDistance { get; set; }

        public Bitmap ResizeImage(Bitmap input, int newWidth, int newHeight)
        {
            Bitmap newImage = new Bitmap(newWidth, newHeight);
            using (Graphics gr = Graphics.FromImage(newImage))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(input, new Rectangle(0, 0, newWidth, newHeight));
            }
            return newImage;
        }

        public Bitmap EmitRays(HTracer.TraseRayFunction traseRayFunction, Label progress)
        {
            double k = Math.Sqrt(RayPerPixel);

            var fullWidthIterations = (int) Math.Round(ImageWidth*k);
            var fullHeightIterations = (int) Math.Round(ImageHeight*k);

            var bmp = new Bitmap(fullWidthIterations, fullHeightIterations);

            for (int y = 0; y < fullHeightIterations; y++)
            {
                for (int x = 0; x < fullWidthIterations; x++)
                {
                    var a = new Point3D(0, 0, 0);
                    var b = new Vector3D(
                        ViewportWidth/fullWidthIterations*x - ViewportWidth/2,
                        ViewportHeight/fullHeightIterations*y - ViewportHeight/2,
                        -ViewportDistance);

                    bmp.SetPixel(x, fullHeightIterations - y - 1, traseRayFunction(new HRay(a, b), RayLifeTime));
                }
                progress.Text = (y + 1).ToString() + " / " + fullHeightIterations.ToString();
                Application.DoEvents();
            }

            var res = ResizeImage(bmp, ImageWidth, ImageHeight);
            return res;
        }

        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }

        public int RayLifeTime { get; set; }
        public Color BackgrundColor { get; set; }
        public double RayPerPixel { get; set; }
    }
}