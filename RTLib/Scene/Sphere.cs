﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using RTLib.Render;
using RTLib.Util;

namespace RTLib.Scene
{
    public class Sphere : SceneObject
    {
        private double _radius;
        private double _radiusSquared;

        public Sphere(Matrix<double> transform, double radius, Color? color = null)
            : base(transform)
        {
            OutputColor = color ?? new Color(0d, 1d, 0d);
            Radius = radius;
        }

        public Color OutputColor { get; set; }

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

        public override bool Intersects(Ray ray, out double t)
        {
            Vector<double> rorig = ray.Origin*InverseTransform;
            Vector<double> rdir = ray.Direction*InverseTransform;

            double a = rdir.DotProduct(rdir);
            double b = 2*rdir.DotProduct(rorig);
            double c = rorig[0]*rorig[0] + rorig[1]*rorig[1] + rorig[2]*rorig[2] - RadiusSquared; //rorig.DotProduct(rorig) - RadiusSquared;

            double discr = b*b - 4*a*c;
            if (discr <= 0)
            {
                t = 0;
                return false;
            }

            double q = (b < 0)
                ? -0.5d*(b - (double)Math.Sqrt(discr))
                : -0.5d*(b + (double)Math.Sqrt(discr));

            double t0 = q/a;
            double t1 = c/q;

            if (t1 < 0)
            {
                t = 0;
                return false;
            }

            if (t0 > t1)
            {
                double temp = t0;
                t0 = t1;
                t1 = temp;
            }

            t = (t0 < 0) ? t1 : t0;
            return true;
        }

        public override Color Shade(Context context, Ray ray)
        {
            return OutputColor;
        }
    }
}