# Rule Registry
Generic Rule Registry that allows to look for Rule matching given element.  

I frequently required to select Tax basing on some properties of element:
- for invoices: issuer country & invoice date 
- for products: product type & country of origin, etc.
- for services: service category

This project is an approach to create configurable Registry.

## Basic usage:
1. Create methods / Predicates that will serve as *Conditions*:
    ```csharp
    static bool IsDomesticToy(FooProduct element) => element.Type.Equals("TOY", StringComparison.OrdinalIgnoreCase)
            && element.CountryOfOrigin.Equals("PL", StringComparison.OrdinalIgnoreCase);
    ```
2. Create *Registry* instance with given set of *Rules*:
    ```csharp
    var taxRule = new Rule<FooProduct, decimal>()
    {
        Name = "Domestic Toys",
        Condition = IsDomesticToy,
        Value = 0.05
    };
    var taxRegistry = new TaxRegistry(new { taxRule });
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
