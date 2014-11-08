using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using RTLib.Util;

namespace RTLib.Flow.Modules
{
    public class TransformModule : IFlowModule
    {
        public Matrix<double> Transform { get; set; }

        public Matrix<double> ManualInverseTransform { get; set; } 

        public TransformModule()
        {
            Transform = Transformation.Translate(0, 0, 0);
            ManualInverseTransform = null;
        }

        public void BuildModule(FlowScene scene, IDictionary<string, IFlowValue> parameters)
        {
            bool useManual = false;
            Vector<double> rotation = FlowUtilities.BuildParameter<Vector<double>>(scene, parameters, "Rotation", false,
                null);

            Vector<double> scale = FlowUtilities.BuildParameter<Vector<double>>(scene, parameters, "Scale", false, Vector<double>.Build.Sparse(3));

            Vector<double> position = FlowUtilities.BuildParameter<Vector<double>>(scene, parameters, "Position", false,
                Vector<double>.Build.Sparse(3));

            if (rotation != null)
            {
                Transform = Transformation.RotateX(rotation[0])*Transformation.RotateY(rotation[1])*
                            Transformation.RotateZ(rotation[2]);
                ManualInverseTransform = Transformation.RotateX(-rotation[0])*Transformation.RotateY(-rotation[1])*
                                         Transformation.RotateZ(-rotation[2]);

                Transform *= Transformation.Scale(scale[0], scale[1], scale[2]);
                ManualInverseTransform *= Transformation.Scale(scale[0], scale[1], scale[2]).Inverse();

                Transform *= Transformation.Translate(position[0], position[1], position[2]);
                Transform *= Transformation.Translate(position[0], position[1], position[2]).Inverse();
            }
            else
            {
                Transform = Transformation.Scale(scale[0], scale[1], scale[2]);
                Transform *= Transformation.Translate(position[0], position[1], position[2]);
            }
        }
    }
}
