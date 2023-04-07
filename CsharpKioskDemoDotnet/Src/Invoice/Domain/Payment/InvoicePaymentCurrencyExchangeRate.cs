/*
 * Copyright 2023 BitPay.
 * All rights reserved.
 */

using CsharpKioskDemoDotnet.Shared;

namespace CsharpKioskDemoDotnet.Invoice.Domain.Payment;

public class InvoicePaymentCurrencyExchangeRate
{
    public long Id { get; }
    [FieldExcludedFromSerialization]
    public InvoicePaymentCurrency PaymentCurrency { get; set; }
    public string CurrencyCode { get; set; }
    public string Rate { get; set; }

    public InvoicePaymentCurrencyExchangeRate(
        InvoicePaymentCurrency paymentCurrency,
        string currencyCode,
        string rate
    )
    {
        PaymentCurrency = paymentCurrency;
        CurrencyCode = currencyCode;
        Rate = rate;
    }
    
    internal InvoicePaymentCurrencyExchangeRate()
    {
    }
}