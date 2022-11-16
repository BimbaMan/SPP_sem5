using System.Reflection;
using System.Runtime.CompilerServices;

namespace Faker.Core.Generators
{
    abstract class SimpleGenerator
    {

        public static IReadOnlyDictionary<Type, Func<object>> SimpleGenerators
        {
            get { return simpleGenerators;  }
        }

        protected static Dictionary<Type, Func<object>> simpleGenerators;
        protected static Random _random;

        static SimpleGenerator()
        {
            simpleGenerators = new();
            _random = new(1);
            InitAll();
        }

        protected static byte[] GetBytes(int length)
        {
            byte[] bytes = new byte[length];
            _random.NextBytes(bytes);
            return bytes;
        }

        private static void InitAll() {
            var subClasses = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.IsAssignableTo(typeof(SimpleGenerator)) && x != typeof(SimpleGenerator));
            foreach (var subClass in subClasses) RuntimeHelpers.RunClassConstructor(subClass.TypeHandle);
        }
    }
}
