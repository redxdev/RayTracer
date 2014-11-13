using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTLib.Material;

namespace RTLib.Flow.Modules
{
    [Module]
    public class RefractionShaderModule : IModuleBuilder
    {
        public string GetModuleName()
        {
            return "Shader.Refraction";
        }

        public IFlowValue CreateModule(FlowScene scene, IDictionary<string, IFlowValue> parameters)
        {
            double refractionIndex = FlowUtilities.BuildParameter<double>(scene, parameters, "RefractionIndex");
            IShader subshader = FlowUtilities.BuildParameter<IShader>(scene, parameters, "Subshader");

            RefractionShader shader = new RefractionShader(refractionIndex, subshader);
            return new GenericValue<RefractionShader>() {Value = shader};
        }
    }
}
