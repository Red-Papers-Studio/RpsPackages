using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Repository.EntityFrameworkCore;
using Repository.UnitTests.Data;
using Repository.UnitTests.Fixtures;

namespace Repository.UnitTests.TestCases;

public class RepositoryEfTestCase : IClassFixture<RepositoryEfFixture>
{
    private readonly RepositoryEfFixture _repFixture;

    public RepositoryEfTestCase(RepositoryEfFixture repFixture)
    {
        _repFixture = repFixture;
    }

    [Fact]
    public void CreateEntity_CorrectlyCreatesEntityInsideDbContext()
    {
        var exp = new TestEntity
        {
            Data = "Testing data creation"
        };

        _repFixture.Repository.Create(exp);
        _repFixture.Repository.SaveChanges();

        DbSet<TestEntity> act = _repFixture.DbContext.Set<TestEntity>();
        act.Should().Contain(exp);
    }

    [Fact]
    public async void CreateAsyncEntity_CorrectlyCreatesEntityInsideDbContext()
    {
        var exp = new TestEntity
        {
            Data = "Testing data creation"
        };

        await _repFixture.Repository.CreateAsync(exp);
        await _repFixture.Repository.SaveChangesAsync();

        DbSet<TestEntity> act = _repFixture.DbContext.Set<TestEntity>();
        act.Should().Contain(exp);
    }


    [Fact]
    public void ReadEntities_CorrectlyReadsEntitiesFromDbContext()
    {
        var exp = new List<TestEntity>
        {
            new()
            {
                Data = "Testing data reading 0"
            },
            new()
            {
                Data = "Testing data reading 1"
            },
            new()
            {
                Data = "Testing data reading 2"
            },
            new()
            {
                Data = "Testing data reading 3"
            }
        };

        _repFixture.DbContext.Set<TestEntity>().AddRange(exp);
        _repFixture.DbContext.SaveChanges();

        List<TestEntity> act = _repFixture.Repository.Read().ToList();
        act.Should().Contain(exp);
    }

    [Fact]
    public void UpdateEntity_CorrectlyUpdatesEntityInsideDbContext()
    {
        var exp = new TestEntity
        {
            Data = "Testing data updating"
        };


        _repFixture.DbContext.Set<TestEntity>().Add(exp);
        _repFixture.DbContext.SaveChanges();

        exp.Data = "Testing data updating";

        _repFixture.Repository.Update(exp);
        _repFixture.Repository.SaveChanges();

        DbSet<TestEntity> act = _repFixture.DbContext.Set<TestEntity>();
        act.Should().Contain(exp);
    }

    [Fact]
    public async void UpdateAsyncEntity_CorrectlyUpdatesEntityInsideDbContext()
    {
        var exp = new TestEntity
        {
            Data = "Testing data updating"
        };


        await _repFixture.DbContext.Set<TestEntity>().AddAsync(exp);
        await _repFixture.DbContext.SaveChangesAsync();

        exp.Data = "Testing data updating";

        await _repFixture.Repository.UpdateAsync(exp);
        await _repFixture.Repository.SaveChangesAsync();

        DbSet<TestEntity> act = _repFixture.DbContext.Set<TestEntity>();
        act.Should().Contain(exp);
    }

    [Fact]
    public void DeleteEntity_CorrectlyDeleteEntityFromDbContext()
    {
        var deletingEntity = new TestEntity
        {
            Data = "Testing data deleting"
        };


        _repFixture.DbContext.Set<TestEntity>().Add(deletingEntity);
        _repFixture.DbContext.SaveChanges();

        _repFixture.Repository.Delete(deletingEntity);
        _repFixture.Repository.SaveChanges();

        DbSet<TestEntity> act = _repFixture.DbContext.Set<TestEntity>();
        act.Should().NotContain(deletingEntity);
    }

    [Fact]
    public async void DeleteAsyncEntity_CorrectlyDeleteEntityFromDbContext()
    {
        var deletingEntity = new TestEntity
        {
            Data = "Testing data deleting"
        };


        _repFixture.DbContext.Set<TestEntity>().Add(deletingEntity);
        await _repFixture.DbContext.SaveChangesAsync();

        await _repFixture.Repository.DeleteAsync(deletingEntity);
        await _repFixture.Repository.SaveChangesAsync();

        DbSet<TestEntity> act = _repFixture.DbContext.Set<TestEntity>();
        act.Should().NotContain(deletingEntity);
    }

    #region ArgumentNullException

    [Fact]
    public void CreateInstance_NullDbContext_ThrowsArgumentNullException()
    {
        Func<RepositoryEf<string>> act = () => new RepositoryEf<string>(null!);
        act.Should().Throw<ArgumentNullException>().WithParameterName("dbContext");
    }

    [Fact]
    public void CreateEntity_Null_ThrowsArgumentNullException()
    {
        Action act = () => _repFixture.Repository.Create(null!);
        act.Should().Throw<ArgumentNullException>().WithParameterName("entity");
    }

    [Fact]
    public async void CreateAsyncEntity_Null_ThrowsArgumentNullException()
    {
        Func<Task> act = () => _repFixture.Repository.CreateAsync(null!);
        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("entity");
    }

    [Fact]
    public void UpdateEntity_Null_ThrowsArgumentNullException()
    {
        Action act = () => _repFixture.Repository.Update(null!);
        act.Should().Throw<ArgumentNullException>().WithParameterName("entity");
    }

    [Fact]
    public async void UpdateAsyncEntity_Null_ThrowsArgumentNullException()
    {
        Func<Task> act = () => _repFixture.Repository.UpdateAsync(null!);
        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("entity");
    }

    [Fact]
    public void DeleteEntity_Null_ThrowsArgumentNullException()
    {
        Action act = () => _repFixture.Repository.Delete(null!);
        act.Should().Throw<ArgumentNullException>().WithParameterName("entity");
    }

    [Fact]
    public async void DeleteAsyncEntity_Null_ThrowsArgumentNullException()
    {
        Func<Task> act = () => _repFixture.Repository.DeleteAsync(null!);
        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("entity");
    }

    #endregion
}