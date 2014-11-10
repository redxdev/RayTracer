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

        private RenderColor[,] _pixels = null;

        private ConcurrentQueue<RenderJob> _finishedJobQueue = new ConcurrentQueue<RenderJob>(); 

        public RenderState(int width, int height, bool randomJobOrder)
        {
            _pixels = new RenderColor[width,height];

            List<RenderJob> jobs = new List<RenderJob>();
            for (int j = 0; j < height; ++j)
            {
                for (int i = 0; i < width; ++i)
                {
                    jobs.Add(new RenderJob() { I = i, J = j });
                }
            }

            if (randomJobOrder)
            {
                Random random = new Random();
                int n = jobs.Count;
                while (n > 1)
                {
                    n--;
                    int k = random.Next(n + 1);
                    RenderJob value = jobs[k];
                    jobs[k] = jobs[n];
                    jobs[n] = value;
                }
            }

            foreach (RenderJob job in jobs)
            {
                _jobs.Enqueue(job);
            }
        }

        public RenderColor[,] Pixels { get { return _pixels; } }

        public int JobsLeft
        {
            get
            {
                return _jobs.Count;
            }
        }

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

        public void FinishJob(int i, int j, RenderColor color)
        {
            _pixels[i, j] = color;
            _finishedJobQueue.Enqueue(new RenderJob() {I = i, J = j});
        }

        public bool HasFinishedJobs()
        {
            return !_finishedJobQueue.IsEmpty;
        }

        public RenderJob? DequeueFinishedJob()
        {
            RenderJob job;
            if (_finishedJobQueue.TryDequeue(out job))
                return job;

            return null;
        }
    }
}
