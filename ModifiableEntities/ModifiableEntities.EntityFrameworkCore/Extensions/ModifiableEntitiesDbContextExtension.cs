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
    public static void ModifyEntitiesOnSaveChanges(this DbContext dbContext)
    {
        throw new NotImplementedException();
    }
}