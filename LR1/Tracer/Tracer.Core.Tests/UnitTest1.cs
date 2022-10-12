using Tracer.Core.Interfaces;

namespace Tracer.Core.Tests;

public class Tests
{
    private readonly ITracer _tracer = new Tracer();
    private readonly int sleepTime = 100;

    private void First_Method()
    {
        _tracer.StartTrace();
        Thread.Sleep(sleepTime);
        _tracer.StopTrace();
    }

    private void Second_Method()
    {
        _tracer.StartTrace();
        Third_Method();
        First_Method();
        Third_Method();
        _tracer.StopTrace();
    }

    private void Third_Method()
    {
        _tracer.StartTrace();
        Thread.Sleep(sleepTime / 4);
        _tracer.StopTrace();
    }


    [Test]
    public void Time_test()
    {
        _tracer.StartTrace();
        Thread.Sleep(sleepTime);
        _tracer.StopTrace();
        long trace_Time = _tracer.GetTraceResult().Threadslist.Single(x => x.Id == Environment.CurrentManagedThreadId)
            .TotalTime;
        Assert.That(trace_Time, Is.GreaterThanOrEqualTo(sleepTime));
    }

    [Test]
    public void Name_test()
    {
        _tracer.StartTrace();
        Thread.Sleep(sleepTime);
        _tracer.StopTrace();
        string fact_Classname = _tracer.GetTraceResult().Threadslist
            .Single(x => x.Id == Environment.CurrentManagedThreadId).InnerMethods[0].Method_class;
        string fact_Methodsname = _tracer.GetTraceResult().Threadslist
            .Single(x => x.Id == Environment.CurrentManagedThreadId).InnerMethods[0].Method_name;
        
        Assert.That(fact_Methodsname, Is.EqualTo(nameof(Name_test)));
        Assert.That(fact_Classname, Is.EqualTo(nameof(Tests)));
    }
    
    [Test]
    public void ThreadCount_test()
    {
        _tracer.StartTrace();

        List<Thread> threads = new List<Thread>();
        for (int i = 0; i < 4; ++i)
        {
            var thread = new Thread(First_Method);
            threads.Add(thread);
            thread.Start();
        }

        foreach (Thread thread in threads) thread.Join();

        _tracer.StopTrace();

        int fact_ThreadCount = _tracer.GetTraceResult().Threadslist.Count;
        Assert.That(fact_ThreadCount, Is.EqualTo(5));
    }

    [Test]
    public void MethodCountTest()
    {
        _tracer.StartTrace();

        First_Method();
        Second_Method();
        Third_Method();

        _tracer.StopTrace();

        int fact_MethodCount = _tracer.GetTraceResult().Threadslist.Single(x => x.Id == Environment.CurrentManagedThreadId).InnerMethods[0].InnerMethods.Count;
        Assert.That(fact_MethodCount, Is.EqualTo(3));
    }
    
    
}