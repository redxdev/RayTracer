using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using RTLib.Render;
using RTLib.Util;

namespace RTLib.Scene
{
    public abstract class SceneObject : Spatial
    {
        protected SceneObject(Matrix<double> transform)
            : base(transform)
        {
        }

        public abstract bool Intersects(Ray ray, out double t);

        public abstract RenderColor Shade(Context context, TraceResult trace);

        public abstract Vector<double> GetNormal(Vector<double> point);

        public abstract Vector<double> GetUV(Vector<double> point);
    }
}
