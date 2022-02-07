namespace TaxHandler;

public interface ITaxDefaultHandler<TElement>
{
    /// <summary>Set default Tax rule for this handler, that will be returned if no Tax Rule was matched.</summary>
    void SetDefaultRule(TaxRule<TElement> taxRule);
}