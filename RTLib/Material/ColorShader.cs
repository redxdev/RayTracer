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
    public class ColorShader : IShader
    {
        public ColorShader(RenderColor color)
        {
            Color = color;
        }

        public RenderColor Color { get; set; }

        public RenderColor RunShader(Spatial spatial, Context context, TraceResult trace)
        {
            return Color;
        }
    }
}
