using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Complex;
using RTLib.Material;
using RTLib.Scene;
using RTLib.Util;

namespace RTLib.Flow.Modules
{
    [Module]
    public class PlanePrimitiveModule : IModuleBuilder
    {
        public string GetModuleName()
        {
            return "Primitive.Plane";
        }

        public IFlowValue CreateModule(FlowScene scene, IDictionary<string, IFlowValue> parameters)
        {
            TransformHelper transform = FlowUtilities.BuildParameter<TransformHelper>(scene, parameters, "Transform",
                false, new TransformHelper() {Transform = Transformation.Translate(0, 0, 0)});
            IShader shader = FlowUtilities.BuildParameter<IShader>(scene, parameters, "Shader");

            Plane primitive = new Plane(transform.Transform, shader);
            if (transform.ManualInverseTransform != null)
            {
                primitive.ManualInverse = true;
                primitive.InverseTransform = transform.ManualInverseTransform;
            }

            return new GenericValue<Plane>() {Value = primitive};
        }
    }
}
