/*
 * Copyright 2023 BitPay.
 * All rights reserved.
 */

using CsharpKioskDemoDotnet.Shared;

namespace CsharpKioskDemoDotnet.Invoice.Domain.Refund;

public class InvoiceRefundInfoAmount
{
    public long Id { get; }
    [FieldExcludedFromSerialization]
    public InvoiceRefundInfo InvoiceRefundInfo { get; set; }
    public string CurrencyCode { get; set; }
    public double? Amount { get; set; }

    public InvoiceRefundInfoAmount(
        InvoiceRefundInfo invoiceRefundInfo,
        string currencyCode
    )
    {
        InvoiceRefundInfo = invoiceRefundInfo;
        CurrencyCode = currencyCode;
    }
    
    internal InvoiceRefundInfoAmount()
    {
    }
}