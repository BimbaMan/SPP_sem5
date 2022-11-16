using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Generators
{
    sealed class SimpleGeneratorTime : SimpleGenerator
    {
        static SimpleGeneratorTime()
        {
            var genFunctions = typeof(SimpleGeneratorTime).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);

            foreach (var func in genFunctions) simpleGenerators[func.ReturnType] = () => func.Invoke(null, null);
        }

        private static TimeSpan TimeStamp() => new(_random.NextInt64(TimeSpan.TicksPerDay*7));
        private static DateTime GetDateTime() => new(_random.NextInt64(DateTime.Today.Ticks));
    }
}
