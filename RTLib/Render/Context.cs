using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Complex;
using RTLib.Util;

namespace RTLib.Render
{
    public class Context
    {
        private int _width = 640;
        private int _height = 480;
        private double _aspectRatio;
        private RenderColor _backgroundColor;

        public Context()
        {
            _aspectRatio = Width/(double) Height;
            _backgroundColor = new RenderColor(0.392d, 0.584d, 0.929d);

            MaxRecursion = 16;
        }

        public int Width
        {
            get { return _width; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");

                _width = value;
                _aspectRatio = Width/(double) Height;
            }
        }

        public int Height
        {
            get { return _height; }
            set
            {
                if(value < 0)
                    throw new ArgumentOutOfRangeException("value");

                _height = value;
                _aspectRatio = Width / (double)Height;
            }
        }

        public double AspectRatio
        {
            get { return _aspectRatio; }
        }

        public RenderColor BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        public int MaxRecursion { get; set; }

        public Camera RenderCamera { get; set; }

        public override string ToString()
        {
            return string.Format(
                "{0}x{1} (r. {2}), mrd {3}",
                Width,
                Height,
                AspectRatio,
                MaxRecursion
                );
        }
    }
}
