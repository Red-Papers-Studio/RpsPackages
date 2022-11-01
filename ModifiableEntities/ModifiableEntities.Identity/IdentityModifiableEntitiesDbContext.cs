using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModifiableEntities.EntityFrameworkCore.Extensions;

namespace ModifiableEntities.Identity;

/// <summary>
///     <see cref="IdentityDbContext"/> for modifiable entities.
/// </summary>
/// <typeparam name="TUser">The type of user objects.</typeparam>
/// <typeparam name="TRole">The type of role objects.</typeparam>
/// <typeparam name="TKey">The type of the primary key for users and roles.</typeparam>
public class IdentityModifiableEntitiesDbContext<TUser, TRole, TKey> : IdentityModifiableEntitiesDbContext<TUser, TRole,
    TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>,
    IdentityUserToken<TKey>>
    where TUser : IdentityUser<TKey>
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    ///     Initializes a new instance of the class.
    /// </summary>
    /// <param name="useLazyLoading">Set to true if you want to use lazy loading propagation.</param>
    public IdentityModifiableEntitiesDbContext(bool useLazyLoading = false) : base(useLazyLoading)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
    /// <param name="useLazyLoading">Set to true if you want to use lazy loading propagation.</param>
    public IdentityModifiableEntitiesDbContext(DbContextOptions options, bool useLazyLoading = false) : base(options,
        useLazyLoading)
    {
    }
}

/// <summary>
///     <see cref="IdentityDbContext"/> for modifiable entities.
/// </summary>
/// <inheritdoc />
public class IdentityModifiableEntitiesDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim,
    TUserToken> : IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
    where TUser : IdentityUser<TKey>
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
    where TUserClaim : IdentityUserClaim<TKey>
    where TUserRole : IdentityUserRole<TKey>
    where TUserLogin : IdentityUserLogin<TKey>
    where TRoleClaim : IdentityRoleClaim<TKey>
    where TUserToken : IdentityUserToken<TKey>
{
    /// <summary>
    ///     Use lazy loading propagation.
    /// </summary>
    private readonly bool _useLazyLoading;

    /// <summary>
    ///     Initializes a new instance of the class.
    /// </summary>
    /// <param name="useLazyLoading">Set to true if you want to use lazy loading propagation.</param>
    public IdentityModifiableEntitiesDbContext(bool useLazyLoading = false)
    {
        _useLazyLoading = useLazyLoading;
    }

    /// <summary>
    ///     Initializes a new instance of the class.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
    /// <param name="useLazyLoading">Set to true if you want to use lazy loading propagation.</param>
    public IdentityModifiableEntitiesDbContext(DbContextOptions options, bool useLazyLoading = false) : base(options)
    {
        _useLazyLoading = useLazyLoading;
    }


    /// <inheritdoc />
    public override int SaveChanges()
    {
        this.ModifyEntitiesOnSaveChanges<TKey>();
        return base.SaveChanges();
    }

    /// <inheritdoc />
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        this.ModifyEntitiesOnSaveChanges<TKey>();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    /// <inheritdoc />
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        this.ModifyEntitiesOnSaveChanges<TKey>();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_useLazyLoading) optionsBuilder.UseLazyLoadingProxies();
        base.OnConfiguring(optionsBuilder);
    }
}