using System.Diagnostics.CodeAnalysis;
using DomainDrivenDesign.UnitTests.Data;
using FluentAssertions;

namespace DomainDrivenDesign.UnitTests.TestCases;

public class ValueObjectTestCase
{
    [Fact]
    public void Clone_ReturnsMemberwiseCloneObject()
    {
        var obj = new TestValueObject();
        var exp = new TestValueObject
        {
            Age = obj.Age,
            Name = obj.Name?.Clone() as string
        };
        var act = obj.Clone() as TestValueObject;

        act.Should().Be(exp);
    }

    [Fact]
    public void Equals_WithSameReference_ReturnsTrue()
    {
        const bool exp = true;
        var objA = new TestValueObject();
        var objB = new TestValueObject();
        bool act = objA.Equals(objB);

        act.Should().Be(exp);
    }

    [Fact]
    public void Equals_WithNull_ReturnsFalse()
    {
        const bool exp = false;
        var obj = new TestValueObject();
        bool act = obj.Equals(null);

        act.Should().Be(exp);
    }

    [Theory]
    [InlineData(0, "")]
    [InlineData(0, null)]
    [InlineData(int.MaxValue, "")]
    [InlineData(int.MinValue, "")]
    public void GetHashCode_ReturnsCorrectHashCode(int age, string name)
    {
        var obj = new TestValueObject { Age = age, Name = name };
        int exp = HashCode.Combine(obj.Age, obj.Name);
        int act = obj.GetHashCode();

        act.Should().Be(exp);
    }

    [Theory]
    [InlineData(0, null)]
    [InlineData(1, "")]
    public void EqualOperator_WithEqualValuedValueObjects_ReturnsTrue(int ageA, string nameA)
    {
        const bool exp = true;
        var objA = new TestValueObject { Age = ageA, Name = nameA };
        var objB = new TestValueObject { Age = ageA, Name = nameA };
        bool act = objA == objB;

        act.Should().Be(exp);
    }

    [Fact]
    public void EqualOperator_WithAtLeastOneNullValueObjects_ReturnsFalse()
    {
        const bool exp = false;
        var objA = new TestValueObject();
        TestValueObject objB = null!;
        bool act = objA == objB;

        act.Should().Be(exp);
    }

    [Fact]
    [SuppressMessage("ReSharper", "EqualExpressionComparison")]
    public void EqualOperator_WithSameReference_ReturnsTrue()
    {
        const bool exp = true;
        var obj = new TestValueObject();
        bool act = obj == obj;

        act.Should().Be(exp);
    }

    [Theory]
    [InlineData(0, null, 0, "")]
    [InlineData(1, "", 1, null)]
    [InlineData(1, "", 2, "Test")]
    public void NotEqualOperator_WithNotEqualValuedValueObjects_ReturnsTrue(int ageA, string nameA, int ageB,
        string nameB)
    {
        const bool exp = true;
        var objA = new TestValueObject { Age = ageA, Name = nameA };
        var objB = new TestValueObject { Age = ageB, Name = nameB };
        bool act = objA != objB;

        act.Should().Be(exp);
    }

    [Fact]
    [SuppressMessage("ReSharper", "EqualExpressionComparison")]
    public void NotEqualOperator_WithSameReference_ReturnsFalse()
    {
        const bool exp = false;
        var obj = new TestValueObject();
        bool act = obj != obj;

        act.Should().Be(exp);
    }
}