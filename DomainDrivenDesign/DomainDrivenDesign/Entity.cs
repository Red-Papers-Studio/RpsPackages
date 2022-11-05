using ModifiableEntities;

namespace DomainDrivenDesign;

/// <summary>
///     Domain entity class.
/// </summary>
/// <typeparam name="TId">Id type of entity.</typeparam>
public abstract class Entity<TId> : IBaseEntity<TId>
{
    private readonly List<Event> _events;

    /// <summary>
    ///     Initializes a new instance of the class.
    /// </summary>
    /// <param name="id">Id of entity.</param>
    protected Entity(TId id)
    {
        Id = id;
        _events = new List<Event>();
    }

    /// <summary>
    ///     Entity events list.
    /// </summary>
    public IReadOnlyList<Event> Events => _events.AsReadOnly();

    /// <inheritdoc />
    public TId Id { get; init; }

    /// <summary>
    ///     Adds event to entity events list.
    /// </summary>
    /// <param name="event">Adding event.</param>
    protected void AddEvent(Event @event)
    {
        _events.Add(@event);
    }

    /// <summary>
    ///     Removes event from entity events list.
    /// </summary>
    /// <param name="event">Removing event.</param>
    protected void RemoveEvent(Event @event)
    {
        _events.Remove(@event);
    }
}