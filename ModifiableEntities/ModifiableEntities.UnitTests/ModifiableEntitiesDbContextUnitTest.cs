using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ModifiableEntities.EntityFrameworkCore;

namespace ModifiableEntities.UnitTests;

public class ModifiableEntitiesDbContextUnitTest : IDisposable
{
    private readonly DbContext _dbContext;

    public ModifiableEntitiesDbContextUnitTest()
    {
        DbContextOptions<ModifiableEntitiesDbContext> dbOptions =
            new DbContextOptionsBuilder<ModifiableEntitiesDbContext>()
                .UseInMemoryDatabase("TestDB")
                .Options;

        _dbContext = new TestingDbContext(dbOptions);

        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
    }

    ~ModifiableEntitiesDbContextUnitTest()
    {
        Dispose(false);
    }

    [Fact]
    public void SaveChanges_UpdatesCorrectlyCreationAndLastModificationDateTimeUtc()
    {
        TestEntity act = new ();
        _dbContext.Set<TestEntity>().Add(act);
        _dbContext.SaveChanges();
        
        act.CreationDateUtc.Should().Be(DateTime.UtcNow);
        act.LastModificationDateUtc.Should().Be(DateTime.UtcNow);
    }

    [Fact]
    public async void SaveChangesAsync_UpdatesCorrectlyCreationAndLastModificationDateTimeUtc()
    {
        TestEntity act = new();
        _dbContext.Set<TestEntity>().Add(act);
        await _dbContext.SaveChangesAsync();
        
        act.CreationDateUtc.Should().Be(DateTime.UtcNow);
        act.LastModificationDateUtc.Should().Be(DateTime.UtcNow);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dbContext.Dispose();
        }
    }

    private class TestEntity : IBaseModifiableEntity<int>
    {
        public int Id { get; init; }
        public DateTime CreationDateUtc { get; init; }
        public DateTime LastModificationDateUtc { get; init; }
    }

    private class TestingDbContext : ModifiableEntitiesDbContext
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