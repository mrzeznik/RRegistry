using System;
using Xunit;
using TaxHandler.DataGenerator;

namespace TaxHandler.Tests;

public class UnitTest1
{
    [Fact]
    public void Rule_Register_Pass()
    {
        var taxRule = new TaxRule<FooDocument>()
        {
            Name = "Rule #1",
            Condition = NonEmptyCondition,
            TaxValue = 1
        };

        var taxHandler = new TaxHandler<FooDocument>();
        taxHandler.RegisterRule(taxRule);
        var foundTax = taxHandler.GetRule("Rule #1");

        Assert.Equal(taxRule, foundTax);
    }

    [Fact]
    public void Rule_Found_Pass()
    {
        var taxRule = new TaxRule<FooDocument>()
        {
            Name = "Rule #1",
            Condition = NonEmptyCondition,
            TaxValue = 1
        };

        var taxHandler = new TaxHandler<FooDocument>();
        taxHandler.RegisterRule(taxRule);

        var document = FooDocument.CreateDummy();

        var foundTax = taxHandler.GetRule(document);

        Assert.NotNull(foundTax);
        Assert.Equal(taxRule, foundTax);
    }

    static bool NonEmptyCondition(FooDocument element) => !string.IsNullOrEmpty(element.Reference);
}