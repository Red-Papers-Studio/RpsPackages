using System.Reflection;

namespace DomainDrivenDesign;

/// <summary>
///     Enumeration class.
/// </summary>
/// <typeparam name="TId">Id type of enumeration.</typeparam>
public abstract class Enumeration<TId>
{
    /// <summary>
    ///     Enumeration Id.
    /// </summary>
    public TId Id { get; }
    /// <summary>
    ///     Enumeration name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Initializes a new instance of the class.
    /// </summary>
    /// <param name="id">Id of enumeration.</param>
    /// <param name="name">Name of enumeration.</param>
    /// <exception cref="ArgumentNullException">Id or Name is null</exception>
    protected Enumeration(TId id, string name)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }


    /// <summary>
    ///     Get all enumeration static instances stored inside enumeration implementation with <typeparamref name="T"/> type
    /// </summary>
    /// <typeparam name="T">Enumeration implementation type.</typeparam>
    /// <returns>Enumeration instances.</returns>
    public static IEnumerable<T> GetAll<T>() where T : Enumeration<TId>
    {
        return typeof(T).GetFields(BindingFlags.Public |
                                   BindingFlags.Static |
                                   BindingFlags.DeclaredOnly)
            .Select(f => f.GetValue(null))
            .Cast<T>();
    }


    /// <summary>
    ///     Returns enumeration static instances with <paramref name="id"/> which is stored inside enumeration implementation with <typeparamref name="T"/> type
    /// </summary>
    /// <param name="id">Enumeration id.</param>
    /// <typeparam name="T">Enumeration implementation type</typeparam>
    /// <returns>Enumeration instance.</returns>
    public static Enumeration<TId> FromId<T>(TId id) where T : Enumeration<TId>
    {
        return GetAll<T>().Single(e => Equals(e.Id, id));
    }

    /// <summary>
    ///     Returns enumeration static instances with <paramref name="name"/> which is stored inside enumeration implementation with <typeparamref name="T"/> type
    /// </summary>
    /// <param name="name">Enumeration name.</param>
    /// <typeparam name="T">Enumeration implementation type</typeparam>
    /// <returns>Enumeration instance.</returns>
    public static Enumeration<TId> FromName<T>(string name) where T : Enumeration<TId>
    {
        return GetAll<T>().Single(e => string.Equals(e.Name, name, StringComparison.OrdinalIgnoreCase));
    }
}