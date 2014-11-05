using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = RTLib.Util.Color;

namespace RTLib.Render.Targets
{
    public class LiveBitmapTarget : IRenderTarget
    {
        private Bitmap bitmap = null;

        public LiveBitmapTarget(Bitmap output)
        {
            bitmap = output;
        }

        public void StartRender(Renderer renderer)
        {
            bitmap.SetResolution(renderer.Context.Width, renderer.Context.Height);
        }

        public void FinishRender(Renderer renderer)
        {
        }

        public void PixelRendered(Renderer renderer, int x, int y, Color color)
        {
            System.Drawing.Color dc = System.Drawing.Color.FromArgb(255, color.RByte, color.GByte, color.BByte);
            bitmap.SetPixel(x, y, dc);
        }
    }
}
