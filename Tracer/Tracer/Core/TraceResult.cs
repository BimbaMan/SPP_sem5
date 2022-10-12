using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [Serializable]
    public class TraceResult
    {
        public List<ThreadTracer> ThreadTracersResult;
        public TraceResult(List<ThreadTracer> threadTracers)
        {
            ThreadTracersResult = new List<ThreadTracer>(threadTracers);
        }

        public TraceResult()
        {
            ThreadTracersResult = new List<ThreadTracer>();
        }
    }
}
