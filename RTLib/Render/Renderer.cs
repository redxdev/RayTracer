using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using RTLib.Render.Targets;
using RTLib.Scene;
using RTLib.Util;

namespace RTLib.Render
{
    public class Renderer
    {
        public Renderer(Context context, SceneGraph graph, LinkedList<IRenderTarget> targets)
        {
            Context = context;
            Graph = graph;
            Targets = targets;
        }

        public Context Context { get; set; }

        public SceneGraph Graph { get; set; }

        public LinkedList<IRenderTarget> Targets { get; set; }

        public void Render()
        {
            Console.WriteLine(string.Format("Starting render with 1 thread", Context.Width, Context.Height));
            Console.WriteLine(Context.ToString());

            Console.WriteLine("Initializing render targets");
            foreach (var target in Targets)
            {
                target.StartRender(this);
            }

            Vector<double> rayOrigin = Vector<double>.Build.DenseOfArray(new double[] {0, 0, 0, 1});
            rayOrigin *= Context.RenderCamera.Transform;

            Console.Write("Rendering... ");

            // the rendering
            for (int j = 0; j < Context.Height; ++j)
            {
                for (int i = 0; i < Context.Width; ++i)
                {
                    if(i == 640 / 2 && j == 480 / 2)
                        Console.WriteLine("There");

                    double x = (2d*((i + 0.5d)/Context.Width) - 1d)*Context.AspectRatio*Context.RenderCamera.Angle;
                    double y = (1d - 2d*((j + 0.5d)/Context.Height))*Context.RenderCamera.Angle;

                    Vector<double> cameraPos = Vector<double>.Build.DenseOfArray(new double[] {x, y, -1, 1});
                    cameraPos *= Context.RenderCamera.Transform;

                    Vector<double> rayDirection = cameraPos - rayOrigin;
                    rayDirection /= (double)rayDirection.Norm(2d);

                    Ray ray = new Ray(rayOrigin, rayDirection, 0, Context.RenderCamera.NearClippingPlane, Context.RenderCamera.FarClippingPlane);
                    Color result = Trace(ray);

                    foreach (var target in Targets)
                    {
                        target.PixelRendered(this, i, j, result);
                    }
                }
            }
            // that's it!

            Console.WriteLine(" done!");

            Console.WriteLine("Finishing render targets");
            foreach (var target in Targets)
            {
                target.FinishRender(this);
            }

            Console.WriteLine("Rendering complete");
        }

        protected Color Trace(Ray ray)
        {
            double tClosest = ray.MaxDistance;
            SceneObject hitObject = null;

            foreach (var obj in Graph.Objects)
            {
                double t = 0;
                if (obj.Intersects(ray, out t))
                {
                    if (t < tClosest && t > ray.MinDistance)
                    {
                        tClosest = t;
                        hitObject = obj;
                    }
                }
            }

            if (hitObject == null)
                return Context.BackgroundColor;

            return hitObject.Shade(Context, ray);
        }
    }
}