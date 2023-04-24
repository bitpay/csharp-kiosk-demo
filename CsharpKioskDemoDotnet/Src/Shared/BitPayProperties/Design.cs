// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Shared.BitPayProperties;

public class Design
{
    public Hero Hero { get; set; } = new();
    public string Logo { get; set; } = null!;
    public PosData PosData { get; set; } = new();

    public string? Currency
    {
        get
        {
            return PosData.GetCurrency();
        }
    }

    public IReadOnlyList<Field> Fields
    {
        get
        {
            return PosData.Fields;
        }
    }
}