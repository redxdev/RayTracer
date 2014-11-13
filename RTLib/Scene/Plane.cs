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

        public override TraceHit Intersects(Ray ray, out double t)
        {
            Vector<double> rorig = ray.Origin * InverseTransform;
            Vector<double> rdir = ray.Direction * InverseTransform;

            Vector<double> normal = GetNormal(null);
            
            t = -(rorig.DotProduct(normal))/(rdir.DotProduct(normal));
            if (t <= 0)
                return TraceHit.Miss;

            return TraceHit.Hit;
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

        public override Vector<double> GetUV(Vector<double> point)
        {
            Vector<double> normal = GetNormal(null);
            Vector<double> uAxis = Vector<double>.Build.DenseOfArray(new double[] {normal[1], normal[2], -normal[0], 0});
            Vector<double> vAxis = MathHelper.CrossProduct(uAxis, normal, 0);
            double u = point.DotProduct(uAxis);
            double v = point.DotProduct(vAxis);
            return Vector<double>.Build.DenseOfArray(new double[] {u, v});
        }
    }
}
