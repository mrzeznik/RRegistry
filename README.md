# Rule Registry
Generic Rule Registry that allows to store and then look for Rules & Values matching given element.  

I frequently required to assign Tax basing on some properties of element:
- for invoices: issuer country & line type 
- for products: product type & country of origin, etc.

This project is an approach to create configurable & flexible Registry that can hold and select proper Values.

## Basic usage:
One can either:
- use generic Registry, with whatever types, conditions and values are desired
- use prefedined Registry implementations: Tax Registry or Action Registry
- implement IRegistry interface

### Samples

**Tax Registry**

This is one of predefined implementation, available in `RRegsitry.Tax` namespace.
1. Define a *Condition*, *Rule* and *Value* that should be assigned to matched elements:
    ```csharp
    static bool IsDomesticToyCondition(FooProduct element) => 
        element.Type == "TOY" && 
        element.CountryOfOrigin == "PL");

    var domesticToyRule = new Rule<FooProduct, decimal>()
    {
        Name = "Domestic Toys",
        Condition = IsDomesticToyCondition,
        Value = 0.05
    };
    ```
2. Create *Registry* instance with given set of *Rules*:
    ```csharp
    var taxRegistry = new TaxRegistry(new { domesticToyRule });
    ```
3. Ask to match *Rule* for given *element*:
    ```csharp
    var product = new FooProduct
    {  
        Name = "Radio controlled car",  
        Type = "TOY",  
        CountryOfOrigin = "PL"  
    };  
    var foundTaxRule = taxRegistry.MatchRule(product);
    // foundTaxRule => TaxRule "Domestic Toys" 
    ```

**Action Registry**

One can use it to create Registry of Actions, that can be used to modify object's parameters.

See `RRegistry.Action` namespace and `ActionRegistryTests.cs` file for more details.