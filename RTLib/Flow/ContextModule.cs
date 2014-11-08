﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using RTLib.Render;
using RTLib.Util;

namespace RTLib.Flow
{
    public class ContextModule : IFlowModule
    {
        public Context SceneContext { get; set; }

        public void BuildModule(FlowScene scene, IDictionary<string, IFlowValue> parameters)
        {
            SceneContext = new Context();

            SceneContext.Width = (int) FlowUtilities.BuildParameter<double>(scene, parameters, "Width");
            SceneContext.Height = (int) FlowUtilities.BuildParameter<double>(scene, parameters, "Height");

            SceneContext.BackgroundColor =
                RenderColor.FromVector(FlowUtilities.BuildParameter<Vector<double>>(scene, parameters, "BackgroundColor",
                    false, Vector<double>.Build.DenseOfArray(new double[] {0.392d, 0.584d, 0.929d})));

            SceneContext.SampleCount =
                (int) FlowUtilities.BuildParameter<double>(scene, parameters, "SampleCount", false, 1);

            Matrix<double> cameraTransform = 
            SceneContext.RenderCamera = new Camera();
        }
    }
}
