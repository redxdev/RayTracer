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

        public override ObjectType GetObjectType()
        {
            return ObjectType.Solid;
        }

        public override bool Intersects(Ray ray, out double t)
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
                return false;
            }

            double discrSqrt = Math.Sqrt(discr);
            double t0 = (-b + discrSqrt)/(2d*a);
            double t1 = (-b - discrSqrt)/(2d*a);

            t = t0 < t1 ? t0 : t1;
            return true;
        }

        public override RenderColor Shade(Context context, TraceResult trace)
        {
            return Shader.RunShader(this, context, trace);
        }

        public override Vector<double> GetNormal(Vector<double> point)
        {
            Vector<double> normal = point*InverseTransform;
            normal /= normal.Norm(2d);
            return normal;
        }
    }
}
