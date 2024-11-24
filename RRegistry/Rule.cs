namespace RRegistry;

/// <summary>Representation of single Rule. Stores representation, with condition that should be met.</summary>
/// <typeparam name="TElement">Element type for which Rule is created.</typeparam>
/// <typeparam name="TOutput">Type of result returned when Rule is matched.</typeparam>
public record Rule<TElement, TOutput>
{
    /// <summary>Condition under which this Rule can be applied.</summary>
    public virtual Predicate<TElement> Condition { get; init; } = _ => false;

    /// <summary>Name Identifier for rule. Defaults to new GUID.</summary>
    public string Name { get; init; } = Guid.NewGuid().ToString();

    /// <summary>Value assigned to this Rule, that can be used, when Condition is met.</summary>
    public virtual TOutput? OutputValue { get; init; }

    /// <summary>Priority of the Rule. Defaults to 0.</summary>
    public int Priority { get; set; } = 0;

    public static Rule<TElement, TOutput> Create(Predicate<TElement> condition, TOutput value, string? name = null, int priority = 0)
    {
        ArgumentNullException.ThrowIfNull(condition, nameof(condition));

        return new Rule<TElement, TOutput>
        {
            Condition = condition,
            Name = name ?? Guid.NewGuid().ToString(),
            OutputValue = value,
            Priority = priority
        };
    }
}