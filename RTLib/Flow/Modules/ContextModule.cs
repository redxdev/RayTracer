using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using RTLib.Render;
using RTLib.Util;

namespace RTLib.Flow.Modules
{
    public class ContextModule : IModuleBuilder
    {
        public string GetModuleName()
        {
            return "Context";
        }

        public IFlowValue CreateModule(FlowScene scene, IDictionary<string, IFlowValue> parameters)
        {
            Context sceneContext = new Context();

            sceneContext.Width = (int) FlowUtilities.BuildParameter<double>(scene, parameters, "Width");
            sceneContext.Height = (int) FlowUtilities.BuildParameter<double>(scene, parameters, "Height");

            sceneContext.BackgroundColor =
                RenderColor.FromVector(FlowUtilities.BuildParameter<Vector<double>>(scene, parameters, "BackgroundColor",
                    false, Vector<double>.Build.DenseOfArray(new double[] {0.392d, 0.584d, 0.929d})));

            sceneContext.SampleCount =
                (int) FlowUtilities.BuildParameter<double>(scene, parameters, "SampleCount", false, 1);

            sceneContext.RenderCamera =
                FlowUtilities.BuildParameter<Camera>(scene, parameters, "Camera");
        }
    }
}
