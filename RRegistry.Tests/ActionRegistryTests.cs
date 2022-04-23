using System;
using Xunit;
using RRegistry.Action;

namespace RRegistry.Tests;

public class ActionRegistryTests
{
    [Fact]
    public void Rule_Register_Pass()
    {
        // given
        var actionRule = new Rule<TestObject, Action<TestObject>>()
        {
            Name = "Rule #1",
            Condition = IdIsSet,
            Value = z => z.Name = "ds"
        };

        var actionRule2 = new Rule<TestObject, Action<TestObject>>()
        {
            Name = "Update empty name with default value",
            Condition = EmptyName,
            Value = z => { z.Name = "(default)"; z.Value = 0; }
        };

        // register new rule in ActionRegistry
        var actionRegistryBuilder = new ActionRegistryBuilder<TestObject>();
        actionRegistryBuilder.RegisterRule(actionRule);
        actionRegistryBuilder.RegisterRule(actionRule2);
        var actionRegistry = actionRegistryBuilder.Build();

        // when
        var foundRule = actionRegistry.FindRule("Rule #1");
        var testObject = new TestObject() { Id = 1 };

        // perform Action from found Rule on testObject
        foundRule.Value(testObject);

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