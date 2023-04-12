/*
 * Copyright 2023 BitPay.
 * All rights reserved.
 */

using CsharpKioskDemoDotnet.Shared;

namespace CsharpKioskDemoDotnet.Invoice.Domain.Payment;

public class InvoicePaymentCurrencyCode
{
    public long Id { get; }
    [FieldExcludedFromSerialization]
    private InvoicePaymentCurrency PaymentCurrency { get; }
    private string PaymentCode { get; }
    public string? PaymentCodeUrl { get; set; }

    public InvoicePaymentCurrencyCode(
        InvoicePaymentCurrency paymentCurrency,
        string paymentCode
    )
    {
        PaymentCurrency = paymentCurrency;
        PaymentCode = paymentCode;
    }
    
    internal InvoicePaymentCurrencyCode()
    {
    }
}