using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.AspNetCore.Identity;
using ModifiableEntities.Identity;
using ModifiableEntities.UnitTests.Data;
using ModifiableEntities.UnitTests.Fixtures;

namespace ModifiableEntities.UnitTests.TestCases;

public class IdentityModifiableEntitiesDbContextTestCase : IClassFixture<IdentityModifiableEntitiesDbContextFixture>
{
    private readonly IdentityModifiableEntitiesDbContextFixture _dbFixture;

    public IdentityModifiableEntitiesDbContextTestCase(IdentityModifiableEntitiesDbContextFixture dbFixture)
    {
        _dbFixture = dbFixture;
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void CreateInstance_DoesNotThrowException(bool useLazyLoading)
    {
        Func<IdentityModifiableEntitiesDbContext<IdentityUser<int>, IdentityRole<int>, int>> act = () =>
            new IdentityModifiableEntitiesDbContext<IdentityUser<int>, IdentityRole<int>, int>(useLazyLoading);
        act.Should().NotThrow();
    }


    [Fact]
    public void WhenEntityAdded_SaveChanges_SetsCorrectlyCreationAndLastModificationDateTimeUtc()
    {
        TestEntity act = new();
        _dbFixture.DbContext.Set<TestEntity>().Add(act);
        _dbFixture.DbContext.SaveChanges();

        act.CreationDateUtc.Should().BeCloseTo(DateTime.UtcNow, 1.Seconds());
        act.LastModificationDateUtc.Should().BeSameDateAs(act.CreationDateUtc);
    }

    [Fact]
    public void WhenEntityUpdated_SaveChanges_SetsCorrectlyLastModificationDateTimeUtc()
    {
        TestEntity act = _dbFixture.DbContext.Set<TestEntity>().First();
        _dbFixture.DbContext.Set<TestEntity>().Update(act);
        _dbFixture.DbContext.SaveChanges();

        act.CreationDateUtc.Should().BeBefore(act.LastModificationDateUtc);
        act.LastModificationDateUtc.Should().BeCloseTo(DateTime.UtcNow, 1.Seconds());
    }

    [Fact]
    public async void WhenEntityAdded_SaveChangesAsync_SetsCorrectlyCreationAndLastModificationDateTimeUtc()
    {
        TestEntity act = new();
        _dbFixture.DbContext.Set<TestEntity>().Add(act);
        await _dbFixture.DbContext.SaveChangesAsync();

        act.CreationDateUtc.Should().BeCloseTo(DateTime.UtcNow, 1.Seconds());
        act.LastModificationDateUtc.Should().BeSameDateAs(act.CreationDateUtc);
    }

    [Fact]
    public async void WhenEntityUpdated_SaveChangesAsync_SetsCorrectlyLastModificationDateTimeUtc()
    {
        TestEntity act = _dbFixture.DbContext.Set<TestEntity>().First();
        _dbFixture.DbContext.Set<TestEntity>().Update(act);
        await _dbFixture.DbContext.SaveChangesAsync();

        act.CreationDateUtc.Should().BeBefore(act.LastModificationDateUtc);
        act.LastModificationDateUtc.Should().BeCloseTo(DateTime.UtcNow, 1.Seconds());
    }
}