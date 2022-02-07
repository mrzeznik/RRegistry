namespace TaxHandler;

/// <summary>Representation of single Tax Rule. Stores Tax representation, with condition that should be met.</summary>
/// <typeparam name="TElement">The element type for which Tax is considered</typeparam>
public record TaxRule<TElement>
{
    /// <summary><c>TValue</c> condition under which <c>TResult</c> Tax will be applied</summary>
    public Predicate<TElement> Condition { get; set; }

    /// <summary>Name Identifier for Tax rule</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Tax that will be applied, when condition is met</summary>
    public int TaxValue { get; set; }
}