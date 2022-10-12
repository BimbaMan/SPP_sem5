using YamlDotNet.Serialization;
using Tracer.Core.Structure;
using Tracer.Serialization.Abstractions;

namespace Tracer.Serialization.Yaml;

public class SerializationYaml: ITraceResultSerializer
{
    public void Serialize(TraceResult traceResult, Stream stream_to)
    {
        using (StreamWriter streamWriter = new StreamWriter(stream_to))
        {
            TextWriter yamlWriter = streamWriter;
            Serializer serializer = new Serializer();
            serializer.Serialize(yamlWriter, traceResult);
        }
    }
}