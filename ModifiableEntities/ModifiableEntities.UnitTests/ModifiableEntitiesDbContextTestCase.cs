using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using ModifiableEntities.EntityFrameworkCore;

namespace ModifiableEntities.UnitTests;

public class ModifiableEntitiesDbContextTestCase : IDisposable
{
    private readonly DbContext _dbContext;

    public ModifiableEntitiesDbContextTestCase()
    {
        DbContextOptions<ModifiableEntitiesDbContext<int>> dbOptions =
            new DbContextOptionsBuilder<ModifiableEntitiesDbContext<int>>()
                .UseInMemoryDatabase("TestDB")
                .Options;

        _dbContext = new TestingDbContext(dbOptions);

        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();

        _dbContext.Set<TestEntity>().Add(new TestEntity());
        _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~ModifiableEntitiesDbContextTestCase()
    {
        Dispose(false);
    }

    [Fact]
    public void WhenEntityAdded_SaveChanges_SetsCorrectlyCreationAndLastModificationDateTimeUtc()
    {
        TestEntity act = new();
        _dbContext.Set<TestEntity>().Add(act);
        _dbContext.SaveChanges();

        act.CreationDateUtc.Should().BeCloseTo(DateTime.UtcNow, 1.Seconds());
        act.LastModificationDateUtc.Should().BeSameDateAs(act.CreationDateUtc);
    }

    [Fact]
    public void WhenEntityUpdated_SaveChanges_SetsCorrectlyLastModificationDateTimeUtc()
    {
        TestEntity act = _dbContext.Set<TestEntity>().First();
        _dbContext.Set<TestEntity>().Update(act);
        _dbContext.SaveChanges();

        act.CreationDateUtc.Should().BeBefore(act.LastModificationDateUtc);
        act.LastModificationDateUtc.Should().BeCloseTo(DateTime.UtcNow, 1.Seconds());
    }

    [Fact]
    public async void WhenEntityAdded_SaveChangesAsync_SetsCorrectlyCreationAndLastModificationDateTimeUtc()
    {
        TestEntity act = new();
        _dbContext.Set<TestEntity>().Add(act);
        await _dbContext.SaveChangesAsync();

        act.CreationDateUtc.Should().BeCloseTo(DateTime.UtcNow, 1.Seconds());
        act.LastModificationDateUtc.Should().BeSameDateAs(act.CreationDateUtc);
    }

    [Fact]
    public async void WhenEntityUpdated_SaveChangesAsync_SetsCorrectlyLastModificationDateTimeUtc()
    {
        TestEntity act = _dbContext.Set<TestEntity>().First();
        _dbContext.Set<TestEntity>().Update(act);
        await _dbContext.SaveChangesAsync();

        act.CreationDateUtc.Should().BeBefore(act.LastModificationDateUtc);
        act.LastModificationDateUtc.Should().BeCloseTo(DateTime.UtcNow, 1.Seconds());
    }

    private void Dispose(bool disposing)
    {
        if (disposing) _dbContext.Dispose();
    }

    private class TestEntity : IBaseModifiableEntity<int>
    {
        public int Id { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public DateTime LastModificationDateUtc { get; set; }
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