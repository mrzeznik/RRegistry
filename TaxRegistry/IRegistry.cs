namespace RRegistry;

/// <summary>Defines basic Registry methods for searching rules.</summary>
/// <typeparam name="TElement">Element type for which Registry is created.</typeparam>
public interface IRegistry<TElement, TOutput>
{
    /// <summary>Returns Rule from registry, for which element matches rule condition.</summary>
    /// <param name="element">Element to check for matching Rule.</param>
    Rule<TElement, TOutput>? MatchRule(TElement element);

    // /// <summary>Add a new Rule to the registry.</summary>
    // /// <param name="ruleDefinition">Rule that will be stored in registry.</param>
    // void RegisterRule(Rule<TElement, decimal> ruleDefinition);

    /// <summary>Find single Rule by it's name identifier.</summary>
    /// <param name="ruleIdentifier">Rule name identifier to get from registry.</param>
    Rule<TElement, TOutput>? FindRule(string ruleIdentifier);
}