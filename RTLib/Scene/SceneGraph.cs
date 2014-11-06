using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTLib.Scene
{
    public class SceneGraph
    {
        public SceneGraph()
        {
            Objects = new LinkedList<SceneObject>();
            Lights = new LinkedList<Light>();
        }

        public LinkedList<SceneObject> Objects { get; set; }

        public LinkedList<Light> Lights { get; set; }
    }
}
