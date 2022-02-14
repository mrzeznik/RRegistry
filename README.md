# Tax registry
Generic Tax registry that allows to select Tax, with user specified Tax rules.  

I frequently required to select Tax basing on some properties of element:
- for invoices: issuer country & invoice date 
- for products: product type & country of origin, etc.
- for services: service category

This project is an approach to create configurable Tax registry.

## Basic usage:
1. Init Tax registry for given element type:
    ```csharp
    var taxRegistry = new TaxRegistry<FooProduct>();
    ```
2. Create methods / Predicates that will serve as conditions:
    ```csharp
    static bool IsDomesticToy(FooProduct element) => element.Type.Equals("TOY", StringComparison.OrdinalIgnoreCase)
            && element.CountryOfOrigin.Equals("PL", StringComparison.OrdinalIgnoreCase);
    ```
3. Register TaxRule for registry instance:
    ```csharp
    var taxRule = new TaxRule<FooProduct>()
    {
        Name = "Domestic Toys",
        Condition = IsDomesticToy,
        TaxValue = 5
    };
    taxRegistry.RegisterRule(taxRule);
    ```
4. Ask to match Tax rule for given element:
    ```csharp
    var product = new FooProduct
    {  
        Name = "Radio controlled car",  
        Type = "TOY",  
        CountryOfOrigin = "PL"  
    };  
    var foundTaxRule = taxRegistry.GetRule(product);
    // foundTaxRule => TaxRule "Domestic Toys" 
    ```