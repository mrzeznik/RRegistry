namespace TaxHandler;

/// <summary>Basic Tax Handler with configurable Tax Rules.</summary>
/// <typeparam name="TElement">Element type for which Tax Handler is created.</typeparam>
public interface ITaxHandler<TElement>
{
    /// <summary>Get Tax Rule from registry, for which element matches rule condition.</summary>
    /// <param name="element">Element to check for matching Tax Rule.</param>
    TaxRule<TElement> GetRule(TElement element);

    /// <summary>Register new Tax Rule for this handler.</summary>
    /// <param name="taxRule">Tax Rule that will be stored in registry.</param>
    void RegisterRule(TaxRule<TElement> taxRule);

    /// <summary>Get single Tax Rule by name identifier.</summary>
    /// <param name="ruleIdentifier">Tax Rule name identifier to get from registry.</param>
    TaxRule<TElement> GetRule(string ruleIdentifier);

    /// <summary>Set default Tax rule for this handler, that will be returned if no Tax Rule was matched.</summary>
    /// <param name="taxRule">Tax Rule to be used as default. This Tax Rule is not added to the registry. Default: <see langword="null"/></param>
    void SetDefaultRule(TaxRule<TElement>? taxRule = null);
}