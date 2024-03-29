// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Shared.BitPayProperties;

public class PosData
{
    private IReadOnlyList<Field> _fields = new List<Field>();
    public IReadOnlyList<Field> Fields
    {
        get => _fields;
        set => _fields = GetPreparedFields(value);
    }

    private IReadOnlyList<Field> GetPreparedFields(IReadOnlyCollection<Field>? fields)
    {
        if (fields == null)
        {
            return new List<Field>();
        }

        var modifiableFieldCollection = new List<Field>(fields);
        if (!HasPriceField(modifiableFieldCollection))
        {
            modifiableFieldCollection.Add(Field.CreatePriceField());
        }

        return modifiableFieldCollection;
    }

    private bool HasPriceField(ICollection<Field> fields)
    {
        return fields.Any(field => "price" == field.Name);
    }

    public string? GetCurrency()
    {
        return (from field in Fields where "price" == field.Type select field.Currency).FirstOrDefault();
    }
}