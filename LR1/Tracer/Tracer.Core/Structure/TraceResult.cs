namespace Tracer.Core.Structure;

public class TraceResult
{
    private readonly object Locker = new object();
    public TraceResult()
    {
        _threadslist = new List<TraceThread>();
    }

    public List<TraceThread> Threadslist => _threadslist;
    private List<TraceThread> _threadslist;

    public void AddMethodToTheradList(TraceMethod method, int threadId)
    {
        var thread = _threadslist.SingleOrDefault(x => x.Id == threadId);

        if (thread == null)
        {
            thread = new TraceThread() {Id = threadId};
            lock (Locker)
            {
                _threadslist.Add(thread);
            }
        }

        thread.AddMethod(method);
    }

    public void PopMethodFromThreadList(int threadId) => _threadslist.Single(x => x.Id == threadId).PopMethod();
}