namespace CsharpKioskDemoDotnet.Shared.BitPayProperties;

public class Design
{
    public Hero Hero { get; set; } = new();
    public string Logo { get; set; } = null!;
    public PosData PosData { get; set; } = new();

    public string? GetCurrency()
    {
        return PosData.GetCurrency();
    }

    public List<Field> GetFields()
    {
        return PosData.Fields;
    }
}