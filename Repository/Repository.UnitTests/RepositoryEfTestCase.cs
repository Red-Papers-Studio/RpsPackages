using FluentAssertions;
using Repository.EntityFrameworkCore;

namespace Repository.UnitTests;

public class RepositoryEfTestCase
{
    [Fact]
    public void CreateInstance_NullDbContext_ThrowsArgumentNullException()
    {
        Func<RepositoryEf<string>> act = () => new RepositoryEf<string>(null!);
        act.Should().Throw<ArgumentNullException>().WithParameterName("dbContext");
    }
}