using System.Windows.Media.Media3D;
using HiTracer;

namespace HiScene
{
    public class HPrimitives
    {
        public static HModel PlaneXy(HShaders.Shader shader, double size)
        {
            var plane = new HModel();
            plane.Colliders.Add(new HFace(new Point3D(1*size, 1*size, 0), new Point3D(-1*size, 1*size, 0),
                new Point3D(-1*size, -1*size, 0), shader));
            plane.Colliders.Add(new HFace(new Point3D(1*size, 1*size, 0), new Point3D(1*size, -1*size, 0),
                new Point3D(-1*size, -1*size, 0), shader));
            return plane;
        }

        public static HModel Cube(HShaders.Shader shader)
        {
            var cube = new HModel();

            HModel f1 = PlaneXy(shader, 1);

            f1.TransformationMatrix = new Matrix3D();
            f1.TransformationMatrix.Rotate(new Quaternion(new Vector3D(1, 0, 0), 90));
            f1.TransformationMatrix.Translate(new Vector3D(0, -1, 0));
            cube.Colliders.AddRange(f1.ApplyTransform());

            f1.TransformationMatrix = new Matrix3D();
            f1.TransformationMatrix.Rotate(new Quaternion(new Vector3D(1, 0, 0), 90));
            f1.TransformationMatrix.Translate(new Vector3D(0, 1, 0));
            cube.Colliders.AddRange(f1.ApplyTransform());

            f1.TransformationMatrix = new Matrix3D();
            f1.TransformationMatrix.Translate(new Vector3D(0, 0, -1));
            cube.Colliders.AddRange(f1.ApplyTransform());

            f1.TransformationMatrix = new Matrix3D();
            f1.TransformationMatrix.Translate(new Vector3D(0, 0, 1));
            cube.Colliders.AddRange(f1.ApplyTransform());

            f1.TransformationMatrix = new Matrix3D();
            f1.TransformationMatrix.Rotate(new Quaternion(new Vector3D(0, 1, 0), 90));
            f1.TransformationMatrix.Translate(new Vector3D(-1, 0, 0));
            cube.Colliders.AddRange(f1.ApplyTransform());

            f1.TransformationMatrix = new Matrix3D();
            f1.TransformationMatrix.Rotate(new Quaternion(new Vector3D(0, 1, 0), 90));
            f1.TransformationMatrix.Translate(new Vector3D(1, 0, 0));
            cube.Colliders.AddRange(f1.ApplyTransform());

            return cube;
        }
    }
}