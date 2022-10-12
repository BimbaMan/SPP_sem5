using System.Diagnostics;
using Tracer.Core.Interfaces;
using Tracer.Core.Structure;

namespace Tracer.Core;

public class Tracer : ITracer
{
    public Tracer()
    {
        _traceResult = new TraceResult();
    }

    private TraceResult _traceResult;

    public void StartTrace()
    {
        StackTrace stackTrace = new StackTrace(1);
        var method = stackTrace.GetFrame(0)?.GetMethod();
        TraceMethod traceMethod = new TraceMethod()
            {Method_name = method!.Name, Method_class = method.DeclaringType!.Name};
        _traceResult.AddMethodToTheradList(traceMethod, Environment.CurrentManagedThreadId);
        traceMethod.StartTrace();
    }

    public void StopTrace()
    {
        _traceResult.PopMethodFromThreadList(Environment.CurrentManagedThreadId);
    }

    public TraceResult GetTraceResult() => _traceResult;
}