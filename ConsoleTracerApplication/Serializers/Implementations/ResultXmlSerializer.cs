using System.Linq;
using System.Xml.Linq;
using ConsoleTracerApplication.Serializers.Interfaces;
using Tracer.Implementations;
using Tracer.TraceResults;

namespace ConsoleTracerApplication.Serializers.Implementations
{
    public class ResultXmlSerializer : AbstractSerializer<TraceResult>
    {
       
        public ResultXmlSerializer(SerializeOption serializeOption) : base(serializeOption) { }

        public override void Serialize(TraceResult data)
        {
            Option.Writer.WriteLine( new XDocument( new XElement("root",
                from thread in data.Threads select SerializeThreadInfo(thread))).ToString());
        }

        private XElement SerializeThreadInfo(TraceThread thread)
        {
            return new XElement("thread",
                new XAttribute("id", thread.Id),
                new XAttribute("time", thread.TotalElapsedTime + "ms"),
                from method in thread.Methods select SerializeMethodInfo(method));
        }

        private XElement SerializeMethodInfo(TraceMethod method)
        {
            var serializedMethod = new XElement("method",
                new XAttribute("name", method.MethodName),
                new XAttribute("time", method.ElapsedTime + "ms"),
                new XAttribute("class", method.ClassName));

            if (method.Methods.Count > 0)
            {
                serializedMethod.Add(from innerMethod in method.Methods select SerializeMethodInfo(innerMethod));
            }

            return serializedMethod;
        } 
    }
}