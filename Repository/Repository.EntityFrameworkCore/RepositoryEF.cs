namespace Repository.EntityFrameworkCore;

/// <summary>
///     EntityFrameworkCore repository.
/// </summary>
/// <typeparam name="T">Entity type stored in this repository.</typeparam>
public class RepositoryEf<T> : IRepositorySavable<T>
    where T : class
{
    /// <inheritdoc />
    public void Create(T entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IQueryable<T> Read()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Update(T entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Delete(T entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task CreateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void SaveChanges()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}