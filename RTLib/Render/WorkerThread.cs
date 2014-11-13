using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using RTLib.Material;
using RTLib.Scene;
using RTLib.Util;

namespace RTLib.Render
{
    public class WorkerThread : IRayTracer
    {
        private Renderer _renderer;
        private int _threadId = 0;
        private bool _stopOnException = true;
        private Exception lastException = null;

        public WorkerThread(Renderer renderer, int threadId, bool stopOnException = true)
        {

            _renderer = renderer;
            _threadId = threadId;
            _stopOnException = stopOnException;
        }

        private Vector<double> BuildCameraPosition(double i, double j)
        {
            double x = (2d * ((i + 0.5d) / _renderer.Context.Width) - 1d) * _renderer.Context.AspectRatio * _renderer.Context.RenderCamera.Angle;
            double y = (1d - 2d * ((j + 0.5d) / _renderer.Context.Height)) * _renderer.Context.RenderCamera.Angle;

            Vector<double> cameraPos = Vector<double>.Build.DenseOfArray(new double[] { x, y, -1, 1 });
            cameraPos *= _renderer.Context.RenderCamera.Transform;

            return cameraPos;
        }

        public void WorkerProc()
        {
            Console.WriteLine(string.Format("Worker thread #{0} started", _threadId));

            int sampleSide = (int) Math.Sqrt(_renderer.Context.SampleCount);
            double sampleOffset = sampleSide/2d;

            Vector<double> rayOrigin = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 0, 1 });
            rayOrigin *= _renderer.Context.RenderCamera.Transform;

            int i = 0;
            int j = 0;
            while (_renderer.State.StartJob(_threadId, out i, out j))
            {
                try
                {
                    RenderColor finalColor = new RenderColor(0, 0, 0);
                    for (int sj = 0; sj < sampleSide; ++sj)
                    {
                        for (int si = 0; si < sampleSide; ++si)
                        {
                            Vector<double> cameraPos = BuildCameraPosition(i + (si - sampleOffset)/sampleSide, j + (sj - sampleOffset)/sampleSide);

                            Vector<double> rayDirection = cameraPos - rayOrigin;
                            rayDirection /= rayDirection.Norm(2d);

                            Ray ray = CreateRay(rayOrigin, rayDirection);
                            TraceResult? result = Trace(ray);
                            RenderColor color = result == null
                                ? _renderer.Context.BackgroundColor
                                : result.Value.HitObject.Shade(_renderer.Context, result.Value);

                            finalColor += color;
                        }
                    }

                    finalColor /= _renderer.Context.SampleCount;

                    _renderer.State.FinishJob(i, j, finalColor);
                }
                catch(Exception e)
                {
                    if(lastException == null || lastException.GetType() != e.GetType() || lastException.Message != e.Message)
                        Console.WriteLine(string.Format("Worker thread #{0} encounterd an exception: {1}", _threadId, e));

                    lastException = e;

                    if (_stopOnException)
                    {
                        Console.WriteLine(string.Format("Worker thread #{0} halting due to exception", _threadId));
                        return;
                    }
                }
            }

            Console.WriteLine(string.Format("Worker thread #{0} finished", _threadId));
        }

        public Ray CreateRay(Vector<double> origin, Vector<double> direction, Ray previous = null)
        {
            return new Ray(origin, direction, previous == null ? 0 : (previous.Recursion + 1),
                _renderer.Context.RenderCamera.NearClipPlane, _renderer.Context.RenderCamera.FarClipPlane);
        }

        public TraceResult? Trace(Ray ray, bool allowInternalHit = true)
        {
            if (ray.Recursion > _renderer.Context.MaxRecursion)
            {
                return null;
            }

            double tClosest = ray.MaxDistance;
            SceneObject hitObject = null;
            TraceHit hitType = TraceHit.Miss;

            foreach (var obj in _renderer.Context.Graph.Objects)
            {
                double t = 0;
                TraceHit hit = obj.Intersects(ray, out t);
                if (hit == TraceHit.Hit || (allowInternalHit && hit == TraceHit.HitInternal))
                {
                    if (t < tClosest && t > ray.MinDistance)
                    {
                        tClosest = t;
                        hitObject = obj;
                        hitType = hit;
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
                Raytracer = this,
                HitType =  hitType
            };
        }
    }
}
