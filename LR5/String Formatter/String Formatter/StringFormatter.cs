using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace String_Formatter
{
    public interface IStringFormatter
    {
        string Format(string template, object target);
    }

    internal class StringFormatter : IStringFormatter
    {
        public static readonly StringFormatter Shared = new StringFormatter();
        public string Format(string template, object target)
        {
            //Разбор target на полученные переменные и их значения
            Type targetType = target.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(targetType.GetProperties());

            List<string> targetName = new List<string>();
            List<string> targetValue = new List<string>();

            foreach (PropertyInfo prop in props)
            {
                targetName.Add(prop.Name);
                targetValue.Add(prop.GetValue(target, null).ToString());
            }

            //Разбиение строки по скобкам
            List<string> templateList = new List<string>();
            templateList.AddRange(template.Split(' '));
            
            for (int i = 0; i < templateList.Count; i++)
            {
                if (templateList[i].Contains("{{"))
                {
                    if (templateList[i].Contains("}}"))
                    {
                        for (int j = 0; j < targetName.Count; j++)
                        {
                            if (templateList[i].Contains(targetName[j]))
                                templateList[i] = templateList[i].Replace("{{" + targetName[j] + "}}", targetValue[j]);
                        }
                    }
                    else
                    {
                        throw new Exception("Syntax error");
                    }
                }
            }
            return string.Join(" ", templateList);; 
        }
    }
}
