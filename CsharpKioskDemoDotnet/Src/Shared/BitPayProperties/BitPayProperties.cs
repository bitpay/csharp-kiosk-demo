// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Shared.BitPayProperties;

public class BitPayProperties
{
    public Design Design { get; set; } = new();
    public string? NotificationEmail { get; set; }

    public string? Currency
    {
        get
        {
            return Design.Currency;
        }
    }

    public IReadOnlyList<Field> Fields
    {
        get
        {
            return Design.Fields;
        }
    }

    public string Mode { 
        get {
            return Design.Mode;
        }
    }

    public decimal MaxPrice { 
        get {
            return Design.MaxPrice;
        }
    }
}