using FluentAssertions;
using Repository.EntityFrameworkCore;
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
    public void CreateInstance_NullDbContext_ThrowsArgumentNullException()
    {
        Func<RepositoryEf<string>> act = () => new RepositoryEf<string>(null!);
        act.Should().Throw<ArgumentNullException>().WithParameterName("dbContext");
    }
}