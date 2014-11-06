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
    public class SurfaceShader : IShader
    {
        public SurfaceShader(double diffuse, double specular, IShader subshader)
        {
            Diffuse = diffuse;
            Specular = specular;
            Subshader = subshader;
        }

        public double Diffuse { get; set; }

        public double Specular { get; set; }

        public IShader Subshader { get; set; }

        public RenderColor RunShader(Spatial spatial, Context context, TraceResult trace)
        {
            SceneObject obj = spatial as SceneObject;
            if (obj == null)
                throw new InvalidCastException("DiffuseShader can only be applied to SceneObjects");

            Vector<double> normal = obj.GetNormal(trace.Intersection);

            RenderColor localColor = Subshader.RunShader(obj, context, trace);
            RenderColor finalColor = new RenderColor(0, 0, 0);

            foreach (Light light in context.Graph.Lights)
            {
                Vector<double> rdir = Vector<double>.Build.DenseOfArray(new double[] {0, 0, 0, 1});
                rdir *= light.Transform;
                rdir = rdir - trace.Intersection;

                double factor = normal.DotProduct(rdir);
                if (factor <= 0)
                    continue;

                Ray ray = new Ray(trace.Intersection, rdir, trace.Raycast.Recursion + 1);
                if (!light.CanShade(context, ray))
                    continue;

                RenderColor lightColor = light.Shade(context, ray);

                finalColor += new RenderColor(
                    Diffuse*factor*lightColor.R,
                    Diffuse*factor*lightColor.G,
                    Diffuse*factor*lightColor.B) + new RenderColor(
                        (1 - Diffuse)*localColor.R,
                        (1 - Diffuse)*localColor.G,
                        (1 - Diffuse)*localColor.B);

                Vector<double> refl = rdir - 2d*rdir.DotProduct(normal)*normal;
                double rd = trace.Raycast.Direction.DotProduct(refl);
                if(rd > 0)
                {
                    double spec = Math.Pow(rd, 20) * Specular;
                    finalColor += spec * lightColor * localColor;
                }
            }

            return finalColor;
        }
    }
}
