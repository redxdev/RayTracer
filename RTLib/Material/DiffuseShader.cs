using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using RTLib.Render;
using RTLib.Scene;
using RTLib.Util;

namespace RTLib.Material
{
    public class DiffuseShader : IShader
    {
        public DiffuseShader(double diffuse, double ambient, IShader subshader)
        {
            Diffuse = diffuse;
            Ambient = ambient;
            Subshader = subshader;
        }

        public double Diffuse { get; set; }

        public double Ambient { get; set; }

        public IShader Subshader { get; set; }

        public RenderColor RunShader(SceneObject obj, Context context, TraceResult trace)
        {
            Vector<double> normal = obj.GetNormal(trace.Intersection);

            RenderColor localColor = Subshader.RunShader(obj, context, trace);
            RenderColor finalColor = new RenderColor(0, 0, 0);

            foreach (SceneObject other in context.Graph.Objects)
            {
                if (other.GetObjectType() != ObjectType.Light || other == obj)
                    continue;

                Light light = other as Light;
                if(light == null)
                    throw new InvalidCastException("Object is marked as light but cannot be cast as such");

                Vector<double> rdir = Vector<double>.Build.DenseOfArray(new double[] {0, 0, 0, 1});
                rdir *= light.Transform;
                rdir = rdir - trace.Intersection;

                Ray ray = new Ray(trace.Intersection, rdir, trace.Raycast.Recursion + 1);

                RenderColor lightColor = light.ShadeLight(context, ray);

                double factor = normal.DotProduct(rdir);
                if (factor <= 0)
                    continue;

                finalColor += new RenderColor(
                    Diffuse*factor*lightColor.R,
                    Diffuse*factor*lightColor.G,
                    Diffuse*factor*lightColor.B) + new RenderColor(
                        Ambient*localColor.R,
                        Ambient*localColor.G,
                        Ambient*localColor.B);
            }

            return finalColor;
        }
    }
}
