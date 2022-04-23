namespace RRegistry.Action;

public class ActionRegistry<TElement> : IRegistry<TElement, Action<TElement>>
{
    private readonly IReadOnlySet<Rule<TElement, Action<TElement>>> _rules = new HashSet<Rule<TElement, Action<TElement>>>();
    private readonly Rule<TElement, Action<TElement>>? _defaultTaxRule = null;

    public ActionRegistry(IEnumerable<Rule<TElement, Action<TElement>>> rules, Rule<TElement, Action<TElement>>? defaultTaxRule = null)
    {
        _defaultTaxRule = defaultTaxRule;
        _rules = rules.ToHashSet();
    }

    internal ActionRegistry()
    {
        // do not expose constructor without parameters
    }

    public Rule<TElement, Action<TElement>>? FindRule(string ruleIdentifier)
    {
        return _rules.SingleOrDefault(x => x.Name.Equals(ruleIdentifier)) ?? _defaultTaxRule;
    }

    public Rule<TElement, Action<TElement>>? MatchRule(TElement element)
    {
        foreach (var rule in _rules)
        {
            if (rule.Condition.Invoke(element)) return rule;
        }

        return _defaultTaxRule;
    }

    public bool TryMatchRule(TElement element, out Rule<TElement, Action<TElement>>? output)
    {
        foreach (var rule in _rules)
        {
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