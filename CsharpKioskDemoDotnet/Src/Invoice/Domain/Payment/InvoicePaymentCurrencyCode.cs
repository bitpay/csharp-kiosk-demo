// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Shared;

namespace CsharpKioskDemoDotnet.Invoice.Domain.Payment;

public class InvoicePaymentCurrencyCode
{
    public long Id { get; init; }
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

#pragma warning disable CS8618
    internal InvoicePaymentCurrencyCode()
#pragma warning restore CS8618
    {
    }
}