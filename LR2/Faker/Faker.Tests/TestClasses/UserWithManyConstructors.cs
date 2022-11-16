using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Tests.TestClasses
{
    public class UserWithManyConstructors
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime createdAt { get; set; }

        public string password;

        public UserWithManyConstructors(int id)
        {
            this.id = id;
        }

        public UserWithManyConstructors(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
