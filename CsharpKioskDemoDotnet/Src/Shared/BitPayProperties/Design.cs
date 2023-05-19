// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Shared.BitPayProperties;

public class Design
{
    public Hero Hero { get; set; } = new();
    public PosData PosData { get; set; } = new();
    public Donation Donation { get; set; } = new();
    public string Logo { get; set; } = null!;
    public string Mode { get; set; } = "standard";

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
    public decimal MaxPrice { 
        get {
            return Donation.MaxPrice;
        }
    }
}