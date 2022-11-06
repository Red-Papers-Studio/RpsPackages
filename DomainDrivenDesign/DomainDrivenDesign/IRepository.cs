namespace DomainDrivenDesign;

/// <inheritdoc />
public interface IRepository<T> : Repository.IRepository<T> where T : class, IAggregateRoot
{
}