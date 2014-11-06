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
    public class DelegateShader : IShader
    {
        private Func<Spatial, Context, TraceResult, RenderColor> _func;

        public DelegateShader(Func<Spatial, Context, TraceResult, RenderColor> func)
        {
            _func = func;
        }

        public RenderColor RunShader(Spatial spatial, Context context, TraceResult trace)
        {
            return _func(spatial, context, trace);
        }
    }
}
