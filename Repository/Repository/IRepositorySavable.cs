namespace Repository;

/// <summary>
///     Savable repository interface.
/// </summary>
/// <typeparam name="T">Entity type stored in this repository.</typeparam>
public interface IRepositorySavable<T> : IRepository<T>
{
    /// <summary>
    ///     Saves repository changes.
    /// </summary>
    void SaveChanges();

    /// <summary>
    ///     Asynchronously saves repository changes.
    /// </summary>
    /// <returns>A Task that represents asynchronous saving changes operation.</returns>
    Task SaveChangesAsync();
}