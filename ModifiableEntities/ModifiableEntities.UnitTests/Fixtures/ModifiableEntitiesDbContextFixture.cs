using Microsoft.EntityFrameworkCore;
using ModifiableEntities.EntityFrameworkCore;
using ModifiableEntities.UnitTests.Data;

namespace ModifiableEntities.UnitTests.Fixtures;

public class ModifiableEntitiesDbContextFixture : IDisposable
{
    public DbContext DbContext { get; }
    
    public ModifiableEntitiesDbContextFixture()
    {
        DbContextOptions<ModifiableEntitiesDbContext<int>> dbOptions =
            new DbContextOptionsBuilder<ModifiableEntitiesDbContext<int>>()
                .UseInMemoryDatabase("TestDB")
                .Options;

        DbContext = new TestingDbContext(dbOptions);

        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();

        DbContext.Set<TestEntity>().Add(new TestEntity());
        DbContext.SaveChanges();
    }
    
    ~ModifiableEntitiesDbContextFixture()
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
    
    private class TestingDbContext : ModifiableEntitiesDbContext<int>
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