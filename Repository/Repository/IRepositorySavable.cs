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
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A Task that represents asynchronous saving changes operation.</returns>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}