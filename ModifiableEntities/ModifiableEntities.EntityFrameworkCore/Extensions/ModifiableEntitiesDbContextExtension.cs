using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ModifiableEntities.EntityFrameworkCore.Extensions;

/// <summary>
///     Extension class needed for modifiable entity database contexts.
/// </summary>
public static class ModifiableEntitiesDbContextExtension
{
    /// <summary>
    ///     Modifies entities last modification and creation dats according on there changes which were made previously.
    /// </summary>
    /// <param name="dbContext">DbContext on which will be applied this update method.</param>
    /// <typeparam name="TId">Id type of entity.</typeparam>
    public static void ModifyEntitiesOnSaveChanges<TId>(this DbContext dbContext)
    {
        IEnumerable<EntityEntry> entries = dbContext.ChangeTracker
            .Entries()
            .Where(e =>
                e.Entity is IBaseModifiableEntity<TId> &&
                e.State is EntityState.Added or EntityState.Modified);

        foreach (EntityEntry entityEntry in entries)
        {
            DateTime utcNow = DateTime.UtcNow;
            var entity = (IBaseModifiableEntity<TId>)entityEntry.Entity;

            GetPropertyInfo<IBaseModifiableEntity<TId>>(nameof(IBaseModifiableEntity<TId>.LastModificationDateUtc))
                .SetValue(entity, utcNow, null);

            if (entityEntry.State == EntityState.Added)
                GetPropertyInfo<IBaseModifiableEntity<TId>>(nameof(IBaseModifiableEntity<TId>.CreationDateUtc))
                    .SetValue(entity, utcNow, null);
        }
    }

    private static PropertyInfo GetPropertyInfo<T>(string propertyName)
    {
        return GetPropertyInfo(typeof(T), propertyName);
    }

    private static PropertyInfo GetPropertyInfo(Type type, string propertyName)
    {
        return type.GetProperty(propertyName) ??
               throw new ArgumentException("Property does not exist", nameof(propertyName));
    }
}