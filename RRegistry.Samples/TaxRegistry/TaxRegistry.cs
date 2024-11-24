using System;
using System.Collections.Generic;
using System.Linq;

namespace RRegistry.Samples.TaxRegistry
{
    public class TaxRegistry<TElement> : BaseRegistry<TElement, decimal>
    {
        public TaxRegistry(IEnumerable<Rule<TElement, decimal>> rules, Rule<TElement, decimal>? defaultRule = null)
            : base(rules, defaultRule)
        {
            if (rules.GroupBy(r => r.Priority).Any(g => g.Count() > 1))
            {
                throw new ArgumentOutOfRangeException(nameof(rules), "All rules must have unique priority values.");
            }
        }
    }
}