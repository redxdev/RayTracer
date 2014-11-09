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
using RTLib.Flow;
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

        public Renderer Renderer { get { return _renderer; } }

        public void DoRender()
        {
            if (_renderer != null)
                return;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Render Flow (*.rf)|*.rf|All Files (*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("A scene file must be loaded to use the renderer!", "Unable to render scene",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FlowScene flow = null;
            try
            {
                flow = FlowUtilities.ParseFile(dialog.FileName);
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to load scene file: " + e.Message, "Unable to render scene",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(e);
                return;
            }

            int xres = renderSettingsWindow.XRes;
            int yres = renderSettingsWindow.YRes;

            Bitmap bitmap = new Bitmap(xres, yres);
            pictureBox.Image = bitmap;

            Context context = new Context
            {
                Width = xres,
                Height = yres,
                MaxRecursion = renderSettingsWindow.MaxRecursionDepth,
                SampleCount = renderSettingsWindow.SampleCount,
                RenderCamera = flow.BuildCamera(),
                Graph = flow.BuildGraph()
            };

            context.RenderCamera.FieldOfView = renderSettingsWindow.FieldOfView;
            context.RenderCamera.NearClipPlane = renderSettingsWindow.NearClippingPlane;
            context.RenderCamera.FarClipPlane = renderSettingsWindow.FarClippingPlane;

            _renderer = new Renderer(context);

            int pixelsLeft = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            _renderer.StartRender(renderSettingsWindow.ThreadCount, renderSettingsWindow.HaltOnException);
            pixelsLeft = _renderer.State.JobsLeft;

            while (!_renderer.IsFinished)
            {
                int newPixelsLeft = _renderer.State.JobsLeft;
                int pixelsFinished = pixelsLeft - newPixelsLeft;
                pixelsLeft = newPixelsLeft;
                double timeLeft = pixelsLeft/(double) pixelsFinished;
                Console.WriteLine(string.Format(
                    "Status: {0} pixels/second, {1} left to render, ~{2:0.00} seconds left", pixelsFinished, pixelsLeft,
                    timeLeft));

                Thread.Sleep(1000);
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
