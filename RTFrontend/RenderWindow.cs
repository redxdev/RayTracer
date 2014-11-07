using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
        private Renderer _renderer = null;

        private RenderSettingsWindow renderSettingsWindow = null;

        public RenderWindow()
        {
            InitializeComponent();

            renderSettingsWindow = new RenderSettingsWindow(this);
            renderSettingsWindow.Show();
        }

        public void DoRender()
        {
            if (_renderer != null)
                return;

            int xres = renderSettingsWindow.XRes;
            int yres = renderSettingsWindow.YRes;

            Bitmap bitmap = new Bitmap(xres, yres);
            pictureBox.Image = bitmap;

            SceneGraph graph = new SceneGraph();

            Matrix<double> om = Transformation.Translate(5, 0, -15);
            graph.Objects.AddLast(new Sphere(om, 3,
                new ReflectionShader(0.3, new SurfaceShader(0.6, 10, new ColorShader(new RenderColor(0.5, 1, 0.5))))));

            om = Transformation.Translate(-5, 0, -9);
            graph.Objects.AddLast(new Sphere(om, 3,
                new ReflectionShader(0.4, new SurfaceShader(0.65, 10, new ColorShader(new RenderColor(0.1, 0.1, 0.1))))));

            om = Transformation.Translate(0, -5, 0);
            graph.Objects.AddLast(new Plane(om,
                new ReflectionShader(0, new SurfaceShader(0.6, 10, new ColorShader(new RenderColor(0.5, 0.5, 0.5))))));

            om = Transformation.Translate(0, 2, 5);
            graph.Lights.AddLast(new PointLight(om, new ColorShader(new RenderColor(1, 1, 1)), 0.6));

            Context context = new Context
            {
                Width = xres,
                Height = yres,
                MaxRecursion = renderSettingsWindow.MaxRecursionDepth,
                SampleCount = renderSettingsWindow.SampleCount
            };

            Matrix<double> cm = Transformation.Translate(0, 0, 5);
            context.RenderCamera = new Camera(cm, renderSettingsWindow.FieldOfView, renderSettingsWindow.NearClippingPlane, renderSettingsWindow.FarClippingPlane);
            context.Graph = graph;

            _renderer = new Renderer(context);

            double lastCheckup = 0d;
            int pixelsLeft = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            _renderer.StartRender(renderSettingsWindow.ThreadCount, renderSettingsWindow.HaltOnException);

            while (!_renderer.IsFinished)
            {
                if(stopwatch.Elapsed.TotalSeconds - lastCheckup > 1d)
                {
                    int newPixelsLeft = _renderer.State.JobsLeft;
                    int pixelsFinished = pixelsLeft - newPixelsLeft;
                    pixelsLeft = newPixelsLeft;
                    lastCheckup = stopwatch.Elapsed.TotalSeconds;
                    Console.WriteLine(string.Format("Status: {0} pixels/second, {1} left to render", pixelsFinished, pixelsLeft));
                }

                Thread.Sleep(10);
            }

            stopwatch.Stop();
            Console.WriteLine(string.Format("Rendering took {0} seconds", stopwatch.Elapsed.TotalSeconds));

            for (int x = 0; x < xres; ++x)
            {
                for (int y = 0; y < yres; ++y)
                {
                    RenderColor color = _renderer.State.Pixels[x, y];
                    System.Drawing.Color bmpColor = System.Drawing.Color.FromArgb(255, color.RByte, color.GByte,
                        color.BByte);
                    bitmap.SetPixel(x, y, bmpColor);
                }
            }

            _renderer = null;

            this.Width = xres;
            this.Height = yres;
        }

        public void SaveBitmap()
        {
            if (pictureBox.Image == null)
            {
                MessageBox.Show("There's no image to save!", "Error saving bitmap", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Bitmap images (*.bmp)|*.bmp|All Files (*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox.Image.Save(dialog.FileName);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Unable to save bitmap", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Saved bitmap", "Save complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
