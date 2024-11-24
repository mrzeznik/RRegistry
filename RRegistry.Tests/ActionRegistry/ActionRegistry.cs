using System;
using System.Collections.Generic;

namespace RRegistry.Tests.ActionRegistry;

///<summary>Generic System.Action rules registry, so you can invoke some sort of action basing on checked object's properties.</summary>
public class ActionRegistry<TElement> : BaseRegistry<TElement, Action<TElement>>
{
    public ActionRegistry(
        IEnumerable<Rule<TElement, Action<TElement>>> rules,
        Rule<TElement, Action<TElement>>? defaultRule = null
        ) : base(rules, defaultRule) { }
}