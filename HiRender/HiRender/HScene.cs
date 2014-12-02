using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiRender
{
    class HScene
    {
        public HScene()
        {

        }        

        public HScene(List<ICollider> colliders, List<HLight> lights, HCamera camera)
        {
            _colliders = colliders;
            _lights = lights;
            _camera = camera;
        }

        private List<ICollider> _colliders;
        private List<HLight> _lights;
        private HCamera _camera;

        public List<ICollider> Colliders
        {
            get
            {
                return _colliders;
            }
        }

        public List<HLight> Lights
        {
            get
            {
                return _lights;
            }
        }

        public HCamera Camera
        {
            get
            {
                return _camera;
            }
        }
    }
}
