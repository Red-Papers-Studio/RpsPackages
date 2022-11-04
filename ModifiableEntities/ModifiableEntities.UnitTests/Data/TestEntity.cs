namespace ModifiableEntities.UnitTests.Data;

public class TestEntity : IBaseModifiableEntity<int>
{
    public int Id { get; set; }
    public DateTime CreationDateUtc { get; set; }
    public DateTime LastModificationDateUtc { get; set; }
}