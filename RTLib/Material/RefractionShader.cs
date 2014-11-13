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
    public class RefractionShader : IShader
    {
        public RefractionShader(double refractionIndex, IShader subshader)
        {
            RefractionIndex = refractionIndex;
            Subshader = subshader;
        }

        public double RefractionIndex { get; set; }

        public IShader Subshader { get; set; }

        public RenderColor RunShader(Spatial spatial, Context context, TraceResult trace)
        {
            SceneObject obj = spatial as SceneObject;
            if (obj == null)
                throw new InvalidCastException("RefractionShader can only be applied to SceneObjects");

            double n = trace.Raycast.RefractionIndex/RefractionIndex;
            int traceResult = trace.HitType == TraceHit.Hit ? 1 : -1;
            Vector<double> N = obj.GetNormal(trace.Intersection)*(double)traceResult;
            double cosI = -N.DotProduct(trace.Raycast.Direction);
            double cosT2 = 1d - n*n*(1d - cosI*cosI);
            if (cosT2 > 0d)
            {
                Vector<double> T = (n*trace.Raycast.Direction) + (n*cosI - Math.Sqrt(cosT2))*N;
                Ray refractRay = trace.Raytracer.CreateRay(trace.Intersection + T*Double.Epsilon, T, trace.Raycast);
                refractRay.RefractionIndex = RefractionIndex;
                TraceResult? result = trace.Raytracer.Trace(refractRay, true);
                if (result != null)
                {
                    RenderColor refractColor = result.Value.HitObject.Shade(context, result.Value);
                    RenderColor localColor = Subshader.RunShader(spatial, context, trace);
                    RenderColor absorbance = localColor*0.15d*-result.Value.T;
                    RenderColor transparency = new RenderColor(Math.Exp(absorbance.R), Math.Exp(absorbance.G), Math.Exp(absorbance.B));
                    return refractColor*transparency;
                }
            }

            return Subshader.RunShader(spatial, context, trace);
        }
    }
}
