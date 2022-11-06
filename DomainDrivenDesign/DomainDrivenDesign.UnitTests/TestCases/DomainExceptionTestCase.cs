using FluentAssertions;

namespace DomainDrivenDesign.UnitTests.TestCases;

public class DomainExceptionTestCase
{
    [Fact]
    public void CreateInstance_WithoutParameters_DoesNotThrowsException()
    {
        Func<DomainException> act = () => new DomainException();
        act.Should().NotThrow();
    }

    [Fact]
    public void CreateInstance_WithNullMessage_DoesNotThrowsException()
    {
        Func<DomainException> act = () => new DomainException(null);
        act.Should().NotThrow();
    }

    [Fact]
    public void CreateInstance_WithNullMessageAndNullInnerException_DoesNotThrowsException()
    {
        Func<DomainException> act = () => new DomainException(null, null);
        act.Should().NotThrow();
    }
}