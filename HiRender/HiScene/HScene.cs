using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using HiTracer;

namespace HiScene
{
    public class HScene
    {
        private readonly List<HModel> _models;

        public HScene()
        {
            _models = new List<HModel>();
        }

        public List<HModel> Models
        {
            get { return _models; }
        }

        public void Transform(Matrix3D matrix)
        {
            foreach (HModel model in _models)
            {
                model.Transform(matrix);
            }
        }

        public List<ICollider> ToColliderList()
        {
            List<ICollider> res = new List<ICollider>();
            foreach (HModel model in _models)
            {
                res.AddRange(model.Colliders);
            }
            return res;
        }
    }
}
