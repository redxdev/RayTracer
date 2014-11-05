using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTLib.Util
{
    public struct RenderColor
    {
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

        public byte RByte { get { return (byte) (_r*255); } }

        public byte GByte { get { return (byte) (_g*255); } }

        public byte BByte { get { return (byte) (_b*255); } }
    }
}
