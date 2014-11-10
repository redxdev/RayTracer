using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTLib.Material;

namespace RTLib.Flow.Modules
{
    [Module]
    public class SurfaceNormalShaderModule : IModuleBuilder
    {
        public string GetModuleName()
        {
            return "Shader.SurfaceNormal";
        }

        public IFlowValue CreateModule(FlowScene scene, IDictionary<string, IFlowValue> parameters)
        {
            return new GenericValue<SurfaceNormalShader>() {Value = new SurfaceNormalShader()};
        }
    }
}
