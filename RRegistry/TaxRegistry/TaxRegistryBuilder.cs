namespace RRegistry.Tax;

public class TaxRegistryBuilder<TElement> : IRegistryBuilder<TElement, decimal?>
{
    private TaxRegistry<TElement> _taxRegistry = new();
    private IList<Rule<TElement, decimal?>> _rules = new List<Rule<TElement, decimal?>>();
    private Rule<TElement, decimal?>? _defaultRule = null;

    public void Reset()
    {
        _rules = new List<Rule<TElement, decimal?>>();
        _taxRegistry = new TaxRegistry<TElement>();
    }

    public void RegisterRule(Rule<TElement, decimal?> taxRule)
    {
        if (taxRule != null) _rules.Add(taxRule);
    }

    public void SetDefaultRule(Rule<TElement, decimal?>? taxRule = null)
    {
        _defaultRule = taxRule;
    }

    public IRegistry<TElement, decimal?> Build()
    {
        _taxRegistry = new TaxRegistry<TElement>(_rules, _defaultRule);

        return _taxRegistry;
    }
}