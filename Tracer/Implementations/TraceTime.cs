using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Tracer.Interfaces;
using Tracer.TraceResults;

namespace Tracer.Implementations
{
    public class TraceTime : ITracer
    {
        private static readonly object ThreadLocker = new object();

        private readonly Dictionary<int, TraceThread> _threads;

        public TraceTime()
        {
            _threads = new Dictionary<int, TraceThread>();
        }

        public TraceResult GetTraceResult()
        {
            return new TraceResult(_threads);
        }

        public void StartTrace()
        {
            var methodBase = new StackTrace().GetFrame(1).GetMethod();
            var methodTracer = new TraceMethod(methodBase.ReflectedType.Name, methodBase.Name);
            var threadTracer = GetThreadTracer(Thread.CurrentThread.ManagedThreadId);

            threadTracer.StartTrace(methodTracer);
        }

        public void StopTrace()
        {
            GetThreadTracer(Thread.CurrentThread.ManagedThreadId).StopTrace();
        }

        private TraceThread GetThreadTracer(int id)
        {
            lock (ThreadLocker)
            {
                if (!_threads.TryGetValue(id, out TraceThread thread))
                {
                    thread = new TraceThread(id);
                    _threads.Add(id, thread);
                }

                return thread;
            }
        }
    }
}