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
        var rule = new BaseRegistry<string, string>(rules);

        Assert.NotNull(rule);
        Assert.Empty(rule.Rules);
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
        var rule = new BaseRegistry<string, string>(rules);

        var foundRule = rule.MatchRule(inputText);

        Assert.NotNull(foundRule);
        Assert.Equal(outputValue, foundRule.Value);
    }

    // set of rules for testing purposes only
    static bool ZeroLengthText(string element) => element.Length == 0;
    static bool StartsWithTest(string element) => element.Length >= 4 && element.Substring(0, 4) == "Test";
}