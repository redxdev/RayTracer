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
        private Func<SceneObject, Context, TraceInfo, RenderColor> _func;

        public DelegateShader(Func<SceneObject, Context, TraceInfo, RenderColor> func)
        {
            _func = func;
        }

        public RenderColor RunShader(SceneObject obj, Context context, TraceInfo trace)
        {
            return _func(obj, context, trace);
        }
    }
}
