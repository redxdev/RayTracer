﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace RTLib.Render
{
    public struct TraceInfo
    {
        public double VX { get; set; }
        public double VY { get; set; }

        public double T { get; set; }

        public Ray Raycast { get; set; }

        public Vector<double> Intersection { get; set; }
    }
}