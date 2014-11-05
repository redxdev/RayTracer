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
            Vector<double> normal = GetNormal(null);

            double denom = normal.DotProduct(ray.Direction);
            if (denom < 1e-6)
            {
                t = 0;
                return false;
            }

            Vector<double> p = Vector<double>.Build.DenseOfArray(new double[] {0, 1, 0, 1});
            p *= Transform;
            p -= ray.Origin;
            t = p.DotProduct(normal);

            return t >= 0;

            /*
            t = -(ray.Origin.DotProduct(normal))/(ray.Direction.DotProduct(normal));
            if (t <= 0)
                return false;

            return true;*/
        }

        public override RenderColor Shade(Context context, TraceInfo trace)
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
