namespace RRegistry.Tests.ProductRegistry;

/// <summary>Intended for tests only</summary>
public sealed class FooProduct
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string CountryOfOrigin { get; set; } = string.Empty;

    public static FooProduct CreateDummy()
    {
        return new()
        {
            Name = "Toy Car",
            Type = "TOY",
            CountryOfOrigin = "PL"
        };
    }
}