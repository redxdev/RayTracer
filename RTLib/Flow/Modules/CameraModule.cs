using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTLib.Render;
using RTLib.Util;

namespace RTLib.Flow.Modules
{
    [Module]
    public class CameraModule : IModuleBuilder
    {
        public string GetModuleName()
        {
            return "Camera";
        }

        public IFlowValue CreateModule(FlowScene scene, IDictionary<string, IFlowValue> parameters)
        {
            Camera renderCamera = new Camera();

            TransformHelper transform = FlowUtilities.BuildParameter<TransformHelper>(scene, parameters,
                "Transform", false, new TransformHelper() {Transform = Transformation.Translate(0, 0, 0)});

            renderCamera.Transform = transform.Transform;
            if (transform.ManualInverseTransform != null)
            {
                renderCamera.ManualInverse = true;
                renderCamera.InverseTransform = transform.ManualInverseTransform;
            }

            return new GenericValue<Camera>() {Value = renderCamera};
        }
    }
}
