using FluentAssertions;
using Repository.EntityFrameworkCore;

namespace Repository.UnitTests;

public class RepositoryEfUnitTest
{
    [Fact]
    public void CreateInstance_NullDbContext_ThrowsArgumentNullException()
    {
        Func<RepositoryEf<string>> act = () => new RepositoryEf<string>(null!);
        act.Should().Throw<ArgumentNullException>().WithParameterName("dbContext");
    }
}
//TEST Signin commits