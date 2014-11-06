using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using RTLib.Scene;
using RTLib.Util;

namespace RTLib.Render
{
    public class WorkerThread : IRayTracer
    {
        private Renderer _renderer;
        private int _threadId = 0;

        public WorkerThread(Renderer renderer, int threadId)
        {
            _renderer = renderer;
            _threadId = threadId;
        }

        public void WorkerProc()
        {
            Console.WriteLine(string.Format("Worker thread #{0} started", _threadId));

            Vector<double> rayOrigin = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 0, 1 });
            rayOrigin *= _renderer.Context.RenderCamera.Transform;

            int i = 0;
            int j = 0;
            while (_renderer.State.StartJob(_threadId, out i, out j))
            {
                double x = (2d * ((i + 0.5d) / _renderer.Context.Width) - 1d) * _renderer.Context.AspectRatio * _renderer.Context.RenderCamera.Angle;
                double y = (1d - 2d * ((j + 0.5d) / _renderer.Context.Height)) * _renderer.Context.RenderCamera.Angle;

                Vector<double> cameraPos = Vector<double>.Build.DenseOfArray(new double[] { x, y, -1, 1 });
                cameraPos *= _renderer.Context.RenderCamera.Transform;

                Vector<double> rayDirection = cameraPos - rayOrigin;
                rayDirection /= rayDirection.Norm(2d);

                Ray ray = new Ray(rayOrigin, rayDirection, 0, _renderer.Context.RenderCamera.NearClippingPlane, _renderer.Context.RenderCamera.FarClippingPlane);
                TraceResult? result = Trace(ray, ObjectType.Solid);
                RenderColor color = result == null
                    ? _renderer.Context.BackgroundColor
                    : result.Value.HitObject.Shade(_renderer.Context, result.Value);
                _renderer.State.FinishJob(i, j, color);
            }

            Console.WriteLine(string.Format("Worker thread #{0} finished", _threadId));
        }

        public TraceResult? Trace(Ray ray, ObjectType traceType)
        {
            double tClosest = ray.MaxDistance;
            SceneObject hitObject = null;

            foreach (var obj in _renderer.Context.Graph.Objects)
            {
                if (obj.GetObjectType() != traceType)
                    continue;

                double t = 0;
                if (obj.Intersects(ray, out t))
                {
                    if (t < tClosest && t > ray.MinDistance)
                    {
                        tClosest = t;
                        hitObject = obj;
                    }
                }
            }

            if (hitObject == null)
                return null;

            return new TraceResult
            {
                T = tClosest,
                Raycast = ray,
                Intersection = ray.Origin + ray.Direction*tClosest,
                HitObject = hitObject,
                Raytracer = this
            };
        }
    }
}
