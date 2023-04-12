/*
 * Copyright 2023 BitPay.
 * All rights reserved.
 */

namespace CsharpKioskDemoDotnet.Invoice.Domain.Refund;

public class InvoiceRefundInfo
{
    public long Id { get; }
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
    
    internal InvoiceRefundInfo()
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