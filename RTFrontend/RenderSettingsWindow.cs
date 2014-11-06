using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTFrontend
{
    public partial class RenderSettingsWindow : Form
    {
        public int XRes { get { return (int) resWidth.Value; } }

        public int YRes { get { return (int) resHeight.Value; } }

        public int ThreadCount { get { return (int) threadCount.Value; } }

        public double FieldOfView { get { return (double) fieldOfView.Value; } }

        public double NearClippingPlane { get { return (double) nearClipPlane.Value; } }

        public double FarClippingPlane { get { return (double) farClipPlane.Value; } }

        public int MaxRecursionDepth { get { return (int) maxRecursionDepth.Value; } }

        public bool HaltOnException { get { return haltOnException.Checked; } }

        private RenderWindow _renderWindow = null;

        public RenderSettingsWindow(RenderWindow rw)
        {
            _renderWindow = rw;

            InitializeComponent();

            this.ControlBox = false;

            this.threadCount.Value = Environment.ProcessorCount;

            this.nearClipPlane.Maximum = decimal.MaxValue;
            this.farClipPlane.Maximum = decimal.MaxValue;
            this.farClipPlane.Value = 1000;
        }

        private void renderButton_Click(object sender, EventArgs e)
        {
            _renderWindow.DoRender();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            _renderWindow.SaveBitmap();
        }
    }
}
