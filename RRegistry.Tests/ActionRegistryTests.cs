using System;

using RRegistry.Tests.ActionRegistry;

using Xunit;

namespace RRegistry.Tests;

public class ActionRegistryTests
{
    [Fact]
    public void Register_NoActionRules_Fails()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            var registry = new ActionRegistry<string>(null);
        });
    }

    [Fact]
    public void Register_EmptyActionRules_Pass()
    {
        var rules = new Rule<string, Action<string>>[] { };
        Assert.NotNull(new ActionRegistry<string>(rules));
    }

    [Fact]
    public void Register_Rule_Pass()
    {
        // given
        var actionRule = new Rule<TestObject, Action<TestObject>>()
        {
            Name = "Rule #1",
            Condition = IdIsSet,
            OutputValue = z => z.Name = "ds"
        };

        var actionRule2 = new Rule<TestObject, Action<TestObject>>()
        {
            Name = "Update empty name with default value",
            Condition = EmptyName,
            OutputValue = z => { z.Name = "(default)"; z.Value = 0; }
        };

        // register new rule in ActionRegistry
        var actionRegistryBuilder = new RegistryBuilder<TestObject, Action<TestObject>>();
        actionRegistryBuilder
            .RegisterRule(actionRule)
            .RegisterRule(actionRule2);
        var actionRegistry = actionRegistryBuilder.Build();

        // when
        var foundRule = actionRegistry.FindRule("Rule #1");
        var testObject = new TestObject() { Id = 1 };

        // perform Action from found Rule on testObject
        foundRule.OutputValue(testObject);

        // then
        Assert.Equal("ds", testObject.Name);
        Assert.Equal(actionRule, foundRule);
    }

    // set of rules for testing purposes only
    static bool IdIsSet(TestObject element) => element.Id != 0;
    static bool EmptyName(TestObject element) => string.IsNullOrWhiteSpace(element.Name);
}

public class TestObject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? Value { get; set; }
}