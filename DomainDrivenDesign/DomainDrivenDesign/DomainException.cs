namespace DomainDrivenDesign;

/// <summary>
///     Domain exception class.
/// </summary>
public class DomainException : Exception
{
    /// <inheritdoc />
    public DomainException()
    {
    }


    /// <inheritdoc />
    public DomainException(string? message) : base(message)
    {
    }

    /// <inheritdoc />
    public DomainException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}