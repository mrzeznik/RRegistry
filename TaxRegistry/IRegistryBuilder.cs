namespace RRegistry;

public interface IRegistryBuilder<TElement, TOutput>
{
    /// <summary>Add a new Rule to the registry.</summary>
    /// <param name="ruleDefinition">Rule that will be stored in registry.</param>
    void RegisterRule(Rule<TElement, TOutput> ruleDefinition);

    /// <summary>Set default Rule, that will be returned if no Rule was matched.</summary>
    /// <param name="taxRule">Rule to be used as default. This Rule is not added to the registry. Default: <see langword="null"/></param>
    void SetDefaultRule(Rule<TElement, TOutput>? taxRule = null);
}