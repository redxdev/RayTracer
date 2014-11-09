using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RTLib.Flow;

namespace RTFrontend
{
    class Program
    {
        private static RenderWindow renderWindow = null;

        private static void ConsoleCancelHandler(object sender, ConsoleCancelEventArgs args)
        {
            if(renderWindow != null && renderWindow.Renderer != null)
            {
                renderWindow.Renderer.CancelRender();
                args.Cancel = true;
            }
        }

        [STAThread]
        public static void Main(string[] args)
        {
            Console.Title = "RTFrontend Console";
            Console.CancelKeyPress += ConsoleCancelHandler;

            Application.EnableVisualStyles();
            renderWindow = new RenderWindow();
            Application.Run(renderWindow);
        }
    }
}
