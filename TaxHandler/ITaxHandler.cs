namespace TaxHandler;

public interface ITaxHandler<TElement>
{
    /// <summary>Get Tax, basing on registered Tax rules.</summary>
    TaxRule<TElement> GetRule(TElement document);

    /// <summary>Register new Tax rule for this handler.</summary>
    void RegisterRule(TaxRule<TElement> taxRule);

    /// <summary>Get single Tax rule from registry.</summary>
    TaxRule<TElement> GetRule(string ruleIdentifier);
}