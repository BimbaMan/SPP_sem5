using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Serialization.Abstractions
{
    public interface ITraceResultSerializer
    {
        // Опционально: возвращает формат, используемый сериализатором (xml/json/yaml).
        // Может быть удобно для выбора имени файлов (см. ниже).
        public string Format { get; }
        void Serialize(TraceResult traceResult, StreamWriter to);
    }
}
