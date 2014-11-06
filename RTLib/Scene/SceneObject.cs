﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using RTLib.Render;
using RTLib.Util;

namespace RTLib.Scene
{
    public abstract class SceneObject
    {
        protected Matrix<double> _transform;

        protected Matrix<double> _inverseTransform;

        private bool _manualInverse = false;

        public Matrix<double> Transform
        {
            get { return _transform; }
            set
            {
                if(value.RowCount != 4 || value.ColumnCount != 4)
                    throw new Exception("Transform matrix must be 4x4");

                _transform = value;
                CalculateInverse();
            }
        }

        public Matrix<double> InverseTransform
        {
            get { return _inverseTransform; }
            set { _inverseTransform = value; }
        }

        public bool ManualInverse
        {
            get { return _manualInverse; }
            set { _manualInverse = value; }
        }

        protected SceneObject(Matrix<double> transform)
        {
            Transform = transform;
        }

        public void CalculateInverse()
        {
            if (_manualInverse)
                return;

            _inverseTransform = _transform.Inverse();
        }

        public abstract bool Intersects(Ray ray, out double t);

        public abstract RenderColor Shade(Context context, TraceInfo trace);

        public abstract Vector<double> GetNormal(Vector<double> point);
    }
}
