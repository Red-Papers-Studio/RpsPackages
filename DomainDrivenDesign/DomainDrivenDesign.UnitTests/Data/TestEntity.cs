namespace DomainDrivenDesign.UnitTests.Data;

public class TestEntity<TId> : Entity<TId>
{
    public TestEntity(TId id) : base(id)
    {
    }

    public new void AddEvent(Event @event)
    {
        base.AddEvent(@event);
    }

    public new void RemoveEvent(Event @event)
    {
        base.RemoveEvent(@event);
    }
}