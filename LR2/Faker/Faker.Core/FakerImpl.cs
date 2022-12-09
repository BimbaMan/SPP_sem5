using Faker.Core.Generators;
using Faker.Core.Interfaces;
using System.Collections;
using System.Reflection;

namespace Faker.Core
{
    public class Faker : IFaker
    {
        public T Create<T>() => (T)Create(typeof(T));
        private HashSet<Type> _dependens = new();
        
        private object Create(Type type)
        {
            if (SimpleGenerator.SimpleGenerators.ContainsKey(type))
            {
                return SimpleGenerator.SimpleGenerators[type]();
            }
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                var generatedList = Activator.CreateInstance(type) as IList;
                for (int i = 0; i < 5; i++)
                {
                    generatedList.Add(Create(type.GetGenericArguments()[0]));
                }
                return generatedList;
            }

            if (_dependens.Contains(type))
            {
                throw new Exception("Cycle depends");
            }
            _dependens.Add(type);

            object result = CreateObj(type);

            _dependens.Remove(type);
            return result;
        }
        
        private object CreateObj(Type type)
        {
            object result;
            ConstructorInfo constructor = null;
            result = null;
            var sortedConstructors = type
                .GetConstructors()
                .OrderByDescending(info => info.GetParameters().Length)
                .ToList();
            foreach (var constructorToCreate in sortedConstructors)
            {
                try
                {
                    constructor = constructorToCreate;
                    result = CreateObj(constructorToCreate);
                    if (result != null)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    
                }
            }
            var parameters = constructor.GetParameters();
            var publicSetters = type.GetProperties().Where(x => x.CanWrite && x.SetMethod.IsPublic);
            IEnumerable<FieldInfo> publicFields = type.GetFields().Where(x => x.IsPublic);

            publicSetters = publicSetters.Where(x => !parameters.Any(y => x.Name == "set_" + y.Name));
            publicFields = publicFields.Where(x => !parameters.Any(y => y.Name == x.Name)).ToArray();
            
            foreach (var field in publicFields)
                field.SetValue(result, Create(field.FieldType));
            foreach (var prop in publicSetters)
                prop.SetValue(result, Create(prop.PropertyType));
            return result;
        }
        private object CreateObj(ConstructorInfo constructor)
        {
            List<object> parametersList = new();
            foreach (var param in constructor.GetParameters())
            {
                parametersList.Add(Create(param.ParameterType));
            }
            return constructor.Invoke(parametersList.ToArray());
        }
    }
}