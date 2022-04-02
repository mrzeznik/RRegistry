using System;
using Xunit;
using RRegistry.DataGenerator;

namespace RRegistry.Tests;

public class TaxRegistryBuilderTests
{
    [Fact]
    public void Registry_Build_Pass()
    {
        var builder = new TaxRegistryBuilder<FooDocument>();
        var taxRule = new Rule<FooDocument, decimal>()
        {
            Name = "Rule #1",
            Condition = _ => false,
            Value = 1
        };

        builder.RegisterRule(taxRule);
        var taxRegistry = builder.Build();
    }
}