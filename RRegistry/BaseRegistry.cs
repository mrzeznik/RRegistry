namespace RRegistry;

public class BaseRegistry<TElement, TOutput> : IRegistry<TElement, TOutput>
{
    public readonly IReadOnlySet<Rule<TElement, TOutput>> Rules = new HashSet<Rule<TElement, TOutput>>();
    public readonly Rule<TElement, TOutput>? DefaultRule = null;

    public BaseRegistry(IEnumerable<Rule<TElement, TOutput>> rules, Rule<TElement, TOutput>? defaultRule = null)
    {
        DefaultRule = defaultRule;
        Rules = rules?.ToHashSet() ?? throw new ArgumentNullException(nameof(rules));
    }

    internal BaseRegistry()
    {
        // exposed to be accessed by builders, otherwise do not expose constructor without parameters
    }

    public virtual Rule<TElement, TOutput>? FindRule(string ruleIdentifier)
    {
        return Rules.SingleOrDefault(x => x.Name.Equals(ruleIdentifier));
    }

    public virtual Rule<TElement, TOutput>? MatchRule(in TElement element)
    {
        foreach (var rule in Rules)
        {
            if (rule.Condition.Invoke(element)) return rule;
        }

        return DefaultRule;
    }

    public virtual bool TryMatchRule(in TElement element, out Rule<TElement, TOutput>? output)
    {
        foreach (var rule in Rules)
        {
            if (rule.Condition.Invoke(element))
            {
                output = rule;
                return true;
            }
        }

        output = DefaultRule;
        return false;
    }
}
