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

        public static Matrix<double> RotateX(double x)
        {
            Matrix<double> mx = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                {1, 0, 0, 0},
                {0, Math.Cos(x), -Math.Sin(x), 0},
                {0, Math.Sin(x), Math.Cos(x), 0},
                {0, 0, 0, 1}
            });

            return mx;
        }

        public static Matrix<double> RotateY(double y)
        {
            Matrix<double> my = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                {Math.Cos(y), 0, Math.Sin(y), 0},
                {0, 1, 0, 0},
                {-Math.Sin(y), 0, Math.Cos(y), 0},
                {0, 0, 0, 1}
            });

            return my;
        }

        public static Matrix<double> RotateZ(double z)
        {
            Matrix<double> mz = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                {Math.Cos(z), -Math.Sin(z), 0, 0},
                {Math.Sin(z), Math.Cos(z), 0, 0},
                {0, 0, 1, 0},
                {0, 0, 0, 1}
            });

            return mz;
        }
    }
}
