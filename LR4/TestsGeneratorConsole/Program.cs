using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestsGeneratorDll;

namespace TestsGeneratorConsole
{


    internal class Program
    {
        static void Main(string[] args)
        {

            List<string> files = new List<string>()
            {
                @"C:\Users\Иван\Desktop\Уник\СПП\LR4\TestsGeneratorConsole\bin\Debug\TestsGeneratorConsole.exe",
                @"C:\Users\Иван\Desktop\Уник\СПП\LR4\TestsGeneratorDll\bin\Debug\TestsGeneratorDll.dll"
            };
            //Путь результата
            TestsGenerator.GenerateXUnitTests(files, @"C:\Users\Иван\Desktop\Уник\СПП\LR4\Result", 5);
            
        }

    }

    public class Tests
    {
        static void TestOne()
        {
            Console.WriteLine(nameof(TestOne));
        }

        static void TestTwo()
        {
            Console.WriteLine(nameof(TestTwo));
        }
    }

    public class Bsuir
    {
        public class Sisharp
        {
            public class Teacher
            {
                static string HelloTeacher()
                {
                    Console.WriteLine("Hello");
                    return String.Empty;
                }
            }
            
            static string HelloStudents()
            {
                Console.WriteLine("Hello");
                return String.Empty;
            }
        }
        
        static string HelloWorld(string text_line)
        {
            Console.WriteLine("Hello");
            return String.Empty;
        }
    }



    public class MyClass
    {
        public void FirstMethod()
        {
            Console.WriteLine("First method");
        }

        public void SecondMethod()
        {
            Console.WriteLine("Second method");
        }

        public void ThirdMethod(int a)
        {
            Console.WriteLine("Third method (int)");
        }

        public void ThirdMethod(double a)
        {
            Console.WriteLine("Third method (double)");
        }
    }
}
