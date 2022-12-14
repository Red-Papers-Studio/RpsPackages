using Microsoft.EntityFrameworkCore;

namespace Repository.EntityFrameworkCore;

/// <summary>
///     EntityFrameworkCore repository.
/// </summary>
/// <typeparam name="T">Entity type stored in this repository.</typeparam>
public class RepositoryEf<T> : IRepositorySavable<T>
    where T : class
{
    private readonly DbContext _dbContext;
    private readonly bool _useAsNoTracking;

    /// <summary>
    ///     Initializes a new instance of the class.
    /// </summary>
    /// <param name="dbContext">DbContext, which is used to store entities.</param>
    /// <param name="useAsNoTracking">Set to true if you want to use AsNoTracking.</param>
    /// <exception cref="ArgumentNullException">DbContext is null.</exception>
    public RepositoryEf(DbContext dbContext, bool useAsNoTracking = true)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _useAsNoTracking = useAsNoTracking;
    }

    /// <inheritdoc />
    public void Create(T entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        _dbContext.Set<T>().Add(entity);
    }

    /// <inheritdoc />
    public IQueryable<T> Read()
    {
        IQueryable<T> res = _dbContext.Set<T>();
        if (_useAsNoTracking) res.AsNoTracking();
        return res;
    }

    /// <inheritdoc />
    public void Update(T entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        _dbContext.Set<T>().Update(entity);
    }

    /// <inheritdoc />
    public void Delete(T entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        _dbContext.Set<T>().Remove(entity);
    }

    /// <inheritdoc />
    public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        await Task.Run(() => _dbContext.Set<T>().Update(entity), cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        await Task.Run(() => _dbContext.Set<T>().Remove(entity), cancellationToken);
    }

    /// <inheritdoc />
    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    /// <inheritdoc />
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}