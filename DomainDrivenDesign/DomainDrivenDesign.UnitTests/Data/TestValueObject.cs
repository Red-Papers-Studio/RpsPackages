namespace DomainDrivenDesign.UnitTests.Data;

public class TestValueObject : ValueObject
{
    public int Age { get; init; }
    public string? Name { get; init; }
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Age;
        yield return Name;
    }

    public static bool operator ==(TestValueObject one, TestValueObject two)
    {
        return EqualOperator(one, two);
    }
    public static bool operator !=(TestValueObject one, TestValueObject two)
    {
        return NotEqualOperator(one, two);
    }
}