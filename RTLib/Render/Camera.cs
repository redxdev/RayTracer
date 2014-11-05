using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using RTLib.Scene;
using RTLib.Util;

namespace RTLib.Render
{
    public class Camera : SceneObject // extends sceneobject purely for convenience, don't actually use Camera in a scene
    {
        private double _fieldOfView;
        private double _angle;

        public Camera(Matrix<double> transform = null, double fov = 90d, double ncp = 0.1d, double fcp = 1000d)
            : base(transform ?? Matrix<double>.Build.SparseIdentity(4, 4))
        {
            FieldOfView = fov;
            NearClippingPlane = ncp;
            FarClippingPlane = fcp;
        }

        public double FieldOfView
        {
            get { return _fieldOfView; }
            set
            {
                if(value < 0)
                    throw new ArgumentOutOfRangeException("value");

                _fieldOfView = value;
                _angle = (double)Math.Atan(Math.PI*(value * 0.5d)/180d);
            }
        }

        public double Angle { get { return _angle; } }

        public double NearClippingPlane { get; set; }

        public double FarClippingPlane { get; set; }

        public override bool Intersects(Ray ray, out double t)
        {
            throw new NotImplementedException();
        }

        public override RenderColor Shade(Context context, TraceInfo trace)
        {
            throw new NotImplementedException();
        }

        public override Vector<double> GetNormal(Vector<double> point)
        {
            throw new NotImplementedException();
        }
    }
}
