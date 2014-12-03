using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;
using HiTracer;

namespace HiScene
{
    public class HScene
    {
        private readonly List<HModel> _models;

        public Matrix3D TransformationMatrix;

        public HScene()
        {
            _models = new List<HModel>();
            TransformationMatrix = new Matrix3D();
        }

        public List<HModel> Models
        {
            get { return _models; }
        }

        public List<ICollider> ApplyTransform()
        {
            var res = new List<ICollider>();
            foreach (HModel model in _models)
            {
                res.AddRange(model.ApplyTransform());
            }
            res = res.Select(collider => collider.Transform(TransformationMatrix)).ToList();
            return res;
        }
    }
}