namespace TaxRegistry.DataGenerator;

/// <summary>Intended for tests only</summary>
public sealed class FooDocument
{
    public string? Reference { get; set; }
    public decimal? Amount { get; set; }
    public string? Currency { get; set; }
    public string? Unit { get; set; }

    public static FooDocument CreateDummy()
    {
        return new()
        {
            Reference = "123456",
            Amount = 100.00M,
            Currency = "PLN",
            Unit = "PL01"
        };
    }
}