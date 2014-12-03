using System.Collections.Generic;
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
    }
}