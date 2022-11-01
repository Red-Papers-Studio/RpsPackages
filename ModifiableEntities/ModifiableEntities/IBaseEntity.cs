namespace ModifiableEntities;

/// <summary>
///     Base interface for all entities.
/// </summary>
/// <typeparam name="TId">Id type of entity.</typeparam>
public interface IBaseEntity<TId>
{
    /// <summary>
    ///     Unique Id of entity.
    /// </summary>
    TId Id { get; set; }
}