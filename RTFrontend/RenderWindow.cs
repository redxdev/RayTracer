using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;
using RTLib.Material;
using RTLib.Render;
using RTLib.Scene;
using RTLib.Util;
using Color = RTLib.Util.RenderColor;

namespace RTFrontend
{
    public partial class RenderWindow : Form
    {
        public const int XRes = 1600;
        public const int YRes = 900;

        private Renderer renderer = null;

        public RenderWindow()
        {
            InitializeComponent();
        }

        private void renderButton_Click(object sender, EventArgs e)
        {
            if (renderer != null)
                return;

            Bitmap bitmap = new Bitmap(XRes, YRes);
            pictureBox.Image = bitmap;

            SceneGraph graph = new SceneGraph();

            DelegateShader normalShader = new DelegateShader((obj, ctx, trace) =>
            {
                Vector<double> normal = obj.GetNormal(trace.Intersection);
                return new RenderColor(normal[0], normal[1], normal[2]);
            });

            Matrix<double> om = Transformation.Translate(0, 0, -5);
            graph.Objects.AddLast(new Sphere(om, 1, normalShader));

            om = Transformation.Translate(0.4, 0, -6)*Transformation.Scale(1, 2, 1);
            graph.Objects.AddLast(new Sphere(om, 1, new ColorShader(new RenderColor(0, 0, 0))));

            om = Transformation.Translate(-2, 0, -4)*Transformation.Scale(0.9, 2, 1.2);
            graph.Objects.AddLast(new Sphere(om, 1, new ColorShader(new RenderColor(0, 0.4, 1))));

            om = Transformation.Translate(0, 0, 0);
            graph.Objects.AddLast(new Plane(om, new ColorShader(new RenderColor(0.5, 0.5, 0))));

            Context context = new Context();
            context.Width = XRes;
            context.Height = YRes;

            Matrix<double> cm = Transformation.Translate(0, 0, 5);
            context.RenderCamera = new Camera(cm, 90);
            
            renderer = new Renderer(context, graph);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            renderer.StartRender(Environment.ProcessorCount);

            while (!renderer.IsFinished)
            {
                Thread.Sleep(10);
            }

            stopwatch.Stop();
            Console.WriteLine(string.Format("Rendering took {0} seconds", stopwatch.Elapsed.TotalSeconds));

            for (int x = 0; x < XRes; ++x)
            {
                for (int y = 0; y < YRes; ++y)
                {
                    RenderColor color = renderer.State.Pixels[x, y];
                    System.Drawing.Color bmpColor = System.Drawing.Color.FromArgb(255, color.RByte, color.GByte,
                        color.BByte);
                    bitmap.SetPixel(x, y, bmpColor);
                }
            }

            renderer = null;

            this.Width = XRes;
            this.Height = YRes;
        }
    }
}
