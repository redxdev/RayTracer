using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTLib.Material;
using RTLib.Material.Texture;

namespace RTLib.Flow.Modules
{
    [Module]
    public class TextureShaderModule : IModuleBuilder
    {
        public string GetModuleName()
        {
            return "Shader.Texture";
        }

        public IFlowValue CreateModule(FlowScene scene, IDictionary<string, IFlowValue> parameters)
        {
            ITexture texture = FlowUtilities.BuildParameter<ITexture>(scene, parameters, "Texture");

            TextureShader shader = new TextureShader(texture);
            return new GenericValue<TextureShader>() {Value = shader};
        }
    }
}
