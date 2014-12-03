using System.Collections.Generic;
using System.Windows.Media.Media3D;
using HiTracer;

namespace HiScene
{
    public class HModel
    {
        private readonly List<ICollider> _colliders;

        public HModel()
        {
            _colliders = new List<ICollider>();
        }

        public List<ICollider> Colliders
        {
            get { return _colliders; }
        }

        public void Transform(Matrix3D matrix)
        {
            foreach (ICollider collider in _colliders)
            {
                collider.Transform(matrix);
            }
        }
    }
}