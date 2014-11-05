using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = RTLib.Util.Color;

namespace RTLib.Render.Targets
{
    public class BitmapTarget : IRenderTarget
    {
        private string outputFile;

        private Bitmap bitmap = null;

        public BitmapTarget(string output)
        {
            outputFile = output;
        }

        public void StartRender(Renderer renderer)
        {
            bitmap = new Bitmap(renderer.Context.Width, renderer.Context.Width);
        }

        public void FinishRender(Renderer renderer)
        {
            bitmap.Save(outputFile);
        }

        public void PixelRendered(Renderer renderer, int x, int y, Color color)
        {
            System.Drawing.Color dc = System.Drawing.Color.FromArgb(255, color.RByte, color.GByte, color.BByte);
            bitmap.SetPixel(x, y, dc);
        }
    }
}
