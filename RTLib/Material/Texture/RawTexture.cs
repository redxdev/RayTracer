using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTLib.Util;

namespace RTLib.Material.Texture
{
    public class RawTexture : ITexture
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public RenderColor[,] Texture { get; set; }

        public bool BilinearFilter { get; set; }

        public RenderColor GetTexel(double u, double v)
        {
            if (BilinearFilter)
            {
                throw new NotImplementedException("Bilinear filtering is not currently supported.");
            }
            else
            {
                double x = u * Width;
                double y = v * Height;

                int tx = (int)MathHelper.Clamp(x, 0, Width - 1);
                int ty = (int)MathHelper.Clamp(y, 0, Height - 1);

                return Texture[tx, ty];
            }
        }
    }
}
