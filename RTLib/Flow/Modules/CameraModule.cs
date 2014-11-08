using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTLib.Render;

namespace RTLib.Flow.Modules
{
    public class CameraModule : IFlowModule
    {
        public Camera RenderCamera { get; set; }

        public void BuildModule(FlowScene scene, IDictionary<string, IFlowValue> parameters)
        {
            RenderCamera = new Camera();

            TransformModule transformModule = FlowUtilities.BuildParameter<TransformModule>(scene, parameters,
                "Transform");

            RenderCamera.Transform = transformModule.Transform;
            if (transformModule.ManualInverseTransform != null)
            {
                RenderCamera.ManualInverse = true;
                RenderCamera.InverseTransform = transformModule.ManualInverseTransform;
            }

            RenderCamera.FieldOfView = FlowUtilities.BuildParameter<double>(scene, parameters, "FieldOfView");
            RenderCamera.NearClipPlane = FlowUtilities.BuildParameter<double>(scene, parameters, "NearClipPlane");
            RenderCamera.FarClipPlane = FlowUtilities.BuildParameter<double>(scene, parameters, "FarClipPlane");
        }
    }
}
