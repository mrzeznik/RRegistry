# Rule Registry

A generic Rule Registry that allows storing and then looking for Rules & Values matching a given element.

## Overview

This project provides a flexible and configurable registry system that can hold and select appropriate values based on defined rules. It is particularly useful for scenarios such as assigning taxes based on properties of elements like invoices or products.

## Features

- **Generic Registry**: Use with any types, conditions, and values.
- **Typed Registry Implementations**: Create specific registry implementations with one or no generic values.
- **Priority Handling**: Rules can be prioritized, and the registry will consider these priorities when matching rules.

## Basic Usage

### Defining a Rule

Define a condition: 
```csharp
static bool IsDomesticToyCondition(FooProduct element) => 
    element.Type == "TOY" && 
    element.CountryOfOrigin == "PL";
```

Define a rule, and value that should be assigned to matched elements:
```csharp
var domesticToyRule = new Rule<FooProduct, decimal>()
{
    Name = "Domestic Toys",
    Condition = IsDomesticToyCondition,
    OutputValue = 0.05m
};
```

### Creating a Registry

Create a registry instance with the given set of rules:
```csharp
var taxRegistry = new BaseRegistry<FooProduct>(new[] { domesticToyRule });
```

### Matching a Rule

Match a rule for a given element:
```csharp
var product = new FooProduct
{  
    Name = "Radio controlled car",  
    Type = "TOY",  
    CountryOfOrigin = "PL"  
};  
var foundTaxRule = taxRegistry.MatchRule(product);
// returns: foundTaxRule => Rule "Domestic Toys" with OutputValue 0.05
```

## Example

Here's a complete example demonstrating the basic usage of the Rule Registry:

```csharp
using System;
using System.Collections.Generic;
using RRegistry;
using RRegistry.Samples.TaxRegistry;

public class FooProduct
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string CountryOfOrigin { get; set; }
}

public class Program
{
    static bool IsDomesticToyCondition(FooProduct element) => 
        element.Type == "TOY" && 
        element.CountryOfOrigin == "PL";

    public static void Main()
    {
        var domesticToyRule = new Rule<FooProduct, decimal>()
        {
            Name = "Domestic Toys",
            Condition = IsDomesticToyCondition,
            OutputValue = 0.05m,
            Priority = 1
        };

        var taxRegistry = new TaxRegistry<FooProduct>(new[] { domesticToyRule });

        var product = new FooProduct
        {  
            Name = "Radio controlled car",  
            Type = "TOY",  
            CountryOfOrigin = "PL"  
        };  
        var foundTaxRule = taxRegistry.MatchRule(product);

        if (foundTaxRule != null)
        {
            Console.WriteLine($"Matched Rule: {foundTaxRule.Name}, Tax: {foundTaxRule.OutputValue}");
        }
        else
        {
            Console.WriteLine("No matching rule found.");
        }
    }
}
```

## Advanced Usage

### Matching Multiple Rules

You can also match multiple rules for a given element:

```csharp
var matchedRules = taxRegistry.MatchRules(product);
foreach (var rule in matchedRules)
{
    Console.WriteLine($"Matched Rule: {rule.Name}, Tax: {rule.OutputValue}");
}
```

### Matching Rules with Priority

The registry takes the priority of rules into consideration when matching:

```csharp
var highPriorityRule = new Rule<FooProduct, decimal>()
{
    Name = "High Priority Rule",
    Condition = IsDomesticToyCondition,
    OutputValue = 0.10m,
    Priority = 2
};

var taxRegistry = new TaxRegistry<FooProduct>(new[] { domesticToyRule, highPriorityRule });

var foundTaxRule = taxRegistry.MatchRule(product);
// foundTaxRule => Rule "High Priority Rule" with OutputValue 0.10
```

## Conclusion

The Rule Registry library provides a powerful and flexible way to define and match rules based on various conditions and priorities. It can be easily extended and customized to fit different use cases and requirements.