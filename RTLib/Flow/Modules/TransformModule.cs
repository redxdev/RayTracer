using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using RTLib.Util;

namespace RTLib.Flow.Modules
{
    public class TransformHelper
    {
        public Matrix<double> Transform { get; set; }
        public Matrix<double> ManualInverseTransform { get; set; } 
    }

    [Module]
    public class TransformModule : IModuleBuilder
    {
        public string GetModuleName()
        {
            return "Util.Transform";
        }

        public IFlowValue CreateModule(FlowScene scene, IDictionary<string, IFlowValue> parameters)
        {
            TransformHelper helper = new TransformHelper() {Transform = null, ManualInverseTransform = null};

            Vector<double> rotation = FlowUtilities.BuildParameter<Vector<double>>(scene, parameters, "Rotation", false,
                null);

            Vector<double> scale = FlowUtilities.BuildParameter<Vector<double>>(scene, parameters, "Scale", false, Vector<double>.Build.Sparse(3));

            Vector<double> position = FlowUtilities.BuildParameter<Vector<double>>(scene, parameters, "Position", false,
                Vector<double>.Build.Sparse(3));

            if (rotation != null)
            {
                helper.Transform= Transformation.RotateX(rotation[0])*Transformation.RotateY(rotation[1])*
                            Transformation.RotateZ(rotation[2]);
                helper.ManualInverseTransform = Transformation.RotateX(-rotation[0])*Transformation.RotateY(-rotation[1])*
                                         Transformation.RotateZ(-rotation[2]);

                helper.Transform *= Transformation.Scale(scale[0], scale[1], scale[2]);
                helper.ManualInverseTransform *= Transformation.Scale(scale[0], scale[1], scale[2]).Inverse();

                helper.Transform *= Transformation.Translate(position[0], position[1], position[2]);
                helper.Transform *= Transformation.Translate(position[0], position[1], position[2]).Inverse();
            }
            else
            {
                helper.Transform = Transformation.Scale(scale[0], scale[1], scale[2]);
                helper.Transform *= Transformation.Translate(position[0], position[1], position[2]);
            }

            return new GenericValue<TransformHelper>() {Value = helper};
        }
    }
}
