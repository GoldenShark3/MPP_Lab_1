using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tracer.Implementations
{
    public class TraceThread
    {
        [JsonPropertyName("id")]
        public int Id { get; private set; }

        [JsonPropertyName("time")]
        public double TotalElapsedTime { get; private set; }

        [JsonPropertyName("methods")]
        public List<TraceMethod> Methods { get; private set; }

        private Stack<TraceMethod> methodsStack; 

        public TraceThread(int id)
        {
            Id = id;
            Methods = new List<TraceMethod>();
            methodsStack = new Stack<TraceMethod>();
        }

        internal TraceThread GetTraceResult()
        {
            var result = new TraceThread(Id);
            result.TotalElapsedTime = TotalElapsedTime;

            foreach (var method in Methods)
            {
                result.Methods.Add(method.GetTraceResult());
            }

            return result;
        }

        internal void StartTrace(TraceMethod method)
        {
            if (methodsStack.Count > 0)
            {
                TraceMethod lastMethod = methodsStack.Peek();
                lastMethod.Methods.Add(method);
            }

            method.StartTrace();
            methodsStack.Push(method);
        }

        internal void StopTrace()
        {
            TraceMethod lastMethod = methodsStack.Pop();
            lastMethod.StopTrace();

            if (methodsStack.Count == 0)
            {
                Methods.Add(lastMethod);
                TotalElapsedTime += lastMethod.ElapsedTime;
            }
        }
    }
}