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
    public class PointLight : SceneObject
    {
        public PointLight(Matrix<double> transform, IShader shader, double intensity = 1d) : base(transform)
        {
            Intensity = intensity;
        }

        public IShader Shader { get; set; }

        public double Intensity { get; set; }
        public override ObjectType GetObjectType()
        {
            return ObjectType.Light;
        }

        public override bool Intersects(Ray ray, out double t)
        {
            throw new NotImplementedException();
        }

        public override RenderColor Shade(Context context, TraceResult trace)
        {
            return Shader.RunShader(this, context, trace) * Intensity;
        }

        public override Vector<double> GetNormal(Vector<double> point)
        {
            throw new NotImplementedException();
        }
    }
}
