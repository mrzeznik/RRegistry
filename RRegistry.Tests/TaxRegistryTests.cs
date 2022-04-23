using System;
using Xunit;
using RRegistry.DataGenerator;
using RRegistry.Tax;

namespace RRegistry.Tests;

public class UnitTest1
{
    [Fact]
    public void Match_ExistingRule_Pass()
    {
        // given
        var taxRule = new Rule<FooDocument, decimal?>()
        {
            Name = "Rule #1",
            Condition = NonEmptyReference,
            Value = 1
        };

        var taxRegistryBuilder = new TaxRegistryBuilder<FooDocument>();
        taxRegistryBuilder.RegisterRule(taxRule);
        var taxRegistry = taxRegistryBuilder.Build();

        var document = FooDocument.CreateDummy();

        // when
        var foundTax = taxRegistry.MatchRule(document);

        // then
        Assert.NotNull(foundTax);
        Assert.Equal(taxRule, foundTax);
    }

    [Fact]
    public void Match_NoAplicableRule_Fail()
    {
        // given
        var taxRule = new Rule<FooProduct, decimal?>()
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

        // when
        var foundTax = taxRegistry.MatchRule(product);

        // then
        Assert.Null(foundTax);
    }

    [Fact]
    public void Match_NoRulesDefined_DefaultAssigned()
    {
        // given
        // just create tax registry and don't register any TaxRules, other than default
        var defaultTaxRule = new Rule<FooProduct, decimal?>()
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

        // when
        var foundTax = taxRegistry.MatchRule(product);

        // then
        Assert.NotNull(foundTax);
        Assert.Equal(defaultTaxRule, foundTax);
    }

    [Fact]
    public void TryMatch_NoRulesDefined_DefaultAssigned()
    {
        // given
        // just create tax registry and don't register any TaxRules, other than default
        var defaultTaxRule = new Rule<FooProduct, decimal?>()
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

        // when
        var foundTax = taxRegistry.TryMatchRule(product, out var output);

        // then
        Assert.False(foundTax);
        Assert.Equal(defaultTaxRule, output);
    }

    [Fact]
    public void TryMatch_Rule_Pass()
    {
        // given
        // just create tax registry and don't register any TaxRules, other than default
        var defaultTaxRule = new Rule<FooProduct, decimal?>()
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

        // when
        var foundTax = taxRegistry.TryMatchRule(product, out var output);

        // then
        Assert.False(foundTax);
        Assert.Equal(defaultTaxRule, output);
    }

    [Fact]
    public void TryMatch_NoDefaultNoRules_Pass()
    {
        // given
        var taxRegistryBuilder = new TaxRegistryBuilder<FooProduct>();
        var taxRegistry = taxRegistryBuilder.Build();

        var product = new FooProduct
        {
            Name = "Radio controlled car",
            Type = "TOY",
            CountryOfOrigin = "CN"
        };

        // when
        var foundTax = taxRegistry.TryMatchRule(product, out var output);

        // then
        Assert.False(foundTax);
        Assert.Null(output);
    }

    // set of rule conditions, for testing purposes only
    static bool NonEmptyReference(FooDocument element) => !string.IsNullOrEmpty(element.Reference);
    static bool ProductIsDomesticToy(FooProduct element) => element.Type.Equals("TOY", StringComparison.OrdinalIgnoreCase)
        && element.CountryOfOrigin.Equals("PL", StringComparison.OrdinalIgnoreCase);
    static bool ProductNotNull(FooProduct element) => element != null;
}