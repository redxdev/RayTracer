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
        public RenderColor RunShader(Spatial spatial, Context context, TraceResult trace)
        {
            SceneObject obj = spatial as SceneObject;
            if (obj == null)
                throw new InvalidCastException("SurfaceNormalShader can only be applied to SceneObjects!");

            Vector<double> normal = obj.GetNormal(trace.Intersection);

            return new RenderColor(normal[0], normal[1], normal[2]);
        }
    }
}
