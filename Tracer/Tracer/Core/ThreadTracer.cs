using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Core
{
    [Serializable]
    public class ThreadTracer
    {
        static object balanceLock = new object();
        public List<MethodTraceInfo> MethodTree;
        public int Id;
        public string ClassName, MethodName;
        public long Time;
        
        public ThreadTracer()
        {

        }

        public ThreadTracer(int id)
        {
            MethodTree = new List<MethodTraceInfo>();
            Id = id;
        }

        public void StartTrace()
        {
            StackTrace sT = new StackTrace(true);
            Stack<MethodTraceInfo> CurrentMethods = new Stack<MethodTraceInfo>();

            for (int i = 2; i < sT.FrameCount - 1; i++)
            {
                StackFrame sf = sT.GetFrame(i);
                
                MethodName = sf.GetMethod().Name;

                ClassName = sf.GetFileName();
                ClassName = ClassName.Substring(ClassName.LastIndexOf('\\') + 1);
                ClassName = ClassName.Substring(0, ClassName.LastIndexOf('.'));

                CurrentMethods.Push(new MethodTraceInfo(MethodName, ClassName));
            }

            GetMethod(CurrentMethods).StopWatch.Start();
        }

        public void StopTrace()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            StackTrace sT = new StackTrace(true);
            Stack<MethodTraceInfo> CurrentMethods = new Stack<MethodTraceInfo>();

            for (int i = 2; i < sT.FrameCount - 1; i++)
            {
                StackFrame sf = sT.GetFrame(i);

                MethodName = sf.GetMethod().Name;

                ClassName = sf.GetFileName();
                ClassName = ClassName.Substring(ClassName.LastIndexOf('\\') + 1);
                ClassName = ClassName.Substring(0, ClassName.LastIndexOf('.'));

                CurrentMethods.Push(new MethodTraceInfo(MethodName, ClassName));
            }

            MethodTraceInfo methodTraceInfo = GetMethod(CurrentMethods);
            methodTraceInfo.StopWatch.Stop();
            stopWatch.Stop();
            methodTraceInfo.Time = methodTraceInfo.StopWatch.ElapsedMilliseconds - stopWatch.ElapsedMilliseconds;
        }

        public MethodTraceInfo GetMethod(Stack<MethodTraceInfo> CurrentMethods)
        {
            MethodTraceInfo info = null, method = null;
            List<MethodTraceInfo> methodlist = MethodTree;
            while(CurrentMethods.Count > 0)
            {
                info = CurrentMethods.Pop();
                method = null;

                foreach (MethodTraceInfo methodInfo in methodlist)
                {
                    if(methodInfo.Name == info.Name && methodInfo.ClassName == info.ClassName)
                    {
                        methodlist = methodInfo.ChildrenMethods;
                        method = methodInfo;
                        break;
                    }
                }
            }

            if(method == null)
            {
                methodlist.Add(info);
            }
            else
            {
                return method;
            }

            return info;
        }
    }
}
