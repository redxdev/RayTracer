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
    public abstract class Light :Spatial
    {
        protected Light(Matrix<double> transform) : base(transform)
        {
        }

        public abstract bool CanShade(Context context, Ray ray);

        public abstract RenderColor Shade(Context context, Ray ray);
    }
}
