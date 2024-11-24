using System;
using Xunit;
using RRegistry.Tests.ProductRegistry;

namespace RRegistry.Tests;

public class UnitTest1
{
    [Fact]
    public void Match_ExistingRule_Pass()
    {
        // given
        var testProductRule = new Rule<FooProduct, decimal?>()
        {
            Name = "Rule #1",
            Condition = NonEmptyCountryOfOrigin,
            OutputValue = 1
        };

        var productRegistry = new ProductRegistry<decimal?>(new[] {testProductRule});

        var document = FooProduct.CreateDummy();

        // when
        var foundTax = productRegistry.MatchRule(document);

        // then
        Assert.NotNull(foundTax);
        Assert.Equal(testProductRule, foundTax);
    }

    [Fact]
    public void Match_NoAplicableRule_Fail()
    {
        // given
        var testProductRule = new Rule<FooProduct, decimal?>()
        {
            Name = "Domestic Toys",
            Condition = ProductIsDomesticToy,
            OutputValue = 5
        };

        var productRegistry = new ProductRegistry<decimal?>(new[] {testProductRule});

        var product = new FooProduct
        {
            Name = "Radio controlled car",
            Type = "TOY",
            CountryOfOrigin = "CN"
        };

        // when
        var foundTax = productRegistry.MatchRule(product);

        // then
        Assert.Null(foundTax);
    }

    [Fact]
    public void Match_NoRulesDefined_DefaultAssigned()
    {
        // given
        // just create tax registry and don't register any TaxRules, other than default
        var defaultRule = new Rule<FooProduct, string>()
        {
            Name = "Default Tax",
            Condition = ProductNotNull,
            OutputValue = "This will be output value from rule"
        };
        var productRegistry = new ProductRegistry<string>([], defaultRule);

        var product = new FooProduct
        {
            Name = "Radio controlled car",
            Type = "TOY",
            CountryOfOrigin = "CN"
        };

        // when
        var foundTax = productRegistry.MatchRule(product);

        // then
        Assert.NotNull(foundTax);
        Assert.Equal(defaultRule, foundTax);
    }

    [Fact]
    public void TryMatch_NoRulesDefined_DefaultAssigned()
    {
        // given
        // just create tax registry and don't register any TaxRules, other than default
        var defaultRule = new Rule<FooProduct, decimal?>()
        {
            Name = "Default Tax",
            Condition = ProductNotNull,
            OutputValue = 0
        };
        var productRegistry = new ProductRegistry<decimal?>([], defaultRule);

        var product = new FooProduct
        {
            Name = "Radio controlled car",
            Type = "TOY",
            CountryOfOrigin = "CN"
        };

        // when
        var foundValue = productRegistry.TryMatchRule(product, out var output);

        // then
        Assert.False(foundValue);
        Assert.Equal(defaultRule, output);
    }

    [Fact]
    public void TryMatch_Rule_Pass()
    {
        // given
        // just create tax registry and don't register any TaxRules, other than default
        var defaultRule = new Rule<FooProduct, decimal?>()
        {
            Name = "Default Tax",
            Condition = ProductNotNull,
            OutputValue = 0
        };
        var productRegistry = new ProductRegistry<decimal?>([], defaultRule);

        var product = new FooProduct
        {
            Name = "Radio controlled car",
            Type = "TOY",
            CountryOfOrigin = "CN"
        };

        // when
        var foundTax = productRegistry.TryMatchRule(product, out var output);

        // then
        Assert.False(foundTax);
        Assert.Equal(defaultRule, output);
    }

    [Fact]
    public void TryMatch_NoDefaultNoRules_Pass()
    {
        // given
        var productRegistry = new ProductRegistry<FooProduct>([]);

        var product = new FooProduct
        {
            Name = "Radio controlled car",
            Type = "TOY",
            CountryOfOrigin = "CN"
        };

        // when
        var foundTax = productRegistry.TryMatchRule(product, out var output);

        // then
        Assert.False(foundTax);
        Assert.Null(output);
    }

    // set of rule conditions, for testing purposes only
    static bool NonEmptyCountryOfOrigin(FooProduct element) => !string.IsNullOrEmpty(element.CountryOfOrigin);
    static bool ProductIsDomesticToy(FooProduct element) => element.Type.Equals("TOY", StringComparison.OrdinalIgnoreCase)
        && element.CountryOfOrigin.Equals("PL", StringComparison.OrdinalIgnoreCase);
    static bool ProductNotNull(FooProduct element) => element != null;
}