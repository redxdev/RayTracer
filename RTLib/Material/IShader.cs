using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTLib.Render;
using RTLib.Scene;
using RTLib.Util;

namespace RTLib.Material
{
    public interface IShader
    {
        RenderColor RunShader(Spatial spatial, Context context, TraceResult trace);
    }
}
