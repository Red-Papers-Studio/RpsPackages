namespace DomainDrivenDesign;

/// <summary>
///     Domain Value object class.
/// </summary>
public abstract class ValueObject : ICloneable
{
    /// <inheritdoc />
    public object Clone()
    {
        return MemberwiseClone();
    }

    /// <summary>Determines whether the specified object instances are considered equal.</summary>
    /// <param name="objA">The first object to compare.</param>
    /// <param name="objB">The first object to compare.</param>
    /// <returns>
    ///     true if the objects are considered equal; otherwise, false. If both <paramref name="objA" /> and
    ///     <paramref name="objB" /> are null, the method returns true.
    /// </returns>
    protected static bool EqualOperator(ValueObject objA, ValueObject objB)
    {
        if (ReferenceEquals(objA, null) ^ ReferenceEquals(objB, null)) return false;
        return ReferenceEquals(objA, objB) || Equals(objA, objB);
    }

    /// <summary>Determines whether the specified object instances are considered not equal.</summary>
    /// <param name="objA">The first object to compare.</param>
    /// <param name="objB">The first object to compare.</param>
    /// <returns>
    ///     true if the objects are considered not equal; otherwise, false. If both <paramref name="objA" /> and
    ///     <paramref name="objB" /> are null, the method returns false.
    /// </returns>
    protected static bool NotEqualOperator(ValueObject objA, ValueObject objB)
    {
        return !EqualOperator(objA, objB);
    }

    /// <summary>
    ///     Get equality components.
    /// </summary>
    /// <returns>objects that represents all equality components.</returns>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType()) return false;

        var other = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x == null! ? 0 : x.GetHashCode())
            .Aggregate(HashCode.Combine);
    }
}