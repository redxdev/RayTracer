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
        }

        public LinkedList<SceneObject> Objects { get; set; }
    }
}
