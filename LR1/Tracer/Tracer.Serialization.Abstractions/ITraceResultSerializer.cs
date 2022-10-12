using Tracer.Core.Structure;

namespace Tracer.Serialization.Abstractions;

public interface ITraceResultSerializer
{
    void Serialize(TraceResult traceResult, Stream stream_to);
}

