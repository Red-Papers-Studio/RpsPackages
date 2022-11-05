namespace DomainDrivenDesign;

/// <summary>
///     Aggregate root interface.
/// </summary>
/// <remarks>
///     Used for <see cref="IRepository{T}" /> so you could not implement repository for non aggregating entities.
/// </remarks>
public interface IAggregateRoot
{
}