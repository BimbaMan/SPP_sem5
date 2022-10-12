using System.Diagnostics;

namespace Tracer.Core.Structure;

public class TraceMethod
{
    public List<TraceMethod> InnerMethods = new List<TraceMethod>();
    public string Method_name { get; set; }
    public string Method_class { get; set; }
    
    private Stopwatch _stopwatch;
    public long TimeSpans { get; set; }

    public void StartTrace()
    {
        _stopwatch = new Stopwatch();
        _stopwatch.Start();
    }
    
    public void StopTrace()
    {
        _stopwatch.Stop();
        TimeSpans = _stopwatch.ElapsedMilliseconds;
    }

}