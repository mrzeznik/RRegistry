using System;
using Xunit;
using RRegistry.DataGenerator;
using RRegistry.Tax;

namespace RRegistry.Tests;

public class TaxRegistryBuilderTests
{
    [Fact]
    public void Rule_Register_Pass()
    {
        var taxRule = new Rule<FooDocument, decimal?>()
        {
            Name = "Rule #1",
            Condition = _ => true,
            Value = 1
        };
        var taxRegistryBuilder = new TaxRegistryBuilder<FooDocument>();

        taxRegistryBuilder.RegisterRule(taxRule);
        var taxRegistry = taxRegistryBuilder.Build();
        var foundTax = taxRegistry.FindRule("Rule #1");

        Assert.Equal(taxRule, foundTax);
    }

    [Fact]
    public void Registry_Build_Pass()
    {
        var builder = new TaxRegistryBuilder<FooDocument>();
        var taxRule = new Rule<FooDocument, decimal?>()
        {
            Name = "Rule #1",
            Condition = _ => false,
            Value = 1
        };

        builder.RegisterRule(taxRule);
        var taxRegistry = builder.Build();
    }
}