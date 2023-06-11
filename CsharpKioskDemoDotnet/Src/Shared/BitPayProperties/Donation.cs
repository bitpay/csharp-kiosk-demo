// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Shared.BitPayProperties
{
    public class Donation
    {
        public IReadOnlyList<decimal> Denominations { get; set; } = new List<decimal>();
        public bool EnableOther { get; set; } = true;
        public string FooterText { get; set; } = null!;
        public string ButtonSelectedBgColor { get; set; } = null!;
        public string ButtonSelectedTextColor { get; set; } = null!;
        public decimal MaxPrice { 
            get {
                return Denominations.Max();
            }
        }
    }
}