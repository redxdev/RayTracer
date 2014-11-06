using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using RTLib.Scene;
using RTLib.Util;

namespace RTLib.Render
{
    public interface IRayTracer
    {
        Ray CreateRay(Vector<double> origin, Vector<double> direction, Ray previous = null);

        TraceResult? Trace(Ray ray);
    }
}
