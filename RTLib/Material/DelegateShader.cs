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
        private Func<SceneObject, Context, TraceResult, RenderColor> _func;

        public DelegateShader(Func<SceneObject, Context, TraceResult, RenderColor> func)
        {
            _func = func;
        }

        public RenderColor RunShader(SceneObject obj, Context context, TraceResult trace)
        {
            return _func(obj, context, trace);
        }
    }
}
