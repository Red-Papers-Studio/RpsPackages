namespace DomainDrivenDesign.UnitTests.Data;

public class TestEnumeration : Enumeration<int>
{
    public static TestEnumeration Test0 = new TestEnumeration(0, nameof(Test0));
    public static TestEnumeration Test1 = new TestEnumeration(1, nameof(Test1));
    
    public TestEnumeration(int id, string name) : base(id, name)
    {
    }
}