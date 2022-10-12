using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Serialization.Abstractions;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml;


namespace SerializeLibrary
{
    public class JsonSerialize : ITraceResultSerializer
    {
        public string Format { get; } = ".json";
        public void Serialize(TraceResult traceResult, StreamWriter to)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(List<ThreadTracer>));
            using (FileStream fs = (FileStream)to.BaseStream)
            {
                formatter.WriteObject(fs, (List<ThreadTracer>)traceResult.ThreadTracersResult);
            }
        }
    }
}
