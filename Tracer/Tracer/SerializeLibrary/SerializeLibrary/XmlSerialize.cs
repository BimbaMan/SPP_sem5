using Serialization.Abstractions;
using Core;
using System.Xml.Linq;
using System.Xml.Serialization;



namespace SerializeLibrary
{
    public class XmlSerialize : ITraceResultSerializer
    {
        public string Format { get; } = ".xml";
        public void Serialize(TraceResult traceResult, StreamWriter to)
        {

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ThreadTracer>));
            xmlSerializer.Serialize(to, (List<ThreadTracer>)traceResult.ThreadTracersResult);
        }

    }
}