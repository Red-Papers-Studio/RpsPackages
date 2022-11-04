namespace ModifiableEntities;

/// <summary>
///     Base interface for all modifiable entities.
/// </summary>
/// <typeparam name="TId">Id type of entity.</typeparam>
public interface IBaseModifiableEntity<TId> : IBaseEntity<TId>
{
    /// <summary>
    ///     Creation date of entity in UTC format.
    /// </summary>
    public DateTime CreationDateUtc { get; init; }

    /// <summary>
    ///     Last modification date of entity in UTC format.
    /// </summary>
    public DateTime LastModificationDateUtc { get; init; }
}