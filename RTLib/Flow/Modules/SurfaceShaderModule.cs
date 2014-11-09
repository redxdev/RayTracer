using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTLib.Material;

namespace RTLib.Flow.Modules
{
    [Module]
    public class SurfaceShaderModule : IModuleBuilder
    {
        public string GetModuleName()
        {
            return "Shader.Surface";
        }

        public IFlowValue CreateModule(FlowScene scene, IDictionary<string, IFlowValue> parameters)
        {
            double diffuse = FlowUtilities.BuildParameter<double>(scene, parameters, "Diffuse", false, 0);
            double specular = FlowUtilities.BuildParameter<double>(scene, parameters, "Specular", false, 0);
            IShader subshader = FlowUtilities.BuildParameter<IShader>(scene, parameters, "Subshader");

            SurfaceShader shader = new SurfaceShader(diffuse, specular, subshader);

            return new GenericValue<SurfaceShader>() {Value = shader};
        }
    }
}
