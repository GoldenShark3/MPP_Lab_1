﻿using Tracer.TraceResults;

namespace Tracer.Interfaces
{
    public interface ITracer
    {
        void StartTrace();

        void StopTrace();

        TraceResult GetTraceResult();
    }
}