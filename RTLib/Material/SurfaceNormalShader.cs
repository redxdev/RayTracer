using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using RTLib.Render;
using RTLib.Scene;
using RTLib.Util;

namespace RTLib.Material
{
    public class SurfaceNormalShader : IShader
    {
        public RenderColor RunShader(SceneObject obj, Context context, TraceInfo trace)
        {
            Vector<double> normal = obj.GetNormal(trace.Intersection);
            return new RenderColor(normal[0], normal[1], normal[2]);
        }
    }
}
