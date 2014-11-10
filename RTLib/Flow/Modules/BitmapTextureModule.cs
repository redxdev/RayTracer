using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTLib.Material.Texture;
using RTLib.Util;

namespace RTLib.Flow.Modules
{
    [Module]
    public class BitmapTextureModule : IModuleBuilder
    {
        public string GetModuleName()
        {
            return "Texture.Bitmap";
        }

        public IFlowValue CreateModule(FlowScene scene, IDictionary<string, IFlowValue> parameters)
        {
            string filepath = FlowUtilities.BuildParameter<string>(scene, parameters, "Path");
            bool bilinear = FlowUtilities.BuildParameter<bool>(scene, parameters, "BilinearFilter", false, false);

            Console.WriteLine(string.Format("Loading bitmap \"{0}\"", filepath));
            Bitmap bitmap = new Bitmap(Path.Combine(scene.RelativePath, filepath));
            Console.WriteLine(string.Format("Loaded bitmap. Binding to {0}x{1} texture...", bitmap.Width, bitmap.Height));

            RawTexture texture = new RawTexture();
            texture.Texture = new RenderColor[bitmap.Width, bitmap.Height];
            texture.Width = bitmap.Width;
            texture.Height = bitmap.Height;
            texture.BilinearFilter = bilinear;
            for (int y = 0; y < bitmap.Height; ++y)
            {
                for (int x = 0; x < bitmap.Width; ++x)
                {
                    texture.Texture[x, y] = RenderColor.FromColor(bitmap.GetPixel(x, y));
                }
            }

            Console.WriteLine(string.Format("Texture \"{0}\" bound.", filepath));

            return new GenericValue<RawTexture>() {Value = texture};
        }
    }
}
