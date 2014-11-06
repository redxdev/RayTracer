using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using RTLib.Render;
using RTLib.Util;

namespace RTLib.Scene
{
    public abstract class Light : SceneObject
    {
        protected Light(Matrix<double> transform) : base(transform)
        {
        }
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
            throw new NotImplementedException();
        }

        public override Vector<double> GetNormal(Vector<double> point)
        {
            throw new NotImplementedException();
        }

        public abstract RenderColor ShadeLight(Context context, Ray ray);
    }
}
