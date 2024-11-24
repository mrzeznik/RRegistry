namespace RRegistry;

///<summary>Generic Rule Registry builder.</summary>
public class RegistryBuilder<TElement, TOutput> : IRegistryBuilder<TElement, TOutput>
{
    private BaseRegistry<TElement, TOutput> _baseRegistry = new();
    private IList<Rule<TElement, TOutput>> _rules = new List<Rule<TElement, TOutput>>();
    private Rule<TElement, TOutput>? _defaultRule = null;

    public void Reset()
    {
        _rules = new List<Rule<TElement, TOutput>>();
        _baseRegistry = new BaseRegistry<TElement, TOutput>();
        _defaultRule = null;
    }

    public virtual IRegistryBuilder<TElement, TOutput> RegisterRule(Rule<TElement, TOutput> ruleDefinition)
    {
        if (ruleDefinition != null) _rules.Add(ruleDefinition);

        return this;
    }

    public virtual void SetDefaultRule(Rule<TElement, TOutput>? rule = null)
    {
        _defaultRule = rule;
    }

    public virtual IRegistry<TElement, TOutput> Build()
    {
        _baseRegistry = new BaseRegistry<TElement, TOutput>(_rules, _defaultRule);

        return _baseRegistry;
    }
}