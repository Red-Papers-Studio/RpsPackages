namespace DomainDrivenDesign.UnitTests.Data;

public class TestEnumeration : Enumeration<int>
{
    public static readonly TestEnumeration Test0 = new(0, nameof(Test0));
    public static readonly TestEnumeration Test1 = new(1, nameof(Test1));

    public TestEnumeration(int id, string name) : base(id, name)
    {
    }
}