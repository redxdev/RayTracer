using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using RTLib.Scene;
using RTLib.Util;

namespace RTLib.Render
{
    public class Renderer
    {
        private Thread[] _threads = null;

        public Renderer(Context context)
        {
            Context = context;
        }

        public Context Context { get; set; }

        public RenderState State { get; set; }

        public bool IsFinished
        {
            get
            {
                foreach (Thread thread in _threads)
                {
                    if (thread.IsAlive)
                        return false;
                }

                return true;
            }
        }

        public void StartRender(int threadCount, bool stopWorkerOnException = true, bool randomJobOrder = false)
        {
            Console.WriteLine("Rendering scene: " + Context);

            Console.Write("Building render state... ");
            State = new RenderState(Context.Width, Context.Height, randomJobOrder);
            Console.WriteLine("done");

            Console.WriteLine(string.Format("Starting {0} worker threads", threadCount));
            _threads = new Thread[threadCount];
            for (int i = 0; i < threadCount; ++i)
            {
                WorkerThread worker = new WorkerThread(this, i + 1, stopWorkerOnException);
                _threads[i] = new Thread(new ThreadStart(worker.WorkerProc));
                _threads[i].IsBackground = true;
                _threads[i].Start();
            }
        }

        public void CancelRender()
        {
            foreach (Thread thread in _threads)
            {
                if(thread.IsAlive)
                    thread.Abort();
            }

            Console.WriteLine("Aborted rendering threads");
            Console.WriteLine("Rendering cancelled.");
        }
    }
}