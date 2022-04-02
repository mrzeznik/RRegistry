namespace RRegistry;

/// <summary>Representation of single Rule. Stores representation, with condition that should be met.</summary>
/// <typeparam name="TElement">Element type for which Rule is created.</typeparam>
public record Rule<TElement, TOutput>
{
    /// <summary>Condition under which this Rule can be applied.</summary>
    public virtual Predicate<TElement> Condition { get; init; } = _ => false;

    /// <summary>Name Identifier for rule.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Value assigned to this Rule, that can be used, when Condition is met.</summary>
    public virtual TOutput? Value { get; init; }
}