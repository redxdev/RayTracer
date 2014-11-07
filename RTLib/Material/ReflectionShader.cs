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
    public class ReflectionShader : IShader
    {
        public ReflectionShader(double reflectitity, IShader subshader)
        {
            Reflectivity = reflectitity;
            Subshader = subshader;
        }

        public double Reflectivity { get; set; }

        public IShader Subshader { get; set; }

        public RenderColor RunShader(Spatial spatial, Context context, TraceResult trace)
        {
            SceneObject obj = spatial as SceneObject;
            if (obj == null)
                throw new InvalidCastException("ReflectionShader can only be applied to SceneObjects");

            Vector<double> normal = obj.GetNormal(trace.Intersection);

            Vector<double> r = trace.Raycast.Direction - 2d*trace.Raycast.Direction.DotProduct(normal)*normal;
            r /= r.Norm(2d);

            Ray ray = trace.Raytracer.CreateRay(trace.Intersection, r, trace.Raycast);
            TraceResult? tr = trace.Raytracer.Trace(ray);

            RenderColor rcol;
            if (tr == null)
            {
                rcol = context.BackgroundColor;
            }
            else
            {
                rcol = tr.Value.HitObject.Shade(context, tr.Value);
            }

            RenderColor lcol = Subshader.RunShader(obj, context, trace);

            return Reflectivity*lcol*rcol + (1 - Reflectivity)*lcol;
        }
    }
}
