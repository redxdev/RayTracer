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
    public class ColorShaderModule : IModuleBuilder
    {
        public string GetModuleName()
        {
            return "Shader.Color";
        }

        public IFlowValue CreateModule(FlowScene scene, IDictionary<string, IFlowValue> parameters)
        {
            Vector<double> colorVec = FlowUtilities.BuildParameter<Vector<double>>(scene, parameters, "Color");

            ColorShader shader = new ColorShader(new RenderColor(colorVec[0], colorVec[1], colorVec[2]));
            return new GenericValue<ColorShader>() {Value = shader};
        }
    }
}
