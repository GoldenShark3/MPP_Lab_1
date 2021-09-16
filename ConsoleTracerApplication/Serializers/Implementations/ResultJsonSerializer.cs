using System.Text.Json;
using Tracer.TraceResults;

namespace ConsoleTracerApplication.Serializers.Implementations
{
    public class ResultJsonSerializer : AbstractSerializer<TraceResult>
    {
        public ResultJsonSerializer(SerializeOption serializeOption) : base(serializeOption) { }

        public override void Serialize(TraceResult data)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = Option.WriteIndented
            };
            var jsonString = JsonSerializer.Serialize(data, options);
            
            Option.Writer.WriteLine(jsonString);
        }

    }
}