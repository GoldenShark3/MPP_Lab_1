using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Tracer.Implementations;

namespace Tracer.TraceResults
{
    [Serializable]
    public class TraceResult
    {
        [JsonPropertyName("threads")]
        public List<TraceThread> Threads { get; }

        public TraceResult() { }

        public TraceResult(Dictionary<int, TraceThread> threads)
        {
            Threads = new List<TraceThread>();

            foreach (var thread in threads)
            {
                Threads.Add(thread.Value.GetTraceResult());
            }
        }
    }
}