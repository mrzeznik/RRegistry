namespace RRegistry.Tax;

/// <summary>Predefined implementation as Tax Registry. Has default Rule: null.</summary>
public class TaxRegistry<TElement> : IRegistry<TElement, decimal?>
{
    private readonly IReadOnlySet<Rule<TElement, decimal?>> _rules = new HashSet<Rule<TElement, decimal?>>();
    private readonly Rule<TElement, decimal?>? _defaultTaxRule = null;

    public TaxRegistry(IEnumerable<Rule<TElement, decimal?>> rules, Rule<TElement, decimal?>? defaultTaxRule = null)
    {
        _defaultTaxRule = defaultTaxRule;
        _rules = rules.ToHashSet();
    }

    public TaxRegistry()
    {
    }

    public Rule<TElement, decimal?>? FindRule(string ruleIdentifier)
    {
        return _rules.SingleOrDefault(x => x.Name.Equals(ruleIdentifier));;
    }

    public Rule<TElement, decimal?>? MatchRule(TElement element)
    {
        foreach (var rule in _rules)
        {
            // return first matched rule
            if (rule.Condition.Invoke(element)) return rule;
        }

        return _defaultTaxRule;
    }

    public bool TryMatchRule(TElement element, out Rule<TElement, decimal?>? output)
    {
        foreach (var rule in _rules)
        {
            // return first matched rule
            if (rule.Condition.Invoke(element))
            {
                output = rule;
                return true;
            }
        }

        output = _defaultTaxRule;
        return false;
    }
}