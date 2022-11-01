using Microsoft.EntityFrameworkCore;
using Repository.EntityFrameworkCore;
using Repository.UnitTests.Data;

namespace Repository.UnitTests.Fixtures;

public class RepositoryEfFixture : IDisposable
{
    private readonly DbContext _dbContext;

    public RepositoryEfFixture()
    {
        DbContextOptions<TestingDbContext> dbOptions =
            new DbContextOptionsBuilder<TestingDbContext>()
                .UseInMemoryDatabase("TestDB")
                .Options;

        _dbContext = new TestingDbContext(dbOptions);

        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();

        _dbContext.SaveChanges();

        Repository = new RepositoryEf<TestEntity>(_dbContext);
    }

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
        if (disposing) _dbContext.Dispose();
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