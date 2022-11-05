namespace DomainDrivenDesign;

/// <summary>
///     Unit of work interface.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    ///     Saves changes.
    /// </summary>
    void SaveChanges();

    /// <summary>
    ///     Asynchronously saves changes.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A Task that represents asynchronous saving changes operation.</returns>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Asynchronously saves entities.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A Task that represents asynchronous saving changes operation.</returns>
    Task SaveEntitiesAsync(CancellationToken cancellationToken = default);
}