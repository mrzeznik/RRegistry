namespace RRegistry.Action;

public class ActionRegistry<TElement> : BaseRegistry<TElement, Action<TElement>>
{
    public ActionRegistry(
        IEnumerable<Rule<TElement, Action<TElement>>> rules,
        Rule<TElement, Action<TElement>>? defaultTaxRule = null
        ) : base(rules, defaultTaxRule) { }

    internal ActionRegistry()
    {
        // exposed to be accessed by builders, otherwise do not expose constructor without parameters
    }
}