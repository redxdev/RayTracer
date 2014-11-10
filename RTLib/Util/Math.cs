using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace RTLib.Util
{
    public static class MathHelper
    {
        public static double Clamp(double value, double min, double max)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        public static Vector<double> CrossProduct(Vector<double> a, Vector<double> b, double fourth)
        {
            return Vector<double>.Build.DenseOfArray(new double[]
            {
                a[2]*b[3] - a[3]*b[2],
                a[3]*b[1] - a[1]*b[3],
                a[1]*b[2] - a[2]*b[1],
                fourth
            });
        }
    }
}
