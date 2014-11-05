using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;
using RTLib.Render;
using RTLib.Render.Targets;
using RTLib.Scene;
using RTLib.Util;

namespace RTFrontend
{
    public partial class RenderWindow : Form
    {
        private Renderer renderer = null;

        public RenderWindow()
        {
            InitializeComponent();
        }

        private void renderButton_Click(object sender, EventArgs e)
        {
            if (renderer != null)
                return;

            Bitmap bitmap = new Bitmap(640, 480);
            pictureBox.Image = bitmap;

            LinkedList<IRenderTarget> targets = new LinkedList<IRenderTarget>();
            targets.AddLast(new LiveBitmapTarget(bitmap));

            SceneGraph graph = new SceneGraph();
            Matrix<double> om = Transformation.Translate(0, 0, -5);
            graph.Objects.AddLast(new Sphere(om, 1));

            Context context = new Context();
            context.Width = 640;
            context.Height = 480;

            Matrix<double> cm = Transformation.Translate(0, 0, 5);
            context.RenderCamera = new Camera(cm, 30);
            
            renderer = new Renderer(context, graph, targets);
            renderer.Render();

            renderer = null;
        }
    }
}
