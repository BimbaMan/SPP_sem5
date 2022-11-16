using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Generators
{
    sealed class SimpleGeneratorString : SimpleGenerator
    {
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        static SimpleGeneratorString()
        {
            var genFunctions = typeof(SimpleGeneratorString).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);

            foreach (var func in genFunctions) simpleGenerators[func.ReturnType] = () => func.Invoke(null, null);
        }

        private static string String() => new(Enumerable.Repeat(chars, _random.Next(0, 128)).Select(s => s[_random.Next(s.Length)]).ToArray());
        private static char Char() => chars[_random.Next(chars.Length)];
    }
}
