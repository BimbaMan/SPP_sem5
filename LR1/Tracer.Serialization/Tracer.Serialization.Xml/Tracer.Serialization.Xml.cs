using System.Xml.Serialization;
using Tracer.Core.Structure;
using Tracer.Serialization.Abstractions;

namespace Tracer.Serialization.Xml;

public class SerializationXml:ITraceResultSerializer
{
    public void Serialize(TraceResult traceResult, Stream stream_to)
    {
        using (StreamWriter streamWriter = new StreamWriter(stream_to))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TraceResult));
            xmlSerializer.Serialize(streamWriter, traceResult);
        }
    }
}