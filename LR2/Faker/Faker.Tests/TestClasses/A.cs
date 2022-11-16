namespace Faker.Tests.TestClasses;

class A
{
    public B b { get; set; }
}

class B
{
    public C c { get; set; }
}


class C
{
    public A a { get; set; }
}