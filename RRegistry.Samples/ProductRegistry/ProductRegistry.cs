using System.Collections.Generic;
using RRegistry;

namespace RRegistry.Samples.ProductRegistry;

///<summary>Semi-generic registry, that will store Rules for objects of FooProduct type, and returns of TOutputElement type.</summary>
public class ProductRegistry<TOutputElement>(
    IEnumerable<Rule<FooProduct, TOutputElement>> rules,
    Rule<FooProduct, TOutputElement>? defaultRule = null
        ) : BaseRegistry<FooProduct, TOutputElement>(rules, defaultRule)
{
}