namespace TaxRegistry;

/// <summary>Representation of single Tax Rule. Stores Tax representation, with condition that should be met.</summary>
/// <typeparam name="TElement">Element type for which Tax Rule is created.</typeparam>
public record TaxRule<TElement>
{
    /// <summary>Condition under which this Tax Rule can be applied.</summary>
    public Predicate<TElement> Condition { get; set; }

    /// <summary>Name Identifier for Tax rule.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Tax that will be applied, when condition is met.</summary>
    public int TaxValue { get; set; }
}