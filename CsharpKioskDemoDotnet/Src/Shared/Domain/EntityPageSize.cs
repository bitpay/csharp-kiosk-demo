namespace CsharpKioskDemoDotnet.Shared.Domain;

public class EntityPageSize
{
    private const int DefaultValue = 10;
    private readonly int? _value;

    public EntityPageSize(int? value)
    {
        _value = value;
    }

    public int Value => GetValue();

    private int GetValue()
    {
        return _value ?? DefaultValue;
    }
}