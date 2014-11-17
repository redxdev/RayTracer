using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using RTLib.Material;
using RTLib.Render;
using RTLib.Util;

namespace RTLib.Scene
{
    public class Sphere : SceneObject
    {
        private double _radius;
        private double _radiusSquared;

        public Sphere(Matrix<double> transform, double radius, IShader shader)
            : base(transform)
        {
            Shader = shader;
            Radius = radius;
        }

        public IShader Shader { get; set; }

        public double Radius
        {
            get { return _radius; }
            set
            {
                if(value < 0)
                    throw new ArgumentOutOfRangeException("value");

                _radius = value;
                _radiusSquared = value*value;
            }
        }

        public double RadiusSquared { get { return _radiusSquared; } }

        public override TraceHit Intersects(Ray ray, out double t)
        {
            Vector<double> rorig = ray.Origin*InverseTransform;
            Vector<double> rdir = ray.Direction*InverseTransform;

            double a = rdir.DotProduct(rdir);
            double b = 2d*rdir.DotProduct(rorig);
            double c = rorig.DotProduct(rorig) - 1d - RadiusSquared;

            double discr = b*b - 4d*a*c;
            if (discr <= 0)
            {
                t = 0;
                return TraceHit.Miss;
            }

            double discrSqrt = Math.Sqrt(discr);
            double t0 = (-b + discrSqrt)/(2d*a);
            double t1 = (-b - discrSqrt)/(2d*a);

            t = t0 < t1 ? t0 : t1;

            return TraceHit.Hit;
        }

        public override RenderColor Shade(Context context, TraceResult trace)
        {
            return Shader.RunShader(this, context, trace);
        }

        public override Vector<double> GetNormal(Vector<double> point)
        {
            Vector<double> origin = Vector<double>.Build.DenseOfArray(new double[] {0, 0, 0, 1});
            origin *= Transform;
            Vector<double> normal = point - origin;
            normal /= normal.Norm(2d);
            return normal;
        }

        public override Vector<double> GetUV(Vector<double> point)
        {
            Vector<double> d = GetNormal(point);

            double u = 0.5d + Math.Atan2(d[2], -d[0])/(2d*Math.PI);
            double v = 0.5d - Math.Asin(d[1])/Math.PI;

            return Vector<double>.Build.DenseOfArray(new double[] {u, v});
        }
    }
}
