namespace Repository;

/// <summary>
///     Repository interface.
/// </summary>
/// <typeparam name="T">Entity type stored in this repository.</typeparam>
public interface IRepository<T>
{
    /// <summary>
    ///     Creates an entity in repository.
    /// </summary>
    /// <param name="entity">Creating entity.</param>
    void Create(T entity);

    /// <summary>
    ///     Returns an IQueryable of entities from repository.
    /// </summary>
    /// <returns>An IQueryable of entities from repository.</returns>
    IQueryable<T> Read();

    /// <summary>
    ///     Updates an entity in repository.
    /// </summary>
    /// <param name="entity">Updating entity.</param>
    void Update(T entity);

    /// <summary>
    ///     Deletes an entity from repository.
    /// </summary>
    /// <param name="entity"></param>
    void Delete(T entity);


    /// <summary>
    ///     Asynchronously creates an entity in repository.
    /// </summary>
    /// <param name="entity">Creating entity.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A Task that represents asynchronous creating operation.</returns>
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);


    /// <summary>
    ///     Asynchronously updates an entity in repository.
    /// </summary>
    /// <param name="entity">Updating entity.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A Task that represents asynchronous updating operation.</returns>
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Asynchronously deletes an entity in repository.
    /// </summary>
    /// <param name="entity">Deleting entity.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A Task that represents asynchronous deleting operation.</returns>
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}