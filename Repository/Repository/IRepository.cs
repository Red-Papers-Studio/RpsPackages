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
    /// <returns>A Task that represents asynchronous creating operation.</returns>
    Task CreateAsync(T entity);


    /// <summary>
    ///     Asynchronously updates an entity in repository.
    /// </summary>
    /// <param name="entity">Updating entity.</param>
    /// <returns>A Task that represents asynchronous updating operation.</returns>
    Task UpdateAsync(T entity);

    /// <summary>
    ///     Asynchronously deletes an entity in repository.
    /// </summary>
    /// <param name="entity">Deleting entity.</param>
    /// <returns>A Task that represents asynchronous deleting operation.</returns>
    Task DeleteAsync(T entity);
}