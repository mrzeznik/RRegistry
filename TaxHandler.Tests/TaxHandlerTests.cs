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
            Condition = NonEmptyReference,
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
            Condition = NonEmptyReference,
            TaxValue = 1
        };

        var taxHandler = new TaxHandler<FooDocument>();
        taxHandler.RegisterRule(taxRule);

        var document = FooDocument.CreateDummy();

        var foundTax = taxHandler.GetRule(document);

        Assert.NotNull(foundTax);
        Assert.Equal(taxRule, foundTax);
    }

    [Fact]
    public void Rule_ToyFound_Pass()
    {
        var taxRule = new TaxRule<FooProduct>()
        {
            Name = "Domestic Toys",
            Condition = ProductIsDomesticToy,
            TaxValue = 5
        };

        var taxHandler = new TaxHandler<FooProduct>();
        taxHandler.RegisterRule(taxRule);

        var document = FooProduct.CreateDummy();

        var foundTax = taxHandler.GetRule(document);

        Assert.NotNull(foundTax);
        Assert.Equal(taxRule, foundTax);
    }

    [Fact]
    public void Rule_NotAplicable_Fail()
    {
        var taxRule = new TaxRule<FooProduct>()
        {
            Name = "Domestic Toys",
            Condition = ProductIsDomesticToy,
            TaxValue = 5
        };

        var taxHandler = new TaxHandler<FooProduct>();
        taxHandler.RegisterRule(taxRule);

        var document = new FooProduct
        {
            Name = "Radio controlled car",
            Type = "TOY",
            CountryOfOrigin = "CN"
        };

        var foundTax = taxHandler.GetRule(document);

        Assert.Null(foundTax);
    }

    static bool NonEmptyReference(FooDocument element) => !string.IsNullOrEmpty(element.Reference);
    static bool ProductIsDomesticToy(FooProduct element) => element.Type.Equals("TOY", StringComparison.OrdinalIgnoreCase)
        && element.CountryOfOrigin.Equals("PL", StringComparison.OrdinalIgnoreCase);
}