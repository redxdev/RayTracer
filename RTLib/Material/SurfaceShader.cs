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
            RenderColor finalColor = new RenderColor(
                        (1 - Diffuse) * localColor.R,
                        (1 - Diffuse) * localColor.G,
                        (1 - Diffuse) * localColor.B);

            foreach (Light light in context.Graph.Lights)
            {
                Vector<double> ldir = Vector<double>.Build.DenseOfArray(new double[] {0, 0, 0, 1});
                ldir *= light.Transform;
                ldir = ldir - trace.Intersection;
                ldir /= ldir.Norm(2d);

                double factor = normal.DotProduct(ldir);
                if (factor <= 0)
                    continue;

                Ray ray = trace.Raytracer.CreateRay(trace.Intersection, ldir, trace.Raycast);
                if (!light.CanShade(context, ray))
                    continue;

                // shadows
                Vector<double> lorigin = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 0, 1 });
                lorigin *= light.Transform;
                Ray shadowRay = trace.Raytracer.CreateRay(lorigin, -ldir, trace.Raycast);
                double tThis = 0;
                double tClosest = double.MaxValue;
                foreach(SceneObject other in context.Graph.Objects)
                {
                    double t = 0;
                    if (other.Intersects(shadowRay, out t))
                    {
                        if (other == obj)
                            tThis = t;
                        else if (t < tClosest)
                            tClosest = t;
                    }
                }

                if (tThis > tClosest)
                    continue; // whoops, in a shadow!

                // diffuse
                RenderColor lightColor = light.Shade(context, ray);

                finalColor += new RenderColor(
                    Diffuse*factor*lightColor.R,
                    Diffuse*factor*lightColor.G,
                    Diffuse*factor*lightColor.B);

                // specular
                Vector<double> refl = ldir - 2d*ldir.DotProduct(normal)*normal;
                refl /= refl.Norm(2d);

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
