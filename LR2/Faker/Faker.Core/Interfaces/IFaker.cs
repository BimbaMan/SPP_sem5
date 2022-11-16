using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Interfaces
{
    public interface IFaker
    {
        public T Create<T>();
    }
}
