namespace ModifiableEntities;

/// <summary>
///     Base interface for all modifiable entities.
/// </summary>
/// <typeparam name="TId">Id type of entity.</typeparam>
public abstract class BaseModifiableEntity<TId> : BaseEntity<TId>
{
    /// <summary>
    ///     Creation date of entity in UTC format.
    /// </summary>
    public DateTime CreationDateUtc { get; protected set; }

    /// <summary>
    ///     Last modification date of entity in UTC format.
    /// </summary>
    public DateTime LastModificationDateUtc { get; protected set; }

    /// <summary>
    ///     Initializes a new instance of the class.
    /// </summary>
    protected BaseModifiableEntity() : this(default!, default, default)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the class.
    /// </summary>
    /// <param name="id">Id of entity.</param>
    /// <param name="creationDateUtc">Entity creation date in UTC format.</param>
    /// <param name="lastModificationDateUtc">Entity last modification date in UTC format.</param>
    protected BaseModifiableEntity(TId id, DateTime creationDateUtc, DateTime lastModificationDateUtc) : base(id)
    {
        CreationDateUtc = creationDateUtc;
        LastModificationDateUtc = lastModificationDateUtc;
    }
}