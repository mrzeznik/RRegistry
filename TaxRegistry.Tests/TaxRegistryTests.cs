using System;
using Xunit;
using TaxRegistry.DataGenerator;

namespace TaxRegistry.Tests;

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

        var taxRegistry = new TaxRegistry<FooDocument>();
        taxRegistry.RegisterRule(taxRule);
        var foundTax = taxRegistry.GetRule("Rule #1");

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

        var taxRegistry = new TaxRegistry<FooDocument>();
        taxRegistry.RegisterRule(taxRule);

        var document = FooDocument.CreateDummy();

        var foundTax = taxRegistry.GetRule(document);

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

        var taxRegistry = new TaxRegistry<FooProduct>();
        taxRegistry.RegisterRule(taxRule);

        var product = FooProduct.CreateDummy();

        var foundTax = taxRegistry.GetRule(product);

        Assert.NotNull(foundTax);
        Assert.Equal(taxRule, foundTax);
    }

    [Fact]
    public void Rule_NotAplicable_Fail()
    {
        var taxRegistry = new TaxRegistry<FooProduct>();
        var taxRule = new TaxRule<FooProduct>()
        {
            Name = "Domestic Toys",
            Condition = ProductIsDomesticToy,
            TaxValue = 5
        };

        taxRegistry.RegisterRule(taxRule);

        var product = new FooProduct
        {
            Name = "Radio controlled car",
            Type = "TOY",
            CountryOfOrigin = "CN"
        };

        var foundTax = taxRegistry.GetRule(product);

        Assert.Null(foundTax);
    }

    [Fact]
    public void Rule_Default_Assigned()
    {
        // just create tax registry and don't register any TaxRules, other than default
        var taxRegistry = new TaxRegistry<FooProduct>();

        var defaultTaxRule = new TaxRule<FooProduct>()
        {
            Name = "Default Tax",
            Condition = ProductNotNull,
            TaxValue = 0
        };

        taxRegistry.SetDefaultRule(defaultTaxRule);

        var product = new FooProduct
        {
            Name = "Radio controlled car",
            Type = "TOY",
            CountryOfOrigin = "CN"
        };

        var foundTax = taxRegistry.GetRule(product);

        Assert.NotNull(foundTax);
        Assert.Equal(defaultTaxRule, foundTax);
    }

    static bool NonEmptyReference(FooDocument element) => !string.IsNullOrEmpty(element.Reference);
    static bool ProductIsDomesticToy(FooProduct element) => element.Type.Equals("TOY", StringComparison.OrdinalIgnoreCase)
        && element.CountryOfOrigin.Equals("PL", StringComparison.OrdinalIgnoreCase);
    static bool ProductNotNull(FooProduct element) => element != null;
}