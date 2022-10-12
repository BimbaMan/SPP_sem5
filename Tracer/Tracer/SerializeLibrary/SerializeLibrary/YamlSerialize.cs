using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serialization.Abstractions;
using Core;

namespace SerializeLibrary
{
    internal class YamlSerialize : ITraceResultSerializer
    {
        public string Format { get; } = ".yaml";
        public void Serialize(TraceResult traceResult, StreamWriter to)
        {
            StringBuilder yaml = new StringBuilder();
            yaml.AppendLine("threads: ");
            foreach (var thread in traceResult.ThreadTracersResult)
            {
                yaml.AppendLine($"  - id: {thread.Id}");
                yaml.AppendLine($"\ttime: {thread.Time}ms");
                yaml.AppendLine($"\tmethods:");
                yaml.Append(GetMethodYaml(thread.MethodTree, "\t"));
            }
            to.WriteLine(yaml.ToString());
            to.Close();
        }

        private string GetMethodYaml(List<MethodTraceInfo> methods, string offset)
        {
            StringBuilder yaml = new();
            foreach (var method in methods)
            {
                yaml.AppendLine($"{offset}  - name: {method.Name}");
                yaml.AppendLine($"{offset}\tclass: {method.ClassName}");
                yaml.AppendLine($"{offset}\ttime: {method.Time}ms");
                yaml.Append(GetMethodYaml(method.ChildrenMethods, offset + "\t"));
            }
            return yaml.ToString();
        }
    }
}
