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

        private RenderWindow _renderWindow = null;

        public RenderSettingsWindow(RenderWindow rw)
        {
            _renderWindow = rw;

            InitializeComponent();

            this.ControlBox = false;

            this.threadCount.Value = Environment.ProcessorCount;
        }

        private void renderButton_Click(object sender, EventArgs e)
        {
            _renderWindow.DoRender();
        }
    }
}
