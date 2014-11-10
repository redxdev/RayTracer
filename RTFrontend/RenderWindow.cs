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
using MathNet.Numerics;
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

        private BackgroundWorker worker = null;

        public RenderWindow()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer,
                true);

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += WorkerProc;
            worker.ProgressChanged += WorkerProgressUpdate;
            worker.RunWorkerCompleted += WorkerFinished;

            renderSettingsWindow = new RenderSettingsWindow(this);
            renderSettingsWindow.Show();
        }

        public Renderer Renderer { get { return _renderer; } }

        public void CancelRender()
        {
            if (_renderer == null)
                return;

            worker.CancelAsync();
            _renderer.CancelRender();
        }

        public void DoRender()
        {
            if (_renderer != null)
            {
                return;
            }

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

            _xres = renderSettingsWindow.XRes;
            _yres = renderSettingsWindow.YRes;

            _bitmap = new Bitmap(_xres, _yres);

            Context context = new Context
            {
                Width = _xres,
                Height = _yres,
                MaxRecursion = renderSettingsWindow.MaxRecursionDepth,
                SampleCount = renderSettingsWindow.SampleCount,
                RenderCamera = flow.BuildCamera(),
                Graph = flow.BuildGraph()
            };

            context.RenderCamera.FieldOfView = renderSettingsWindow.FieldOfView;
            context.RenderCamera.NearClipPlane = renderSettingsWindow.NearClippingPlane;
            context.RenderCamera.FarClipPlane = renderSettingsWindow.FarClippingPlane;

            _renderer = new Renderer(context);

            this.Width = _xres;
            this.Height = _yres;

            this.Refresh();

            worker.RunWorkerAsync();
        }

        private Bitmap _bitmap = null;
        private int _xres = 0;
        private int _yres = 0;

        private void WorkerProc(object sender, EventArgs e)
        {
            int pixelsLeft = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            _renderer.StartRender(renderSettingsWindow.ThreadCount, renderSettingsWindow.HaltOnException, renderSettingsWindow.RandomJobOrder);
            pixelsLeft = _renderer.State.JobsLeft;

            double lastStatusUpdate = 0;

            while (!_renderer.IsFinished)
            {
                if (lastStatusUpdate + 1d < stopwatch.Elapsed.TotalSeconds)
                {
                    lastStatusUpdate = stopwatch.Elapsed.TotalSeconds;

                    int newPixelsLeft = _renderer.State.JobsLeft;
                    int pixelsFinished = pixelsLeft - newPixelsLeft;
                    pixelsLeft = newPixelsLeft;
                    double timeLeft = pixelsLeft / (double)pixelsFinished;
                    Console.WriteLine(string.Format(
                        "Status: {0} pixels/second, {1} left to render, ~{2:0.00} seconds left", pixelsFinished, pixelsLeft,
                        timeLeft));
                }

                if (renderSettingsWindow.LiveRendering)
                {
                    int left = int.MaxValue;
                    int right = 0;
                    int top = int.MaxValue;
                    int bottom = 0;
                    List<RenderJob> jobs = new List<RenderJob>();
                    while (_renderer.State.HasFinishedJobs())
                    {
                        if (_renderer.IsFinished)
                        {
                            jobs.Clear();
                            break;
                        }

                        RenderJob? job = _renderer.State.DequeueFinishedJob();
                        if (!job.HasValue)
                            continue;

                        if (job.Value.I < left)
                            left = job.Value.I;
                        if (job.Value.I > right)
                            right = job.Value.I;
                        if (job.Value.J < top)
                            top = job.Value.J;
                        if (job.Value.J > bottom)
                            bottom = job.Value.J;

                        jobs.Add(job.Value);
                    }

                    if (jobs.Count > 0)
                        worker.ReportProgress(0,
                            Tuple.Create(jobs.ToArray(), new Rectangle(left, top, right - left, bottom - top)));
                }

                Thread.Sleep(10);
            }

            stopwatch.Stop();
            Console.WriteLine(string.Format("Rendering took {0} seconds", stopwatch.Elapsed.TotalSeconds));
        }

        private void WorkerProgressUpdate(object sender, ProgressChangedEventArgs e)
        {
            Tuple<RenderJob[], Rectangle> args = (Tuple<RenderJob[], Rectangle>) e.UserState;
            RenderJob[] jobs = args.Item1;
            foreach (RenderJob job in jobs)
            {
                RenderColor color = _renderer.State.Pixels[job.I, job.J];
                System.Drawing.Color bmpColor = System.Drawing.Color.FromArgb(255, color.RByte, color.GByte,
                    color.BByte);

                _bitmap.SetPixel(job.I, job.J, bmpColor);
            }

            this.Invalidate(args.Item2);
            this.Update();
        }

        private void WorkerFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Generating final image...");

            for (int x = 0; x < _xres; ++x)
            {
                for (int y = 0; y < _yres; ++y)
                {
                    RenderColor color = _renderer.State.Pixels[x, y];
                    System.Drawing.Color bmpColor = System.Drawing.Color.FromArgb(255, color.RByte, color.GByte,
                        color.BByte);
                    _bitmap.SetPixel(x, y, bmpColor);
                }
            }

            _renderer = null;

            this.Width = _xres;
            this.Height = _yres;

            this.Refresh();

            Console.WriteLine("Image generation complete.");
        }

        public void SaveBitmap()
        {
            if (_bitmap == null)
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
                    _bitmap.Save(dialog.FileName);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Unable to save bitmap", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Saved bitmap", "Save complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_bitmap != null)
            {
                e.Graphics.DrawImage(_bitmap, 0, 0, Width, Height);
            }
        }
    }
}
