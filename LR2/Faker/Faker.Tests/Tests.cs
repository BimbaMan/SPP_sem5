using Faker.Core.Interfaces;
using Faker.Tests.TestClasses;

namespace Faker.Tests
{
    public class UnitTest1
    {
        private IFaker _faker = new Core.Faker();

        [Fact]
        public void NumericTest()
        {
            Assert.IsType<int>(_faker.Create<int>());
            Assert.IsType<float>(_faker.Create<float>());
            Assert.IsType<double>(_faker.Create<double>());
            Assert.IsType<short>(_faker.Create<short>());
        }

        [Fact]
        public void StringTest()
        {
            Assert.IsType<string>(_faker.Create<string>());
            Assert.IsType<char>(_faker.Create<char>());
        }
        
        [Fact]
        public void DateTest()
        {
            Assert.IsType<TimeSpan>(_faker.Create<TimeSpan>());
            Assert.IsType<DateTime>(_faker.Create<DateTime>());
        }

        [Fact]
        public void ComplexType()
        {
            Assert.NotNull(_faker.Create<UserWithManyConstructors>());
        }

        [Fact]
        public void ListTest()
        {
            Assert.NotNull(_faker.Create<UserGroup>());
        }
        
        [Fact]
        public void CircularDependencyTest()
        {
            Assert.Throws<Exception>(() => _faker.Create<A>());
        }
    }
}