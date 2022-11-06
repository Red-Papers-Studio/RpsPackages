namespace ModifiableEntities.UnitTests.Data;

public class TestEntity : IBaseModifiableEntity<int>
{
    public int Id { get; init; }
    public DateTime CreationDateUtc { get; init; }
    public DateTime LastModificationDateUtc { get; init; }
}