using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTLib.Material;

namespace RTLib.Flow.Modules
{
    [Module]
    public class ReflectionShaderModule : IModuleBuilder
    {
        public string GetModuleName()
        {
            return "Shader.Reflection";
        }

        public IFlowValue CreateModule(FlowScene scene, IDictionary<string, IFlowValue> parameters)
        {
            double reflectivity = FlowUtilities.BuildParameter<double>(scene, parameters, "Reflectivity");
            IShader subshader = FlowUtilities.BuildParameter<IShader>(scene, parameters, "Subshader");

            ReflectionShader shader = new ReflectionShader(reflectivity, subshader);

            return new GenericValue<ReflectionShader>() {Value = shader};
        }
    }
}
