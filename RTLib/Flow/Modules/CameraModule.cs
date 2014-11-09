using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTLib.Render;

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
                "Transform");

            renderCamera.Transform = transform.Transform;
            if (transform.ManualInverseTransform != null)
            {
                renderCamera.ManualInverse = true;
                renderCamera.InverseTransform = transform.ManualInverseTransform;
            }

            renderCamera.FieldOfView = FlowUtilities.BuildParameter<double>(scene, parameters, "FieldOfView");
            renderCamera.NearClipPlane = FlowUtilities.BuildParameter<double>(scene, parameters, "NearClipPlane");
            renderCamera.FarClipPlane = FlowUtilities.BuildParameter<double>(scene, parameters, "FarClipPlane");

            return new GenericValue<Camera>() {Value = renderCamera};
        }
    }
}
