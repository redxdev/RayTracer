using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace RTLib.Render
{
    public class Ray
    {
        public Ray(Vector<double> origin, Vector<double> direction, int recursion = 0, double min = 0, double max = double.MaxValue, double refractionIndex = 1d)
        {
            Origin = origin;
            Direction = direction;
            MinDistance = min;
            MaxDistance = max;
            Recursion = recursion;
            RefractionIndex = refractionIndex;
        }

        public Vector<double> Origin { get; set; }

        public Vector<double> Direction { get; set; }

        public double MinDistance { get; set; }

        public double MaxDistance { get; set; }

        public int Recursion { get; set; }

        public double RefractionIndex { get; set; }
    }
}
