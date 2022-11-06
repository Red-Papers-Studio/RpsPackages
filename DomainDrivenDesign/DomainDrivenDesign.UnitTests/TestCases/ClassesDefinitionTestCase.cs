using System.Reflection;
using DomainDrivenDesign.UnitTests.Data;
using FluentAssertions;

namespace DomainDrivenDesign.UnitTests.TestCases;

public class ClassesDefinitionTestCase
{
    [Fact]
    public void DomainExceptionClass_IsWellDefined()
    {
        Type act = typeof(DomainException);
        act.Should().BeDerivedFrom<Exception>().And.HaveDefaultConstructor().And
            .HaveConstructor(new List<Type> { typeof(string) }).And
            .HaveConstructor(new List<Type> { typeof(string), typeof(Exception) });
    }

    [Fact]
    public void TestEntityClass_IsWellDefined()
    {
        Type act = typeof(TestEntity<int>);
        act.Should().BeDerivedFrom<Entity<int>>();
    }

    [Fact]
    public void TestEventClass_IsWellDefined()
    {
        Type act = typeof(TestEvent);
        act.Should().BeDerivedFrom<Event>();
    }

    [Fact]
    public void TestEnumerationClass_IsWellDefined()
    {
        Type act = typeof(TestEnumeration);
        act.Should().BeDerivedFrom<Enumeration<int>>().And.Implement<IComparable<Enumeration<int>>>();
        act.GetFields(BindingFlags.Public |
                          BindingFlags.Static |
                          BindingFlags.DeclaredOnly).Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void TestValueObjectClass_IsWellDefined()
    {
        Type act = typeof(TestValueObject);
        act.Should().BeDerivedFrom<ValueObject>().And.Implement<ICloneable>().And
            .HaveProperty<string?>(nameof(TestValueObject.Name)).And
            .HaveProperty<int>(nameof(TestValueObject.Age));
    }
}