namespace RRegistry;

/// <summary>Defines basic Registry methods for searching rules. Allows to match single Rule for element that matches conditions.</summary>
/// <typeparam name="TElement">Element type for which Registry is created.</typeparam>
public interface IRegistry<TElement, TOutput>
{
    /// <summary>Returns first Rule with matching conditions. If multiple Rules apply, only first one is returned. If nothing is found, default Rule will be returned.</summary>
    /// <param name="element">Element to check for matching Rule.</param>
    Rule<TElement, TOutput>? MatchRule(in TElement element);

    /// <summary>Returns first Rule with matching conditions. Returned value tells whether matching was sucessful.
    /// <para>If no Rule was matched, default Rule will be returned as <paramref name="output"/> and <see langword="false"/> as returned value.</para></summary>
    /// <param name="element">Element to check for matching Rule.</param>
    bool TryMatchRule(in TElement element, out Rule<TElement, TOutput>? output);

    /// <summary>Find single Rule by it's name identifier. If nothing is found, <see langword="null"/> will be returned.</summary>
    /// <param name="ruleIdentifier">Rule name identifier to get from registry.</param>
    Rule<TElement, TOutput>? FindRule(string ruleIdentifier);
}