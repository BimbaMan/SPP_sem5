using System.Reflection;
using Tracer.Core.Interfaces;
using Tracer.Core.Structure;
using Tracer.Serialization.Abstractions;

namespace Tracer.Example;

public class Foo
{
    private Bar _bar;
    private ITracer _tracer;

    internal Foo(ITracer tracer)
    {
        _tracer = tracer;
        _bar = new Bar(_tracer);
    }

    public void MyMethod()
    {
        _tracer.StartTrace();
        _bar.InnerMethod();
        _bar.InnerMethod();
        _bar.Sec_InnerMethod();
        _bar.InnerMethod();

        Thread.Sleep(200);
        _tracer.StopTrace();
    }
}

public class Bar
{
    private ITracer _tracer;

    internal Bar(ITracer tracer)
    {
        _tracer = tracer;
    }

    public void InnerMethod()
    {
        _tracer.StartTrace();
        Thread.Sleep(100);
        Sec_InnerMethod();
        _tracer.StopTrace();
    }

    public void Sec_InnerMethod()
    {
        _tracer.StartTrace();
        Thread.Sleep(50);
        _tracer.StopTrace();
    }
}

internal static class Program
{
    static void Main()
    {
        ITracer tracer = new Tracer.Core.Tracer();

        Thread newThread = new Thread(() =>
        {
            var buff = new Foo(tracer);
            buff.MyMethod();
        });
        newThread.Start();
        newThread.Join();
        var buff = new Foo(tracer);
        buff.MyMethod();
        var result = tracer.GetTraceResult();
        JsonSerializationExample(result);
        YamlSerializationExample(result);
        XmlSerializationExample(result);
    }

    static void JsonSerializationExample(TraceResult traceResult)
    {
        using FileStream fileStream = File.Open(@"1result\Result.json", FileMode.Create);
        Assembly assembly =
            Assembly.LoadFrom(
                @"C:\Users\Иван\Desktop\Уник\СПП\LR1\Tracer.Serialization\Tracer.Serialization.Json\bin\Debug\net6.0\Tracer.Serialization.Json.dll");
        Type type = assembly.GetType("Tracer.Serialization.Json.SerializationJson", true);
        var obj = (ITraceResultSerializer) Activator.CreateInstance(type);
        obj.Serialize(traceResult, fileStream);
    }

    static void YamlSerializationExample(TraceResult traceResult)
    {
        using FileStream fileStream = File.Open(@"1result\Result.yaml", FileMode.Create);
        Assembly assembly =
            Assembly.LoadFrom(
                @"C:\Users\Иван\Desktop\Уник\СПП\LR1\Tracer.Serialization\Tracer.Serialization.Yaml\bin\Debug\net6.0\Tracer.Serialization.Yaml.dll");
        Type type = assembly.GetType("Tracer.Serialization.Yaml.SerializationYaml", true);
        var obj = (ITraceResultSerializer) Activator.CreateInstance(type);
        obj.Serialize(traceResult, fileStream);
    }

    static void XmlSerializationExample(TraceResult traceResult)
    {
        using FileStream fileStream = File.Open(@"1result\Result.xml", FileMode.Create);
        Assembly assembly =
            Assembly.LoadFrom(
                @"C:\Users\Иван\Desktop\Уник\СПП\LR1\Tracer.Serialization\Tracer.Serialization.Xml\bin\Debug\net6.0\Tracer.Serialization.Xml.dll");
        Type type = assembly.GetType("Tracer.Serialization.Xml.SerializationXml", true);
        var obj = (ITraceResultSerializer) Activator.CreateInstance(type);
        obj.Serialize(traceResult, fileStream);
    }
}