namespace RRegistry.Action;

public class ActionRegistryBuilder<TElement> : IRegistryBuilder<TElement, Action<TElement>>
{
    private ActionRegistry<TElement> _actionRegistry = new();
    private IList<Rule<TElement, Action<TElement>>> _rules = new List<Rule<TElement, Action<TElement>>>();
    private Rule<TElement, Action<TElement>>? _defaultRule = null;

    public void Reset()
    {
        _rules = new List<Rule<TElement, Action<TElement>>>();
        _actionRegistry = new ActionRegistry<TElement>();
    }

    public void RegisterRule(Rule<TElement, Action<TElement>> taxRule)
    {
        if (taxRule != null) _rules.Add(taxRule);
    }

    public void SetDefaultRule(Rule<TElement, Action<TElement>>? taxRule = null)
    {
        _defaultRule = taxRule;
    }

    public IRegistry<TElement, Action<TElement>> Build()
    {
        _actionRegistry = new ActionRegistry<TElement>(_rules, _defaultRule);

        return _actionRegistry;
    }
}