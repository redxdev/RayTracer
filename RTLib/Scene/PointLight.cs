using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using RTLib.Material;
using RTLib.Render;
using RTLib.Util;

namespace RTLib.Scene
{
    public class PointLight : Light
    {
        public PointLight(Matrix<double> transform, IShader shader, double intensity = 1d) : base(transform)
        {
            Intensity = intensity;
            Shader = shader;
        }

        public IShader Shader { get; set; }

        public double Intensity { get; set; }
        public override ObjectType GetObjectType()
        {
            return ObjectType.Light;
        }

        public override RenderColor ShadeLight(Context context, Ray ray)
        {
            TraceResult trace = new TraceResult()
            {
                Raycast = ray
            };

            return Shader.RunShader(this, context, trace) * Intensity;
        }
    }
}
