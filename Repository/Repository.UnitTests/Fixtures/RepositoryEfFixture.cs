using Microsoft.EntityFrameworkCore;
using Repository.EntityFrameworkCore;
using Repository.UnitTests.Data;

namespace Repository.UnitTests.Fixtures;

public class RepositoryEfFixture : IDisposable
{
    public RepositoryEfFixture()
    {
        DbContextOptions<TestingDbContext> dbOptions =
            new DbContextOptionsBuilder<TestingDbContext>()
                .UseInMemoryDatabase("TestDB")
                .Options;

        DbContext = new TestingDbContext(dbOptions);

        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();

        DbContext.SaveChanges();

        Repository = new RepositoryEf<TestEntity>(DbContext);
    }

    public DbContext DbContext { get; }
    public IRepositorySavable<TestEntity> Repository { get; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~RepositoryEfFixture()
    {
        Dispose(false);
    }

    private void Dispose(bool disposing)
    {
        if (disposing) DbContext.Dispose();
    }

    private class TestingDbContext : DbContext
    {
        private readonly bool _useLazyLoading;

        public TestingDbContext(bool useLazyLoading = false)
        {
            _useLazyLoading = useLazyLoading;
        }

        public TestingDbContext(DbContextOptions options, bool useLazyLoading = false) : base(options)
        {
            _useLazyLoading = useLazyLoading;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Model.AddEntityType(typeof(TestEntity));
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_useLazyLoading) optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}