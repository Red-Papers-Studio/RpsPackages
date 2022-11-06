using System.Collections.Immutable;
using System.Data.Common;
using DomainDrivenDesign.UnitTests.Data;
using FluentAssertions;

namespace DomainDrivenDesign.UnitTests.TestCases;

public class EnumerationTestCase
{
    [Theory]
    [InlineData(0, "test")]
    [InlineData(int.MaxValue, "test")]
    [InlineData(int.MinValue, "test")]
    public void CreateInstance_WithCorrectIdAndCorrectName_DoesNotThrowException(int id, string name)
    {
        Func<TestEnumeration> act = () => new TestEnumeration(id, name);
        act.Should().NotThrow();
    }

    [Fact]
    public void CreateInstance_WithCorrectIdAndNullName_ThrowsArgumentNullException()
    {
        Func<TestEnumeration> act = () => new TestEnumeration(0, null!);
        act.Should().Throw<ArgumentNullException>().WithParameterName("name");
    }

    [Theory]
    [InlineData(0, "test")]
    [InlineData(int.MaxValue, "test")]
    [InlineData(int.MinValue, "test")]
    public void CreateInstance_SetsIdAndNameCorrectly(int expId, string expName)
    {
        var act = new TestEnumeration(expId, expName);
        act.Id.Should().Be(expId);
        act.Name.Should().Be(expName);
    }


    [Fact]
    public void GetAll_ReturnsAllStaticDefinedEnumerationProperties()
    {
        var exp = new List<Enumeration<int>>
        {
            TestEnumeration.Test0,
            TestEnumeration.Test1
        };
        IEnumerable<Enumeration<int>> act = TestEnumeration.GetAll<TestEnumeration>();
        act.Should().Contain(exp);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void FromId_WithCorrectId_ReturnsEnumerationCorrectly(int id)
    {
        Enumeration<int> exp = id switch
        {
            0 => TestEnumeration.Test0,
            1 => TestEnumeration.Test1,
            _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
        };
        Enumeration<int> act = TestEnumeration.FromId<TestEnumeration>(id);
        act.Should().Be(exp);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(int.MaxValue)]
    public void FromId_WithIncorrectId_ThrowsException(int id)
    {
        Action act = () => TestEnumeration.FromId<TestEnumeration>(id);
        act.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [InlineData(nameof(TestEnumeration.Test0))]
    [InlineData(nameof(TestEnumeration.Test1))]
    public void FromId_WithCorrectName_ReturnsEnumerationCorrectly(string name)
    {
        Enumeration<int> exp = name switch
        {
            nameof(TestEnumeration.Test0) => TestEnumeration.Test0,
            nameof(TestEnumeration.Test1) => TestEnumeration.Test1,
            _ => throw new ArgumentOutOfRangeException(nameof(name), name, null)
        };
        Enumeration<int> act = TestEnumeration.FromName<TestEnumeration>(name);
        act.Should().Be(exp);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void FromId_WithIncorrectName_ThrowsException(string name)
    {
        Action act = () => TestEnumeration.FromName<TestEnumeration>(name);
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void SortingEnumerationsWithCompareToMethod_SortsCorrectlyById()
    {
        List<TestEnumeration> exp = TestEnumeration.GetAll<TestEnumeration>().ToList();
        List<TestEnumeration> act = TestEnumeration.GetAll<TestEnumeration>().ToList();

        exp.Sort((a,b)=>a.Id.CompareTo(b.Id));
        act.Sort();
        act.Should().BeInAscendingOrder().And.ContainInOrder(exp);
    }

    [Fact]
    public void CompareTo_WithNull_Returns1()
    {
        const int exp = 1;
        int act = TestEnumeration.Test0.CompareTo(null);
        act.Should().Be(exp);
    }
    
    [Fact]
    public void CompareTo_WithIdenticalEnumeration_Returns0()
    {
        const int exp = 0;
        int act = TestEnumeration.Test0.CompareTo(TestEnumeration.Test0);
        act.Should().Be(exp);
    }
}