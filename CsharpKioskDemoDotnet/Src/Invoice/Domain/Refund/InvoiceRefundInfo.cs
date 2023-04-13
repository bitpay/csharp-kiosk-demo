/*
 * Copyright 2023 BitPay.
 * All rights reserved.
 */

using CsharpKioskDemoDotnet.Shared;

namespace CsharpKioskDemoDotnet.Invoice.Domain.Refund;

public class InvoiceRefundInfo
{
    public long Id { get; init; }
    [FieldExcludedFromSerialization]
    private InvoiceRefund InvoiceRefund { get; }
    public string? SupportRequest { get; set; }
    public string CurrencyCode { get; set; }
    public ICollection<InvoiceRefundInfoAmount> InvoiceRefundInfoAmounts { get; } = new List<InvoiceRefundInfoAmount>();

    public InvoiceRefundInfo(
        InvoiceRefund invoiceRefund,
        string currencyCode
    )
    {
        InvoiceRefund = invoiceRefund;
        CurrencyCode = currencyCode;
    }
    
#pragma warning disable CS8618
    internal InvoiceRefundInfo()
#pragma warning restore CS8618
    {
    }

    public void AddRefundInfoAmounts(ICollection<InvoiceRefundInfoAmount> invoiceRefundInfoAmounts)
    {
        foreach (var item in invoiceRefundInfoAmounts)
        {
            InvoiceRefundInfoAmounts.Add(item);
        }
    }
}