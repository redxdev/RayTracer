using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using RTLib.Material;
using RTLib.Scene;
using RTLib.Util;

namespace RTLib.Flow.Modules
{
    [Module]
    public class SpherePrimitiveModule : IModuleBuilder
    {
        public string GetModuleName()
        {
            return "Primitive.Sphere";
        }

        public IFlowValue CreateModule(FlowScene scene, IDictionary<string, IFlowValue> parameters)
        {
            double radius = FlowUtilities.BuildParameter<double>(scene, parameters, "Radius", false, 1);
            TransformHelper transform = FlowUtilities.BuildParameter<TransformHelper>(scene, parameters, "Transform",
                false, new TransformHelper() {Transform = Transformation.Translate(0, 0, 0)});
            IShader shader = FlowUtilities.BuildParameter<IShader>(scene, parameters, "Shader");

            Sphere primitive = new Sphere(transform.Transform, radius, shader);
            if (transform.ManualInverseTransform != null)
            {
                primitive.ManualInverse = true;
                primitive.InverseTransform = transform.ManualInverseTransform;
            }

            return new GenericValue<Sphere>() {Value = primitive};
        }
    }
}
