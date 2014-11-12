using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using RTLib.Material;
using RTLib.Util;

namespace RTLib.Flow.Modules
{
    [Module]
    public class UVShaderModule : IModuleBuilder
    {
        public string GetModuleName()
        {
            return "Shader.UV";
        }

        public IFlowValue CreateModule(FlowScene scene, IDictionary<string, IFlowValue> parameters)
        {
            UVShader shader = new UVShader();
            return new GenericValue<UVShader>() { Value = shader };
        }
    }
}
