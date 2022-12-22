using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace TestsGeneratorDll.Includes
{
    internal class AssembliesProvider
    {
        int runningCount;
        object sync = new object(); 

        List<string> filePaths;
        List<Assembly> filesAssemblies = new List<Assembly>();

        public List<Assembly> GetAssemblies(List<string> filePaths, int restriction)
        {
            this.filePaths = filePaths;

            Func<string, Assembly> functionToExacute = GetAssembly;
            GetAssembliesByUsingMonitor(functionToExacute, restriction);

            return filesAssemblies;

        }
        
        public void GetAssembliesByUsingMonitor(Func<string, Assembly> function, int restriction)
        {
            object param;

            runningCount = filePaths.Count;
            ThreadPool.SetMaxThreads(restriction, restriction); 

            for (int i = 0; i < filePaths.Count; i++) 
            {
                param = new List<object>() { function, i };
                ThreadPool.QueueUserWorkItem(AddAssemblyAction, param); 
            }
            
            lock (sync) //заглушка
                if (runningCount > 0)
                    Monitor.Wait(sync);
        }

        private void AddAssemblyAction(object state)
        {
            List<object> parameters = (List<object>)state;
            var function = (Func<string, Assembly>)parameters[0];
            string filePath = filePaths[(int)parameters[1]];
            Assembly assembly = function(filePath); 
            
            lock (sync)
            {
                filesAssemblies.Add(assembly);

                runningCount--;

                if (runningCount == 0)
                    Monitor.Pulse(sync); 
            }
        }


        private Assembly GetAssembly(string filePath) 
        {
            return Assembly.LoadFile(filePath); 
        }
    }
}
