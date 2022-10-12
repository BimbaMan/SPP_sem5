namespace Tracer.Core.Structure;

public class TraceThread
{
    public int Id { get; set; }

    public List<TraceMethod> InnerMethods => _rootMethods;
    public long TotalTime => _rootMethods.Sum(x => x.TimeSpans);


    private Stack<TraceMethod> _methodStack = new Stack<TraceMethod>();
    private List<TraceMethod> _rootMethods = new List<TraceMethod>();

    private bool InRootOfThread => _methodStack.Count == 0;

    public void AddMethod(TraceMethod tracedMethod)
    {
        if (InRootOfThread)
        {
            _rootMethods.Add(tracedMethod);
        }
        else
        {
            _methodStack.Peek().InnerMethods.Add(tracedMethod);
        }

        _methodStack.Push(tracedMethod);
    }

    public void PopMethod()
    {
        _methodStack.Pop().StopTrace();
    }
}