using DomainDrivenDesign.UnitTests.Data;
using FluentAssertions;

namespace DomainDrivenDesign.UnitTests.TestCases;

public class EntityTestCase
{
    [Theory]
    [InlineData(0)]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue)]
    public void CreateInstance_WithCorrectIdIn_IdSetCorrectly(int expected)
    {
        var entity = new TestEntity<int>(expected);
        int actual = entity.Id;
        actual.Should().Be(expected);
    }

    [Fact]
    public void AddEvent_AddsEventCorrectly()
    {
        var entity = new TestEntity<int>(0);
        Event expected = new TestEvent();
        entity.AddEvent(expected);
        entity.Events.Should().Contain(expected);
    }

    [Fact]
    public void RemoveEvent_RemovesEventCorrectly()
    {
        var entity = new TestEntity<int>(0);
        Event expected = new TestEvent();
        entity.AddEvent(new TestEvent());
        entity.AddEvent(expected);
        entity.RemoveEvent(expected);

        entity.Events.Should().NotContain(expected);
    }
}