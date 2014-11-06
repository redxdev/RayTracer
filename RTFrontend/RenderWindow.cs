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
        private Renderer renderer = null;

        private RenderSettingsWindow renderSettingsWindow = null;

        public RenderWindow()
        {
            InitializeComponent();

            renderSettingsWindow = new RenderSettingsWindow(this);
            renderSettingsWindow.Show();
        }

        public void DoRender()
        {
            if (renderer != null)
                return;

            int xres = renderSettingsWindow.XRes;
            int yres = renderSettingsWindow.YRes;

            Bitmap bitmap = new Bitmap(xres, yres);
            pictureBox.Image = bitmap;

            SceneGraph graph = new SceneGraph();

            Matrix<double> om = Transformation.Translate(0, 0, -5);
            graph.Objects.AddLast(new Sphere(om, 3, new DiffuseShader(0.9, 0.1, new ColorShader(new RenderColor(0.5, 0.5, 0.5)))));

            om = Transformation.Translate(0, 5, -3);
            graph.Objects.AddLast(new PointLight(om, new ColorShader(new RenderColor(1, 1, 1)), 0.5));

            Context context = new Context();
            context.Width = xres;
            context.Height = yres;

            Matrix<double> cm = Transformation.Translate(0, 0, 5);
            context.RenderCamera = new Camera(cm, 90);
            context.Graph = graph;
            
            renderer = new Renderer(context);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            renderer.StartRender(renderSettingsWindow.ThreadCount);

            while (!renderer.IsFinished)
            {
                Thread.Sleep(10);
            }

            stopwatch.Stop();
            Console.WriteLine(string.Format("Rendering took {0} seconds", stopwatch.Elapsed.TotalSeconds));

            for (int x = 0; x < xres; ++x)
            {
                for (int y = 0; y < yres; ++y)
                {
                    RenderColor color = renderer.State.Pixels[x, y];
                    System.Drawing.Color bmpColor = System.Drawing.Color.FromArgb(255, color.RByte, color.GByte,
                        color.BByte);
                    bitmap.SetPixel(x, y, bmpColor);
                }
            }

            renderer = null;

            this.Width = xres;
            this.Height = yres;
        }
    }
}
