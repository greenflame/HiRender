using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Media.Media3D;
using HiTracer;

namespace HiScene
{
    public class HModel
    {
        private readonly List<ICollider> _colliders;

        public Matrix3D TransformationMatrix;

        public HModel()
        {
            _colliders = new List<ICollider>();
            TransformationMatrix = new Matrix3D();
        }

        public List<ICollider> Colliders
        {
            get { return _colliders; }
        }

        public List<ICollider> ApplyTransform()
        {
            return _colliders.Select(collider => collider.Transform(TransformationMatrix)).ToList();
        }

        public static HModel Load(string fileName)
        {
            var model = new HModel();

            string[] lines = File.ReadAllLines(fileName);

            List<Point3D> vertexes = new List<Point3D>();

            foreach (string line in lines)
            {
                string[] tmp = line.Split(' ');

                if (tmp[0] == "v")
                {
                    double d1 = Double.Parse(tmp[1], CultureInfo.InvariantCulture);
                    double d2 = Double.Parse(tmp[2], CultureInfo.InvariantCulture);
                    double d3 = Double.Parse(tmp[3], CultureInfo.InvariantCulture);

                    vertexes.Add(new Point3D(d1,d2,d3));
                }

                if (tmp[0] == "f")
                {
                    for (int i = 2; i < tmp.Count() - 1; i++)
                        model.Colliders.Add(new HFaceCollider(
                            vertexes[Convert.ToInt32(tmp[1]) - 1],
                            vertexes[Convert.ToInt32(tmp[i]) - 1],
                            vertexes[Convert.ToInt32(tmp[i + 1]) - 1],
                            HShaders.DiffuseGrayShader
                            ));
                }
            }

            return model;
        }
    }
}