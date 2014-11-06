using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using RTLib.Material;
using RTLib.Render;
using RTLib.Util;

namespace RTLib.Scene
{
    public class Plane : SceneObject
    {
        public Plane(Matrix<double> transform, IShader shader) : base(transform)
        {
            Shader = shader;
        }

        public IShader Shader { get; set; }

        public override bool Intersects(Ray ray, out double t)
        {
            Vector<double> rorig = ray.Origin * InverseTransform;
            Vector<double> rdir = ray.Direction * InverseTransform;

            Vector<double> normal = GetNormal(null);
            
            t = -(rorig.DotProduct(normal))/(rdir.DotProduct(normal));
            if (t <= 0)
                return false;

            return true;
        }

        public override RenderColor Shade(Context context, TraceResult trace)
        {
            return Shader.RunShader(this, context, trace);
        }

        public override Vector<double> GetNormal(Vector<double> point)
        {
            Vector<double> normal = Vector<double>.Build.DenseOfArray(new double[] {0, 1, 0, 0});
            normal *= Transform;
            normal /= normal.Norm(2d);
            return normal;
        }
    }
}
