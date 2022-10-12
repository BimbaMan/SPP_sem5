using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Serialization
{
    public interface ITraceSerializer
    {
        string Serialize(TraceResult traceResult);
        TraceResult Deserialize(string traceResult);
        string GetFileExtension();
    }
}
