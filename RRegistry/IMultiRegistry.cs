namespace RRegistry;

/// <summary>Defines basic Registry methods for searching rules. Allows to match multiple Rules for element that matches conditions.</summary>
/// <typeparam name="TElement">Element type for which Registry is created.</typeparam>
public interface IMultiRegistry<TElement, TOutput>
{
    /// <summary>Returns all Rules with matching conditions. If nothing is found, empty collection of Rules will be returned.</summary>
    /// <param name="element">Element to check for matching Rules.</param>
    IEnumerable<Rule<TElement, TOutput>> MatchRules(TElement element);

    /// <summary>Returns all Rules with matching conditions. Returned value tells whether matching was successful.
    /// <para>If no Rule was matched, empty collection of Rules will be returned as <paramref name="output"/> and <see langword="false"/> as returned value.</para></summary>
    /// <param name="element">Element to check for matching Rule.</param>
    bool TryMatchRules(TElement element, out IEnumerable<Rule<TElement, TOutput>> output);

    /// <summary>Tries to match at least a given number of rules.</summary>
    /// <param name="element">Element to check for matching Rules.</param>
    /// <param name="minCount">Minimum number of rules to match.</param>
    /// <param name="output">Output collection of matched rules.</param>
    /// <returns>True if at least the given number of rules are matched, otherwise false.</returns>
    bool TryMatchAtLeastRules(TElement element, int minCount, out IEnumerable<Rule<TElement, TOutput>> output);

    /// <summary>Tries to match a maximum given number of rules.</summary>
    /// <param name="element">Element to check for matching Rules.</param>
    /// <param name="maxCount">Maximum number of rules to match.</param>
    /// <param name="output">Output collection of matched rules.</param>
    /// <returns>True if at least one rule is matched and the number of matched rules is less than or equal to the given maximum, otherwise false.</returns>
    bool TryMatchAtMostRules(TElement element, int maxCount, out IEnumerable<Rule<TElement, TOutput>> output);
}