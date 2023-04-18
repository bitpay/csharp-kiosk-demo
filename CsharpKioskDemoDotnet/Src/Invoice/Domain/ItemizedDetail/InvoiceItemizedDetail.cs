// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Shared;

namespace CsharpKioskDemoDotnet.Invoice.Domain.ItemizedDetail;

public class InvoiceItemizedDetail
{
    public long Id { get; }
    [FieldExcludedFromSerialization]
    public Invoice Invoice { get; set; }
    public double? Amount { get; set; }
    public string? Description { get; set; }
    public bool? IsFee { get; set; }

    public InvoiceItemizedDetail(Invoice invoice)
    {
        Invoice = invoice;
    }

#pragma warning disable CS8618
    internal InvoiceItemizedDetail()
#pragma warning restore CS8618
    {
    }
}