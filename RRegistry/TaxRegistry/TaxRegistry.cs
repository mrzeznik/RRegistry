namespace RRegistry.Tax;

/// <summary>Predefined implementation as Tax Registry. Null values are allowed. Has default Rule: null. </summary>
public class TaxRegistry<TElement> : BaseRegistry<TElement, decimal?>
{
    public TaxRegistry(
        IEnumerable<Rule<TElement, decimal?>> rules,
        Rule<TElement, decimal?>? defaultTaxRule = null
        ) : base(rules, defaultTaxRule) { }

    internal TaxRegistry() { }
}