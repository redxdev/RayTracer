using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTLib.Material;
using RTLib.Scene;
using RTLib.Util;

namespace RTLib.Flow.Modules
{
    [Module]
    public class PointLightModule : IModuleBuilder
    {
        public string GetModuleName()
        {
            return "Light.Point";
        }

        public IFlowValue CreateModule(FlowScene scene, IDictionary<string, IFlowValue> parameters)
        {
            TransformHelper transform = FlowUtilities.BuildParameter<TransformHelper>(scene, parameters, "Transform",
                false, new TransformHelper() { Transform = Transformation.Translate(0, 0, 0) });
            IShader shader = FlowUtilities.BuildParameter<IShader>(scene, parameters, "Shader");
            double intensity = FlowUtilities.BuildParameter<double>(scene, parameters, "Intensity", false, 1d);

            PointLight light = new PointLight(transform.Transform, shader, intensity);
            if (transform.ManualInverseTransform != null)
            {
                light.ManualInverse = true;
                light.InverseTransform = transform.ManualInverseTransform;
            }

            return new GenericValue<PointLight> {Value = light};
        }
    }
}
