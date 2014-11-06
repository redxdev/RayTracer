using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTFrontend
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.Title = "RTFrontend Console";

            Application.EnableVisualStyles();
            Application.Run(new RenderWindow());
        }
    }
}
