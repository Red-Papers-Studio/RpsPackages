namespace ModifiableEntities;

/// <summary>
///     Base interface for all entities.
/// </summary>
/// <typeparam name="TId">Id type of entity.</typeparam>
public abstract class BaseEntity<TId>
{
    /// <summary>
    ///     Unique Id of entity.
    /// </summary>
    public TId Id { get; protected set; }

    /// <summary>
    ///     Initializes a new instance of the class.
    /// </summary>
    protected BaseEntity() : this(default!)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the class.
    /// </summary>
    /// <param name="id">Id of entity.</param>
    /// <exception cref="ArgumentNullException">Id is null.</exception>
    protected BaseEntity(TId id)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }
}