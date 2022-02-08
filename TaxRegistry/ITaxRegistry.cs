namespace TaxRegistry;

/// <summary>Basic Tax Registry with configurable Tax Rules.</summary>
/// <typeparam name="TElement">Element type for which Tax Registry is created.</typeparam>
public interface ITaxRegistry<TElement>
{
    /// <summary>Get Tax Rule from registry, for which element matches rule condition.</summary>
    /// <param name="element">Element to check for matching Tax Rule.</param>
    TaxRule<TElement> GetRule(TElement element);

    /// <summary>Add a new Tax Rule to the registry.</summary>
    /// <param name="taxRule">Tax Rule that will be stored in registry.</param>
    void RegisterRule(TaxRule<TElement> taxRule);

    /// <summary>Get single Tax Rule by name identifier.</summary>
    /// <param name="ruleIdentifier">Tax Rule name identifier to get from registry.</param>
    TaxRule<TElement> GetRule(string ruleIdentifier);

    /// <summary>Set default Tax Rule, that will be returned if no Tax Rule was matched.</summary>
    /// <param name="taxRule">Tax Rule to be used as default. This Tax Rule is not added to the registry. Default: <see langword="null"/></param>
    void SetDefaultRule(TaxRule<TElement>? taxRule = null);
}