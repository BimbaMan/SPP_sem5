using Core;
using System.Reflection;
using Serialization.Abstractions;

namespace Example
{
    public class Program
    {
        public static void Main()
        {
            System.Console.WriteLine("Tracer in process...");
            TracerRealizer tracer = new TracerRealizer();
            Thread FirstThread = new Thread(new Foo(tracer).CustomMethod),
            SecondThread = new Thread(new Bar(tracer).InnerMethod);
            Thread ThirdThread = new Thread(new Foo(tracer).M0);
            List <Thread> threads = new List<Thread>();
            threads.Add(FirstThread);
            threads.Add(SecondThread);
            threads.Add(ThirdThread);

            foreach (Thread thread in threads)
            {
                thread.Start();
            }

            Boolean threadEnd = false;

            while (!threadEnd)
            {
                foreach (Thread thread in threads)
                {
                    if (thread.IsAlive)
                        threadEnd = false;
                    else
                        threadEnd = true;
                }
            }

            TraceResult threadtracerres = tracer.GetResult();

            Assembly assembly = Assembly.LoadFrom("Z:/Tracer/Tracer/SerializeLibrary/SerializeLibrary/bin/Debug/net6.0/SerializeLibrary.dll"); 
            
            Type[] serTypes = assembly.GetTypes();

            foreach (Type serType in serTypes)
            {
                if (serType.GetInterface("ITraceResultSerializer") != null)
                {
                    Console.WriteLine(serType.Name);
                    ITraceResultSerializer serializer = (ITraceResultSerializer)assembly.CreateInstance(serType.FullName);
                    StreamWriter sw1 = new StreamWriter(serType.Name + serializer.Format);
                    StreamWriter sw2 = new StreamWriter(serType.Name + ".txt");
                    serializer.Serialize(threadtracerres, sw1);
                    serializer.Serialize(threadtracerres, sw2);
                }
            }
            Console.WriteLine("Tracer finished successfully!");
           Console.WriteLine("Total threads: " + threads.Count);
        }
    }
}