namespace RRegistry;

public class BaseRegistryBuilder<TElement, TOutput> : IRegistryBuilder<TElement, TOutput>
{
    private BaseRegistry<TElement, TOutput> _baseRegistry = new();
    private IList<Rule<TElement, TOutput>> _rules = new List<Rule<TElement, TOutput>>();
    private Rule<TElement, TOutput>? _defaultRule = null;

    public void Reset()
    {
        _rules = new List<Rule<TElement, TOutput>>();
        _baseRegistry = new BaseRegistry<TElement, TOutput>();
    }

    public virtual void RegisterRule(Rule<TElement, TOutput> taxRule)
    {
        if (taxRule != null) _rules.Add(taxRule);
    }

    public virtual void SetDefaultRule(Rule<TElement, TOutput>? taxRule = null)
    {
        _defaultRule = taxRule;
    }

    public virtual IRegistry<TElement, TOutput> Build()
    {
        _baseRegistry = new BaseRegistry<TElement, TOutput>(_rules, _defaultRule);

        return _baseRegistry;
    }
}