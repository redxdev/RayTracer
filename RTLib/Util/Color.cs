using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace RTLib.Util
{
    public struct RenderColor
    {
        public static RenderColor FromVector(Vector<double> vector)
        {
            if(vector.Count < 3)
                throw new InvalidCastException("Cannot convert a vector of < 3 size to a RenderColor");

            return new RenderColor(vector[0], vector[1], vector[2]);
        }

        public static RenderColor FromColor(Color color)
        {
            return new RenderColor(color.R / 255d, color.G / 255d, color.B / 255d);
        }

        public static RenderColor operator *(RenderColor color, double val)
        {
            return new RenderColor(color._r*val, color._g*val, color._b*val);
        }

        public static RenderColor operator *(double val, RenderColor color)
        {
            return color*val;
        }

        public static RenderColor operator *(RenderColor a, RenderColor b)
        {
            return new RenderColor(a.R * b.R, a.G * b.G, a.B * b.B);
        }

        public static RenderColor operator /(RenderColor color, double val)
        {
            return color*(1/val);
        }

        public static RenderColor operator +(RenderColor a, RenderColor b)
        {
            return new RenderColor(a._r + b._r, a._g + b._g, a._b + b._b);
        }

        public static RenderColor operator -(RenderColor a, RenderColor b)
        {
            return new RenderColor(a._r - b._r, a._g - b._g, a._b - b._b);
        }

        private double _r;
        private double _g;
        private double _b;

        public RenderColor(double r, double g, double b)
        {
            _r = r;
            _g = g;
            _b = b;
        }

        public double R { get { return _r; } set { _r = value; } }

        public double G { get { return _g; } set { _g = value; } }

        public double B { get { return _b; } set { _b = value; } }

        public byte RByte
        {
            get
            {
                return (byte) (MathHelper.Clamp(_r, 0, 1)*255);
            }
        }

        public byte GByte { get { return (byte) (MathHelper.Clamp(_g, 0, 1)*255); } }

        public byte BByte { get { return (byte) (MathHelper.Clamp(_b, 0, 1)*255); } }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", R, G, B);
        }
    }
}
