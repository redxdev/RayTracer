using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using RTLib.Scene;

namespace RTLib.Render
{
    public enum TraceHit
    {
        Miss,
        Hit,
        HitInternal
    }

    public struct TraceResult
    {
        public double T { get; set; }

        public Ray Raycast { get; set; }

        public Vector<double> Intersection { get; set; }

        public SceneObject HitObject { get; set; }

        public IRayTracer Raytracer { get; set; }

        public TraceHit HitType { get; set; }
    }
}
