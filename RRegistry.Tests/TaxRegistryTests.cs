using System;
using System.Collections.Generic;

using RRegistry.Samples.TaxRegistry;

using Xunit;

namespace RRegistry.Tests
{
    public class TaxRegistryTests
    {
        [Fact]
        public void Register_NoRules_Fails()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var registry = new TaxRegistry<string>(null);
            });
        }

        [Fact]
        public void Register_EmptyRules_Pass()
        {
            var rules = new List<Rule<string, decimal>>() { };
            var registry = new TaxRegistry<string>(rules);

            Assert.NotNull(registry);
            Assert.Empty(registry.Rules);
        }

        [Theory]
        [InlineData("Domestic", 0.05)]
        [InlineData("Imported", 0.10)]
        public void Match_ExistingRule_Pass(string inputText, decimal outputValue)
        {
            var rules = new List<Rule<string, decimal>>()
            {
                Rule<string, decimal>.Create(IsDomestic, 0.05m, priority: 1),
                Rule<string, decimal>.Create(IsImported, 0.10m, priority: 2)
            };
            var registry = new TaxRegistry<string>(rules);

            var foundRule = registry.MatchRule(inputText);

            Assert.NotNull(foundRule);
            Assert.Equal(outputValue, foundRule.OutputValue);
        }

        [Fact]
        public void Register_RulesWithSamePriority_ThrowsException()
        {
            var rules = new List<Rule<string, decimal>>()
            {
                Rule<string, decimal>.Create(IsDomestic, 0.05m, priority: 1),
                Rule<string, decimal>.Create(IsImported, 0.10m, priority: 1)
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => new TaxRegistry<string>(rules));
        }

        [Fact]
        public void Register_RulesWithDifferentPriorities_SortedCorrectly()
        {
            var rules = new List<Rule<string, decimal>>()
            {
                Rule<string, decimal>.Create(IsDomestic, 0.05m, priority: 1),
                Rule<string, decimal>.Create(IsImported, 0.10m, priority: 2)
            };
            var registry = new TaxRegistry<string>(rules);

            var sortedRules = new List<Rule<string, decimal>>(registry.Rules);

            Assert.Equal(2, sortedRules.Count);
            Assert.Equal(0.10m, sortedRules[0].OutputValue);
            Assert.Equal(0.05m, sortedRules[1].OutputValue);
        }

        [Theory]
        [InlineData("Domestic", 0.05)]
        [InlineData("Imported", 0.10)]
        public void Match_RuleWithHigherPriority_Pass(string inputText, decimal outputValue)
        {
            var rules = new List<Rule<string, decimal>>()
            {
                Rule<string, decimal>.Create(IsDomestic, 0.05m, priority: 1),
                Rule<string, decimal>.Create(IsImported, 0.10m, priority: 2)
            };
            var registry = new TaxRegistry<string>(rules);

            var foundRule = registry.MatchRule(inputText);

            Assert.NotNull(foundRule);
            Assert.Equal(outputValue, foundRule.OutputValue);
        }

        // Helper methods for testing
        static bool IsDomestic(string element) => element == "Domestic";
        static bool IsImported(string element) => element == "Imported";
    }
}