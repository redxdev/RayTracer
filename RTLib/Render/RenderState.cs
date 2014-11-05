using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RTLib.Util;

namespace RTLib.Render
{
    public struct RenderJob
    {
        public int I { get; set; }
        public int J { get; set; }
    }

    public class RenderState
    {
        private ConcurrentQueue<RenderJob> _jobs = new ConcurrentQueue<RenderJob>();

        private Color[,] _pixels = null;

        public RenderState(int width, int height)
        {
            _pixels = new Color[width,height];

            for (int j = 0; j < height; ++j)
            {
                for (int i = 0; i < width; ++i)
                {
                    _jobs.Enqueue(new RenderJob() {I = i, J = j});
                }
            }
        }

        public Color[,] Pixels { get { return _pixels; } }

        public bool StartJob(int threadId, out int i, out int j)
        {
            if (_jobs.IsEmpty)
            {
                i = 0;
                j = 0;
                return false;
            }

            RenderJob job;

            while (!_jobs.TryDequeue(out job))
            {
                if (_jobs.IsEmpty)
                {
                    i = 0;
                    j = 0;
                    return false;
                }

                Console.WriteLine(string.Format("TryDequeue failed on thread #{0}, retrying...", threadId));
            }

            i = job.I;
            j = job.J;
            return true;
        }

        public void FinishJob(int i, int j, Color color)
        {
            _pixels[i, j] = color;
        }
    }
}
