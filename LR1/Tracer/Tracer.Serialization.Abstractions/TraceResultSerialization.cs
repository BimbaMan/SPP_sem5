using Tracer.Core.Structure;

namespace Tracer.Serialization.Abstractions;

public class TraceResultSerialization: ITraceResultSerializer
{
    public void Serialize(TraceResult traceResult, Stream stream_to)
    {
        using (StreamWriter streamWriter = new StreamWriter(stream_to))
        {
            foreach (var thread in traceResult.Threadslist)
            {
                streamWriter.WriteLine($"Thread ID {thread.Id}");
                foreach (var method in thread.InnerMethods)
                {
                    streamWriter.WriteLine($"{method.Method_class}.{method.Method_name} - {method.TimeSpans}");
                    foreach (var methodInner in method.InnerMethods)
                    {
                        WriteToFile(methodInner, " ", streamWriter);
                    }
                }
            }
        }
    }

    private void WriteToFile(TraceMethod traceMethod, string offset, StreamWriter stream_to)
    {
        stream_to.WriteLine($"{offset}{traceMethod.Method_class}.{traceMethod.Method_name} - {traceMethod.TimeSpans}");
        foreach (var methodInner in traceMethod.InnerMethods)
        {
            WriteToFile(methodInner, offset+" ", stream_to);
        }
    }
}