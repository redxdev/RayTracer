using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using RTLib.Scene;
using RTLib.Render;
using RTLib.Util;

namespace RTLib.Material
{
    public class UVShader : IShader
    {
        public RenderColor RunShader(Spatial spatial, Context context, TraceResult trace)
        {
            SceneObject obj = spatial as SceneObject;
            if (obj == null)
                throw new InvalidCastException("UVShader can only be applied to SceneObjects");

            Vector<double> uv = obj.GetUV(trace.Intersection);
            return new RenderColor(uv[0], uv[1], 0);
        }
    }
}
