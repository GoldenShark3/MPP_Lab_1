using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Tracer.Implementations
{
    public class TraceMethod
    {
        [JsonPropertyName("name")]
        public string MethodName { get; private set; }

        [JsonPropertyName("class")]
        public string ClassName { get; private set; }

        [JsonPropertyName("time")]
        public double ElapsedTime { get; private set; }

        [JsonPropertyName("methods")]
        public List<TraceMethod> Methods { get; internal set; }

        private readonly Stopwatch Stopwatch;

        public TraceMethod(string className, string methodName)
        {
            ClassName = className;
            MethodName = methodName;
            Methods = new List<TraceMethod>();
            Stopwatch = new Stopwatch();
        }

        internal TraceMethod GetTraceResult()
        {
            var result = new TraceMethod(ClassName, MethodName);
            result.ElapsedTime = ElapsedTime;

            foreach (var method in Methods)
            {
                result.Methods.Add(method.GetTraceResult());
            }

            return result;
        }

        internal void StartTrace()
        {
            Stopwatch.Start();
        }

        internal void StopTrace()
        {
            Stopwatch.Stop();
            ElapsedTime = Stopwatch.Elapsed.TotalMilliseconds;
        }
    }
}