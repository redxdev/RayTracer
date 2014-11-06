using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RTLib.Util;

namespace RTLib.Render
{
    public interface IRayTracer
    {
        TraceResult? Trace(Ray ray);
    }
}
