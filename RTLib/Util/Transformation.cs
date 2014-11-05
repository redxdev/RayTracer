using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace RTLib.Util
{
    public static class Transformation
    {
        public static Matrix<double> Translate(double x, double y, double z)
        {
            Matrix<double> matrix = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                {1, 0, 0, 0},
                {0, 1, 0, 0},
                {0, 0, 1, 0},
                {x, y, z, 1}
            });

            return matrix;
        }

        public static Matrix<double> Scale(double x, double y, double z)
        {
            Matrix<double> matrix = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                {x, 0, 0, 0},
                {0, y, 0, 0},
                {0, 0, z, 0},
                {0, 0, 0, 1}
            });

            return matrix;
        }
    }
}
