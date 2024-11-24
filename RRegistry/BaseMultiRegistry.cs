using System;

namespace RRegistry;

public class BaseMultiRegistry<TElement, TOutput> : BaseRegistry<TElement, TOutput>, IMultiRegistry<TElement, TOutput>
{
    public BaseMultiRegistry(IEnumerable<Rule<TElement, TOutput>> rules, Rule<TElement, TOutput>? defaultRule = null) 
        : base(rules, defaultRule)
    {
    }

    internal BaseMultiRegistry()
    {
        // exposed to be accessed by builders, otherwise do not expose constructor without parameters
    }

    public IEnumerable<Rule<TElement, TOutput>> MatchRules(TElement element)
    {
        foreach (var rule in Rules)
        {
            if (rule.Condition.Invoke(element)) yield return rule;
        }

        yield break;
    }

    public bool TryMatchRules(TElement element, out IEnumerable<Rule<TElement, TOutput>> output)
    {
        output = Array.Empty<Rule<TElement, TOutput>>();
        foreach (var rule in Rules)
        {
            if (rule.Condition.Invoke(element))
            {
                output = output.Append(rule);
            }
        }

        return output.Any();
    }

    public bool TryMatchAtLeastRules(TElement element, int minCount, out IEnumerable<Rule<TElement, TOutput>> output)
    {
        var matchedRules = Rules.Where(rule => rule.Condition.Invoke(element)).ToList();
        output = matchedRules;
        return matchedRules.Count >= minCount;
    }

    public bool TryMatchAtMostRules(TElement element, int maxCount, out IEnumerable<Rule<TElement, TOutput>> output)
    {
        var matchedRules = Rules.Where(rule => rule.Condition.Invoke(element)).Take(maxCount).ToList();
        output = matchedRules;
        return matchedRules.Any() && matchedRules.Count <= maxCount;
    }
}
