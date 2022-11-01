using Microsoft.EntityFrameworkCore;
using ModifiableEntities.EntityFrameworkCore.Extensions;

namespace ModifiableEntities.EntityFrameworkCore;

/// <summary>
///     <see cref="DbContext" /> for modifiable entities.
/// </summary>
/// <typeparam name="TId">Id type of entity.</typeparam>
public class ModifiableEntitiesDbContext<TId> : DbContext
{
    private readonly bool _useLazyLoading;

    /// <summary>
    ///     Initializes a new instance of the class.
    /// </summary>
    /// <param name="useLazyLoading">Set to true if you want to use lazy loading propagation.</param>
    public ModifiableEntitiesDbContext(bool useLazyLoading = false)
    {
        _useLazyLoading = useLazyLoading;
    }

    /// <summary>
    ///     Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
    /// <param name="useLazyLoading">Set to true if you want to use lazy loading propagation.</param>
    public ModifiableEntitiesDbContext(DbContextOptions options, bool useLazyLoading = false) : base(options)
    {
        _useLazyLoading = useLazyLoading;
    }

    /// <inheritdoc />
    public override int SaveChanges()
    {
        this.ModifyEntitiesOnSaveChanges<TId>();
        return base.SaveChanges();
    }

    /// <inheritdoc />
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        this.ModifyEntitiesOnSaveChanges<TId>();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    /// <inheritdoc />
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        this.ModifyEntitiesOnSaveChanges<TId>();
        return base.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        this.ModifyEntitiesOnSaveChanges<TId>();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_useLazyLoading) optionsBuilder.UseLazyLoadingProxies();
        base.OnConfiguring(optionsBuilder);
    }
}