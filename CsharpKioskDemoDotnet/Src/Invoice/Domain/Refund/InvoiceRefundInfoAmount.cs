// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Shared;

namespace CsharpKioskDemoDotnet.Invoice.Domain.Refund;

public class InvoiceRefundInfoAmount
{
    public long Id { get; init; }
    [FieldExcludedFromSerialization]
    public InvoiceRefundInfo InvoiceRefundInfo { get; set; }
    public string CurrencyCode { get; set; }
    public decimal? Amount { get; set; }

    public InvoiceRefundInfoAmount(
        InvoiceRefundInfo invoiceRefundInfo,
        string currencyCode
    )
    {
        InvoiceRefundInfo = invoiceRefundInfo;
        CurrencyCode = currencyCode;
    }

#pragma warning disable CS8618
    internal InvoiceRefundInfoAmount()
#pragma warning restore CS8618
    {
    }
}