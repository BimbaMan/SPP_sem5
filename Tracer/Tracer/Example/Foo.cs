using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Example
{
    public class Foo
    {
        private Bar _bar;
        private ITracer _tracer;

        public Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }
        
        public void M0()
        {
            M1();
            M2();
        }

        private void M1()
        {
            _tracer.StartTrace();
            Thread.Sleep(300);
            _tracer.StopTrace();
        }

        private void M2()
        {
            _tracer.StartTrace();
            Thread.Sleep(200);
            _tracer.StopTrace();
        }
        public void CustomMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);

            _bar.InnerMethod();

            _tracer.StopTrace();
        }
    }
}
