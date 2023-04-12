namespace CsharpKioskDemoDotnet.Shared.BitPayProperties;

public class BitPayProperties
{
    public Design Design { get; set; }
    public string? NotificationEmail { get; set; }

    public string? GetCurrency()
    {
        return Design.GetCurrency();
    }

    public List<Field> GetFields()
    {
        return Design.GetFields();
    }
}