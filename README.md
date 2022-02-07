# Tax handler
Generic Tax handler, with user specified Tax rule registry.  

I frequently required to select Tax basing on some properties of element:
- for invoice: issuer country & element type 
- for product: type, country of origin, etc.

This project is an approach to create configurable Tax handler.

## How to use:
1. Init Tax handler for given element type:
    ```csharp
            var taxHandler = new TaxHandler<FooProduct>();
    ```
2. Create methods / Predicates that will serve as conditions:
    ```csharp
    static bool NonEmptyReference(FooDocument element) => !string.IsNullOrEmpty(element.Reference);  
    static bool ProductIsDomesticToy(FooProduct element) => element.Type.Equals("TOY", StringComparison.OrdinalIgnoreCase)
            && element.CountryOfOrigin.Equals("PL", StringComparison.OrdinalIgnoreCase);
    ```
3. Register TaxRule for handler instance:
    ```csharp
    var taxRule = new TaxRule<FooProduct>()
    {
        Name = "Domestic Toys",
        Condition = ProductIsDomesticToy,
        TaxValue = 5
    };
    taxHandler.RegisterRule(taxRule);
    ```
4. Ask to match Tax rule for given element:
    ```csharp
    var product = new FooProduct
    {  
        Name = "Radio controlled car",  
        Type = "TOY",  
        CountryOfOrigin = "PL"  
    };  
    var foundTaxRule = taxHandler.GetRule(product);
    // foundTaxRule => TaxRule "Domestic Toys" 
    ```

## Todo
- set default tax
- return all applicable taxes instead of first one

