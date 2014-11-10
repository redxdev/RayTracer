using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using RTLib.Material.Texture;
using RTLib.Render;
using RTLib.Scene;
using RTLib.Util;

namespace RTLib.Material
{
    public class TextureShader : IShader
    {
        public TextureShader(ITexture texture)
        {
            Texture = texture;
        }

        public ITexture Texture { get; set; }

        public RenderColor RunShader(Spatial spatial, Context context, TraceResult trace)
        {
            SceneObject obj = spatial as SceneObject;
            if(obj == null)
                throw new InvalidCastException("TextureShader can only be applied to SceneObjects");

            Vector<double> uv = obj.GetUV(trace.Intersection);
            return Texture.GetTexel(uv[0], uv[1]);
        }
    }
}
