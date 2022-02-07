namespace TaxHandler;

public class TaxHandler<TElement> : ITaxHandler<TElement>
{
    private IList<TaxRule<TElement>> _rules = new List<TaxRule<TElement>>();

    public void RegisterRule(TaxRule<TElement> taxRule)
    {
        _rules.Add(taxRule);
    }

    /// <summary>Get single Tax rule from registry.</summary>
    public TaxRule<TElement> GetRule(string ruleIdentifier)
    {
        return _rules.Single(x => x.Name.Equals(ruleIdentifier));
    }

    public TaxRule<TElement> GetRule(TElement document)
    {
        foreach (var rule in _rules)
        {
            if (rule.Condition.Invoke(document)) return rule;
        }

        return null;
    }
}