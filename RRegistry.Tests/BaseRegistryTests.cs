using System;
using System.Collections.Generic;
using Xunit;

namespace RRegistry.Tests;

public class BaseRegistryTests
{
    [Fact]
    public void Register_NoRules_Fails()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            var registry = new BaseRegistry<string, string?>(null);
        });
    }

    [Fact]
    public void Register_EmptyRules_Pass()
    {
        var rules = new List<Rule<string, string>>() { };
        var registry = new BaseRegistry<string, string>(rules);

        Assert.NotNull(registry);
        Assert.Empty(registry.Rules);
    }

    [Theory]
    [InlineData("", "(no text)")]
    [InlineData("TestString", "(text starts with string)")]
    public void Match_ExistingRule_Pass(string inputText, string outputValue)
    {
        var rules = new List<Rule<string, string>>()
        {
            Rule<string, string>.Create(ZeroLengthText, "(no text)"),
            Rule<string, string>.Create(StartsWithTest, "(text starts with string)")
        };
        var registry = new BaseRegistry<string, string>(rules);

        var foundRule = registry.MatchRule(inputText);

        Assert.NotNull(foundRule);
        Assert.Equal(outputValue, foundRule.OutputValue);
    }

    [Fact]
    public void Register_RulesWithSamePriority_Pass()
    {
        var rules = new List<Rule<string, string>>()
        {
            Rule<string, string>.Create(ZeroLengthText, "(no text)", priority: 1),
            Rule<string, string>.Create(StartsWithTest, "(text starts with string)", priority: 1)
        };
        var registry = new BaseRegistry<string, string>(rules);

        Assert.NotNull(registry);
        Assert.Equal(2, registry.Rules.Count);
    }

    [Fact]
    public void Register_RulesWithDifferentPriorities_SortedCorrectly()
    {
        var rules = new List<Rule<string, string>>()
        {
            Rule<string, string>.Create(ZeroLengthText, "(no text)", priority: 1),
            Rule<string, string>.Create(StartsWithTest, "(text starts with string)", priority: 2)
        };
        var registry = new BaseRegistry<string, string>(rules);

        var sortedRules = new List<Rule<string, string>>(registry.Rules);

        Assert.Equal(2, sortedRules.Count);
        Assert.Equal("(text starts with string)", sortedRules[0].OutputValue);
        Assert.Equal("(no text)", sortedRules[1].OutputValue);
    }

    [Theory]
    [InlineData("", "(no text)")]
    [InlineData("TestString", "(text starts with string)")]
    public void Match_RuleWithHigherPriority_Pass(string inputText, string outputValue)
    {
        var rules = new List<Rule<string, string>>()
        {
            Rule<string, string>.Create(ZeroLengthText, "(no text)", priority: 1),
            Rule<string, string>.Create(StartsWithTest, "(text starts with string)", priority: 2)
        };
        var registry = new BaseRegistry<string, string>(rules);

        var foundRule = registry.MatchRule(inputText);

        Assert.NotNull(foundRule);
        Assert.Equal(outputValue, foundRule.OutputValue);
    }

    // set of rules for testing purposes only
    static bool ZeroLengthText(string element) => element.Length == 0;
    static bool StartsWithTest(string element) => element.Length >= 4 && element.Substring(0, 4) == "Test";
}