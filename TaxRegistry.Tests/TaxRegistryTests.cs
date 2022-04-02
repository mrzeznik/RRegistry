using System;
using Xunit;
using RRegistry.DataGenerator;

namespace RRegistry.Tests;

public class UnitTest1
{
    [Fact]
    public void Rule_Register_Pass()
    {
        var taxRule = new Rule<FooDocument, decimal>()
        {
            Name = "Rule #1",
            Condition = NonEmptyReference,
            Value = 1
        };

        var taxRegistryBuilder = new TaxRegistryBuilder<FooDocument>();
        taxRegistryBuilder.RegisterRule(taxRule);
        var taxRegistry = taxRegistryBuilder.Build();
        var foundTax = taxRegistry.FindRule("Rule #1");

        Assert.Equal(taxRule, foundTax);
    }

    [Fact]
    public void Rule_Found_Pass()
    {
        var taxRule = new Rule<FooDocument, decimal>()
        {
            Name = "Rule #1",
            Condition = NonEmptyReference,
            Value = 1
        };

        var taxRegistryBuilder = new TaxRegistryBuilder<FooDocument>();
        taxRegistryBuilder.RegisterRule(taxRule);
        var taxRegistry = taxRegistryBuilder.Build();

        var document = FooDocument.CreateDummy();

        var foundTax = taxRegistry.MatchRule(document);

        Assert.NotNull(foundTax);
        Assert.Equal(taxRule, foundTax);
    }

    [Fact]
    public void Rule_ToyFound_Pass()
    {
        var taxRule = new Rule<FooProduct, decimal>()
        {
            Name = "Domestic Toys",
            Condition = ProductIsDomesticToy,
            Value = 5
        };

        var taxRegistryBuilder = new TaxRegistryBuilder<FooProduct>();
        taxRegistryBuilder.RegisterRule(taxRule);
        var taxRegistry = taxRegistryBuilder.Build();

        var product = FooProduct.CreateDummy();

        var foundTax = taxRegistry.MatchRule(product);

        Assert.NotNull(foundTax);
        Assert.Equal(taxRule, foundTax);
    }

    [Fact]
    public void Rule_NotAplicable_Fail()
    {
        var taxRule = new Rule<FooProduct, decimal>()
        {
            Name = "Domestic Toys",
            Condition = ProductIsDomesticToy,
            Value = 5
        };

        var taxRegistryBuilder = new TaxRegistryBuilder<FooProduct>();
        taxRegistryBuilder.RegisterRule(taxRule);
        var taxRegistry = taxRegistryBuilder.Build();

        var product = new FooProduct
        {
            Name = "Radio controlled car",
            Type = "TOY",
            CountryOfOrigin = "CN"
        };

        var foundTax = taxRegistry.MatchRule(product);

        Assert.Null(foundTax);
    }

    [Fact]
    public void Rule_Default_Assigned()
    {
        // just create tax registry and don't register any TaxRules, other than default
        var defaultTaxRule = new Rule<FooProduct, decimal>()
        {
            Name = "Default Tax",
            Condition = ProductNotNull,
            Value = 0
        };
        var taxRegistryBuilder = new TaxRegistryBuilder<FooProduct>();
        taxRegistryBuilder.SetDefaultRule(defaultTaxRule);
        var taxRegistry = taxRegistryBuilder.Build();

        var product = new FooProduct
        {
            Name = "Radio controlled car",
            Type = "TOY",
            CountryOfOrigin = "CN"
        };

        var foundTax = taxRegistry.MatchRule(product);

        Assert.NotNull(foundTax);
        Assert.Equal(defaultTaxRule, foundTax);
    }

    // set of rules for testing purposes only
    static bool NonEmptyReference(FooDocument element) => !string.IsNullOrEmpty(element.Reference);
    static bool ProductIsDomesticToy(FooProduct element) => element.Type.Equals("TOY", StringComparison.OrdinalIgnoreCase)
        && element.CountryOfOrigin.Equals("PL", StringComparison.OrdinalIgnoreCase);
    static bool ProductNotNull(FooProduct element) => element != null;
}