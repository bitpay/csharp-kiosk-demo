namespace CsharpKioskDemoDotnet.Shared.Domain;

public class EntityPageNumber
{
    private const int DefaultValue = 1;
    private readonly int? _value;

    public EntityPageNumber(int? value)
    {
        _value = value;
    }

    public int Value => GetValue();

    private int GetValue()
    {
        return _value ?? DefaultValue;
    }
}