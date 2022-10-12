using Newtonsoft.Json;
using Tracer.Core.Structure;
using Tracer.Serialization.Abstractions;

namespace Tracer.Serialization.Json;

public class SerializationJson: ITraceResultSerializer
{
    public void Serialize(TraceResult traceResult, Stream stream_to)
    {
        using (StreamWriter streamWriter = new StreamWriter(stream_to))
        {
            using (JsonTextWriter jsonTextWriter = new JsonTextWriter(streamWriter))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(jsonTextWriter, traceResult);
            }
        }
    }
}