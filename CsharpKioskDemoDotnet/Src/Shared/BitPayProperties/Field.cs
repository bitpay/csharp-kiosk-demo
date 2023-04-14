namespace CsharpKioskDemoDotnet.Shared.BitPayProperties;

public class Field
{
    public string? Type { get; set; }
    public bool Required { get; set; }
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Label { get; set; }
    public List<Option> Options { get; set; } = new();
    public string? Currency { get; set; } = "USD";

    public static Field CreatePriceField()
    {
        return new Field
        {
            Type = "price",
            Required = true,
            Id = "price",
            Name = "price"
        };
    }
}