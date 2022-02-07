namespace TaxHandler;

public interface ITaxHandler<TElement>
{
    /// <summary>Get Tax, basing on registered Tax rules.</summary>
    TaxRule<TElement> GetRule(TElement document);

    /// <summary>Register new Tax rule for this handler.</summary>
    void RegisterRule<TRule>(TaxRule<TRule> taxRule);

    /// <summary>Get single Tax rule from registry.</summary>
    TaxRule<TElement> GetRule(string ruleIdentifier);
}

public class TaxHandler<TElement> : ITaxHandler<TElement>
{
    public void RegisterRule<TRule>(TaxRule<TRule> taxRule)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>Get single Tax rule from registry.</summary>
    public TaxRule<TElement> GetRule(string ruleIdentifier)
    {
        throw new System.NotImplementedException();
    }

    public TaxRule<TElement> GetRule(TElement document)
    {
        throw new NotImplementedException();
    }
}