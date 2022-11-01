using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModifiableEntities.Identity;
using ModifiableEntities.UnitTests.Data;

namespace ModifiableEntities.UnitTests.Fixtures;

public class IdentityModifiableEntitiesDbContextFixture : IDisposable
{
    public DbContext DbContext { get; }
    
    public IdentityModifiableEntitiesDbContextFixture()
    {
        DbContextOptions<IdentityModifiableEntitiesDbContext<IdentityUser<int>, IdentityRole<int>, int>> dbOptions =
            new DbContextOptionsBuilder<
                    IdentityModifiableEntitiesDbContext<IdentityUser<int>, IdentityRole<int>, int>>()
                .UseInMemoryDatabase("TestDB")
                .Options;

        DbContext = new TestingDbContext(dbOptions);

        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();

        DbContext.Set<TestEntity>().Add(new TestEntity());
        DbContext.SaveChanges();
    }
    
    ~IdentityModifiableEntitiesDbContextFixture()
    {
        Dispose(false);
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    
    private void Dispose(bool disposing)
    {
        if (disposing) DbContext.Dispose();
    }
    
    private class TestingDbContext : IdentityModifiableEntitiesDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public TestingDbContext(bool useLazyLoading = false) : base(useLazyLoading)
        {
        }

        public TestingDbContext(DbContextOptions options, bool useLazyLoading = false) : base(options, useLazyLoading)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Model.AddEntityType(typeof(TestEntity));
            base.OnModelCreating(modelBuilder);
        }
    }
}