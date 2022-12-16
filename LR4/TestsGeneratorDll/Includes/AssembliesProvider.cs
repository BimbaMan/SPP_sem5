using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace TestsGeneratorDll.Includes
{
    // класс - поставщик сборок
    internal class AssembliesProvider
    {
        int runningCount;
        object sync = new object(); //синхроназатор

        List<string> filePaths;
        List<Assembly> filesAssemblies = new List<Assembly>();

        public List<Assembly> GetAssemblies(List<string> filePaths, int restriction)
        {
            this.filePaths = filePaths;

            Func<string, Assembly> functionToExacute = GetAssembly; //делегат (для возможности ссылаться на GetAssembly)
            GetAssembliesByUsingMonitor(functionToExacute, restriction);

            return filesAssemblies;

        }
        
        public void GetAssembliesByUsingMonitor(Func<string, Assembly> function, int restriction)
        {
            object param;

            runningCount = filePaths.Count;
            ThreadPool.SetMaxThreads(restriction, restriction); //(количесвто рабочих потоков в пуле, количество асинк потоков ввода-вывода)

            for (int i = 0; i < filePaths.Count; i++) 
            {
                param = new List<object>() { function, i };
                ThreadPool.QueueUserWorkItem(AddAssemblyAction, param); //Помещает метод в очередь на выполнение и указывает объект, содержащий данные для использования методом.
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
            Assembly assembly = function(filePath); //вызов GetAssebly
            
            lock (sync)
            {
                filesAssemblies.Add(assembly);

                runningCount--;

                if (runningCount == 0)
                    Monitor.Pulse(sync); // Pulse уведомляет главный поток из очереди ожидания, что текущий поток освободил объект локер
            }
        }


        private Assembly GetAssembly(string filePath) //получение сборки из пути к файлу
        {
            return Assembly.LoadFile(filePath); 
        }
    }
}
