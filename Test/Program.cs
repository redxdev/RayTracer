using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using RTLib.Render;
using RTLib.Render.Targets;
using RTLib.Scene;
using RTLib.Util;

namespace RTTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Context rc = new Context();

            Matrix<double> cameraMatrix = Transformation.Translate(0, 0, 5);
            rc.RenderCamera = new Camera(cameraMatrix, 30d);

            SceneGraph graph = new SceneGraph();
            Matrix<double> om = Transformation.Translate(0, 0, -5);
            graph.Objects.AddLast(new Sphere(om, 1d));

            LinkedList<IRenderTarget> targets = new LinkedList<IRenderTarget>();
            targets.AddLast(new BitmapTarget("test.bmp"));

            Renderer renderer = new Renderer(rc, graph, targets);
            renderer.Render();

            Console.ReadKey();
        }
    }
}
