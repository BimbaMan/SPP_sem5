using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Generators
{
    sealed class SimpleGeneratorNumeric : SimpleGenerator
    {
        static SimpleGeneratorNumeric()
        {
            var genFunctions = typeof(SimpleGeneratorNumeric).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);

            foreach (var func in genFunctions) simpleGenerators[func.ReturnType] = () => func.Invoke(null, null);
        }

        private static short Int16() => BitConverter.ToInt16(GetBytes(sizeof(short)));
        private static int Int32() => BitConverter.ToInt32(GetBytes(sizeof(int)));
        private static ushort UInt16() => BitConverter.ToUInt16(GetBytes(sizeof(ushort)));
        private static uint UInt32() => BitConverter.ToUInt32(GetBytes(sizeof(uint)));
        private static float Float() => BitConverter.ToSingle(GetBytes(sizeof(float)));
        private static double Double() => BitConverter.ToDouble(GetBytes(sizeof(double)));
    }
}
